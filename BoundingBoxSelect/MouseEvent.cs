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

namespace BoundingBoxSelect
{
    public partial class Form1 : Form
    {

        #region Private
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
                    case OperateType.DrawArrow:
                        Point[] points = obj.Data as Point[];
                        using (Pen pen = new Pen(obj.Color))
                        {
                            pen.EndCap = LineCap.Custom;
                            pen.CustomEndCap = new AdjustableArrowCap(4, 4, true);
                            g.DrawLine(pen, points[0], points[1]);
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


        #endregion


        private void pictureBox1_OnMouseEnter(object sender, EventArgs e)
        {
            //base.OnMouseEnter(e);
            Cursor = SelectCursor;
        }

        private void pictureBox1_OnMouseDown(object sender, MouseEventArgs e)
        {
            //base.OnMouseDown(e);

            if (e.Button == MouseButtons.Left)
            {
                btn_next_picture.Enabled = true;
                if (SelectedImage) //已经圈定
                {
                    if (SizeGrip != SizeGrip.None)
                    {
                        mouseDown_ = true;
                        mouseDownPoint_ = e.Location;
                        pictureBox1.Invalidate();
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

        private void pictureBox1_OnMouseMove(object sender, MouseEventArgs e)
        {
            //base.OnMouseMove(e);
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

        private void pictureBox1_OnMouseUp(object sender, MouseEventArgs e)
        {
            //base.OnMouseUp(e);

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
                    pictureBox1.Invalidate();
                    if (SizeGrip != SizeGrip.None)
                    {
                        selectImageBounds_ = SelectImageRect;
                    }
                }

                mouseDown_ = false;
                //mouseDownPoint_ = Point.Empty;
            }
        }


        private void pictureBox1_OnPaint(object sender, PaintEventArgs e)
        {
            //base.OnPaint(e);

            Graphics g = e.Graphics;
            g.SmoothingMode = SmoothingMode.AntiAlias;
            //画headbox
            if (Btn_SetConfig.Enabled == false && img != null && img.Count != 0)
            {
                Bitmap _img = new Bitmap(img[curPtr].path);
                int ori_image_width = _img.Width;
                int ori_image_height = _img.Height;
                g.DrawImage(_img, new Rectangle(0, 0, image_width_, image_height_));
                using (Pen pen = new Pen(Color.Red, 2))
                {
                    scale_x = (double)(ori_image_width) / image_width_;
                    scale_y = (double)(ori_image_height) / image_height_;
                    int xmin = (int)Math.Floor((double)img[curPtr].GT_Head_box[0].X / scale_x);
                    int ymin = (int)Math.Floor((double)img[curPtr].GT_Head_box[0].Y / scale_y);
                    g.DrawRectangle(pen, new Rectangle(xmin, ymin, (int)((double)img[curPtr].GT_Head_box[0].Width / scale_x), (int)((double)img[curPtr].GT_Head_box[0].Height / scale_y)));
                    if (img[curPtr].fakeBox.Count != 0)
                    {
                        foreach (Rectangle tmp in img[curPtr].fakeBox)
                        {
                            g.DrawRectangle(pen, tmp);
                        }
                    }
                }
            }
            if (SelectImageRect.Width != 0 && SelectImageRect.Height != 0)
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
                    foreach(Rectangle tmp in img[curPtr].fakeBox)
                    {
                        g.DrawRectangle(pen, tmp);
                    }
                    
                }

                DrawOperate(g);
            }
        }
    }
}