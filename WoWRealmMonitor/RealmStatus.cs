using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace WoWRealmMonitor
{
    [DataContract]
    public class RealmStatus
    {
        [DataMember()]
        public String type;
        [DataMember()]
        public String population;
        [DataMember()]
        public bool queue;
        [DataMember()]
        public bool status;
        [DataMember()]
        public String name;
        [DataMember()]
        public String slug;

        /// <summary>
        /// An override that only considers the realm's queue and status when comparing.
        /// </summary>
        /// <param name="rsOther">The RealmStatus to compare this instance to.</param>
        /// <returns></returns>
        public bool Equals(RealmStatus rsOther)
        {
            return this.queue == rsOther.queue
                && this.status == rsOther.status;
        }

    }
}
