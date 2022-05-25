using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SBWSDepositApi.Models;
using SBWSFinanceApi.LL;
using SBWSFinanceApi.Models;

namespace SBWSFinanceApi.Controllers
{
    
    [Route("api/Loan")]
    [ApiController]  
    [EnableCors("AllowOrigin")] 
    public class LoanController : ControllerBase
    {
        LoanOpenLL _ll = new LoanOpenLL(); 
         
        [Route("F_GET_EFF_INTT_RT")]
        [HttpPost]
        public decimal F_GET_EFF_INTT_RT(p_loan_param prp)
        {         
            return _ll.F_GET_EFF_INTT_RT(prp);
        } 
        
        [Route("PopulateCropAmtDueDt")]
        [HttpPost]
        public p_loan_param PopulateCropAmtDueDt(p_loan_param prp)
        {         
            return _ll.PopulateCropAmtDueDt(prp); 
        }
        
        [Route("GetLoanData")]
        [HttpPost]
         public LoanOpenDM GetLoanData(tm_loan_all loan)
        {         
           
            return _ll.GetLoanData(loan);
        } 

        [Route("InsertLoanAccountOpeningData")]
        [HttpPost]
         public String InsertLoanAccountOpeningData(LoanOpenDM loan)
        {         
           return _ll.InsertLoanAccountOpeningData(loan);
        } 
        [Route("InsertLoanTransactionData")]
        [HttpPost]
         public String InsertLoanTransactionData(LoanOpenDM loan)
        {         
           return _ll.InsertLoanTransactionData(loan);
        } 
        
        
         [Route("PopulateLoanAccountNumber")]
        [HttpPost]
         public String PopulateLoanAccountNumber(p_gen_param prp)
        {         
           
            return _ll.PopulateLoanAccountNumber(prp);
        }

         [Route("UpdateLoanAccountOpeningData")]
        [HttpPost]
         public int UpdateLoanAccountOpeningData(LoanOpenDM loan)
        {         
           
            return _ll.UpdateLoanAccountOpeningData(loan);
        } 
        [Route("GetLoanAllWithChild")]
        [HttpPost]
         public tm_loan_all GetLoanAllWithChild(tm_loan_all loan)
        {         
           
            return _ll.GetLoanAllWithChild(loan);
        }
         [Route("CalculateLoanInterest")]
        [HttpPost]
         public p_loan_param CalculateLoanInterest(p_loan_param loan)
        {         
           
            return _ll.CalculateLoanInterest(loan);
        }

         [Route("CalculateLoanAccWiseInterest")]
        [HttpPost]
         public List<p_loan_param> CalculateLoanAccWiseInterest(List<p_loan_param> loan)
        {         
           
            return _ll.CalculateLoanAccWiseInterest(loan);
        }
         [Route("GetSmKccParam")]
        [HttpPost]
         public List<sm_kcc_param> GetSmKccParam()
        {         
           
            return _ll.GetSmKccParam();
        }

        [Route("ApproveLoanAccountTranaction")]
        [HttpPost]
         public string ApproveLoanAccountTranaction(p_gen_param pgp)
        {         
           
            return _ll.ApproveLoanAccountTranaction(pgp);
        }

        [Route("GetSmLoanSanctionList")]
        [HttpPost]
        public List<sm_loan_sanction> GetSmLoanSanctionList()
        { 
           return _ll.GetSmLoanSanctionList();
        }
        
        [Route("GetLoanDtls")]
        [HttpPost]
         public List<AccDtlsLov> GetLoanDtls(p_gen_param pgp)
        {         
           
            return _ll.GetLoanDtls(pgp);
        }

        [Route("GetLoanDtlsByID")]
        [HttpPost]
         public List<AccDtlsLov> GetLoanDtlsByID(p_gen_param pgp)
        {         
           
            return _ll.GetLoanDtlsByID(pgp);
        }
        
        [Route("PopulateLoanRepSch")]
        [HttpPost]
         public List<tt_rep_sch> PopulateLoanRepSch(p_loan_param prp)
        {         
            return _ll.PopulateLoanRepSch(prp);
        }
        
        [Route("GetKccData")]
        [HttpPost]
         public KccMstDM GetKccData(mm_kcc_member_dtls loan)
        {         
           
            return _ll.GetKccData(loan);
        } 

        [Route("InsertKccData")]
        [HttpPost]
         public String InsertKccData(KccMstDM loan)
        {         
           return _ll.InsertKccData(loan);
        } 
        [Route("UpdateKccData")]
        [HttpPost]
         public int UpdateKccData(KccMstDM loan)
        {         
           return _ll.UpdateKccData(loan);
        } 
        
        
        [Route("DeleteKccData")]
        [HttpPost]
         public int DeleteKccData(mm_kcc_member_dtls loan)
        {         
           
            return _ll.DeleteKccData(loan);
        }  
        
        
       
    }
}
   