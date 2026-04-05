using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InvertorsIT.Connections
{
    public static class UserSession
    {
        public static int CurrentUserID { get; set; }
        public static string CurrentUsername { get; set; }
        public static string CurrentUserRole { get; set; }
        public static int CurrentUserRoleID { get; set; }
    }
}
