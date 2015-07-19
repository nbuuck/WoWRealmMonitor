using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Net;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;

namespace WoWRealmMonitor
{

    enum RealmState { UP = 0, DOWN = 1, OTHER = 2 };
    enum RealmEventType { GONE_UP, GONE_DOWN, START_QUEUE, END_QUEUE, OTHER };

    public partial class frmMain : Form
    {

        const string strAPI = "http://us.battle.net/api/wow/realm/status";
        const string strRealmsFile = "realms.txt";
        
        RealmStatusResponse rsrPrevious = null; // The previous JSON response for comparison of states.
        RealmStatusResponse rsrCurrent = null;  // The most current response.
        List<RealmEvent> events = new List<RealmEvent>(); // Events to show a NotifyIcon for.

        int intExceptionCount = 0;              // How many exceptions encountered between successes.
        bool isExceptionWarningShown = false;   // Has a Notify bubble about issues been shown?
        bool isClosingFromTray = false;         // Is "Exit" in tray menu being used?
        bool isLoading = true;                  // Is the form still loading?
        bool isRealmsFile = false;              // Are we using a realms file at load time?
        Timer wrmTimer;

        public frmMain()
        {
            InitializeComponent();
            wrmTimer = new Timer();
        }

        #region Data Handling Functions

        private RealmState GetLocalRealmState(String strRealmName)
        {
            for (int i = 0; i < dgvRealms.Rows.Count; i++)
            {
                DataGridViewRow currentRow = dgvRealms.Rows[i];
                if (currentRow.Cells["colRealmName"].Value != null
                    && currentRow.Cells["colRealmName"].Value.ToString() == strRealmName)
                {
                    if (currentRow.Cells["colStatus"].Value != null
                        && currentRow.Cells["colStatus"].Value.ToString() == "UP")
                    {
                        return RealmState.UP;
                    }
                    else if (currentRow.Cells["colStatus"].Value != null
                        && currentRow.Cells["colStatus"].Value.ToString() == "DOWN")
                    {
                        return RealmState.DOWN;
                    }
                }
            }
            return RealmState.OTHER;
        }

