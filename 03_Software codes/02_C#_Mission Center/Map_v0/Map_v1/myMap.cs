using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using GMap.NET;
using GMap.NET.MapProviders;
using GMap.NET.WindowsForms;
using GMap.NET.WindowsForms.Markers;
using GMap.NET.WindowsForms.ToolTips;
using System.IO.Ports;
using System.Threading;
using System.Data.OleDb;
using System.Runtime.Serialization;

namespace Map_v1
{
    public struct PolyBoundry
    {
        public double Xmin;
        public double Xmax;
        public double Ymin;
        public double Ymax;
    }

    class myMap
    {
        public myMap()
        {
        }

        public int justGo()
        {
            int a = 1;
            return a;
        }

        public PolyBoundry values = new PolyBoundry();

        public DialogResult ShowInputDialog(ref string input)
        {
            System.Drawing.Size size = new System.Drawing.Size(200, 70);
            Form inputBox = new Form();

            inputBox.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            inputBox.ClientSize = size;
            inputBox.Text = "File name:";
            inputBox.StartPosition = FormStartPosition.CenterParent;

            System.Windows.Forms.TextBox textBox = new TextBox();
            textBox.Size = new System.Drawing.Size(size.Width - 10, 23);
            textBox.Location = new System.Drawing.Point(5, 5);
            textBox.Text = input;
            inputBox.Controls.Add(textBox);

            Button okButton = new Button();
            okButton.DialogResult = System.Windows.Forms.DialogResult.OK;
            okButton.Name = "okButton";
            okButton.Size = new System.Drawing.Size(75, 23);
            okButton.Text = "&OK";
            okButton.Location = new System.Drawing.Point(size.Width - 80 - 80, 39);
            inputBox.Controls.Add(okButton);

            Button cancelButton = new Button();
            cancelButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            cancelButton.Name = "cancelButton";
            cancelButton.Size = new System.Drawing.Size(75, 23);
            cancelButton.Text = "&Cancel";
            cancelButton.Location = new System.Drawing.Point(size.Width - 80, 39);
            inputBox.Controls.Add(cancelButton);

            inputBox.AcceptButton = okButton;
            inputBox.CancelButton = cancelButton;

            DialogResult result = inputBox.ShowDialog();
            input = textBox.Text;
            return result;
        }

        public PolyBoundry calulateBoundry(List<PointLatLng> coords)
        {
            PolyBoundry values = new PolyBoundry();

            //  var center = map.Position;
            var c = coords.Count;
            int minXpoint, maxXpoint, minYpoint, maxYpoint; // records whis element# marker ins the max.&min
            // var X = 0;

            double Xsum = 0, Ysum = 0;
            for (int i = 0; i < c; i++)
            {
                Xsum = Xsum + coords.ElementAt(i).Lat;
                Ysum = Ysum + coords.ElementAt(i).Lng;
            }

            var Xmin = Xsum / c; var Xmax = Xsum / c;   //center.Lng  - average
            var Ymin = Ysum / c; var Ymax = Ysum / c; //center.Lat - average


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

            return values;
        }

        public double getDistance(PointLatLng p1, PointLatLng p2)
        {
            GMapRoute route = new GMapRoute("getDistance");
            route.Points.Add(p1);
            route.Points.Add(p2);
            double distance = route.Distance;
            route.Clear();
            route = null;

            return distance;
        }


        public string decodeNMEA(string serialData)  //SerialPortGPS.ReadLine  procedure not in use.
        {
            string InNMEA, InNMEA1, InNMEA2, newNMEA;
           // char coma = ',';  //or char
           // char star = '*';  //or char
            // char limiters[] = {coma, star};
            int i = 0;
            string[,] Sats;

            InNMEA = serialData;
            //InNMEA = ReceiveSerialData()
            //InNMEA = "$GPGGA,201702.000,4730.7845,N,01906.9620,E,1,09,0.9,150.9,M,0.0,M,,0000*6A"
            //InNMEA = "$GPGSV,3,1,11,26,69,307,18,28,65,135,29,08,56,056,28,05,44,229,38*7D"
            //ListBox1.Items.Add(InNMEA)

           // String[] NMEAfields = InNMEA.Split(coma);  //limiters
            string[] NMEAfields = InNMEA.Split(new[] { ',', '*' }, StringSplitOptions.RemoveEmptyEntries);

          //  NMEAfields[0] = "---";

            String[] NMEAdecoded = new String[100];

            switch (NMEAfields[0].Substring(4, 3))
            {
                case "GGA":  //$GPGGA,201702.000,4730.7845,N,01906.9620,E,1,09,0.9,150.9,M,0.0,M,,0000*6A
                    {
                        //UTC time:
                        NMEAdecoded[1] = NMEAfields[1].Substring(1, 2) + "h" + NMEAfields[1].Substring(3, 2) + "m" + NMEAfields[1].Substring(5, 2) + "s";
                        //Latitude & :Longitude
                        NMEAdecoded[2] = NMEAfields[2].Substring(1, 2) + "° " + NMEAfields[2].Substring(3, 7) + "'";
                        NMEAdecoded[3] = NMEAfields[4].Substring(1, 3) + "° " + NMEAfields[4].Substring(4, 7) + "'";
                       // txtRoverRemLat.Text = NMEAfields[2];
                      //  txtRoverRemLng.Text = NMEAfields[3];
                      //  txtRoverRemHead.Text = "heading"; // ??? which field ???
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
                    return ""; // NMEAdecoded;
                    }
                case "GSV":   // GSV—GNSS Satellites in View  $GPGSV,3,1,11,  26,69,307,18,  28,65,135,29,  08,56,056,28,  05,44,229,38  *7D
                    {
                        return ""; // NMEAdecoded;
                    }
            }  // end switch
            return ""; // NMEAdecoded;
        }



    }
}
