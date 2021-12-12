using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using GrafikaDLL;

/*
 * érintő mozgatásnál a végpont is mozogjon
 * a weight legyen scrollbarral mozgatható (globáliasan, advanced: ívenként)
 * checkbox -> zárt görbe legyen
 * érintő vektor ellentétes vektora is látszódjon, azzal is lehessen mozgatni
 * Zárt görbe esetén fillezzük ki a görbét
 */
namespace GrafikaAlap
{
    public partial class Form1 : Form
    {
        private Graphics g;
        readonly List<Vector2> V = new List<Vector2>();
        readonly List<double> weights = new List<double>();
        const double DEFAULT_WEIGHT = 2.0;

        int grabbed = -1;
        bool isGrabByOppositeTangent = false;
        double weight = DEFAULT_WEIGHT;
        const double dw = 0.2;
        readonly Bitmap bmp;

        public Form1()
        {
            InitializeComponent();
            bmp = new Bitmap(canvas.Width, canvas.Height);

            canvas.Image = bmp;
            canvas.MouseWheel += canvas_MouseWheel;
            label1.Text = $"Global weight: {weight}";
        }


        private void canvas_Paint(object sender, PaintEventArgs e)
        {
            if (g == null)
            {
                g = Graphics.FromImage(bmp);
            }

            g.Clear(Color.White);
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;

            for (int i = 0; i < V.Count - 3; i += 2)
            {
                g.DrawHermiteArc(new Pen(Color.Blue, 2f),
                    new HermiteArc(V[i], V[i + 2], 
                    V[i + 1] - V[i], 
                    V[i + 3] - V[i + 2], 
                    isWeightGlobal.Checked ? weight : weights[i / 2]));
            }

            if (isClosedArc.Checked && V.Count > 2)
            {
                g.DrawHermiteArc(new Pen(Color.Blue, 2f),
                    new HermiteArc(V[V.Count - 2], V[0],
                    V[V.Count - 1] - V[V.Count - 2], 
                    V[1] - V[0],
                    isWeightGlobal.Checked ? weight : weights[weights.Count - 1]));

                if (isFilled.Checked && grabbed == -1)
                {
                    double minX = V.Where((vec, i) => i % 2 == 0).Select((vec) => vec.x).Min();
                    double maxX = V.Where((vec, i) => i % 2 == 0).Select((vec) => vec.x).Max();
                    double minY = V.Where((vec, i) => i % 2 == 0).Select((vec) => vec.y).Min();
                    double maxY = V.Where((vec, i) => i % 2 == 0).Select((vec) => vec.y).Max();

                    int centerX = (int)((minX + maxX) / 2);
                    int centerY = (int)((minY + maxY) / 2);

                    bmp.FillStack4(Color.White, Color.Cyan, centerX, centerY);
                }
            }

            for (int i = 0; i < V.Count - 1; i += 2)
            {
                g.DrawLine(Pens.Black, V[i], V[i + 1]);
                Vector2 diff = V[i + 1] - V[i];
                g.DrawLine(Pens.Black, V[i], V[i] - diff);
                g.DrawPoint(Pens.Black, Brushes.White, V[i], 5);
            }

            canvas.Image = bmp;
        }

        private void canvas_MouseDown(object sender, MouseEventArgs e)
        {
            for (int i = 0; i < V.Count - 1; i += 2)
            {
                if (e.CloseEnough(V[i]))
                {
                    grabbed = i;
                    isGrabByOppositeTangent = false;
                    break;
                }

                if (e.CloseEnough(V[i + 1]))
                {
                    grabbed = i + 1;
                    isGrabByOppositeTangent = false;
                    break;
                }

                Vector2 diff = V[i + 1] - V[i];
                Vector2 oppositeTangent = V[i] - diff;
                if (e.CloseEnough(oppositeTangent))
                {
                    grabbed = i + 1;
                    isGrabByOppositeTangent = true;
                    break;
                }
            }

            if (grabbed == -1)
            {
                V.Add(new Vector2(e.X, e.Y));
                V.Add(new Vector2(e.X, e.Y));
                weights.Add(DEFAULT_WEIGHT);
                updateWeights();                
                grabbed = V.Count - 1;
                canvas.Invalidate();
            }
        }

        private void canvas_MouseMove(object sender, MouseEventArgs e)
        {
            if (grabbed != -1)
            {
                Vector2 v = V[grabbed];

                if (!isGrabByOppositeTangent)
                {
                    V[grabbed] = new Vector2(e.X, e.Y);

                    if (grabbed % 2 == 0 && grabbed < V.Count - 1)
                    {
                        Vector2 next = V[grabbed + 1];
                        Vector2 diff = next - v;
                        V[grabbed + 1] = V[grabbed] + diff;
                    }
                }
                else if (grabbed > 0)
                {
                    Vector2 diff0 = v - V[grabbed - 1] - V[grabbed - 1];
                    Vector2 diff = diff0 + new Vector2(e.X, e.Y);
                    V[grabbed] = v - diff;
                }

                canvas.Invalidate();
            }
        }

        private void canvas_MouseUp(object sender, MouseEventArgs e)
        {
            grabbed = -1;
        }

        private void isClosed_CheckedChanged(object sender, EventArgs e)
        {
            isFilled.Enabled = isClosedArc.Checked;
            canvas.Invalidate();
        }

        private void canvas_MouseWheel(object sender, MouseEventArgs e)
        {
            if (isWeightGlobal.Checked)
            {
                weight += e.Delta > 0 ? dw : -dw;
                label1.Text = $"Global weight: {weight}";
            }
            else
            {
                if (weights.Count == 0) return;

                Vector2 cursor = new Vector2(e.X, e.Y);
                int index = 0;

                double diffSquared(double x, double y) => (x - y) * (x - y);
                double distance(Vector2 p0, Vector2 p1) =>
                    Math.Sqrt(diffSquared(p0.x, p1.x) + diffSquared(p0.y, p1.y));

                for (int i = 1; i < V.Count; i += 2)
                {
                    if (distance(V[i], cursor) < distance(V[index], cursor))
                    {
                        index = i;
                    }
                }

                weights[index / 2] += e.Delta > 0 ? dw : -dw;
                updateWeights();
            }

            canvas.Invalidate();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            V.Clear();
            weights.Clear();
            canvas.Invalidate();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (V.Count > 0)
            {
                V.RemoveAt(V.Count - 1);
                V.RemoveAt(V.Count - 1);

                weights.RemoveAt(weights.Count - 1);
                updateWeights();
            }
            canvas.Invalidate();
        }

        private void isWeightGlobal_CheckedChanged(object sender, EventArgs e)
        {
            listBox1.Enabled = !isWeightGlobal.Checked;
            label1.Enabled = isWeightGlobal.Checked;
            canvas.Invalidate();
        }

        private void updateWeights()
        {
            listBox1.Items.Clear();
            foreach (var item in weights)
            {
                listBox1.Items.Add($"{item}");
            }
        }

        private void isFilled_CheckedChanged(object sender, EventArgs e)
        {
            canvas.Invalidate();
        }

    }
}
