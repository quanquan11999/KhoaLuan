﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace KhoaLuan.Models
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class QLTroEntities : DbContext
    {
        public QLTroEntities()
            : base("name=QLTroEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<Account> Accounts { get; set; }
        public virtual DbSet<AccStatu> AccStatus { get; set; }
        public virtual DbSet<Bill> Bills { get; set; }
        public virtual DbSet<BillDetail> BillDetails { get; set; }
        public virtual DbSet<ConfirmEmail> ConfirmEmails { get; set; }
        public virtual DbSet<Criterion> Criteria { get; set; }
        public virtual DbSet<District> Districts { get; set; }
        public virtual DbSet<Image> Images { get; set; }
        public virtual DbSet<Info> Infoes { get; set; }
        public virtual DbSet<MotelRoom> MotelRooms { get; set; }
        public virtual DbSet<MotelStatu> MotelStatus { get; set; }
        public virtual DbSet<Post> Posts { get; set; }
        public virtual DbSet<Province> Provinces { get; set; }
        public virtual DbSet<Renter> Renters { get; set; }
        public virtual DbSet<Ward> Wards { get; set; }
    }
}
