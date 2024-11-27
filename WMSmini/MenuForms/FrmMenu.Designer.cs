namespace WMSMobileClient
{
    partial class FrmMenu
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.MainMenu mainMenu1;

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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmMenu));
            this.mainMenu1 = new System.Windows.Forms.MainMenu();
            this.PBPutaway = new System.Windows.Forms.PictureBox();
            this.PBReceiving = new System.Windows.Forms.PictureBox();
            this.PBexports = new System.Windows.Forms.PictureBox();
            this.PBpicking = new System.Windows.Forms.PictureBox();
            this.PBinventory = new System.Windows.Forms.PictureBox();
            this.PBMenuSettings = new System.Windows.Forms.PictureBox();
            this.SuspendLayout();
            // 
            // PBPutaway
            // 
            this.PBPutaway.BackColor = System.Drawing.Color.Transparent;
            this.PBPutaway.Image = ((System.Drawing.Image)(resources.GetObject("PBPutaway.Image")));
            this.PBPutaway.Location = new System.Drawing.Point(57, 395);
            this.PBPutaway.Name = "PBPutaway";
            this.PBPutaway.Size = new System.Drawing.Size(140, 140);
            this.PBPutaway.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.PBPutaway.Visible = false;
            this.PBPutaway.MouseMove += new System.Windows.Forms.MouseEventHandler(this.PBPutaway_MouseMove);
            this.PBPutaway.Click += new System.EventHandler(this.PBPutaway_Click);
            this.PBPutaway.MouseDown += new System.Windows.Forms.MouseEventHandler(this.PBPutaway_MouseDown);
            // 
            // PBReceiving
            // 
            this.PBReceiving.BackColor = System.Drawing.Color.Transparent;
            this.PBReceiving.Image = ((System.Drawing.Image)(resources.GetObject("PBReceiving.Image")));
            this.PBReceiving.Location = new System.Drawing.Point(58, 249);
            this.PBReceiving.Name = "PBReceiving";
            this.PBReceiving.Size = new System.Drawing.Size(140, 140);
            this.PBReceiving.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.PBReceiving.MouseMove += new System.Windows.Forms.MouseEventHandler(this.PBReceiving_MouseMove);
            this.PBReceiving.Click += new System.EventHandler(this.PBReceiving_Click);
            this.PBReceiving.GotFocus += new System.EventHandler(this.PBReceiving_GotFocus);
            this.PBReceiving.MouseDown += new System.Windows.Forms.MouseEventHandler(this.PBReceiving_MouseDown);
            // 
            // PBexports
            // 
            this.PBexports.BackColor = System.Drawing.Color.Transparent;
            this.PBexports.Image = ((System.Drawing.Image)(resources.GetObject("PBexports.Image")));
            this.PBexports.Location = new System.Drawing.Point(267, 247);
            this.PBexports.Name = "PBexports";
            this.PBexports.Size = new System.Drawing.Size(140, 140);
            this.PBexports.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.PBexports.Visible = false;
            this.PBexports.MouseMove += new System.Windows.Forms.MouseEventHandler(this.PBexports_MouseMove);
            this.PBexports.Click += new System.EventHandler(this.PBexports_Click);
            this.PBexports.GotFocus += new System.EventHandler(this.PBexports_GotFocus);
            this.PBexports.MouseDown += new System.Windows.Forms.MouseEventHandler(this.PBexports_MouseDown);
            // 
            // PBpicking
            // 
            this.PBpicking.BackColor = System.Drawing.Color.Transparent;
            this.PBpicking.Image = ((System.Drawing.Image)(resources.GetObject("PBpicking.Image")));
            this.PBpicking.Location = new System.Drawing.Point(57, 63);
            this.PBpicking.Name = "PBpicking";
            this.PBpicking.Size = new System.Drawing.Size(140, 140);
            this.PBpicking.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.PBpicking.MouseMove += new System.Windows.Forms.MouseEventHandler(this.PBpicking_MouseMove);
            this.PBpicking.Click += new System.EventHandler(this.PBpicking_Click);
            this.PBpicking.GotFocus += new System.EventHandler(this.PBpicking_GotFocus);
            this.PBpicking.MouseDown += new System.Windows.Forms.MouseEventHandler(this.PBpicking_MouseDown);
            // 
            // PBinventory
            // 
            this.PBinventory.BackColor = System.Drawing.Color.Transparent;
            this.PBinventory.Image = ((System.Drawing.Image)(resources.GetObject("PBinventory.Image")));
            this.PBinventory.Location = new System.Drawing.Point(267, 63);
            this.PBinventory.Name = "PBinventory";
            this.PBinventory.Size = new System.Drawing.Size(140, 140);
            this.PBinventory.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.PBinventory.MouseMove += new System.Windows.Forms.MouseEventHandler(this.PBinventory_MouseMove);
            this.PBinventory.Click += new System.EventHandler(this.PBinventory_Click);
            this.PBinventory.GotFocus += new System.EventHandler(this.PBinventory_GotFocus);
            this.PBinventory.MouseDown += new System.Windows.Forms.MouseEventHandler(this.PBinventory_MouseDown);
            // 
            // PBMenuSettings
            // 
            this.PBMenuSettings.Image = ((System.Drawing.Image)(resources.GetObject("PBMenuSettings.Image")));
            this.PBMenuSettings.Location = new System.Drawing.Point(267, 395);
            this.PBMenuSettings.Name = "PBMenuSettings";
            this.PBMenuSettings.Size = new System.Drawing.Size(140, 140);
            this.PBMenuSettings.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.PBMenuSettings.MouseMove += new System.Windows.Forms.MouseEventHandler(this.PBMenuSettings_MouseMove);
            this.PBMenuSettings.Click += new System.EventHandler(this.PBMenuSettings_Click);
            this.PBMenuSettings.MouseDown += new System.Windows.Forms.MouseEventHandler(this.PBMenuSettings_MouseDown);
            // 
            // FrmMenu
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(480, 640);
            this.ControlBox = false;
            this.Controls.Add(this.PBPutaway);
            this.Controls.Add(this.PBReceiving);
            this.Controls.Add(this.PBexports);
            this.Controls.Add(this.PBpicking);
            this.Controls.Add(this.PBinventory);
            this.Controls.Add(this.PBMenuSettings);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Location = new System.Drawing.Point(0, 0);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmMenu";
            this.Text = "WMSClient - ΗΟΜΕ";
            this.Load += new System.EventHandler(this.FrmMenu_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.FrmMenu_KeyDown);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox PBinventory;
        private System.Windows.Forms.PictureBox PBMenuSettings;
        private System.Windows.Forms.PictureBox PBpicking;
        private System.Windows.Forms.PictureBox PBexports;
        private System.Windows.Forms.PictureBox PBReceiving;
        private System.Windows.Forms.PictureBox PBPutaway;
    }
}