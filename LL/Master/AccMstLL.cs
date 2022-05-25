
using System;
using System.Collections.Generic;
using SBWSFinanceApi.Config;
using SBWSFinanceApi.DL;
using SBWSFinanceApi.Models;
using SBWSFinanceApi.Utility;

namespace SBWSFinanceApi.LL
{
    public class AccMstLL
    {
       AccMstDL _dac = new AccMstDL(); 
        public List<m_acc_master> GetAccountMaster()
        {
           return _dac.GetAccountMaster();
        }
        public List<mm_acc_type> GetAccountTypeMaster()
        {
           return _dac.GetAccountTypeMaster();
        }
        public List<mm_constitution> GetConstitution()
        {
           return _dac.GetConstitution();
        }
         public List<mm_oprational_intr> GetOprationalInstr()
        {
           return _dac.GetOprationalInstr();
        }
        
        public List<m_user_master> GetUserDtls(m_user_master mum)
        {
           return _dac.GetUserDtls(mum);
        }

        public int UpdateUserstatus(m_user_master mum)
         {
           return _dac.UpdateUserstatus(mum);
        }

        public List<mm_category> GetCategoryMaster()
        {
           return _dac.GetCategoryMaster();
        }
        public List<mm_state> GetStateMaster()
        {
           return _dac.GetStateMaster();
        }
        public List<mm_dist> GetDistMaster()
        {
           return _dac.GetDistMaster();
        }
        public List<mm_vill> GetVillageMaster()
        {
           return _dac.GetVillageMaster();
        }
        public List<mm_service_area> GetServiceAreaMaster()
        {
           return _dac.GetServiceAreaMaster();
        }
        public List<mm_block> GetBlockMaster()
        {
           return _dac.GetBlockMaster();
        }
        public List<mm_kyc> GetKycMaster()
        {
           return _dac.GetKycMaster();
        }
        public List<mm_title> GetTitleMaster()
        {
           return _dac.GetTitleMaster();
        }
         public List<m_branch> GetBranchMaster()
        {
           return _dac.GetBranchMaster();
        }
        public List<sm_parameter> GetSystemParameter()
        {
           return _dac.GetSystemParameter();
        }
        internal List<mm_operation> GetOperationMaster()
        {
            return _dac.GetOperationMaster();
        }
        internal List<mm_operation> GetOperationDtls()
        {
            return _dac.GetOperationDtls();
        }
        
        internal List<mm_instalment_type> GetInstalmentTypeMaster()
        {
            return _dac.GetInstalmentTypeMaster();
        }

        internal List<mm_crop> GetCropMaster()
        {
            return _dac.GetCropMaster();
        }

        internal List<mm_activity> GetActivityMaster()
        {
            return _dac.GetActivityMaster();
        }

        internal List<mm_sector> GetSectorMaster()
        {
            return _dac.GetSectorMaster();
        }
        
          
        internal int UpdateSystemParameter(sm_parameter smParam)
        {
            return _dac.UpdateSystemParameter(smParam);
        }

       

    }
}