using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Collections;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;

namespace Paint_
{
    public partial class Form1 : Form
    {
        enum Paint_state { Brush, LIne, None, Rect, circle, FillRect, Fillellipes, eraser, Bezier, Beziers, FillPolygon, String, FillRectColor, Fill,Load , curves,closed };

        Paint_state paint_State = Paint_state.None;
        Color Color_brush;
        Color Color_eraser;
        int Size_brus;
        int Size_eraser;
        Graphics g;


        int? intx = null;
        int? inty = null;

        bool Start_brush = false;
        bool Start_eraser = false;


        bool Start_Line = false;
        Point LocationLine;

        bool StartRect = false;
        Point Start_Pos;

        bool StartFillRect = false;
        Point Start_FillPos;

        bool StartFillellipes = false;
        Point Start_FillEllipsPos;

        bool Startcircle = false;
        Point Start_circlePos;

        bool MoveForm = false;
        Point LocationStart;

        int StartBezier = 0;
        Point[] points_Bezier = new Point[3];

        int StartBezierS = 0;
        Point[] points_BezierS;

        int StartFillPolygon = 0;
        Point[] points_FillPolygon;

        bool StartFillRectColor = false;
        Point Start_FillColorPos;

        int StartCurveS = 0;
        Point[] points_CurveS;

        int StartClosedCurveS = 0;
        Point[] points_ClosedCurveS;

        public Form1()
        {
            InitializeComponent();

            Color_eraser = Color.White;
            Color_brush = Color.Black;
            Size_brus = (int)numericUpDown4.Value;
            Size_eraser = 3;
            g = panel1.CreateGraphics();
        }

        public void Disable_start()
        {
            Start_brush = false;
            Start_Line = false;
            StartRect = false;
            StartFillRect = false;
            StartFillellipes = false;
            Start_eraser = false;
            StartBezier = 0;
            StartBezierS = 0;
            StartFillPolygon = 0;
            textBox1.Visible = false;
            numericUpDown3.Visible = false;
            numericUpDown1.Visible = false;
            numericUpDown2.Visible = false;
        }

        private void panel2_Click(object sender, EventArgs e)
        {
            paint_State = Paint_state.Brush;
            Disable_start();
        }


        private void panel6_Click(object sender, EventArgs e)
        {
            paint_State = Paint_state.Rect;
            Disable_start();
        }

