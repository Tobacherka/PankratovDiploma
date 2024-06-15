using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AvaloniaApplication.Classes
{
    /// <summary>
    /// Класс для хранения строки подключения к API
    /// </summary>
    public static class Connection
    {
        // Строка подключения к api
        public static UriBuilder ConnectionDB = new UriBuilder("http://localhost:5267/Command");
        public static string ConnectionStr = "http://localhost:5267/Command";
    }
}
