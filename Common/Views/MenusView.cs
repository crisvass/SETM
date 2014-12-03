using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Views
{
    public class MenusView
    {
        public int MenuId { get; set; }
        public string Action { get; set; }
        public string Url { get; set; }
        public string Title { get; set; }
        public int Position { get; set; }
        public bool HasSubmenus { get; set; }
        public IEnumerable<MenusView> Submenus { get; set; }
    }
}
