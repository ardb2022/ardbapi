using System;
using System.Collections.Generic;
using SBWSFinanceApi.Config;
using SBWSFinanceApi.Models;
using SBWSFinanceApi.Utility;

namespace SBWSFinanceApi.DL
{
    public class AccMstDL
    {
        string _statement;
        internal List<m_acc_master> GetAccountMaster()
        {
            List<m_acc_master> mamRets=new List<m_acc_master>();
            string _query=" SELECT SCHEDULE_CD, SUB_SCHEDULE_CD, ACC_CD, ACC_NAME, ACC_TYPE, IMPL_FLAG, ONLINE_FLAG, MIS_ACC_CD, TRADING_FLAG, STOCK_CD, N_TRIAL_CD"
                         +" FROM M_ACC_MASTER";
            using (var connection = OrclDbConnection.NewConnection)
            {              
                _statement = string.Format(_query);
                using (var command = OrclDbConnection.Command(connection, _statement))
                {
                    using (var reader = command.ExecuteReader())
                    {
                        if (reader.HasRows)     
                        {
                            while (reader.Read())
                            {                                
                               var mam = new m_acc_master();
                                mam.schedule_cd = UtilityM.CheckNull<int>(reader["SCHEDULE_CD"]);
                                mam.sub_schedule_cd = UtilityM.CheckNull<int>(reader["SUB_SCHEDULE_CD"]);
                                mam.acc_cd = UtilityM.CheckNull<int>(reader["ACC_CD"]);
                                mam.acc_name = UtilityM.CheckNull<string>(reader["ACC_NAME"]);
                                mam.acc_type = UtilityM.CheckNull<string>(reader["ACC_TYPE"]);
                                mam.impl_flag = UtilityM.CheckNull<string>(reader["IMPL_FLAG"]);
                                mam.online_flag = UtilityM.CheckNull<string>(reader["ONLINE_FLAG"]);
                                mam.mis_acc_cd = UtilityM.CheckNull<int>(reader["MIS_ACC_CD"]);
                                mam.trading_flag = UtilityM.CheckNull<string>(reader["TRADING_FLAG"]);
                                mam.stock_cd = UtilityM.CheckNull<int>(reader["STOCK_CD"]);
                                mam.n_trial_cd = UtilityM.CheckNull<int>(reader["N_TRIAL_CD"]);
                                mamRets.Add(mam);
                            }
                        }
                    }
                }
            
            }
            return mamRets;
        }  

