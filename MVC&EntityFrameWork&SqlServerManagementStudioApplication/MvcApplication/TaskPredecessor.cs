//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace MvcApplication
{
    using System;
    using System.Collections.Generic;
    
    public partial class TaskPredecessor
    {
        public int RelationshipId { get; set; }
        public Nullable<int> TaskId { get; set; }
        public Nullable<int> PredecessorId { get; set; }
    
        public virtual Task Task { get; set; }
        public virtual Task Task1 { get; set; }
    }
}
