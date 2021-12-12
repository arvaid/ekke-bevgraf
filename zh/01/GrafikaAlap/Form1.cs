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
        Rectangle r1, r2;
        int x1, y1;
        int with1, height1;
        int x2, y2;
        int with2, height2;

        int catched;
        bool resize;
        int dx, dy;

        public Form1()
        {
            InitializeComponent();
            x1 = 100;
            y1 = 100;
            with1 = 200;
            height1 = 100;

            x2 = 120;
            y2 = 120;
            with2 = 150;
            height2 = 50;

            resize = false;
            catched = -1;
            
            r1 = new Rectangle(x1,y1, with1, height1);
            r2 = new Rectangle(x2, y2, with2, height2);
        }

        private void canvas_Paint(object sender, PaintEventArgs e)
        {
            g = e.Graphics;

            g.DrawRectangle(Pens.Blue, r1);
            g.DrawRectangle(Pens.Red, r2);
        }
        private void canvas_MouseDown(object sender, MouseEventArgs e)
        {
            switch (e.Button)
            {
                case MouseButtons.Left:
                    
                    if (e.CloseEnough(new PointF(r1.X, r1.Y)))
                    {
                        catched = 1;
                        dx = e.X - r1.X;
                        dy = e.Y - r1.Y;
                        return;
                    }
                    else if (e.CloseEnough(new PointF(r1.X + r1.Width, r1.Y + r1.Height)))
                    {
                        catched = 1;
                        resize = true;
                        return;
                    }


                    else if (e.CloseEnough(new PointF(r2.X, r2.Y)))
                    {
                        catched = 2;
                        dx = e.X - r2.X;
                        dy = e.Y - r2.Y;
                        return;
                    }
                    else if (e.CloseEnough(new PointF(r2.X + r2.Width, r2.Y + r2.Height)))
                    {
                        catched = 2;
                        resize = true;
                        return;
                    }


                    canvas.Invalidate();


                break;
                case MouseButtons.Middle: break;
                case MouseButtons.Right: break;
                default: break;
            }
        }
        private void canvas_MouseMove(object sender, MouseEventArgs e)
        {
            switch (e.Button)
            {
                case MouseButtons.Left:
                    if (catched == 1)
                    {
                        if (resize)
                        {
                            Rectangle rectangle = r1;
                            rectangle.Width = e.X - rectangle.X;
                            rectangle.Height = e.Y - rectangle.Y;
                            if (rectangle.X < r2.X && rectangle.Y < r2.Y && rectangle.Width > r2.Width + (r2.X - rectangle.X) && rectangle.Height > r2.Height + (r2.Y - rectangle.Y))
                            {
                                r1 = rectangle;
                            }
                        }
                        else
                        {
                            Rectangle rectangle = r1;
                            rectangle.X = e.X - dx;
                            rectangle.Y = e.Y - dy;
                            if (rectangle.X < r2.X && rectangle.Y <r2.Y && rectangle.Width > r2.Width + (r2.X - rectangle.X) && rectangle.Height > r2.Height + (r2.Y - rectangle.Y))
                            {
                                r1 = rectangle;
                            }
                        }
                    }

                    else if (catched == 2)
                    {
                        if (resize)
                        {
                            Rectangle rectangle = r2;
                            rectangle.Width = e.X - rectangle.X;
                            rectangle.Height = e.Y - rectangle.Y;
                            if(r1.X < rectangle.X && r1.Y < rectangle.Y && r1.Width > rectangle.Width + (r2.X - r1.X) && r1.Height > rectangle.Height + (r2.Y - r1.Y))
                            {
                                r2 = rectangle;
                            }
                            
                        }
                        else
                        {
                            Rectangle rectangle = r2;
                            rectangle.X = e.X - dx;
                            rectangle.Y = e.Y - dy;

                            if (r1.X < rectangle.X && r1.Y < rectangle.Y && r1.Width > rectangle.Width + (r2.X - r1.X) && r1.Height > rectangle.Height + (r2.Y - r1.Y))
                            {
                                r2 = rectangle;
                            }
                        }
                    }

                    canvas.Invalidate();


                    break;
                case MouseButtons.Middle: break;
                case MouseButtons.Right: break;
                default: break;
            }
        }
        private void canvas_MouseUp(object sender, MouseEventArgs e)
        {
            switch (e.Button)
            {
                case MouseButtons.Left:
                    catched = -1;
                    resize = false;
                    canvas.Invalidate();
                    break;
                case MouseButtons.Middle: break;
                case MouseButtons.Right: break;
                default: break;
            }
        }
    }
}
