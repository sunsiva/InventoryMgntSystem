﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace IMS.Data
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class IMSEntities : DbContext
    {
        public IMSEntities()
            : base("name=IMSEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<C__MigrationHistory> C__MigrationHistory { get; set; }
        public virtual DbSet<AspNetRole> AspNetRoles { get; set; }
        public virtual DbSet<AspNetUserClaim> AspNetUserClaims { get; set; }
        public virtual DbSet<AspNetUserLogin> AspNetUserLogins { get; set; }
        public virtual DbSet<AspNetUser> AspNetUsers { get; set; }
        public virtual DbSet<COMPANY> COMPANies { get; set; }
        public virtual DbSet<CONTACT_DETAILS> CONTACT_DETAILS { get; set; }
        public virtual DbSet<CUST_STATUS_HISTORY> CUST_STATUS_HISTORY { get; set; }
        public virtual DbSet<CUSTOMER> CUSTOMERs { get; set; }
        public virtual DbSet<CUSTXPROD> CUSTXPRODs { get; set; }
        public virtual DbSet<EVENTSCHEDULE> EVENTSCHEDULEs { get; set; }
        public virtual DbSet<EXCEPTION_LOGGING> EXCEPTION_LOGGING { get; set; }
        public virtual DbSet<LOCATION_MASTER> LOCATION_MASTER { get; set; }
        public virtual DbSet<LOOKUP_CATEGORIES> LOOKUP_CATEGORIES { get; set; }
        public virtual DbSet<LOOKUP_DATA> LOOKUP_DATA { get; set; }
        public virtual DbSet<PRODUCT> PRODUCTS { get; set; }
        public virtual DbSet<PURCHASE_ORDER> PURCHASE_ORDER { get; set; }
        public virtual DbSet<QUOTATION> QUOTATIONs { get; set; }
        public virtual DbSet<RATING_MASTER> RATING_MASTER { get; set; }
        public virtual DbSet<RFQ> RFQs { get; set; }
        public virtual DbSet<UserXRole> UserXRoles { get; set; }
        public virtual DbSet<WORKORDER> WORKORDERs { get; set; }
    }
}