﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace DAN_LI_Milica_Karetic.Model
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class HospitalDBEntities : DbContext
    {
        public HospitalDBEntities()
            : base("name=HospitalDBEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<tblDoctor> tblDoctors { get; set; }
        public virtual DbSet<tblSickLeave> tblSickLeaves { get; set; }
        public virtual DbSet<tblUser> tblUsers { get; set; }
    }
}