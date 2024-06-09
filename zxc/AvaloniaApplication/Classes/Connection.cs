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
        public static UriBuilder ConnectionDB = new UriBuilder("http://91.108.241.127:81/Command");
    }
}
