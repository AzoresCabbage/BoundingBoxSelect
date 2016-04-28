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

    internal enum OperateType
    {
        None = 0,
        DrawRectangle,
        DrawEllipse,
        DrawArrow,
        DrawLine,
        DrawText
    }

    internal enum SizeGrip
    {
        None = 0,
        Top,
        Bottom,
        Left,
        Right,
        TopLeft,
        TopRight,
        BottomLeft,
        BottomRight,
        All
    }

}
