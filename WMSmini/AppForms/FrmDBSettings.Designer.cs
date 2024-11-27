namespace WMSMobileClient
{
    partial class FrmDBSettings
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmDBSettings));
            this.mainMenu1 = new System.Windows.Forms.MainMenu();
            this.LBPathInfo = new System.Windows.Forms.Label();
            this.LBPathlabel = new System.Windows.Forms.Label();
            this.LB = new System.Windows.Forms.Label();
            this.LBDBSize = new System.Windows.Forms.Label();
            this.BtnDeleteDB = new System.Windows.Forms.Button();
            this.BtnCreateDB = new System.Windows.Forms.Button();
            this.PBBtnBck = new System.Windows.Forms.PictureBox();
            this.PBMenuBar = new System.Windows.Forms.PictureBox();
            this.SuspendLayout();
            // 
            // LBPathInfo
            // 
            this.LBPathInfo.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.LBPathInfo.Location = new System.Drawing.Point(5, 26);
            this.LBPathInfo.Name = "LBPathInfo";
            this.LBPathInfo.Size = new System.Drawing.Size(226, 46);
            // 
            // LBPathlabel
            // 
            this.LBPathlabel.Font = new System.Drawing.Font("Tahoma", 7F, System.Drawing.FontStyle.Regular);
            this.LBPathlabel.Location = new System.Drawing.Point(3, 10);
            this.LBPathlabel.Name = "LBPathlabel";
            this.LBPathlabel.Size = new System.Drawing.Size(224, 20);
            this.LBPathlabel.Text = "Διαδρομή στο μέσο αποθήκευσης";
            // 
            // LB
            // 
            this.LB.Font = new System.Drawing.Font("Tahoma", 7F, System.Drawing.FontStyle.Regular);
            this.LB.Location = new System.Drawing.Point(7, 72);
            this.LB.Name = "LB";
            this.LB.Size = new System.Drawing.Size(224, 20);
            this.LB.Text = "Μέγεθος Βάσης Δεδομένων";
            // 
            // LBDBSize
            // 
            this.LBDBSize.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.LBDBSize.Location = new System.Drawing.Point(9, 92);
            this.LBDBSize.Name = "LBDBSize";
            this.LBDBSize.Size = new System.Drawing.Size(224, 21);
            // 
            // BtnDeleteDB
            // 
            this.BtnDeleteDB.BackColor = System.Drawing.Color.Tomato;
            this.BtnDeleteDB.Location = new System.Drawing.Point(8, 133);
            this.BtnDeleteDB.Name = "BtnDeleteDB";
            this.BtnDeleteDB.Size = new System.Drawing.Size(201, 28);
            this.BtnDeleteDB.TabIndex = 6;
            this.BtnDeleteDB.Text = "Διαγραφή Βάσης Δεδομένων";
            this.BtnDeleteDB.Click += new System.EventHandler(this.BtnDeleteDB_Click);
            this.BtnDeleteDB.KeyDown += new System.Windows.Forms.KeyEventHandler(this.BtnDeleteDB_KeyDown);
            // 
            // BtnCreateDB
            // 
            this.BtnCreateDB.BackColor = System.Drawing.Color.LightSeaGreen;
            this.BtnCreateDB.Location = new System.Drawing.Point(8, 167);
            this.BtnCreateDB.Name = "BtnCreateDB";
            this.BtnCreateDB.Size = new System.Drawing.Size(201, 28);
            this.BtnCreateDB.TabIndex = 7;
            this.BtnCreateDB.Text = "Δημιουργία Βάσης Δεδομένων";
            this.BtnCreateDB.Click += new System.EventHandler(this.BtnCreateDB_Click);
            this.BtnCreateDB.KeyDown += new System.Windows.Forms.KeyEventHandler(this.BtnCreateDB_KeyDown);
            // 
            // PBBtnBck
            // 
            this.PBBtnBck.Image = ((System.Drawing.Image)(resources.GetObject("PBBtnBck.Image")));
            this.PBBtnBck.Location = new System.Drawing.Point(5, 273);
            this.PBBtnBck.Name = "PBBtnBck";
            this.PBBtnBck.Size = new System.Drawing.Size(64, 44);
            this.PBBtnBck.Click += new System.EventHandler(this.PBBtnBck_Click);
            this.PBBtnBck.GotFocus += new System.EventHandler(this.PBBtnBck_GotFocus);
            this.PBBtnBck.MouseDown += new System.Windows.Forms.MouseEventHandler(this.PBBtnBck_MouseDown);
            this.PBBtnBck.LostFocus += new System.EventHandler(this.PBBtnBck_LostFocus);
            this.PBBtnBck.MouseUp += new System.Windows.Forms.MouseEventHandler(this.PBBtnBck_MouseUp);
            // 
            // PBMenuBar
            // 
            this.PBMenuBar.Image = ((System.Drawing.Image)(resources.GetObject("PBMenuBar.Image")));
            this.PBMenuBar.Location = new System.Drawing.Point(-1, 270);
            this.PBMenuBar.Name = "PBMenuBar";
            this.PBMenuBar.Size = new System.Drawing.Size(241, 50);
            // 
            // FrmDBSettings
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(240, 320);
            this.ControlBox = false;
            this.Controls.Add(this.PBBtnBck);
            this.Controls.Add(this.PBMenuBar);
            this.Controls.Add(this.BtnCreateDB);
            this.Controls.Add(this.BtnDeleteDB);
            this.Controls.Add(this.LB);
            this.Controls.Add(this.LBDBSize);
            this.Controls.Add(this.LBPathlabel);
            this.Controls.Add(this.LBPathInfo);
            this.Name = "FrmDBSettings";
            this.Text = "Τοπική Βάση Δεδομένων";
            this.Load += new System.EventHandler(this.FrmDBSettings_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.FrmDBSettings_KeyDown);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label LBPathInfo;
        private System.Windows.Forms.Label LBPathlabel;
        private System.Windows.Forms.Label LB;
        private System.Windows.Forms.Label LBDBSize;
        private System.Windows.Forms.Button BtnDeleteDB;
        private System.Windows.Forms.Button BtnCreateDB;
        private System.Windows.Forms.PictureBox PBBtnBck;
        private System.Windows.Forms.PictureBox PBMenuBar;

        
    }
}