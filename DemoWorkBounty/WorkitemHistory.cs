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
    
    public partial class WorkitemHistory
    {
        public int WorkitemID { get; set; }
        public int WorkitemStatusID { get; set; }
        public int UpdatedBy { get; set; }
        public System.DateTime UpdatedDateTIme { get; set; }
        public int WorkitemHistoryID { get; set; }
    
        public virtual UserInfo UserInfo { get; set; }
        public virtual Workitem Workitem { get; set; }
        public virtual WorkitemStatu WorkitemStatu { get; set; }
    }
}
