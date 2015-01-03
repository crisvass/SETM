using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer_WebServices.AdvancedSearchDecorator
{
    public abstract class AdvancedSearchDecorator: AdvancedSearchComponent
    {
        public AdvancedSearchComponent AdvancedSearchComponent { get; set; }

        public AdvancedSearchDecorator() { }

        public AdvancedSearchDecorator(AdvancedSearchComponent component)
        {
            AdvancedSearchComponent = component;
        }
    }
}
