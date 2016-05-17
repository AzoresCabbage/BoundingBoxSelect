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
        private string basepath = "";
        private int image_width_;
        private int image_height_;
        private int image_x_;
        private int image_y_;

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

        private double scale_x, scale_y;
        internal class ImageInfo
        {
            public string path;
            public Rectangle box;
            public Rectangle GT_Head_box;
            public ImageInfo(string _path)
            {
                path = _path;
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

        private void btn_next_picture_Click(object sender, EventArgs e)
        {
            if (curPtr >= img.Count - 1)
            {
                if (selectedImage_)
                {
                    int xmin = (int)Math.Floor((double)(selectImageRect_.X - image_x_) * scale_x);
                    int width = (int)Math.Floor((double)selectImageRect_.Width * scale_x);
                    int ymin = (int)Math.Floor((double)(selectImageRect_.Y - image_y_) * scale_y);
                    int height = (int)Math.Floor((double)selectImageRect_.Height * scale_y);

                    img[curPtr].box = new Rectangle(xmin, ymin, width, height);
                }
                MessageBox.Show("已经到最后一张");
                return;
            }
            if (!selectedImage_ && (img[curPtr].box.Width == 0 || img[curPtr].box.Height == 0))
            {
                MessageBox.Show("当前图片还未标注");
                return;
            }
            if (selectedImage_)
            {
                int xmin = (int)Math.Floor((double)(selectImageRect_.X - image_x_) * scale_x);
                int width = (int)Math.Floor((double)selectImageRect_.Width * scale_x);
                int ymin = (int)Math.Floor((double)(selectImageRect_.Y - image_y_) * scale_y);
                int height = (int)Math.Floor((double)selectImageRect_.Height * scale_y);

                img[curPtr].box = new Rectangle(xmin, ymin, width, height);
            }
            ResetBoxVar();

            ++curPtr;
            SetPictureBoxPicture();
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if(keyData == Keys.N)
            {
                btn_next_picture.PerformClick();
                return true;
            }
            if (keyData == Keys.P)
            {

                btn_pre_picture.PerformClick();
                return true;
            }
            return base.ProcessCmdKey(ref msg, keyData);　// 其他键按默认处理　
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            btn_next_picture.Enabled = false;
            btn_pre_picture.Enabled = false;

            //检测picture上的鼠标事件
            pictureBox1.MouseEnter += new EventHandler(pictureBox1_OnMouseEnter);
            pictureBox1.MouseDown += new MouseEventHandler(pictureBox1_OnMouseDown);
            pictureBox1.MouseMove += new MouseEventHandler(pictureBox1_OnMouseMove);
            pictureBox1.MouseUp += new MouseEventHandler(pictureBox1_OnMouseUp);
            pictureBox1.Paint += new PaintEventHandler(pictureBox1_OnPaint);

            TB_basepath.Text = Path.Combine(new string[] {System.Environment.CurrentDirectory, "images"});
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

            if (Btn_SetConfig.Enabled == false)
            {
                //写入当前所有annotation
                try
                {
                    StreamWriter sw = new StreamWriter("annotation.txt", false);
                    //sw.Write(img.Count);

                    for (int i = 0; i < img.Count; ++i)
                    {
                        if (img[i].box != null && img[i].box.Width != 0 && img[i].box.Height != 0)
                        {
                            sw.WriteLine(1);
                            sw.WriteLine(img[i].box.X + " " + img[i].box.Y + " " + img[i].box.Width + " " + img[i].box.Height);
                        }
                        else
                        {
                            sw.WriteLine(0);
                        }
                    }

                    sw.Close();
                }
                catch (IOException ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            base.OnClosing(e);
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            image_height_ = (int)((double)(pictureBox1.Height / 2.5)) - 50;
            image_width_ = (int)((double)(pictureBox1.Width / 2.5)) - 50;
            image_x_ = 100;// (pictureBox1.Width - image_width_) / 2;
            image_y_ = 100;// (pictureBox1.Height - image_height_) / 2;
            pictureBox1.Invalidate();
        }

        private void ResetBoxVar()
        {
            endPoint_ = Point.Empty;
            mouseDownPoint_ = Point.Empty;
            SelectedImage = false;
            mouseDown_ = false;
            selectImageRect_.Width = selectImageRect_.Height = 0;
            //btn_next_picture.Enabled = false;
        }

        private void SetPictureBoxPicture()
        {
            picture_path.Text = img[curPtr].path;
            pictureBox1.Invalidate();

            //g.Dispose();
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

        private void button1_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog folderSelectDlg = new FolderBrowserDialog();
            folderSelectDlg.Description = "请选择文件夹路径";

            if (folderSelectDlg.ShowDialog() == DialogResult.OK)
            {
                string folderPath = folderSelectDlg.SelectedPath;
                TB_basepath.Text = folderPath;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (!Directory.Exists(TB_basepath.Text))
            {
                MessageBox.Show("选择的路径不是一个文件夹路径");
                return;
            }
            btn_next_picture.Enabled = true;
            btn_pre_picture.Enabled = true;
            Btn_SetConfig.Enabled = false;
            basepath = TB_basepath.Text;

            img = new List<ImageInfo>();

            DirectoryInfo TheFolder = new DirectoryInfo(basepath);
            foreach (FileInfo NextFile in TheFolder.GetFiles())
                img.Add(new ImageInfo(Path.Combine(new string[] { basepath, NextFile.Name })));

            // 读取之前所有的annotation
            try
            {
                FileStream fs = new FileStream("annotation.txt", FileMode.OpenOrCreate);
                StreamReader sr = new StreamReader(fs);
                string line = "";
                curPtr = 0;
                while (line != null)
                {
                    line = sr.ReadLine();
                    if (line == null || line.Equals(""))
                        continue;
                    int cnt = int.Parse(line);
                    Rectangle anno = new Rectangle();
                    for (int j = 0; j < cnt; ++j)
                    {
                        line = sr.ReadLine();
                        string[] num = line.Split(' ');
                        anno.X = int.Parse(num[0]);
                        anno.Y = int.Parse(num[1]);
                        anno.Width = int.Parse(num[2]);
                        anno.Height = int.Parse(num[3]);
                        img[curPtr].box = anno;
                    }
                    if(cnt != 0)
                        ++curPtr;
                }

                sr.Close();
                fs.Close();
            }
            catch (IOException ex)
            {
                MessageBox.Show(ex.Message);
            }

            try
            {
                string name = System.DateTime.Now.ToFileTime().ToString() + "_annotation_backup.txt";
                StreamWriter sw = new StreamWriter(name, false);
                //sw.Write(img.Count);

                for (int i = 0; i < img.Count; ++i)
                {
                    if (img[i].box != null && img[i].box.Width != 0 && img[i].box.Height != 0)
                    {
                        sw.WriteLine(1);
                        sw.WriteLine(img[i].box.X + " " + img[i].box.Y + " " + img[i].box.Width + " " + img[i].box.Height);
                    }
                    else
                    {
                        sw.WriteLine(0);
                    }
                }

                sw.Close();
            }
            catch (IOException ex)
            {
                MessageBox.Show(ex.Message);
            }

            //读取所有head-box的annotation
            try
            {
                FileStream fs = new FileStream("head_boxes.txt", FileMode.Open);
                StreamReader sr = new StreamReader(fs);
                string line = "";
                int tmp_Ptr = 0;
                while (line != null)
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
                        img[tmp_Ptr].GT_Head_box = anno;
                    }
                    ++ tmp_Ptr;
                }
                if(tmp_Ptr != img.Count)
                {
                    MessageBox.Show("head_boxes与数据库不匹配，img_num vs head_box_num = " + img.Count + " " + tmp_Ptr);
                }
                sr.Close();
                fs.Close();
            }
            catch (IOException ex)
            {
                MessageBox.Show(ex.Message);
            }

            if (curPtr == img.Count)
                --curPtr;
            ResetBoxVar();
            SetPictureBoxPicture();
        }
    }
}
