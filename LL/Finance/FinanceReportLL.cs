
using System;
using System.Collections.Generic;
using SBWSFinanceApi.Config;
using SBWSFinanceApi.DL;
using SBWSFinanceApi.Models;
using SBWSFinanceApi.Utility;

namespace SBWSFinanceApi.LL
{
    public class FinanceReportLL
    {
       CashCumTrialDL _dacCashCumTrialDL = new CashCumTrialDL(); 
        internal List<tt_cash_cum_trial> PopulateCashCumTrial(p_report_param prp)
        {         
            return _dacCashCumTrialDL.PopulateCashCumTrial(prp);
        }  

         TrialBalanceDL _dacTrialBalanceDL = new TrialBalanceDL(); 
        internal List<tt_trial_balance> PopulateTrialBalance(p_report_param prp)
        {         
            return _dacTrialBalanceDL.PopulateTrialBalance(prp);
        }  

        DailyCashBookDL _dacDailyCashBookDL = new DailyCashBookDL(); 
        internal List<tt_cash_account> PopulateDailyCashBook(p_report_param prp)
        {         
            return _dacDailyCashBookDL.PopulateDailyCashBook(prp);
        }

        DayScrollBookDL _dacDayScrollBookDL = new DayScrollBookDL(); 
        internal List<tt_day_scroll> PopulateDayScrollBook(p_report_param prp)
        {         
            return _dacDayScrollBookDL.PopulateDayScrollBook(prp);
        }       
        internal List<tt_gl_trans> getGeneralLedgerTransactionDtls(p_report_param prm)
        {
            var _dac = new RptGeneralLedgerTransactionDtlsDL();
            return _dac.getGeneralLedgerTransactionDtls(prm);
        }
        internal List<tt_gl_trans> getGeneralLedgerTransactionDtlsOrdrByVuchrID(p_report_param prm)
        {
            var _dac = new RptGeneralLedgerTransactionDtlsDL();
            return _dac.getGeneralLedgerTransactionDtls(prm, true);
        }
    }
}