        private void panel1_MouseDown(object sender, MouseEventArgs e)
        {
            if (paint_State == Paint_state.Brush)
            {
                Start_brush = true;
            }
            else
                if (paint_State == Paint_state.LIne)
            {
                if (!Start_Line)
                {
                    LocationLine = new Point(e.X, e.Y);
                    Start_Line = true;
                }
                else
                {
                    Pen p;
                    p = new Pen(Color_brush, Size_brus);
                    g.DrawLine(p, new Point(LocationLine.X, LocationLine.Y), new Point(e.X, e.Y));
                    LocationLine = new Point(e.X, e.Y);
                }
            }
            else
                if (paint_State == Paint_state.Rect)
            {
                if (!StartRect)
                {
                    Start_Pos = new Point(e.X, e.Y);
                    StartRect = true;
                }
                else
                {
                    Pen p;
                    p = new Pen(Color_brush, Size_brus);
                    if (Start_Pos.X < e.X && Start_Pos.Y < e.Y) { g.DrawRectangle(p, new Rectangle(Start_Pos.X, Start_Pos.Y, e.X - Start_Pos.X, e.Y - Start_Pos.Y)); }
                    else
                    if (Start_Pos.X > e.X && Start_Pos.Y > e.Y) { g.DrawRectangle(p, new Rectangle(e.X, e.Y, Start_Pos.X - e.X, Start_Pos.Y - e.Y)); }
                    else
                    if (Start_Pos.X > e.X && Start_Pos.Y < e.Y) { g.DrawRectangle(p, new Rectangle(e.X, Start_Pos.Y, Start_Pos.X - e.X, e.Y - Start_Pos.Y)); }
                    else
                    if (Start_Pos.X < e.X && Start_Pos.Y > e.Y) { g.DrawRectangle(p, new Rectangle(Start_Pos.X, e.Y, e.X - Start_Pos.X, Start_Pos.Y - e.Y)); }
                    StartRect = false;
                }

            }
            else
                if (paint_State == Paint_state.FillRect)
            {
                if (!StartFillRect)
                {
                    Start_FillPos = new Point(e.X, e.Y);
                    StartFillRect = true;
                }
                else
                {
                    Pen p;
                    p = new Pen(Color_brush, Size_brus);
                    if (Start_FillPos.X < e.X && Start_FillPos.Y < e.Y) { g.FillRectangle(p.Brush, new Rectangle(Start_FillPos.X, Start_FillPos.Y, e.X - Start_FillPos.X, e.Y - Start_FillPos.Y)); }
                    else
                    if (Start_FillPos.X > e.X && Start_FillPos.Y > e.Y) { g.FillRectangle(p.Brush, new Rectangle(e.X, e.Y, Start_FillPos.X - e.X, Start_FillPos.Y - e.Y)); }
                    else
                    if (Start_FillPos.X > e.X && Start_FillPos.Y < e.Y) { g.FillRectangle(p.Brush, new Rectangle(e.X, Start_FillPos.Y, Start_FillPos.X - e.X, e.Y - Start_FillPos.Y)); }
                    else
                    if (Start_FillPos.X < e.X && Start_FillPos.Y > e.Y) { g.FillRectangle(p.Brush, new Rectangle(Start_FillPos.X, e.Y, e.X - Start_FillPos.X, Start_FillPos.Y - e.Y)); }
                    StartFillRect = false;
                }
            }
            else
                 if (paint_State == Paint_state.Fillellipes)
            {
                if (!StartFillellipes)
                {
                    Start_FillEllipsPos = new Point(e.X, e.Y);
                    StartFillellipes = true;
                }
                else
                {
                    Pen p;
                    p = new Pen(Color_brush, Size_brus);
                    if (Start_FillEllipsPos.X < e.X && Start_FillEllipsPos.Y < e.Y) { g.FillEllipse(p.Brush, new Rectangle(Start_FillEllipsPos.X, Start_FillEllipsPos.Y, e.X - Start_FillEllipsPos.X, e.Y - Start_FillEllipsPos.Y)); }
                    else
                    if (Start_FillEllipsPos.X > e.X && Start_FillEllipsPos.Y > e.Y) { g.FillEllipse(p.Brush, new Rectangle(e.X, e.Y, Start_FillEllipsPos.X - e.X, Start_FillEllipsPos.Y - e.Y)); }
                    else
                    if (Start_FillEllipsPos.X > e.X && Start_FillEllipsPos.Y < e.Y) { g.FillEllipse(p.Brush, new Rectangle(e.X, Start_FillEllipsPos.Y, Start_FillEllipsPos.X - e.X, e.Y - Start_FillEllipsPos.Y)); }
                    else
                    if (Start_FillEllipsPos.X < e.X && Start_FillEllipsPos.Y > e.Y) { g.FillEllipse(p.Brush, new Rectangle(Start_FillEllipsPos.X, e.Y, e.X - Start_FillEllipsPos.X, Start_FillEllipsPos.Y - e.Y)); }
                    StartFillellipes = false;
                }
            }
            else
                 if (paint_State == Paint_state.circle)
            {
                if (!Startcircle)
                {
                    Start_circlePos = new Point(e.X, e.Y);
                    Startcircle = true;
                }
                else
                {
                    Pen p;
                    p = new Pen(Color_brush, Size_brus);
                    if (Start_circlePos.X < e.X && Start_circlePos.Y < e.Y) { g.DrawEllipse(p, new Rectangle(Start_circlePos.X, Start_circlePos.Y, e.X - Start_circlePos.X, e.Y - Start_circlePos.Y)); }
                    else
                    if (Start_circlePos.X > e.X && Start_circlePos.Y > e.Y) { g.DrawEllipse(p, new Rectangle(e.X, e.Y, Start_circlePos.X - e.X, Start_circlePos.Y - e.Y)); }
                    else
                    if (Start_circlePos.X > e.X && Start_circlePos.Y < e.Y) { g.DrawEllipse(p, new Rectangle(e.X, Start_circlePos.Y, Start_circlePos.X - e.X, e.Y - Start_circlePos.Y)); }
                    else
                    if (Start_circlePos.X < e.X && Start_circlePos.Y > e.Y) { g.DrawEllipse(p, new Rectangle(Start_circlePos.X, e.Y, e.X - Start_circlePos.X, Start_circlePos.Y - e.Y)); }
                    Startcircle = false;
                }
            }
            else if (paint_State == Paint_state.eraser)
            {
                Start_eraser = true;
            }

            else if (paint_State == Paint_state.Bezier)
            {
                if (StartBezier < 3)
                {
                    points_Bezier[StartBezier] = new Point(e.X, e.Y);
                    StartBezier++;
                }
                else
                {
                    Pen p;
                    p = new Pen(Color_brush, Size_brus);

                    g.DrawBezier(p, points_Bezier[0], points_Bezier[1], points_Bezier[2], new Point(e.X, e.Y));
                    StartBezier = 0;
                }
            } else if (paint_State == Paint_state.Beziers)
            {
                if (StartBezierS < numericUpDown1.Value - 1)
                {
                    if (StartBezierS == 0) points_BezierS = new Point[(int)numericUpDown1.Value];

                    points_BezierS[StartBezierS] = new Point(e.X, e.Y);
                    StartBezierS++;

                }
                else
                {
                    Pen p;
                    p = new Pen(Color_brush, Size_brus);
                    points_BezierS[StartBezierS] = new Point(e.X, e.Y);
                    g.DrawBeziers(p, points_BezierS);
                    StartBezierS = 0;
                }
            } else
                 if (paint_State == Paint_state.FillPolygon)
            {
                if (StartFillPolygon < numericUpDown2.Value - 1)
                {
                    if (StartFillPolygon == 0) points_FillPolygon = new Point[(int)numericUpDown2.Value];

                    points_FillPolygon[StartFillPolygon] = new Point(e.X, e.Y);
                    StartFillPolygon++;

                }
                else
                {
                    Pen p;
                    p = new Pen(Color_brush, Size_brus);
                    points_FillPolygon[StartFillPolygon] = new Point(e.X, e.Y);
                    g.FillPolygon(p.Brush, points_FillPolygon);
                    StartFillPolygon = 0;
                }
            } else if (paint_State == Paint_state.String)
            {
                string drawString = textBox1.Text;
                Font drawFont = new Font("Arial", (int)numericUpDown3.Value);
                SolidBrush drawBrush = new SolidBrush(Color_brush);

                float x = e.X - 100;
                float y = e.Y - 25;
                float width = 200.0F;
                float height = 50.0F;
                RectangleF drawRect = new RectangleF(x, y, width, height);

                StringFormat drawFormat = new StringFormat();
                drawFormat.Alignment = StringAlignment.Center;

                g.DrawString(drawString, drawFont, drawBrush, drawRect, drawFormat);
            } else if (paint_State == Paint_state.FillRectColor)
            {
                if (!StartFillRectColor)
                {
                    Start_FillColorPos = new Point(e.X, e.Y);
                    StartFillRectColor = true;
                }
                else 
                {
                    Rectangle rectangle_ = new Rectangle();
                    if (Start_FillColorPos.X < e.X && Start_FillColorPos.Y < e.Y)
                    {
                        rectangle_ = new Rectangle(Start_FillColorPos.X, Start_FillColorPos.Y, e.X - Start_FillColorPos.X, e.Y - Start_FillColorPos.Y);
                    }
                    else
                        if (Start_FillColorPos.X > e.X && Start_FillColorPos.Y > e.Y)
                    {
                        rectangle_ = new Rectangle(e.X, e.Y, Start_FillColorPos.X - e.X, Start_FillColorPos.Y - e.Y);
                    }
                    else
                    if (Start_FillColorPos.X > e.X && Start_FillColorPos.Y < e.Y)
                    {
                        rectangle_ = new Rectangle(e.X, Start_FillColorPos.Y, Start_FillColorPos.X - e.X, e.Y - Start_FillColorPos.Y);
                    }
                    else
                    if (Start_FillColorPos.X < e.X && Start_FillColorPos.Y > e.Y)
                    {
                        rectangle_ = new Rectangle(Start_FillColorPos.X, e.Y, e.X - Start_FillColorPos.X, Start_FillColorPos.Y - e.Y);
                    }

                    LinearGradientBrush linearGradientBrush = new LinearGradientBrush(rectangle_, Color.Red, Color.Yellow, (int)numericUpDown5.Value);
                    ColorBlend cblend = new ColorBlend(3);
                    cblend.Colors = new Color[3] { p1.BackColor, p2.BackColor, p3.BackColor };
                    cblend.Positions = new float[3] { 0f, 0.5f, 1f };

                    linearGradientBrush.InterpolationColors = cblend;

                    g.FillRectangle(linearGradientBrush, rectangle_);
                    StartFillRectColor = false;
                    
                }
            }else
                if (paint_State == Paint_state.Fill)
            {
                Bitmap myBitmap = new Bitmap(panel1.Width, panel1.Height);
                Graphics g = Graphics.FromImage(myBitmap);
                Point Location_ = new Point((panel1.Location.X), (panel1.Location.Y));
                g.CopyFromScreen(PointToScreen(Location_), new Point(0, 0), panel1.Size);

                Color temp = myBitmap.GetPixel(e.X, e.Y);
                Pread.BackColor = temp;

                for(int Xcount=0; Xcount < myBitmap.Width; Xcount++)
                {
                    for(int Ycount = 0; Ycount < myBitmap.Height; Ycount++)
                    {
                        if (myBitmap.GetPixel(Xcount, Ycount) == temp)
                            myBitmap.SetPixel(Xcount, Ycount, pFill.BackColor);
                    }
                }
                this.g.DrawImage(myBitmap, 0, 0, myBitmap.Width, myBitmap.Height);
            }
            else
                if (paint_State == Paint_state.Load)
            {
                Point p = new Point(e.X, e.Y);
                Image_Load(p);
            }
            else
                if (paint_State == Paint_state.curves)
            {
                if (StartCurveS < numericUpDown6.Value - 1)
                {
                    if (StartCurveS == 0) points_CurveS = new Point[(int)numericUpDown6.Value];

                    points_CurveS[StartCurveS] = new Point(e.X, e.Y);
                    StartCurveS++;
                }
                else
                {
                    Pen p;
                    p = new Pen(Color_brush, Size_brus);
                    points_CurveS[StartCurveS] = new Point(e.X, e.Y);
                    g.DrawCurve(p, points_CurveS);
                    StartCurveS = 0;
                }
            }else
                if (paint_State == Paint_state.closed)
            {
                if (StartClosedCurveS < numericUpDown7.Value - 1)
                {
                    if (StartClosedCurveS == 0) points_ClosedCurveS = new Point[(int)numericUpDown7.Value];

                    points_ClosedCurveS[StartClosedCurveS] = new Point(e.X, e.Y);
                    StartClosedCurveS++;
                }
                else
                {
                    Pen p;
                    p = new Pen(Color_brush, Size_brus);
                    points_ClosedCurveS[StartClosedCurveS] = new Point(e.X, e.Y);
                    g.DrawClosedCurve(p, points_ClosedCurveS);
                    StartClosedCurveS = 0;
                }
            }
        }
 
