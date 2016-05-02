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
using System.Drawing.Imaging;
using System.Drawing.Drawing2D;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;

namespace BoundingBoxSelect
{
    public partial class FrameArea : UserControl
    {

        #region Variable
        private int image_width_;
        private int image_height_;

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
        
        [Serializable]
        public class ImageInfo
        {
            public string path;
            public List<Rectangle> box;
            public List<int> id_list;
            public ImageInfo(string _path)
            {
                path = _path;
                box = new List<Rectangle>();
                id_list = new List<int>();
            }

        }
        
        private List<ImageInfo> img;
        private int curPtr;
        #endregion

        #region Properties

        public List<ImageInfo> Image
        {
            get { return img; }
        }

        public int CurPtr
        {
            get { return curPtr; }
        }

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]

        internal Cursor SelectCursor
        {
            get { return selectCursor_; }
            set { selectCursor_ = value; }
        }

        internal Cursor DrawCursor
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
                    Pb_frame_.Invalidate();
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

        #region Private Method

        //计算resize的小矩形位置
        private void CalCulateSizeGripRect()
        {
            Rectangle rect = SelectImageRect;

            int x = rect.X;
            int y = rect.Y;
            int centerX = x + rect.Width / 2;
            int centerY = y + rect.Height / 2;

            Dictionary<SizeGrip, Rectangle> list = SizeGripRectList;
            list.Clear();

            list.Add(
                SizeGrip.TopLeft,
                new Rectangle(x - 2, y - 2, 5, 5));
            list.Add(
                SizeGrip.TopRight,
                new Rectangle(rect.Right - 2, y - 2, 5, 5));
            list.Add(
                SizeGrip.BottomLeft,
                new Rectangle(x - 2, rect.Bottom - 2, 5, 5));
            list.Add(
                SizeGrip.BottomRight,
                new Rectangle(rect.Right - 2, rect.Bottom - 2, 5, 5));
            list.Add(
                SizeGrip.Top,
                new Rectangle(centerX - 2, y - 2, 5, 5));
            list.Add(
                SizeGrip.Bottom,
                new Rectangle(centerX - 2, rect.Bottom - 2, 5, 5));
            list.Add(
                SizeGrip.Left,
                new Rectangle(x - 2, centerY - 2, 5, 5));
            list.Add(
                SizeGrip.Right,
                new Rectangle(rect.Right - 2, centerY - 2, 5, 5));
        }

        private Rectangle ImageBoundsToRect(Rectangle bounds)
        {
            Rectangle rect = bounds;
            int x = 0;
            int y = 0;

            x = Math.Min(rect.X, rect.Right);
            y = Math.Min(rect.Y, rect.Bottom);

            rect.X = x;
            rect.Y = y;
            rect.Width = Math.Max(1, Math.Abs(rect.Width));
            rect.Height = Math.Max(1, Math.Abs(rect.Height));
            return rect;
        }

        private Rectangle GetSelectImageRect(Point endPoint)
        {
            selectImageBounds_ = Rectangle.FromLTRB(
                mouseDownPoint_.X,
                mouseDownPoint_.Y,
                endPoint.X,
                endPoint.Y);

            return ImageBoundsToRect(selectImageBounds_);
        }

        private void ChangeSelctImageRect(Point point)
        {
            Rectangle rect = selectImageBounds_;
            int left = rect.Left;
            int top = rect.Top;
            int right = rect.Right;
            int bottom = rect.Bottom;
            bool sizeGripAll = false;

            switch (SizeGrip)
            {
                case SizeGrip.All:
                    rect.Offset(
                        point.X - mouseDownPoint_.X, point.Y - mouseDownPoint_.Y);
                    sizeGripAll = true;
                    break;
                case SizeGrip.TopLeft:
                    left = point.X;
                    top = point.Y;
                    break;
                case SizeGrip.TopRight:
                    right = point.X;
                    top = point.Y;
                    break;
                case SizeGrip.BottomLeft:
                    left = point.X;
                    bottom = point.Y;
                    break;
                case SizeGrip.BottomRight:
                    right = point.X;
                    bottom = point.Y;
                    break;
                case SizeGrip.Top:
                    top = point.Y;
                    break;
                case SizeGrip.Bottom:
                    bottom = point.Y;
                    break;
                case SizeGrip.Left:
                    left = point.X;
                    break;
                case SizeGrip.Right:
                    right = point.X;
                    break;
            }

            //_selectImageBounds = rect;
            if (!sizeGripAll)
            {
                rect.X = left;
                rect.Y = top;
                rect.Width = right - left;
                rect.Height = bottom - top;
            }
            mouseDownPoint_ = point;
            selectImageBounds_ = rect;
            SelectImageRect = ImageBoundsToRect(rect); ;
        }

