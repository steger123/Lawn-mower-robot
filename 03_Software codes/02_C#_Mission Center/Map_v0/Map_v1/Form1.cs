using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using GMap.NET;
using GMap.NET.MapProviders;
using GMap.NET.WindowsForms;
using GMap.NET.WindowsForms.Markers;
using GMap.NET.WindowsForms.ToolTips;
using System.IO.Ports;
using System.IO;

using System.Threading;
using System.Data.OleDb;
//using Map_v1;   //my class


//https://archive.codeplex.com/?p=greatmaps
//https://github.com/radioman/greatmaps/blob/master/Demo.WindowsForms/Forms/MainForm.cs
// https://archive.codeplex.com/?p=greatmaps
// http://www.independent-software.com/gmap-net-beginners-tutorial-adding-clickable-markers-to-your-map-updates-for-vs2015-and-gmap-1-7.html
//format a selection: Ctrl+K, Ctrl+F
//format a document: Ctrl+K, Ctrl+D

namespace Map_v1
{
    public partial class Form1 : Form
    {
     //   public SerialPort _serialPort { get; private set; }
        string[] _ports;
        public bool Open = false;

        private SerialPort comport = new SerialPort();
    //  comport.DataReceived += new SerialDataReceivedEventHandler(port_DataReceived); // !port_DataReceived comport = serialPort1
    
        struct PolyBoundry
        {
            public double Xmin;
            public double Xmax;
            public double Ymin;
            public double Ymax;
        }

        bool drag;
        bool btnStartPointFlag = false;
        bool noMoreStartPoint = false;
        GMapOverlay markers = new GMapOverlay("markers");
        GMapOverlay markersStart = new GMapOverlay("markersStart"); // only for one, where the route to be start
        GMapOverlay markersNext = new GMapOverlay("markersNext"); // only for the next possible waypoint, check inside or not
        GMapOverlay markersWP = new GMapOverlay("markersWP"); // only for one, for the next, calulated waypoint
        GMapOverlay markersNextT = new GMapOverlay("markersNextT"); // only for one, for the next, calulated waypoint
        GMapOverlay markersPath = new GMapOverlay("markersPath"); // Show the WP ont he route in Gridview

        GMapOverlay polygons = new GMapOverlay("polygons");
        GMapOverlay routes = new GMapOverlay("routes");
        //   GMapOverlay routes = new GMapOverlay("routes");

        GMapMarker activeMarker;

        double startLat, startLng;
        // object activeMarker;

        // for heading, icon rotating :
        //   private readonly Bitmap icon = Resources.planetracker;
        public static GMapOverlay routesoverlay;

        private OleDbConnection connetion = new OleDbConnection();

        myMap mymap = new myMap();          // My CLASS !!!!! reach the function in myMap.cs with: mymap. ...
        double area = 0; //Calulated area in m^2

        public Form1()
        {
            InitializeComponent();
            routesoverlay = new GMapOverlay("routes");
            map.Overlays.Add(routesoverlay);

            dataGridViewPlan.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataGridView2.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            connetion.ConnectionString = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=C:\00_Robot\03_Visual Studio\Farmer.accdb;Persist Security Info=False;";
            //https://www.youtube.com/watch?v=AE-PS6-sL7U 
            //https://www.connectionstrings.com/access-2007/
            int val;
            val = mymap.justGo(); // JUST try
        }


        private void Form1_Load_1(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'farmerDataSet.waypoints' table. You can move, or remove it, as needed.
            this.waypointsTableAdapter.Fill(this.farmerDataSet.waypoints);
            myTooltip.Show("Tooltip text goes here", btnStartPoint);

            GMaps.Instance.Mode = AccessMode.ServerAndCache;

            map.RoutesEnabled = true;
            map.PolygonsEnabled = true;
            map.MarkersEnabled = true;
            map.NegativeMode = false;
            map.RetryLoadTile = 0;
            map.ShowTileGridLines = false;
            map.AllowDrop = true;
            map.IgnoreMarkerOnMouseWheel = true;
            map.DragButton = MouseButtons.Left;
            map.DisableFocusOnMouseEnter = false;
            map.MinZoom = 0;
            map.MaxZoom = 24;
            map.Zoom = 18;
            map.Position = new PointLatLng(28.458505, 77.287437); //start coordinate
            // map.MapProvider = GMapProviders.GoogleMap;
            map.MapProvider = GMap.NET.MapProviders.BingHybridMapProvider.Instance;
            GMap.NET.GMaps.Instance.Mode = GMap.NET.AccessMode.ServerOnly;
            // GMap.NET.GMaps.Instance.Mode = GMap.NET.AccessMode.CacheOnly; //if no internet

            map.Overlays.Add(markers);
            map.Overlays.Add(routes);
            map.Overlays.Add(polygons);

            cmbMapTypes.Items.Add("Google Maps Satelite");
            cmbMapTypes.Items.Add("Google Maps Callejero");
            cmbMapTypes.Items.Add("Google Maps Hibrid");
            cmbMapTypes.Items.Add("OpenStreetMap");
            cmbMapTypes.Items.Add("OpenCycleMap");
            cmbMapTypes.Items.Add("Bing");
            cmbMapTypes.SelectedIndex = 5;

            cmbAreaUnit.Items.Add("m^2");
            cmbAreaUnit.Items.Add("ha");
            cmbAreaUnit.Items.Add("km^2");
            cmbAreaUnit.Items.Add("ft^2");
            cmbAreaUnit.Items.Add("acres");
            cmbAreaUnit.Items.Add("miles^2");
            cmbAreaUnit.SelectedIndex = 0;

            try
            {

                connetion.Open();
                lblCheckConneton.Text = "Connected";
                connetion.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error " + ex);
            }
        }


        private void port_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
			// If the com port has been closed, do nothing
			if (!comport.IsOpen) return;
        string data = comport.ReadExisting();
        lbRoverMsg.Items.Add(data);

        }

        private void btnLoadIntoMap_Click(object sender, EventArgs e)
        {
            map.ShowCenter = false;  // turn off red cross
            map.MinZoom = 0;
            map.MaxZoom = 24;
            map.Zoom = 18;

            double lat = Convert.ToDouble(txtLat.Text);
            double lng = Convert.ToDouble(txtLong.Text);
            PointLatLng point = new PointLatLng(lat, lng);
            GMapMarker marker = new GMarkerGoogle(point, GMarkerGoogleType.red_pushpin);
            markers.Markers.Add(marker);

            marker.ToolTipText = "hello\nout there";

            List<PointLatLng> points = new List<PointLatLng>();
            points.Add(new PointLatLng(28.459806, 77.2874577));
            points.Add(new PointLatLng(28.459806, 77.2884577));
            points.Add(new PointLatLng(28.458806, 77.2884577));
            points.Add(new PointLatLng(28.458806, 77.2874577));
            GMapPolygon polygon = new GMapPolygon(points, "My polygon");
            polygon.Fill = new SolidBrush(Color.FromArgb(50, Color.Red));
            polygon.Stroke = new Pen(Color.Red, 1);
            polygons.Polygons.Add(polygon);


            List<PointLatLng> r_points = new List<PointLatLng>();
            r_points.Add(new PointLatLng(28.449806, 77.2874577));
            r_points.Add(new PointLatLng(28.459806, 77.2884577));
            r_points.Add(new PointLatLng(28.458806, 77.2884577));
            r_points.Add(new PointLatLng(28.458806, 77.2874577));
            GMapRoute route = new GMapRoute(r_points, "A walk in the park");
            route.Stroke = new Pen(Color.Blue, 3);
            routes.Routes.Add(route);

            double distance = Math.Round(route.Distance, 3);
            txtOutput.Text = distance + " km";
            map.Refresh();
        }

