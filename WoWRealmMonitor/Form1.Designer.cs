namespace WoWRealmMonitor
{
    partial class frmMain
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmMain));
            this.cxmRealmRow = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.tsmDeleteRealm = new System.Windows.Forms.ToolStripMenuItem();
            this.btnStart = new System.Windows.Forms.Button();
            this.btnHide = new System.Windows.Forms.Button();
            this.cxmNotifyIcon = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.showToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.realmNotifyIcon = new System.Windows.Forms.NotifyIcon(this.components);
            this.dgvRealms = new System.Windows.Forms.DataGridView();
            this.txtLog = new System.Windows.Forms.TextBox();
            this.colRealmName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colStatus = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colQueue = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cxmRealmRow.SuspendLayout();
            this.cxmNotifyIcon.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvRealms)).BeginInit();
            this.SuspendLayout();
            // 
            // cxmRealmRow
            // 
            this.cxmRealmRow.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmDeleteRealm});
            this.cxmRealmRow.Name = "cxmRealmRow";
            this.cxmRealmRow.ShowImageMargin = false;
            this.cxmRealmRow.Size = new System.Drawing.Size(83, 26);
            // 
            // tsmDeleteRealm
            // 
            this.tsmDeleteRealm.Name = "tsmDeleteRealm";
            this.tsmDeleteRealm.Size = new System.Drawing.Size(82, 22);
            this.tsmDeleteRealm.Text = "Delete";
            this.tsmDeleteRealm.Click += new System.EventHandler(this.tsmDeleteRealm_Click);
            // 
            // btnStart
            // 
            this.btnStart.Location = new System.Drawing.Point(322, 12);
            this.btnStart.Name = "btnStart";
            this.btnStart.Size = new System.Drawing.Size(52, 27);
            this.btnStart.TabIndex = 2;
            this.btnStart.Text = "Start";
            this.btnStart.UseVisualStyleBackColor = true;
            this.btnStart.Click += new System.EventHandler(this.btnStart_Click);
            // 
            // btnHide
            // 
            this.btnHide.Location = new System.Drawing.Point(322, 45);
            this.btnHide.Name = "btnHide";
            this.btnHide.Size = new System.Drawing.Size(52, 27);
            this.btnHide.TabIndex = 3;
            this.btnHide.Text = "Hide";
            this.btnHide.UseVisualStyleBackColor = true;
            this.btnHide.Click += new System.EventHandler(this.btnHide_Click);
            // 
            // cxmNotifyIcon
            // 
            this.cxmNotifyIcon.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.showToolStripMenuItem,
            this.exitToolStripMenuItem});
            this.cxmNotifyIcon.Name = "cxmNotifyIcon";
            this.cxmNotifyIcon.ShowImageMargin = false;
            this.cxmNotifyIcon.Size = new System.Drawing.Size(128, 70);
            // 
            // showToolStripMenuItem
            // 
            this.showToolStripMenuItem.Name = "showToolStripMenuItem";
            this.showToolStripMenuItem.Size = new System.Drawing.Size(127, 22);
            this.showToolStripMenuItem.Text = "Show";
            this.showToolStripMenuItem.Click += new System.EventHandler(this.showToolStripMenuItem_Click);
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(127, 22);
            this.exitToolStripMenuItem.Text = "Exit";
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.exitToolStripMenuItem_Click);
            // 
            // realmNotifyIcon
            // 
            this.realmNotifyIcon.BalloonTipIcon = System.Windows.Forms.ToolTipIcon.Info;
            this.realmNotifyIcon.BalloonTipTitle = "WoW Realm Monitor";
            this.realmNotifyIcon.ContextMenuStrip = this.cxmNotifyIcon;
            this.realmNotifyIcon.Icon = ((System.Drawing.Icon)(resources.GetObject("realmNotifyIcon.Icon")));
            this.realmNotifyIcon.Text = "WoW Realm Monitor";
            this.realmNotifyIcon.Visible = true;
            this.realmNotifyIcon.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.realmNotifyIcon_MouseDoubleClick);
            // 
            // dgvRealms
            // 
            this.dgvRealms.BackgroundColor = System.Drawing.SystemColors.ControlLight;
            this.dgvRealms.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvRealms.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colRealmName,
            this.colStatus,
            this.colQueue});
            this.dgvRealms.ContextMenuStrip = this.cxmRealmRow;
            this.dgvRealms.Location = new System.Drawing.Point(14, 12);
            this.dgvRealms.Name = "dgvRealms";
            this.dgvRealms.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            this.dgvRealms.RowHeadersWidth = 25;
            this.dgvRealms.RowTemplate.ContextMenuStrip = this.cxmRealmRow;
            this.dgvRealms.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.dgvRealms.Size = new System.Drawing.Size(302, 131);
            this.dgvRealms.TabIndex = 4;
            // 
            // txtLog
            // 
            this.txtLog.Font = new System.Drawing.Font("Lucida Console", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtLog.Location = new System.Drawing.Point(14, 149);
            this.txtLog.Multiline = true;
            this.txtLog.Name = "txtLog";
            this.txtLog.ReadOnly = true;
            this.txtLog.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtLog.Size = new System.Drawing.Size(360, 123);
            this.txtLog.TabIndex = 5;
            // 
            // colRealmName
            // 
            this.colRealmName.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.colRealmName.HeaderText = "Realm";
            this.colRealmName.Name = "colRealmName";
            // 
            // colStatus
            // 
            this.colStatus.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.colStatus.HeaderText = "Status";
            this.colStatus.Name = "colStatus";
            this.colStatus.Width = 62;
            // 
            // colQueue
            // 
            this.colQueue.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.colQueue.HeaderText = "Queue";
            this.colQueue.Name = "colQueue";
            this.colQueue.Width = 64;
            // 
            // frmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(386, 284);
            this.Controls.Add(this.txtLog);
            this.Controls.Add(this.dgvRealms);
            this.Controls.Add(this.btnHide);
            this.Controls.Add(this.btnStart);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmMain";
            this.Text = "WoW Realm Monitor Settings";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmMain_FormClosing);
            this.Load += new System.EventHandler(this.frmMain_Load);
            this.Shown += new System.EventHandler(this.frmMain_Shown);
            this.cxmRealmRow.ResumeLayout(false);
            this.cxmNotifyIcon.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvRealms)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnStart;
        private System.Windows.Forms.Button btnHide;
        private System.Windows.Forms.ContextMenuStrip cxmRealmRow;
        private System.Windows.Forms.ToolStripMenuItem tsmDeleteRealm;
        private System.Windows.Forms.ContextMenuStrip cxmNotifyIcon;
        private System.Windows.Forms.ToolStripMenuItem showToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.NotifyIcon realmNotifyIcon;
        private System.Windows.Forms.DataGridView dgvRealms;
        private System.Windows.Forms.TextBox txtLog;
        private System.Windows.Forms.DataGridViewTextBoxColumn colRealmName;
        private System.Windows.Forms.DataGridViewTextBoxColumn colStatus;
        private System.Windows.Forms.DataGridViewTextBoxColumn colQueue;
    }
}

