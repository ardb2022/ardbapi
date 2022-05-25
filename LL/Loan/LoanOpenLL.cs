
using System;
using System.Collections.Generic;
using SBWSDepositApi.Deposit;
using SBWSDepositApi.Models;
using SBWSFinanceApi.Config;
using SBWSFinanceApi.DL;
using SBWSFinanceApi.Models;
using SBWSFinanceApi.Utility;

namespace SBWSFinanceApi.LL
{
    public class LoanOpenLL
    {
       LoanOpenDL _dac = new LoanOpenDL(); 
        internal LoanOpenDM GetLoanData(tm_loan_all loan)
        {         
           
            return _dac.GetLoanData(loan);
        } 
        
         internal decimal F_GET_EFF_INTT_RT(p_loan_param prp)
        {         
           
            return _dac.F_GET_EFF_INTT_RT(prp);
        } 
        internal p_loan_param PopulateCropAmtDueDt(p_loan_param prp)
        {
                return _dac.PopulateCropAmtDueDt(prp);
        }
        
          internal String InsertLoanAccountOpeningData(LoanOpenDM loan)
        {         
           
            return _dac.InsertLoanAccountOpeningData(loan);
        } 
           internal String InsertLoanTransactionData(LoanOpenDM loan)
        {         
           
            return _dac.InsertLoanTransactionData(loan);
        } 
        
        internal string PopulateLoanAccountNumber(p_gen_param prp)
        {
           return _dac.PopulateLoanAccountNumber(prp);
        
        }
       internal int UpdateLoanAccountOpeningData(LoanOpenDM loan)
        {
           return _dac.UpdateLoanAccountOpeningData(loan);
        
        }

        internal tm_loan_all GetLoanAllWithChild(tm_loan_all loan)
        {
            return _dac.GetLoanAllWithChild(loan);
        }

        internal p_loan_param CalculateLoanInterest(p_loan_param prp)
        {
            return _dac.CalculateLoanInterest(prp);
        }

         internal List<p_loan_param> CalculateLoanAccWiseInterest(List<p_loan_param> prp)
        {
            return _dac.CalculateLoanAccWiseInterest(prp);
        }
       
        internal List<sm_kcc_param> GetSmKccParam()
        {
            return _dac.GetSmKccParam();            

        }

        internal string ApproveLoanAccountTranaction(p_gen_param pgp)
        {
            return _dac.ApproveLoanAccountTranaction(pgp);
        }
   
        internal List<sm_loan_sanction> GetSmLoanSanctionList()
        {
           return _dac.GetSmLoanSanctionList();
        }
        
        internal List<AccDtlsLov> GetLoanDtls(p_gen_param pgp)
        {         
           
            return _dac.GetLoanDtls(pgp);
        }        
        internal List<AccDtlsLov> GetLoanDtlsByID(p_gen_param pgp)
        {         
           
            return _dac.GetLoanDtlsByID(pgp);
        }
        internal List<tt_rep_sch> PopulateLoanRepSch(p_loan_param prp)
        {
             return _dac.PopulateLoanRepSch(prp);
        }

        KccMstDL _dackcc = new KccMstDL(); 
        internal string InsertKccData(KccMstDM acc)
        {
            return _dackcc.InsertKccData(acc);
        }

        internal int UpdateKccData(KccMstDM acc)
        {
            return _dackcc.UpdateKccData(acc);
        }

        internal int DeleteKccData(mm_kcc_member_dtls acc)
        {
            return _dackcc.DeleteKccData(acc);
        }

        internal KccMstDM GetKccData(mm_kcc_member_dtls td)
        {
            return _dackcc.GetKccData(td);
        }
    
    }
}