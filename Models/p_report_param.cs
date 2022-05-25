using System;

namespace SBWSFinanceApi.Models
{
    public class p_report_param
    {
         public int acc_cd {get; set;}
         public string brn_cd {get; set;}
         public DateTime from_dt {get; set;}  
         public DateTime to_dt {get; set;} 
         public DateTime trial_dt {get; set;}
         public int pl_acc_cd {get; set;}
         public int gp_acc_cd {get; set;}         
         public int ad_from_acc_cd {get; set;}
         public int ad_to_acc_cd {get; set;}
         
    }
}