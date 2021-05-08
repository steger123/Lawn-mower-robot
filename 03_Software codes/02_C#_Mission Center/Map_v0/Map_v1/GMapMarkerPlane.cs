using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//https://nugrohowidijatmiko.blogspot.com/2018/06/tutorial-c-rotate-gmapnet-marker-from.html
using GMap.NET;
using GMap.NET.WindowsForms;
using GMap.NET.WindowsForms.Markers;

using System.Drawing;
using System.Drawing.Drawing2D;

namespace Map_v1
{
    // used to override the drawing of the waypoint box bounding

    class GMapMarkerPlane : GMapMarker
    {
      //  private readonly Bitmap icon = Resource.planetracker;
        float heading = 0;

        public GMapMarkerPlane(PointLatLng p, float heading)
            : base(p)
        {
            this.heading = heading;
          //  Size = icon.Size;
        }

        public override void OnRender(Graphics g)
        {
            Matrix temp = g.Transform;
            g.TranslateTransform(LocalPosition.X, LocalPosition.Y);
            g.RotateTransform(-Overlay.Control.Bearing);

            try
            {
                g.RotateTransform(heading);
            }
            catch { }

        //    g.DrawImageUnscaled(icon, icon.Width / -2, icon.Height / -2);
            g.Transform = temp;
        }

    }
}
