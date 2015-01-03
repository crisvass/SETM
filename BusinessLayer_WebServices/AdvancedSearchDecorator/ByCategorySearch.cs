using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common;
using DataAccessLayer;

namespace BusinessLayer_WebServices.AdvancedSearchDecorator
{
    public class ByCategorySearch : AdvancedSearchDecorator
    {
        public ByCategorySearch() { }

        public ByCategorySearch(AdvancedSearchComponent component) : base(component) { }

        public Guid CategoryId { get; set; }

        public override IEnumerable<Product> Search(IEnumerable<Product> products)
        {
            ProductsRepository pr = new ProductsRepository();

            IEnumerable<Product> result = products;
            if (CategoryId != null && CategoryId != Guid.Parse("00000000-0000-0000-0000-000000000000"))
                result = pr.SearchByCategory(products, CategoryId);

            if (AdvancedSearchComponent != null)
                result = AdvancedSearchComponent.Search(result);

            return result;
        }
    }
}
