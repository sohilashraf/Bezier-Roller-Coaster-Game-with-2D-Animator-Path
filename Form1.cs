using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Graphics1_project
{
    public class CAdvActor
    {
        public Rectangle rcDst, rcSrc;
        public Bitmap img;
    }
    class CActor
    {
        public int x, y, flagR, dy = -1, dxE = -1;
        public List<Bitmap> imgs = new List<Bitmap>();
        public int iframe = 0;
        public int ctTick = 1;
    }
    class objects
    {
        public int index;
        public string type;
        Point p;
    }   
    public partial class Form1 : Form
    {
        Bitmap off;

        int z = 0;
        int flagSpace = 0;
        bool left, right, s, d, space;
        int ctHero = 0;
        int start_x = 250;
        int value = 0;
        int var = 1;
        int Ycurve = 40;

        string type = "";

        CActor circle = new CActor();
        List<DDA> lines=new List<DDA>();
        List<Circle>circles=new List<Circle>();
        List<BezierCurve> curves = new List<BezierCurve>();
        List<Point> Pp;
        List<CAdvActor> LBackGround = new List<CAdvActor>();
        List<CActor> LHero = new List<CActor>();
        List<Point> points = new List<Point>();
        List<objects> o = new List<objects>();
        Timer tt = new Timer();

        public Form1()
        {
            this.WindowState = FormWindowState.Maximized;
            this.Load += Form1_Load;
            this.Paint += Form1_Paint;
            KeyDown += Form1_KeyDown;
            KeyUp += Form1_KeyUp;
            tt.Tick += Tt_Tick;
            tt.Start();
        }

        private void Tt_Tick(object sender, EventArgs e)
        {
            if (!left && !right)
            {
                LHero[0].iframe++;
                if (LHero[0].iframe >= 10)
                {
                    LHero[0].iframe = 1;
                }
            }
            if (left)
            {
                LBackGround[0].rcSrc.X -= 1 * 15;
                if (LBackGround[0].rcSrc.X < 0)
                {
                    LBackGround[0].rcSrc.X = 0;
                }
            }
            if (right)
            {
               
                if (LBackGround[0].rcSrc.X + this.Width <= LBackGround[0].img.Width)
                {
                    LBackGround[0].rcSrc.X += 1 * 15;
                }
               
            } 
            if(flagSpace==1&&z<points.Count)
            {
               
                if (circle.x >= this.Width / 2)
                {
                    if (LBackGround[0].rcSrc.X + this.Width <= LBackGround[0].img.Width)
                    {
                        if (o[z].type=="line")
                        {
                            if (points[z].X > points[z - 1].X)
                            {
                                LBackGround[0].rcSrc.X += 1 * 7+var;
                            }
                        }
                       else
                        {
                            if (points[z].X > points[z - 1].X)
                            {
                                LBackGround[0].rcSrc.X += 1 * 3+var;
                            }

                        }
                    }
                }
                circle.x = points[z].X;
                circle.y = points[z].Y;                
                z+=var;
            }
            DrawDubb(this.CreateGraphics());
        }
        private void Form1_KeyUp(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Left:
                    left = false;
                    LHero[0].iframe = 8;
                    break;
                case Keys.Right:
                    right = false;
                    ctHero = 0;
                    LHero[0].iframe = 8;
                    break;
            }
        }
        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Left:
                    left = true;    
                    
                    break;
                case Keys.Right:
                    right = true;
                    if (ctHero == 0)
                    {
                        LHero[0].iframe = 10;
                        ctHero = 1;
                    }
                    break;
                case Keys.L:
                    if (start_x >= this.Width / 2)
                    {
                        if (LBackGround[0].rcSrc.X + this.Width <= LBackGround[0].img.Width)
                        {
                            LBackGround[0].rcSrc.X += 1 * 100;
                        }
                    }
                    DDA pnn = new DDA();
                    pnn.X = (float)start_x;
                    pnn.Y = (float)this.ClientSize.Height - 147;
                    pnn.Xe = start_x +200;
                    pnn.Ye = pnn.Y;
                    start_x = (int)pnn.Xe;
                    type = "line";
                    lines.Add(pnn);
                    List<Point> P = lines[lines.Count-1].CalcNextPoint();
                    foreach (var _ in P)
                    {
                        objects x = new objects();
                        x.type = "line";
                        o.Add(x);
                    }
                    for (int i=0;i<P.Count;i++)
                    {
                        Point pnnn = new Point();
                        pnnn.X = P[i].X;
                        pnnn.Y = P[i].Y;
                        points.Add(pnnn);
                    }
                    break;
                case Keys.B:

                    if (start_x >= this.Width / 2)
                    {
                        if (LBackGround[0].rcSrc.X + this.Width <= LBackGround[0].img.Width)
                        {
                            LBackGround[0].rcSrc.X += 1 * 500;
                        }
                    }
                   
                    BezierCurve obj = new BezierCurve();
                    obj.SetControlPoint(new Point(start_x , this.ClientSize.Height - 147));
                    obj.SetControlPoint(new Point(start_x+ 300 , this.ClientSize.Height - 147));

                    obj.SetControlPoint(new Point(start_x + 200 , this.ClientSize.Height - 900));

                    obj.SetControlPoint(new Point(start_x + 300, this.ClientSize.Height - 147));
                    obj.SetControlPoint(new Point(start_x + 450, this.ClientSize.Height - 147));

                   
                    start_x += 450;
                    curves.Add(obj);
                    type = "curve";
                    List<Point> p = curves[curves.Count - 1].calc();
                    points.AddRange(p);
                    foreach (var _ in p)
                    {
                        objects x = new objects();
                        x.type = "Curve";
                        o.Add(x);
                    }
                    break;

                case Keys.Up:
                    if (type == "line")
                    {
                        DDA lastLine = lines[lines.Count - 1];
                        lastLine.Xe += 100;
                        int oldPointsCount = lastLine.Points.Count;
                        points.RemoveRange(points.Count - oldPointsCount, oldPointsCount);
                        List<Point> newPoints = lastLine.CalcNextPoint();
                        points.AddRange(newPoints);
                        o.RemoveRange(o.Count - oldPointsCount, oldPointsCount);
                        foreach (var _ in newPoints)
                        {
                            objects x = new objects();
                            x.type = "line";
                            o.Add(x);
                        }
                        start_x += 100;
                        if (start_x >= this.Width / 2)
                        {
                            if (LBackGround[0].rcSrc.X + this.Width <= LBackGround[0].img.Width)
                            {
                                LBackGround[0].rcSrc.X += 1 * 100;
                            }
                        }
                    }
                    else if (type == "circle"&&circles.Count>0)
                    {
                            Circle lastCircle = circles[circles.Count - 1];
                            if (lastCircle.Rad < 300)
                            {
                                if (start_x >= this.Width / 2)
                                {
                                    if (LBackGround[0].rcSrc.X + this.Width <= LBackGround[0].img.Width)
                                    {
                                        LBackGround[0].rcSrc.X += 1 * 100;
                                    }
                                }

                                // Remove old points
                                DDA lastLine = lines[lines.Count - 1];
                                DDA lastLine2 = lines[lines.Count - 2];
                                List<Point> oldCirclePoints = lastCircle.CirclePoints;
                                int totalOldPoints = lastLine.Points.Count + lastLine2.Points.Count + oldCirclePoints.Count;
                                points.RemoveRange(points.Count - totalOldPoints, totalOldPoints);
                                o.RemoveRange(o.Count - totalOldPoints, totalOldPoints);

                                // Update start_x and the circle's radius
                                start_x -= lastCircle.Rad * 2;
                                lastCircle.Rad += 50;

                                // Update line positions
                                lastLine2.X = start_x;
                                lastLine2.Xe = start_x + lastCircle.Rad;
                                lastLine.X = start_x + lastCircle.Rad;
                                lastLine.Xe = start_x + lastCircle.Rad * 2;

                                // Update circle's center
                                lastCircle.XC = start_x + lastCircle.Rad - value;
                                lastCircle.YC = (int)((float)this.ClientSize.Height - 155) - lastCircle.Rad;

                                // Calculate new points
                                List<Point> newCirclePoints = lastCircle.calcCircle();
                                start_x += lastCircle.Rad * 2;
                                List<Point> newLine2Points = lastLine2.CalcNextPoint();
                                List<Point> newLinePoints = lastLine.CalcNextPoint();

                                // Add new points to the list
                                points.AddRange(newLine2Points);
                                points.AddRange(newCirclePoints);
                                points.AddRange(newLinePoints);

                                // Add new points to the objects list
                                foreach (var _ in newLine2Points)
                                {
                                    objects x = new objects();
                                    x.type = "line";
                                    o.Add(x);
                                }
                                foreach (var _ in newCirclePoints)
                                {
                                    objects x = new objects();
                                    x.type = "circle";
                                    o.Add(x);
                                }
                                foreach (var _ in newLinePoints)
                                {
                                    objects x = new objects();
                                    x.type = "line";
                                    o.Add(x);
                                }
                            }
                    }
                    else if (type == "curve" && curves.Count > 0)
                    {

                        BezierCurve LastCurve = curves[curves.Count - 1];
                        
                        Point PnnC = new Point();
                        float xx=LastCurve.ControlPoints[2].Y - Ycurve;
                        if(xx>1000*-1)
                        {
                            PnnC.Y = LastCurve.ControlPoints[2].Y - Ycurve;
                            PnnC.X = LastCurve.ControlPoints[2].X;
                            LastCurve.ControlPoints[2] = PnnC;
                            int pp = LastCurve.PointsC.Count;
                            points.RemoveRange(points.Count - pp, pp);
                            List<Point> newPoints = LastCurve.calc();
                            points.AddRange(newPoints);
                            o.RemoveRange(o.Count - pp, pp);
                            foreach (var _ in newPoints)
                            {
                                objects x = new objects();
                                x.type = "curve";
                                o.Add(x);
                            }
                            if (LastCurve.ControlPoints[2].Y - Ycurve > 0)
                            {
                                Ycurve = 0;
                            }
                            Ycurve += 40;
                        }
                    }
                    break;
                case Keys.Down:
                    if (type == "line" &&lines.Count>0)
                    {
                        DDA lastLine = lines[lines.Count - 1];
                        lastLine.Xe -= 100;
                        int oldPointsCount = lastLine.Points.Count;
                        points.RemoveRange(points.Count - oldPointsCount, oldPointsCount);
                        o.RemoveRange(o.Count - oldPointsCount, oldPointsCount);
                        List<Point> newPoints = lastLine.CalcNextPoint();
                        points.AddRange(newPoints);
                        foreach (var _ in newPoints)
                        {
                            objects x = new objects();
                            x.type = "line";
                            o.Add(x);
                        }
                        start_x -= 100;
                        if (start_x <= this.Width / 2)
                        {
                            LBackGround[0].rcSrc.X -= 1 * 15;
                            if (LBackGround[0].rcSrc.X < 0)
                            {
                                LBackGround[0].rcSrc.X = 0;
                            }
                        }
                    }
                    else if (type == "circle")
                    {
                        if (circles[circles.Count - 1].Rad > 100)
                        {
                            if (start_x >= this.Width / 2)
                            {
                                if (LBackGround[0].rcSrc.X - this.Width > 0)
                                {
                                    LBackGround[0].rcSrc.X -= 1 * 20;
                                }
                            }

                            Circle lastCircle = circles[circles.Count - 1];

                            DDA lastLine = lines[lines.Count - 1];
                            DDA lastLine2 = lines[lines.Count - 2];
                            List<Point> oldCirclePoints = lastCircle.CirclePoints;
                            int totalOldPoints = lastLine.Points.Count + lastLine2.Points.Count + oldCirclePoints.Count;

                            points.RemoveRange(points.Count - totalOldPoints, totalOldPoints);
                            o.RemoveRange(o.Count - totalOldPoints, totalOldPoints);

                            // Update start_x and the circle's radius
                            start_x -= lastCircle.Rad * 2;
                            lastCircle.Rad -= 50;

                            // Update line positions
                            lastLine2.X = start_x;
                            lastLine2.Xe = start_x + lastCircle.Rad;
                            lastLine.X = start_x + lastCircle.Rad;

                            // Update circle's center
                            lastCircle.XC = start_x + lastCircle.Rad - value;
                            lastCircle.YC = (int)((float)this.ClientSize.Height - 155) - lastCircle.Rad;

                            start_x += lastCircle.Rad * 2;
                            lastLine.Xe = start_x;

                            // Calculate new points
                            List<Point> newCirclePoints = lastCircle.calcCircle();
                            List<Point> newLine2Points = lastLine2.CalcNextPoint();
                            List<Point> newLinePoints = lastLine.CalcNextPoint();

                            // Add new points to the list
                            points.AddRange(newLine2Points);
                            points.AddRange(newCirclePoints);
                            points.AddRange(newLinePoints);

                            // Add new points to the objects list
                            foreach (var _ in newLine2Points)
                            {
                                objects x = new objects();
                                x.type = "line";
                                x.index = circles.Count-1;
                                o.Add(x);
                            }
                            foreach (var _ in newCirclePoints)
                            {
                                objects x = new objects();
                                x.type = "circle";
                                x.index = circles.Count - 1;

                                o.Add(x);
                            }
                            foreach (var _ in newLinePoints)
                            {
                                objects x = new objects();
                                x.type = "line";
                                o.Add(x);
                            }
                        }

                    }
                    else if (type == "curve" && curves.Count > 0)
                    {
                        BezierCurve LastCurve = curves[curves.Count - 1];
                       
                        Point PnnC = new Point();
                        PnnC.Y = LastCurve.ControlPoints[2].Y + Ycurve;
                        PnnC.X = LastCurve.ControlPoints[2].X;
                        LastCurve.ControlPoints[2] = PnnC;
                        Ycurve += 40;
                        int pp = LastCurve.PointsC.Count;
                        points.RemoveRange(points.Count - pp, pp);
                        List<Point> newPoints = LastCurve.calc();
                        points.AddRange(newPoints);
                        o.RemoveRange(o.Count - pp, pp);
                        foreach (var _ in newPoints)
                        {
                            objects x = new objects();
                            x.type = "curve";
                            o.Add(x);
                        }
                        if (LastCurve.ControlPoints[2].Y > this.ClientSize.Height)
                        {
                            Ycurve = 0;
                        }
                    }
                    break;

                case Keys.C:
                    if (start_x >= this.Width / 2)
                    {
                        if (LBackGround[0].rcSrc.X + this.Width <= LBackGround[0].img.Width)
                        {
                            LBackGround[0].rcSrc.X += 1 * 200;
                        }
                    }

                    type = "circle";

                    // Add first line
                    DDA pnn2 = new DDA();
                    pnn2.X = start_x;
                    pnn2.Xe = start_x + 150;
                    pnn2.Y = this.ClientSize.Height - 147;
                    pnn2.Ye = this.ClientSize.Height - 147;
                    lines.Add(pnn2);

                    List<Point> Pp = lines[lines.Count - 1].CalcNextPoint();
                    points.AddRange(Pp);

                    // Add objects for the first line points
                    foreach (var _ in Pp)
                    {
                        objects x = new objects();
                        x.type = "line";
                        x.index = circles.Count - 1;

                        o.Add(x);
                    }

                    // Add circle
                    Circle c = new Circle();
                    c.Rad = 150;
                    c.XC = start_x + c.Rad;
                    c.YC = (int)((float)this.ClientSize.Height - 147) - c.Rad;
                    start_x += c.Rad * 2;

                    DDA pnn3 = new DDA();
                    pnn3.X = start_x - 150;
                    pnn3.Xe = start_x;
                    pnn3.Y = this.ClientSize.Height - 147;
                    pnn3.Ye = this.ClientSize.Height - 147;
                    lines.Add(pnn3);
                    circles.Add(c);

                    List<Point> points2 = circles[circles.Count - 1].calcCircle();
                    points.AddRange(points2);

                    // Add objects for the circle points
                    foreach (var _ in points2)
                    {
                        objects x = new objects();
                        x.type = "circle";
                        o.Add(x);
                    }

                    // Add second line
                    List<Point> Pp2 = lines[lines.Count - 1].CalcNextPoint();
                    points.AddRange(Pp2);

                    // Add objects for the second line points
                    foreach (var _ in Pp2)
                    {
                        objects x = new objects();
                        x.type = "line";
                        o.Add(x);
                    }
                    break;

                case Keys.Space:
                    LBackGround[0].rcSrc.X = 0;
                    z = 0;
                    circle.x = points[0].X;
                    circle.y = points[0].Y;
                  
                    flagSpace = 1;
                    break;
                case Keys.S:
                    var = 2;
                    break;              
            }
        }
        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            DrawDubb(e.Graphics);
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            off = new Bitmap(ClientSize.Width, ClientSize.Height);

            CreateBackGround();
            CreateHero();
        }
        void CreateBackGround()
        {
            CAdvActor pnn = new CAdvActor();
            pnn.img = new Bitmap("image (6) (2).png");
            pnn.rcSrc = new Rectangle(0, 0, this.Width, this.Height -150);
            pnn.rcDst = new Rectangle(pnn.rcDst.X, pnn.rcDst.Y, this.Width, this.Height);
            LBackGround.Add(pnn);
        }
        void CreateHero()
        {
            CActor pnn = new CActor();
            pnn.x = 100;
            pnn.y = this.Height - 310;

            
            for (int i = 1; i <= 10; i++)
            {
                Bitmap img = new Bitmap("Idle (" + i + ").png");

                pnn.imgs.Add(img);
            }
            for (int i = 1; i <= 8; i++)
            {
                Bitmap img = new Bitmap("Run (" + i + ").png");

                pnn.imgs.Add(img);
            }
            LHero.Add(pnn);
        }
        void DrawDubb(Graphics g)
        {
            Graphics g2 = Graphics.FromImage(off);
            DrawScene(g2);
            g.DrawImage(off, 0, 0);
        }
        void DrawScene(Graphics g2)
        {
            g2.Clear(Color.Black);

            // Draw the background images
            for (int i = 0; i < LBackGround.Count; i++)
            {
                g2.DrawImage(LBackGround[i].img, LBackGround[i].rcDst, LBackGround[i].rcSrc, GraphicsUnit.Pixel);
            }
            // Draw lines
            using (Pen thickPen = new Pen(Color.Red, 5))
            {
                for (int i = 0; i < lines.Count; i++)
                {
                    g2.DrawLine(thickPen, lines[i].X - LBackGround[0].rcSrc.X, lines[i].Y - LBackGround[0].rcSrc.Y, lines[i].Xe - LBackGround[0].rcSrc.X, lines[i].Ye - LBackGround[0].rcSrc.Y);
                }
            }
            // Draw circles
            for (int i = 0; i < circles.Count; i++)
            {
                using (Pen thickPen = new Pen(Color.Red, 10))
                {
                    g2.DrawLine(thickPen, circles[i].line.X - LBackGround[0].rcSrc.X, circles[i].line.Y - LBackGround[0].rcSrc.Y, circles[i].line.Xe - LBackGround[0].rcSrc.X, circles[i].line.Ye - LBackGround[0].rcSrc.Y);
                }
                circles[i].Drawcircle(g2, LBackGround[0].rcSrc.X, LBackGround[0].rcSrc.Y);
            }

            // Draw curves
            for (int i = 0; i < curves.Count; i++)
            {
                curves[i].DrawCurve(g2, LBackGround[0].rcSrc.X, LBackGround[0].rcSrc.Y);
            }

            // Draw heroes
            for (int i = 0; i < LHero.Count; i++)
            {
                int k = LHero[i].iframe;
                g2.DrawImage(LHero[i].imgs[k], LHero[i].x - LBackGround[0].rcSrc.X, LHero[i].y - LBackGround[0].rcSrc.Y);
            }

            // Draw a small black ellipse
            g2.FillEllipse(Brushes.Black, circle.x - LBackGround[0].rcSrc.X, circle.y - LBackGround[0].rcSrc.Y, 10, 10);

            // Load and prepare the image to be flipped
            Bitmap img = new Bitmap("beach-ball.png");
            Color clr = img.GetPixel(0, 0);
            img.MakeTransparent(clr);
            Bitmap bmp = new Bitmap(img.Width, img.Height);
            Graphics g3 = Graphics.FromImage(bmp);
            g3.TranslateTransform((float)bmp.Width / 2, (float)bmp.Height / 2);

            // Perform the flipping transformation
            if (z < o.Count)
            {
                if (o[z].type == "circle")
                {
                    float dx = points[z].X - circles[o[z].index].XC;
                    float dy = points[z].Y - circles[o[z].index].YC;
                    float thetaRadians = (float)Math.Atan2(dy, dx);
                    float thetaDegrees = thetaRadians * (180 / (float)Math.PI);

                    // Rotate the image by 180 degrees to flip it
                    g3.RotateTransform(180);
                }
            }
            // Draw the flipped image
            g3.DrawImage(img, -img.Width / 2, -img.Height / 2);

            // Translate back to the original coordinates
            g2.TranslateTransform(-(float)bmp.Width / 2, -(float)bmp.Height / 2);
            if(flagSpace==1)
            {
                g2.DrawImage(bmp, circle.x - LBackGround[0].rcSrc.X, circle.y - LBackGround[0].rcSrc.Y);

            }
        }
    }
}
