using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Runtime.Serialization;
using GMap.NET;
using GMap.NET.WindowsForms;

namespace Map_v1
{
    
    [Serializable]
    public class GMarkerArrow : GMapMarker, ISerializable
    {
        [NonSerialized]
        public Brush Fill = new SolidBrush(Color.FromArgb(155, Color.Blue));
        public Pen myPen = new Pen(Color.Yellow, 5);
        
        //myPen.DashStyle = System.Drawing.Drawing2D.DashStyle.Dot;
        //https://www.informit.com/articles/article.aspx?p=25357&seqNum=3#:~:text=A%20pen%20is%20an%20object,can%20also%20create%20your%20own.
        //[NonSerialized]
        //public Pen Pen = new Pen(Brushes.Blue, 5);

        static readonly Point[] Arrow = new Point[] { new Point(-7, 7), new Point(0, -22), new Point(7, 7), new Point(0, 2) };

        public float Bearing = 0;
        private float scale = 1;

        public float Scale
        {
            get
            {
                return scale;
            }
            set
            {
                scale = value;

                Size = new System.Drawing.Size((int)(14 * scale), (int)(14 * scale));
                Offset = new System.Drawing.Point(-Size.Width / 2, (int)(-Size.Height / 1.4));
            }
        }

        public GMarkerArrow(PointLatLng p)
            : base(p)
        {
            Scale = (float)1.4;
        }

        public override void OnRender(Graphics g)
        {
            //g.DrawRectangle(myPen, new System.Drawing.Rectangle(ToolTipPosition.X-10, ToolTipPosition.Y-10, 20, 20));
            g.DrawEllipse(myPen, new System.Drawing.Rectangle(ToolTipPosition.X - 15, ToolTipPosition.Y - 15, 30, 30));
            
            {
                g.TranslateTransform(ToolTipPosition.X, ToolTipPosition.Y);
                var c = g.BeginContainer();
                {
                    g.RotateTransform(Bearing - Overlay.Control.Bearing);
                    g.ScaleTransform(Scale, Scale);
                   // g.DrawEllipse(1, 1, 1, 1, 1);
                    g.FillPolygon(Fill, Arrow);
                }
                g.EndContainer(c);
                g.TranslateTransform(-ToolTipPosition.X, -ToolTipPosition.Y);
            }
        }

        public override void Dispose()
        {
            if(myPen != null)
            {
               myPen.Dispose();
               myPen = null;
            }

            if (Fill != null)
            {
                Fill.Dispose();
                Fill = null;
            }

            base.Dispose();
        }

        #region ISerializable Members

        void ISerializable.GetObjectData(SerializationInfo info, StreamingContext context)
        {
            base.GetObjectData(info, context);
        }

        protected GMarkerArrow(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }

        #endregion
    }

}
