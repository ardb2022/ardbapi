using System;

namespace SBWSFinanceApi.Models
{
    public class tt_gl_trans
    {
        public int acc_cd { get; set; }
        public DateTime voucher_dt { get; set; }
        public decimal dr_amt { get; set; }
        public decimal cr_amt { get; set; }
        public decimal trans_month { get; set; }
        public decimal trans_year { get; set; }
        public decimal opng_bal {get;set;}
    }
}