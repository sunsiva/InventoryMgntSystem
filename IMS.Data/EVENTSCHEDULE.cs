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
    
    public partial class EVENTSCHEDULE
    {
        public int ID { get; set; }
        public string TITLE { get; set; }
        public int SOMEIMPORTANTKEY { get; set; }
        public System.DateTime DATETIMESCHEDULED { get; set; }
        public int APPOINTMENTLENGTH { get; set; }
        public int STATUSENUM { get; set; }
        public string COMMENTS { get; set; }
        public Nullable<bool> ISACTIVE { get; set; }
        public Nullable<System.DateTime> MODIFIED_BY { get; set; }
        public Nullable<System.DateTime> MODIFIED_ON { get; set; }
        public Nullable<System.DateTime> CREATED_BY { get; set; }
        public Nullable<System.DateTime> CREATED_ON { get; set; }
    }
}