        private void map_MouseClick(object sender, MouseEventArgs e)  // *** add NEW markers ! ****
        {
            if (e.Button == MouseButtons.Right)
            {
                var position = map.FromLocalToLatLng(e.X, e.Y);
                double lat = position.Lat;
                double lng = position.Lng;
                txtX.Text = lat + " ";
                txtY.Text = lng + " ";
                // var lat = map.FromLocalToLatLng(e.X, e.Y).Lat;
                // var lng = map.FromLocalToLatLng(e.X, e.Y).Lng;

                // map.Position = point; // auto  ceter
                // var markers = new GMapOverlay("markers");
                if ((btnStartPoint.FlatStyle == FlatStyle.Flat) && (noMoreStartPoint == false))
                {
                    var markerStart = new GMarkerGoogle(position, GMarkerGoogleType.red);
                    map.Overlays.Add(markersStart); markersStart.Markers.Add(markerStart);
                    noMoreStartPoint = true;
                }
                else
                {
                    var marker = new GMarkerGoogle(position, GMarkerGoogleType.blue);
                    map.Overlays.Add(markers); markers.Markers.Add(marker);
                }
            }
        } // end map_MouseClick

        
        private void map_OnMarkerLeave(GMapMarker item)
        {
            if (false == drag)
            {
                activeMarker = null;
            }
        }
        private void map_OnMarkerEnter(GMapMarker item)  // hoovering OnMarkerLeave
        {
            if (false == drag)
            {
                startLat = item.Position.Lat;
                startLng = item.Position.Lng;
                activeMarker = item;
                txtX.Text = startLat.ToString();
                txtY.Text = startLng.ToString();
            }
        }

        // Moving marker 3 routines
        private void map_MouseDown(object sender, MouseEventArgs e)
        {
            if ((e.Button == MouseButtons.Left) && (map.IsMouseOverMarker) && null != activeMarker)
            {
                drag = true;
                txtOutput.Text = "drag = true";
            }
        }
        // https://presentation427.rssing.com/chan-4175058/all_p131.html

        private void map_MouseMove(object sender, MouseEventArgs e)
        {
            if ((e.Button == MouseButtons.Left) && (drag))
            {
                var prevPos = activeMarker.Position;
                activeMarker.Position = map.FromLocalToLatLng(e.X, e.Y);
                map.Refresh();
            }
        }

        private void map_MouseUp(object sender, MouseEventArgs e)
        {
            if ((e.Button == MouseButtons.Left) && (drag))
            {
                drag = false;
                if (!activeMarker.IsMouseOver)
                {
                    activeMarker = null;
                }
            }
        }


        private void btnRemoveMarker_Click(object sender, EventArgs e)
        {
            if (markers.Markers.Count > 0)
            {
                //markers.Markers.OrderByDescending(1);
                markers.Markers.RemoveAt(markers.Markers.Count - 1);
                //    var x = markers.Markers.
            }
        }


        private void btnConnectMarker_Click(object sender, EventArgs e)
        {
            //if (routes.Routes.Count > 0) return; //WP already connected on the map (double click to connect them)
            if (polygons.Polygons.Count > 0)  // to be improve to remove all !!
            {
                //polygons.Polygons.RemoveAt(polygons.Polygons.Count - 1);
                polygons.Polygons.Clear();
            }
            if (routes.Routes.Count > 0)  // to be improve to remove all !!
            {
                for (int i = 0; i < routes.Routes.Count + 1; i++)
                    routes.Routes.RemoveAt(i);
                routes.Routes.Clear();
            }
            markersNext.Markers.Clear();
            markersNextT.Markers.Clear();
            clearMapCounter = 0;

            // Delete all data from datagridview
            dataGridViewPlan.DataSource = null;
            dataGridViewPlan.Rows.Clear();
            dataGridViewPlan.Refresh();


            //  ClearMap();
            List<PointLatLng> points = new List<PointLatLng>();
            for (int i = 0; i < markers.Markers.Count; i++)     // Collect boundry markers on the MAP & store in points for polygon making
            {
                var x = markers.Markers.ElementAt(i).Position.Lat;
                var y = markers.Markers.ElementAt(i).Position.Lng;
                // txtX.Text = x.ToString();
                // txtY.Text = "Connect";
                points.Add(new PointLatLng(x, y));
            }

            GMapPolygon polygon = new GMapPolygon(points, "My polygon");
            polygon.Fill = new SolidBrush(Color.FromArgb(50, Color.Red));
            polygon.Stroke = new Pen(Color.Red, 1);
            polygons.Polygons.Add(polygon);

            PolyBoundry boundry = calulateBoundry(points); // come cack with min/max X/Y of the boundry of the polygon
            //  txtOutput.Text = PolyBoundry.

            calcPolygonArea(points); //m^2, feet^2 etc.

            drawPath(boundry);  //pass the min/max X/Y to replace the FOR cycles (points can be passed as well)

        }

        private PolyBoundry calulateBoundry(List<PointLatLng> coords)
        {
            PolyBoundry values = new PolyBoundry();

            var center = map.Position;
            var c = coords.Count;
            int minXpoint, maxXpoint, minYpoint, maxYpoint; // records whis element# marker ins the max.&min
            // var X = 0;
            var Xmin = center.Lng; var Xmax = center.Lng;
            var Ymin = center.Lat; var Ymax = center.Lat;

            for (int i = 0; i < coords.Count; i++)
            {
                if (coords.ElementAt(i).Lng < Xmin)
                {
                    Xmin = coords.ElementAt(i).Lng;
                    minXpoint = i; // record ElementAt
                }
                if (coords.ElementAt(i).Lng > Xmax)
                {
                    Xmax = coords.ElementAt(i).Lng;
                    maxXpoint = i; // record ElementAt
                }
                if (coords.ElementAt(i).Lat < Ymin)
                {
                    Ymin = coords.ElementAt(i).Lat;
                    minYpoint = i; // record ElementAt
                }
                if (coords.ElementAt(i).Lat > Ymax)
                {
                    Ymax = coords.ElementAt(i).Lat;
                    maxYpoint = i; // record ElementAt
                }
            }
            values.Xmin = Xmin;
            values.Xmax = Xmax;
            values.Ymin = Ymin;
            values.Ymax = Ymax;
            // MessageBox.Show("Passed Boundry!");
            return values;
        }


        private void calcPolygonArea(List<PointLatLng> coords)
        {
            if (markers.Markers.Count > 2)
            {
                IList<PointLatLng> points = new List<PointLatLng>();
                foreach (PointLatLng coord in coords)
                {
                    PointLatLng p = new PointLatLng(
                      coord.Lng * (System.Math.PI * 6378137 / 180),
                      coord.Lat * (System.Math.PI * 6378137 / 180)
                    );
                    points.Add(p);
                }
                // Add point 0 to the end again:
                points.Add(points[0]); // bug coming if no points on the map !!!

                // Calculate polygon area (in square meters):
                area = System.Math.Abs(points.Take(points.Count - 1)
                  .Select((p, i) => (points[i + 1].Lat - p.Lat) * (points[i + 1].Lng + p.Lng))
                  .Sum() / 2);
                double convArea = 0;
                switch (cmbAreaUnit.Text)
                {
                    case "m^2":
                        txtArea.Text = area.ToString("0.000");
                        break;
                    case "ha":
                        convArea = area / (100 * 100);
                        txtArea.Text = convArea.ToString("0.000");
                        break;
                    case "km^2":
                        convArea = area / (1000 * 1000);
                        txtArea.Text = convArea.ToString("0.000");
                        break;
                    case "ft^2":
                        convArea = area * 10.7639f;
                        txtArea.Text = convArea.ToString("0.000");
                        break;
                    case "acres":
                        area = area / 43560f;
                        txtArea.Text = convArea.ToString("0.000");
                        break;
                    case "miles^2":
                        convArea = area / 640f;
                        txtArea.Text = convArea.ToString("0.000");
                        break;

                }

            }
        }

