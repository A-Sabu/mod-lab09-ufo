using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Lab9
{
    public partial class Form1: Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        double sin(double x, int n)
        {
            double sin = 0;
            for (int i = 1; i <= n; i++)
            {
                sin += Math.Pow(-1, i - 1) * Math.Pow(x, 2 * i - 1) / fact(2 * i - 1);
            }
            return sin;
        }

        double cos(double x, int n)
        {
            double cos = 0;
            for (int i = 1; i <= n; i++)
            {
                cos += Math.Pow(-1, i - 1) * Math.Pow(x, 2 * i - 2) / fact(2 * i - 2);
            }

            return cos;
        }

        double arctg(double x, int n)
        {
            double arctg = 0;
            if (Math.Abs(x) <= 1)
            {
                for (int i = 1; i <= n; i++)
                {
                    arctg += Math.Pow(-1, i - 1) * Math.Pow(x, 2 * i - 1) / (2 * i - 1);
                }
            }
            else
            {
                arctg = (x >= 1) ? Math.PI / 2 : -Math.PI / 2;
                for (int i = 0; i < n; i++)
                {
                    arctg -= Math.Pow(-1, i) / ((2 * i + 1) * Math.Pow(x, 2 * i + 1));
                }
            }
            return arctg;
        }

        int fact(int x) => (x <= 0) ? 1 : x * fact(x - 1);

        double DrawLineWithSumError(Point startPoint, Point finishPoint, double step, int n, int r, PaintEventArgs e)
        {
            if (e != null)
            {
                e.Graphics.ScaleTransform(0.5f, 0.5f);
                e.Graphics.DrawEllipse(new Pen(Color.Black, 10), startPoint.X, startPoint.Y, r, r);
                e.Graphics.DrawEllipse(new Pen(Color.Black, 10), finishPoint.X, finishPoint.Y, r, r);
            }

            double x1 = startPoint.X;
            double y1 = startPoint.Y;
            double x2 = finishPoint.X;
            double y2 = finishPoint.Y;
            double distance = Math.Sqrt(Math.Pow(Math.Abs(x1 - x2), 2) + Math.Pow(Math.Abs(y1 - y2), 2));
            double error = Math.Abs(x1 - x2) + Math.Abs(y1 - y2);
            double angle = arctg((y2 - y1) / (x1 - x2), n);

            while (distance <= error)
            {
                x1 = x1 + step * cos(angle, n);
                y1 = y1 - step * sin(angle, n);
                distance = Math.Sqrt(Math.Pow(Math.Abs(x1 - x2), 2) + Math.Pow(Math.Abs(y1 - y2), 2));
                error = (distance < error) ? distance : error;

                if (e != null) e.Graphics.DrawEllipse(new Pen(Color.DarkGreen, 5), (int)x1, (int)y1, 1, 1);
            }
            return (error-r > 0) ? error - r : 0;
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            Graphics graphics = e.Graphics;

            Point startPoint = new Point(100, 100);
            Point finishPoint = new Point(500, 600);
            double error = DrawLineWithSumError(startPoint, finishPoint, 1, 4, 4, e);

            MessageBox.Show($"Погрешность: {error}");
            
            List<int> nList = new List<int>();
            List<int> rList = new List<int>();
            int n;
            for (int r = 1; r <= 20; r++)
            {
                n = 16;
                do
                {
                    n--;
                    error = DrawLineWithSumError(startPoint, finishPoint, 1, n, r, null);
                } while (error == 0);
                nList.Add(n);
                rList.Add(r);
            }

            for (int i=0; i<nList.Count; i++)
            {
                Console.WriteLine($"R: {rList[i]} N: {nList[i]}");
            }
            Graphic g = new Graphic(rList, nList);
            g.Show();
            
        }
    }
}
