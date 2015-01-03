using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common;

namespace BusinessLayer_WebServices.AdvancedSearchDecorator
{
    public class ByDescriptionKeywordSearch : AdvancedSearchDecorator
    {
        public ByDescriptionKeywordSearch() { }

        public ByDescriptionKeywordSearch(AdvancedSearchComponent component) : base(component) { }

        public string Keyword { get; set; }

        public override IEnumerable<Product> Search(IEnumerable<Product> products)
        {
            IEnumerable<Product> result = products;
            if(!string.IsNullOrEmpty(Keyword))
                result = products.Where(x => x.Description.ToLower().Contains(Keyword));

            if (AdvancedSearchComponent != null)
                result = AdvancedSearchComponent.Search(result);

            return result;
        }
    }
}