        internal List<mm_acc_type> GetAccountTypeMaster()
        {
            List<mm_acc_type> mamRets=new List<mm_acc_type>();
            string _query=" SELECT  ACC_TYPE_CD,ACC_TYPE_DESC,TRANS_WAY,DEP_LOAN_FLAG,INTT_TRF_TYPE, CC_FLAG , REP_SCH_FLAG , INTT_CALC_TYPE FROM MM_ACC_TYPE";
                        
            using (var connection = OrclDbConnection.NewConnection)
            {              
                _statement = string.Format(_query);
                using (var command = OrclDbConnection.Command(connection, _statement))
                {
                    using (var reader = command.ExecuteReader())
                    {
                        if (reader.HasRows)     
                        {
                            while (reader.Read())
                            {                                
                               var mam = new mm_acc_type();
                                mam.acc_type_cd = UtilityM.CheckNull<int>(reader["ACC_TYPE_CD"]);
                                mam.acc_type_desc = UtilityM.CheckNull<string>(reader["ACC_TYPE_DESC"]);
                                mam.trans_way = UtilityM.CheckNull<string>(reader["TRANS_WAY"]);
                                mam.dep_loan_flag = UtilityM.CheckNull<string>(reader["DEP_LOAN_FLAG"]);
                                mam.intt_trf_type = UtilityM.CheckNull<string>(reader["INTT_TRF_TYPE"]);
                                mam.cc_flag = UtilityM.CheckNull<string>(reader["CC_FLAG"]);
                                mam.rep_sch_flag = UtilityM.CheckNull<string>(reader["REP_SCH_FLAG"]);
                                mam.intt_calc_type = UtilityM.CheckNull<string>(reader["INTT_CALC_TYPE"]);
                                mamRets.Add(mam);
                            }
                        }
                    }
                }
            
            }
            return mamRets;
        }  

internal List<mm_constitution> GetConstitution()
        {
            List<mm_constitution> mamRets=new List<mm_constitution>();
            string _query="SELECT  ACC_TYPE_CD,CONSTITUTION_CD,CONSTITUTION_DESC,ACC_CD,INTT_ACC_CD,INTT_PROV_ACC_CD,ALLOW_TRANS FROM MM_CONSTITUTION";
                        
            using (var connection = OrclDbConnection.NewConnection)
            {              
                _statement = string.Format(_query);
                using (var command = OrclDbConnection.Command(connection, _statement))
                {
                    using (var reader = command.ExecuteReader())
                    {
                        if (reader.HasRows)     
                        {
                            while (reader.Read())
                            {                                
                               var mam = new mm_constitution();
                                mam.acc_type_cd = UtilityM.CheckNull<int>(reader["ACC_TYPE_CD"]);
                                mam.constitution_cd = UtilityM.CheckNull<int>(reader["CONSTITUTION_CD"]);
                                mam.constitution_desc = UtilityM.CheckNull<string>(reader["CONSTITUTION_DESC"]);
                                mam.acc_cd = UtilityM.CheckNull<int>(reader["ACC_CD"]);
                                mam.intt_acc_cd = UtilityM.CheckNull<int>(reader["INTT_ACC_CD"]);
                                mam.intt_prov_acc_cd = UtilityM.CheckNull<int>(reader["INTT_PROV_ACC_CD"]);
                                mam.allow_trans = UtilityM.CheckNull<string>(reader["ALLOW_TRANS"]);
                                
                                mamRets.Add(mam);
                            }
                        }
                    }
                }
            
            }
            return mamRets;
        }  

        internal List<mm_oprational_intr> GetOprationalInstr()
        {
            List<mm_oprational_intr> mamRets=new List<mm_oprational_intr>();
            string _query="SELECT OPRN_CD,OPRN_DESC FROM  MM_OPERATIONAL_INSTR";
                        
            using (var connection = OrclDbConnection.NewConnection)
            {              
                _statement = string.Format(_query);
                using (var command = OrclDbConnection.Command(connection, _statement))
                {
                    using (var reader = command.ExecuteReader())
                    {
                        if (reader.HasRows)     
                        {
                            while (reader.Read())
                            {                                
                               var mam = new mm_oprational_intr();
                                mam.oprn_cd = UtilityM.CheckNull<int>(reader["OPRN_CD"]);
                                mam.oprn_desc = UtilityM.CheckNull<string>(reader["OPRN_DESC"]);
                                mamRets.Add(mam);
                            }
                        }
                    }
                }
            
            }
            return mamRets;
        }

       

        internal List<m_user_master> GetUserDtls(m_user_master mum)
        {
            UserDL ud=new UserDL();
            var passkey=ud.GetUserPass(mum.password); 
            List<m_user_master> mumRets=new List<m_user_master>();
            string _query=" SELECT BRN_CD, USER_ID, PASSWORD, LOGIN_STATUS, USER_TYPE, USER_FIRST_NAME, USER_MIDDLE_NAME, USER_LAST_NAME "
                        +" FROM M_USER_MASTER WHERE  USER_ID={0} AND PASSWORD={1} AND BRN_CD={2}";
            using (var connection = OrclDbConnection.NewConnection)
            {              
               _statement = string.Format(_query,                
                                            string.Concat("'",  mum.user_id, "'"),
                                            string.Concat("'",  passkey, "'"),
                                            string.Concat("'",  mum.brn_cd, "'")
                                            );
                using (var command = OrclDbConnection.Command(connection, _statement))
                {
                    using (var reader = command.ExecuteReader())
                    {
                        if (reader.HasRows)     
                        {
                            while (reader.Read())
                            {    
                                var mumt = new m_user_master();                            
                                mumt.brn_cd = UtilityM.CheckNull<string>(reader["BRN_CD"]);
                                mumt.user_id = UtilityM.CheckNull<string>(reader["USER_ID"]);
                                mumt.login_status = UtilityM.CheckNull<string>(reader["LOGIN_STATUS"]);
                                mumt.user_type = UtilityM.CheckNull<string>(reader["USER_TYPE"]);
                                mumt.user_first_name = UtilityM.CheckNull<string>(reader["USER_FIRST_NAME"]);
                                mumt.user_middle_name = UtilityM.CheckNull<string>(reader["USER_MIDDLE_NAME"]);
                                mumt.user_last_name = UtilityM.CheckNull<string>(reader["USER_LAST_NAME"]);
                                mumRets.Add(mumt);

                            }
                        }
                    }
                }
            }
            return mumRets;
        }

