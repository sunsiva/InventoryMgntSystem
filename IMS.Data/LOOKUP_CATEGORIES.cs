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
    
    public partial class LOOKUP_CATEGORIES
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public LOOKUP_CATEGORIES()
        {
            this.LOOKUP_DATA = new HashSet<LOOKUP_DATA>();
        }
    
        public System.Guid LOOKUPCATEGORYID { get; set; }
        public string LOOKUPCATEGORYCODE { get; set; }
        public string LOOKUPCATEGORYDESC { get; set; }
        public Nullable<bool> ISAPPSPECIFIC { get; set; }
        public Nullable<bool> ISORGSPECIFIC { get; set; }
        public bool ISACTIVE { get; set; }
        public Nullable<System.DateTime> CREATEDON { get; set; }
        public string CREATEDBY { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<LOOKUP_DATA> LOOKUP_DATA { get; set; }
    }
}
