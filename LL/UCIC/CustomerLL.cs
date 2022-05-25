
using System;
using System.Collections.Generic;
using SBWSDepositApi.Models;
using SBWSFinanceApi.Config;
using SBWSFinanceApi.DL;
using SBWSFinanceApi.Models;
using SBWSFinanceApi.Utility;

namespace SBWSFinanceApi.LL
{
    public class CustomerLL
    {
       CustomerDL _dac = new CustomerDL(); 
        internal List<mm_customer> GetCustomerDtls(mm_customer pmc)
        {         
           
            return _dac.GetCustomerDtls(pmc);
        }  
        internal decimal InsertCustomerDtls(mm_customer pmc)
        {         
           
            return _dac.InsertCustomerDtls(pmc);
        }
        internal int UpdateCustomerDtls(mm_customer pmc)
        {         
           
            return _dac.UpdateCustomerDtls(pmc);
        }  
        internal int DeleteCustomerDtls(mm_customer pmc)
        {         
           
            return _dac.DeleteCustomerDtls(pmc);
        }   
         
          internal List<tm_deposit> GetDepositDtls(mm_customer pmc)
        {

            return _dac.GetDepositDtls(pmc);
        }

        
    }
}