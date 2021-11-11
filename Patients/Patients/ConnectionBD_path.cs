using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Patients
{
    class ConnectionBD_path
    {
        public static string Path()
        {
            string path = "server=localhost; user=root;database=covid_passport;password=4321";
            return path;
        }
    }
}
