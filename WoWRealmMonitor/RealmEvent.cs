using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WoWRealmMonitor
{
    class RealmEvent
    {

        private String strRealmName = "";
        public String Name
        {
            get
            {
                return strRealmName;
            }
        }
        
        private RealmEventType type;
        public RealmEventType Type
        {
            get
            {
                return type;
            }
        }

        public RealmEvent(String strRealmName, RealmEventType reType)
        {
            this.strRealmName = strRealmName;
            this.type = reType;
        }

        public String GetEventDescription()
        {
            String strDesc = strRealmName + " is ";
            switch (type)
            {
                case RealmEventType.END_QUEUE:
                    strDesc += " not queueing.";
                    break;
                case RealmEventType.GONE_DOWN:
                    strDesc += " now DOWN.";
                    break;
                case RealmEventType.GONE_UP:
                    strDesc += " now UP.";
                    break;
                case RealmEventType.OTHER:
                    strDesc += " in an unknown state.";
                    break;
                case RealmEventType.START_QUEUE:
                    strDesc += " now queueing players.";
                    break;
            }
            return strDesc;
        }

    }
}