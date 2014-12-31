using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common;
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
                     CategoryId = c.Id,
                     CategoryName = c.Name
                 });

            List<CategoryView> categoriesWithSub = new List<CategoryView>();
            CategoryView cat;
            foreach (CategoryView cv in categories)
            {
                cat = cv;
                cat.SubCategories = (from c in Entity.ProductCategories
                                     where c.ParentId == cv.CategoryId
                                     select new CategoryView()
                                     {
                                         CategoryId = c.Id,
                                         CategoryName = c.Name
                                     });
                categoriesWithSub.Add(cat);
            }

            return categoriesWithSub;
        }

        public string GetCategoryName(Guid categoryId)
        {
            return Entity.ProductCategories.SingleOrDefault(pc => pc.Id == categoryId).Name;
        }

        public ProductCategory GetCategory(Guid id)
        {
            return Entity.ProductCategories.SingleOrDefault(pc => pc.Id == id);
        }

        public CategoryView GetCategoryView(Guid id)
        {
            return Entity.ProductCategories.Select(pc => new CategoryView()
            {
                CategoryId = pc.Id,
                CategoryName = pc.Name
            }).SingleOrDefault(pc => pc.CategoryId == id);
        }

        public IEnumerable<CategoryView> GetSubCategories(Guid id)
        {
            return Entity.ProductCategories.Where(pc => pc.ParentId == id)
                .Select(pc => new CategoryView()
            {
                CategoryId = pc.Id,
                CategoryName = pc.Name
            });
        }

        public void AddCategory(ProductCategory pc)
        {
            Entity.ProductCategories.Add(pc);
            Entity.SaveChanges();
        }

        public void UpdateCategory(ProductCategory pc)
        {
            ProductCategory category = GetCategory(pc.Id);
            category.Name = pc.Name;
            Entity.SaveChanges();
        }

        public void DeleteCategory(Guid id)
        {
            Entity.ProductCategories.Remove(GetCategory(id));
            Entity.SaveChanges();
        }

        public void DeleteSubCategories(Guid id)
        {
            IEnumerable<CategoryView> subcategories = GetSubCategories(id);
            foreach (CategoryView cv in subcategories)
            {
                DeleteCategory(cv.CategoryId);
            }
        }

        public bool CategoryIsUsed(Guid id)
        {
            return Entity.Products.Where(p => p.ProductCategoryId == id).Count() > 0;
        }

        public bool SubCategoriesAreUsed(Guid id)
        {
            bool subcategoryIsUsed = false;
            IEnumerable<CategoryView> subcategories = GetSubCategories(id);
            foreach (CategoryView cv in subcategories)
            {
                if(Entity.Products.Where(p => p.ProductCategoryId == cv.CategoryId).Count() > 0)
                {
                    subcategoryIsUsed = true;
                }
            }
            return subcategoryIsUsed;
        }
    }
}
