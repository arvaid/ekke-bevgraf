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

namespace GrafikaAlap
{
    public partial class Form1 : Form
    {
        Graphics g;
        Random rnd = new Random();
        List<Rectangle> windows;
        Line[] lines = new Line[500];
        //Generáljanak minden szakasznak saját színt
        Color[] colors = new Color[500];

        int gotcha = -1;
        float dx, dy;

        public Form1()
        {
            InitializeComponent();
            windows = new List<Rectangle>()
            {
                new Rectangle(30, 40, 100, 130),
                new Rectangle(150, 200, 70, 160),
                new Rectangle(510, 10, 40, 350)
            };

            for (int i = 0; i < lines.Length; i++)
            {
                lines[i] = new Line(rnd.Next(canvas.Width), rnd.Next(canvas.Height),
                                    rnd.Next(canvas.Width), rnd.Next(canvas.Height));
                colors[i] = Color.FromArgb(rnd.Next(255), 0, rnd.Next(255));
            }
        }

        private void canvas_Paint(object sender, PaintEventArgs e)
        {
            g = e.Graphics;

            for (int i = 0; i < windows.Count; i++)
                g.DrawRectangle(Pens.Black, windows[i]);

            for (int i = 0; i < lines.Length; i++)
                for (int j = 0; j < windows.Count; j++)
                    g.Clip(new Pen(colors[i]), windows[j], lines[i]);
        }
        private void canvas_MouseDown(object sender, MouseEventArgs e)
        {
            //Ha valamelyik téglalapot elkapom -> mozgatás
            //Egyébként.
            //  Létrehozok egy új 0x0-es téglalapot
            //  ami rögtön meg is van fogva
            for (int i = 0; i < windows.Count; i++)
            {
                if (e.Catch(windows[i], out dx, out dy))
                {
                    gotcha = i;
                    break;
                }
            }
        }
        private void canvas_MouseMove(object sender, MouseEventArgs e)
        {
            //Ha valami meg van fogva (külön kell kezelni, hogy mozgatás miatt, vagy méretezés miatt)
            //  mozgatom, vagy méretezem
            if (gotcha != -1)
            {
                var w = windows[gotcha];

                windows[gotcha] = new Rectangle(
                    (int)(e.X - dx).Clamp(0, canvas.Width - w.Width - 1), 
                    (int)(e.Y - dy).Clamp(0, canvas.Height - w.Height - 1), 
                    w.Width,
                    w.Height);

                canvas.Invalidate();
            }
        }
        private void canvas_MouseUp(object sender, MouseEventArgs e)
        {
            gotcha = -1;
        }
    }
}
