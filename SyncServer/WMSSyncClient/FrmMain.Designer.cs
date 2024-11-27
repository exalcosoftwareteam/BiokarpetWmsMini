namespace WMSSyncClient
{
    partial class FrmMain
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

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
            this.TBWSVCRootFolder = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.ImgSyncItems = new System.Windows.Forms.PictureBox();
            this.ImgOkWebserviceCon = new System.Windows.Forms.PictureBox();
            this.LBSyncItems = new System.Windows.Forms.Label();
            this.LBSyncLots = new System.Windows.Forms.Label();
            this.ImgSyncLots = new System.Windows.Forms.PictureBox();
            this.label2 = new System.Windows.Forms.Label();
            this.DDLStore = new System.Windows.Forms.ComboBox();
            this.BtnSetStore = new System.Windows.Forms.Button();
            this.BtnSyncData = new System.Windows.Forms.Button();
            this.LBStoreID = new System.Windows.Forms.Label();
            this.LBWebserviceStatus = new System.Windows.Forms.Label();
            this.BWorker = new System.ComponentModel.BackgroundWorker();
            this.PGBar = new System.Windows.Forms.ProgressBar();
            this.BtnCancel = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.CkBmunits = new System.Windows.Forms.CheckBox();
            this.label4 = new System.Windows.Forms.Label();
            this.CkBItems = new System.Windows.Forms.CheckBox();
            this.CkBLots = new System.Windows.Forms.CheckBox();
            this.LBwebserviceVersion = new System.Windows.Forms.Label();
            this.PGLotBar = new System.Windows.Forms.ProgressBar();
            this.BtnCancel2 = new System.Windows.Forms.Button();
            this.BWorker2 = new System.ComponentModel.BackgroundWorker();
            ((System.ComponentModel.ISupportInitialize)(this.ImgSyncItems)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ImgOkWebserviceCon)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ImgSyncLots)).BeginInit();
            this.SuspendLayout();
            // 
            // TBWSVCRootFolder
            // 
            this.TBWSVCRootFolder.BackColor = System.Drawing.Color.Gainsboro;
            this.TBWSVCRootFolder.Enabled = false;
            this.TBWSVCRootFolder.Location = new System.Drawing.Point(101, 97);
            this.TBWSVCRootFolder.Name = "TBWSVCRootFolder";
            this.TBWSVCRootFolder.Size = new System.Drawing.Size(617, 20);
            this.TBWSVCRootFolder.TabIndex = 12;
            this.TBWSVCRootFolder.TextChanged += new System.EventHandler(this.TBWSVCRootFolder_TextChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(14, 101);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(75, 13);
            this.label1.TabIndex = 13;
            this.label1.Text = "Σύνδεση με...";
            // 
            // ImgSyncItems
            // 
            this.ImgSyncItems.Image = global::WMSSyncClient.Properties.Resources.error;
            this.ImgSyncItems.Location = new System.Drawing.Point(681, 308);
            this.ImgSyncItems.Name = "ImgSyncItems";
            this.ImgSyncItems.Size = new System.Drawing.Size(37, 32);
            this.ImgSyncItems.TabIndex = 15;
            this.ImgSyncItems.TabStop = false;
            this.ImgSyncItems.Visible = false;
            // 
            // ImgOkWebserviceCon
            // 
            this.ImgOkWebserviceCon.Image = global::WMSSyncClient.Properties.Resources.ok;
            this.ImgOkWebserviceCon.Location = new System.Drawing.Point(724, 83);
            this.ImgOkWebserviceCon.Name = "ImgOkWebserviceCon";
            this.ImgOkWebserviceCon.Size = new System.Drawing.Size(37, 32);
            this.ImgOkWebserviceCon.TabIndex = 14;
            this.ImgOkWebserviceCon.TabStop = false;
            this.ImgOkWebserviceCon.Visible = false;
            // 
            // LBSyncItems
            // 
            this.LBSyncItems.AutoSize = true;
            this.LBSyncItems.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(161)));
            this.LBSyncItems.Location = new System.Drawing.Point(99, 311);
            this.LBSyncItems.Name = "LBSyncItems";
            this.LBSyncItems.Size = new System.Drawing.Size(376, 29);
            this.LBSyncItems.TabIndex = 16;
            this.LBSyncItems.Text = ".......................Συγχρονισμός  Ειδών";
            this.LBSyncItems.Visible = false;
            // 
            // LBSyncLots
            // 
            this.LBSyncLots.AutoSize = true;
            this.LBSyncLots.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(161)));
            this.LBSyncLots.Location = new System.Drawing.Point(106, 449);
            this.LBSyncLots.Name = "LBSyncLots";
            this.LBSyncLots.Size = new System.Drawing.Size(379, 29);
            this.LBSyncLots.TabIndex = 17;
            this.LBSyncLots.Text = ".................Συγχρονισμός Παρτίδων";
            this.LBSyncLots.Visible = false;
            // 
            // ImgSyncLots
            // 
            this.ImgSyncLots.Image = global::WMSSyncClient.Properties.Resources.error;
            this.ImgSyncLots.Location = new System.Drawing.Point(682, 443);
            this.ImgSyncLots.Name = "ImgSyncLots";
            this.ImgSyncLots.Size = new System.Drawing.Size(37, 32);
            this.ImgSyncLots.TabIndex = 18;
            this.ImgSyncLots.TabStop = false;
            this.ImgSyncLots.Visible = false;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(17, 47);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(71, 13);
            this.label2.TabIndex = 19;
            this.label2.Text = "Αποθ. Χώρος";
            // 
            // DDLStore
            // 
            this.DDLStore.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.DDLStore.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(161)));
            this.DDLStore.FormattingEnabled = true;
            this.DDLStore.Location = new System.Drawing.Point(104, 38);
            this.DDLStore.Name = "DDLStore";
            this.DDLStore.Size = new System.Drawing.Size(614, 32);
            this.DDLStore.TabIndex = 20;
            this.DDLStore.SelectionChangeCommitted += new System.EventHandler(this.DDLStore_SelectionChangeCommitted);
            this.DDLStore.SelectedIndexChanged += new System.EventHandler(this.DDLStore_SelectedIndexChanged);
            this.DDLStore.Leave += new System.EventHandler(this.DDLStore_Leave);
            this.DDLStore.Enter += new System.EventHandler(this.DDLStore_Enter);
            // 
            // BtnSetStore
            // 
            this.BtnSetStore.Location = new System.Drawing.Point(724, 36);
            this.BtnSetStore.Name = "BtnSetStore";
            this.BtnSetStore.Size = new System.Drawing.Size(53, 34);
            this.BtnSetStore.TabIndex = 21;
            this.BtnSetStore.Text = "Αλλαγή";
            this.BtnSetStore.UseVisualStyleBackColor = true;
            this.BtnSetStore.Click += new System.EventHandler(this.BtnSetStore_Click);
            // 
            // BtnSyncData
            // 
            this.BtnSyncData.BackColor = System.Drawing.Color.GreenYellow;
            this.BtnSyncData.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(161)));
            this.BtnSyncData.Location = new System.Drawing.Point(104, 213);
            this.BtnSyncData.Name = "BtnSyncData";
            this.BtnSyncData.Size = new System.Drawing.Size(614, 71);
            this.BtnSyncData.TabIndex = 22;
            this.BtnSyncData.Text = "Λήψη Δεδομένων";
            this.BtnSyncData.UseVisualStyleBackColor = false;
            this.BtnSyncData.Click += new System.EventHandler(this.BtnSyncData_Click);
            // 
            // LBStoreID
            // 
            this.LBStoreID.AutoSize = true;
            this.LBStoreID.Location = new System.Drawing.Point(45, 64);
            this.LBStoreID.Name = "LBStoreID";
            this.LBStoreID.Size = new System.Drawing.Size(43, 13);
            this.LBStoreID.TabIndex = 23;
            this.LBStoreID.Text = "            ";
            // 
            // LBWebserviceStatus
            // 
            this.LBWebserviceStatus.AutoSize = true;
            this.LBWebserviceStatus.BackColor = System.Drawing.SystemColors.Control;
            this.LBWebserviceStatus.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F);
            this.LBWebserviceStatus.ForeColor = System.Drawing.Color.Red;
            this.LBWebserviceStatus.Location = new System.Drawing.Point(104, 129);
            this.LBWebserviceStatus.Name = "LBWebserviceStatus";
            this.LBWebserviceStatus.Size = new System.Drawing.Size(229, 24);
            this.LBWebserviceStatus.TabIndex = 24;
            this.LBWebserviceStatus.Text = "Κατάσταση Υπηρεσίας ...";
            this.LBWebserviceStatus.Visible = false;
            // 
            // BWorker
            // 
            this.BWorker.WorkerReportsProgress = true;
            this.BWorker.WorkerSupportsCancellation = true;
            this.BWorker.DoWork += new System.ComponentModel.DoWorkEventHandler(this.BWorker_DoWork);
            this.BWorker.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.BWorker_RunWorkerCompleted);
            this.BWorker.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.BWorker_ProgressChanged);
            // 
            // PGBar
            // 
            this.PGBar.Location = new System.Drawing.Point(104, 343);
            this.PGBar.Name = "PGBar";
            this.PGBar.Size = new System.Drawing.Size(612, 23);
            this.PGBar.TabIndex = 26;
            // 
            // BtnCancel
            // 
            this.BtnCancel.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(161)));
            this.BtnCancel.Location = new System.Drawing.Point(722, 343);
            this.BtnCancel.Name = "BtnCancel";
            this.BtnCancel.Size = new System.Drawing.Size(52, 23);
            this.BtnCancel.TabIndex = 27;
            this.BtnCancel.Text = "Ακύρωση";
            this.BtnCancel.UseVisualStyleBackColor = true;
            this.BtnCancel.Click += new System.EventHandler(this.BtnCancel_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.ForeColor = System.Drawing.Color.Gray;
            this.label3.Location = new System.Drawing.Point(12, 9);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(170, 13);
            this.label3.TabIndex = 28;
            this.label3.Text = "Sync Client Version 1.1. 09052012";
            // 
            // CkBmunits
            // 
            this.CkBmunits.AutoSize = true;
            this.CkBmunits.Location = new System.Drawing.Point(108, 175);
            this.CkBmunits.Name = "CkBmunits";
            this.CkBmunits.Size = new System.Drawing.Size(125, 17);
            this.CkBmunits.TabIndex = 29;
            this.CkBmunits.Text = "Μονάδες Μέτρησης";
            this.CkBmunits.UseVisualStyleBackColor = true;
            this.CkBmunits.CheckedChanged += new System.EventHandler(this.CkBmunits_CheckedChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(14, 176);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(70, 13);
            this.label4.TabIndex = 30;
            this.label4.Text = "Μεταφορά...";
            // 
            // CkBItems
            // 
            this.CkBItems.AutoSize = true;
            this.CkBItems.Location = new System.Drawing.Point(271, 175);
            this.CkBItems.Name = "CkBItems";
            this.CkBItems.Size = new System.Drawing.Size(47, 17);
            this.CkBItems.TabIndex = 31;
            this.CkBItems.Text = "Είδη";
            this.CkBItems.UseVisualStyleBackColor = true;
            this.CkBItems.CheckedChanged += new System.EventHandler(this.CkBItems_CheckedChanged);
            // 
            // CkBLots
            // 
            this.CkBLots.AutoSize = true;
            this.CkBLots.Location = new System.Drawing.Point(473, 172);
            this.CkBLots.Name = "CkBLots";
            this.CkBLots.Size = new System.Drawing.Size(73, 17);
            this.CkBLots.TabIndex = 32;
            this.CkBLots.Text = "Παρτίδες";
            this.CkBLots.UseVisualStyleBackColor = true;
            this.CkBLots.CheckedChanged += new System.EventHandler(this.CkBLots_CheckedChanged);
            // 
            // LBwebserviceVersion
            // 
            this.LBwebserviceVersion.AutoSize = true;
            this.LBwebserviceVersion.ForeColor = System.Drawing.Color.Gray;
            this.LBwebserviceVersion.Location = new System.Drawing.Point(102, 83);
            this.LBwebserviceVersion.Name = "LBwebserviceVersion";
            this.LBwebserviceVersion.Size = new System.Drawing.Size(113, 13);
            this.LBwebserviceVersion.TabIndex = 33;
            this.LBwebserviceVersion.Text = "Web Service Version :";
            // 
            // PGLotBar
            // 
            this.PGLotBar.Location = new System.Drawing.Point(108, 481);
            this.PGLotBar.Name = "PGLotBar";
            this.PGLotBar.Size = new System.Drawing.Size(612, 23);
            this.PGLotBar.TabIndex = 34;
            // 
            // BtnCancel2
            // 
            this.BtnCancel2.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(161)));
            this.BtnCancel2.Location = new System.Drawing.Point(725, 481);
            this.BtnCancel2.Name = "BtnCancel2";
            this.BtnCancel2.Size = new System.Drawing.Size(52, 23);
            this.BtnCancel2.TabIndex = 35;
            this.BtnCancel2.Text = "Ακύρωση";
            this.BtnCancel2.UseVisualStyleBackColor = true;
            this.BtnCancel2.Click += new System.EventHandler(this.BtnCancel2_Click);
            // 
            // BWorker2
            // 
            this.BWorker2.WorkerReportsProgress = true;
            this.BWorker2.WorkerSupportsCancellation = true;
            this.BWorker2.DoWork += new System.ComponentModel.DoWorkEventHandler(this.BWorker2_DoWork);
            this.BWorker2.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.BWorker2_RunWorkerCompleted);
            this.BWorker2.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.BWorker2_ProgressChanged);
            // 
            // FrmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(789, 553);
            this.Controls.Add(this.BtnCancel2);
            this.Controls.Add(this.PGLotBar);
            this.Controls.Add(this.LBwebserviceVersion);
            this.Controls.Add(this.CkBLots);
            this.Controls.Add(this.CkBItems);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.CkBmunits);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.BtnCancel);
            this.Controls.Add(this.PGBar);
            this.Controls.Add(this.LBWebserviceStatus);
            this.Controls.Add(this.LBStoreID);
            this.Controls.Add(this.BtnSyncData);
            this.Controls.Add(this.BtnSetStore);
            this.Controls.Add(this.DDLStore);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.ImgSyncLots);
            this.Controls.Add(this.LBSyncLots);
            this.Controls.Add(this.LBSyncItems);
            this.Controls.Add(this.ImgSyncItems);
            this.Controls.Add(this.ImgOkWebserviceCon);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.TBWSVCRootFolder);
            this.Name = "FrmMain";
            this.Text = "WMS Sync Client";
            this.Load += new System.EventHandler(this.FrmMain_Load);
            ((System.ComponentModel.ISupportInitialize)(this.ImgSyncItems)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ImgOkWebserviceCon)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ImgSyncLots)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox TBWSVCRootFolder;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.PictureBox ImgOkWebserviceCon;
        private System.Windows.Forms.PictureBox ImgSyncItems;
        private System.Windows.Forms.Label LBSyncLots;
        private System.Windows.Forms.PictureBox ImgSyncLots;
        private System.Windows.Forms.Label LBSyncItems;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox DDLStore;
        private System.Windows.Forms.Button BtnSetStore;
        private System.Windows.Forms.Button BtnSyncData;
        private System.Windows.Forms.Label LBStoreID;
        private System.Windows.Forms.Label LBWebserviceStatus;
        private System.ComponentModel.BackgroundWorker BWorker;
        private System.Windows.Forms.ProgressBar PGBar;
        private System.Windows.Forms.Button BtnCancel;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.CheckBox CkBmunits;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.CheckBox CkBItems;
        private System.Windows.Forms.CheckBox CkBLots;
        private System.Windows.Forms.Label LBwebserviceVersion;
        private System.Windows.Forms.ProgressBar PGLotBar;
        private System.Windows.Forms.Button BtnCancel2;
        private System.ComponentModel.BackgroundWorker BWorker2;
    }
}

