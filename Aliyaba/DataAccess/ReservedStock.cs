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
    
    public partial class ReservedStock
    {
        public int ID { get; set; }
        public Nullable<int> StockID { get; set; }
        public int Quantity { get; set; }
        public Nullable<int> OrderID { get; set; }
        public bool IsPrepared { get; set; }
    
        public virtual Order Order { get; set; }
        public virtual Stock Stock { get; set; }
    }
}
