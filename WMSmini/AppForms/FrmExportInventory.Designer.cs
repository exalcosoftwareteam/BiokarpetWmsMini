namespace WMSMobileClient
{
    partial class FrmExportInventory
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmExportInventory));
            this.mainMenu1 = new System.Windows.Forms.MainMenu();
            this.LBExpInvRows = new System.Windows.Forms.Label();
            this.BtnSyncData = new System.Windows.Forms.Button();
            this.LBExportInventory = new System.Windows.Forms.Label();
            this.ImgExportInventory = new System.Windows.Forms.PictureBox();
            this.PBBtnBck = new System.Windows.Forms.PictureBox();
            this.PBMenuBar = new System.Windows.Forms.PictureBox();
            this.SuspendLayout();
            // 
            // LBExpInvRows
            // 
            this.LBExpInvRows.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Bold);
            this.LBExpInvRows.ForeColor = System.Drawing.Color.SteelBlue;
            this.LBExpInvRows.Location = new System.Drawing.Point(10, 158);
            this.LBExpInvRows.Name = "LBExpInvRows";
            this.LBExpInvRows.Size = new System.Drawing.Size(223, 57);
            this.LBExpInvRows.Text = "Εξαγωγή";
            this.LBExpInvRows.Visible = false;
            // 
            // BtnSyncData
            // 
            this.BtnSyncData.BackColor = System.Drawing.Color.DeepSkyBlue;
            this.BtnSyncData.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular);
            this.BtnSyncData.ForeColor = System.Drawing.Color.White;
            this.BtnSyncData.Location = new System.Drawing.Point(10, 9);
            this.BtnSyncData.Name = "BtnSyncData";
            this.BtnSyncData.Size = new System.Drawing.Size(223, 70);
            this.BtnSyncData.TabIndex = 48;
            this.BtnSyncData.Text = "Εξαγωγή Απογραφής";
            this.BtnSyncData.Click += new System.EventHandler(this.BtnSyncData_Click);
            // 
            // LBExportInventory
            // 
            this.LBExportInventory.Location = new System.Drawing.Point(15, 122);
            this.LBExportInventory.Name = "LBExportInventory";
            this.LBExportInventory.Size = new System.Drawing.Size(136, 16);
            this.LBExportInventory.Text = "Εξαγωγή απογραφής...";
            // 
            // ImgExportInventory
            // 
            this.ImgExportInventory.Image = ((System.Drawing.Image)(resources.GetObject("ImgExportInventory.Image")));
            this.ImgExportInventory.Location = new System.Drawing.Point(185, 106);
            this.ImgExportInventory.Name = "ImgExportInventory";
            this.ImgExportInventory.Size = new System.Drawing.Size(48, 39);
            this.ImgExportInventory.Visible = false;
            // 
            // PBBtnBck
            // 
            this.PBBtnBck.Image = ((System.Drawing.Image)(resources.GetObject("PBBtnBck.Image")));
            this.PBBtnBck.Location = new System.Drawing.Point(5, 274);
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
            this.PBMenuBar.Location = new System.Drawing.Point(0, 270);
            this.PBMenuBar.Name = "PBMenuBar";
            this.PBMenuBar.Size = new System.Drawing.Size(241, 50);
            // 
            // FrmExportInventory
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.ClientSize = new System.Drawing.Size(240, 320);
            this.ControlBox = false;
            this.Controls.Add(this.PBBtnBck);
            this.Controls.Add(this.PBMenuBar);
            this.Controls.Add(this.ImgExportInventory);
            this.Controls.Add(this.LBExpInvRows);
            this.Controls.Add(this.BtnSyncData);
            this.Controls.Add(this.LBExportInventory);
            this.Name = "FrmExportInventory";
            this.Text = "Εξαγωγή Δεδομένων Απογραφής";
            this.Load += new System.EventHandler(this.FrmExportInventory_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.FrmExportInventory_KeyDown);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label LBExpInvRows;
        private System.Windows.Forms.Button BtnSyncData;
        private System.Windows.Forms.Label LBExportInventory;
        private System.Windows.Forms.PictureBox ImgExportInventory;
        private System.Windows.Forms.PictureBox PBBtnBck;
        private System.Windows.Forms.PictureBox PBMenuBar;
    }
}