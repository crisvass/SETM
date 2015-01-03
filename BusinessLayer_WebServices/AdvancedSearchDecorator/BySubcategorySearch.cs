using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common;

namespace BusinessLayer_WebServices.AdvancedSearchDecorator
{
    public class BySubcategorySearch : AdvancedSearchDecorator
    {
        public BySubcategorySearch() { }

        public BySubcategorySearch(AdvancedSearchComponent component) : base(component) { }

        public Guid SubcategoryId { get; set; }

        public override IEnumerable<Product> Search(IEnumerable<Product> products)
        {
            IEnumerable<Product> result = products;
            if (SubcategoryId != null && SubcategoryId != Guid.Parse("00000000-0000-0000-0000-000000000000"))
                result = products.Where(x => x.ProductCategoryId == SubcategoryId);

            if (AdvancedSearchComponent != null)
                result = AdvancedSearchComponent.Search(result);

            return result;
        }
    }
}
