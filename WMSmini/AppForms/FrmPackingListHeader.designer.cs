namespace WMSMobileClient
{
    partial class FrmPackingListHeader
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmPackingListHeader));
            this.mainMenu1 = new System.Windows.Forms.MainMenu();
            this.ΤΒPackListDate = new System.Windows.Forms.TextBox();
            this.LBMunitQty = new System.Windows.Forms.Label();
            this.PBoxPackDate = new System.Windows.Forms.PictureBox();
            this.LBItemCode = new System.Windows.Forms.Label();
            this.LBMsgBox = new System.Windows.Forms.Label();
            this.PBoxMessage = new System.Windows.Forms.PictureBox();
            this.BtnSave = new System.Windows.Forms.PictureBox();
            this.PBBtnBck = new System.Windows.Forms.PictureBox();
            this.PBMenuBar = new System.Windows.Forms.PictureBox();
            this.OnScreenKeyboard = new Microsoft.WindowsCE.Forms.InputPanel(this.components);
            this.PBSoftKeyb = new System.Windows.Forms.PictureBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.TBcustomercode = new System.Windows.Forms.TextBox();
            this.TBtranstype = new System.Windows.Forms.TextBox();
            this.PBoxTransType = new System.Windows.Forms.PictureBox();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.cb_dsrid = new System.Windows.Forms.ComboBox();
            this.TBPackHeaderComments = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // ΤΒPackListDate
            // 
            this.ΤΒPackListDate.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(247)))), ((int)(((byte)(247)))));
            this.ΤΒPackListDate.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.ΤΒPackListDate.Font = new System.Drawing.Font("Tahoma", 13F, System.Drawing.FontStyle.Regular);
            this.ΤΒPackListDate.Location = new System.Drawing.Point(14, 24);
            this.ΤΒPackListDate.Name = "ΤΒPackListDate";
            this.ΤΒPackListDate.Size = new System.Drawing.Size(90, 27);
            this.ΤΒPackListDate.TabIndex = 22;
            this.ΤΒPackListDate.TextChanged += new System.EventHandler(this.ΤΒΙnvDate_TextChanged);
            this.ΤΒPackListDate.GotFocus += new System.EventHandler(this.ΤΒΙnvDate_GotFocus);
            this.ΤΒPackListDate.KeyDown += new System.Windows.Forms.KeyEventHandler(this.ΤΒΙnvDate_KeyDown);
            this.ΤΒPackListDate.LostFocus += new System.EventHandler(this.ΤΒΙnvDate_LostFocus);
            // 
            // LBMunitQty
            // 
            this.LBMunitQty.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Bold);
            this.LBMunitQty.Location = new System.Drawing.Point(15, 5);
            this.LBMunitQty.Name = "LBMunitQty";
            this.LBMunitQty.Size = new System.Drawing.Size(100, 11);
            this.LBMunitQty.Text = "Ημ/νία";
            // 
            // PBoxPackDate
            // 
            this.PBoxPackDate.Image = ((System.Drawing.Image)(resources.GetObject("PBoxPackDate.Image")));
            this.PBoxPackDate.Location = new System.Drawing.Point(7, 18);
            this.PBoxPackDate.Name = "PBoxPackDate";
            this.PBoxPackDate.Size = new System.Drawing.Size(128, 37);
            this.PBoxPackDate.Click += new System.EventHandler(this.PBoxInvDate_Click);
            // 
            // LBItemCode
            // 
            this.LBItemCode.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Bold);
            this.LBItemCode.Location = new System.Drawing.Point(9, 58);
            this.LBItemCode.Name = "LBItemCode";
            this.LBItemCode.Size = new System.Drawing.Size(100, 13);
            this.LBItemCode.Text = "Περιγραφή";
            // 
            // LBMsgBox
            // 
            this.LBMsgBox.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(244)))), ((int)(((byte)(235)))), ((int)(((byte)(163)))));
            this.LBMsgBox.Font = new System.Drawing.Font("Tahoma", 10F, System.Drawing.FontStyle.Bold);
            this.LBMsgBox.ForeColor = System.Drawing.Color.Brown;
            this.LBMsgBox.Location = new System.Drawing.Point(11, 205);
            this.LBMsgBox.Name = "LBMsgBox";
            this.LBMsgBox.Size = new System.Drawing.Size(212, 30);
            this.LBMsgBox.Visible = false;
            // 
            // PBoxMessage
            // 
            this.PBoxMessage.Image = ((System.Drawing.Image)(resources.GetObject("PBoxMessage.Image")));
            this.PBoxMessage.Location = new System.Drawing.Point(5, 202);
            this.PBoxMessage.Name = "PBoxMessage";
            this.PBoxMessage.Size = new System.Drawing.Size(228, 38);
            this.PBoxMessage.Visible = false;
            // 
            // BtnSave
            // 
            this.BtnSave.Image = ((System.Drawing.Image)(resources.GetObject("BtnSave.Image")));
            this.BtnSave.Location = new System.Drawing.Point(172, 273);
            this.BtnSave.Name = "BtnSave";
            this.BtnSave.Size = new System.Drawing.Size(64, 44);
            this.BtnSave.Click += new System.EventHandler(this.BtnSave_Click);
            this.BtnSave.GotFocus += new System.EventHandler(this.BtnSave_GotFocus);
            this.BtnSave.MouseDown += new System.Windows.Forms.MouseEventHandler(this.BtnSave_MouseDown);
            this.BtnSave.LostFocus += new System.EventHandler(this.BtnSave_LostFocus);
            // 
            // PBBtnBck
            // 
            this.PBBtnBck.Image = ((System.Drawing.Image)(resources.GetObject("PBBtnBck.Image")));
            this.PBBtnBck.Location = new System.Drawing.Point(5, 275);
            this.PBBtnBck.Name = "PBBtnBck";
            this.PBBtnBck.Size = new System.Drawing.Size(64, 44);
            this.PBBtnBck.Click += new System.EventHandler(this.PBBtnBck_Click);
            // 
            // PBMenuBar
            // 
            this.PBMenuBar.Image = ((System.Drawing.Image)(resources.GetObject("PBMenuBar.Image")));
            this.PBMenuBar.Location = new System.Drawing.Point(0, 271);
            this.PBMenuBar.Name = "PBMenuBar";
            this.PBMenuBar.Size = new System.Drawing.Size(241, 50);
            // 
            // PBSoftKeyb
            // 
            this.PBSoftKeyb.Image = ((System.Drawing.Image)(resources.GetObject("PBSoftKeyb.Image")));
            this.PBSoftKeyb.Location = new System.Drawing.Point(210, 6);
            this.PBSoftKeyb.Name = "PBSoftKeyb";
            this.PBSoftKeyb.Size = new System.Drawing.Size(21, 10);
            this.PBSoftKeyb.Click += new System.EventHandler(this.PBSoftKeyb_Click);
            // 
            // label1
            // 
            this.label1.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Bold);
            this.label1.Location = new System.Drawing.Point(10, 101);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(100, 13);
            this.label1.Text = "Κωδ. Πελάτη";
            // 
            // label2
            // 
            this.label2.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Bold);
            this.label2.Location = new System.Drawing.Point(10, 146);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(135, 13);
            this.label2.Text = "Σειρά Παραστατικού";
            // 
            // TBcustomercode
            // 
            this.TBcustomercode.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(247)))), ((int)(((byte)(247)))));
            this.TBcustomercode.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.TBcustomercode.Location = new System.Drawing.Point(14, 120);
            this.TBcustomercode.Name = "TBcustomercode";
            this.TBcustomercode.Size = new System.Drawing.Size(160, 21);
            this.TBcustomercode.TabIndex = 36;
            this.TBcustomercode.KeyDown += new System.Windows.Forms.KeyEventHandler(this.TBcustomercode_KeyDown);
            // 
            // TBtranstype
            // 
            this.TBtranstype.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(247)))), ((int)(((byte)(247)))));
            this.TBtranstype.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.TBtranstype.Location = new System.Drawing.Point(205, 165);
            this.TBtranstype.Multiline = true;
            this.TBtranstype.Name = "TBtranstype";
            this.TBtranstype.Size = new System.Drawing.Size(18, 17);
            this.TBtranstype.TabIndex = 39;
            this.TBtranstype.Visible = false;
            // 
            // PBoxTransType
            // 
            this.PBoxTransType.Image = ((System.Drawing.Image)(resources.GetObject("PBoxTransType.Image")));
            this.PBoxTransType.Location = new System.Drawing.Point(8, 118);
            this.PBoxTransType.Name = "PBoxTransType";
            this.PBoxTransType.Size = new System.Drawing.Size(191, 25);
            this.PBoxTransType.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(8, 74);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(191, 25);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            // 
            // pictureBox2
            // 
            this.pictureBox2.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox2.Image")));
            this.pictureBox2.Location = new System.Drawing.Point(202, 140);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(21, 19);
            this.pictureBox2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox2.Visible = false;
            // 
            // cb_dsrid
            // 
            this.cb_dsrid.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(247)))), ((int)(((byte)(247)))));
            this.cb_dsrid.Location = new System.Drawing.Point(8, 162);
            this.cb_dsrid.Name = "cb_dsrid";
            this.cb_dsrid.Size = new System.Drawing.Size(191, 22);
            this.cb_dsrid.TabIndex = 54;
            this.cb_dsrid.SelectedValueChanged += new System.EventHandler(this.cb_dsrid_SelectedValueChanged);
            this.cb_dsrid.KeyDown += new System.Windows.Forms.KeyEventHandler(this.cb_dsrid_KeyDown);
            // 
            // TBPackHeaderComments
            // 
            this.TBPackHeaderComments.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(247)))), ((int)(((byte)(247)))));
            this.TBPackHeaderComments.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.TBPackHeaderComments.Location = new System.Drawing.Point(13, 77);
            this.TBPackHeaderComments.Name = "TBPackHeaderComments";
            this.TBPackHeaderComments.Size = new System.Drawing.Size(160, 21);
            this.TBPackHeaderComments.TabIndex = 69;
            this.TBPackHeaderComments.KeyDown += new System.Windows.Forms.KeyEventHandler(this.TBPackHeaderComments_KeyDown);
            // 
            // FrmPackingListHeader
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.ClientSize = new System.Drawing.Size(240, 320);
            this.ControlBox = false;
            this.Controls.Add(this.cb_dsrid);
            this.Controls.Add(this.TBPackHeaderComments);
            this.Controls.Add(this.TBtranstype);
            this.Controls.Add(this.TBcustomercode);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.PBSoftKeyb);
            this.Controls.Add(this.BtnSave);
            this.Controls.Add(this.PBBtnBck);
            this.Controls.Add(this.PBMenuBar);
            this.Controls.Add(this.LBMsgBox);
            this.Controls.Add(this.PBoxMessage);
            this.Controls.Add(this.LBItemCode);
            this.Controls.Add(this.ΤΒPackListDate);
            this.Controls.Add(this.LBMunitQty);
            this.Controls.Add(this.PBoxPackDate);
            this.Controls.Add(this.pictureBox2);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.PBoxTransType);
            this.KeyPreview = true;
            this.Name = "FrmPackingListHeader";
            this.Text = "Νέα Διακίνηση";
            this.Load += new System.EventHandler(this.FrmInventoryHeader_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.FrmInventoryHeader_KeyDown);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TextBox ΤΒPackListDate;
        private System.Windows.Forms.Label LBMunitQty;
        private System.Windows.Forms.PictureBox PBoxPackDate;
        private System.Windows.Forms.Label LBItemCode;
        private System.Windows.Forms.Label LBMsgBox;
        private System.Windows.Forms.PictureBox PBoxMessage;
        private System.Windows.Forms.PictureBox BtnSave;
        private System.Windows.Forms.PictureBox PBBtnBck;
        private System.Windows.Forms.PictureBox PBMenuBar;
        private Microsoft.WindowsCE.Forms.InputPanel OnScreenKeyboard;
        private System.Windows.Forms.PictureBox PBSoftKeyb;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox TBcustomercode;
        private System.Windows.Forms.TextBox TBtranstype;
        private System.Windows.Forms.PictureBox PBoxTransType;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.PictureBox pictureBox2;
        private System.Windows.Forms.ComboBox cb_dsrid;
        private System.Windows.Forms.TextBox TBPackHeaderComments;
    }
}