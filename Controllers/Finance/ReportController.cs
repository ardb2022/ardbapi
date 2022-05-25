using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SBWSFinanceApi.LL;
using SBWSFinanceApi.Models;

namespace SBWSFinanceApi.Controllers
{
    [EnableCors("AllowOrigin")] 
    [Route("api/Report")]
    [ApiController]
    public class ReportController : ControllerBase
    {
        FinanceReportLL _ll = new FinanceReportLL(); 
        [Route("PopulateDailyCashBook")]
        [HttpPost]
        public List<tt_cash_account> PopulateDailyCashBook([FromBody] p_report_param prp)
        {
           return _ll.PopulateDailyCashBook(prp);
        }

        [Route("PopulateCashCumTrial")]
        [HttpPost]
        public List<tt_cash_cum_trial> PopulateCashCumTrial([FromBody] p_report_param prp)
        {
           return _ll.PopulateCashCumTrial(prp);
        }
        
        [Route("PopulateDayScrollBook")]
        [HttpPost]
        public List<tt_day_scroll> PopulateDayScrollBook([FromBody] p_report_param prp)
        {      
           return _ll.PopulateDayScrollBook(prp);
        }

      [Route("GLTD")]
      [HttpPost]
      public List<tt_gl_trans> getGeneralLedgerTransactionDtls([FromBody] p_report_param prm)
      {
         return _ll.getGeneralLedgerTransactionDtls(prm);
      }

      [Route("GLTD2")]
      [HttpPost]
      public List<tt_gl_trans> getGeneralLedgerTransactionDtlsOrdrByVuchrID([FromBody] p_report_param prm)
      {
         return _ll.getGeneralLedgerTransactionDtlsOrdrByVuchrID(prm);
      }
        
      [Route("PopulateTrialBalance")]
      [HttpPost]
      public List<tt_trial_balance> PopulateTrialBalance([FromBody] p_report_param prm)
      {
         return _ll.PopulateTrialBalance(prm);
      }

    }
}

