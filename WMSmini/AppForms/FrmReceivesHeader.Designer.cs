namespace WMSMobileClient
{
    partial class FrmReceivesHeader
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmReceivesHeader));
            this.mainMenu1 = new System.Windows.Forms.MainMenu();
            this.DGReceivesList = new System.Windows.Forms.DataGrid();
            this.LBSelectInventory = new System.Windows.Forms.Label();
            this.LBMsgBox = new System.Windows.Forms.Label();
            this.PBoxMessage = new System.Windows.Forms.PictureBox();
            this.PBMenuBar = new System.Windows.Forms.PictureBox();
            this.PBBtnBck = new System.Windows.Forms.PictureBox();
            this.PBBtnEnter = new System.Windows.Forms.PictureBox();
            this.BtnView = new System.Windows.Forms.PictureBox();
            this.BtnNewInventory = new System.Windows.Forms.PictureBox();
            this.btn_newtask = new System.Windows.Forms.Button();
            this.tb_ftrid = new System.Windows.Forms.TextBox();
            this.PBoxSearch = new System.Windows.Forms.PictureBox();
            this.SuspendLayout();
            // 
            // DGReceivesList
            // 
            this.DGReceivesList.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            this.DGReceivesList.Location = new System.Drawing.Point(10, 30);
            this.DGReceivesList.Name = "DGReceivesList";
            this.DGReceivesList.Size = new System.Drawing.Size(227, 121);
            this.DGReceivesList.TabIndex = 0;
            this.DGReceivesList.CurrentCellChanged += new System.EventHandler(this.DGInvHeaderList_CurrentCellChanged);
            this.DGReceivesList.GotFocus += new System.EventHandler(this.DGInvHeaderList_GotFocus);
            this.DGReceivesList.KeyDown += new System.Windows.Forms.KeyEventHandler(this.DGInvHeaderList_KeyDown);
            // 
            // LBSelectInventory
            // 
            this.LBSelectInventory.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.LBSelectInventory.Location = new System.Drawing.Point(12, 14);
            this.LBSelectInventory.Name = "LBSelectInventory";
            this.LBSelectInventory.Size = new System.Drawing.Size(152, 13);
            this.LBSelectInventory.Text = "Δελτία αποστολής";
            // 
            // LBMsgBox
            // 
            this.LBMsgBox.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(244)))), ((int)(((byte)(235)))), ((int)(((byte)(163)))));
            this.LBMsgBox.Font = new System.Drawing.Font("Tahoma", 10F, System.Drawing.FontStyle.Bold);
            this.LBMsgBox.ForeColor = System.Drawing.Color.Brown;
            this.LBMsgBox.Location = new System.Drawing.Point(15, 192);
            this.LBMsgBox.Name = "LBMsgBox";
            this.LBMsgBox.Size = new System.Drawing.Size(217, 43);
            this.LBMsgBox.Visible = false;
            // 
            // PBoxMessage
            // 
            this.PBoxMessage.Image = ((System.Drawing.Image)(resources.GetObject("PBoxMessage.Image")));
            this.PBoxMessage.Location = new System.Drawing.Point(12, 188);
            this.PBoxMessage.Name = "PBoxMessage";
            this.PBoxMessage.Size = new System.Drawing.Size(225, 55);
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
            this.BtnView.Visible = false;
            this.BtnView.GotFocus += new System.EventHandler(this.BtnView_GotFocus);
            this.BtnView.MouseDown += new System.Windows.Forms.MouseEventHandler(this.BtnView_MouseDown);
            // 
            // BtnNewInventory
            // 
            this.BtnNewInventory.Image = ((System.Drawing.Image)(resources.GetObject("BtnNewInventory.Image")));
            this.BtnNewInventory.Location = new System.Drawing.Point(69, 273);
            this.BtnNewInventory.Name = "BtnNewInventory";
            this.BtnNewInventory.Size = new System.Drawing.Size(46, 44);
            this.BtnNewInventory.Visible = false;
            // 
            // btn_newtask
            // 
            this.btn_newtask.Font = new System.Drawing.Font("Tahoma", 7F, System.Drawing.FontStyle.Bold);
            this.btn_newtask.Location = new System.Drawing.Point(171, 157);
            this.btn_newtask.Name = "btn_newtask";
            this.btn_newtask.Size = new System.Drawing.Size(66, 20);
            this.btn_newtask.TabIndex = 76;
            this.btn_newtask.Text = "Δημιουργία";
            this.btn_newtask.Click += new System.EventHandler(this.btn_newtask_Click);
            // 
            // tb_ftrid
            // 
            this.tb_ftrid.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(247)))), ((int)(((byte)(247)))));
            this.tb_ftrid.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.tb_ftrid.Location = new System.Drawing.Point(14, 158);
            this.tb_ftrid.Name = "tb_ftrid";
            this.tb_ftrid.Size = new System.Drawing.Size(153, 21);
            this.tb_ftrid.TabIndex = 75;
            this.tb_ftrid.TextChanged += new System.EventHandler(this.tb_ftrid_TextChanged);
            this.tb_ftrid.KeyDown += new System.Windows.Forms.KeyEventHandler(this.tb_ftrid_KeyDown);
            // 
            // PBoxSearch
            // 
            this.PBoxSearch.Image = ((System.Drawing.Image)(resources.GetObject("PBoxSearch.Image")));
            this.PBoxSearch.Location = new System.Drawing.Point(12, 157);
            this.PBoxSearch.Name = "PBoxSearch";
            this.PBoxSearch.Size = new System.Drawing.Size(158, 23);
            this.PBoxSearch.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            // 
            // FrmReceivesHeader
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.ClientSize = new System.Drawing.Size(240, 320);
            this.ControlBox = false;
            this.Controls.Add(this.btn_newtask);
            this.Controls.Add(this.tb_ftrid);
            this.Controls.Add(this.BtnNewInventory);
            this.Controls.Add(this.BtnView);
            this.Controls.Add(this.PBBtnEnter);
            this.Controls.Add(this.PBBtnBck);
            this.Controls.Add(this.PBMenuBar);
            this.Controls.Add(this.LBSelectInventory);
            this.Controls.Add(this.DGReceivesList);
            this.Controls.Add(this.PBoxSearch);
            this.Controls.Add(this.LBMsgBox);
            this.Controls.Add(this.PBoxMessage);
            this.MinimizeBox = false;
            this.Name = "FrmReceivesHeader";
            this.Text = "Παραλαβές";
            this.Load += new System.EventHandler(this.FrmReceivesHeader_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGrid DGReceivesList;
        private System.Windows.Forms.Label LBSelectInventory;
        private System.Windows.Forms.Label LBMsgBox;
        private System.Windows.Forms.PictureBox PBoxMessage;
        private System.Windows.Forms.PictureBox PBMenuBar;
        private System.Windows.Forms.PictureBox PBBtnBck;
        private System.Windows.Forms.PictureBox PBBtnEnter;
        private System.Windows.Forms.PictureBox BtnView;
        private System.Windows.Forms.PictureBox BtnNewInventory;
        private System.Windows.Forms.Button btn_newtask;
        private System.Windows.Forms.TextBox tb_ftrid;
        private System.Windows.Forms.PictureBox PBoxSearch;
        
    }
}