namespace WMSMobileClient
{
    partial class FrmReceiveView
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmReceiveView));
            this.mainMenu1 = new System.Windows.Forms.MainMenu();
            this.LBMsgBox = new System.Windows.Forms.Label();
            this.DGInventorytemsList = new System.Windows.Forms.DataGrid();
            this.BtnDelete = new System.Windows.Forms.PictureBox();
            this.PBBtnBck = new System.Windows.Forms.PictureBox();
            this.PBMenuBar = new System.Windows.Forms.PictureBox();
            this.PBoxMessage = new System.Windows.Forms.PictureBox();
            this.BtnView = new System.Windows.Forms.PictureBox();
            this.BtnSync = new System.Windows.Forms.PictureBox();
            this.lb_barcode = new System.Windows.Forms.Label();
            this.lb_itemcode = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.lb_itemdesc = new System.Windows.Forms.Label();
            this.lb_widthlength = new System.Windows.Forms.Label();
            this.lb_color = new System.Windows.Forms.Label();
            this.lb_draft = new System.Windows.Forms.Label();
            this.tb_lotcode = new System.Windows.Forms.TextBox();
            this.tb_qty = new System.Windows.Forms.TextBox();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.SuspendLayout();
            // 
            // LBMsgBox
            // 
            this.LBMsgBox.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(244)))), ((int)(((byte)(235)))), ((int)(((byte)(163)))));
            this.LBMsgBox.Font = new System.Drawing.Font("Tahoma", 10F, System.Drawing.FontStyle.Bold);
            this.LBMsgBox.ForeColor = System.Drawing.Color.Brown;
            this.LBMsgBox.Location = new System.Drawing.Point(12, 214);
            this.LBMsgBox.Name = "LBMsgBox";
            this.LBMsgBox.Size = new System.Drawing.Size(212, 23);
            this.LBMsgBox.Visible = false;
            // 
            // DGInventorytemsList
            // 
            this.DGInventorytemsList.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            this.DGInventorytemsList.Location = new System.Drawing.Point(12, 77);
            this.DGInventorytemsList.Name = "DGInventorytemsList";
            this.DGInventorytemsList.Size = new System.Drawing.Size(216, 131);
            this.DGInventorytemsList.TabIndex = 3;
            this.DGInventorytemsList.CurrentCellChanged += new System.EventHandler(this.DGInventorytemsList_CurrentCellChanged);
            this.DGInventorytemsList.KeyDown += new System.Windows.Forms.KeyEventHandler(this.DGInventorytemsList_KeyDown);
            // 
            // BtnDelete
            // 
            this.BtnDelete.Image = ((System.Drawing.Image)(resources.GetObject("BtnDelete.Image")));
            this.BtnDelete.Location = new System.Drawing.Point(75, 275);
            this.BtnDelete.Name = "BtnDelete";
            this.BtnDelete.Size = new System.Drawing.Size(46, 44);
            this.BtnDelete.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.BtnDelete.Visible = false;
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
            this.PBoxMessage.Location = new System.Drawing.Point(6, 210);
            this.PBoxMessage.Name = "PBoxMessage";
            this.PBoxMessage.Size = new System.Drawing.Size(228, 31);
            this.PBoxMessage.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.PBoxMessage.Visible = false;
            // 
            // BtnView
            // 
            this.BtnView.Image = ((System.Drawing.Image)(resources.GetObject("BtnView.Image")));
            this.BtnView.Location = new System.Drawing.Point(135, 274);
            this.BtnView.Name = "BtnView";
            this.BtnView.Size = new System.Drawing.Size(46, 44);
            this.BtnView.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.BtnView.Visible = false;
            // 
            // BtnSync
            // 
            this.BtnSync.Image = ((System.Drawing.Image)(resources.GetObject("BtnSync.Image")));
            this.BtnSync.Location = new System.Drawing.Point(190, 273);
            this.BtnSync.Name = "BtnSync";
            this.BtnSync.Size = new System.Drawing.Size(46, 44);
            this.BtnSync.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.BtnSync.Visible = false;
            // 
            // lb_barcode
            // 
            this.lb_barcode.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.lb_barcode.Location = new System.Drawing.Point(1, 8);
            this.lb_barcode.Name = "lb_barcode";
            this.lb_barcode.Size = new System.Drawing.Size(20, 14);
            this.lb_barcode.Text = "|| ||";
            this.lb_barcode.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // lb_itemcode
            // 
            this.lb_itemcode.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Bold);
            this.lb_itemcode.Location = new System.Drawing.Point(15, 32);
            this.lb_itemcode.Name = "lb_itemcode";
            this.lb_itemcode.Size = new System.Drawing.Size(139, 14);
            // 
            // label2
            // 
            this.label2.Font = new System.Drawing.Font("Tahoma", 6F, System.Drawing.FontStyle.Regular);
            this.label2.ForeColor = System.Drawing.Color.DarkRed;
            this.label2.Location = new System.Drawing.Point(220, 12);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(19, 12);
            this.label2.Text = "TMX";
            // 
            // lb_itemdesc
            // 
            this.lb_itemdesc.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Bold);
            this.lb_itemdesc.Location = new System.Drawing.Point(15, 46);
            this.lb_itemdesc.Name = "lb_itemdesc";
            this.lb_itemdesc.Size = new System.Drawing.Size(139, 14);
            // 
            // lb_widthlength
            // 
            this.lb_widthlength.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.lb_widthlength.ForeColor = System.Drawing.SystemColors.ControlDark;
            this.lb_widthlength.Location = new System.Drawing.Point(15, 60);
            this.lb_widthlength.Name = "lb_widthlength";
            this.lb_widthlength.Size = new System.Drawing.Size(213, 14);
            // 
            // lb_color
            // 
            this.lb_color.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Bold);
            this.lb_color.ForeColor = System.Drawing.Color.Goldenrod;
            this.lb_color.Location = new System.Drawing.Point(160, 31);
            this.lb_color.Name = "lb_color";
            this.lb_color.Size = new System.Drawing.Size(68, 15);
            this.lb_color.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // lb_draft
            // 
            this.lb_draft.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.lb_draft.Location = new System.Drawing.Point(163, 46);
            this.lb_draft.Name = "lb_draft";
            this.lb_draft.Size = new System.Drawing.Size(65, 14);
            this.lb_draft.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // tb_lotcode
            // 
            this.tb_lotcode.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(247)))), ((int)(((byte)(247)))));
            this.tb_lotcode.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.tb_lotcode.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.tb_lotcode.Location = new System.Drawing.Point(32, 4);
            this.tb_lotcode.Name = "tb_lotcode";
            this.tb_lotcode.Size = new System.Drawing.Size(133, 21);
            this.tb_lotcode.TabIndex = 96;
            this.tb_lotcode.GotFocus += new System.EventHandler(this.tb_lotcode_GotFocus);
            this.tb_lotcode.KeyDown += new System.Windows.Forms.KeyEventHandler(this.tb_lotcode_KeyDown);
            this.tb_lotcode.LostFocus += new System.EventHandler(this.tb_lotcode_LostFocus);
            // 
            // tb_qty
            // 
            this.tb_qty.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(247)))), ((int)(((byte)(247)))));
            this.tb_qty.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.tb_qty.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.tb_qty.Location = new System.Drawing.Point(180, 4);
            this.tb_qty.Name = "tb_qty";
            this.tb_qty.Size = new System.Drawing.Size(34, 21);
            this.tb_qty.TabIndex = 112;
            this.tb_qty.GotFocus += new System.EventHandler(this.tb_qty_GotFocus);
            this.tb_qty.KeyDown += new System.Windows.Forms.KeyEventHandler(this.tb_qty_KeyDown_1);
            this.tb_qty.LostFocus += new System.EventHandler(this.tb_qty_LostFocus);
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(26, 2);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(143, 25);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            // 
            // pictureBox2
            // 
            this.pictureBox2.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox2.Image")));
            this.pictureBox2.Location = new System.Drawing.Point(176, 2);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(43, 25);
            this.pictureBox2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            // 
            // FrmReceiveView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.ClientSize = new System.Drawing.Size(240, 320);
            this.Controls.Add(this.tb_qty);
            this.Controls.Add(this.tb_lotcode);
            this.Controls.Add(this.lb_draft);
            this.Controls.Add(this.lb_color);
            this.Controls.Add(this.lb_widthlength);
            this.Controls.Add(this.lb_itemdesc);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.lb_itemcode);
            this.Controls.Add(this.lb_barcode);
            this.Controls.Add(this.BtnView);
            this.Controls.Add(this.BtnSync);
            this.Controls.Add(this.BtnDelete);
            this.Controls.Add(this.PBBtnBck);
            this.Controls.Add(this.PBMenuBar);
            this.Controls.Add(this.LBMsgBox);
            this.Controls.Add(this.PBoxMessage);
            this.Controls.Add(this.DGInventorytemsList);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.pictureBox2);
            this.Name = "FrmReceiveView";
            this.Text = "Παραλαβές";
            this.Load += new System.EventHandler(this.FrmInventoryView_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.FrmInventoryView_KeyDown);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label LBMsgBox;
        private System.Windows.Forms.PictureBox PBoxMessage;
        private System.Windows.Forms.DataGrid DGInventorytemsList;
        private System.Windows.Forms.PictureBox PBBtnBck;
        private System.Windows.Forms.PictureBox PBMenuBar;
        private System.Windows.Forms.PictureBox BtnDelete;
        private System.Windows.Forms.PictureBox BtnView;
        private System.Windows.Forms.PictureBox BtnSync;
        private System.Windows.Forms.Label lb_barcode;
        private System.Windows.Forms.Label lb_itemcode;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label lb_itemdesc;
        private System.Windows.Forms.Label lb_widthlength;
        private System.Windows.Forms.Label lb_color;
        private System.Windows.Forms.Label lb_draft;
        private System.Windows.Forms.TextBox tb_lotcode;
        private System.Windows.Forms.TextBox tb_qty;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.PictureBox pictureBox2;
    }
}