using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using System.Windows.Forms;

namespace WoWRealmMonitor
{

    [DataContract]
    public class RealmStatusResponse
    {
        
        /// <summary>
        /// The List of realms that met the parameters passed to the Community API.
        /// </summary>
        [DataMember()]
        public List<RealmStatus> realms;
        
        /// <summary>
        /// Retrieve the RealmStatus for a particular realm by name.
        /// </summary>
        /// <param name="strRealmName">The full name of the realm.</param>
        /// <returns></returns>
        public RealmStatus GetRealm(String strRealmName)
        {
            IEnumerator<RealmStatus> en = realms.GetEnumerator();

            while (en.MoveNext())
            {
                if (en.Current.name == strRealmName)
                {
                    return en.Current;
                }
            }

            return null;
        }

        /// <summary>
        /// Checks to see if the response contains info for a particular realm.
        /// </summary>
        /// <param name="strRealmName">The name of the realm to check for in the response.</param>
        /// <returns></returns>
        public bool HasRealm(String strRealmName)
        {
            IEnumerator<RealmStatus> en = realms.GetEnumerator();

            while (en.MoveNext())
            {
                if (en.Current.name == strRealmName)
                {
                    return true;
                }
            }

            return false;
        }

        /// <summary>
        /// Ouput this realm list to a DataGridView.
        /// Columns: Realm Name, Status, Queue
        /// </summary>
        /// <param name="dgv"></param>
        public void RenderToDataGrid(DataGridView dgv)
        {
            // Clear non-active and non-new rows.
            foreach (DataGridViewRow row in dgv.Rows)
            {
                if (!IsRowInEdit(row))
                {
                    if (row.Cells["colRealmName"].Value != null)
                    {
                        MessageBox.Show("Removing row for " + row.Cells["colRealmName"].Value.ToString());
                    }
                    dgv.Rows.Remove(row);
                }
            }

            // Output statuses.
            foreach (RealmStatus s in realms)
            {
                MessageBox.Show("Render " + s.name);
                dgv.Rows.Add(new[] { s.name, ((s.status) ? "UP" : "DONE"), ((s.queue) ? "YES" : "NO") });
            }
        }

        private bool IsRowInEdit(DataGridViewRow row)
        {
            foreach (DataGridViewCell cell in row.Cells)
            {
                if (cell.IsInEditMode)
                {
                    return true;
                }
            }
            return false;
        }

    }

}