        internal int UpdateUserstatus(m_user_master mum)
        {
            int _ret =0;
             string _statementins="";
            string _query=" UPDATE M_USER_MASTER SET LOGIN_STATUS={0} "
                        +"  WHERE  USER_ID={1} AND BRN_CD={2}";
            string _qinsaudit = "INSERT INTO SM_AUDIT_TRAIL VALUES "
                               +" ((SELECT MAX(LOGIN_SRL) +1 FROM SM_AUDIT_TRAIL), "
                               +" {0}, "
                               +" sysdate, "
                               +" {1}, "
                               +" (SELECT upper(user_first_name|| ' ' ||user_middle_name||' '||user_last_name) "
                               +" FROM M_USER_MASTER WHERE user_id={2}), "
                               +" NULL, "
                               +" {3} )";
            string _qupdaudit = " UPDATE SM_AUDIT_TRAIL SET LOGOUT_DT=SYSDATE "
                               +" WHERE LOGIN_USER={0} and brn_cd={1} "
                               +" AND LOGOUT_DT IS NULL";


            using (var connection = OrclDbConnection.NewConnection)
            {              
               _statement = string.Format(_query,                
                                            string.Concat("'",  mum.login_status, "'"),
                                            string.Concat("'",  mum.user_id, "'"),
                                            string.Concat("'",  mum.brn_cd, "'")
                                            );
                 if ( mum.login_status=="Y")
                 {
                 _statementins = string.Format(_qinsaudit,                
                                            string.Concat("'",  mum.user_id, "'"),
                                            string.Concat("'",  mum.user_id, "'"),
                                            string.Concat("'",  mum.user_id, "'"),
                                            string.Concat("'",  mum.brn_cd, "'")
                                            );
                 }
                 else
                 {
                 _statementins = string.Format(_qupdaudit,
                                            string.Concat("'",  mum.user_id, "'"),
                                            string.Concat("'",  mum.brn_cd, "'")
                                            );
                 }
                 using (var transaction = connection.BeginTransaction())
                {
                    try
                    { 
                 using (var command = OrclDbConnection.Command(connection, _statement))
                        {
                            _ret = command.ExecuteNonQuery();
                           // transaction.Commit();
                            // _ret = 0;
                        }
                        using (var command = OrclDbConnection.Command(connection, _statementins))
                        {
                            _ret = command.ExecuteNonQuery();
                            //transaction.Commit();
                            // _ret = 0;
                        }
                        
                        transaction.Commit();
                           
                    }
                    catch (Exception ex)
                    {
                        transaction.Rollback();
                        _ret = -1;
                    }
                }
            }
            return _ret;
        }
        internal List<mm_category> GetCategoryMaster()
        {
            List<mm_category> mamRets=new List<mm_category>();
            string _query=" SELECT CATG_CD, CATG_DESC"
                         +" FROM MM_CATEGORY";
            using (var connection = OrclDbConnection.NewConnection)
            {              
                _statement = string.Format(_query);
                using (var command = OrclDbConnection.Command(connection, _statement))
                {
                    using (var reader = command.ExecuteReader())
                    {
                        if (reader.HasRows)     
                        {
                            while (reader.Read())
                            {                                
                               var mam = new mm_category();
                                mam.catg_cd = UtilityM.CheckNull<int>(reader["CATG_CD"]);
                                mam.catg_desc = UtilityM.CheckNull<string>(reader["CATG_DESC"]);                                
                                mamRets.Add(mam);
                            }
                        }
                    }
                }
            
            }
            return mamRets;
        }

