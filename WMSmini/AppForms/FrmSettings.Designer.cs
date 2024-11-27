namespace WMSMobileClient
{
    partial class FrmSettings
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmSettings));
            this.mainMenu1 = new System.Windows.Forms.MainMenu();
            this.TBWebService = new System.Windows.Forms.TextBox();
            this.LBWEBService = new System.Windows.Forms.Label();
            this.TBCompID = new System.Windows.Forms.TextBox();
            this.TBBranchID = new System.Windows.Forms.TextBox();
            this.LBBRanchID = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.TBStoreID = new System.Windows.Forms.TextBox();
            this.PBoxStoreID = new System.Windows.Forms.PictureBox();
            this.PBoxBranchID = new System.Windows.Forms.PictureBox();
            this.PBoxCompID = new System.Windows.Forms.PictureBox();
            this.BtnCheckWSConnection = new System.Windows.Forms.Button();
            this.PBoxWSConStatus = new System.Windows.Forms.PictureBox();
            this.LLbUpdate = new System.Windows.Forms.LinkLabel();
            this.LbVersion = new System.Windows.Forms.Label();
            this.PBMenuBar = new System.Windows.Forms.PictureBox();
            this.PBBtnBck = new System.Windows.Forms.PictureBox();
            this.BtnSave = new System.Windows.Forms.PictureBox();
            this.BtnDBSettings = new System.Windows.Forms.PictureBox();
            this.PBSoftKeyb = new System.Windows.Forms.PictureBox();
            this.OnScreenKeyboard = new Microsoft.WindowsCE.Forms.InputPanel(this.components);
            this.LBIpAddress = new System.Windows.Forms.Label();
            this.PBoxWebService = new System.Windows.Forms.PictureBox();
            this.TBSyncWebService = new System.Windows.Forms.TextBox();
            this.PBoxSyncWebService = new System.Windows.Forms.PictureBox();
            this.PBoxWSSyncConStatus = new System.Windows.Forms.PictureBox();
            this.cbStores = new System.Windows.Forms.ComboBox();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.cb_alterinvexport = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // TBWebService
            // 
            this.TBWebService.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(247)))), ((int)(((byte)(247)))));
            this.TBWebService.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.TBWebService.Location = new System.Drawing.Point(13, 62);
            this.TBWebService.Multiline = true;
            this.TBWebService.Name = "TBWebService";
            this.TBWebService.Size = new System.Drawing.Size(193, 16);
            this.TBWebService.TabIndex = 6;
            this.TBWebService.GotFocus += new System.EventHandler(this.TBWebService_GotFocus);
            this.TBWebService.KeyDown += new System.Windows.Forms.KeyEventHandler(this.TBWebService_KeyDown);
            this.TBWebService.LostFocus += new System.EventHandler(this.TBWebService_LostFocus);
            // 
            // LBWEBService
            // 
            this.LBWEBService.Font = new System.Drawing.Font("Tahoma", 7F, System.Drawing.FontStyle.Bold);
            this.LBWEBService.Location = new System.Drawing.Point(5, 48);
            this.LBWEBService.Name = "LBWEBService";
            this.LBWEBService.Size = new System.Drawing.Size(127, 13);
            this.LBWEBService.Text = "# Inventory WEB Service";
            // 
            // TBCompID
            // 
            this.TBCompID.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(247)))), ((int)(((byte)(247)))));
            this.TBCompID.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.TBCompID.Location = new System.Drawing.Point(13, 140);
            this.TBCompID.Multiline = true;
            this.TBCompID.Name = "TBCompID";
            this.TBCompID.Size = new System.Drawing.Size(49, 12);
            this.TBCompID.TabIndex = 21;
            this.TBCompID.GotFocus += new System.EventHandler(this.TBCompID_GotFocus);
            this.TBCompID.KeyDown += new System.Windows.Forms.KeyEventHandler(this.TBCompID_KeyDown);
            this.TBCompID.LostFocus += new System.EventHandler(this.TBCompID_LostFocus);
            // 
            // TBBranchID
            // 
            this.TBBranchID.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(247)))), ((int)(((byte)(247)))));
            this.TBBranchID.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.TBBranchID.Location = new System.Drawing.Point(91, 140);
            this.TBBranchID.Multiline = true;
            this.TBBranchID.Name = "TBBranchID";
            this.TBBranchID.Size = new System.Drawing.Size(49, 12);
            this.TBBranchID.TabIndex = 25;
            this.TBBranchID.GotFocus += new System.EventHandler(this.TBBranchID_GotFocus);
            this.TBBranchID.KeyDown += new System.Windows.Forms.KeyEventHandler(this.TBBranchID_KeyDown);
            this.TBBranchID.LostFocus += new System.EventHandler(this.TBBranchID_LostFocus);
            // 
            // LBBRanchID
            // 
            this.LBBRanchID.Font = new System.Drawing.Font("Tahoma", 7F, System.Drawing.FontStyle.Bold);
            this.LBBRanchID.Location = new System.Drawing.Point(83, 122);
            this.LBBRanchID.Name = "LBBRanchID";
            this.LBBRanchID.Size = new System.Drawing.Size(67, 13);
            this.LBBRanchID.Text = "# Branchid";
            // 
            // label2
            // 
            this.label2.Font = new System.Drawing.Font("Tahoma", 7F, System.Drawing.FontStyle.Bold);
            this.label2.Location = new System.Drawing.Point(13, 122);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(56, 12);
            this.label2.Text = "# Compid";
            // 
            // TBStoreID
            // 
            this.TBStoreID.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(247)))), ((int)(((byte)(247)))));
            this.TBStoreID.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.TBStoreID.Location = new System.Drawing.Point(164, 140);
            this.TBStoreID.Multiline = true;
            this.TBStoreID.Name = "TBStoreID";
            this.TBStoreID.Size = new System.Drawing.Size(43, 12);
            this.TBStoreID.TabIndex = 32;
            this.TBStoreID.GotFocus += new System.EventHandler(this.TBStoreID_GotFocus);
            this.TBStoreID.KeyDown += new System.Windows.Forms.KeyEventHandler(this.TBStoreID_KeyDown);
            this.TBStoreID.LostFocus += new System.EventHandler(this.TBStoreID_LostFocus);
            // 
            // PBoxStoreID
            // 
            this.PBoxStoreID.Image = ((System.Drawing.Image)(resources.GetObject("PBoxStoreID.Image")));
            this.PBoxStoreID.Location = new System.Drawing.Point(159, 137);
            this.PBoxStoreID.Name = "PBoxStoreID";
            this.PBoxStoreID.Size = new System.Drawing.Size(56, 19);
            this.PBoxStoreID.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.PBoxStoreID.Click += new System.EventHandler(this.PBoxStoreID_Click);
            // 
            // PBoxBranchID
            // 
            this.PBoxBranchID.Image = ((System.Drawing.Image)(resources.GetObject("PBoxBranchID.Image")));
            this.PBoxBranchID.Location = new System.Drawing.Point(86, 137);
            this.PBoxBranchID.Name = "PBoxBranchID";
            this.PBoxBranchID.Size = new System.Drawing.Size(56, 19);
            this.PBoxBranchID.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.PBoxBranchID.Click += new System.EventHandler(this.PBoxBranchID_Click);
            // 
            // PBoxCompID
            // 
            this.PBoxCompID.Image = ((System.Drawing.Image)(resources.GetObject("PBoxCompID.Image")));
            this.PBoxCompID.Location = new System.Drawing.Point(7, 137);
            this.PBoxCompID.Name = "PBoxCompID";
            this.PBoxCompID.Size = new System.Drawing.Size(56, 19);
            this.PBoxCompID.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.PBoxCompID.Click += new System.EventHandler(this.PBoxCompID_Click);
            // 
            // BtnCheckWSConnection
            // 
            this.BtnCheckWSConnection.BackColor = System.Drawing.Color.WhiteSmoke;
            this.BtnCheckWSConnection.Font = new System.Drawing.Font("Tahoma", 7F, System.Drawing.FontStyle.Regular);
            this.BtnCheckWSConnection.Location = new System.Drawing.Point(136, 44);
            this.BtnCheckWSConnection.Name = "BtnCheckWSConnection";
            this.BtnCheckWSConnection.Size = new System.Drawing.Size(82, 16);
            this.BtnCheckWSConnection.TabIndex = 69;
            this.BtnCheckWSConnection.Text = "Έλεγχος Σύνδεσης";
            this.BtnCheckWSConnection.Click += new System.EventHandler(this.BtnCheckWSConnection_Click);
            this.BtnCheckWSConnection.KeyDown += new System.Windows.Forms.KeyEventHandler(this.BtnCheckWSConnection_KeyDown);
            // 
            // PBoxWSConStatus
            // 
            this.PBoxWSConStatus.Location = new System.Drawing.Point(216, 59);
            this.PBoxWSConStatus.Name = "PBoxWSConStatus";
            this.PBoxWSConStatus.Size = new System.Drawing.Size(22, 21);
            this.PBoxWSConStatus.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.PBoxWSConStatus.Visible = false;
            // 
            // LLbUpdate
            // 
            this.LLbUpdate.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Underline);
            this.LLbUpdate.Location = new System.Drawing.Point(6, 202);
            this.LLbUpdate.Name = "LLbUpdate";
            this.LLbUpdate.Size = new System.Drawing.Size(101, 14);
            this.LLbUpdate.TabIndex = 78;
            this.LLbUpdate.Text = "Download Update";
            this.LLbUpdate.Click += new System.EventHandler(this.LLbUpdate_Click);
            // 
            // LbVersion
            // 
            this.LbVersion.Font = new System.Drawing.Font("Tahoma", 7F, System.Drawing.FontStyle.Regular);
            this.LbVersion.ForeColor = System.Drawing.Color.Black;
            this.LbVersion.Location = new System.Drawing.Point(6, 192);
            this.LbVersion.Name = "LbVersion";
            this.LbVersion.Size = new System.Drawing.Size(100, 10);
            this.LbVersion.Text = "Version 2.1 17072023";
            // 
            // PBMenuBar
            // 
            this.PBMenuBar.Image = ((System.Drawing.Image)(resources.GetObject("PBMenuBar.Image")));
            this.PBMenuBar.Location = new System.Drawing.Point(-1, 270);
            this.PBMenuBar.Name = "PBMenuBar";
            this.PBMenuBar.Size = new System.Drawing.Size(241, 50);
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
            // BtnSave
            // 
            this.BtnSave.Image = ((System.Drawing.Image)(resources.GetObject("BtnSave.Image")));
            this.BtnSave.Location = new System.Drawing.Point(165, 273);
            this.BtnSave.Name = "BtnSave";
            this.BtnSave.Size = new System.Drawing.Size(64, 44);
            this.BtnSave.Click += new System.EventHandler(this.BtnSave_Click);
            this.BtnSave.GotFocus += new System.EventHandler(this.BtnSave_GotFocus);
            this.BtnSave.MouseDown += new System.Windows.Forms.MouseEventHandler(this.BtnSave_MouseDown);
            this.BtnSave.LostFocus += new System.EventHandler(this.BtnSave_LostFocus);
            this.BtnSave.MouseUp += new System.Windows.Forms.MouseEventHandler(this.BtnSave_MouseUp);
            // 
            // BtnDBSettings
            // 
            this.BtnDBSettings.Image = ((System.Drawing.Image)(resources.GetObject("BtnDBSettings.Image")));
            this.BtnDBSettings.Location = new System.Drawing.Point(179, 197);
            this.BtnDBSettings.Name = "BtnDBSettings";
            this.BtnDBSettings.Size = new System.Drawing.Size(50, 50);
            this.BtnDBSettings.Click += new System.EventHandler(this.BtnDBSettings_Click);
            this.BtnDBSettings.GotFocus += new System.EventHandler(this.BtnDBSettings_GotFocus);
            this.BtnDBSettings.MouseDown += new System.Windows.Forms.MouseEventHandler(this.BtnDBSettings_MouseDown);
            this.BtnDBSettings.LostFocus += new System.EventHandler(this.BtnDBSettings_LostFocus);
            this.BtnDBSettings.MouseUp += new System.Windows.Forms.MouseEventHandler(this.BtnDBSettings_MouseUp);
            // 
            // PBSoftKeyb
            // 
            this.PBSoftKeyb.Image = ((System.Drawing.Image)(resources.GetObject("PBSoftKeyb.Image")));
            this.PBSoftKeyb.Location = new System.Drawing.Point(213, 5);
            this.PBSoftKeyb.Name = "PBSoftKeyb";
            this.PBSoftKeyb.Size = new System.Drawing.Size(21, 10);
            this.PBSoftKeyb.Click += new System.EventHandler(this.PBSoftKeyb_Click);
            // 
            // LBIpAddress
            // 
            this.LBIpAddress.Font = new System.Drawing.Font("Tahoma", 7F, System.Drawing.FontStyle.Regular);
            this.LBIpAddress.ForeColor = System.Drawing.Color.DarkSeaGreen;
            this.LBIpAddress.Location = new System.Drawing.Point(10, 179);
            this.LBIpAddress.Name = "LBIpAddress";
            this.LBIpAddress.Size = new System.Drawing.Size(116, 10);
            // 
            // PBoxWebService
            // 
            this.PBoxWebService.Image = ((System.Drawing.Image)(resources.GetObject("PBoxWebService.Image")));
            this.PBoxWebService.Location = new System.Drawing.Point(7, 61);
            this.PBoxWebService.Name = "PBoxWebService";
            this.PBoxWebService.Size = new System.Drawing.Size(227, 19);
            this.PBoxWebService.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            // 
            // TBSyncWebService
            // 
            this.TBSyncWebService.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(247)))), ((int)(((byte)(247)))));
            this.TBSyncWebService.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.TBSyncWebService.Location = new System.Drawing.Point(13, 102);
            this.TBSyncWebService.Multiline = true;
            this.TBSyncWebService.Name = "TBSyncWebService";
            this.TBSyncWebService.Size = new System.Drawing.Size(193, 16);
            this.TBSyncWebService.TabIndex = 150;
            // 
            // PBoxSyncWebService
            // 
            this.PBoxSyncWebService.Image = ((System.Drawing.Image)(resources.GetObject("PBoxSyncWebService.Image")));
            this.PBoxSyncWebService.Location = new System.Drawing.Point(7, 100);
            this.PBoxSyncWebService.Name = "PBoxSyncWebService";
            this.PBoxSyncWebService.Size = new System.Drawing.Size(227, 19);
            this.PBoxSyncWebService.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            // 
            // PBoxWSSyncConStatus
            // 
            this.PBoxWSSyncConStatus.Location = new System.Drawing.Point(216, 98);
            this.PBoxWSSyncConStatus.Name = "PBoxWSSyncConStatus";
            this.PBoxWSSyncConStatus.Size = new System.Drawing.Size(22, 21);
            this.PBoxWSSyncConStatus.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.PBoxWSSyncConStatus.Visible = false;
            // 
            // cbStores
            // 
            this.cbStores.Location = new System.Drawing.Point(7, 20);
            this.cbStores.Name = "cbStores";
            this.cbStores.Size = new System.Drawing.Size(221, 22);
            this.cbStores.TabIndex = 2;
            this.cbStores.SelectedIndexChanged += new System.EventHandler(this.cbStores_SelectedIndexChanged);
            this.cbStores.SelectedValueChanged += new System.EventHandler(this.cbStores_SelectedValueChanged);
            // 
            // label7
            // 
            this.label7.Font = new System.Drawing.Font("Tahoma", 7F, System.Drawing.FontStyle.Bold);
            this.label7.Location = new System.Drawing.Point(5, 5);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(190, 12);
            this.label7.Text = "# Επιλογή Υποκαταστήματος";
            // 
            // label8
            // 
            this.label8.Font = new System.Drawing.Font("Tahoma", 7F, System.Drawing.FontStyle.Bold);
            this.label8.Location = new System.Drawing.Point(5, 86);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(162, 13);
            this.label8.Text = "# Atlantis WEB Service";
            // 
            // label1
            // 
            this.label1.Font = new System.Drawing.Font("Tahoma", 7F, System.Drawing.FontStyle.Bold);
            this.label1.Location = new System.Drawing.Point(158, 123);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(63, 13);
            this.label1.Text = "# Storeid";
            // 
            // button1
            // 
            this.button1.BackColor = System.Drawing.Color.WhiteSmoke;
            this.button1.Font = new System.Drawing.Font("Tahoma", 7F, System.Drawing.FontStyle.Regular);
            this.button1.Location = new System.Drawing.Point(10, 221);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(82, 18);
            this.button1.TabIndex = 153;
            this.button1.Text = "Exit Application";
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // cb_alterinvexport
            // 
            this.cb_alterinvexport.Font = new System.Drawing.Font("Tahoma", 7F, System.Drawing.FontStyle.Regular);
            this.cb_alterinvexport.Location = new System.Drawing.Point(7, 159);
            this.cb_alterinvexport.Name = "cb_alterinvexport";
            this.cb_alterinvexport.Size = new System.Drawing.Size(211, 15);
            this.cb_alterinvexport.TabIndex = 201;
            this.cb_alterinvexport.Text = "Εναλλακτική εξαγωγή απογραφής";
            // 
            // FrmSettings
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(240, 320);
            this.ControlBox = false;
            this.Controls.Add(this.cb_alterinvexport);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.cbStores);
            this.Controls.Add(this.PBoxWSSyncConStatus);
            this.Controls.Add(this.LBIpAddress);
            this.Controls.Add(this.PBSoftKeyb);
            this.Controls.Add(this.BtnDBSettings);
            this.Controls.Add(this.BtnSave);
            this.Controls.Add(this.PBBtnBck);
            this.Controls.Add(this.LbVersion);
            this.Controls.Add(this.LLbUpdate);
            this.Controls.Add(this.PBoxWSConStatus);
            this.Controls.Add(this.BtnCheckWSConnection);
            this.Controls.Add(this.TBStoreID);
            this.Controls.Add(this.PBoxStoreID);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.LBBRanchID);
            this.Controls.Add(this.TBBranchID);
            this.Controls.Add(this.PBoxBranchID);
            this.Controls.Add(this.TBCompID);
            this.Controls.Add(this.PBoxCompID);
            this.Controls.Add(this.LBWEBService);
            this.Controls.Add(this.TBWebService);
            this.Controls.Add(this.PBMenuBar);
            this.Controls.Add(this.TBSyncWebService);
            this.Controls.Add(this.PBoxSyncWebService);
            this.Controls.Add(this.PBoxWebService);
            this.Name = "FrmSettings";
            this.Text = "WMS/Mobile - Ρυθμίσεις";
            this.Load += new System.EventHandler(this.FrmSettings_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.FrmSettings_KeyDown);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TextBox TBWebService;
        private System.Windows.Forms.Label LBWEBService;
        private System.Windows.Forms.TextBox TBCompID;
        private System.Windows.Forms.PictureBox PBoxCompID;
        private System.Windows.Forms.PictureBox PBoxBranchID;
        private System.Windows.Forms.TextBox TBBranchID;
        private System.Windows.Forms.Label LBBRanchID;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.PictureBox PBoxStoreID;
        private System.Windows.Forms.TextBox TBStoreID;
        private System.Windows.Forms.Button BtnCheckWSConnection;
        private System.Windows.Forms.PictureBox PBoxWSConStatus;
        private System.Windows.Forms.LinkLabel LLbUpdate;
        private System.Windows.Forms.Label LbVersion;
        private System.Windows.Forms.PictureBox PBMenuBar;
        private System.Windows.Forms.PictureBox PBBtnBck;
        private System.Windows.Forms.PictureBox BtnSave;
        private System.Windows.Forms.PictureBox BtnDBSettings;
        private System.Windows.Forms.PictureBox PBSoftKeyb;
        private Microsoft.WindowsCE.Forms.InputPanel OnScreenKeyboard;
        private System.Windows.Forms.Label LBIpAddress;
        private System.Windows.Forms.PictureBox PBoxWebService;
        private System.Windows.Forms.TextBox TBSyncWebService;
        private System.Windows.Forms.PictureBox PBoxSyncWebService;
        private System.Windows.Forms.PictureBox PBoxWSSyncConStatus;
        private System.Windows.Forms.ComboBox cbStores;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.CheckBox cb_alterinvexport;
    }
}