        private void DrawOperate(Graphics g)
        {
            foreach (OperateObject obj in OperateManager.OperateList)
            {
                switch (obj.OperateType)
                {
                    case OperateType.DrawRectangle:
                        using (Pen pen = new Pen(obj.Color))
                        {
                            g.DrawRectangle(
                                pen,
                                (Rectangle)obj.Data);
                        }
                        break;
                    case OperateType.DrawEllipse:
                        using (Pen pen = new Pen(obj.Color))
                        {
                            g.DrawEllipse(
                                pen,
                                (Rectangle)obj.Data);
                        }
                        break;
                    case OperateType.DrawLine:
                        using (Pen pen = new Pen(obj.Color))
                        {
                            g.DrawLines(pen, obj.Data as Point[]);
                        }
                        break;
                }
            }
        }

        private void SetSizeGrip(Point point)
        {
            SizeGrip = SizeGrip.None;
            foreach (SizeGrip sizeGrip in SizeGripRectList.Keys)
            {
                if (SizeGripRectList[sizeGrip].Contains(point))
                {
                    SizeGrip = sizeGrip;
                    break;
                }
            }

            if (SizeGrip == SizeGrip.None)
            {
                //if (SelectImageRect.Contains(point))
                //{
                //    SizeGrip = SizeGrip.All;
                //}
            }

            switch (SizeGrip)
            {
                case SizeGrip.TopLeft:
                    topLeft = point;
                    Cursor = Cursors.SizeNWSE;
                    break;
                case SizeGrip.BottomRight:
                    bottomRight = point;
                    Cursor = Cursors.SizeNWSE;
                    break;
                case SizeGrip.TopRight:
                    topLeft.Y = point.Y;
                    bottomRight.X = point.X;
                    Cursor = Cursors.SizeNESW;
                    break;
                case SizeGrip.BottomLeft:
                    bottomRight.Y = point.Y;
                    topLeft.X = point.X;
                    Cursor = Cursors.SizeNESW;
                    break;
                case SizeGrip.Top:
                    topLeft.Y = point.Y;
                    Cursor = Cursors.SizeNS;
                    break;
                case SizeGrip.Bottom:
                    bottomRight.Y = point.Y;
                    Cursor = Cursors.SizeNS;
                    break;
                case SizeGrip.Left:
                    topLeft.X = point.X;
                    Cursor = Cursors.SizeWE;
                    break;
                case SizeGrip.Right:
                    bottomRight.X = point.X;
                    Cursor = Cursors.SizeWE;
                    break;
                //case SizeGrip.All:
                //    Cursor = Cursors.SizeAll;
                //    break;
                default:
                    Cursor = SelectCursor;
                    break;
            }
        }

        private void ResetBoxVar()
        {
            endPoint_ = Point.Empty;
            mouseDownPoint_ = Point.Empty;
            SelectedImage = false;
            mouseDown_ = false;
            selectImageRect_.Width = selectImageRect_.Height = 0;
        }

        private void SetPictureBoxPicture()
        {
            image_width_ = Pb_frame_.Width / 2;
            image_height_ = Pb_frame_.Height / 2;
            Pb_frame_.Invalidate();
        }

        #endregion

        #region Mouse Event
        private void pictureBox_OnMouseEnter(object sender, EventArgs e)
        {
            if (img == null)
                return;
            Cursor = SelectCursor;
        }

        private void pictureBox_OnMouseDown(object sender, MouseEventArgs e)
        {
            if (img == null)
                return;
            if (e.Button == MouseButtons.Left)
            {
                if (SelectedImage) //已经圈定
                {
                    if (SizeGrip != SizeGrip.None)
                    {
                        mouseDown_ = true;
                        mouseDownPoint_ = e.Location;
                        Pb_frame_.Invalidate();
                    }
                }
                else
                {
                    mouseDown_ = true;
                    mouseDownPoint_ = e.Location;
                    topLeft = e.Location;
                }
            }
        }