        internal List<mm_state> GetStateMaster()
        {
            List<mm_state> mamRets=new List<mm_state>();
            string _query=" SELECT STATE_CD, STATE_NAME"
                         +" FROM MM_STATE";
            using (var connection = OrclDbConnection.NewConnection)
            {              
                _statement = string.Format(_query);
                using (var command = OrclDbConnection.Command(connection, _statement))
                {
                    using (var reader = command.ExecuteReader())
                    {
                        if (reader.HasRows)     
                        {
                            while (reader.Read())
                            {                                
                               var mam = new mm_state();
                                mam.state_cd = UtilityM.CheckNull<int>(reader["STATE_CD"]);
                                mam.state_name = UtilityM.CheckNull<string>(reader["STATE_NAME"]);                                
                                mamRets.Add(mam);
                            }
                        }
                    }
                }
            
            }
            return mamRets;
        }

        internal List<mm_dist> GetDistMaster()
        {
            List<mm_dist> mamRets=new List<mm_dist>();
            string _query=" SELECT DIST_CD, DIST_NAME"
                         +" FROM MM_DIST";
            using (var connection = OrclDbConnection.NewConnection)
            {              
                _statement = string.Format(_query);
                using (var command = OrclDbConnection.Command(connection, _statement))
                {
                    using (var reader = command.ExecuteReader())
                    {
                        if (reader.HasRows)     
                        {
                            while (reader.Read())
                            {                                
                               var mam = new mm_dist();
                                mam.dist_cd = UtilityM.CheckNull<int>(reader["DIST_CD"]);
                                mam.dist_name = UtilityM.CheckNull<string>(reader["DIST_NAME"]);                                
                                mamRets.Add(mam);
                            }
                        }
                    }
                }
            
            }
            return mamRets;
        }
        internal List<mm_vill> GetVillageMaster()
        {
            List<mm_vill> mamRets=new List<mm_vill>();
            string _query=" SELECT STATE_CD, DIST_CD,BLOCK_CD,VILL_CD,VILL_NAME,PS_CD,SERVICE_AREA_CD,VILLAGE_ID"
                         +" FROM MM_VILL";
            using (var connection = OrclDbConnection.NewConnection)
            {              
                _statement = string.Format(_query);
                using (var command = OrclDbConnection.Command(connection, _statement))
                {
                    using (var reader = command.ExecuteReader())
                    {
                        if (reader.HasRows)     
                        {
                            while (reader.Read())
                            {                                
                               var mam = new mm_vill();
                                mam.state_cd = UtilityM.CheckNull<string>(reader["STATE_CD"]);
                                mam.dist_cd = UtilityM.CheckNull<string>(reader["DIST_CD"]);
                                mam.block_cd = UtilityM.CheckNull<string>(reader["BLOCK_CD"]);
                                mam.vill_cd = UtilityM.CheckNull<string>(reader["VILL_CD"]);
                                mam.vill_name = UtilityM.CheckNull<string>(reader["VILL_NAME"]);
                                mam.ps_cd = UtilityM.CheckNull<string>(reader["PS_CD"]);  
                                mam.service_area_cd = UtilityM.CheckNull<string>(reader["SERVICE_AREA_CD"]);  
                                mam.village_id = UtilityM.CheckNull<decimal>(reader["VILLAGE_ID"]);                             
                                mamRets.Add(mam);
                            }
                        }
                    }
                }
            
            }
            return mamRets;
        }      