        private void panel1_MouseMove(object sender, MouseEventArgs e)
        {
            if (Start_brush)
            {
                if (paint_State == Paint_state.Brush)
                {
                    Pen p;
                    p = new Pen(Color_brush,(int)numericUpDown4.Value);
                    g.DrawLine(p, new Point(intx ?? e.X, inty ?? e.Y), new Point(e.X, e.Y));
                    intx = e.X;
                    inty = e.Y;
                }
            }else
                if (Start_eraser)
            {
                if (paint_State == Paint_state.eraser)
                {
                    Pen p;
                    p = new Pen(Color_eraser, Size_eraser);
                    g.DrawLine(p, new Point(intx ?? e.X, inty ?? e.Y), new Point(e.X, e.Y));
                    intx = e.X;
                    inty = e.Y;
                }
            }

        }

        private void panel1_MouseUp(object sender, MouseEventArgs e)
        {
            if (paint_State == Paint_state.Brush)
            {
                Start_brush = false;
                intx = null;
                inty = null;
            }else
                if(paint_State == Paint_state.eraser)
            {
                Start_eraser = false;
                intx = null;
                inty = null;
            }
          
        }

        private void panel7_Click(object sender, EventArgs e)
        {
            paint_State = Paint_state.FillRect;
            Disable_start();
        }

