using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WoWRealmMonitor
{
    class URLHelper
    {

        private String strURL = "";

        public String URL
        {
            get
            {
                return strURL;
            }
        }

        private int intParamCount = 0;

        public int Count
        {
            get
            {
                return intParamCount;
            }
        }

        public URLHelper(String strURL)
        {
            this.strURL = strURL;
        }

        public void AddParamater(String strParamName, String strParamValue)
        {
            if (strParamName == null
                || strParamValue == null
                || strParamName.Trim() == ""
                || strParamValue.Trim() == "")
            {
                return;
            }
            String strDelim = ((intParamCount == 0)?"?":"&");
            strURL += strDelim + strParamName + "=" + strParamValue;
            intParamCount++;
        }

    }
}