        internal List<mm_service_area> GetServiceAreaMaster()
        {
            List<mm_service_area> mamRets=new List<mm_service_area>();
            string _query=" SELECT STATE_CD, DIST_CD,BLOCK_CD,SERVICE_AREA_NAME,SERVICE_AREA_CD"
                         +" FROM MM_SERVICE_AREA";
            using (var connection = OrclDbConnection.NewConnection)
            {              
                _statement = string.Format(_query);
                using (var command = OrclDbConnection.Command(connection, _statement))
                {
                    using (var reader = command.ExecuteReader())
                    {
                        if (reader.HasRows)     
                        {
                            while (reader.Read())
                            {                                
                               var mam = new mm_service_area();
                                mam.state_cd = UtilityM.CheckNull<string>(reader["STATE_CD"]);
                                mam.dist_cd = UtilityM.CheckNull<string>(reader["DIST_CD"]);
                                mam.block_cd = UtilityM.CheckNull<string>(reader["BLOCK_CD"]);
                                mam.service_area_name = UtilityM.CheckNull<string>(reader["SERVICE_AREA_NAME"]);
                                mam.service_area_cd = UtilityM.CheckNull<string>(reader["SERVICE_AREA_CD"]);  
                                mamRets.Add(mam);
                            }
                        }
                    }
                }
            
            }
            return mamRets;
        }   

        internal List<mm_block> GetBlockMaster()
        {
            List<mm_block> mamRets=new List<mm_block>();
            string _query=" SELECT STATE_CD, DIST_CD,BLOCK_CD,BLOCK_NAME"
                         +" FROM MM_BLOCK";
            using (var connection = OrclDbConnection.NewConnection)
            {              
                _statement = string.Format(_query);
                using (var command = OrclDbConnection.Command(connection, _statement))
                {
                    using (var reader = command.ExecuteReader())
                    {
                        if (reader.HasRows)     
                        {
                            while (reader.Read())
                            {                                
                               var mam = new mm_block();
                                mam.state_cd = UtilityM.CheckNull<string>(reader["STATE_CD"]);
                                mam.dist_cd = UtilityM.CheckNull<string>(reader["DIST_CD"]);
                                mam.block_cd = UtilityM.CheckNull<string>(reader["BLOCK_CD"]);
                                mam.block_name = UtilityM.CheckNull<string>(reader["BLOCK_NAME"]);
                                mamRets.Add(mam);
                            }
                        }
                    }
                }
            
            }
            return mamRets;
        }    

        internal List<mm_kyc> GetKycMaster()
        {
            List<mm_kyc> mamRets=new List<mm_kyc>();
            string _query=" SELECT KYC_TYPE, KYC_DESC"
                         +" FROM MM_KYC";
            using (var connection = OrclDbConnection.NewConnection)
            {              
                _statement = string.Format(_query);
                using (var command = OrclDbConnection.Command(connection, _statement))
                {
                    using (var reader = command.ExecuteReader())
                    {
                        if (reader.HasRows)     
                        {
                            while (reader.Read())
                            {                                
                               var mam = new mm_kyc();
                                mam.kyc_type = UtilityM.CheckNull<string>(reader["KYC_TYPE"]);
                                mam.kyc_desc = UtilityM.CheckNull<string>(reader["KYC_DESC"]);
                                mamRets.Add(mam);
                            }
                        }
                    }
                }
            
            }
            return mamRets;
        }           

        internal List<mm_title> GetTitleMaster()
        {
            List<mm_title> mamRets=new List<mm_title>();
            string _query=" SELECT TITLE_CODE, TITLE"
                         +" FROM MM_TITLE";
            using (var connection = OrclDbConnection.NewConnection)
            {              
                _statement = string.Format(_query);
                using (var command = OrclDbConnection.Command(connection, _statement))
                {
                    using (var reader = command.ExecuteReader())
                    {
                        if (reader.HasRows)     
                        {
                            while (reader.Read())
                            {                                
                               var mam = new mm_title();
                                mam.title_code = UtilityM.CheckNull<Int16>(reader["TITLE_CODE"]);
                                mam.title = UtilityM.CheckNull<string>(reader["TITLE"]);
                                mamRets.Add(mam);
                            }
                        }
                    }
                }
            
            }
            return mamRets;
        }           

