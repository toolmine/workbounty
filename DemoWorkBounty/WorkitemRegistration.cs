//------------------------------------------------------------------------------
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
    using System.Collections.Generic;
    
    public partial class WorkitemRegistration
    {
        public int WorkitemID { get; set; }
        public int UserID { get; set; }
        public bool IsExclusive { get; set; }
        public bool IsFavourite { get; set; }
        public bool IsRegistered { get; set; }
        public int WorkitemRegistrationID { get; set; }
    
        public virtual UserInfo UserInfo { get; set; }
        public virtual Workitem Workitem { get; set; }
    }
}
