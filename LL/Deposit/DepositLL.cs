
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
    public class DepositLL
    {
        DepositDL _dac = new DepositDL();
        internal List<tm_deposit> GetDeposit(tm_deposit pmc)
        {
            if (pmc.temp_flag == 1)
                return _dac.GetDepositTemp(pmc);
            else
                return _dac.GetDeposit(pmc);
        }

        internal decimal InsertDeposit(tm_deposit pmc)
        {
            if (pmc.temp_flag == 1)
                return _dac.InsertDepositTemp(pmc);
            else
                return _dac.InsertDeposit(pmc);
        }

        internal int UpdateDeposit(tm_deposit pmc)
        {
            if (pmc.temp_flag == 1)
                return _dac.UpdateDepositTemp(pmc);
            else
                return _dac.UpdateDeposit(pmc);
        }

        internal int DeleteDeposit(tm_deposit pmc)
        {
            if (pmc.temp_flag == 1)
                return _dac.DeleteDepositTemp(pmc);
            else
                return _dac.DeleteDeposit(pmc);
        }

        internal List<tm_deposit> GetDepositView(tm_deposit pmc)
        {

            return _dac.GetDepositView(pmc);
        }

        internal List<tm_depositall> GetDepositWithChild(tm_depositall dep)
        {
            return _dac.GetDepositWithChild(dep);
        }

        internal string ApproveAccountTranaction(p_gen_param pgp)
        {
            return _dac.ApproveAccountTranaction(pgp);
        }

        internal int isDormantAccount(tm_deposit dep)
        {
            return _dac.isDormantAccount(dep);
        }
        internal List<td_def_trans_trf> GetPrevTransaction(tm_deposit tvd)
        {
            return _dac.GetPrevTransaction(tvd);
        }

        internal int UpdateDepositLockUnlock(tm_deposit pmc)
        {
            return _dac.UpdateDepositLockUnlock(pmc);
        }

        internal AccOpenDM GetDepositAddlInfo(tm_deposit td)
        {
            return _dac.GetDepositAddlInfo(td);
        }
        internal List<AccDtlsLov> GetAccDtls(p_gen_param prm)
        {
            return _dac.GetAccDtls(prm);
        }
        internal List<mm_customer> GetCustDtls(p_gen_param prm)
        {
            return _dac.GetCustDtls(prm);
        }
         internal List<tm_daily_deposit> GetDailyDeposit(tm_deposit dep)
        {
            return _dac.GetDailyDeposit(dep);
        }
    }
}