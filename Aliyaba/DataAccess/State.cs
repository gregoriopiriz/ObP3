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
    
    public partial class State
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public System.DateTime Date { get; set; }
        public Nullable<int> EmployeeID { get; set; }
        public int OrderID { get; set; }
    
        public virtual Employee Employee { get; set; }
        public virtual Order Order { get; set; }
    }
}