        private void pictureBox_OnMouseMove(object sender, MouseEventArgs e)
        {
            if (img == null)
                return;
            //按下左键的时候移动鼠标
            if (mouseDown_)
            {
                if (!SelectedImage)
                {
                    SelectImageRect = GetSelectImageRect(e.Location);
                }
                else
                {
                    if (SizeGrip != SizeGrip.None)
                    {
                        ChangeSelctImageRect(e.Location);
                    }
                }

            }
            else //没有左键的时候移动鼠标
            {
                if (!SelectedImage) //没有选中矩形的时候do nothing
                {
                    //toolTip.SetToolTip(this, ToolTipStartCapture);
                }
                else
                {//有选中的矩形
                    if (OperateManager.OperateCount == 0)
                    {
                        SetSizeGrip(e.Location);
                    }
                }
            }
        }

        private void pictureBox_OnMouseUp(object sender, MouseEventArgs e)
        {
            if (img == null)
                return;
            if (e.Button == MouseButtons.Left)
            {
                if (!SelectedImage)
                {
                    SelectImageRect = GetSelectImageRect(e.Location);
                    if (!SelectImageRect.IsEmpty)
                    {
                        SelectedImage = true;
                        endPoint_ = e.Location;
                        bottomRight = e.Location;
                    }
                }
                else
                {
                    endPoint_ = e.Location;
                    Pb_frame_.Invalidate();
                    if (SizeGrip != SizeGrip.None)
                    {
                        selectImageBounds_ = SelectImageRect;
                    }
                }

                mouseDown_ = false;
            }
        }

