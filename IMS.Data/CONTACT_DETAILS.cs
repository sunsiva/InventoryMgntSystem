//------------------------------------------------------------------------------
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
    using System.Collections.Generic;
    
    public partial class CONTACT_DETAILS
    {
        public System.Guid CONT_ID { get; set; }
        public System.Guid COMP_ID { get; set; }
        public string CONTACT_TYPE { get; set; }
        public string CONTACT_MAIL { get; set; }
        public string CONTACT_NAME { get; set; }
        public string CONTACT_PHONE { get; set; }
        public string ALTERNATECONTACT_NAME { get; set; }
        public string ALTERNATECONTACT_PHONE { get; set; }
        public string CONTACT_FAX { get; set; }
        public string WEBSITE { get; set; }
        public System.Guid LOC_ID { get; set; }
        public string LANDMARK { get; set; }
        public string BRANCH_NAME { get; set; }
        public string BRANCH_CODE { get; set; }
        public string BRANCH_DESC { get; set; }
        public bool ISCUSTOMERHEADOFFICE { get; set; }
        public bool ISACTIVE { get; set; }
        public Nullable<System.Guid> MODIFIED_BY { get; set; }
        public Nullable<System.DateTime> MODIFIED_ON { get; set; }
        public System.Guid CREATED_BY { get; set; }
        public System.DateTime CREATED_ON { get; set; }
    
        public virtual LOCATION_MASTER LOCATION_MASTER { get; set; }
    }
}
