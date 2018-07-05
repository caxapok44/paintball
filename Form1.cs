using System;
using System.Drawing;
using System.Windows.Forms;

namespace MiniPaint
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            
            snapShot = new Bitmap(pnl_Draw.ClientRectangle.Width, pnl_Draw.ClientRectangle.Height);
            tempDraw = (Bitmap)snapShot.Clone();

        }

        #region init_staff
        bool startPaint = true;
        Graphics g;
        bool down = false;  
        bool drawSquare = false;
        bool drawRectangle = false;
        bool drawCircle = false;
        public int downX, downY,UpX,UpY;
        Pen pen;
        SolidBrush sb;
        Bitmap snapShot, tempDraw;
        PaintEventArgs hell;
        #endregion
    
        #region _MainGraph_
        private void pnl_Draw_MouseDown_1(object sender, MouseEventArgs e)
        {         
            down = true;
            downX = e.X;
            downY = e.Y;
            tempDraw = (Bitmap)snapShot.Clone();
           
        }

        private void pnl_Draw_MouseMove_1(object sender, MouseEventArgs e)
        {
            if (down)
            {
                UpX = e.X;
                UpY = e.Y;
                pnl_Draw.Invalidate();
                pnl_Draw.Update();
            }
        }

        private void pnl_Draw_MouseUp_1(object sender, MouseEventArgs e)
        {
            down = false;

            snapShot = (Bitmap)tempDraw.Clone();
        }
        private void pnl_Draw_Paint(object sender, PaintEventArgs e)
          {
            
            if (tempDraw!=null)
            {
                if (drawSquare)
                {
                    tempDraw = (Bitmap)snapShot.Clone();
                    g = Graphics.FromImage(tempDraw);
                    pen = new Pen(btn_PenColor.BackColor, int.Parse(txt_ShapeSize.Text));
                    g.DrawLine(pen, downX, downY, UpX, UpY);
                    e.Graphics.DrawImageUnscaled(tempDraw, 0, 0);
                    g.Dispose();


                }
                if (drawRectangle)
                {
                    tempDraw = (Bitmap)snapShot.Clone();
                    g = Graphics.FromImage(tempDraw);
                    pen = new Pen(btn_PenColor.BackColor, int.Parse(txt_ShapeSize.Text));
                    sb = new SolidBrush(button1.BackColor);
                    pen.Alignment = System.Drawing.Drawing2D.PenAlignment.Inset;
                    g.FillRectangle(sb, downX, downY, UpX - downX, UpY - downY);
                    g.DrawRectangle(pen, downX, downY, UpX - downX, UpY - downY);

                    e.Graphics.DrawImageUnscaled(tempDraw, 0, 0);
                    g.Dispose();
                }
                if (drawCircle)
                {
                    tempDraw = (Bitmap)snapShot.Clone();
                    g = Graphics.FromImage(tempDraw);
                    pen = new Pen(btn_PenColor.BackColor, int.Parse(txt_ShapeSize.Text));
                    sb = new SolidBrush(button1.BackColor);
                    pen.Alignment = System.Drawing.Drawing2D.PenAlignment.Inset;
                    
                    g.FillEllipse(sb, downX, downY, UpX - downX, UpY - downY);
                    g.DrawEllipse(pen, downX, downY, UpX - downX, UpY - downY);

                    e.Graphics.DrawImageUnscaled(tempDraw, 0, 0);
                    g.Dispose();
                }
                if (startPaint)
                {
                    g = Graphics.FromImage(tempDraw);
                    pen = new Pen(btn_PenColor.BackColor, int.Parse(txt_ShapeSize.Text));
                    g.DrawLine(pen, downX, downY, UpX, UpY);
                    e.Graphics.DrawImageUnscaled(tempDraw, 0, 0);
                    g.Dispose();
                    downX = UpX;
                    downY = UpY;

                }

                hell = e;

            }
          }
        private void trackBar1_Scroll(object sender, EventArgs e)
        {
            txt_ShapeSize.Text = Convert.ToString(trackBar1.Value);
        }
        #endregion

        #region __Buttons_init__
        private void btn_Square_Click(object sender, EventArgs e)
        {
            drawSquare = true;
            drawCircle = false;
            drawRectangle = false;
            startPaint = false;
        }

        private void btn_Rectangle_Click(object sender, EventArgs e)
        {
            drawSquare = false;
            drawCircle = false;
            drawRectangle = true;
            startPaint = false;
        }

        private void btn_Circle_Click(object sender, EventArgs e)
        {
            drawSquare = false;
            drawCircle = true;
            drawRectangle = false;
            startPaint = false;
        }

       

        private void button2_Click(object sender, EventArgs e)
        {
            drawSquare = false;
            drawCircle = false;
            drawRectangle = false;
            startPaint = true;
        }
        #endregion

        #region __StripMenu__
        //Exit under File Menu 
        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if(MessageBox.Show("Do you want to Exit?","Exit",MessageBoxButtons.YesNo,MessageBoxIcon.Information)==DialogResult.Yes)
            {
                Application.Exit();
            }
        }

        private void newToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Graphics g = pnl_Draw.CreateGraphics();
            g.Clear(Color.White);
            pnl_Draw.Image = null;
            snapShot = new Bitmap(pnl_Draw.ClientRectangle.Width, pnl_Draw.ClientRectangle.Height);
            tempDraw = (Bitmap)snapShot.Clone();
            g.Dispose();
        }
        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                pnl_Draw.Image = Image.FromFile(openFileDialog1.FileName);
                snapShot = (Bitmap)pnl_Draw.Image;
                tempDraw = (Bitmap)snapShot.Clone();
                this.Text = openFileDialog1.FileName;
            }
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                string fileName = saveFileDialog1.FileName;
                string strFilExtn = fileName.Remove(0, fileName.Length - 3);
                this.Text = saveFileDialog1.FileName;

                switch (strFilExtn)
                {
                    case "bmp": snapShot.Save(fileName, System.Drawing.Imaging.ImageFormat.Bmp);  break;
                    case "jpg": snapShot.Save(fileName, System.Drawing.Imaging.ImageFormat.Jpeg); break;
                    case "png": snapShot.Save(fileName, System.Drawing.Imaging.ImageFormat.Png);  break;
                    case "tif": snapShot.Save(fileName, System.Drawing.Imaging.ImageFormat.Tiff); break;
                    case "gof": snapShot.Save(fileName, System.Drawing.Imaging.ImageFormat.Gif);  break;
                    default: break;
                        break;
                }
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }



        //About under Help Menu
        private void aboutMiniPaintToolStripMenuItem_Click(object sender, EventArgs e)
        {
            About a = new About();
            a.ShowDialog();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //Open Color Dialog and Set BackColor of btn_PenColor if user click on OK
            ColorDialog c = new ColorDialog();
            if(c.ShowDialog()==DialogResult.OK)
            {
                btn_PenColor.BackColor = c.Color;
            }
        }
         private void btn_CanvasColor_Click_1(object sender, EventArgs e)
                {
                    ColorDialog c = new ColorDialog();
                    if(c.ShowDialog()==DialogResult.OK)
                    {
                        pnl_Draw.BackColor = c.Color;
                        btn_CanvasColor.BackColor = c.Color;
                    }
                }

        private void button1_Click_1(object sender, EventArgs e)
        {
            //Open Color Dialog and Set BackColor of btn_PenColor if user click on OK
            ColorDialog cf = new ColorDialog();
            if (cf.ShowDialog() == DialogResult.OK)
            {
                button1.BackColor = cf.Color;
            }
        }

      
        #endregion

    }
}
