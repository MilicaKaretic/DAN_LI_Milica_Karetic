using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAN_LI_Milica_Karetic.Model
{
    class LoggedInDoctor
    {
        /// <summary>
        /// Current user
        /// </summary>
        private static tblDoctor currentDoctor;
        public static tblDoctor CurrentDoctor
        {
            get { return currentDoctor; }
            set { currentDoctor = value; }
        }
    }
}
