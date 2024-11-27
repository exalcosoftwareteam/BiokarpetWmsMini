namespace WMSMobileClient
{
    partial class FrmPackingListView
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmPackingListView));
            this.mainMenu1 = new System.Windows.Forms.MainMenu();
            this.LBMsgBox = new System.Windows.Forms.Label();
            this.DGPackingItemsList = new System.Windows.Forms.DataGrid();
            this.CkBoxLastRecords = new System.Windows.Forms.CheckBox();
            this.LBLastRecords = new System.Windows.Forms.Label();
            this.TBSearch = new System.Windows.Forms.TextBox();
            this.LBSearch = new System.Windows.Forms.Label();
            this.BtnDelete = new System.Windows.Forms.PictureBox();
            this.PBBtnBck = new System.Windows.Forms.PictureBox();
            this.PBMenuBar = new System.Windows.Forms.PictureBox();
            this.PBoxMessage = new System.Windows.Forms.PictureBox();
            this.PBoxSearch = new System.Windows.Forms.PictureBox();
            this.BtnView = new System.Windows.Forms.PictureBox();
            this.BtnSync = new System.Windows.Forms.PictureBox();
            this.SuspendLayout();
            // 
            // LBMsgBox
            // 
            this.LBMsgBox.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(244)))), ((int)(((byte)(235)))), ((int)(((byte)(163)))));
            this.LBMsgBox.Font = new System.Drawing.Font("Tahoma", 10F, System.Drawing.FontStyle.Bold);
            this.LBMsgBox.ForeColor = System.Drawing.Color.Brown;
            this.LBMsgBox.Location = new System.Drawing.Point(12, 205);
            this.LBMsgBox.Name = "LBMsgBox";
            this.LBMsgBox.Size = new System.Drawing.Size(212, 30);
            this.LBMsgBox.Visible = false;
            // 
            // DGPackingItemsList
            // 
            this.DGPackingItemsList.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            this.DGPackingItemsList.Location = new System.Drawing.Point(12, 35);
            this.DGPackingItemsList.Name = "DGPackingItemsList";
            this.DGPackingItemsList.Size = new System.Drawing.Size(216, 203);
            this.DGPackingItemsList.TabIndex = 3;
            this.DGPackingItemsList.DoubleClick += new System.EventHandler(this.DGPackingItemsList_DoubleClick);
            this.DGPackingItemsList.CurrentCellChanged += new System.EventHandler(this.DGInventorytemsList_CurrentCellChanged);
            this.DGPackingItemsList.KeyDown += new System.Windows.Forms.KeyEventHandler(this.DGInventorytemsList_KeyDown);
            // 
            // CkBoxLastRecords
            // 
            this.CkBoxLastRecords.Font = new System.Drawing.Font("Tahoma", 7F, System.Drawing.FontStyle.Regular);
            this.CkBoxLastRecords.Location = new System.Drawing.Point(208, 12);
            this.CkBoxLastRecords.Name = "CkBoxLastRecords";
            this.CkBoxLastRecords.Size = new System.Drawing.Size(25, 20);
            this.CkBoxLastRecords.TabIndex = 67;
            this.CkBoxLastRecords.CheckStateChanged += new System.EventHandler(this.CkBoxLastRecords_CheckStateChanged);
            // 
            // LBLastRecords
            // 
            this.LBLastRecords.Font = new System.Drawing.Font("Tahoma", 7F, System.Drawing.FontStyle.Regular);
            this.LBLastRecords.Location = new System.Drawing.Point(133, 2);
            this.LBLastRecords.Name = "LBLastRecords";
            this.LBLastRecords.Size = new System.Drawing.Size(100, 10);
            this.LBLastRecords.Text = "Τελευτ. 10 εγγραφές";
            // 
            // TBSearch
            // 
            this.TBSearch.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(247)))), ((int)(((byte)(247)))));
            this.TBSearch.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.TBSearch.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.TBSearch.Location = new System.Drawing.Point(15, 13);
            this.TBSearch.Name = "TBSearch";
            this.TBSearch.Size = new System.Drawing.Size(104, 19);
            this.TBSearch.TabIndex = 69;
            this.TBSearch.TextChanged += new System.EventHandler(this.TBSearch_TextChanged);
            this.TBSearch.GotFocus += new System.EventHandler(this.TBSearch_GotFocus);
            this.TBSearch.KeyDown += new System.Windows.Forms.KeyEventHandler(this.TBSearch_KeyDown);
            this.TBSearch.LostFocus += new System.EventHandler(this.TBSearch_LostFocus);
            // 
            // LBSearch
            // 
            this.LBSearch.Font = new System.Drawing.Font("Tahoma", 7F, System.Drawing.FontStyle.Regular);
            this.LBSearch.Location = new System.Drawing.Point(15, 0);
            this.LBSearch.Name = "LBSearch";
            this.LBSearch.Size = new System.Drawing.Size(100, 10);
            this.LBSearch.Text = "Κωδικός/Παρτίδα";
            // 
            // BtnDelete
            // 
            this.BtnDelete.Image = ((System.Drawing.Image)(resources.GetObject("BtnDelete.Image")));
            this.BtnDelete.Location = new System.Drawing.Point(75, 275);
            this.BtnDelete.Name = "BtnDelete";
            this.BtnDelete.Size = new System.Drawing.Size(46, 44);
            this.BtnDelete.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.BtnDelete.Click += new System.EventHandler(this.BtnDelete_Click);
            // 
            // PBBtnBck
            // 
            this.PBBtnBck.Image = ((System.Drawing.Image)(resources.GetObject("PBBtnBck.Image")));
            this.PBBtnBck.Location = new System.Drawing.Point(1, 274);
            this.PBBtnBck.Name = "PBBtnBck";
            this.PBBtnBck.Size = new System.Drawing.Size(64, 44);
            this.PBBtnBck.Click += new System.EventHandler(this.PBBtnBck_Click);
            // 
            // PBMenuBar
            // 
            this.PBMenuBar.Image = ((System.Drawing.Image)(resources.GetObject("PBMenuBar.Image")));
            this.PBMenuBar.Location = new System.Drawing.Point(-1, 270);
            this.PBMenuBar.Name = "PBMenuBar";
            this.PBMenuBar.Size = new System.Drawing.Size(241, 50);
            // 
            // PBoxMessage
            // 
            this.PBoxMessage.Image = ((System.Drawing.Image)(resources.GetObject("PBoxMessage.Image")));
            this.PBoxMessage.Location = new System.Drawing.Point(6, 201);
            this.PBoxMessage.Name = "PBoxMessage";
            this.PBoxMessage.Size = new System.Drawing.Size(228, 38);
            this.PBoxMessage.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.PBoxMessage.Visible = false;
            // 
            // PBoxSearch
            // 
            this.PBoxSearch.Image = ((System.Drawing.Image)(resources.GetObject("PBoxSearch.Image")));
            this.PBoxSearch.Location = new System.Drawing.Point(10, 11);
            this.PBoxSearch.Name = "PBoxSearch";
            this.PBoxSearch.Size = new System.Drawing.Size(111, 23);
            this.PBoxSearch.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            // 
            // BtnView
            // 
            this.BtnView.Image = ((System.Drawing.Image)(resources.GetObject("BtnView.Image")));
            this.BtnView.Location = new System.Drawing.Point(135, 274);
            this.BtnView.Name = "BtnView";
            this.BtnView.Size = new System.Drawing.Size(46, 44);
            this.BtnView.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.BtnView.Click += new System.EventHandler(this.BtnView_Click);
            // 
            // BtnSync
            // 
            this.BtnSync.Image = ((System.Drawing.Image)(resources.GetObject("BtnSync.Image")));
            this.BtnSync.Location = new System.Drawing.Point(190, 273);
            this.BtnSync.Name = "BtnSync";
            this.BtnSync.Size = new System.Drawing.Size(46, 44);
            this.BtnSync.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.BtnSync.Click += new System.EventHandler(this.BtnSync_Click);
            // 
            // FrmPackingListView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.ClientSize = new System.Drawing.Size(240, 320);
            this.Controls.Add(this.BtnView);
            this.Controls.Add(this.BtnSync);
            this.Controls.Add(this.BtnDelete);
            this.Controls.Add(this.PBBtnBck);
            this.Controls.Add(this.PBMenuBar);
            this.Controls.Add(this.LBSearch);
            this.Controls.Add(this.TBSearch);
            this.Controls.Add(this.LBLastRecords);
            this.Controls.Add(this.CkBoxLastRecords);
            this.Controls.Add(this.LBMsgBox);
            this.Controls.Add(this.PBoxMessage);
            this.Controls.Add(this.DGPackingItemsList);
            this.Controls.Add(this.PBoxSearch);
            this.Name = "FrmPackingListView";
            this.Text = "Προβολή";
            this.Load += new System.EventHandler(this.FrmInventoryView_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.FrmInventoryView_KeyDown);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label LBMsgBox;
        private System.Windows.Forms.PictureBox PBoxMessage;
        private System.Windows.Forms.DataGrid DGPackingItemsList;
        private System.Windows.Forms.CheckBox CkBoxLastRecords;
        private System.Windows.Forms.Label LBLastRecords;
        private System.Windows.Forms.TextBox TBSearch;
        private System.Windows.Forms.Label LBSearch;
        private System.Windows.Forms.PictureBox PBoxSearch;
        private System.Windows.Forms.PictureBox PBBtnBck;
        private System.Windows.Forms.PictureBox PBMenuBar;
        private System.Windows.Forms.PictureBox BtnDelete;
        private System.Windows.Forms.PictureBox BtnView;
        private System.Windows.Forms.PictureBox BtnSync;
    }
}