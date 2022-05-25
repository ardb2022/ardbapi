using System;

namespace SBWSFinanceApi.Models
{
    public class standing_instr
    {         
       public int    acc_type_from {get; set;}
       public string acc_num_from {get; set;}
       public int    acc_type_to {get; set;}
       public string acc_num_to {get; set;}
       public string instr_status {get; set;}
       public DateTime first_trf_dt {get; set;}
       public string periodicity {get; set;}
       public string prn_intt_flag {get; set;}
       public decimal amount {get; set;}
       public Int64 srl_no  {get; set;}
    }
}