        internal List<m_branch> GetBranchMaster()
        {
            List<m_branch> mamRets=new List<m_branch>();
            string _query=" SELECT BRN_CD, BRN_NAME,BRN_ADDR,BRN_IFSC_CODE,IP_ADDRESS"
                         +" FROM M_BRANCH";
            using (var connection = OrclDbConnection.NewConnection)
            {              
                _statement = string.Format(_query);
                using (var command = OrclDbConnection.Command(connection, _statement))
                {
                    using (var reader = command.ExecuteReader())
                    {
                        if (reader.HasRows)     
                        {
                            while (reader.Read())
                            {                                
                               var mam = new m_branch();
                                mam.brn_cd = UtilityM.CheckNull<string>(reader["BRN_CD"]);
                                mam.brn_name = UtilityM.CheckNull<string>(reader["BRN_NAME"]);
                                mam.brn_addr = UtilityM.CheckNull<string>(reader["BRN_ADDR"]);
                                mam.brn_ifsc_code = UtilityM.CheckNull<string>(reader["BRN_IFSC_CODE"]);
                                mam.ip_address = UtilityM.CheckNull<string>(reader["IP_ADDRESS"]);
                                mamRets.Add(mam);
                            }
                        }
                    }
                }
            
            }
            return mamRets;
        }     

         internal List<sm_parameter> GetSystemParameter()
        {
            List<sm_parameter> mamRets=new List<sm_parameter>();
            string _query=" SELECT PARAM_CD, PARAM_DESC,PARAM_VALUE,EDIT_FLAG"
                         +" FROM SM_PARAMETER";
            using (var connection = OrclDbConnection.NewConnection)
            {              
                _statement = string.Format(_query);
                using (var command = OrclDbConnection.Command(connection, _statement))
                {
                    using (var reader = command.ExecuteReader())
                    {
                        if (reader.HasRows)     
                        {
                            while (reader.Read())
                            {                                
                               var mam = new sm_parameter();
                                mam.param_cd = UtilityM.CheckNull<string>(reader["PARAM_CD"]);
                                mam.param_desc = UtilityM.CheckNull<string>(reader["PARAM_DESC"]);
                                mam.param_value = UtilityM.CheckNull<string>(reader["PARAM_VALUE"]);
                                mam.edit_flag = UtilityM.CheckNull<string>(reader["EDIT_FLAG"]);
                                mamRets.Add(mam);
                            }
                        }
                    }
                }
            
            }
            return mamRets;
        }   

        internal List<mm_operation> GetOperationMaster()
        {
            List<mm_operation> mamRets=new List<mm_operation>();
            string _query=" SELECT OPRN_CD, OPRN_DESC,ACC_TYPE_CD,MODULE_TYPE"
                         +" FROM MM_OPERATION";
            using (var connection = OrclDbConnection.NewConnection)
            {              
                _statement = string.Format(_query);
                using (var command = OrclDbConnection.Command(connection, _statement))
                {
                    using (var reader = command.ExecuteReader())
                    {
                        if (reader.HasRows)     
                        {
                            while (reader.Read())
                            {                                
                               var mam = new mm_operation();
                                mam.oprn_cd = UtilityM.CheckNull<decimal>(reader["OPRN_CD"]);
                                mam.oprn_desc = UtilityM.CheckNull<string>(reader["OPRN_DESC"]);
                                mam.acc_type_cd = UtilityM.CheckNull<decimal>(reader["ACC_TYPE_CD"]);
                                mam.module_type = UtilityM.CheckNull<string>(reader["MODULE_TYPE"]);
                                mamRets.Add(mam);
                            }
                        }
                    }
                }
            
            }
            return mamRets;
        }

