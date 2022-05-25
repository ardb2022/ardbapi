using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SBWSDepositApi.Deposit;
using SBWSDepositApi.Models;
using SBWSFinanceApi.LL;
using SBWSFinanceApi.Models;

namespace SBWSFinanceApi.Controllers
{
    [Route("api/Deposit")]
    [ApiController]
    [EnableCors("AllowOrigin")]

    public class DepositController : ControllerBase
    {
        AccountOpenLL _ll = new AccountOpenLL();
        [Route("InsertAccountOpeningData")]
        [HttpPost]
        public string InsertAccountOpeningData([FromBody] AccOpenDM tvd)
        {
            return _ll.InsertAccountOpeningData(tvd);
        }


        [Route("UpdateAccountOpeningData")]
        [HttpPost]
        public int UpdateAccountOpeningData([FromBody] AccOpenDM tvd)
        {
            return _ll.UpdateAccountOpeningData(tvd);
        }

        [Route("UpdateAccountOpeningDataOrg")]
        [HttpPost]
        public int UpdateAccountOpeningDataOrg([FromBody] AccOpenDM tvd)
        {
            return _ll.UpdateAccountOpeningDataOrg(tvd);
        }



        [Route("PopulateAccountNumber")]
        [HttpPost]
        public string PopulateAccountNumber([FromBody] p_gen_param tvd)
        {
            return _ll.PopulateAccountNumber(tvd);
        }
        [Route("F_CALCRDINTT_REG")]
        [HttpPost]
        public decimal F_CALCRDINTT_REG([FromBody] p_gen_param tvd)
        {
            return _ll.F_CALCRDINTT_REG(tvd);
        }
        [Route("F_CALCTDINTT_REG")]
        [HttpPost]
        public decimal F_CALCTDINTT_REG([FromBody] p_gen_param tvd)
        {
            return _ll.F_CALCTDINTT_REG(tvd);
        }
        [Route("F_CALC_SB_INTT")]
        [HttpPost]
        public decimal F_CALC_SB_INTT([FromBody] p_gen_param tvd)
        {
            return _ll.F_CALC_SB_INTT(tvd);
        }
        [Route("F_CAL_RD_PENALTY")]
        [HttpPost]
        public decimal F_CAL_RD_PENALTY([FromBody] p_gen_param tvd)
        {
            return _ll.F_CAL_RD_PENALTY(tvd);
        }
        [Route("GET_INT_RATE")]
        [HttpPost]
        public float GET_INT_RATE([FromBody] p_gen_param tvd)
        {
            return _ll.GET_INT_RATE(tvd);
        }
        [Route("GetCustMinSavingsAccNo")]
        [HttpPost]
        public string GetCustMinSavingsAccNo([FromBody] tm_deposit cust)
        {
            return _ll.GetCustMinSavingsAccNo(cust);
        }

        [Route("GetAccountOpeningTempData")]
        [HttpPost]
        public AccOpenDM GetAccountOpeningTempData([FromBody] tm_deposit td)
        {
            return _ll.GetAccountOpeningTempData(td);
        }


        [Route("GetAccountOpeningData")]
        [HttpPost]
        public AccOpenDM GetAccountOpeningData([FromBody] tm_deposit td)
        {
            return _ll.GetAccountOpeningData(td);
        }


        [Route("DeleteAccountOpeningData")]
        [HttpPost]
        public int DeleteAccountOpeningData([FromBody] td_def_trans_trf td)
        {
            return _ll.DeleteAccountOpeningData(td);
        }


        AccholderLL _ll1 = new AccholderLL();
        [Route("GetAccholder")]
        [HttpPost]
        public List<td_accholder> GetAccholder([FromBody] td_accholder tvd)
        {
            return _ll1.GetAccholder(tvd);
        }
        [Route("InsertAccholder")]
        [HttpPost]
        public decimal InsertAccholder([FromBody] td_accholder tvd)
        {
            return _ll1.InsertAccholder(tvd);
        }
        [Route("UpdateAccholder")]
        [HttpPost]
        public int UpdateAccholder([FromBody] td_accholder tvd)
        {
            return _ll1.UpdateAccholder(tvd);
        }
        [Route("DeleteAccholder")]
        [HttpPost]
        public int DeleteAccholder([FromBody] td_accholder tvd)
        {
            return _ll1.DeleteAccholder(tvd);
        }


