using System;

namespace SBWSDepositApi.Models
{
  public class td_introducer: BaseModel
  {
    public string brn_cd               {get; set;}
    public int    acc_type_cd          {get; set;}
    public string    acc_num              {get; set;} 
    public Int16    srl_no               {get; set;} 
    public string introducer_name      {get; set;} 
    public int    introducer_acc_type  {get; set;} 
    public string introducer_acc_num   {get; set;} 
    //public int temp_flag {get;set;}
  }
}