        internal List<mm_operation> GetOperationDtls()
        {
            List<mm_operation> mamRets=new List<mm_operation>();
            string _query=" SELECT MO.ACC_TYPE_CD ACC_TYPE_CD,  AC.ACC_TYPE_DESC ACC_TYPE_DESC,  MO.OPRN_CD OPRN_CD, "
                        +"  MO.OPRN_DESC OPRN_DESC, MO.MODULE_TYPE MODULE_TYPE "
                         +" FROM MM_ACC_TYPE AC,   MM_OPERATION MO"
                         +"  WHERE AC.ACC_TYPE_CD=MO.ACC_TYPE_CD ";
            using (var connection = OrclDbConnection.NewConnection)
            {              
                _statement = string.Format(_query);
                using (var command = OrclDbConnection.Command(connection, _statement))
                {
                    using (var reader = command.ExecuteReader())
                    {
                        if (reader.HasRows)     
                        {
                            while (reader.Read())
                            {                                
                               var mam = new mm_operation();
                                mam.oprn_cd = UtilityM.CheckNull<decimal>(reader["OPRN_CD"]);
                                mam.oprn_desc = UtilityM.CheckNull<string>(reader["OPRN_DESC"]);
                                mam.acc_type_cd = UtilityM.CheckNull<decimal>(reader["ACC_TYPE_CD"]);
                                mam.module_type = UtilityM.CheckNull<string>(reader["MODULE_TYPE"]);
                                 mam.acc_type_desc = UtilityM.CheckNull<string>(reader["ACC_TYPE_DESC"]);
                                mamRets.Add(mam);
                            }
                        }
                    }
                }
            
            }
            return mamRets;
        }

        internal List<mm_sector> GetSectorMaster()
        {
            List<mm_sector> mamRets=new List<mm_sector>();
            string _query=" SELECT SECTOR_CD,SECTOR_DESC,PRIORITY_FLAG FROM MM_SECTOR";
            using (var connection = OrclDbConnection.NewConnection)
            {              
                _statement = string.Format(_query);
                using (var command = OrclDbConnection.Command(connection, _statement))
                {
                    using (var reader = command.ExecuteReader())
                    {
                        if (reader.HasRows)     
                        {
                            while (reader.Read())
                            {                                
                               var mam = new mm_sector();
                                mam.sector_cd = UtilityM.CheckNull<string>(reader["SECTOR_CD"]);
                                mam.sector_desc = UtilityM.CheckNull<string>(reader["SECTOR_DESC"]);
                                mam.priority_flag = UtilityM.CheckNull<string>(reader["PRIORITY_FLAG"]);
                                mamRets.Add(mam);
                            }
                        }
                    }
                }
            
            }
            return mamRets;
        }

          internal List<mm_activity> GetActivityMaster()
        {
            List<mm_activity> mamRets=new List<mm_activity>();
            string _query="SELECT SECTOR_CD,ACTIVITY_CD,ACTIVITY_DESC,ACTIVITY_DISPLAY_CD FROM MM_ACTIVITY";
            using (var connection = OrclDbConnection.NewConnection)
            {              
                _statement = string.Format(_query);
                using (var command = OrclDbConnection.Command(connection, _statement))
                {
                    using (var reader = command.ExecuteReader())
                    {
                        if (reader.HasRows)     
                        {
                            while (reader.Read())
                            {                                
                               var mam = new mm_activity();
                                mam.sector_cd = UtilityM.CheckNull<string>(reader["SECTOR_CD"]);
                                mam.activity_cd = UtilityM.CheckNull<string>(reader["ACTIVITY_CD"]);
                                mam.activity_desc = UtilityM.CheckNull<string>(reader["ACTIVITY_DESC"]);
                                mam.activity_display_cd = UtilityM.CheckNull<string>(reader["ACTIVITY_DISPLAY_CD"]);
                                mamRets.Add(mam);
                            }
                        }
                    }
                }
            
            }
            return mamRets;
        }

