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
    
    public partial class Seller
    {
        public string Id { get; set; }
        public bool RequiresDelivery { get; set; }
        public string IBANNumber { get; set; }
    
        public virtual ApplicationUser ApplicationUser { get; set; }
    }
}
