using Avalonia.Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AvaloniaApplication.Classes
{
    public static class GlobalBuffer
    {
        public static Grid _mainGrid;
        public static string? Name = null;
        public static string Role;
        internal static List<DbProduct>? Products {  get; set; }
    }
}