        DepositLL _ll2 = new DepositLL();
        [Route("GetDeposit")]
        [HttpPost]
        public List<tm_deposit> GetDeposit([FromBody] tm_deposit tvd)
        {
            return _ll2.GetDeposit(tvd);
        }
        [Route("ApproveAccountTranaction")]
        [HttpPost]
        public string ApproveAccountTranaction([FromBody] p_gen_param tvd)
        {
            return _ll2.ApproveAccountTranaction(tvd);
        }

        [Route("isDormantAccount")]
        [HttpPost]
        public int isDormantAccount([FromBody] tm_deposit tvd)
        {
            return _ll2.isDormantAccount(tvd);
        }

        [Route("GetPrevTransaction")]
        [HttpPost]
        public List<td_def_trans_trf> GetPrevTransaction([FromBody] tm_deposit tvd)
        {
            return _ll2.GetPrevTransaction(tvd);
        }

        [Route("GetDepositAddlInfo")]
        [HttpPost]
        public AccOpenDM GetDepositAddlInfo([FromBody] tm_deposit tvd)
        {
            return _ll2.GetDepositAddlInfo(tvd);
        }

        [Route("InsertDeposit")]
        [HttpPost]
        public decimal InsertDeposit([FromBody] tm_deposit tvd)
        {
            return _ll2.InsertDeposit(tvd);
        }
        [Route("UpdateDeposit")]
        [HttpPost]
        public int UpdateDeposit([FromBody] tm_deposit tvd)
        {
            return _ll2.UpdateDeposit(tvd);
        }
        [Route("DeleteDeposit")]
        [HttpPost]
        public int DeleteDeposit([FromBody] tm_deposit tvd)
        {
            return _ll2.DeleteDeposit(tvd);
        }
        [Route("GetDepositView")]
        [HttpPost]
        public List<tm_deposit> GetDepositView([FromBody] tm_deposit tvd)
        {
            return _ll2.GetDepositView(tvd);
        }
        [Route("GetDepositWithChild")]
        [HttpPost]
        public List<tm_depositall> GetDepositWithChild([FromBody] tm_depositall tvd)
        {
            return _ll2.GetDepositWithChild(tvd);
        }

        [Route("UpdateDepositLockUnlock")]
        [HttpPost]
        public int UpdateDepositLockUnlock([FromBody] tm_deposit tvd)
        {
            return _ll2.UpdateDepositLockUnlock(tvd);
        }
        [Route("GetDailyDeposit")]
        [HttpPost]
        public List<tm_daily_deposit> GetDailyDeposit(tm_deposit dep)
        {
              return _ll2.GetDailyDeposit(dep);
        }
        IntroducerLL _ll3 = new IntroducerLL();
        [Route("GetIntroducer")]
        [HttpPost]
        public List<td_introducer> GetIntroducer([FromBody] td_introducer tvd)
        {
            return _ll3.GetIntroducer(tvd);
        }
        [Route("InsertIntroducer")]
        [HttpPost]
        public decimal InsertIntroducer([FromBody] td_introducer tvd)
        {
            return _ll3.InsertIntroducer(tvd);
        }
        [Route("UpdateIntroducer")]
        [HttpPost]
        public int UpdateIntroducer([FromBody] td_introducer tvd)
        {
            return _ll3.UpdateIntroducer(tvd);
        }
        [Route("DeleteIntroducer")]
        [HttpPost]
        public int DeleteIntroducer([FromBody] td_introducer tvd)
        {
            return _ll3.DeleteIntroducer(tvd);
        }


        NomineeLL _ll4 = new NomineeLL();
        [Route("GetNominee")]
        [HttpPost]
        public List<td_nominee> GetNominee([FromBody] td_nominee tvd)
        {
            return _ll4.GetNominee(tvd);
        }
        [Route("InsertNominee")]
        [HttpPost]
        public decimal InsertNominee([FromBody] td_nominee tvd)
        {
            return _ll4.InsertNominee(tvd);
        }
        [Route("UpdateNominee")]
        [HttpPost]
        public int UpdateNominee([FromBody] td_nominee tvd)
        {
            return _ll4.UpdateNominee(tvd);
        }
        [Route("DeleteNominee")]
        [HttpPost]
        public int DeleteNominee([FromBody] td_nominee tvd)
        {
            return _ll4.DeleteNominee(tvd);
        }