        private void btnCalcArea_Click(object sender, EventArgs e)
        {
            // calculateArea();
        }

        private void drawPath(PolyBoundry boundry)   // IMPORTANT ROUTINE !!!!! ***************************
        {
            List<PointLatLng> WPoints = new List<PointLatLng>();
            List<PointLatLng> WPturn = new List<PointLatLng>();
            int WPcounter = 0;  //Waipoit counter
            // MessageBox.Show("in drawPath!");
            double A1 = 0, A2 = 0, A3 = 0, A4 = 0, B1 = 0, B2 = 0, B3 = 0, B4 = 0;
            // double nextDistanceX = 0.001, nextDistanceY = 0.0003;
            double dX, dY;
            dX = Convert.ToDouble(txtXstep.Text); //LONG
            dY = Convert.ToDouble(txtYstep.Text);  //LAT

            String pathDir = pathDirection(markersStart, 0); // Wht is the angle betw. form Start to closest marker ?
            //pathDir = "U2";
            // int angle;

            // A1 = nextDistanceX; A2 = 0; B1 = 0; B2 = -nextDistanceY;
            switch (pathDir)
            {
                case "R1":  //Left upper corner, got Right & Down
                    A1 = dX; A2 = 0; A3 = -dX; A4 = 0; B1 = 0; B2 = -dY; B3 = 0; B4 = -dY;
                    break;
                case "L1": //Right upper corner, got Left & Down
                    A1 = -dX; A2 = 0; A3 = dX; A4 = 0; B1 = 0; B2 = -dY; B3 = 0; B4 = -dY;
                    break;
                case "D1": //Right upper corner, got Down & Right
                    A1 = 0; A2 = dX; A3 = 0; A4 = dX; B1 = -dY; B2 = 0; B3 = dY; B4 = 0;
                    break;
                case "D2": //Right upper corner, got Down & Right
                    A1 = 0; A2 = -dX; A3 = 0; A4 = -dX; B1 = -dY; B2 = 0; B3 = dY; B4 = 0;
                    break;
                case "U1": //Right upper corner, got Down & Right
                    A1 = 0; A2 = dX; A3 = 0; A4 = dX; B1 = dY; B2 = 0; B3 = -dY; B4 = 0;
                    break;
                case "U2": //Right upper corner, got Down & Right
                    A1 = 0; A2 = -dX; A3 = 0; A4 = -dX; B1 = dY; B2 = 0; B3 = -dY; B4 = 0;
                    break;
                case "R2": //Right upper corner, got Down & Right
                    A1 = dX; A2 = 0; A3 = -dX; A4 = 0; B1 = 0; B2 = dY; B3 = 0; B4 = dY;
                    break;
                case "L2": //Right upper corner, got Down & Right
                    A1 = -dX; A2 = 0; A3 = dX; A4 = 0; B1 = 0; B2 = dY; B3 = 0; B4 = dY;
                    break;
                default:
                    Console.WriteLine("Nothing");
                    break;
            }

            if (markers.Markers.Count > 2)  // GLOBAL variable for polygon's markers
            {
                bool t = pointInside(markersStart, 0);  // is the starting point inside the polygon ?
                if (t)
                {

                    txtX.Text = "@@@ INSIDE @@@";  // <- full FOR cycle coming here !!!
                    var start = markersStart.Markers.ElementAt(0).Position;  //this is the pos/ clcik of the red starter mark
                    WPoints.Add(new PointLatLng(start.Lat, start.Lng));
                    WPcounter++;  // not required
                    //************** ROUTINE **********************
                    //**********************************************************************
                    bool abort = false;
                    do
                    {
                        // *********************GO RIGHT *************************************************
                        int s = 0; // next point counter
                        //if inside: take a new point right from the Starting point
                        while (t)  // run until unless next point is inside
                        {
                            var markerNext = new GMarkerGoogle(new PointLatLng(start.Lat + B1 * s, start.Lng + A1 * s), GMarkerGoogleType.yellow_small)
                            {
                                ToolTipText = (markersNext.Markers.Count - 1).ToString(),
                                ToolTipMode = MarkerTooltipMode.OnMouseOver
                            };

                            map.Overlays.Add(markersNext); markersNext.Markers.Add(markerNext); // Add to the layer. How delete ???
                            t = pointInside(markersNext, markersNext.Markers.Count - 1);  // Is marker inside the polygon ? s : lastMarkerNr in the array
                            s++;
                        }  // end while
                        //  MessageBox.Show("B1: " + B1 + "A1: " + A1);

                        markersNext.Markers.RemoveAt(markersNext.Markers.Count - 1);  // this is the marker which is out of polygon
                        //markersNext.Markers.Count run to 0 !
                        var nextWP = markersNext.Markers.ElementAt(markersNext.Markers.Count - 1).Position;  // Take the last WP which is in the poligon/area
                        WPoints.Add(new PointLatLng(nextWP.Lat, nextWP.Lng));  // add the WP in the row to the ROUTE

                        WPcounter++;  // not required
                        //   MessageBox.Show("B2: " + B2 + "A2: " + A2);

                        // *********************GO DOWN *************************************************
                        start.Lat = nextWP.Lat + B2; // go one step down
                        start.Lng = nextWP.Lng + A2; // Save the turning maprker for inside check  t = pointInside( ...
                        var markerNextT = new GMarkerGoogle(new PointLatLng(start.Lat, start.Lng), GMarkerGoogleType.yellow_small);
                        map.Overlays.Add(markersNextT); markersNextT.Markers.Add(markerNextT); //check is the turnign point out or not if yes, ABORT loop
                        /**/
                        t = pointInside(markersNextT, markersNextT.Markers.Count - 1);
                        //if (start.Lat > boundry.Ymin)  // durign this step the point might be out of polygon
                        //   MessageBox.Show("Will Go Back");
                        if (t)
                        {                              // check with  t = pointInside( ...) !!!! only
                            WPoints.Add(new PointLatLng(start.Lat, start.Lng));
                            WPcounter++;  // not required
                        }
                        else
                        {
                            abort = true;  //out of boundry, but markerNextY/start.Lat coord not reached boundry.Ymin !!!
                            //abort loop !
                        }

                        // *********************GO BACK *************************************************
                        if (!abort)
                        {
                            t = true; s = 0; // very important
                            while (t)  // run until unless next point is inside
                            {
                                var markerNext = new GMarkerGoogle(new PointLatLng(start.Lat + B3 * s, start.Lng + A3 * s), GMarkerGoogleType.yellow_small)
                                {
                                    ToolTipText = (markersNext.Markers.Count - 1).ToString(),
                                    ToolTipMode = MarkerTooltipMode.OnMouseOver
                                };
                                map.Overlays.Add(markersNext); markersNext.Markers.Add(markerNext);
                                t = pointInside(markersNext, markersNext.Markers.Count - 1);  // s : lastMarkerNr in the array
                                s++;
                            }  // end while
                            markersNext.Markers.RemoveAt(markersNext.Markers.Count - 1);  // this is the marker which is out of polygon
                            nextWP = markersNext.Markers.ElementAt(markersNext.Markers.Count - 1).Position;  // Take the last WP which is in the poligon/area
                            WPoints.Add(new PointLatLng(nextWP.Lat, nextWP.Lng));
                            //    MessageBox.Show("B1: " + B1 + " A1: " + A1);

                            // *********************GO DOWN **********************************************
                            start.Lat = nextWP.Lat + B4; // go one step down Y
                            start.Lng = nextWP.Lng + A4;  //X
                            markerNextT = new GMarkerGoogle(new PointLatLng(start.Lat, start.Lng), GMarkerGoogleType.yellow_small);
                            map.Overlays.Add(markersNextT); markersNextT.Markers.Add(markerNextT); //check is the turnign point out or not if yes, ABORT loop
                            //    MessageBox.Show("Go Down " + B2 + " " + A2);
                            /**/
                            t = pointInside(markersNextT, markersNextT.Markers.Count - 1);
                            if (t)  //if one step down WP in the polygon
                            //  if (start.Lat > boundry.Ymin)   // durign this step the point might be out of polygon
                            {                               // check with  t = pointInside( ...)  !!!! only
                                WPoints.Add(new PointLatLng(start.Lat, start.Lng));
                                WPcounter++;  // not required
                            }
                            else
                            {
                                abort = true;
                                //abort loop !
                            }
                        }
                        t = true; s = 0;  //very important !

                        if (start.Lat < boundry.Ymin)
                        {
                            txtOutput.Text = "rout out of boundry"; //stop FOR
                        }

                    } while ((start.Lat > boundry.Ymin) && !abort);  // end FOR

                    GMapRoute route = new GMapRoute(WPoints, "A walk in the park");
                    route.Stroke = new Pen(Color.Blue, 3); routes.Routes.Add(route);
                }
                else
                    txtX.Text = "outside";  // Exit !!!
            } // end if markers.Markers.Count > 2
            loadWPtoTable(WPoints);  // 12 -> 24 !!
        }

