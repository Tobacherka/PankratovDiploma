using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AvaloniaApplication.Classes
{
    public static class Connection
    {
        public static UriBuilder ConnectionDB = new UriBuilder("https://localhost:7250/Command");
    }
}
