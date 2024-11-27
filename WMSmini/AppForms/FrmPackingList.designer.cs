namespace WMSMobileClient
{
    partial class FrmPackingList
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmPackingList));
            this.mainMenu1 = new System.Windows.Forms.MainMenu();
            this.TBLotCode = new System.Windows.Forms.TextBox();
            this.LBLotCode = new System.Windows.Forms.Label();
            this.TBItemCode = new System.Windows.Forms.TextBox();
            this.LBItemCode = new System.Windows.Forms.Label();
            this.LBItemDesc = new System.Windows.Forms.Label();
            this.LBDimensions = new System.Windows.Forms.Label();
            this.LBMunitQty = new System.Windows.Forms.Label();
            this.TBQty = new System.Windows.Forms.TextBox();
            this.LBMsgBox = new System.Windows.Forms.Label();
            this.LBAlterMunit = new System.Windows.Forms.Label();
            this.LBAlterQty = new System.Windows.Forms.Label();
            this.BtnDelete = new System.Windows.Forms.PictureBox();
            this.BtnSave = new System.Windows.Forms.PictureBox();
            this.PBBtnBck = new System.Windows.Forms.PictureBox();
            this.PBoxQty = new System.Windows.Forms.PictureBox();
            this.PBoxITemCode = new System.Windows.Forms.PictureBox();
            this.PBoxBarcode = new System.Windows.Forms.PictureBox();
            this.PBoxLotCode = new System.Windows.Forms.PictureBox();
            this.PBoxMessage = new System.Windows.Forms.PictureBox();
            this.PBMenuBar = new System.Windows.Forms.PictureBox();
            this.BtnSaveFake = new System.Windows.Forms.Button();
            this.LBColor = new System.Windows.Forms.Label();
            this.LBDraft = new System.Windows.Forms.Label();
            this.lbcounter = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // TBLotCode
            // 
            this.TBLotCode.AcceptsReturn = true;
            this.TBLotCode.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(247)))), ((int)(((byte)(247)))));
            this.TBLotCode.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.TBLotCode.Font = new System.Drawing.Font("Tahoma", 13F, System.Drawing.FontStyle.Regular);
            this.TBLotCode.Location = new System.Drawing.Point(13, 20);
            this.TBLotCode.MaxLength = 20;
            this.TBLotCode.Name = "TBLotCode";
            this.TBLotCode.Size = new System.Drawing.Size(160, 27);
            this.TBLotCode.TabIndex = 0;
            this.TBLotCode.TextChanged += new System.EventHandler(this.TBLotCode_TextChanged);
            this.TBLotCode.GotFocus += new System.EventHandler(this.TBLotCode_GotFocus);
            this.TBLotCode.KeyDown += new System.Windows.Forms.KeyEventHandler(this.TBLotCode_KeyDown);
            this.TBLotCode.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.TBLotCode_KeyPress);
            this.TBLotCode.LostFocus += new System.EventHandler(this.TBLotCode_LostFocus);
            // 
            // LBLotCode
            // 
            this.LBLotCode.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Bold);
            this.LBLotCode.Location = new System.Drawing.Point(46, 1);
            this.LBLotCode.Name = "LBLotCode";
            this.LBLotCode.Size = new System.Drawing.Size(100, 13);
            this.LBLotCode.Text = "Παρτίδα";
            // 
            // TBItemCode
            // 
            this.TBItemCode.AcceptsReturn = true;
            this.TBItemCode.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(247)))), ((int)(((byte)(247)))));
            this.TBItemCode.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.TBItemCode.Font = new System.Drawing.Font("Tahoma", 13F, System.Drawing.FontStyle.Regular);
            this.TBItemCode.Location = new System.Drawing.Point(11, 75);
            this.TBItemCode.MaxLength = 20;
            this.TBItemCode.Name = "TBItemCode";
            this.TBItemCode.Size = new System.Drawing.Size(160, 27);
            this.TBItemCode.TabIndex = 5;
            this.TBItemCode.TextChanged += new System.EventHandler(this.TBItemCode_TextChanged);
            this.TBItemCode.GotFocus += new System.EventHandler(this.TBItemCode_GotFocus);
            this.TBItemCode.KeyDown += new System.Windows.Forms.KeyEventHandler(this.TBItemCode_KeyDown);
            this.TBItemCode.LostFocus += new System.EventHandler(this.TBItemCode_LostFocus);
            // 
            // LBItemCode
            // 
            this.LBItemCode.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Bold);
            this.LBItemCode.Location = new System.Drawing.Point(17, 53);
            this.LBItemCode.Name = "LBItemCode";
            this.LBItemCode.Size = new System.Drawing.Size(100, 13);
            this.LBItemCode.Text = "Κωδικός Είδους";
            // 
            // LBItemDesc
            // 
            this.LBItemDesc.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Bold);
            this.LBItemDesc.ForeColor = System.Drawing.Color.DarkSlateGray;
            this.LBItemDesc.Location = new System.Drawing.Point(3, 109);
            this.LBItemDesc.Name = "LBItemDesc";
            this.LBItemDesc.Size = new System.Drawing.Size(234, 26);
            // 
            // LBDimensions
            // 
            this.LBDimensions.Font = new System.Drawing.Font("Tahoma", 10F, System.Drawing.FontStyle.Bold);
            this.LBDimensions.ForeColor = System.Drawing.Color.OrangeRed;
            this.LBDimensions.Location = new System.Drawing.Point(7, 136);
            this.LBDimensions.Name = "LBDimensions";
            this.LBDimensions.Size = new System.Drawing.Size(126, 17);
            // 
            // LBMunitQty
            // 
            this.LBMunitQty.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Bold);
            this.LBMunitQty.Location = new System.Drawing.Point(7, 151);
            this.LBMunitQty.Name = "LBMunitQty";
            this.LBMunitQty.Size = new System.Drawing.Size(100, 11);
            this.LBMunitQty.Text = "Ποσότητα";
            // 
            // TBQty
            // 
            this.TBQty.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(247)))), ((int)(((byte)(247)))));
            this.TBQty.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.TBQty.Font = new System.Drawing.Font("Tahoma", 13F, System.Drawing.FontStyle.Regular);
            this.TBQty.Location = new System.Drawing.Point(9, 170);
            this.TBQty.Name = "TBQty";
            this.TBQty.Size = new System.Drawing.Size(75, 27);
            this.TBQty.TabIndex = 19;
            this.TBQty.TextChanged += new System.EventHandler(this.TBQty_TextChanged);
            this.TBQty.GotFocus += new System.EventHandler(this.TBQty_GotFocus);
            this.TBQty.KeyDown += new System.Windows.Forms.KeyEventHandler(this.TBQty_KeyDown);
            this.TBQty.LostFocus += new System.EventHandler(this.TBQty_LostFocus);
            // 
            // LBMsgBox
            // 
            this.LBMsgBox.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(244)))), ((int)(((byte)(235)))), ((int)(((byte)(163)))));
            this.LBMsgBox.Font = new System.Drawing.Font("Tahoma", 10F, System.Drawing.FontStyle.Bold);
            this.LBMsgBox.ForeColor = System.Drawing.Color.Brown;
            this.LBMsgBox.Location = new System.Drawing.Point(12, 216);
            this.LBMsgBox.Name = "LBMsgBox";
            this.LBMsgBox.Size = new System.Drawing.Size(212, 30);
            this.LBMsgBox.Visible = false;
            // 
            // LBAlterMunit
            // 
            this.LBAlterMunit.Location = new System.Drawing.Point(139, 139);
            this.LBAlterMunit.Name = "LBAlterMunit";
            this.LBAlterMunit.Size = new System.Drawing.Size(30, 13);
            this.LBAlterMunit.Text = "M2";
            this.LBAlterMunit.Visible = false;
            // 
            // LBAlterQty
            // 
            this.LBAlterQty.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.LBAlterQty.Location = new System.Drawing.Point(167, 138);
            this.LBAlterQty.Name = "LBAlterQty";
            this.LBAlterQty.Size = new System.Drawing.Size(59, 20);
            // 
            // BtnDelete
            // 
            this.BtnDelete.Image = ((System.Drawing.Image)(resources.GetObject("BtnDelete.Image")));
            this.BtnDelete.Location = new System.Drawing.Point(95, 276);
            this.BtnDelete.Name = "BtnDelete";
            this.BtnDelete.Size = new System.Drawing.Size(46, 44);
            this.BtnDelete.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.BtnDelete.Click += new System.EventHandler(this.BtnDelete_Click);
            this.BtnDelete.GotFocus += new System.EventHandler(this.BtnDelete_GotFocus);
            this.BtnDelete.MouseDown += new System.Windows.Forms.MouseEventHandler(this.BtnDelete_MouseDown);
            this.BtnDelete.LostFocus += new System.EventHandler(this.BtnDelete_LostFocus);
            this.BtnDelete.MouseUp += new System.Windows.Forms.MouseEventHandler(this.BtnDelete_MouseUp);
            // 
            // BtnSave
            // 
            this.BtnSave.Image = ((System.Drawing.Image)(resources.GetObject("BtnSave.Image")));
            this.BtnSave.Location = new System.Drawing.Point(168, 276);
            this.BtnSave.Name = "BtnSave";
            this.BtnSave.Size = new System.Drawing.Size(64, 44);
            this.BtnSave.Click += new System.EventHandler(this.BtnSave_Click);
            this.BtnSave.GotFocus += new System.EventHandler(this.BtnSave_GotFocus);
            this.BtnSave.MouseDown += new System.Windows.Forms.MouseEventHandler(this.BtnSave_MouseDown);
            this.BtnSave.LostFocus += new System.EventHandler(this.BtnSave_LostFocus);
            this.BtnSave.MouseUp += new System.Windows.Forms.MouseEventHandler(this.BtnSave_MouseUp);
            // 
            // PBBtnBck
            // 
            this.PBBtnBck.Image = ((System.Drawing.Image)(resources.GetObject("PBBtnBck.Image")));
            this.PBBtnBck.Location = new System.Drawing.Point(3, 275);
            this.PBBtnBck.Name = "PBBtnBck";
            this.PBBtnBck.Size = new System.Drawing.Size(64, 44);
            this.PBBtnBck.Click += new System.EventHandler(this.PBBtnBck_Click);
            this.PBBtnBck.GotFocus += new System.EventHandler(this.PBBtnBck_GotFocus);
            this.PBBtnBck.MouseDown += new System.Windows.Forms.MouseEventHandler(this.PBBtnBck_MouseDown);
            this.PBBtnBck.LostFocus += new System.EventHandler(this.PBBtnBck_LostFocus);
            this.PBBtnBck.MouseUp += new System.Windows.Forms.MouseEventHandler(this.PBBtnBck_MouseUp);
            // 
            // PBoxQty
            // 
            this.PBoxQty.Image = ((System.Drawing.Image)(resources.GetObject("PBoxQty.Image")));
            this.PBoxQty.Location = new System.Drawing.Point(2, 164);
            this.PBoxQty.Name = "PBoxQty";
            this.PBoxQty.Size = new System.Drawing.Size(109, 37);
            this.PBoxQty.Click += new System.EventHandler(this.PBoxQty_Click);
            // 
            // PBoxITemCode
            // 
            this.PBoxITemCode.Image = ((System.Drawing.Image)(resources.GetObject("PBoxITemCode.Image")));
            this.PBoxITemCode.Location = new System.Drawing.Point(3, 69);
            this.PBoxITemCode.Name = "PBoxITemCode";
            this.PBoxITemCode.Size = new System.Drawing.Size(234, 37);
            this.PBoxITemCode.Click += new System.EventHandler(this.PBoxITemCode_Click);
            // 
            // PBoxBarcode
            // 
            this.PBoxBarcode.Image = ((System.Drawing.Image)(resources.GetObject("PBoxBarcode.Image")));
            this.PBoxBarcode.Location = new System.Drawing.Point(9, 3);
            this.PBoxBarcode.Name = "PBoxBarcode";
            this.PBoxBarcode.Size = new System.Drawing.Size(33, 10);
            // 
            // PBoxLotCode
            // 
            this.PBoxLotCode.Image = ((System.Drawing.Image)(resources.GetObject("PBoxLotCode.Image")));
            this.PBoxLotCode.Location = new System.Drawing.Point(4, 14);
            this.PBoxLotCode.Name = "PBoxLotCode";
            this.PBoxLotCode.Size = new System.Drawing.Size(234, 37);
            this.PBoxLotCode.Click += new System.EventHandler(this.PBoxLotCode_Click);
            // 
            // PBoxMessage
            // 
            this.PBoxMessage.Image = ((System.Drawing.Image)(resources.GetObject("PBoxMessage.Image")));
            this.PBoxMessage.Location = new System.Drawing.Point(6, 213);
            this.PBoxMessage.Name = "PBoxMessage";
            this.PBoxMessage.Size = new System.Drawing.Size(228, 38);
            this.PBoxMessage.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.PBoxMessage.Visible = false;
            this.PBoxMessage.Click += new System.EventHandler(this.PBoxMessage_Click);
            // 
            // PBMenuBar
            // 
            this.PBMenuBar.Image = ((System.Drawing.Image)(resources.GetObject("PBMenuBar.Image")));
            this.PBMenuBar.Location = new System.Drawing.Point(0, 272);
            this.PBMenuBar.Name = "PBMenuBar";
            this.PBMenuBar.Size = new System.Drawing.Size(241, 50);
            // 
            // BtnSaveFake
            // 
            this.BtnSaveFake.Enabled = false;
            this.BtnSaveFake.Location = new System.Drawing.Point(193, 286);
            this.BtnSaveFake.Name = "BtnSaveFake";
            this.BtnSaveFake.Size = new System.Drawing.Size(31, 20);
            this.BtnSaveFake.TabIndex = 113;
            this.BtnSaveFake.Text = "Save";
            this.BtnSaveFake.Visible = false;
            this.BtnSaveFake.Click += new System.EventHandler(this.BtnSaveFake_Click);
            this.BtnSaveFake.KeyDown += new System.Windows.Forms.KeyEventHandler(this.BtnSaveFake_KeyDown);
            // 
            // LBColor
            // 
            this.LBColor.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.LBColor.Location = new System.Drawing.Point(115, 163);
            this.LBColor.Name = "LBColor";
            this.LBColor.Size = new System.Drawing.Size(124, 12);
            this.LBColor.ParentChanged += new System.EventHandler(this.LBScanned_ParentChanged);
            // 
            // LBDraft
            // 
            this.LBDraft.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.LBDraft.Location = new System.Drawing.Point(115, 175);
            this.LBDraft.Name = "LBDraft";
            this.LBDraft.Size = new System.Drawing.Size(124, 13);
            // 
            // lbcounter
            // 
            this.lbcounter.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.lbcounter.ForeColor = System.Drawing.Color.Maroon;
            this.lbcounter.Location = new System.Drawing.Point(115, 194);
            this.lbcounter.Name = "lbcounter";
            this.lbcounter.Size = new System.Drawing.Size(124, 14);
            // 
            // FrmPackingList
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(240, 320);
            this.ControlBox = false;
            this.Controls.Add(this.lbcounter);
            this.Controls.Add(this.LBDraft);
            this.Controls.Add(this.BtnSave);
            this.Controls.Add(this.BtnSaveFake);
            this.Controls.Add(this.BtnDelete);
            this.Controls.Add(this.PBBtnBck);
            this.Controls.Add(this.LBColor);
            this.Controls.Add(this.LBAlterQty);
            this.Controls.Add(this.LBAlterMunit);
            this.Controls.Add(this.LBMsgBox);
            this.Controls.Add(this.TBQty);
            this.Controls.Add(this.LBMunitQty);
            this.Controls.Add(this.PBoxQty);
            this.Controls.Add(this.LBDimensions);
            this.Controls.Add(this.LBItemDesc);
            this.Controls.Add(this.LBItemCode);
            this.Controls.Add(this.TBItemCode);
            this.Controls.Add(this.PBoxITemCode);
            this.Controls.Add(this.PBoxBarcode);
            this.Controls.Add(this.LBLotCode);
            this.Controls.Add(this.TBLotCode);
            this.Controls.Add(this.PBoxLotCode);
            this.Controls.Add(this.PBoxMessage);
            this.Controls.Add(this.PBMenuBar);
            this.Name = "FrmPackingList";
            this.Text = "Εκτέλεση";
            this.Load += new System.EventHandler(this.FrmInventory_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.FrmInventory_KeyDown);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TextBox TBLotCode;
        private System.Windows.Forms.PictureBox PBoxLotCode;
        private System.Windows.Forms.Label LBLotCode;
        private System.Windows.Forms.PictureBox PBoxBarcode;
        private System.Windows.Forms.PictureBox PBoxITemCode;
        private System.Windows.Forms.TextBox TBItemCode;
        private System.Windows.Forms.Label LBItemCode;
        private System.Windows.Forms.Label LBItemDesc;
        private System.Windows.Forms.Label LBDimensions;
        private System.Windows.Forms.PictureBox PBoxQty;
        private System.Windows.Forms.Label LBMunitQty;
        private System.Windows.Forms.TextBox TBQty;
        private System.Windows.Forms.Label LBMsgBox;
        private System.Windows.Forms.PictureBox PBoxMessage;
        private System.Windows.Forms.Label LBAlterMunit;
        private System.Windows.Forms.Label LBAlterQty;
        private System.Windows.Forms.PictureBox BtnSave;
        private System.Windows.Forms.PictureBox PBBtnBck;
        private System.Windows.Forms.PictureBox PBMenuBar;
        private System.Windows.Forms.PictureBox BtnDelete;
        private System.Windows.Forms.Button BtnSaveFake;
        private System.Windows.Forms.Label LBColor;
        private System.Windows.Forms.Label LBDraft;
        private System.Windows.Forms.Label lbcounter;
    }
}