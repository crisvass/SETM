//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Common
{
    using System;
    using System.Collections.Generic;
    
    public partial class CreditCardType
    {
        public CreditCardType()
        {
            this.CreditCardDetails = new HashSet<CreditCardDetail>();
        }
    
        public int Id { get; set; }
        public string CreditCardType1 { get; set; }
        public bool IsDeleted { get; set; }
    
        public virtual ICollection<CreditCardDetail> CreditCardDetails { get; set; }
    }
}
