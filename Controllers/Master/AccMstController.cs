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
    [Route("api/Mst")]
    [ApiController]
    public class AccMstController : ControllerBase
    {
        AccMstLL _ll = new AccMstLL(); 
        [Route("GetAccountMaster")]
        [HttpPost]
        public List<m_acc_master> GetAccountMaster()
        {
           return _ll.GetAccountMaster();
        }
        [Route("GetAccountTypeMaster")]
        [HttpPost]
        public List<mm_acc_type> GetAccountTypeMaster()
        { 
           return _ll.GetAccountTypeMaster();
        }

         [Route("GetConstitution")]
        [HttpPost]
        public List<mm_constitution> GetConstitution()
        {
           return _ll.GetConstitution();
        }

        [Route("GetOprationalInstr")]
        [HttpPost]
        public List<mm_oprational_intr> GetOprationalInstr()
        {
           return _ll.GetOprationalInstr();
        }

        [Route("GetUserDtls")]
        [HttpPost]
        public List<m_user_master> GetUserDtls(m_user_master mum)
        {
           return _ll.GetUserDtls(mum);
        }
        [Route("UpdateUserstatus")]
        [HttpPost]
        public int UpdateUserstatus(m_user_master mum)
        {
           return _ll.UpdateUserstatus(mum);
        }

        [Route("GetCategoryMaster")]
        [HttpPost]
        public List<mm_category> GetCategoryMaster()
        {
           return _ll.GetCategoryMaster();
        }

        [Route("GetStateMaster")]
        [HttpPost]
        public List<mm_state> GetStateMaster()
        {
           return _ll.GetStateMaster();
        }

        [Route("GetDistMaster")]
        [HttpPost]
        public List<mm_dist> GetDistMaster()
        {
           return _ll.GetDistMaster();
        }

        [Route("GetVillageMaster")]
        [HttpPost]
        public List<mm_vill> GetVillageMaster()
        {
           return _ll.GetVillageMaster();
        }

        [Route("GetServiceAreaMaster")]
        [HttpPost]
        public List<mm_service_area> GetServiceAreaMaster()
        {
           return _ll.GetServiceAreaMaster();
        }

        [Route("GetBlockMaster")]
        [HttpPost]
        public List<mm_block> GetBlockMaster()
        {
           return _ll.GetBlockMaster();
        }

        [Route("GetKycMaster")]
        [HttpPost]
        public List<mm_kyc> GetKycMaster()
        {
           return _ll.GetKycMaster();
        }
        [Route("GetTitleMaster")]
        [HttpPost]
        public List<mm_title> GetTitleMaster()
        {
           return _ll.GetTitleMaster();
        }

        [Route("GetBranchMaster")]
        [HttpPost]
        public List<m_branch> GetBranchMaster()
        {
           return _ll.GetBranchMaster();
        }

        [Route("GetSystemParameter")]
        [HttpPost]
        public List<sm_parameter> GetSystemParameter()
        {
           return _ll.GetSystemParameter();
        }

        [Route("GetOperationMaster")]
        [HttpPost]
        public List<mm_operation> GetOperationMaster()
        {
           return _ll.GetOperationMaster();
        }

        [Route("GetOperationDtls")]
        [HttpPost]
        public List<mm_operation> GetOperationDtls()
        {
           return _ll.GetOperationDtls();
        }

        [Route("GetInstalmentTypeMaster")]
        [HttpPost]
        public List<mm_instalment_type> GetInstalmentTypeMaster()
        {
            return _ll.GetInstalmentTypeMaster();
        }
        [Route("GetCropMaster")]
        [HttpPost]
        public List<mm_crop> GetCropMaster()
        {
            return _ll.GetCropMaster();
        }
        [Route("GetActivityMaster")]
        [HttpPost]
        public List<mm_activity> GetActivityMaster()
        {
            return _ll.GetActivityMaster();
        }
        [Route("GetSectorMaster")]
        [HttpPost]
        public List<mm_sector> GetSectorMaster()
        {
            return _ll.GetSectorMaster();
        }


        [Route("UpdateSystemParameter")]
        [HttpPost]
        public int UpdateSystemParameter(sm_parameter smParam)
        {
            return _ll.UpdateSystemParameter(smParam);
        }
          
    }
}
