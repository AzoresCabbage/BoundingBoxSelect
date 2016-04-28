using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace BoundingBoxSelect
{
    public partial class Form1 : Form
    {

        #region Fields

        private Cursor selectCursor_ = Cursors.Default;
        private Cursor drawCursor_ = Cursors.Cross;

        private Point mouseDownPoint_;
        private Point endPoint_;
        private bool mouseDown_;
        private Rectangle selectImageRect_;
        private Rectangle selectImageBounds_;
        private bool selectedImage_;
        private SizeGrip sizeGrip_;
        private Dictionary<SizeGrip, Rectangle> sizeGripRectList_;
        private OperateManager operateManager_;
        private Point topLeft, bottomRight;

        private double scale_x, scale_y;
        internal class ImageInfo
        {
            public string path;
            public List<Rectangle> box;
            public List<Rectangle> fakeBox;
            public ImageInfo(string _path)
            {
                path = _path;
                box = new List<Rectangle>();
                fakeBox = new List<Rectangle>();
            }
        }
        List<ImageInfo> img;
        int curPtr;

        #endregion

        #region Properties

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]


        public Cursor SelectCursor
        {
            get { return selectCursor_; }
            set { selectCursor_ = value; }
        }

        public Cursor DrawCursor
        {
            get { return drawCursor_; }
            set { drawCursor_ = value; }
        }

        internal bool SelectedImage
        {
            get { return selectedImage_; }
            set { selectedImage_ = value; }
        }

        internal Rectangle SelectImageRect
        {
            get { return selectImageRect_; }
            set
            {
                selectImageRect_ = value;
                if (!selectImageRect_.IsEmpty)
                {
                    CalCulateSizeGripRect();
                    pictureBox1.Invalidate();
                }
            }
        }

        internal SizeGrip SizeGrip
        {
            get { return sizeGrip_; }
            set { sizeGrip_ = value; }
        }

        internal Dictionary<SizeGrip, Rectangle> SizeGripRectList
        {
            get
            {
                if (sizeGripRectList_ == null)
                {
                    sizeGripRectList_ = new Dictionary<SizeGrip, Rectangle>();
                }
                return sizeGripRectList_;
            }
        }

        internal OperateManager OperateManager
        {
            get
            {
                if (operateManager_ == null)
                {
                    operateManager_ = new OperateManager();
                }
                return operateManager_;
            }
        }


        #endregion

        public Form1()
        {
            InitializeComponent();
        }

        private void btn_next_box_Click(object sender, EventArgs e)
        {

            scale_x = (double)(pictureBox1.Width) / pictureBox1.Image.Width;
            scale_y = (double)(pictureBox1.Height) / pictureBox1.Image.Height;
            int xmin = (int)Math.Floor((double)topLeft.X / scale_x);
            int xmax = (int)Math.Floor((double)bottomRight.X / scale_x);
            int ymin = (int)Math.Floor((double)topLeft.Y / scale_y);
            int ymax = (int)Math.Floor((double)bottomRight.Y / scale_y);
            img[curPtr].box.Add(new Rectangle(xmin, ymin, xmax - xmin, ymax - ymin));
            img[curPtr].fakeBox.Add(new Rectangle(topLeft.X, topLeft.Y, bottomRight.X - topLeft.X, bottomRight.Y - topLeft.Y));
            ResetBoxVar();
        }

        private void btn_next_picture_Click(object sender, EventArgs e)
        {
            if(curPtr >= img.Count - 1)
            {
                MessageBox.Show("已经到最后一张");
                return;
            }
            ++curPtr;
            SetPictureBoxPicture();
            ResetBoxVar();
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == Keys.Space)
            {
                btn_next_box.PerformClick();
                return true;
            }
            else if(keyData == Keys.N)
            {
                btn_next_picture.PerformClick();
                return true;
            }
            return base.ProcessCmdKey(ref msg, keyData);　// 其他键按默认处理　
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            //检测picture上的鼠标事件
            pictureBox1.MouseEnter += new EventHandler(pictureBox1_OnMouseEnter);
            pictureBox1.MouseDown += new MouseEventHandler(pictureBox1_OnMouseDown);
            pictureBox1.MouseMove += new MouseEventHandler(pictureBox1_OnMouseMove);
            pictureBox1.MouseUp += new MouseEventHandler(pictureBox1_OnMouseUp);
            pictureBox1.Paint += new PaintEventHandler(pictureBox1_OnPaint);

            img = new List<ImageInfo>();
            // 读取图片list
            try
            {
                FileStream fs = new FileStream("list.txt", FileMode.OpenOrCreate);
                StreamReader listReader = new StreamReader(fs);
                string line = "";
                while(line != null)
                {
                    line = listReader.ReadLine();
                    if (line != null && !line.Equals(""))
                        img.Add(new ImageInfo(line));
                }
                listReader.Close();
                fs.Close();
            }
            catch(IOException ex)
            {
                MessageBox.Show(ex.Message);
            }

           // img.Add(new ImageInfo("abcd"));

            // 读取之前所有的annotation
            try
            {
                FileStream fs = new FileStream("annotation.txt", FileMode.OpenOrCreate);
                StreamReader sr = new StreamReader(fs);
                string line = "";
                curPtr = 0;
                while(line != null)
                {
                    line = sr.ReadLine();
                    if (line == null || line.Equals(""))
                        continue;
                    int cnt = int.Parse(line);
                    for (int j = 0; j < cnt; ++j)
                    {
                        line = sr.ReadLine();
                        string[] num = line.Split(' ');
                        Rectangle anno = new Rectangle();
                        anno.X = int.Parse(num[0]);
                        anno.Y = int.Parse(num[1]);
                        anno.Width = int.Parse(num[2]);
                        anno.Height = int.Parse(num[3]);
                        img[curPtr].box.Add(anno);
                    }
                    ++curPtr;
                }

                sr.Close();
                fs.Close();
            }
            catch (IOException ex)
            {
                MessageBox.Show(ex.Message);
            }
            if (curPtr == img.Count)
                -- curPtr;
            ResetBoxVar();
            SetPictureBoxPicture();
        }

        protected override void OnClosing(CancelEventArgs e)
        {
            base.OnClosing(e);
            if (sizeGripRectList_ != null)
            {
                sizeGripRectList_.Clear();
                sizeGripRectList_ = null;
            }
            if (operateManager_ != null)
            {
                operateManager_.Dispose();
                operateManager_ = null;
            }

            selectCursor_ = null;
            drawCursor_ = null;

            //写入当前所有annotation
            try
            {
                FileStream fs = new FileStream("annotation.txt", FileMode.OpenOrCreate);
                StreamWriter sw = new StreamWriter(fs);
                //sw.Write(img.Count);
                
                for (int i = 0; i < curPtr;++i )
                {
                    sw.WriteLine(img[i].box.Count);
                    for (int j = 0; j < img[i].box.Count; ++j)
                    {
                        sw.WriteLine(img[i].box[j].X + " " + img[i].box[j].Y + " " + img[i].box[j].Width + " " + img[i].box[j].Height);
                    }
                }

                sw.Close();
                fs.Close();
            }
            catch (IOException ex)
            {
                MessageBox.Show(ex.Message);
            }
            base.OnClosing(e);
        }

        private void ResetBoxVar()
        {
            endPoint_ = Point.Empty;
            mouseDownPoint_ = Point.Empty;
            SelectedImage = false;
            mouseDown_ = false;
            selectImageRect_.Width = selectImageRect_.Height = 0;
            btn_next_box.Enabled = false;
        }

        private void SetPictureBoxPicture()
        {
            pictureBox1.ImageLocation = img[curPtr].path;
            picture_path.Text = img[curPtr].path;
        }

        private void btn_pre_picture_Click(object sender, EventArgs e)
        {
            if (curPtr == 0)
            {
                MessageBox.Show("已经是第一张");
                return;
            }
            --curPtr;
            ResetBoxVar();
            SetPictureBoxPicture();
        }
    }
}
