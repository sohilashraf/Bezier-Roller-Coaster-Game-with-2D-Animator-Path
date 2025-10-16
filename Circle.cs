using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace Graphics1_project
{
    internal class Circle
    {
       public DDA line = new DDA();
        public int Rad;
        public int XC;
        public int YC;
        public float thRadian;
        public List<Point> CirclePoints = new List<Point>();

        public void Drawcircle(Graphics g,int xx,int yy)
        {
           // CirclePoints.Clear();
            for (float i = 0; i <= (2 * Math.PI); i += (float)((2 * Math.PI) / 360))
            {
                //(float)(i * Math.PI / 180);

                float x = (float)(Rad * Math.Cos(i));
                float y = (float)(Rad * Math.Sin(i));

                x += XC;
                y += YC;
               // CirclePoints.Add(new Point((int)x, (int)y));
                g.FillEllipse(Brushes.Red, x - xx, y -yy, 5, 5);
            }
        }
        public List<Point> calcCircle()
        {
            CirclePoints.Clear();
            for (float i = (float)(Math.PI / 2); i > 0; i -= (float)((2 * Math.PI) / 90))
            {
                //(float)(i * Math.PI / 180);

                float x = (float)(Rad * Math.Cos(i));
                float y = (float)(Rad * Math.Sin(i));

                x += XC;
                y += YC;
                CirclePoints.Add(new Point((int)x, (int)y));

            }
            for (float i = (float)(2 * Math.PI); i > (float)((3 * Math.PI) / 2); i -= (float)((2 * Math.PI) / 90))
            {
                float x = (float)(Rad * Math.Cos(i));
                float y = (float)(Rad * Math.Sin(i));

                x += XC;
                y += YC;
                CirclePoints.Add(new Point((int)x, (int)y));
            }
            for (float i = (float)(3 * Math.PI/2); i > (float)( Math.PI ); i -= (float)((2 * Math.PI) / 90))
            {
                float x = (float)(Rad * Math.Cos(i));
                float y = (float)(Rad * Math.Sin(i));

                x += XC;
                y += YC;
                CirclePoints.Add(new Point((int)x, (int)y));
            }
            for (float i = (float)(Math.PI); i > (float)(Math.PI/2); i -= (float)((2 * Math.PI) / 90))
            {
                float x = (float)(Rad * Math.Cos(i));
                float y = (float)(Rad * Math.Sin(i));

                x += XC;
                y += YC;
                CirclePoints.Add(new Point((int)x, (int)y));
            }

            return CirclePoints;
        }
        public PointF Getnextpoint(int theta)
        {

            PointF p = new PointF();

            thRadian = (float)(theta * Math.PI / 180);

            p.X = (float)(Rad * Math.Cos(thRadian)) + XC;
            p.Y = (float)(Rad * Math.Sin(thRadian)) + YC;
            return p;
        }


    }
}
