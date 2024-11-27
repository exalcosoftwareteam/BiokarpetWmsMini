namespace WMSMobileClient
{
    partial class FrmCreateTradeCode
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmCreateTradeCode));
            this.mainMenu1 = new System.Windows.Forms.MainMenu();
            this.LBExpInvRows = new System.Windows.Forms.Label();
            this.LBExportInventory = new System.Windows.Forms.Label();
            this.ImgExportInventory = new System.Windows.Forms.PictureBox();
            this.PBBtnBck = new System.Windows.Forms.PictureBox();
            this.PBMenuBar = new System.Windows.Forms.PictureBox();
            this.button1 = new System.Windows.Forms.Button();
            this.TBdsrid = new System.Windows.Forms.TextBox();
            this.PBoxTransType = new System.Windows.Forms.PictureBox();
            this.label4 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // LBExpInvRows
            // 
            this.LBExpInvRows.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Bold);
            this.LBExpInvRows.ForeColor = System.Drawing.Color.Chartreuse;
            this.LBExpInvRows.Location = new System.Drawing.Point(15, 235);
            this.LBExpInvRows.Name = "LBExpInvRows";
            this.LBExpInvRows.Size = new System.Drawing.Size(136, 16);
            this.LBExpInvRows.Text = "Εξαγωγή";
            this.LBExpInvRows.Visible = false;
            // 
            // LBExportInventory
            // 
            this.LBExportInventory.Location = new System.Drawing.Point(15, 218);
            this.LBExportInventory.Name = "LBExportInventory";
            this.LBExportInventory.Size = new System.Drawing.Size(136, 16);
            this.LBExportInventory.Text = "Εξαγωγή Packing List...";
            // 
            // ImgExportInventory
            // 
            this.ImgExportInventory.Image = ((System.Drawing.Image)(resources.GetObject("ImgExportInventory.Image")));
            this.ImgExportInventory.Location = new System.Drawing.Point(185, 214);
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
            // button1
            // 
            this.button1.BackColor = System.Drawing.Color.GreenYellow;
            this.button1.Location = new System.Drawing.Point(15, 26);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(218, 48);
            this.button1.TabIndex = 68;
            this.button1.Text = "Δημιουργία Παραστατικού";
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // TBdsrid
            // 
            this.TBdsrid.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(247)))), ((int)(((byte)(247)))));
            this.TBdsrid.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.TBdsrid.Location = new System.Drawing.Point(28, 118);
            this.TBdsrid.Multiline = true;
            this.TBdsrid.Name = "TBdsrid";
            this.TBdsrid.Size = new System.Drawing.Size(72, 12);
            this.TBdsrid.TabIndex = 117;
            // 
            // PBoxTransType
            // 
            this.PBoxTransType.Image = ((System.Drawing.Image)(resources.GetObject("PBoxTransType.Image")));
            this.PBoxTransType.Location = new System.Drawing.Point(18, 114);
            this.PBoxTransType.Name = "PBoxTransType";
            this.PBoxTransType.Size = new System.Drawing.Size(96, 19);
            this.PBoxTransType.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            // 
            // label4
            // 
            this.label4.Font = new System.Drawing.Font("Tahoma", 7F, System.Drawing.FontStyle.Bold);
            this.label4.Location = new System.Drawing.Point(29, 100);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(184, 11);
            this.label4.Text = "# Σειρά παραστατικού (DSRID)";
            // 
            // FrmCreateTradeCode
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.ClientSize = new System.Drawing.Size(240, 320);
            this.ControlBox = false;
            this.Controls.Add(this.TBdsrid);
            this.Controls.Add(this.PBoxTransType);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.PBBtnBck);
            this.Controls.Add(this.PBMenuBar);
            this.Controls.Add(this.ImgExportInventory);
            this.Controls.Add(this.LBExpInvRows);
            this.Controls.Add(this.LBExportInventory);
            this.Name = "FrmCreateTradeCode";
            this.Text = "Εξαγωγή Διακίνησης";
            this.Load += new System.EventHandler(this.FrmCreateTradeCode_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.FrmExportInventory_KeyDown);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label LBExpInvRows;
        private System.Windows.Forms.Label LBExportInventory;
        private System.Windows.Forms.PictureBox ImgExportInventory;
        private System.Windows.Forms.PictureBox PBBtnBck;
        private System.Windows.Forms.PictureBox PBMenuBar;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.TextBox TBdsrid;
        private System.Windows.Forms.PictureBox PBoxTransType;
        private System.Windows.Forms.Label label4;
    }
}