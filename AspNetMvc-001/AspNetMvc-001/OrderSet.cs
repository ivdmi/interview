//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace AspNetMvc_001
{
    using System;
    using System.Collections.Generic;
    
    public partial class OrderSet
    {
        public int OrderId { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public string Quantity { get; set; }
        public System.DateTime DateOrder { get; set; }
        public int CustomerId { get; set; }
        public int Customer_CustomerId { get; set; }
    
        public virtual CustomerSet CustomerSet { get; set; }
    }
}