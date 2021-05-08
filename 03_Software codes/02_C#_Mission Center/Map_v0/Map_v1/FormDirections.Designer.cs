namespace Map_v1
{
    partial class FormDirections
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.btnLeft = new System.Windows.Forms.Button();
            this.btnDown = new System.Windows.Forms.Button();
            this.btnRight = new System.Windows.Forms.Button();
            this.btnUp = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btnLeft
            // 
            this.btnLeft.BackgroundImage = global::Map_v1.Properties.Resources.dir_Left;
            this.btnLeft.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnLeft.Location = new System.Drawing.Point(12, 75);
            this.btnLeft.Name = "btnLeft";
            this.btnLeft.Size = new System.Drawing.Size(58, 58);
            this.btnLeft.TabIndex = 3;
            this.btnLeft.UseVisualStyleBackColor = true;
            this.btnLeft.Click += new System.EventHandler(this.btnLeft_Click);
            // 
            // btnDown
            // 
            this.btnDown.BackgroundImage = global::Map_v1.Properties.Resources.dir_Down;
            this.btnDown.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnDown.Location = new System.Drawing.Point(76, 75);
            this.btnDown.Name = "btnDown";
            this.btnDown.Size = new System.Drawing.Size(58, 58);
            this.btnDown.TabIndex = 2;
            this.btnDown.UseVisualStyleBackColor = true;
            this.btnDown.Click += new System.EventHandler(this.btnDown_Click);
            // 
            // btnRight
            // 
            this.btnRight.BackgroundImage = global::Map_v1.Properties.Resources.dir_Right;
            this.btnRight.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnRight.Location = new System.Drawing.Point(76, 12);
            this.btnRight.Name = "btnRight";
            this.btnRight.Size = new System.Drawing.Size(58, 58);
            this.btnRight.TabIndex = 1;
            this.btnRight.UseVisualStyleBackColor = true;
            this.btnRight.Click += new System.EventHandler(this.btnRight_Click);
            // 
            // btnUp
            // 
            this.btnUp.BackgroundImage = global::Map_v1.Properties.Resources.dir_Up;
            this.btnUp.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnUp.Location = new System.Drawing.Point(12, 12);
            this.btnUp.Name = "btnUp";
            this.btnUp.Size = new System.Drawing.Size(58, 58);
            this.btnUp.TabIndex = 0;
            this.btnUp.UseVisualStyleBackColor = true;
            this.btnUp.Click += new System.EventHandler(this.btnUp_Click);
            // 
            // FormDirections
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(149, 144);
            this.Controls.Add(this.btnLeft);
            this.Controls.Add(this.btnDown);
            this.Controls.Add(this.btnRight);
            this.Controls.Add(this.btnUp);
            this.Name = "FormDirections";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "FormDirections";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnUp;
        private System.Windows.Forms.Button btnRight;
        private System.Windows.Forms.Button btnDown;
        private System.Windows.Forms.Button btnLeft;
    }
}