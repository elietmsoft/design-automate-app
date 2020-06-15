using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Forms;

namespace memory_automate.Utils
{
    public class Dessin
    {
        
        Graphics graphe { get; set; }
        public int step { get; set; }
        public Brush color_Bc { get; set; }
        public Brush color_line { get; set; }
        public Brush color_true { get; set; }
        public Brush color_false { get; set; }
        public int height { get; set; }
        public int ray { get; set; }
        public int length { get; set; }
        public Color color_circle { get; set; }
        public Dessin(Control pan)
        {
            color_circle = Color.Green;
            color_Bc = Brushes.Blue;
            color_line = Brushes.White;
            color_true = Brushes.Green;
            color_false = Brushes.Red;
            height = 200;
            ray = 30;
            length = 60;
            graphe = pan.CreateGraphics();
            graphe.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.High;
            graphe.CompositingQuality = System.Drawing.Drawing2D.CompositingQuality.HighQuality;
            graphe.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
            graphe.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAlias;
            pan.Refresh();
        }
        public void drawLine(Point pt, int count, int position, string valeur)
        {
            if (count == 0)
                return;
            if (count == 1)
                graphe.DrawLine(new Pen(Brushes.White, 2), pt, new Point(pt.X + 60, pt.Y));
            else
            {
                int haut = 200;
                int bas = 100;
                bool h = true;
                int M = count / 2;
                int pas = haut / 2;
                if (count % 2 == 0)
                {
                    //paire
                    //créer en haut
                    if (position >= M)
                        h = false;//la position est signalée en bas
                    int R = 0;
                    if (h)
                    {
                        //positionné en haut
                        R = pas * (M - position);
                    }
                    else
                    {
                        R = pas * (M - position - 1);
                    }
                    int y = pt.Y;
                    graphe.DrawLine(new Pen(color_line, 2), pt, new Point(pt.X + 60, y + R));
                    drawCircle(new Point(pt.X + length, y + R - (ray / 2)), valeur);
                    drawLine(new Point(pt.X + length + ray, y + R),1);
                }

                else
                {
                    if (position == M + 1)
                    {
                        //au milieu
                        graphe.DrawLine(new Pen(color_line, 2), pt, new Point(pt.X + length, pt.Y));
                    }
                    else
                    {
                        if (position >= M)
                            h = false;//la position est signalée en bas
                        int R = 0;
                        if (h)
                        {
                            //positionné en haut
                            R = pas * (M - position);
                        }
                        else
                        {
                            R = pas * (M - position - 1);
                        }
                        int y = pt.Y;
                        graphe.DrawLine(new Pen(color_line, 2), pt, new Point(pt.X + 60, y + R));
                    }
                }
            }
        }
        public void createLine(Point ptA, Point ptB)
        {
            graphe.DrawLine(new Pen(color_line, 2), ptA, ptB);
        }
        public void createTriangle(Point A, Point B, Point C)
        {
            graphe.DrawPolygon(new Pen(Brushes.Tan, 2), new Point[] {A,B,C });
        }
        public void drawLine(Point pt, int count, int position)
        {
            if (count == 0)
                return;
            if (count == 1)
                graphe.DrawLine(new Pen(color_line, 2), pt, new Point(pt.X + length, pt.Y));
            else
            {
                int haut = 200;
                int bas = 100;
                bool h = true;
                int M = count / 2;
                int pas = haut / 2;
                if (count % 2 == 0)
                {
                    //paire
                    //créer en haut
                    if (position >= M)
                        h = false;//la position est signalée en bas
                    int R = 0;
                    if (h)
                    {
                        //positionné en haut
                        R = pas * (M - position);
                    }
                    else
                    {
                        R = pas * (M - position - 1);
                    }
                    int y = pt.Y;
                    graphe.DrawLine(new Pen(color_line, 2), pt, new Point(pt.X + length, y + R));
                }
                else
                {
                    if (position == M + 1)
                    {
                        //au milieu
                        graphe.DrawLine(new Pen(color_line, 2), pt, new Point(pt.X + length, pt.Y));
                    }
                    else
                    {
                        if (position >= M)
                            h = false;//la position est signalée en bas
                        int R = 0;
                        if (h)
                        {
                            //positionné en haut
                            R = pas * (M - position);
                        }
                        else
                        {
                            R = pas * (M - position - 1);
                        }
                        int y = pt.Y;
                        graphe.DrawLine(new Pen(color_line, 2), pt, new Point(pt.X + length, y + R));
                    }
                }
            }
        }
        public Point drawLine(Point pt, int count, int position, int propertie, int haut)
        {
            Point point = new Point();
            if (count == 1)
            {
                graphe.DrawLine(new Pen(color_line, 2), pt, new Point(pt.X + length, pt.Y));
                point = new Point(pt.X + length, pt.Y);
            }
            else
            {
                bool h = true;
                int M = count / 2;
                int pas = haut / 2;
                if (count % 2 == 0)
                {
                    //paire
                    //créer en haut
                    if (position >= M)
                        h = false;//la position est signalée en bas
                    int R = 0;
                    if (h)
                    {
                        //positionné en haut
                        R = pas * (M - position);
                    }
                    else
                    {
                        R = pas * (M - position - 1);
                    }
                    int y = pt.Y;
                    graphe.DrawLine(new Pen(color_line, 2), pt, new Point(pt.X + length, y + R));
                    point = new Point(pt.X + length, y + R);
                }
                else
                {
                    if (position == M + 1)
                    {
                        //au milieu
                        graphe.DrawLine(new Pen(color_line, 2), pt, new Point(pt.X + length, pt.Y));
                        point = new Point(pt.X + length, pt.Y);
                    }
                    else
                    {
                        if (position >= M)
                            h = false;//la position est signalée en bas
                        int R = 0;
                        if (h)
                        {
                            //positionné en haut
                            R = pas * (M - position);
                        }
                        else
                        {
                            R = pas * (M - position - 1);
                        }
                        int y = pt.Y;
                        graphe.DrawLine(new Pen(color_line, 2), pt, new Point(pt.X + length, y + R));
                        point = new Point(pt.X + length, y + R);
                    }
                }
            }
            return point;
        }

