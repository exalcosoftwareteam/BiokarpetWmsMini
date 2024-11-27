namespace WMSMobileClient
{
    partial class FrmOfflineSettings
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmOfflineSettings));
            this.mainMenu1 = new System.Windows.Forms.MainMenu();
            this.LBSyncItems = new System.Windows.Forms.Label();
            this.LBSyncLots = new System.Windows.Forms.Label();
            this.ImgSyncLots = new System.Windows.Forms.PictureBox();
            this.LBImportitemsrows = new System.Windows.Forms.Label();
            this.LBImportlotrows = new System.Windows.Forms.Label();
            this.ImgSyncItems = new System.Windows.Forms.PictureBox();
            this.LBSyncInfo = new System.Windows.Forms.Label();
            this.PBBtnBck = new System.Windows.Forms.PictureBox();
            this.PBMenuBar = new System.Windows.Forms.PictureBox();
            this.BtnSyncData = new System.Windows.Forms.Button();
            this.lb_etimatedtime = new System.Windows.Forms.Label();
            this.dateTimePicker1 = new System.Windows.Forms.DateTimePicker();
            this.cb_fromdate = new System.Windows.Forms.CheckBox();
            this.label1 = new System.Windows.Forms.Label();
            this.cb_checkifexists = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // LBSyncItems
            // 
            this.LBSyncItems.Location = new System.Drawing.Point(10, 159);
            this.LBSyncItems.Name = "LBSyncItems";
            this.LBSyncItems.Size = new System.Drawing.Size(136, 16);
            this.LBSyncItems.Text = "Συγχρονισμός  Ειδών...";
            // 
            // LBSyncLots
            // 
            this.LBSyncLots.Location = new System.Drawing.Point(10, 202);
            this.LBSyncLots.Name = "LBSyncLots";
            this.LBSyncLots.Size = new System.Drawing.Size(173, 20);
            this.LBSyncLots.Text = "Συγχρονισμός Παρτίδων...";
            // 
            // ImgSyncLots
            // 
            this.ImgSyncLots.Image = ((System.Drawing.Image)(resources.GetObject("ImgSyncLots.Image")));
            this.ImgSyncLots.Location = new System.Drawing.Point(193, 201);
            this.ImgSyncLots.Name = "ImgSyncLots";
            this.ImgSyncLots.Size = new System.Drawing.Size(39, 36);
            this.ImgSyncLots.Visible = false;
            // 
            // LBImportitemsrows
            // 
            this.LBImportitemsrows.Font = new System.Drawing.Font("Tahoma", 7F, System.Drawing.FontStyle.Bold);
            this.LBImportitemsrows.ForeColor = System.Drawing.Color.SteelBlue;
            this.LBImportitemsrows.Location = new System.Drawing.Point(9, 179);
            this.LBImportitemsrows.Name = "LBImportitemsrows";
            this.LBImportitemsrows.Size = new System.Drawing.Size(177, 16);
            this.LBImportitemsrows.Text = "Εισαγωγή";
            this.LBImportitemsrows.Visible = false;
            // 
            // LBImportlotrows
            // 
            this.LBImportlotrows.Font = new System.Drawing.Font("Tahoma", 7F, System.Drawing.FontStyle.Bold);
            this.LBImportlotrows.ForeColor = System.Drawing.Color.SteelBlue;
            this.LBImportlotrows.Location = new System.Drawing.Point(9, 220);
            this.LBImportlotrows.Name = "LBImportlotrows";
            this.LBImportlotrows.Size = new System.Drawing.Size(177, 16);
            this.LBImportlotrows.Text = "Εισαγωγή";
            this.LBImportlotrows.Visible = false;
            // 
            // ImgSyncItems
            // 
            this.ImgSyncItems.Image = ((System.Drawing.Image)(resources.GetObject("ImgSyncItems.Image")));
            this.ImgSyncItems.Location = new System.Drawing.Point(193, 153);
            this.ImgSyncItems.Name = "ImgSyncItems";
            this.ImgSyncItems.Size = new System.Drawing.Size(39, 42);
            this.ImgSyncItems.Visible = false;
            // 
            // LBSyncInfo
            // 
            this.LBSyncInfo.Font = new System.Drawing.Font("Tahoma", 7F, System.Drawing.FontStyle.Regular);
            this.LBSyncInfo.Location = new System.Drawing.Point(11, 121);
            this.LBSyncInfo.Name = "LBSyncInfo";
            this.LBSyncInfo.Size = new System.Drawing.Size(217, 10);
            this.LBSyncInfo.Text = "Βρέθηκαν :";
            // 
            // PBBtnBck
            // 
            this.PBBtnBck.Image = ((System.Drawing.Image)(resources.GetObject("PBBtnBck.Image")));
            this.PBBtnBck.Location = new System.Drawing.Point(2, 274);
            this.PBBtnBck.Name = "PBBtnBck";
            this.PBBtnBck.Size = new System.Drawing.Size(64, 44);
            this.PBBtnBck.Click += new System.EventHandler(this.PBBtnBck_Click);
            this.PBBtnBck.GotFocus += new System.EventHandler(this.PBBtnBck_GotFocus);
            this.PBBtnBck.MouseDown += new System.Windows.Forms.MouseEventHandler(this.PBBtnBck_MouseDown);
            this.PBBtnBck.LostFocus += new System.EventHandler(this.PBBtnBck_LostFocus);
            // 
            // PBMenuBar
            // 
            this.PBMenuBar.Image = ((System.Drawing.Image)(resources.GetObject("PBMenuBar.Image")));
            this.PBMenuBar.Location = new System.Drawing.Point(0, 270);
            this.PBMenuBar.Name = "PBMenuBar";
            this.PBMenuBar.Size = new System.Drawing.Size(241, 50);
            // 
            // BtnSyncData
            // 
            this.BtnSyncData.BackColor = System.Drawing.Color.DeepSkyBlue;
            this.BtnSyncData.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular);
            this.BtnSyncData.ForeColor = System.Drawing.Color.White;
            this.BtnSyncData.Location = new System.Drawing.Point(12, 14);
            this.BtnSyncData.Name = "BtnSyncData";
            this.BtnSyncData.Size = new System.Drawing.Size(218, 72);
            this.BtnSyncData.TabIndex = 13;
            this.BtnSyncData.Text = "Ενημέρωση Δεδομένων";
            this.BtnSyncData.Click += new System.EventHandler(this.button1_Click);
            // 
            // lb_etimatedtime
            // 
            this.lb_etimatedtime.Font = new System.Drawing.Font("Tahoma", 7F, System.Drawing.FontStyle.Regular);
            this.lb_etimatedtime.Location = new System.Drawing.Point(11, 135);
            this.lb_etimatedtime.Name = "lb_etimatedtime";
            this.lb_etimatedtime.Size = new System.Drawing.Size(217, 13);
            // 
            // dateTimePicker1
            // 
            this.dateTimePicker1.Enabled = false;
            this.dateTimePicker1.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dateTimePicker1.Location = new System.Drawing.Point(223, 112);
            this.dateTimePicker1.Name = "dateTimePicker1";
            this.dateTimePicker1.Size = new System.Drawing.Size(2, 22);
            this.dateTimePicker1.TabIndex = 23;
            this.dateTimePicker1.Visible = false;
            // 
            // cb_fromdate
            // 
            this.cb_fromdate.Location = new System.Drawing.Point(174, 94);
            this.cb_fromdate.Name = "cb_fromdate";
            this.cb_fromdate.Size = new System.Drawing.Size(28, 20);
            this.cb_fromdate.TabIndex = 24;
            this.cb_fromdate.Visible = false;
            this.cb_fromdate.CheckStateChanged += new System.EventHandler(this.cb_fromdate_CheckStateChanged);
            this.cb_fromdate.Click += new System.EventHandler(this.checkBox1_Click);
            // 
            // label1
            // 
            this.label1.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.label1.Location = new System.Drawing.Point(208, 96);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(24, 13);
            this.label1.Text = "Από ημ/νία";
            this.label1.TextAlign = System.Drawing.ContentAlignment.TopRight;
            this.label1.Visible = false;
            // 
            // cb_checkifexists
            // 
            this.cb_checkifexists.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.cb_checkifexists.Location = new System.Drawing.Point(12, 94);
            this.cb_checkifexists.Name = "cb_checkifexists";
            this.cb_checkifexists.Size = new System.Drawing.Size(220, 19);
            this.cb_checkifexists.TabIndex = 35;
            this.cb_checkifexists.Text = "Ενημέρωση υπάρχοντων ( πιο αργό )";
            this.cb_checkifexists.CheckStateChanged += new System.EventHandler(this.cb_checkifexists_CheckStateChanged);
            // 
            // FrmOfflineSettings
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.ClientSize = new System.Drawing.Size(240, 320);
            this.Controls.Add(this.cb_checkifexists);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.cb_fromdate);
            this.Controls.Add(this.dateTimePicker1);
            this.Controls.Add(this.lb_etimatedtime);
            this.Controls.Add(this.BtnSyncData);
            this.Controls.Add(this.PBBtnBck);
            this.Controls.Add(this.PBMenuBar);
            this.Controls.Add(this.LBSyncInfo);
            this.Controls.Add(this.ImgSyncItems);
            this.Controls.Add(this.LBImportlotrows);
            this.Controls.Add(this.LBImportitemsrows);
            this.Controls.Add(this.ImgSyncLots);
            this.Controls.Add(this.LBSyncLots);
            this.Controls.Add(this.LBSyncItems);
            this.Name = "FrmOfflineSettings";
            this.Text = "Λήψη σταθερών στοιχείων";
            this.Load += new System.EventHandler(this.FrmOfflineSettings_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.FrmOfflineSettings_KeyDown);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label LBSyncItems;
        private System.Windows.Forms.Label LBSyncLots;
        private System.Windows.Forms.PictureBox ImgSyncLots;
        private System.Windows.Forms.Label LBImportitemsrows;
        private System.Windows.Forms.Label LBImportlotrows;
        private System.Windows.Forms.PictureBox ImgSyncItems;
        private System.Windows.Forms.Label LBSyncInfo;
        private System.Windows.Forms.PictureBox PBBtnBck;
        private System.Windows.Forms.PictureBox PBMenuBar;
        private System.Windows.Forms.Button BtnSyncData;
        private System.Windows.Forms.Label lb_etimatedtime;
        private System.Windows.Forms.DateTimePicker dateTimePicker1;
        private System.Windows.Forms.CheckBox cb_fromdate;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.CheckBox cb_checkifexists;
    }
}