        //int pnpoly(int nvert, float vertx, float verty, float testx, float testy)
        private bool pointInside(GMapOverlay test, int pos)  // which makter nuber to be tested
        {
            int i, j = 0;
            Boolean c = false;
            int[] vertX = new int[1000];
            int[] vertY = new int[1000]; ;  // Cartesian coordinates
            int testX = 0;
            int testY = 0;

            for (i = 0; i < markers.Markers.Count; i++) //convert geofence markers map coord to Cartesian
            {
                var p = map.FromLatLngToLocal(new PointLatLng(markers.Markers.ElementAt(i).Position.Lat, markers.Markers.ElementAt(i).Position.Lng));
                vertX[i] = (int)p.X;   // convert map coord to display coord
                vertY[i] = (int)p.Y;
                txtX.Text = vertX[i].ToString();
                txtY.Text = vertY[i].ToString();
            }

            if (test.Markers.Count == 0)
            {
                return false;
            }
            //Bug Erro coming if red start marker not pressed, but rout planning clikced !!!
            //The Start point:
            var pp = map.FromLatLngToLocal(new PointLatLng(test.Markers.ElementAt(pos).Position.Lat, test.Markers.ElementAt(pos).Position.Lng));

            testX = (int)pp.X;
            testY = (int)pp.Y;
            txtX.Text = testX.ToString();
            txtY.Text = testY.ToString();

            for (i = 0, j = markers.Markers.Count - 1; i < markers.Markers.Count; j = i++)
            {
                if (((vertY[i] > testY) != (vertY[j] > testY)) &&
                 (testX < (vertX[j] - vertX[i]) * (testY - vertY[i]) / (vertY[j] - vertY[i]) + vertX[i]))
                {
                    c = !c;
                }
            }

            return c;
            //  return true;
        }

        // ---------------------------------------

        private String pathDirection(GMapOverlay test, int pos)  // which makter is the closest ot the Stating/test point
        {                                                      // required to distane & autmatic pathDrawing
            int i, j = 0;
            Boolean c = false;
            int[] vertX = new int[1000];
            int[] vertY = new int[1000]; ;  // Cartesian coordinates
            int testX = 0;  // to store the Start poitn X ccord from the map Coords
            int testY = 0;
            double[] dist = new double[1000];
            int closestMarker = -1;
            double closestMarkerAngle = -1;
            int closestDistance = -1;

            if (test.Markers.Count == 0)
            {
                return "no";
            }

            //Bug Erro coming if red start marker not pressed, but rout planning clikced !!!
            //The Start point:
            var pp = map.FromLatLngToLocal(new PointLatLng(test.Markers.ElementAt(pos).Position.Lat, test.Markers.ElementAt(pos).Position.Lng));

            testX = (int)pp.X;
            testY = (int)pp.Y;
            txtX.Text = testX.ToString();
            txtY.Text = testY.ToString();

            for (i = 0; i < markers.Markers.Count; i++) //convert geofence markers map coord to Cartesian
            {
                var p = map.FromLatLngToLocal(new PointLatLng(markers.Markers.ElementAt(i).Position.Lat, markers.Markers.ElementAt(i).Position.Lng));
                vertX[i] = (int)p.X;   // convert map coord to display coord
                vertY[i] = (int)p.Y;
                // txtX.Text = vertX[i].ToString();
                // txtY.Text = vertY[i].ToString();
                double distX = Math.Abs(vertX[i] - testX);
                double distY = Math.Abs(vertY[i] - testY);
                dist[i] = Math.Sqrt(distX * distX + distY * distY);  //Store distance from Star to to Marker (root quare not requried)
            }

            double minDist = 99999;
            for (i = 0; i < markers.Markers.Count; i++)  // find out the index of the closest marker to the Start point
            {
                if (dist[i] < minDist)
                {
                    minDist = dist[i];
                    closestMarker = i;
                }

            }

            closestDistance = (int)dist[closestMarker];  //==minDist
            closestMarkerAngle = Math.Atan2(testY - vertY[closestMarker], vertX[closestMarker] - testX);
            closestMarkerAngle = closestMarkerAngle * 180 / Math.PI;

            string direction = "left/right/up/down ?";
            //  if (mymap.ShowInputDialog(ref direction) == System.Windows.Forms.DialogResult.Cancel)  // Access to myMap.cs CLASS !!!!
            //     return "no";

            FormDirections child = new FormDirections();  // Kind of inputdialog
            //    child.direction = "Hello";
            child.ShowDialog();
            //  MessageBox.Show("Got:" + child.direction);
            direction = child.direction;

            String pathDirection = "";

            if (0 < closestMarkerAngle && closestMarkerAngle < 90 && direction == "down")
                pathDirection = "D2";
            if (0 < closestMarkerAngle && closestMarkerAngle < 90 && direction == "left")
                pathDirection = "L1";
            if (90 < closestMarkerAngle && closestMarkerAngle < 180 && direction == "right")
                pathDirection = "R1";
            if (90 < closestMarkerAngle && closestMarkerAngle < 180 && direction == "down")
                pathDirection = "D1";
            if (-180 < closestMarkerAngle && closestMarkerAngle < -90 && direction == "up")
                pathDirection = "U1";
            if (-180 < closestMarkerAngle && closestMarkerAngle < -90 && direction == "right")
                pathDirection = "R2";
            if (-90 < closestMarkerAngle && closestMarkerAngle < 0 && direction == "left")
                pathDirection = "L2";
            if (-90 < closestMarkerAngle && closestMarkerAngle < 0 && direction == "up")
                pathDirection = "U2";

            //MessageBox.Show("Angle: " + (int)closestMarkerAngle);

            return pathDirection; ;
            //  return true;
        }