        private void panel8_Click(object sender, EventArgs e)
        {
            paint_State = Paint_state.Fillellipes;
            Disable_start();
        }

        private void panel3_Click(object sender, EventArgs e)
        {
            if (colorDialog1.ShowDialog() == DialogResult.OK)
            {
                Color_brush = colorDialog1.Color;
            }
        }

        private void Form1_MouseDown(object sender, MouseEventArgs e)
        {
            MoveForm = true;
            LocationStart = e.Location;
            
        }

        private void Form1_MouseMove(object sender, MouseEventArgs e)
        {
            if (MoveForm)
            {
                Location = new Point(Location.X + (e.Location.X - LocationStart.X),
                                   Location.Y + (e.Location.Y - LocationStart.Y));
            }
        }

        private void Form1_MouseUp(object sender, MouseEventArgs e)
        {
            MoveForm = false;
        }

        

        private void panel4_Click(object sender, EventArgs e)
        {
            paint_State = Paint_state.LIne;
            Disable_start();
        }

        private void panel5_Click(object sender, EventArgs e)
        {
            paint_State = Paint_state.circle;
            Disable_start();
        }

        private void panel9_Click(object sender, EventArgs e)
        {
            paint_State = Paint_state.eraser;
            Disable_start();
        }

       

        private void panel10_Click(object sender, EventArgs e)
        {
            paint_State = Paint_state.Bezier;
            Disable_start();
        }

