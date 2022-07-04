﻿//------------------------------------------------------------------------------
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
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    using System.Data.Entity.Core.Objects;
    using System.Linq;
    
    public partial class TaskManagementEntities : DbContext
    {
        public TaskManagementEntities()
            : base("name=TaskManagementEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<Project> Projects { get; set; }
        public virtual DbSet<Resource> Resources { get; set; }
        public virtual DbSet<ResourceTask> ResourceTasks { get; set; }
        public virtual DbSet<Task> Tasks { get; set; }
        public virtual DbSet<TaskPredecessor> TaskPredecessors { get; set; }
    
        public virtual ObjectResult<GetProjectCost_Result> GetProjectCost()
        {
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<GetProjectCost_Result>("GetProjectCost");
        }
    
        public virtual ObjectResult<GetTaskCost_Result> GetTaskCost()
        {
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<GetTaskCost_Result>("GetTaskCost");
        }
    
        public virtual ObjectResult<GetResourceTaskDetails_Result> GetResourceTaskDetails()
        {
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<GetResourceTaskDetails_Result>("GetResourceTaskDetails");
        }
    }
}
