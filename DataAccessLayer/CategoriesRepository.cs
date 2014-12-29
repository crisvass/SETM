using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common.Views;

namespace DataAccessLayer
{
    public class CategoriesRepository : ConnectionClass
    {
        public IEnumerable<CategoryView> GetCategories()
        {
            IEnumerable<CategoryView> categories =
                (from c in Entity.ProductCategories
                 where c.ParentId == null
                 select new CategoryView()
                 {
                     Id = c.Id,
                     CategoryName = c.Name
                 });

            List<CategoryView> categoriesWithSub = new List<CategoryView>();
            CategoryView cat;
            foreach (CategoryView cv in categories)
            {
                cat = cv;
                cat.SubCategories = (from c in Entity.ProductCategories
                                     where c.ParentId == cv.Id
                                     select new CategoryView()
                                     {
                                         Id = c.Id,
                                         CategoryName = c.Name
                                     });
                categoriesWithSub.Add(cat);
            }

            return categoriesWithSub;
        }

        public string GetCategoryName(int categoryId)
        {
            return Entity.ProductCategories.SingleOrDefault(pc => pc.Id == categoryId).Name;
        }
    }
}
