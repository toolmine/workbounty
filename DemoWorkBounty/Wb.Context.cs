﻿//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace DemoWorkBounty
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class WorkBountyDBEntities2 : DbContext
    {
        public WorkBountyDBEntities2()
            : base("name=WorkBountyDBEntities2")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public DbSet<Team> Teams { get; set; }
        public DbSet<UserInfo> UserInfoes { get; set; }
        public DbSet<Workitem> Workitems { get; set; }
        public DbSet<WorkItemAssignment> WorkItemAssignments { get; set; }
        public DbSet<WorkitemStatu> WorkitemStatus { get; set; }
        public DbSet<WorkitemDistribution> WorkitemDistributions { get; set; }
        public DbSet<WorkitemHistory> WorkitemHistories { get; set; }
        public DbSet<WorkitemRegistration> WorkitemRegistrations { get; set; }
    }
}
