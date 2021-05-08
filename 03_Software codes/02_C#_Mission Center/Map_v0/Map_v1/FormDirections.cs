using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Map_v1
{
    public partial class FormDirections : Form
    {
        public FormDirections()
        {
            InitializeComponent();
        }

       public String direction = "---";
       public bool clickDone = false;

        private void btnUp_Click(object sender, EventArgs e)
        {
            direction = "up";
            clickDone = true;
           // this.Hide();
            this.Close();
        }

        private void btnRight_Click(object sender, EventArgs e)
        {
            direction = "right";
            clickDone = true;
            // this.Hide();
            this.Close();
        }

        private void btnDown_Click(object sender, EventArgs e)
        {
            direction = "down";
            clickDone = true;
            // this.Hide();
            this.Close();
        }

        private void btnLeft_Click(object sender, EventArgs e)
        {
            direction = "left";
            clickDone = true;
            // this.Hide();
            this.Close();
        }


    }
}
