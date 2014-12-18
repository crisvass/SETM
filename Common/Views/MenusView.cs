using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Common.Views
{
    public class MenusView
    {
        [Display(Name = "Menu ID")]
        public Guid MenuId { get; set; }
        [Display(Name = "Action Name")]
        public string Action { get; set; }
        [Display(Name = "URL")]
        public string Url { get; set; }
        [Required]
        [Display(Name = "Title")]
        public string Title { get; set; }
        [Display(Name = "Position")]
        public int Position { get; set; }
        public bool HasSubmenus { get; set; }
        [Display(Name = "Submenus")]
        public IEnumerable<MenusView> Submenus { get; set; }
        [Required]
        [Display(Name = "Roles")]
        public IEnumerable<RoleView> MenuRoles { get; set; }
        public SelectList MenuRolesList { get; set; }

        public Guid ParentId { get; set; }
    }
}
