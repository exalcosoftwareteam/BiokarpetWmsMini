namespace WMSMobileClient
{
    partial class FrmSelectPackingList
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmSelectPackingList));
            this.mainMenu1 = new System.Windows.Forms.MainMenu();
            this.DGPackHeaderList = new System.Windows.Forms.DataGrid();
            this.LBSelectInventory = new System.Windows.Forms.Label();
            this.LBMsgBox = new System.Windows.Forms.Label();
            this.PBoxMessage = new System.Windows.Forms.PictureBox();
            this.label1 = new System.Windows.Forms.Label();
            this.PBMenuBar = new System.Windows.Forms.PictureBox();
            this.PBBtnBck = new System.Windows.Forms.PictureBox();
            this.PBBtnEnter = new System.Windows.Forms.PictureBox();
            this.BtnView = new System.Windows.Forms.PictureBox();
            this.BtnNewPackHeader = new System.Windows.Forms.PictureBox();
            this.PBSync = new System.Windows.Forms.PictureBox();
            this.SuspendLayout();
            // 
            // DGPackHeaderList
            // 
            this.DGPackHeaderList.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            this.DGPackHeaderList.Location = new System.Drawing.Point(12, 60);
            this.DGPackHeaderList.Name = "DGPackHeaderList";
            this.DGPackHeaderList.Size = new System.Drawing.Size(216, 120);
            this.DGPackHeaderList.TabIndex = 0;
            this.DGPackHeaderList.LostFocus += new System.EventHandler(this.DGInvHeaderList_LostFocus);
            this.DGPackHeaderList.CurrentCellChanged += new System.EventHandler(this.DGInvHeaderList_CurrentCellChanged);
            this.DGPackHeaderList.GotFocus += new System.EventHandler(this.DGInvHeaderList_GotFocus);
            this.DGPackHeaderList.KeyDown += new System.Windows.Forms.KeyEventHandler(this.DGInvHeaderList_KeyDown);
            // 
            // LBSelectInventory
            // 
            this.LBSelectInventory.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.LBSelectInventory.Location = new System.Drawing.Point(12, 43);
            this.LBSelectInventory.Name = "LBSelectInventory";
            this.LBSelectInventory.Size = new System.Drawing.Size(152, 14);
            this.LBSelectInventory.Text = "Επιλογή Διακίνησης";
            // 
            // LBMsgBox
            // 
            this.LBMsgBox.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(244)))), ((int)(((byte)(235)))), ((int)(((byte)(163)))));
            this.LBMsgBox.Font = new System.Drawing.Font("Tahoma", 10F, System.Drawing.FontStyle.Bold);
            this.LBMsgBox.ForeColor = System.Drawing.Color.Brown;
            this.LBMsgBox.Location = new System.Drawing.Point(12, 198);
            this.LBMsgBox.Name = "LBMsgBox";
            this.LBMsgBox.Size = new System.Drawing.Size(212, 30);
            this.LBMsgBox.Visible = false;
            // 
            // PBoxMessage
            // 
            this.PBoxMessage.Image = ((System.Drawing.Image)(resources.GetObject("PBoxMessage.Image")));
            this.PBoxMessage.Location = new System.Drawing.Point(6, 195);
            this.PBoxMessage.Name = "PBoxMessage";
            this.PBoxMessage.Size = new System.Drawing.Size(228, 38);
            this.PBoxMessage.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.PBoxMessage.Visible = false;
            this.PBoxMessage.Click += new System.EventHandler(this.PBoxMessage_Click);
            // 
            // label1
            // 
            this.label1.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.label1.ForeColor = System.Drawing.Color.OliveDrab;
            this.label1.Location = new System.Drawing.Point(12, 7);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(152, 14);
            this.label1.Text = "Εισαγωγή σταθερών στοιχείων";
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
            this.PBBtnBck.Location = new System.Drawing.Point(1, 274);
            this.PBBtnBck.Name = "PBBtnBck";
            this.PBBtnBck.Size = new System.Drawing.Size(64, 44);
            this.PBBtnBck.Click += new System.EventHandler(this.PBBtnBck_Click);
            this.PBBtnBck.MouseDown += new System.Windows.Forms.MouseEventHandler(this.PBBtnBck_MouseDown);
            // 
            // PBBtnEnter
            // 
            this.PBBtnEnter.Image = ((System.Drawing.Image)(resources.GetObject("PBBtnEnter.Image")));
            this.PBBtnEnter.Location = new System.Drawing.Point(173, 274);
            this.PBBtnEnter.Name = "PBBtnEnter";
            this.PBBtnEnter.Size = new System.Drawing.Size(64, 44);
            this.PBBtnEnter.Click += new System.EventHandler(this.PBBtnEnter_Click);
            // 
            // BtnView
            // 
            this.BtnView.Image = ((System.Drawing.Image)(resources.GetObject("BtnView.Image")));
            this.BtnView.Location = new System.Drawing.Point(121, 273);
            this.BtnView.Name = "BtnView";
            this.BtnView.Size = new System.Drawing.Size(46, 44);
            this.BtnView.Click += new System.EventHandler(this.BtnView_Click);
            this.BtnView.GotFocus += new System.EventHandler(this.BtnView_GotFocus);
            this.BtnView.MouseDown += new System.Windows.Forms.MouseEventHandler(this.BtnView_MouseDown);
            // 
            // BtnNewPackHeader
            // 
            this.BtnNewPackHeader.Image = ((System.Drawing.Image)(resources.GetObject("BtnNewPackHeader.Image")));
            this.BtnNewPackHeader.Location = new System.Drawing.Point(69, 273);
            this.BtnNewPackHeader.Name = "BtnNewPackHeader";
            this.BtnNewPackHeader.Size = new System.Drawing.Size(46, 44);
            this.BtnNewPackHeader.Click += new System.EventHandler(this.BtnNewInventory_Click);
            this.BtnNewPackHeader.GotFocus += new System.EventHandler(this.BtnNewInventory_GotFocus);
            this.BtnNewPackHeader.MouseDown += new System.Windows.Forms.MouseEventHandler(this.BtnNewInventory_MouseDown);
            // 
            // PBSync
            // 
            this.PBSync.Image = ((System.Drawing.Image)(resources.GetObject("PBSync.Image")));
            this.PBSync.Location = new System.Drawing.Point(192, 6);
            this.PBSync.Name = "PBSync";
            this.PBSync.Size = new System.Drawing.Size(35, 24);
            this.PBSync.Click += new System.EventHandler(this.PBSync_Click);
            // 
            // FrmSelectPackingList
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.ClientSize = new System.Drawing.Size(240, 320);
            this.ControlBox = false;
            this.Controls.Add(this.PBSync);
            this.Controls.Add(this.BtnNewPackHeader);
            this.Controls.Add(this.BtnView);
            this.Controls.Add(this.PBBtnEnter);
            this.Controls.Add(this.PBBtnBck);
            this.Controls.Add(this.PBMenuBar);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.LBMsgBox);
            this.Controls.Add(this.PBoxMessage);
            this.Controls.Add(this.LBSelectInventory);
            this.Controls.Add(this.DGPackHeaderList);
            this.MinimizeBox = false;
            this.Name = "FrmSelectPackingList";
            this.Text = "Διακινήσεις";
            this.Load += new System.EventHandler(this.FrmSelectInventoryHeader_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.FrmSelectInventoryHeader_KeyDown);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGrid DGPackHeaderList;
        private System.Windows.Forms.Label LBSelectInventory;
        private System.Windows.Forms.Label LBMsgBox;
        private System.Windows.Forms.PictureBox PBoxMessage;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.PictureBox PBMenuBar;
        private System.Windows.Forms.PictureBox PBBtnBck;
        private System.Windows.Forms.PictureBox PBBtnEnter;
        private System.Windows.Forms.PictureBox BtnView;
        private System.Windows.Forms.PictureBox BtnNewPackHeader;
        private System.Windows.Forms.PictureBox PBSync;
        
    }
}