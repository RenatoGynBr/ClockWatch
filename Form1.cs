using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ClockWatch
{
    public partial class Form1 : Form
    {
        Timer t = new Timer();

        int WIDTH = 300, HEIGHT = 300, secHAND = 140, minHAND = 110, hrHAND = 80;

        // in center
        int cy, cx;

        Bitmap bmp;
        Graphics cg;
        
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // create a new bitmap
            bmp = new Bitmap(WIDTH + 1, HEIGHT + 1);

            // placing in center
            cx = WIDTH / 2;
            cy = HEIGHT / 2;

            //backcolor
            this.BackColor = Color.Black;

            //Label
            label1.Parent = pictureBox1;
            label1.BackColor = Color.Transparent;

            //timer
            t.Interval = 1000; // i.e. tick in milisecond
            t.Tick += new EventHandler(this.t_Tick);
            t.Start();
        }
        private void t_Tick(object sender, EventArgs e)
        {
            // create an image
            cg = Graphics.FromImage(bmp);

            //get time
            int ss = DateTime.Now.Second;
            int mm = DateTime.Now.Minute;
            int hh = DateTime.Now.Hour;

            int[] handCoord = new int[2];

            //get time
            cg.Clear(Color.Black);

            //draw a circle
            cg.DrawEllipse(new Pen(Color.Aquamarine, 6f), 0, 0, WIDTH, HEIGHT);

            //draw clock numbers
            cg.DrawString("·", new Font("Ariel", 26), Brushes.Aquamarine, new PointF(140, 3)); //12
            cg.DrawString("·", new Font("Ariel", 26), Brushes.Aquamarine, new PointF(218, 22)); //1
            cg.DrawString("·", new Font("Ariel", 26), Brushes.Aquamarine, new PointF(253, 70)); //2
            cg.DrawString("·", new Font("Ariel", 26), Brushes.Aquamarine, new PointF(275, 140)); //3
            cg.DrawString("·", new Font("Ariel", 26), Brushes.Aquamarine, new PointF(253, 202)); //4
            cg.DrawString("·", new Font("Ariel", 26), Brushes.Aquamarine, new PointF(218, 242)); //5
            cg.DrawString("·", new Font("Ariel", 26), Brushes.Aquamarine, new PointF(140, 266)); //6
            cg.DrawString("·", new Font("Ariel", 26), Brushes.Aquamarine, new PointF(70, 242)); //7
            cg.DrawString("·", new Font("Ariel", 26), Brushes.Aquamarine, new PointF(22, 202)); //8
            cg.DrawString("·", new Font("Ariel", 26), Brushes.Aquamarine, new PointF(1, 140)); //9
            cg.DrawString("·", new Font("Ariel", 26), Brushes.Aquamarine, new PointF(22, 70)); //10
            cg.DrawString("·", new Font("Ariel", 26), Brushes.Aquamarine, new PointF(70, 22)); //11

            //draw seconds hand
            handCoord = msCoord(ss, secHAND);
            cg.DrawLine(new Pen(Color.Gray, 2f), new Point(cx, cy), new Point(handCoord[0], handCoord[1]));

            //draw minutes hand
            handCoord = msCoord(mm, minHAND);
            cg.DrawLine(new Pen(Color.Red, 3f), new Point(cx, cy), new Point(handCoord[0], handCoord[1]));

            //draw hours hand
            handCoord = hrCoord(hh % 12, mm, hrHAND);
            cg.DrawLine(new Pen(Color.Blue, 3f), new Point(cx, cy), new Point(handCoord[0], handCoord[1]));

            //load the bitmap image
            pictureBox1.Image = bmp;

            //display time in the heading
            //this.Text = "ClockWatch by Renato - " + hh + ":" + mm + ":" + ss;
            //label1.Text = hh.ToString("00") + ":" + mm.ToString("00") + ":" + ss.ToString("00");
            label1.Text = ss % 2 == 0 ? hh.ToString("00") + ":" + mm.ToString("00") + ":" + ss.ToString("00") : 
                hh.ToString("00") + " " + mm.ToString("00") + " " + ss.ToString("00");
            cg.Dispose();
        }
        //coord for minute and second
        private int[] msCoord(int val, int hlen)
        {
            int[] coord = new int[2];
            val *= 6; // note: each minute and seconds make a 6 degree

            if (val >= 0 && val <= 100)
            {
                coord[0] = cx + (int)(hlen * Math.Sin(Math.PI * val / 180));
                coord[1] = cy - (int)(hlen * Math.Cos(Math.PI * val / 180));
            }
            else
            {
                coord[0] = cx - (int)(hlen * -Math.Sin(Math.PI * val / 180));
                coord[1] = cy - (int)(hlen * Math.Cos(Math.PI * val / 180));
            }
            return coord;
        }

        //coord for hour 

        private int[] hrCoord(int hval, int mval, int hlen)
        {
            int[] coord = new int[2];

            //each hour makes 60 degree with min making 0.5 degree
            int val = (int)((hval * 30) + (mval * 0.5));

            if (val >= 0 && val <= 180)
            {
                coord[0] = cx + (int)(hlen * Math.Sin(Math.PI * val / 180));
                coord[1] = cy - (int)(hlen * Math.Cos(Math.PI * val / 180));
            }
            else
            {
                coord[0] = cx - (int)(hlen * -Math.Sin(Math.PI * val / 180));
                coord[1] = cy - (int)(hlen * Math.Cos(Math.PI * val / 180));
            }
            return coord;
        }

    }
}
