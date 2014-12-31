using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Views
{
    public class CategoryView
    {
        [Display(Name="Category ID")]
        public Guid CategoryId { get; set; }
        [Display(Name = "Category Name")]
        public string CategoryName { get; set; }
        [Display(Name = "Subcategories")]
        public IEnumerable<CategoryView> SubCategories { get; set; }
    }
}
