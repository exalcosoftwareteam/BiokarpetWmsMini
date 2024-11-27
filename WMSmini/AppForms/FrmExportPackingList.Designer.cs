namespace WMSMobileClient
{
    partial class FrmExportPackingList
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmExportPackingList));
            this.mainMenu1 = new System.Windows.Forms.MainMenu();
            this.LBExpInvRows = new System.Windows.Forms.Label();
            this.BtnSyncData = new System.Windows.Forms.Button();
            this.LBExportInventory = new System.Windows.Forms.Label();
            this.ImgExportInventory = new System.Windows.Forms.PictureBox();
            this.PBBtnBck = new System.Windows.Forms.PictureBox();
            this.PBMenuBar = new System.Windows.Forms.PictureBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.TBTransType = new System.Windows.Forms.TextBox();
            this.PBoxTransType = new System.Windows.Forms.PictureBox();
            this.TBCustomerCode = new System.Windows.Forms.TextBox();
            this.PBoxCustomerCode = new System.Windows.Forms.PictureBox();
            this.button1 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // LBExpInvRows
            // 
            this.LBExpInvRows.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Bold);
            this.LBExpInvRows.ForeColor = System.Drawing.Color.Chartreuse;
            this.LBExpInvRows.Location = new System.Drawing.Point(3, 179);
            this.LBExpInvRows.Name = "LBExpInvRows";
            this.LBExpInvRows.Size = new System.Drawing.Size(179, 37);
            this.LBExpInvRows.Text = "Εξαγωγή";
            this.LBExpInvRows.Visible = false;
            // 
            // BtnSyncData
            // 
            this.BtnSyncData.BackColor = System.Drawing.Color.GreenYellow;
            this.BtnSyncData.Location = new System.Drawing.Point(15, 24);
            this.BtnSyncData.Name = "BtnSyncData";
            this.BtnSyncData.Size = new System.Drawing.Size(218, 48);
            this.BtnSyncData.TabIndex = 48;
            this.BtnSyncData.Text = "Εξαγωγή Διακίνησης";
            this.BtnSyncData.Click += new System.EventHandler(this.BtnSyncData_Click);
            // 
            // LBExportInventory
            // 
            this.LBExportInventory.Location = new System.Drawing.Point(3, 162);
            this.LBExportInventory.Name = "LBExportInventory";
            this.LBExportInventory.Size = new System.Drawing.Size(163, 16);
            this.LBExportInventory.Text = "Εξαγωγή Διακίνησης...";
            // 
            // ImgExportInventory
            // 
            this.ImgExportInventory.Image = ((System.Drawing.Image)(resources.GetObject("ImgExportInventory.Image")));
            this.ImgExportInventory.Location = new System.Drawing.Point(185, 158);
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
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(15, 17);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(95, 16);
            this.label1.Text = "Κωδ.Κίνησης";
            this.label1.Visible = false;
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(23, 21);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(79, 16);
            this.label2.Text = "Κωδ.Πελάτη";
            this.label2.Visible = false;
            // 
            // TBTransType
            // 
            this.TBTransType.AcceptsReturn = true;
            this.TBTransType.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(247)))), ((int)(((byte)(247)))));
            this.TBTransType.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.TBTransType.Font = new System.Drawing.Font("Tahoma", 13F, System.Drawing.FontStyle.Regular);
            this.TBTransType.Location = new System.Drawing.Point(108, 12);
            this.TBTransType.MaxLength = 20;
            this.TBTransType.Name = "TBTransType";
            this.TBTransType.Size = new System.Drawing.Size(74, 27);
            this.TBTransType.TabIndex = 55;
            this.TBTransType.Visible = false;
            this.TBTransType.GotFocus += new System.EventHandler(this.TBTransType_GotFocus);
            this.TBTransType.LostFocus += new System.EventHandler(this.TBTransType_LostFocus);
            // 
            // PBoxTransType
            // 
            this.PBoxTransType.Image = ((System.Drawing.Image)(resources.GetObject("PBoxTransType.Image")));
            this.PBoxTransType.Location = new System.Drawing.Point(100, 7);
            this.PBoxTransType.Name = "PBoxTransType";
            this.PBoxTransType.Size = new System.Drawing.Size(113, 33);
            this.PBoxTransType.Visible = false;
            // 
            // TBCustomerCode
            // 
            this.TBCustomerCode.AcceptsReturn = true;
            this.TBCustomerCode.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(247)))), ((int)(((byte)(247)))));
            this.TBCustomerCode.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.TBCustomerCode.Font = new System.Drawing.Font("Tahoma", 13F, System.Drawing.FontStyle.Regular);
            this.TBCustomerCode.Location = new System.Drawing.Point(116, 13);
            this.TBCustomerCode.MaxLength = 20;
            this.TBCustomerCode.Name = "TBCustomerCode";
            this.TBCustomerCode.Size = new System.Drawing.Size(74, 27);
            this.TBCustomerCode.TabIndex = 58;
            this.TBCustomerCode.Visible = false;
            this.TBCustomerCode.GotFocus += new System.EventHandler(this.TBCustomerCode_GotFocus);
            this.TBCustomerCode.LostFocus += new System.EventHandler(this.TBCustomerCode_LostFocus);
            // 
            // PBoxCustomerCode
            // 
            this.PBoxCustomerCode.Image = ((System.Drawing.Image)(resources.GetObject("PBoxCustomerCode.Image")));
            this.PBoxCustomerCode.Location = new System.Drawing.Point(108, 8);
            this.PBoxCustomerCode.Name = "PBoxCustomerCode";
            this.PBoxCustomerCode.Size = new System.Drawing.Size(113, 33);
            this.PBoxCustomerCode.Visible = false;
            // 
            // button1
            // 
            this.button1.BackColor = System.Drawing.Color.GreenYellow;
            this.button1.Location = new System.Drawing.Point(15, 85);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(218, 48);
            this.button1.TabIndex = 68;
            this.button1.Text = "Δημιουργία Παραστατικού";
            this.button1.Visible = false;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // FrmExportPackingList
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.ClientSize = new System.Drawing.Size(240, 320);
            this.ControlBox = false;
            this.Controls.Add(this.button1);
            this.Controls.Add(this.BtnSyncData);
            this.Controls.Add(this.TBCustomerCode);
            this.Controls.Add(this.PBoxCustomerCode);
            this.Controls.Add(this.TBTransType);
            this.Controls.Add(this.PBoxTransType);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.PBBtnBck);
            this.Controls.Add(this.PBMenuBar);
            this.Controls.Add(this.ImgExportInventory);
            this.Controls.Add(this.LBExpInvRows);
            this.Controls.Add(this.LBExportInventory);
            this.Name = "FrmExportPackingList";
            this.Text = "Εξαγωγή";
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
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox TBTransType;
        private System.Windows.Forms.PictureBox PBoxTransType;
        private System.Windows.Forms.TextBox TBCustomerCode;
        private System.Windows.Forms.PictureBox PBoxCustomerCode;
        private System.Windows.Forms.Button button1;
    }
}