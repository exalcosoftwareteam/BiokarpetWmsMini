namespace WMSMobileClient
{
    partial class FrmInventoryHeader
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmInventoryHeader));
            this.mainMenu1 = new System.Windows.Forms.MainMenu();
            this.ΤΒΙnvDate = new System.Windows.Forms.TextBox();
            this.LBMunitQty = new System.Windows.Forms.Label();
            this.PBoxInvDate = new System.Windows.Forms.PictureBox();
            this.LBItemCode = new System.Windows.Forms.Label();
            this.TBInvHeaderComments = new System.Windows.Forms.TextBox();
            this.PBoxInvHeaderComments = new System.Windows.Forms.PictureBox();
            this.LBMsgBox = new System.Windows.Forms.Label();
            this.PBoxMessage = new System.Windows.Forms.PictureBox();
            this.BtnSave = new System.Windows.Forms.PictureBox();
            this.PBBtnBck = new System.Windows.Forms.PictureBox();
            this.PBMenuBar = new System.Windows.Forms.PictureBox();
            this.OnScreenKeyboard = new Microsoft.WindowsCE.Forms.InputPanel(this.components);
            this.PBSoftKeyb = new System.Windows.Forms.PictureBox();
            this.cb_forcedelete = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // ΤΒΙnvDate
            // 
            this.ΤΒΙnvDate.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(247)))), ((int)(((byte)(247)))));
            this.ΤΒΙnvDate.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.ΤΒΙnvDate.Font = new System.Drawing.Font("Tahoma", 13F, System.Drawing.FontStyle.Regular);
            this.ΤΒΙnvDate.Location = new System.Drawing.Point(12, 30);
            this.ΤΒΙnvDate.Name = "ΤΒΙnvDate";
            this.ΤΒΙnvDate.Size = new System.Drawing.Size(90, 27);
            this.ΤΒΙnvDate.TabIndex = 22;
            this.ΤΒΙnvDate.TextChanged += new System.EventHandler(this.ΤΒΙnvDate_TextChanged);
            this.ΤΒΙnvDate.GotFocus += new System.EventHandler(this.ΤΒΙnvDate_GotFocus);
            this.ΤΒΙnvDate.KeyDown += new System.Windows.Forms.KeyEventHandler(this.ΤΒΙnvDate_KeyDown);
            this.ΤΒΙnvDate.LostFocus += new System.EventHandler(this.ΤΒΙnvDate_LostFocus);
            // 
            // LBMunitQty
            // 
            this.LBMunitQty.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Bold);
            this.LBMunitQty.Location = new System.Drawing.Point(14, 12);
            this.LBMunitQty.Name = "LBMunitQty";
            this.LBMunitQty.Size = new System.Drawing.Size(100, 11);
            this.LBMunitQty.Text = "Ημ/νία";
            // 
            // PBoxInvDate
            // 
            this.PBoxInvDate.Image = ((System.Drawing.Image)(resources.GetObject("PBoxInvDate.Image")));
            this.PBoxInvDate.Location = new System.Drawing.Point(6, 25);
            this.PBoxInvDate.Name = "PBoxInvDate";
            this.PBoxInvDate.Size = new System.Drawing.Size(128, 37);
            this.PBoxInvDate.Click += new System.EventHandler(this.PBoxInvDate_Click);
            // 
            // LBItemCode
            // 
            this.LBItemCode.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Bold);
            this.LBItemCode.Location = new System.Drawing.Point(8, 72);
            this.LBItemCode.Name = "LBItemCode";
            this.LBItemCode.Size = new System.Drawing.Size(100, 13);
            this.LBItemCode.Text = "Περιγραφή";
            // 
            // TBInvHeaderComments
            // 
            this.TBInvHeaderComments.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(247)))), ((int)(((byte)(247)))));
            this.TBInvHeaderComments.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.TBInvHeaderComments.Font = new System.Drawing.Font("Tahoma", 13F, System.Drawing.FontStyle.Regular);
            this.TBInvHeaderComments.Location = new System.Drawing.Point(8, 94);
            this.TBInvHeaderComments.Name = "TBInvHeaderComments";
            this.TBInvHeaderComments.Size = new System.Drawing.Size(160, 27);
            this.TBInvHeaderComments.TabIndex = 27;
            this.TBInvHeaderComments.TextChanged += new System.EventHandler(this.TBInvHeaderComments_TextChanged);
            this.TBInvHeaderComments.GotFocus += new System.EventHandler(this.TBInvHeaderComments_GotFocus);
            this.TBInvHeaderComments.KeyDown += new System.Windows.Forms.KeyEventHandler(this.TBInvHeaderComments_KeyDown);
            this.TBInvHeaderComments.LostFocus += new System.EventHandler(this.TBInvHeaderComments_LostFocus);
            // 
            // PBoxInvHeaderComments
            // 
            this.PBoxInvHeaderComments.Image = ((System.Drawing.Image)(resources.GetObject("PBoxInvHeaderComments.Image")));
            this.PBoxInvHeaderComments.Location = new System.Drawing.Point(3, 88);
            this.PBoxInvHeaderComments.Name = "PBoxInvHeaderComments";
            this.PBoxInvHeaderComments.Size = new System.Drawing.Size(234, 37);
            this.PBoxInvHeaderComments.Click += new System.EventHandler(this.PBoxInvHeaderComments_Click);
            // 
            // LBMsgBox
            // 
            this.LBMsgBox.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(244)))), ((int)(((byte)(235)))), ((int)(((byte)(163)))));
            this.LBMsgBox.Font = new System.Drawing.Font("Tahoma", 10F, System.Drawing.FontStyle.Bold);
            this.LBMsgBox.ForeColor = System.Drawing.Color.Brown;
            this.LBMsgBox.Location = new System.Drawing.Point(10, 181);
            this.LBMsgBox.Name = "LBMsgBox";
            this.LBMsgBox.Size = new System.Drawing.Size(221, 59);
            this.LBMsgBox.Visible = false;
            // 
            // PBoxMessage
            // 
            this.PBoxMessage.Image = ((System.Drawing.Image)(resources.GetObject("PBoxMessage.Image")));
            this.PBoxMessage.Location = new System.Drawing.Point(8, 176);
            this.PBoxMessage.Name = "PBoxMessage";
            this.PBoxMessage.Size = new System.Drawing.Size(228, 67);
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
            // cb_forcedelete
            // 
            this.cb_forcedelete.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.cb_forcedelete.ForeColor = System.Drawing.Color.Red;
            this.cb_forcedelete.Location = new System.Drawing.Point(14, 152);
            this.cb_forcedelete.Name = "cb_forcedelete";
            this.cb_forcedelete.Size = new System.Drawing.Size(217, 20);
            this.cb_forcedelete.TabIndex = 31;
            this.cb_forcedelete.Text = "Επανενημέρωση καταλόγου Atlantis";
            this.cb_forcedelete.Visible = false;
            this.cb_forcedelete.CheckStateChanged += new System.EventHandler(this.checkBox1_CheckStateChanged);
            // 
            // FrmInventoryHeader
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.ClientSize = new System.Drawing.Size(240, 320);
            this.ControlBox = false;
            this.Controls.Add(this.cb_forcedelete);
            this.Controls.Add(this.PBSoftKeyb);
            this.Controls.Add(this.BtnSave);
            this.Controls.Add(this.PBBtnBck);
            this.Controls.Add(this.PBMenuBar);
            this.Controls.Add(this.LBItemCode);
            this.Controls.Add(this.TBInvHeaderComments);
            this.Controls.Add(this.PBoxInvHeaderComments);
            this.Controls.Add(this.ΤΒΙnvDate);
            this.Controls.Add(this.LBMunitQty);
            this.Controls.Add(this.PBoxInvDate);
            this.Controls.Add(this.LBMsgBox);
            this.Controls.Add(this.PBoxMessage);
            this.KeyPreview = true;
            this.Name = "FrmInventoryHeader";
            this.Text = "Εργασία Απογραφής";
            this.Load += new System.EventHandler(this.FrmInventoryHeader_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.FrmInventoryHeader_KeyDown);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TextBox ΤΒΙnvDate;
        private System.Windows.Forms.Label LBMunitQty;
        private System.Windows.Forms.PictureBox PBoxInvDate;
        private System.Windows.Forms.Label LBItemCode;
        private System.Windows.Forms.TextBox TBInvHeaderComments;
        private System.Windows.Forms.PictureBox PBoxInvHeaderComments;
        private System.Windows.Forms.Label LBMsgBox;
        private System.Windows.Forms.PictureBox PBoxMessage;
        private System.Windows.Forms.PictureBox BtnSave;
        private System.Windows.Forms.PictureBox PBBtnBck;
        private System.Windows.Forms.PictureBox PBMenuBar;
        private Microsoft.WindowsCE.Forms.InputPanel OnScreenKeyboard;
        private System.Windows.Forms.PictureBox PBSoftKeyb;
        private System.Windows.Forms.CheckBox cb_forcedelete;
    }
}