        private void panel11_Click(object sender, EventArgs e)
        {
            paint_State = Paint_state.Beziers;
            Disable_start();
            numericUpDown1.Visible = true;
        }

        private void panel12_Click(object sender, EventArgs e)
        {
            paint_State = Paint_state.FillPolygon;
            Disable_start();
            numericUpDown2.Visible = true;
        }

        private void panel13_Click(object sender, EventArgs e)
        {
            paint_State = Paint_state.String;
            Disable_start();

            textBox1.Visible = true;
            numericUpDown3.Visible = true;

        }

        private void panel15_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void panel14_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void panel16_Click(object sender, EventArgs e)
        {
            paint_State = Paint_state.FillRectColor;
            Disable_start();
        }

        private void p1_Click(object sender, EventArgs e)
        {
            if (colorDialog1.ShowDialog() == DialogResult.OK)
            {
               p1.BackColor = colorDialog1.Color;
            }
        }

        private void p2_Click(object sender, EventArgs e)
        {
            if (colorDialog1.ShowDialog() == DialogResult.OK)
            {
                p2.BackColor = colorDialog1.Color;
            }
        }

        private void p3_Click(object sender, EventArgs e)
        {
            if (colorDialog1.ShowDialog() == DialogResult.OK)
            {
                p3.BackColor = colorDialog1.Color;
            }
        }

        private void panel17_Click(object sender, EventArgs e)
        {
            paint_State = Paint_state.Fill;
            Disable_start();
        }

        private void pFill_Click(object sender, EventArgs e)
        {
            if (colorDialog1.ShowDialog() == DialogResult.OK)
            {
                pFill.BackColor = colorDialog1.Color;
            }
        }

        private void panel18_Click(object sender, EventArgs e)
        {
            SaveImage(panel1);
        }

        private void SaveImage(Control theControl)
        {
            Bitmap controlBitMap = new Bitmap(theControl.Width, theControl.Height);
            Graphics g = Graphics.FromImage(controlBitMap);
            Point Location_ = new Point((panel1.Location.X), (panel1.Location.Y));
            g.CopyFromScreen(PointToScreen(Location_), new Point(0, 0), panel1.Size);

            ImageFormat Filter_name = ImageFormat.Png;
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "JPeg Image|*.jpg|Bitmap Image|*.bmb|PNG Image|*.png";
            saveFileDialog.Title = "Save an Image File";

            saveFileDialog.ShowDialog();
            if(saveFileDialog.FileName != null)
            {
                if(saveFileDialog.FileName != "")
                {
                    switch (saveFileDialog.FilterIndex)
                    {
                        case 1:
                            Filter_name = ImageFormat.Jpeg;
                            break;
                        case 2:
                            Filter_name = ImageFormat.Bmp;
                            break;
                        case 3:
                            Filter_name = ImageFormat.Png;
                            break;
                    }
                    controlBitMap.Save(saveFileDialog.FileName, Filter_name);
                }
            }
        }

        Image groupPanelImage;
        private void Image_Load(Point Loc)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();

            openFileDialog.InitialDirectory = "c:\\";
            openFileDialog.Filter = "Jpeg Image|*.jpg|Bitmap Image|*.bmp|PNG Image|*.png|All files(*.*)|*.*";
            openFileDialog.FilterIndex = 2;
            openFileDialog.RestoreDirectory = true;

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                groupPanelImage = Image.FromFile(openFileDialog.FileName);
            }

            g.DrawImage(groupPanelImage, Loc);
        }

        private void panel19_Click(object sender, EventArgs e)
        {
            paint_State = Paint_state.Load;
            Disable_start();
        }

        private void panel20_Click(object sender, EventArgs e)
        {
            paint_State = Paint_state.curves;
            Disable_start();
        }

        private void panel21_Click(object sender, EventArgs e)
        {
           // paint_State == Paint_state.closed;
           // Disable_start();
        }
    }
}