        public void drawCircle(Point pt, string valeur)
        {
            graphe.DrawEllipse(new Pen(color_circle, 3), pt.X, pt.Y, ray,ray);
            graphe.DrawString(valeur, new Font("courrier", 12), Brushes.White, new Point(pt.X + (ray / 6), pt.Y + (ray / 6)));
        }
        public void writeText(Point pt, string valeur)
        {
            graphe.DrawString(valeur, new Font("courrier", 12), Brushes.White, new Point(pt.X, pt.Y ));
        }
        public Point drawCirclePt(Point pt, string valeur)
        {
            Point point = new Point();
            graphe.DrawEllipse(new Pen(color_circle, 3), pt.X, pt.Y - (ray / 2), ray, ray);
            graphe.DrawString(valeur, new Font("courrier", 12), color_Bc, new Point(pt.X + (ray / 6), pt.Y - 12));
            point = new Point(pt.X + ray, pt.Y);
            return point;
        }

        public void drawString(Point pt, string valeur)
        {
            graphe.DrawString(valeur, new Font("rockwell", 16), Brushes.DarkGoldenrod, pt);
        }
        public void drawString(Point pt, string valeur, Brush color)
        {
            graphe.DrawString(valeur, new Font("rockwell", 16), color, new Point(pt.X, pt.Y - (ray / 2)));
        }
        /// <summary>
        /// pt for set the point position
        /// nbre for set line's count
        /// </summary>
        /// <param name="pt"></param>
        /// <param name="nbre"></param>
        public void drawLine(Point pt, int nbre)
        {
            if (nbre == 0)
                return;
            if (nbre == 1)
                graphe.DrawLine(new Pen(Brushes.White, 2), pt, new Point(pt.X + step, pt.Y));
            else
            {
                if (nbre % 2 == 0)
                {
                    int count = nbre / 2;
                    int q = 20;
                    int pas = q;
                    int y = pt.Y;
                    for (int i = 1; i <= nbre / 2; i++)
                    {
                        int r = (pas * i);
                        graphe.DrawLine(new Pen(Brushes.White, 1), pt, new Point(pt.X + step, y + r));
                        graphe.DrawLine(new Pen(Brushes.White, 1), pt, new Point(pt.X + step, y - r));
                    }
                }
                else
                {
                    int count = nbre / 2;
                    int q = 20;
                    int pas = q;
                    int y = pt.Y;
                    for (int i = 1; i <= nbre / 2; i++)
                    {
                        int r = (pas * i);
                        graphe.DrawLine(new Pen(Brushes.White, 1), pt, new Point(pt.X + step, y + r));
                        graphe.DrawLine(new Pen(Brushes.White, 1), pt, new Point(pt.X + step, y - r));
                    }
                    graphe.DrawLine(new Pen(Brushes.White, 1), pt, new Point(pt.X + step, y));
                }
            }
        }
    }
    public class Position
    {
        public Point depart { get; set; }
        public Point arrive { get; set; }
        public string valeur { get; set; }
    }

    
}