        SignatoryDL _ll5 = new SignatoryDL();
        [Route("GetSignatory")]
        [HttpPost]
        public List<td_signatory> GetSignatory([FromBody] td_signatory tvd)
        {
            return _ll5.GetSignatory(tvd);
        }
        [Route("InsertSignatory")]
        [HttpPost]
        public decimal InsertSignatory([FromBody] td_signatory tvd)
        {
            return _ll5.InsertSignatory(tvd);
        }
        [Route("UpdateSignatory")]
        [HttpPost]
        public int UpdateSignatory([FromBody] td_signatory tvd)
        {
            return _ll5.UpdateSignatory(tvd);
        }
        [Route("DeleteSignatory")]
        [HttpPost]
        public int DeleteSignatory([FromBody] td_signatory tvd)
        {
            return _ll5.DeleteSignatory(tvd);
        }

        RDInstallmentLL _RDInstallment = new RDInstallmentLL();
        [Route("GetRDInstallment")]
        [HttpPost]
        public List<td_rd_installment> GetRDInstallment([FromBody] td_rd_installment tvd)
        {
            return _RDInstallment.GetRDInstallment(tvd);
        }

        InttDetailsLL _InttDetails = new InttDetailsLL();
        [Route("GetInttDetails")]
        [HttpPost]
        public List<td_intt_dtls> GetInttDetails([FromBody] td_intt_dtls tvd)
        {
            return _InttDetails.GetInttDetails(tvd);
        }

        DepositRenewTmpLL _DepositRenewTmpLL = new DepositRenewTmpLL();
        [Route("GetDepositRenewTmp")]
        [HttpPost]
        public List<tm_deposit> GetDepositRenewTmp([FromBody] tm_deposit tvd)
        {
            return _DepositRenewTmpLL.GetDepositRenewTmp(tvd);
        }


        AccountTransLL _ll6 = new AccountTransLL();
        [Route("GetShadowBalance")]
        [HttpPost]
        public decimal GetShadowBalance([FromBody] tm_deposit td)
        {
            return _ll6.GetShadowBalance(td);
        }

        DepositLL __ll = new DepositLL();
        [Route("GetAccDtls")]
        [HttpPost]
        public List<AccDtlsLov> GetAccDtls([FromBody] p_gen_param prm)
        {
            // ad_acc_type_cd NUMBER,as_cust_name 
            return __ll.GetAccDtls(prm);
        }
        [Route("GetCustDtls")]
        [HttpPost]
        public List<mm_customer> GetCustDtls([FromBody] p_gen_param prm)
        {
            // ad_acc_type_cd NUMBER,as_cust_name 
            return __ll.GetCustDtls(prm);
        }

        NeftPayLL _NeftPayLLLL = new NeftPayLL();
        [Route("GetNeftOutDtls")]
        [HttpPost]
        public List<td_outward_payment> GetNeftOutDtls([FromBody] td_outward_payment tvd)
        {
            return _NeftPayLLLL.GetNeftOutDtls(tvd);
        }

        [Route("InsertNeftOutDtls")]
        [HttpPost]
        public int InsertNeftOutDtls([FromBody] td_outward_payment tvd)
        {
            return _NeftPayLLLL.InsertNeftOutDtls(tvd);
        }

        [Route("GetIfscCode")]
        [HttpPost]
        public List<mm_ifsc_code> GetIfscCode([FromBody] td_outward_payment ifsc)
        {
            return _NeftPayLLLL.GetIfscCode(ifsc.bene_ifsc_code);  
        }
        [Route("UpdateNeftOutDtls")]
        [HttpPost]
        public int UpdateNeftOutDtls([FromBody] td_outward_payment nom)
        {
            return _NeftPayLLLL.UpdateNeftOutDtls(nom);  
        }       

         [Route("ApproveNeftPaymentTrans")]
        [HttpPost]
        public int ApproveNeftPaymentTrans([FromBody] td_outward_payment nom)
        {
            return _NeftPayLLLL.ApproveNeftPaymentTrans(nom);  
        }
        [Route("DeleteNeftOutDtls")]
        [HttpPost]
        public int DeleteNeftOutDtls([FromBody] td_outward_payment nom)
        {
            return _NeftPayLLLL.DeleteNeftOutDtls(nom);  
        }
         [Route("GetNeftCharge")]
        [HttpPost]
        public decimal GetNeftCharge([FromBody] p_gen_param pgp)
        {
            return _NeftPayLLLL.GetNeftCharge(pgp);  
        }
        
        
    }
}