         internal List<mm_crop> GetCropMaster()
        {
            List<mm_crop> mamRets=new List<mm_crop>();
            string _query="SELECT CROP_CD,CROP_DESC,INS_FLG,ACTIVITY_CD FROM MM_CROP";
            using (var connection = OrclDbConnection.NewConnection)
            {              
                _statement = string.Format(_query);
                using (var command = OrclDbConnection.Command(connection, _statement))
                {
                    using (var reader = command.ExecuteReader())
                    {
                        if (reader.HasRows)     
                        {
                            while (reader.Read())
                            {                                
                               var mam = new mm_crop();
                                mam.crop_cd = UtilityM.CheckNull<string>(reader["CROP_CD"]);
                                mam.crop_desc = UtilityM.CheckNull<string>(reader["CROP_DESC"]);
                                mam.ins_flg = UtilityM.CheckNull<string>(reader["INS_FLG"]);
                                mam.activity_cd = UtilityM.CheckNull<string>(reader["ACTIVITY_CD"]);
                                mamRets.Add(mam);
                            }
                        }
                    }
                }
            
            }
            return mamRets;
        }

         internal List<mm_instalment_type> GetInstalmentTypeMaster()
        {
            List<mm_instalment_type> mamRets=new List<mm_instalment_type>();
            string _query=" SELECT SL_NO,INS_DESC,INS_TYPE,DESC_TYPE FROM MM_INSTALMENT_TYPE";
            using (var connection = OrclDbConnection.NewConnection)
            {              
                _statement = string.Format(_query);
                using (var command = OrclDbConnection.Command(connection, _statement))
                {
                    using (var reader = command.ExecuteReader())
                    {
                        if (reader.HasRows)     
                        {
                            while (reader.Read())
                            {                                
                               var mam = new mm_instalment_type();
                                mam.sl_no    = UtilityM.CheckNull<Int32>(reader["SL_NO"]);
                                mam.ins_desc = UtilityM.CheckNull<string>(reader["INS_DESC"]);
                                mam.ins_type = UtilityM.CheckNull<Int32>(reader["INS_TYPE"]).ToString();
                                mam.desc_type = UtilityM.CheckNull<string>(reader["DESC_TYPE"]);
                                mamRets.Add(mam);
                            }
                        }
                    }
                }
            
            }
            return mamRets;
        }
     internal List<mm_role> GetRoleMaster()
        {
            List<mm_role> mamRets=new List<mm_role>();
            string _query="SELECT ROLE_CD,ROLE_TYPE FROM MM_ROLE";
            using (var connection = OrclDbConnection.NewConnection)
            {              
                _statement = string.Format(_query);
                using (var command = OrclDbConnection.Command(connection, _statement))
                {
                    using (var reader = command.ExecuteReader())
                    {
                        if (reader.HasRows)     
                        {
                            while (reader.Read())
                            {                                
                               var mam = new mm_role();
                                mam.role_cd    = UtilityM.CheckNull<Int32>(reader["ROLE_CD"]);
                                mam.role_type = UtilityM.CheckNull<string>(reader["ROLE_TYPE"]);;
                                mamRets.Add(mam);
                            }
                        }
                    }
                }
            
            }
            return mamRets;
        }
         internal int UpdateSystemParameter(sm_parameter smParam)
        {
            int _ret=0;   

            string _query=" UPDATE SM_PARAMETER " 
             +" SET PARAM_VALUE = {0} "
            +"  WHERE PARAM_CD  = {1}  ";

            using (var connection = OrclDbConnection.NewConnection)
            {              
                 using (var transaction = connection.BeginTransaction())
                {               
                    try
                    {
                     _statement = string.Format(_query,
                                                string.Concat("'", smParam.param_value, "'") ,
                                                string.Concat("'", smParam.param_cd, "'")
                                          );

                        using (var command = OrclDbConnection.Command(connection, _statement))
                        {                   
                            command.ExecuteNonQuery();
                            transaction.Commit();
                            _ret=0;
                        }
                    }
                    catch (Exception ex)
                    {
                        transaction.Rollback();
                        _ret=-1;
                    }
                }           
            }
            return _ret;
        }

                                                                                       

}
}
