namespace WMSMobileClient
{
    partial class FrmSelectInventoryHeaderOnline
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmSelectInventoryHeaderOnline));
            this.mainMenu1 = new System.Windows.Forms.MainMenu();
            this.DGInvHeaderList = new System.Windows.Forms.DataGrid();
            this.LBSelectInventory = new System.Windows.Forms.Label();
            this.LBMsgBox = new System.Windows.Forms.Label();
            this.PBoxMessage = new System.Windows.Forms.PictureBox();
            this.PBMenuBar = new System.Windows.Forms.PictureBox();
            this.PBBtnBck = new System.Windows.Forms.PictureBox();
            this.PBBtnEnter = new System.Windows.Forms.PictureBox();
            this.BtnView = new System.Windows.Forms.PictureBox();
            this.BtnNewInventory = new System.Windows.Forms.PictureBox();
            this.SuspendLayout();
            // 
            // DGInvHeaderList
            // 
            this.DGInvHeaderList.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            this.DGInvHeaderList.Location = new System.Drawing.Point(12, 20);
            this.DGInvHeaderList.Name = "DGInvHeaderList";
            this.DGInvHeaderList.Size = new System.Drawing.Size(216, 169);
            this.DGInvHeaderList.TabIndex = 0;
            this.DGInvHeaderList.LostFocus += new System.EventHandler(this.DGInvHeaderList_LostFocus);
            this.DGInvHeaderList.CurrentCellChanged += new System.EventHandler(this.DGInvHeaderList_CurrentCellChanged);
            this.DGInvHeaderList.GotFocus += new System.EventHandler(this.DGInvHeaderList_GotFocus);
            this.DGInvHeaderList.KeyDown += new System.Windows.Forms.KeyEventHandler(this.DGInvHeaderList_KeyDown);
            // 
            // LBSelectInventory
            // 
            this.LBSelectInventory.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.LBSelectInventory.Location = new System.Drawing.Point(12, 3);
            this.LBSelectInventory.Name = "LBSelectInventory";
            this.LBSelectInventory.Size = new System.Drawing.Size(152, 14);
            this.LBSelectInventory.Text = "Επιλογή Εργασίας Απογραφής";
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
            // BtnNewInventory
            // 
            this.BtnNewInventory.Image = ((System.Drawing.Image)(resources.GetObject("BtnNewInventory.Image")));
            this.BtnNewInventory.Location = new System.Drawing.Point(69, 273);
            this.BtnNewInventory.Name = "BtnNewInventory";
            this.BtnNewInventory.Size = new System.Drawing.Size(46, 44);
            this.BtnNewInventory.Click += new System.EventHandler(this.BtnNewInventory_Click);
            this.BtnNewInventory.GotFocus += new System.EventHandler(this.BtnNewInventory_GotFocus);
            this.BtnNewInventory.MouseDown += new System.Windows.Forms.MouseEventHandler(this.BtnNewInventory_MouseDown);
            // 
            // FrmSelectInventoryHeaderOnline
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.ClientSize = new System.Drawing.Size(240, 320);
            this.ControlBox = false;
            this.Controls.Add(this.BtnNewInventory);
            this.Controls.Add(this.BtnView);
            this.Controls.Add(this.PBBtnEnter);
            this.Controls.Add(this.PBBtnBck);
            this.Controls.Add(this.PBMenuBar);
            this.Controls.Add(this.LBMsgBox);
            this.Controls.Add(this.PBoxMessage);
            this.Controls.Add(this.LBSelectInventory);
            this.Controls.Add(this.DGInvHeaderList);
            this.MinimizeBox = false;
            this.Name = "FrmSelectInventoryHeaderOnline";
            this.Text = "Εργασίες Απογραφής";
            this.Load += new System.EventHandler(this.FrmSelectInventoryHeader_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.FrmSelectInventoryHeader_KeyDown);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGrid DGInvHeaderList;
        private System.Windows.Forms.Label LBSelectInventory;
        private System.Windows.Forms.Label LBMsgBox;
        private System.Windows.Forms.PictureBox PBoxMessage;
        private System.Windows.Forms.PictureBox PBMenuBar;
        private System.Windows.Forms.PictureBox PBBtnBck;
        private System.Windows.Forms.PictureBox PBBtnEnter;
        private System.Windows.Forms.PictureBox BtnView;
        private System.Windows.Forms.PictureBox BtnNewInventory;
        
    }
}