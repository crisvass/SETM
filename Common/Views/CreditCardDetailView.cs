using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Views
{
    public class CreditCardDetailView
    {
        public int Id { get; set;}
        public int CreditCardTypeId { get; set; }
        public string CreditCardType { get; set; }
        public string CardHolderName { get; set; }
        public DateTime ExpiryDate { get; set; }
    }
}
