//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace DataAccess
{
    using System;
    using System.Collections.Generic;
    
    public partial class Price
    {
        public int ID { get; set; }
        public double Price1 { get; set; }
        public System.DateTime Date { get; set; }
        public string ProductCode { get; set; }
    
        public virtual Product Product { get; set; }
    }
}
