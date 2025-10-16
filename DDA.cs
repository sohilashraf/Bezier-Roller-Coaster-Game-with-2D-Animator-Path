using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Graphics1_project
{
    internal class DDA
    {
        public List<Point> Points = new List<Point>();
        public float X, Y;
        public float Xe, Ye;
        float dy, dx, m;
        public float cx, cy;
        int speed = 10;
       
        public void calc()
        {
            dy = Ye - Y;
            dx = Xe - X;
            m = Math.Abs(dy / dx);
            cx = X;
            cy = Y;
        }
        public List<Point> CalcNextPoint()
        {
           Points.Clear();
           calc();
           while(cx<Xe)
            {

                if (Math.Abs(dx) >= Math.Abs(dy))
                {
                    if (X < Xe)
                    {
                        cx += speed;
                        cy += m * speed;
                       

                    }
                    else
                    {
                        cx -= speed;
                        cy -= m * speed;
                        if (cx <= Xe)
                        {
                            break;
                        }
                    }
                }
                //else
                //{
                //    if (Y < Ye)
                //    {
                //        cy += speed;
                //        cx += 1 / m * speed;
                //        if (cy >= Ye)
                //        {
                //            return Points;
                //        }
                //    }
                //    else
                //    {
                //        cy -= speed;
                //        cx -= 1 / m * speed;
                //        if (cy <= Ye)
                //        {
                //            return Points;
                //        }
                //    }

                //}
                Point pnn = new Point();
                pnn.X =(int) cx;
                pnn.Y = (int)cy;
                Points.Add(pnn);


            }

            return Points;
        }

    }
}