        private void cbMapTypes_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbMapTypes.Text == "Google Maps Satélite")
                map.MapProvider = GMapProviders.GoogleSatelliteMap;
            if (cmbMapTypes.Text == "Google Maps Callejero")
                map.MapProvider = GMapProviders.GoogleMap;
            if (cmbMapTypes.Text == "Google Maps Híbrido")
                map.MapProvider = GMapProviders.GoogleHybridMap;
            if (cmbMapTypes.Text == "OpenStreetMap")
                map.MapProvider = GMapProviders.OpenStreetMap;
            if (cmbMapTypes.Text == "OpenCycleMap")
                map.MapProvider = GMapProviders.OpenCycleMap;
            if (cmbMapTypes.Text == "Bing")
                map.MapProvider = GMap.NET.MapProviders.BingHybridMapProvider.Instance;
            //map.MapProvider = GMapProviders.BingHybridMapProvider;
            txtX.Focus();
            map.Refresh();
        }


        // ================================================================
        private void btnRefresh_Click(object sender, EventArgs e)
        {
            _ports = SerialPort.GetPortNames();
            comPorts.DataSource = _ports;
        }

        private void btnConnect_Click(object sender, EventArgs e)
        {
            bool error = false;

            //https://www.c-sharpcorner.com/uploadfile/eclipsed4utoo/communicating-with-serial-port-in-C-Sharp/
            if (Open == false)
            {
                string port = this.comPorts.GetItemText(this.comPorts.SelectedItem);
                // _serialPort = new SerialPort(port, 57600, Parity.None, 8, StopBits.One);
            //    serialPort1.BaudRate = 9600;
            //    serialPort1.DataBits = 8;
            //    serialPort1.StopBits = (StopBits)Enum.Parse(typeof(StopBits), "1");
          //      serialPort1.Parity = (Parity)Enum.Parse(typeof(Parity), "None");
                serialPort1.PortName = port;

                try
				{
					serialPort1.Open();
				}
                catch (UnauthorizedAccessException) { error = true; }
                catch (IOException) { error = true; }
                catch (ArgumentException) { error = true; }

                if (error) MessageBox.Show(this, "Could not open the COM port.  Most likely it is already in use, has been removed, or is unavailable.", "COM Port Unavalible", MessageBoxButtons.OK, MessageBoxIcon.Stop);
				
              //  serialPort1.Open();
                timerRover.Enabled = true;
                //this.btnConnect.Image = Map_v1.Properties.Resources.Stop_pic.jpeg;
                btnConnect.BackgroundImage = Image.FromFile("C:\\00_Robot\\03_Visual Studio\\Map_v0\\Map_v1\\Properties\\img\\connect_.png");
                // btnClosePort.ImageAlign = ContentAlignment.MiddleRight;
                // btnClosePort.TextAlign = ContentAlignment.MiddleLeft;
                // btnClosePort.FlatStyle = FlatStyle.Flat;
                Open = true;
            }
            else
            {
                btnConnect.BackgroundImage = Image.FromFile("C:\\00_Robot\\03_Visual Studio\\Map_v0\\Map_v1\\Properties\\img\\dis-connect.png");
                serialPort1.Close();
                serialPort1.Dispose();
                Open = false;
                timerRover.Enabled = false;
            }
        }


        // https://nugrohowidijatmiko.blogspot.com/2018/06/tutorial-c-rotate-gmapnet-marker-from.html
        // https://stackoverflow.com/questions/40120376/how-to-rotate-gmap-net-marker-in-c-sharp
        private void GPSheading()
        {
            routesoverlay.Markers.Clear();

            // Get the most up-to-date data received from the sensor.
            /*      var curMeas = vn200.CurrentMeasurements;  // the heading data from IMU sensor V200 from Vectorav.

                  PointLatLng point = new PointLatLng((float)curMeas.LatitudeLongitudeAltitude.X,
                                                      (float)curMeas.LatitudeLongitudeAltitude.Y);
                  map.Position = point;

                  var plane = new GMapMarkerPlane(point, (float)curMeas.YawPitchRoll.YawInDegs);
                  routesoverlay.Markers.Add(plane);
             */
        }

        int clearMapCounter = 0;
        private void btnClarMap_Click(object sender, EventArgs e)
        {
            clearMapCounter++;
            ClearMap(clearMapCounter);
            txtArea.Text = "";
            cmbAreaUnit.SelectedItem = 1;  //ha
        }

        private void ClearMap(int counter)
        {
            // first click delete this to be able to modify the corner markes of the polygon:
            if (counter % 2 == 1) {
            polygons.Polygons.Clear(); // polygon
            markersNext.Markers.Clear();  // continouse path
            routes.Routes.Clear();          // rout generated based on markersNext
            markersNextT.Markers.Clear();  //turning point marker
            markers.Markers.Clear();  // robot position
                }
            /*
                        markers = null;
                        polygons = null;
                        markersNext = null;
                        routes = null;
                        markersNextT = null;
                        markersStart = null;
                        */
            else {
            markers.Markers.Clear();   //markers for polygon
            markersStart.Markers.Clear();
            //  chkStart.Enabled = true;  //btnStartPoint.FatStyle
            btnStartPoint.FlatStyle = FlatStyle.Standard;
            }

        }

        private void btnLoadTable_Click(object sender, EventArgs e)
        {
            try
            {
                connetion.Open();
                OleDbCommand command = new OleDbCommand();
                command.Connection = connetion;
                OleDbDataAdapter da = new OleDbDataAdapter(command);
                DataTable dt = new DataTable();
                string querry;

                /*      querry = "DELETE FROM waypoints";
                    command.CommandText = querry;
                    da = new OleDbDataAdapter(command);
                    dt = new DataTable();
                    da.Fill(dt);

                            int A, B, C;     A = 1; B = 1; C = 1; //Add points in the Database:
                              querry = "INSERT INTO waypoints(SNr, Lat, Lng) VALUES(" + A + "," + B + "," + C + ")";
                              command.CommandText = querry;
                              da = new OleDbDataAdapter(command);
                              dt = new DataTable();
                              da.Fill(dt);
                              */
                querry = "select * from waypoints";  //Load database to Gridview
                command.CommandText = querry;
                da.Fill(dt);
                dataGridViewPlan.DataSource = dt;  //datatable

                lblCheckConneton.Text = "Connected";
                connetion.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error " + ex);
            }
        }

        private void loadWPtoTable(List<PointLatLng> test)
        {
            try
            {
                connetion.Open();
                OleDbCommand command = new OleDbCommand();
                command.Connection = connetion;
                OleDbDataAdapter da = new OleDbDataAdapter(command);
                DataTable dt = new DataTable();
                string querry;

                querry = "DELETE FROM tempWaypoints";
                command.CommandText = querry;
                da = new OleDbDataAdapter(command);
                dt = new DataTable();
                da.Fill(dt);

                int A, B, C;

                for (int i = 0; i < test.Count; i++)
                {
                    // dt.Rows.Add(i + 1, test.ElementAt(i).Lat, test.ElementAt(i).Lng);
                    A = i + 1;
                    querry = "INSERT INTO tempWaypoints(SNr, Lat, Lng) VALUES(" + A + "," + test.ElementAt(i).Lat + "," + test.ElementAt(i).Lng + ")";
                    command.CommandText = querry;
                    da = new OleDbDataAdapter(command);
                    dt = new DataTable();
                    da.Fill(dt);
                }

                querry = "select * from tempWaypoints";  //Load database to Gridview
                command.CommandText = querry;
                da.Fill(dt);
                dataGridViewPlan.DataSource = dt;  //datatable

                lblCheckConneton.Text = "Connected";
                connetion.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error " + ex);
            }
        }

        private void btnSaveTable_Click(object sender, EventArgs e)
        {
            connetion.Open();
            OleDbCommand command = new OleDbCommand();
            command.Connection = connetion;
            OleDbDataAdapter da = new OleDbDataAdapter(command);
            DataTable dt = new DataTable();
            string querry;

            querry = "INSERT INTO waypoints SELECT * FROM tempWaypoints";
            command.CommandText = querry;
            da = new OleDbDataAdapter(command);
            dt = new DataTable();
            da.Fill(dt);
            connetion.Close();
            MessageBox.Show("Done", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void btnSaveTableTo_Click(object sender, EventArgs e)
        {
            string filename = "?";
            if (mymap.ShowInputDialog(ref filename) == System.Windows.Forms.DialogResult.Cancel)  // Access to myMap.cs CLASS !!!!
                return;
            connetion.Open();
            OleDbCommand command = new OleDbCommand();
            command.Connection = connetion;
            OleDbDataAdapter da = new OleDbDataAdapter(command);
            DataTable dt = new DataTable();
            string querry;

            querry = "CREATE TABLE " + filename + " (Snr VARCHAR(64), Lat VARCHAR(64), Lng VARCHAR(64))";
            command.CommandText = querry;
            da = new OleDbDataAdapter(command);
            dt = new DataTable();
            da.Fill(dt);

            querry = "INSERT INTO " + filename + " SELECT * FROM tempWaypoints";
            command.CommandText = querry;
            da = new OleDbDataAdapter(command);
            dt = new DataTable();
            da.Fill(dt);

            connetion.Close();
            MessageBox.Show("Done", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }



        private void btnLoadRoute_Click(object sender, EventArgs e)
        {
            List<string> tables = new List<string>();
            //  DataTable table = new DataTable();  == dt
            if (routes.Routes.Count > 0)  // to be improve to remove all !!
            {
                routes.Routes.Clear();
            }
            //  if (markersNext.Markers.Count > 0)
            //  {
            // markersNext.Markers.RemoveAt(markersNext.Markers.Count - 1);
            markers.Markers.Clear();
            markersNext.Markers.Clear();
            markersStart.Markers.Clear();
            markersNextT.Markers.Clear();
            markersWP.Markers.Clear();
            markersPath.Markers.Clear();
            polygons.Polygons.Clear();

            // }
            connetion.Open();
            OleDbCommand command = new OleDbCommand();
            command.Connection = connetion;
            OleDbDataAdapter da = new OleDbDataAdapter(command);
            DataTable dt = new DataTable();
            string querry;

            dt.Columns.Add("Snr", typeof(int));
            dt.Columns.Add("Lat", typeof(string));
            dt.Columns.Add("Lng", typeof(string));
            dataGridView2.DataSource = dt;   //Show data from MS Access in C# gridwiev

            var datasetName = cmbRoutes.Text;
            querry = "select * from " + datasetName;  //Load database to Gridview
            command.CommandText = querry;
            da.Fill(dt);
            dataGridView2.DataSource = dt;  //datatable
            connetion.Close();

            List<PointLatLng> r_points = new List<PointLatLng>();

            int c = dt.Rows.Count;
            int i;
            // String lat, lng;
            //https://stackoverflow.com/questions/13816490/get-cell-value-from-a-datatable-in-c-sharp
            for (i = 0; i < c; i++)
            {
                //  Srn = (int) dt.Rows[i].Field<double>("Snr");
                int Srn = dt.Rows[i].Field<int>(0);
                String lat = dt.Rows[i].Field<string>(1);
                String lng = dt.Rows[i].Field<string>(2);
                //  lat = dt.Rows[i].Field<double>("Lat");
                //   lng = dt.Rows[i].Field<double>("Lng");
                r_points.Add(new PointLatLng(Convert.ToDouble(lat), Convert.ToDouble(lng)));
            }

            GMapRoute loadedRoute = new GMapRoute(r_points, "A walk in the park");
            loadedRoute.Stroke = new Pen(Color.Red, 3);
            routes.Routes.Add(loadedRoute);

            String latS = dt.Rows[0].Field<string>(1);  // yellow arrow coords, thus shall come from the rover's GPS !!!
            String lngS = dt.Rows[0].Field<string>(2);
            //*   r_points.Add(new PointLatLng(Convert.ToDouble(latS), Convert.ToDouble(lngS)));

            //*    Bitmap bmpMarker = (Bitmap)Image.FromFile("heading_arrow2.png");  //(Bitmap) tractor.png heading_arrow.png
            //Random r = new Random();
            //float angle = (float)(360 * r.NextDouble());
            //*       float angle = 90; // this shoul come from the compass !!!
            //*      Bitmap iconNavIcon = RotateImage(bmpMarker, angle);
            // PointLatLng position = new PointLatLng(Convert.ToDouble(latS), Convert.ToDouble(lngS));
            //*     var markerStart = new GMarkerGoogle(new PointLatLng(Convert.ToDouble(latS), Convert.ToDouble(lngS)), iconNavIcon); //
            // markerStart.Offset = new Point(- bmpMarker.Width / 2, - bmpMarker.Height);
            //*     markerStart.Offset = new Point(-20,-30);
            //*     map.Overlays.Add(markersStart); markersStart.Markers.Add(markerStart);
            //Change marker postion requreid for GPS feed!!!:  markerStart.Markers[0].Position = new PointLatLng(30.0000, 30.00000);

            //GMarkerArrow marker1 = new GMarkerArrow(new PointLatLng(-30, -40));  // it isi in GMarkerArrow.cs class
/*            GMarkerArrow marker1 = new GMarkerArrow(new PointLatLng(Convert.ToDouble(latS), Convert.ToDouble(lngS)));  // it isi in GMarkerArrow.cs class
            marker1.ToolTipText = "Rover position";
            marker1.ToolTip.Fill = Brushes.Black;
            marker1.ToolTip.Foreground = Brushes.White;
            marker1.ToolTip.Stroke = Pens.Black;
            marker1.Bearing = 0; // this should come from the compass/robot IMU !!!
            marker1.Fill = new SolidBrush(Color.FromArgb(155, Color.Yellow)); // Arrow color
            markers.Markers.Add(marker1); map.Overlays.Add(markers);  //map is the name of the map on the form !
  */          //  marker1.Bearing = 120; // this shoul come from the compass !!!
            //  markers.Markers.Add(marker1); map.Overlays.Add(markers);  //map is the name of the map on the form !

        }

        public static Bitmap RotateImage(Bitmap b, float angle)
        {
            //create a new empty bitmap to hold rotated image
            Bitmap returnBitmap = new Bitmap(b.Width, b.Height);
            //make a graphics object from the empty bitmap
            using (Graphics g = Graphics.FromImage(returnBitmap))
            {
                //move rotation point to center of image
                g.TranslateTransform((float)b.Width / 2, (float)b.Height / 2);
                //rotate
                g.RotateTransform(angle);
                //move image back
                g.TranslateTransform(-(float)b.Width / 2, -(float)b.Height / 2);
                //draw passed in image onto graphics object
                g.DrawImage(b, new Point(0, 0));
            }
            return returnBitmap;
        }

        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            connetion.Open();  //Update the list of the tables from the MS Acess Databese 
            OleDbCommand command = new OleDbCommand();
            command.Connection = connetion;
            OleDbDataAdapter da = new OleDbDataAdapter(command);
            DataTable dt = new DataTable();
            //   string querry;

            cmbRoutes.Items.Clear();
            foreach (DataRow r in connetion.GetSchema("Tables").Select("TABLE_TYPE = 'TABLE'"))
                // tables.Add(r["TABLE_NAME"].ToString());
                cmbRoutes.Items.Add(r["TABLE_NAME"].ToString());

            cmbRoutes.SelectedIndex = 0;

            connetion.Close();
        }

        private void dataGridView2_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (markersNext.Markers.Count > 0)
            {
                //markers.Markers.OrderByDescending(1);
                markersNext.Markers.RemoveAt(markersNext.Markers.Count - 1); //Other case multyple vesions on map
                //    var x = markers.Markers.
            }

            // MessageBox.Show("Mouse clicked in the datagridview!");
            // if (dataGridView2.CurrentCell.ColumnIndex.Equals(1) && e.RowIndex != -1)
            if (e.RowIndex != -1)
            {
                if (dataGridView2.CurrentCell != null && dataGridView2.CurrentCell.Value != System.DBNull.Value)
                {
                    var r = dataGridView2.CurrentCell.RowIndex;
                    String lat = (string)dataGridView2.Rows[r].Cells[1].Value;
                    String lng = (string)dataGridView2.Rows[r].Cells[2].Value;
                    // MessageBox.Show(lat.ToString()); MessageBox.Show(lng.ToString());
                    var markerNext = new GMarkerGoogle(new PointLatLng(Convert.ToDouble(lat), Convert.ToDouble(lng)), GMarkerGoogleType.blue)
                    {
                        ToolTipText = "Next point",
                        ToolTipMode = MarkerTooltipMode.OnMouseOver
                    };
                    map.Overlays.Add(markersNext); markersNext.Markers.Add(markerNext);
                    //  markersNext.Markers.RemoveAt(markersNext.Markers.Count - 1);  // this is the marker which is out of polygon
                }
            }
        }

        private void timerRover_Tick(object sender, EventArgs e)
        {
            String incData; // date from rover
            //String RecievedData;
            //RecievedData = serialPort1.ReadExisting();

            if (serialPort1.BytesToRead == 0)
            {
             //   MessageBox.Show("No bytes");
                return;
            }
            else  //   if (!(RecievedData == ""))
            {
                incData = serialPort1.ReadLine();
                lbRoverMsg.Items.Add(incData);  // add message to list box
                if (incData.Substring(0,3) == "LAT")
                {
                    txtRoverRemLat.Text = incData.Substring(3, incData.Length-3); //Rover remot lat
                }
                if (incData.Substring(0, 3) == "LNG")
                {
                    txtRoverRemLng.Text = incData.Substring(3, incData.Length - 3); //Rover remot lat
                }
                if (incData.Substring(0, 3) == "HED")
                {
                    txtRoverRemHead.Text = incData.Substring(3, incData.Length - 3); //Rover remot lat
                }

                // Console.WriteLine(incData.StartsWith("S")); //Check wheter first character of string is same as specified value

    /*          string[] NMEAfields = incData.Split(new[] { ',', '*' }, StringSplitOptions.RemoveEmptyEntries);
                String[] NMEAdecoded = new String[100];
                //https://www.trimble.com/OEM_ReceiverHelp/V4.44/en/NMEA-0183messages_MessageOverview.html
                switch (NMEAfields[0].Substring(4, 3))
                {
                    case "GGA":  //$GPGGA,201702.000,4730.7845,N,01906.9620,E,1,09,0.9,150.9,M,0.0,M,,0000*6A
                        {
                            //UTC time:
                            NMEAdecoded[1] = NMEAfields[1].Substring(1, 2) + "h" + NMEAfields[1].Substring(3, 2) + "m" + NMEAfields[1].Substring(5, 2) + "s";
                            //Latitude & :Longitude
                            NMEAdecoded[2] = NMEAfields[2].Substring(1, 2) + "° " + NMEAfields[2].Substring(3, 7) + "'";
                            NMEAdecoded[3] = NMEAfields[4].Substring(1, 3) + "° " + NMEAfields[4].Substring(4, 7) + "'";
                            txtRoverRemLat.Text = NMEAfields[2];
                            txtRoverRemLng.Text = NMEAfields[3];
                            txtRoverRemHead.Text = "heading"; // ??? which field ???
                            NMEAdecoded[4] = NMEAfields[6];  //Position Fix Indicator !!! important !
                            //0: Fix not valid 1: GPS fix 2: Differential GPS fix, OmniSTAR VBS 4: Real-Time Kinematic, fixed integers 5: Real-Time Kinematic, float integers, OmniSTAR XP/HP or Location RTK
                            NMEAdecoded[5] = NMEAfields[7];  //Number of SVs in use, range from 00 through to 24+
                            NMEAdecoded[6] = NMEAfields[8];  //HDOP Horizontal Dilution of Precision
                            NMEAfields[7] = NMEAfields[9];  //MSL Altitude 	Orthometric height (MSL reference)
                            NMEAfields[8] = NMEAfields[10];  //M: unit of measure for orthometric height is meters
                            NMEAfields[9] = NMEAfields[11];  //Geoid Separation
                            NMEAfields[10] = NMEAfields[12]; //M: geoid separation measured in meters
                            NMEAfields[11] = NMEAfields[13]; //Age of differential GPS data record, Type 1 or Type 9. Null field when DGPS is not used.
                            NMEAfields[12] = NMEAfields[14]; //Reference station ID, range 0000-4095. A null field when any reference station ID is selected and no corrections are received
                            NMEAfields[13] = NMEAfields[15]; //	The checksum data, always begins with *
                            break;
                        }
                    case "GSA": // GSA—GNSS DOP and Active Satellites
                        {
                            NMEAfields[20] = NMEAfields[3]; NMEAfields[21] = NMEAfields[4]; //SV on Channel 1-2
                            NMEAfields[22] = NMEAfields[5]; NMEAfields[23] = NMEAfields[6]; //SV on Channel 3-4
                            NMEAfields[24] = NMEAfields[7]; NMEAfields[25] = NMEAfields[8]; //SV on Channel 3-4
                            NMEAfields[26] = NMEAfields[9]; NMEAfields[27] = NMEAfields[10]; //SV on Channel 3-4
                            NMEAfields[28] = NMEAfields[11]; NMEAfields[29] = NMEAfields[12]; //SV on Channel 3-4
                            NMEAfields[30] = NMEAfields[12]; NMEAfields[31] = NMEAfields[14]; //SV on Channel 3-4
                            NMEAfields[32] = NMEAfields[15];   //Position Dilution of Precision
                            NMEAfields[33] = NMEAfields[16];    //'Horizontal Dilution of Precision
                            NMEAfields[34] = NMEAfields[17];    // 'Vertical Dilution of Precision
                            break;
                        }
                    case "GSV":   // GSV—GNSS Satellites in View  $GPGSV,3,1,11,  26,69,307,18,  28,65,135,29,  08,56,056,28,  05,44,229,38  *7D
                        {
                            //6	Azimuth, degrees from True North, 000° through 359°
                            break;
                        }
                    case "HDT":  //$GPHDT,123.456,T*00
                        {
                            // NMEAfields:
                         //   0	Message ID $GPHDT
                         //   1	Heading in degrees
                         //   2	T: Indicates heading relative to True North
                         //   3	The checksum data, always begins with  
                           
                            break;
                        }
                }  // end switch
        */

            }  //end else
        }  //end tick

        private void dataGridViewPlan_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (markersPath.Markers.Count > 0)
            {
                markersPath.Markers.RemoveAt(markersPath.Markers.Count - 1); //Other case multyple vesions on map
            }
            // MessageBox.Show("Mouse clicked in the datagridview!");
            // if (dataGridView2.CurrentCell.ColumnIndex.Equals(1) && e.RowIndex != -1)
            if (e.RowIndex != -1)
            {
                if (dataGridViewPlan.CurrentCell != null && dataGridViewPlan.CurrentCell.Value != System.DBNull.Value)
                {
                    var r = dataGridViewPlan.CurrentCell.RowIndex;
                    String lat = (string)dataGridViewPlan.Rows[r].Cells[1].Value;
                    String lng = (string)dataGridViewPlan.Rows[r].Cells[2].Value;
                    // MessageBox.Show(lat.ToString()); MessageBox.Show(lng.ToString());
                    var markerPath = new GMarkerGoogle(new PointLatLng(Convert.ToDouble(lat), Convert.ToDouble(lng)), GMarkerGoogleType.green)
                    {
                        ToolTipText = "Next point",
                        ToolTipMode = MarkerTooltipMode.OnMouseOver
                    };
                    map.Overlays.Add(markersPath); markersPath.Markers.Add(markerPath);
                    //  markersNext.Markers.RemoveAt(markersNext.Markers.Count - 1);  // this is the marker which is out of polygon
                }
            }
        }

        private void cmbAreaUnit_SelectedIndexChanged(object sender, EventArgs e)
        {
            double convArea = 0;
            switch (cmbAreaUnit.Text)
            {
                case "m^2":
                    txtArea.Text = area.ToString("0.000");
                    break;
                case "ha":
                    convArea = area / (100 * 100);
                    txtArea.Text = convArea.ToString("0.000");
                    break;
                case "km^2":
                    convArea = area / (1000 * 1000);
                    txtArea.Text = convArea.ToString("0.000");
                    break;
                case "ft^2":
                    convArea = area * 10.7639f;
                    txtArea.Text = convArea.ToString("0.000");
                    break;
                case "acres":
                    convArea = area / 43560f;
                    txtArea.Text = convArea.ToString("0.000");
                    break;
                case "miles^2":
                    convArea = area / 640f;
                    txtArea.Text = convArea.ToString("0.000");
                    break;
            }
        }

        private void btnPoligon_Click(object sender, EventArgs e)
        {
            //  string direction = "left/right/up/down ?";
            FormDirections child = new FormDirections();
            //    child.direction = "Hello";
            child.ShowDialog();

            //  MessageBox.Show("Got:" + child.direction);
            //    Console.WriteLine(child.direction);


        }

        private void btnStartPoint_Click(object sender, EventArgs e)  //toggle button apperance to start red mark.
        {
            if (btnStartPointFlag == false)  // = btnStartPoint.FlatStyle == FlatStyle.Standard
            {
                btnStartPoint.FlatStyle = FlatStyle.Flat;
                btnStartPointFlag = !btnStartPointFlag;
            }
            else
            {
                btnStartPoint.FlatStyle = FlatStyle.Standard;
                btnStartPointFlag = !btnStartPointFlag;
            }
        }

        private void btnMeasureDistance_Click(object sender, EventArgs e)
        {
            // markers.Markers.ElementAt(i).Position.Lat;
            var x1 = markers.Markers.ElementAt(0).Position.Lat;
            var y1 = markers.Markers.ElementAt(0).Position.Lng;
            var x2 = markers.Markers.ElementAt(1).Position.Lat;
            var y2 = markers.Markers.ElementAt(1).Position.Lng;

            PointLatLng p1 = new PointLatLng(x1, y1);
            PointLatLng p2 = new PointLatLng(x2, y2);
            double dist = mymap.getDistance(p1, p2);

            //double dist = mymap.getDistance(markers.Markers.ElementAt(0), markers.Markers.ElementAt(1));
            txtDistanceM.Text = (dist * 1000).ToString("0.0");
            txtDistanceKm.Text = (dist).ToString("0.00");
        }

        private void btnForward_Click(object sender, EventArgs e)
        {
            if (Open == true)
                serialPort1.Write("forward, 50," + txtWheelTurnNo.Text);  //forward,50,1
            else MessageBox.Show("Port open. Close it !");
        }

        private void btnBackward_Click(object sender, EventArgs e)
        {
            if (Open == true)
                serialPort1.Write("backward, 50," + txtWheelTurnNo.Text);
            else MessageBox.Show("Port open. Close it !");
        }

        private void bthLeft_Click(object sender, EventArgs e)
        {
            if (Open == true)
                serialPort1.Write("left, 50," + txtWheelTurnNo.Text);
            else MessageBox.Show("Port open. Close it !");
        }

        private void btnRight_Click(object sender, EventArgs e)
        {
            if (Open == true)
                serialPort1.Write("right," + txtWheelTurnNo.Text);
            else MessageBox.Show("Port open. Close it !");
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            if (Open == true)
            serialPort1.Write("start");  //forward,50,1
            else MessageBox.Show("Port open. Close it !");
        }

        private void btnStop_Click(object sender, EventArgs e)
        {
            if (Open == true)
            serialPort1.Write("stop, 50, 1");  //forward,50,1
            else MessageBox.Show("Port open. Close it !");
        }

        private void btnSendMessage_Click(object sender, EventArgs e)
        {
            if (Open == true)
                serialPort1.Write(txtSendMessage.Text);  //forward,50,1
            else MessageBox.Show("Port open. Close it !");
        }

        private void btnClearListbox_Click(object sender, EventArgs e)
        {
            lbRoverMsg.Items.Clear();
        }

        private void btnPutRoverMap_Click(object sender, EventArgs e)
        {
            String latS = txtRoverRemLat.Text;  // yellow arrow coords, thus shall come from the rover's GPS !!!
            String lngS = txtRoverRemLng.Text;
            String headS = txtRoverRemHead.Text;

            GMarkerArrow marker1 = new GMarkerArrow(new PointLatLng(Convert.ToDouble(latS), Convert.ToDouble(lngS)));  // it isi in GMarkerArrow.cs class
            marker1.ToolTipText = "Rover position";
            marker1.ToolTip.Fill = Brushes.Black;
            marker1.ToolTip.Foreground = Brushes.White;
            marker1.ToolTip.Stroke = Pens.Black;
            marker1.Bearing = float.Parse(headS); // this should come from the compass/robot IMU !!!
            marker1.Fill = new SolidBrush(Color.FromArgb(155, Color.Green)); // Arrow color
            markers.Markers.Add(marker1); map.Overlays.Add(markers);  //map is the name of the map on the form !
        }

       
    } // end public partial class Form1 : Form
}
