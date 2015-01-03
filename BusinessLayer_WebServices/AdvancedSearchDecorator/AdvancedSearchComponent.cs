using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common;

namespace BusinessLayer_WebServices.AdvancedSearchDecorator
{
    public abstract class AdvancedSearchComponent
    {
        public abstract IEnumerable<Product> Search(IEnumerable<Product> products);
    }
}
