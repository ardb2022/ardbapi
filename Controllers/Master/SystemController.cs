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
    [Route("api/Sys")]
    [ApiController]
    public class SystemController : ControllerBase
    {
        DayOperationLL _ll = new DayOperationLL(); 
        [Route("GetDayOperation")]
        [HttpPost]
        public List<sd_day_operation> GetDayOperation([FromBody] sd_day_operation tvd)
        {
           return _ll.GetDayOperation(tvd);
        }

        [Route("W_DAY_CLOSE")]
        [HttpPost]
        public p_gen_param W_DAY_CLOSE([FromBody] p_gen_param pgp)
        {
           return _ll.W_DAY_CLOSE(pgp);
        }
        
        [Route("W_DAY_OPEN")]
        [HttpPost]
        public p_gen_param W_DAY_OPEN([FromBody] p_gen_param pgp)
        {
           return _ll.W_DAY_OPEN(pgp);
        }
        
        UserLL _ll1 = new UserLL(); 
        [Route("GetUserIDDtls")]
        [HttpPost]
        public List<m_user_master> GetUserIDDtls([FromBody] m_user_master mum)
        {
           return _ll1.GetUserIDDtls(mum);
        }

        [Route("DeleteUserMaster")]
        [HttpPost]
        public int DeleteUserMaster([FromBody] m_user_master mum)
        {
           return _ll1.DeleteUserMaster(mum);
        }
        
        [Route("UpdateUserMaster")]
        [HttpPost] 
        public int UpdateUserMaster([FromBody] m_user_master mum)
        {
           return _ll1.UpdateUserMaster(mum);
        }
        
        [Route("InsertUserMaster")]
        [HttpPost] 
        public int InsertUserMaster([FromBody] m_user_master mum)
        {
           return _ll1.InsertUserMaster(mum);
        }
    }
}

