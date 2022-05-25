using System;
using System.Collections.Generic;

namespace SBWSFinanceApi.Models
{
    public class tm_loan_sanction
    {
         public string loan_id {get;set;} 
         public decimal sanc_no {get;set;} 
         public DateTime? sanc_dt {get;set;} 
         public string created_by {get;set;} 
         public DateTime? created_dt {get;set;} 
         public string modified_by {get;set;} 
         public DateTime? modified_dt {get;set;} 
         public string approval_status {get;set;} 
         public string approved_by {get;set;} 
         public DateTime? approved_dt {get;set;} 
         public string memo_no {get;set;}
    }
}