        private void pictureBox_OnPaint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            g.SmoothingMode = SmoothingMode.AntiAlias;
            //画body box
            if (img != null && img.Count != 0)
            {
                Bitmap _img = new Bitmap(img[curPtr].path);
                int ori_image_width = _img.Width;
                int ori_image_height = _img.Height;
                g.DrawImage(_img, new Rectangle((Pb_frame_.Width - image_width_)/2, (Pb_frame_.Height - image_height_)/2, image_width_, image_height_));
                using (Pen pen = new Pen(Color.Red, 2))
                {
                    scale_x = (double)(ori_image_width) / image_width_;
                    scale_y = (double)(ori_image_height) / image_height_;
                    for (int i=0;i < img[curPtr].box.Count;++i)
                    {
                        Rectangle rect = img[curPtr].box[i];
                        int id = img[curPtr].id_list[i];
                        int xmin = (int)Math.Floor((double)rect.X / scale_x);
                        int ymin = (int)Math.Floor((double)rect.Y / scale_y);
                        g.DrawRectangle(pen, new Rectangle(xmin, ymin, (int)((double)rect.Width / scale_x), (int)((double)rect.Height / scale_y)));
                        g.FillRectangle(new SolidBrush(Color.LightYellow), xmin, ymin, 15, 15);
                        g.DrawString(id.ToString(), new Font("Arial", 10), new SolidBrush(Color.Black), new Point(xmin, ymin));
                    }
                }
            }
            if (img != null && SelectImageRect.Width != 0 && SelectImageRect.Height != 0)
            {
                Rectangle rect = SelectImageRect;
                if (mouseDown_)
                {
                    if (!SelectedImage || SizeGrip != SizeGrip.None)
                    {
                        using (SolidBrush brush = new SolidBrush(
                            Color.FromArgb(90, Color.AliceBlue)))
                        {
                            g.FillRectangle(brush, rect);
                        }
                    }
                }

                using (Pen pen = new Pen(Color.Yellow, 2))
                {
                    g.DrawRectangle(pen, rect);
                    using (SolidBrush brush = new SolidBrush(Color.AliceBlue))
                    {
                        foreach (Rectangle sizeGripRect in SizeGripRectList.Values)
                        {
                            g.FillRectangle(
                                brush,
                                sizeGripRect);
                        }
                    }

                }

                DrawOperate(g);
            }
        }

        #endregion

        #region Button Click
        private void Btn_pre_frame_Click(object sender, EventArgs e)
        {
            if (img == null)
                return;
            if (curPtr == 0)
            {
                MessageBox.Show("已经是第一张图片");
                return;
            }
            --curPtr;
            ResetBoxVar();
            image_width_ = Pb_frame_.Width / 2;
            image_height_ = Pb_frame_.Height / 2;
            Pb_frame_.Invalidate();
        }

        private void Btn_next_frame_Click(object sender, EventArgs e)
        {
            if (img == null)
                return;
            if (curPtr >= img.Count - 1)
            {
                MessageBox.Show("已经是最后一张图片");
                return;
            }
            ++curPtr;
            ResetBoxVar();
            image_width_ = Pb_frame_.Width / 2;
            image_height_ = Pb_frame_.Height / 2;
            Pb_frame_.Invalidate();
            Btn_next_frame_.Enabled = false;
        }

        private void Btn_submit_Bbox_Click(object sender, EventArgs e)
        {
            if (img == null)
                return;
            if (Tb_id_.Text == "")
            {
                MessageBox.Show("请输入编号");
                return;
            }
            if(!SelectedImage)
            {
                MessageBox.Show("请框定图中行人");
                return;
            }
            topLeft.X = Math.Min(topLeft.X, bottomRight.X);
            topLeft.Y = Math.Min(topLeft.Y, bottomRight.Y);
            bottomRight.X = Math.Max(topLeft.X, bottomRight.X);
            bottomRight.Y = Math.Max(topLeft.Y, bottomRight.Y);
            int xmin = (int)Math.Floor((double)topLeft.X * scale_x);
            int xmax = (int)Math.Floor((double)bottomRight.X * scale_x);
            int ymin = (int)Math.Floor((double)topLeft.Y * scale_y);
            int ymax = (int)Math.Floor((double)bottomRight.Y * scale_y);
            img[curPtr].box.Add(new Rectangle(xmin, ymin, xmax - xmin, ymax - ymin));
            img[curPtr].id_list.Add(int.Parse(Tb_id_.Text));
            ResetBoxVar();
            Btn_next_frame_.Enabled = true;
        }

        private void Btn_no_people_Click(object sender, EventArgs e)
        {
            Btn_next_frame_.Enabled = true;
            Btn_next_frame_.PerformClick();
            Btn_next_frame_.Enabled = false;
        }

        private void Btn_redo_Click(object sender, EventArgs e)
        {
            if(img[curPtr].box.Count != 0)
            {
                int last_idx = img[curPtr].box.Count - 1;
                img[curPtr].box.RemoveAt(last_idx);
                img[curPtr].id_list.RemoveAt(last_idx);
            }
        }
        #endregion

        public FrameArea()
        {
            InitializeComponent();
        }

        private static List<T> Clone<T>(object List)
        {
            using (Stream objectStream = new MemoryStream())
            {
                IFormatter formatter = new BinaryFormatter();
                formatter.Serialize(objectStream, List);
                objectStream.Seek(0, SeekOrigin.Begin);
                return formatter.Deserialize(objectStream) as List<T>;
            }
        }

        #region Public Method
        public void init(List<ImageInfo> _img, int _curPtr)
        {
            img = Clone<ImageInfo>(_img);
            curPtr = _curPtr;
            image_height_ = Pb_frame_.Height / 2;
            image_width_ = Pb_frame_.Width / 2;
            Btn_submit_Bbox_.Enabled = true;
            Btn_pre_frame_.Enabled = true;
            Btn_no_people_.Enabled = true;
            Btn_redo_.Enabled = true;
            Pb_frame_.Enabled = true;
            Tb_id_.Enabled = true;
        }

        public void ToNextPicture()
        {
            if (img == null)
                return;
            if (curPtr >= img.Count - 1)
            {
                return;
            }
            ++curPtr;
            ResetBoxVar();
            image_width_ = Pb_frame_.Width / 2;
            image_height_ = Pb_frame_.Height / 2;
            Pb_frame_.Invalidate();
        }
        public void ToPrePicture()
        {
            if (img == null)
                return;
            if (curPtr == 0)
            {
                return;
            }
            --curPtr;
            ResetBoxVar();
            image_width_ = Pb_frame_.Width / 2;
            image_height_ = Pb_frame_.Height / 2;
            Pb_frame_.Invalidate();
        }
        #endregion

        private void FrameArea_Load(object sender, EventArgs e)
        {
            Btn_next_frame_.Enabled = false;
            Btn_pre_frame_.Enabled = false;
            Btn_submit_Bbox_.Enabled = false;
            Btn_no_people_.Enabled = false;
            Btn_redo_.Enabled = false;
            Pb_frame_.Enabled = false;
            Tb_id_.Enabled = false;
            img = new List<ImageInfo>();
            Pb_frame_.MouseEnter += new EventHandler(pictureBox_OnMouseEnter);
            Pb_frame_.MouseDown += new MouseEventHandler(pictureBox_OnMouseDown);
            Pb_frame_.MouseMove += new MouseEventHandler(pictureBox_OnMouseMove);
            Pb_frame_.MouseUp += new MouseEventHandler(pictureBox_OnMouseUp);
            Pb_frame_.Paint += new PaintEventHandler(pictureBox_OnPaint);
        }

        protected override void OnClientSizeChanged(EventArgs e)
        {
            base.OnClientSizeChanged(e);
            Pb_frame_.Size = new Size(this.Width, this.Height - 40);
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            Pb_frame_.Invalidate();
        }

        

        
    }
}