        private bool GetLocalRealmQueueState(String strRealmName)
        {
            for (int i = 0; i < dgvRealms.Rows.Count; i++)
            {
                DataGridViewRow currentRow = dgvRealms.Rows[i];
                if (currentRow.Cells["colRealmName"].Value != null
                    && currentRow.Cells["colRealmName"].Value.ToString() == strRealmName)
                {
                    if (currentRow.Cells["colQueue"].Value.ToString().ToUpper() == "YES")
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
            }
            return false;
        }

        private string GetTimestamp()
        {
            return "[" + DateTime.Now.ToString("HH:mm:ss") + "]";
        }

        private void AddLogMessage(String strMessage)
        {
            txtLog.AppendText(GetTimestamp() + " " + strMessage + "\n");
            txtLog.SelectionStart = txtLog.Text.Length;
            txtLog.ScrollToCaret();
        }

        private void CheckRealms()
        {
            
            URLHelper u = new URLHelper(strAPI);

            foreach(DataGridViewRow row in dgvRealms.Rows)
            {
                if (row.Cells["colRealmName"].Value != null
                    && row.Cells["colRealmName"].Value.ToString() != ""
                    && row.Cells["colRealmName"].IsInEditMode == false)
                {
                    AddLogMessage("+" + row.Cells["colRealmName"].Value.ToString());
                    u.AddParamater("realm",row.Cells["colRealmName"].Value.ToString());
                }
            }

            if (u.Count > 0)
            {

                try
                {

                    HttpWebRequest req = (HttpWebRequest)WebRequest.Create(u.URL);
                    HttpWebResponse resp = (HttpWebResponse)req.GetResponse();
                    StreamReader reader = new StreamReader(resp.GetResponseStream());

                    DataContractJsonSerializer ser = new DataContractJsonSerializer(typeof(RealmStatusResponse));
                    RealmStatusResponse rStatuses = (RealmStatusResponse)ser.ReadObject(reader.BaseStream);

                    if (rsrCurrent != null)
                    {
                        rsrPrevious = rsrCurrent;
                    }
                    rsrCurrent = rStatuses;

                    foreach (RealmStatus rs in rsrCurrent.realms)
                    {
                        AddLogMessage("GOT " + rs.name);
                    }

                    rsrCurrent.RenderToDataGrid(dgvRealms);

                }
                catch (Exception)
                {
                    intExceptionCount++;
                    return;
                }

                // Reset our Exception watcher state
                // since we had a successful realm check.
                intExceptionCount = 0;
                isExceptionWarningShown = false;

            }
            
        }

        private void ToggleVisibility()
        {
            if (this.Visible)
            {
                this.Hide();
            }
            else
            {
                this.Show();
            }
        }

        private void SaveLocalRealmList(bool exitOnSave)
        {
            FileStream fs = null;
            StreamWriter sw = null;

            try
            {

                if (!File.Exists(strRealmsFile))
                {
                    fs = File.Create(strRealmsFile);
                }
                else
                {
                    fs = File.OpenWrite(strRealmsFile);
                }

                sw = new StreamWriter(fs);

                using (sw)
                {
                    for (int i = 0; i < dgvRealms.Rows.Count; i++)
                    {
                        DataGridViewRow cr = dgvRealms.Rows[i];
                        if (cr.Cells["colRealmName"].Value != null)
                        {
                            sw.WriteLine(cr.Cells["colRealmName"].Value.ToString() + ":" +
                                ((cr.Cells["colStatus"].Value == null) ? "?" : cr.Cells["colStatus"].Value.ToString()) + ":" +
                                ((cr.Cells["colQueue"].Value == null) ? "?" : cr.Cells["colQueue"].Value.ToString()) + "\n");
                        }

                    }
                }

                sw.Close();
                fs.Close();

            }
            catch (Exception)
            {
                // Presently, this condition never occurs.
                if (!exitOnSave)
                {
                    AddLogMessage("Couldn't save your realms list.");
                }
            }

            if (exitOnSave)
            {
                Application.Exit();
            }

        }

        private bool LoadLocalRealmList()
        {
            if (!File.Exists(strRealmsFile))
            {
                AddLogMessage("Please add a realm to the list.");
                return false;
            }

            try
            {

                FileStream fs = new FileStream(strRealmsFile, FileMode.Open);
                StreamReader sr = new StreamReader(fs);

                using (sr)
                {
                    while (!sr.EndOfStream)
                    {
                        String strLine = sr.ReadLine();
                        String[] strStatus = strLine.Split(':');
                        if (strStatus.Length == 3)
                        {
                            dgvRealms.Rows.Add(strStatus);
                        }
                    }
                }

                sr.Close();
                fs.Close();

            }
            catch (Exception)
            {
                AddLogMessage("Couldn't load your realms.txt.");
                return false;
            }

            return true;

        }

        private void ToggleTimerButton()
        {
            if (wrmTimer.Enabled)
            {
                wrmTimer.Stop();
                btnStart.Text = "Start";
                AddLogMessage("Stopped watching realm status.");
            }
            else
            {
                wrmTimer.Start();
                btnStart.Text = "Stop";
                AddLogMessage("Started watching realm status.");
            }
        }

        private void CheckExceptionCount()
        {
            if (intExceptionCount > 3
                && isExceptionWarningShown == false)
            {
                realmNotifyIcon.BalloonTipTitle = "WoW Realm Monitor";
                realmNotifyIcon.BalloonTipText = "WRM is having trouble checking your realms.\n\n" +
                    "Your connection or Battle.net may be down.";
                realmNotifyIcon.BalloonTipIcon = ToolTipIcon.Warning;
                realmNotifyIcon.ShowBalloonTip(100);
                isExceptionWarningShown = true;
            }
        }

        private void NotifyRealmState(String strRealmName, RealmState rstate)
        {
            string strStateText = "";
            switch (rstate)
            {
                case RealmState.UP:
                    strStateText = "UP";
                    break;
                case RealmState.DOWN:
                    strStateText = "DOWN";
                    break;
                default:
                    strStateText = null;
                    break;
            }
            if (strStateText == null) { return; }

            AddLogMessage(strRealmName + " is now " + strStateText + ".");
            realmNotifyIcon.BalloonTipText = strRealmName + " is now " + strStateText + ".";
            realmNotifyIcon.BalloonTipIcon = ToolTipIcon.Info;
            realmNotifyIcon.ShowBalloonTip(100);

        }

        private void NotifyQueueState(String strRealmName, bool isRealmQueued)
        {
            String strQueueText = ((isRealmQueued) ? "queing players" : "not queueing players");
            AddLogMessage(strRealmName + " is now " + strQueueText + ".");
            realmNotifyIcon.BalloonTipText = strRealmName + " is now " + strQueueText + ".";
            realmNotifyIcon.BalloonTipIcon = ToolTipIcon.Info;
            realmNotifyIcon.ShowBalloonTip(100);
        }

        #endregion

        #region Event Handlers

        private void frmMain_Load(object sender, EventArgs e)
        {

            // Setup Timer.
            wrmTimer.Interval = 10000;
            wrmTimer.Tick += new EventHandler(wrmTimer_Tick);

            // Tray Icon
            realmNotifyIcon.BalloonTipTitle = "WoW Realm Monitor";

            // Auto-Hide
            // If realms have previously been defined, just auto
            // hide and start monitoring.
            if (LoadLocalRealmList())
            {
                isRealmsFile = true;
                ToggleTimerButton();
            }

        }

        void wrmTimer_Tick(object sender, EventArgs e)
        {
            //AddLogMessage("Tick.");
            CheckRealms();
            CheckExceptionCount(); // Every tick, see if we're getting a lot of errors.
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            ToggleTimerButton();
        }

        private void tsmDeleteRealm_Click(object sender, EventArgs e)
        {
            if (dgvRealms.CurrentRow != null
                && !dgvRealms.IsCurrentCellDirty
                && !dgvRealms.IsCurrentRowDirty
                && dgvRealms.NewRowIndex != dgvRealms.CurrentRow.Index)
            {
                dgvRealms.Rows.Remove(dgvRealms.CurrentRow);
            }
        }

        private void realmNotifyIcon_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            ToggleVisibility();
        }

        private void btnHide_Click(object sender, EventArgs e)
        {
            ToggleVisibility();
        }

        private void showToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ToggleVisibility();
        }

        private void frmMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (isClosingFromTray)
            {
                SaveLocalRealmList(true);
                realmNotifyIcon.Visible = false;
                realmNotifyIcon.Dispose();
            }
            else
            {
                e.Cancel = true;
                ToggleVisibility();
            }
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            isClosingFromTray = true;
            this.Close();
        }

        private void frmMain_Shown(object sender, EventArgs e)
        {
            if (!isLoading) { return; }
            isLoading = false;
            if (isRealmsFile)
            {
                ToggleVisibility();
            }
        }

        #endregion

    }

}