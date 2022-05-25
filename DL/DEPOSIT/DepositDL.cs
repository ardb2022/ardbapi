using System;
using System.Data;
using System.Collections.Generic;
using SBWSDepositApi.Models;
using SBWSFinanceApi.Config;
using SBWSFinanceApi.DL;
using SBWSFinanceApi.Models;
using SBWSFinanceApi.Utility;
using Oracle.ManagedDataAccess.Client;

namespace SBWSDepositApi.Deposit
{
    public class DepositDL
    {
        string _statement;
        internal List<tm_deposit> GetDepositTemp(tm_deposit dep)
        {
            List<tm_deposit> depoList = new List<tm_deposit>();

            string _query = " SELECT BRN_CD, ACC_TYPE_CD, ACC_NUM, RENEW_ID, CUST_CD, INTT_TRF_TYPE, CONSTITUTION_CD, "
                            + " OPRN_INSTR_CD, OPENING_DT, PRN_AMT, INTT_AMT, DEP_PERIOD, INSTL_AMT, INSTL_NO,   "
                            + " MAT_DT, INTT_RT, TDS_APPLICABLE, LAST_INTT_CALC_DT, ACC_CLOSE_DT,                "
                            + " CLOSING_PRN_AMT, CLOSING_INTT_AMT, PENAL_AMT, EXT_INSTL_TOT, MAT_STATUS, ACC_STATUS, "
                            + " CURR_BAL, CLR_BAL, STANDING_INSTR_FLAG, CHEQUE_FACILITY_FLAG,                         "
                            + " CREATED_BY, CREATED_DT, MODIFIED_BY, MODIFIED_DT, APPROVAL_STATUS, APPROVED_BY,       "
                            + " APPROVED_DT, USER_ACC_NUM, LOCK_MODE, LOAN_ID, CERT_NO, BONUS_AMT, PENAL_INTT_RT,     "
                            + " BONUS_INTT_RT, TRANSFER_FLAG, TRANSFER_DT, AGENT_CD                                   "
                            + " FROM TM_DEPOSIT_TEMP                                                                  "
                            + " WHERE BRN_CD={0} AND ACC_NUM={1}   AND ACC_TYPE_CD = {2}   ";

            using (var connection = OrclDbConnection.NewConnection)
            {
                _statement = string.Format(_query,
                                          !string.IsNullOrWhiteSpace(dep.brn_cd) ? string.Concat("'", dep.brn_cd, "'") : "brn_cd",
                                          !string.IsNullOrWhiteSpace(dep.acc_num) ? string.Concat("'", dep.acc_num, "'") : "acc_num",
                                          (dep.acc_type_cd > 0 ? dep.acc_type_cd.ToString() : "ACC_TYPE_CD"));
                using (var command = OrclDbConnection.Command(connection, _statement))
                {
                    using (var reader = command.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            {
                                while (reader.Read())
                                {
                                    var d = new tm_deposit();
                                    d.brn_cd = UtilityM.CheckNull<string>(reader["BRN_CD"]);
                                    d.acc_type_cd = UtilityM.CheckNull<int>(reader["ACC_TYPE_CD"]);
                                    d.acc_num = UtilityM.CheckNull<string>(reader["ACC_NUM"]);
                                    d.renew_id = UtilityM.CheckNull<int>(reader["RENEW_ID"]);
                                    d.cust_cd = UtilityM.CheckNull<decimal>(reader["CUST_CD"]);
                                    d.intt_trf_type = UtilityM.CheckNull<string>(reader["INTT_TRF_TYPE"]);
                                    d.constitution_cd = UtilityM.CheckNull<int>(reader["CONSTITUTION_CD"]);
                                    d.oprn_instr_cd = UtilityM.CheckNull<int>(reader["OPRN_INSTR_CD"]);
                                    d.opening_dt = UtilityM.CheckNull<DateTime>(reader["OPENING_DT"]);
                                    d.prn_amt = UtilityM.CheckNull<decimal>(reader["PRN_AMT"]);
                                    d.intt_amt = UtilityM.CheckNull<decimal>(reader["INTT_AMT"]);
                                    d.dep_period = UtilityM.CheckNull<string>(reader["DEP_PERIOD"]);
                                    d.instl_amt = UtilityM.CheckNull<decimal>(reader["INSTL_AMT"]);
                                    d.instl_no = UtilityM.CheckNull<decimal>(reader["INSTL_NO"]);
                                    d.mat_dt = UtilityM.CheckNull<DateTime>(reader["MAT_DT"]);
                                    d.intt_rt = UtilityM.CheckNull<decimal>(reader["INTT_RT"]);
                                    d.tds_applicable = UtilityM.CheckNull<string>(reader["TDS_APPLICABLE"]);
                                    d.last_intt_calc_dt = UtilityM.CheckNull<DateTime>(reader["LAST_INTT_CALC_DT"]);
                                    d.acc_close_dt = UtilityM.CheckNull<DateTime>(reader["ACC_CLOSE_DT"]);
                                    d.closing_prn_amt = UtilityM.CheckNull<decimal>(reader["CLOSING_PRN_AMT"]);
                                    d.closing_intt_amt = UtilityM.CheckNull<decimal>(reader["CLOSING_INTT_AMT"]);
                                    d.penal_amt = UtilityM.CheckNull<decimal>(reader["PENAL_AMT"]);
                                    d.ext_instl_tot = UtilityM.CheckNull<decimal>(reader["EXT_INSTL_TOT"]);
                                    d.mat_status = UtilityM.CheckNull<string>(reader["MAT_STATUS"]);
                                    d.acc_status = UtilityM.CheckNull<string>(reader["ACC_STATUS"]);
                                    d.curr_bal = UtilityM.CheckNull<decimal>(reader["CURR_BAL"]);
                                    d.clr_bal = UtilityM.CheckNull<decimal>(reader["CLR_BAL"]);
                                    d.standing_instr_flag = UtilityM.CheckNull<string>(reader["STANDING_INSTR_FLAG"]);
                                    d.cheque_facility_flag = UtilityM.CheckNull<string>(reader["CHEQUE_FACILITY_FLAG"]);
                                    d.created_by = UtilityM.CheckNull<string>(reader["CREATED_BY"]);
                                    d.created_dt = UtilityM.CheckNull<DateTime>(reader["CREATED_DT"]);
                                    d.modified_by = UtilityM.CheckNull<string>(reader["MODIFIED_BY"]);
                                    d.modified_dt = UtilityM.CheckNull<DateTime>(reader["MODIFIED_DT"]);
                                    d.approval_status = UtilityM.CheckNull<string>(reader["APPROVAL_STATUS"]);
                                    d.approved_by = UtilityM.CheckNull<string>(reader["APPROVED_BY"]);
                                    d.approved_dt = UtilityM.CheckNull<DateTime>(reader["APPROVED_DT"]);
                                    d.user_acc_num = UtilityM.CheckNull<string>(reader["USER_ACC_NUM"]);
                                    d.lock_mode = UtilityM.CheckNull<string>(reader["LOCK_MODE"]);
                                    d.loan_id = UtilityM.CheckNull<string>(reader["LOAN_ID"]);
                                    d.cert_no = UtilityM.CheckNull<string>(reader["CERT_NO"]);
                                    d.bonus_amt = UtilityM.CheckNull<decimal>(reader["BONUS_AMT"]);
                                    d.penal_intt_rt = UtilityM.CheckNull<decimal>(reader["PENAL_INTT_RT"]);
                                    d.bonus_intt_rt = UtilityM.CheckNull<decimal>(reader["BONUS_INTT_RT"]);
                                    d.transfer_flag = UtilityM.CheckNull<string>(reader["TRANSFER_FLAG"]);
                                    d.transfer_dt = UtilityM.CheckNull<DateTime>(reader["TRANSFER_DT"]);
                                    d.agent_cd = UtilityM.CheckNull<string>(reader["AGENT_CD"]);

                                    depoList.Add(d);
                                }
                            }
                        }
                    }
                }
            }

            return depoList;
        }
        
        internal decimal InsertDepositTemp(tm_deposit dep)
        {
            List<td_signatory> sigList = new List<td_signatory>();

            string _query = " INSERT INTO TM_DEPOSIT_TEMP ( BRN_CD, ACC_TYPE_CD, ACC_NUM, RENEW_ID, CUST_CD, INTT_TRF_TYPE, CONSTITUTION_CD,"
                           + " OPRN_INSTR_CD, OPENING_DT, PRN_AMT, INTT_AMT, DEP_PERIOD, INSTL_AMT, INSTL_NO, MAT_DT, INTT_RT, TDS_APPLICABLE,     "
                           + " LAST_INTT_CALC_DT, ACC_CLOSE_DT, CLOSING_PRN_AMT, CLOSING_INTT_AMT, PENAL_AMT, EXT_INSTL_TOT, MAT_STATUS, ACC_STATUS,"
                           + " CURR_BAL, CLR_BAL, STANDING_INSTR_FLAG, CHEQUE_FACILITY_FLAG, CREATED_BY, CREATED_DT, MODIFIED_BY, MODIFIED_DT,      "
                           + " APPROVAL_STATUS, APPROVED_BY, APPROVED_DT, USER_ACC_NUM, LOCK_MODE, LOAN_ID, CERT_NO, BONUS_AMT, PENAL_INTT_RT,      "
                           + " BONUS_INTT_RT, TRANSFER_FLAG, TRANSFER_DT, AGENT_CD   "
                           + " VALUES({0},{1},{2},{3},{4},{5},{6},{7},to_date('{8}','dd-mm-yyyy' ),{9},{10},{11},{12},{13}, to_date('{14}','dd-mm-yyyy' ),"
                           + " {15},{16}, to_date('{17}','dd-mm-yyyy' ), to_date('{18}','dd-mm-yyyy' ),{19},{20},{21},{22},{23},{24}, "
                           + " {25},{26},{27},{28},{29}, to_date('{30}','dd-mm-yyyy' ),{31}, to_date('{32}','dd-mm-yyyy' ),{33}, "
                           + " to_date('{34}','dd-mm-yyyy' ),{35},{36}, to_date('{37}','dd-mm-yyyy' ),{38},{39},{40},{41},{42},{43},to_date('{44}','dd-mm-yyyy' ),{45})";

            using (var connection = OrclDbConnection.NewConnection)
            {
                using (var transaction = connection.BeginTransaction())
                {
                    try
                    {
                        _statement = string.Format(_query,
                       string.Concat("'", dep.brn_cd, "'"),
                       string.Concat("'", dep.acc_type_cd, "'"),
                       string.Concat("'", dep.acc_num, "'"),
                       string.Concat("'", dep.renew_id, "'"),
                       string.Concat("'", dep.cust_cd, "'"),
                       string.Concat("'", dep.intt_trf_type, "'"),
                       string.Concat("'", dep.constitution_cd, "'"),
                       string.Concat("'", dep.oprn_instr_cd, "'"),
                       string.IsNullOrWhiteSpace(dep.opening_dt.ToString()) ? null : string.Concat("'", dep.opening_dt.Value.ToString("dd/MM/yyyy"), "'"),
                       string.Concat("'", dep.prn_amt, "'"),
                       string.Concat("'", dep.intt_amt, "'"),
                       string.Concat("'", dep.dep_period, "'"),
                       string.Concat("'", dep.instl_amt, "'"),
                       string.Concat("'", dep.instl_no, "'"),
                       string.IsNullOrWhiteSpace(dep.mat_dt.ToString()) ? null : string.Concat("'", dep.mat_dt.Value.ToString("dd/MM/yyyy"), "'"),
                       string.Concat("'", dep.intt_rt, "'"),
                       string.Concat("'", dep.tds_applicable, "'"),
                       string.IsNullOrWhiteSpace(dep.last_intt_calc_dt.ToString()) ? null : string.Concat("'", dep.last_intt_calc_dt.Value.ToString("dd/MM/yyyy"), "'"),
                       string.IsNullOrWhiteSpace(dep.acc_close_dt.ToString()) ? null : string.Concat("'", dep.acc_close_dt.Value.ToString("dd/MM/yyyy"), "'"),
                       string.Concat("'", dep.closing_prn_amt, "'"),
                       string.Concat("'", dep.closing_intt_amt, "'"),
                       string.Concat("'", dep.penal_amt, "'"),
                       string.Concat("'", dep.ext_instl_tot, "'"),
                       string.Concat("'", dep.mat_status, "'"),
                       string.Concat("'", dep.acc_status, "'"),
                       string.Concat("'", dep.curr_bal, "'"),
                       string.Concat("'", dep.clr_bal, "'"),
                       string.Concat("'", dep.standing_instr_flag, "'"),
                       string.Concat("'", dep.cheque_facility_flag, "'"),
                       string.Concat("'", dep.created_by, "'"),
                       string.IsNullOrWhiteSpace(dep.created_dt.ToString()) ? null : string.Concat("'", dep.created_dt.Value.ToString("dd/MM/yyyy"), "'"),
                       string.Concat("'", dep.modified_by, "'"),
                       string.IsNullOrWhiteSpace(dep.modified_dt.ToString()) ? null : string.Concat("'", dep.modified_dt.Value.ToString("dd/MM/yyyy"), "'"),
                       string.Concat("'", dep.approval_status, "'"),
                       string.Concat("'", dep.approved_by, "'"),
                       string.IsNullOrWhiteSpace(dep.approved_dt.ToString()) ? null : string.Concat("'", dep.approved_dt.Value.ToString("dd/MM/yyyy"), "'"),
                       string.Concat("'", dep.user_acc_num, "'"),
                       string.Concat("'", dep.lock_mode, "'"),
                       string.Concat("'", dep.loan_id, "'"),
                       string.Concat("'", dep.cert_no, "'"),
                       string.Concat("'", dep.bonus_amt, "'"),
                       string.Concat("'", dep.penal_intt_rt, "'"),
                       string.Concat("'", dep.bonus_intt_rt, "'"),
                       string.Concat("'", dep.transfer_flag, "'"),
                       string.IsNullOrWhiteSpace(dep.transfer_dt.ToString()) ? null : string.Concat("'", dep.transfer_dt.Value.ToString("dd/MM/yyyy"), "'"),
                       string.Concat("'", dep.agent_cd, "'")
                                                    );

                        using (var command = OrclDbConnection.Command(connection, _statement))
                        {
                            command.ExecuteNonQuery();
                            transaction.Commit();
                        }

                    }
                    catch (Exception ex)
                    {
                        transaction.Rollback();
                        return -1;
                    }
                }
            }

            return 0;
        }
        internal int UpdateDepositTemp(tm_deposit dep)
        {
            int _ret = 0;

            string _query = " UPDATE TM_DEPOSIT_TEMP "
               + "brn_cd               = NVL({0},  brn_cd              ),"
               + "acc_type_cd          = NVL({1},  acc_type_cd         ),"
               + "acc_num              = NVL({2},  acc_num             ),"
               + "renew_id             = NVL({3},  renew_id            ),"
               + "cust_cd              = NVL({4},  cust_cd             ),"
               + "intt_trf_type        = NVL({5},  intt_trf_type       ),"
               + "constitution_cd      = NVL({6},  constitution_cd     ),"
               + "oprn_instr_cd        = NVL({7},  oprn_instr_cd       ),"
               + "opening_dt           = NVL(to_date('{8}','dd-mm-yyyy' ),  opening_dt ),"
               + "prn_amt              = NVL({9},  prn_amt             ),"
               + "intt_amt             = NVL({10}, intt_amt            ),"
               + "dep_period           = NVL({11}, dep_period          ),"
               + "instl_amt            = NVL({12}, instl_amt           ),"
               + "instl_no             = NVL({13}, instl_no            ),"
               + "mat_dt               = NVL(to_date('{14}','dd-mm-yyyy' ), mat_dt ),"
               + "intt_rt              = NVL({15}, intt_rt             ),"
               + "tds_applicable       = NVL({16}, tds_applicable      ),"
               + "last_intt_calc_dt    = NVL(to_date('{17}','dd-mm-yyyy' ), last_intt_calc_dt ),"
               + "acc_close_dt         = NVL(to_date('{18}','dd-mm-yyyy' ), acc_close_dt ),"
               + "closing_prn_amt      = NVL({19}, closing_prn_amt     ),"
               + "closing_intt_amt     = NVL({20}, closing_intt_amt    ),"
               + "penal_amt            = NVL({21}, penal_amt           ),"
               + "ext_instl_tot        = NVL({22}, ext_instl_tot       ),"
               + "mat_status           = NVL({23}, mat_status          ),"
               + "acc_status           = NVL({24}, acc_status          ),"
               + "curr_bal             = NVL({25}, curr_bal            ),"
               + "clr_bal              = NVL({26}, clr_bal             ),"
               + "standing_instr_flag  = NVL({27}, standing_instr_flag ),"
               + "cheque_facility_flag = NVL({28}, cheque_facility_flag),"
               + "created_by           = NVL({29}, created_by          ),"
               + "created_dt           = NVL(to_date('{30}','dd-mm-yyyy' ), created_dt ),"
               + "modified_by          = NVL({31}, modified_by         ),"
               + "modified_dt          = NVL(to_date({32}','dd-mm-yyyy' ), modified_dt ),"
               + "approval_status      = NVL({33}, approval_status     ),"
               + "approved_by          = NVL({34}, approved_by         ),"
               + "approved_dt          = NVL(to_date('{35}','dd-mm-yyyy' ), approved_dt ),"
               + "user_acc_num         = NVL({36}, user_acc_num        ),"
               + "lock_mode            = NVL({37}, lock_mode           ),"
               + "loan_id              = NVL({38}, loan_id             ),"
               + "cert_no              = NVL({39}, cert_no             ),"
               + "bonus_amt            = NVL({40}, bonus_amt           ),"
               + "penal_intt_rt        = NVL({41}, penal_intt_rt       ),"
               + "bonus_intt_rt        = NVL({42}, bonus_intt_rt       ),"
               + "transfer_flag        = NVL({43}, transfer_flag       ),"
               + "transfer_dt          = NVL(to_date('{44}','dd-mm-yyyy' ), transfer_dt ),"
               + "agent_cd             = NVL({45}, agent_cd            ) "
               + "WHERE brn_cd = NVL({46}, brn_cd) AND acc_num = NVL('{47}',  acc_num )";

            using (var connection = OrclDbConnection.NewConnection)
            {
                using (var transaction = connection.BeginTransaction())
                {
                    try
                    {
                        _statement = string.Format(_query,
                       string.Concat("'", dep.brn_cd, "'"),
                       string.Concat("'", dep.acc_type_cd, "'"),
                       string.Concat("'", dep.acc_num, "'"),
                       string.Concat("'", dep.renew_id, "'"),
                       string.Concat("'", dep.cust_cd, "'"),
                       string.Concat("'", dep.intt_trf_type, "'"),
                       string.Concat("'", dep.constitution_cd, "'"),
                       string.Concat("'", dep.oprn_instr_cd, "'"),
                       string.IsNullOrWhiteSpace(dep.opening_dt.ToString()) ? null : string.Concat("'", dep.opening_dt.Value.ToString("dd/MM/yyyy"), ","),
                       string.Concat("'", dep.prn_amt, "'"),
                       string.Concat("'", dep.intt_amt, "'"),
                       string.Concat("'", dep.dep_period, "'"),
                       string.Concat("'", dep.instl_amt, "'"),
                       string.Concat("'", dep.instl_no, "'"),
                       string.IsNullOrWhiteSpace(dep.mat_dt.ToString()) ? null : string.Concat("'", dep.mat_dt.Value.ToString("dd/MM/yyyy"), "'"),
                       string.Concat("'", dep.intt_rt, "'"),
                       string.Concat("'", dep.tds_applicable, "'"),
                       string.IsNullOrWhiteSpace(dep.last_intt_calc_dt.ToString()) ? null : string.Concat("'", dep.last_intt_calc_dt.Value.ToString("dd/MM/yyyy"), "'"),
                       string.IsNullOrWhiteSpace(dep.acc_close_dt.ToString()) ? null : string.Concat("'", dep.acc_close_dt.Value.ToString("dd/MM/yyyy"), "'"),
                       string.Concat("'", dep.closing_prn_amt, "'"),
                       string.Concat("'", dep.closing_intt_amt, "'"),
                       string.Concat("'", dep.penal_amt, "'"),
                       string.Concat("'", dep.ext_instl_tot, "'"),
                       string.Concat("'", dep.mat_status, "'"),
                       string.Concat("'", dep.acc_status, "'"),
                       string.Concat("'", dep.curr_bal, "'"),
                       string.Concat("'", dep.clr_bal, "'"),
                       string.Concat("'", dep.standing_instr_flag, "'"),
                       string.Concat("'", dep.cheque_facility_flag, "'"),
                       string.Concat("'", dep.created_by, "'"),
                       string.IsNullOrWhiteSpace(dep.created_dt.ToString()) ? null : string.Concat("'", dep.created_dt.Value.ToString("dd/MM/yyyy"), "'"),
                       string.Concat("'", dep.modified_by, "'"),
                       string.IsNullOrWhiteSpace(dep.modified_dt.ToString()) ? null : string.Concat("'", dep.modified_dt.Value.ToString("dd/MM/yyyy"), "'"),
                       string.Concat("'", dep.approval_status, "'"),
                       string.Concat("'", dep.approved_by, "'"),
                       string.IsNullOrWhiteSpace(dep.approved_dt.ToString()) ? null : string.Concat("'", dep.approved_dt.Value.ToString("dd/MM/yyyy"), "'"),
                       string.Concat("'", dep.user_acc_num, "'"),
                       string.Concat("'", dep.lock_mode, "'"),
                       string.Concat("'", dep.loan_id, "'"),
                       string.Concat("'", dep.cert_no, "'"),
                       string.Concat("'", dep.bonus_amt, "'"),
                       string.Concat("'", dep.penal_intt_rt, "'"),
                       string.Concat("'", dep.bonus_intt_rt, "'"),
                       string.Concat("'", dep.transfer_flag, "'"),
                       string.IsNullOrWhiteSpace(dep.transfer_dt.ToString()) ? null : string.Concat("'", dep.transfer_dt.Value.ToString("dd/MM/yyyy"), "'"),
                       string.Concat("'", dep.agent_cd, "'"),
                       string.Concat("'", dep.brn_cd, "'"),
                       string.Concat("'", dep.acc_num, "'")
                       );

                        using (var command = OrclDbConnection.Command(connection, _statement))
                        {
                            command.ExecuteNonQuery();
                            transaction.Commit();
                            _ret = 0;
                        }
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
        internal int DeleteDepositTemp(tm_deposit dep)
        {
            int _ret = 0;

            string _query = " DELETE FROM TM_DEPOSIT_TEMP "
                          + " WHERE brn_cd = {0} AND acc_num = {1} AND ACC_TYPE_CD={2}";

            using (var connection = OrclDbConnection.NewConnection)
            {
                using (var transaction = connection.BeginTransaction())
                {
                    try
                    {
                        _statement = string.Format(_query,
                                             !string.IsNullOrWhiteSpace(dep.brn_cd) ? string.Concat("'", dep.brn_cd, "'") : "brn_cd",
                                             !string.IsNullOrWhiteSpace(dep.acc_num) ? string.Concat("'", dep.acc_num, "'") : "acc_num",
                                             dep.acc_type_cd != 0 ? Convert.ToString(dep.acc_type_cd) : "ACC_TYPE_CD"
                                              );

                        using (var command = OrclDbConnection.Command(connection, _statement))
                        {
                            command.ExecuteNonQuery();
                            transaction.Commit();
                            _ret = 0;
                        }
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
        internal List<tm_deposit> GetDeposit(tm_deposit dep)
        {
            List<tm_deposit> depoList = new List<tm_deposit>();

            string _query = " SELECT BRN_CD, ACC_TYPE_CD, ACC_NUM, RENEW_ID, CUST_CD, INTT_TRF_TYPE, CONSTITUTION_CD, "
                            + " OPRN_INSTR_CD, OPENING_DT, PRN_AMT, INTT_AMT, DEP_PERIOD, INSTL_AMT, INSTL_NO,   "
                            + " MAT_DT, INTT_RT, TDS_APPLICABLE, LAST_INTT_CALC_DT, ACC_CLOSE_DT,                "
                            + " CLOSING_PRN_AMT, CLOSING_INTT_AMT, PENAL_AMT, EXT_INSTL_TOT, MAT_STATUS, ACC_STATUS, "
                            + " CURR_BAL, CLR_BAL, STANDING_INSTR_FLAG, CHEQUE_FACILITY_FLAG,                         "
                            + " CREATED_BY, CREATED_DT, MODIFIED_BY, MODIFIED_DT, APPROVAL_STATUS, APPROVED_BY,       "
                            + " APPROVED_DT, USER_ACC_NUM, LOCK_MODE, LOAN_ID, CERT_NO, BONUS_AMT, PENAL_INTT_RT,     "
                            + " BONUS_INTT_RT, TRANSFER_FLAG, TRANSFER_DT, AGENT_CD                                   "
                            + " FROM TM_DEPOSIT                                                                  "
                            + " WHERE BRN_CD={0} AND ACC_NUM={1} AND ACC_TYPE_CD={2} ";

            using (var connection = OrclDbConnection.NewConnection)
            {
                _statement = string.Format(_query,
                                          !string.IsNullOrWhiteSpace(dep.brn_cd) ? string.Concat("'", dep.brn_cd, "'") : "brn_cd",
                                          !string.IsNullOrWhiteSpace(dep.acc_num) ? string.Concat("'", dep.acc_num, "'") : "acc_num",
                                          dep.acc_type_cd != 0 ? Convert.ToString(dep.acc_type_cd) : "ACC_TYPE_CD"
                                           );
                using (var command = OrclDbConnection.Command(connection, _statement))
                {
                    using (var reader = command.ExecuteReader())
                    {
                        try
                        {
                            if (reader.HasRows)
                            {
                                {
                                    while (reader.Read())
                                    {
                                        var d = new tm_deposit();
                                        d.brn_cd = UtilityM.CheckNull<string>(reader["BRN_CD"]);
                                        d.acc_type_cd = UtilityM.CheckNull<int>(reader["ACC_TYPE_CD"]);
                                        d.acc_num = UtilityM.CheckNull<string>(reader["ACC_NUM"]);
                                        d.renew_id = UtilityM.CheckNull<int>(reader["RENEW_ID"]);
                                        d.cust_cd = UtilityM.CheckNull<decimal>(reader["CUST_CD"]);
                                        d.intt_trf_type = UtilityM.CheckNull<string>(reader["INTT_TRF_TYPE"]);
                                        d.constitution_cd = UtilityM.CheckNull<int>(reader["CONSTITUTION_CD"]);
                                        d.oprn_instr_cd = UtilityM.CheckNull<int>(reader["OPRN_INSTR_CD"]);
                                        d.opening_dt = UtilityM.CheckNull<DateTime>(reader["OPENING_DT"]);
                                        d.prn_amt = UtilityM.CheckNull<decimal>(reader["PRN_AMT"]);
                                        d.intt_amt = UtilityM.CheckNull<decimal>(reader["INTT_AMT"]);
                                        d.dep_period = UtilityM.CheckNull<string>(reader["DEP_PERIOD"]);
                                        d.instl_amt = UtilityM.CheckNull<decimal>(reader["INSTL_AMT"]);
                                        d.instl_no = UtilityM.CheckNull<decimal>(reader["INSTL_NO"]);
                                        d.mat_dt = UtilityM.CheckNull<DateTime>(reader["MAT_DT"]);
                                        d.intt_rt = UtilityM.CheckNull<decimal>(reader["INTT_RT"]);
                                        d.tds_applicable = UtilityM.CheckNull<string>(reader["TDS_APPLICABLE"]);
                                        d.last_intt_calc_dt = UtilityM.CheckNull<DateTime>(reader["LAST_INTT_CALC_DT"]);
                                        d.acc_close_dt = UtilityM.CheckNull<DateTime>(reader["ACC_CLOSE_DT"]);
                                        d.closing_prn_amt = UtilityM.CheckNull<decimal>(reader["CLOSING_PRN_AMT"]);
                                        d.closing_intt_amt = UtilityM.CheckNull<decimal>(reader["CLOSING_INTT_AMT"]);
                                        d.penal_amt = UtilityM.CheckNull<decimal>(reader["PENAL_AMT"]);
                                        d.ext_instl_tot = UtilityM.CheckNull<decimal>(reader["EXT_INSTL_TOT"]);
                                        d.mat_status = UtilityM.CheckNull<string>(reader["MAT_STATUS"]);
                                        d.acc_status = UtilityM.CheckNull<string>(reader["ACC_STATUS"]);
                                        d.curr_bal = UtilityM.CheckNull<decimal>(reader["CURR_BAL"]);
                                        d.clr_bal = UtilityM.CheckNull<decimal>(reader["CLR_BAL"]);
                                        d.standing_instr_flag = UtilityM.CheckNull<string>(reader["STANDING_INSTR_FLAG"]);
                                        d.cheque_facility_flag = UtilityM.CheckNull<string>(reader["CHEQUE_FACILITY_FLAG"]);
                                        d.created_by = UtilityM.CheckNull<string>(reader["CREATED_BY"]);
                                        d.created_dt = UtilityM.CheckNull<DateTime>(reader["CREATED_DT"]);
                                        d.modified_by = UtilityM.CheckNull<string>(reader["MODIFIED_BY"]);
                                        d.modified_dt = UtilityM.CheckNull<DateTime>(reader["MODIFIED_DT"]);
                                        d.approval_status = UtilityM.CheckNull<string>(reader["APPROVAL_STATUS"]);
                                        d.approved_by = UtilityM.CheckNull<string>(reader["APPROVED_BY"]);
                                        d.approved_dt = UtilityM.CheckNull<DateTime>(reader["APPROVED_DT"]);
                                        d.user_acc_num = UtilityM.CheckNull<string>(reader["USER_ACC_NUM"]);
                                        d.lock_mode = UtilityM.CheckNull<string>(reader["LOCK_MODE"]);
                                        d.loan_id = UtilityM.CheckNull<string>(reader["LOAN_ID"]);
                                        d.cert_no = UtilityM.CheckNull<string>(reader["CERT_NO"]);
                                        d.bonus_amt = UtilityM.CheckNull<decimal>(reader["BONUS_AMT"]);
                                        d.penal_intt_rt = UtilityM.CheckNull<decimal>(reader["PENAL_INTT_RT"]);
                                        d.bonus_intt_rt = UtilityM.CheckNull<decimal>(reader["BONUS_INTT_RT"]);
                                        d.transfer_flag = UtilityM.CheckNull<string>(reader["TRANSFER_FLAG"]);
                                        d.transfer_dt = UtilityM.CheckNull<DateTime>(reader["TRANSFER_DT"]);
                                        d.agent_cd = UtilityM.CheckNull<string>(reader["AGENT_CD"]);

                                        depoList.Add(d);
                                    }
                                }
                            }

                        }
                        catch (Exception ex)
                        {

                        }
                    }
                }
            }

            return depoList;
        }
        internal decimal InsertDeposit(tm_deposit dep)
        {
            List<td_signatory> sigList = new List<td_signatory>();

            string _query = " INSERT INTO TM_DEPOSIT ( BRN_CD, ACC_TYPE_CD, ACC_NUM, RENEW_ID, CUST_CD, INTT_TRF_TYPE, CONSTITUTION_CD,"
                           + " OPRN_INSTR_CD, OPENING_DT, PRN_AMT, INTT_AMT, DEP_PERIOD, INSTL_AMT, INSTL_NO, MAT_DT, INTT_RT, TDS_APPLICABLE,     "
                           + " LAST_INTT_CALC_DT, ACC_CLOSE_DT, CLOSING_PRN_AMT, CLOSING_INTT_AMT, PENAL_AMT, EXT_INSTL_TOT, MAT_STATUS, ACC_STATUS,"
                           + " CURR_BAL, CLR_BAL, STANDING_INSTR_FLAG, CHEQUE_FACILITY_FLAG, CREATED_BY, CREATED_DT, MODIFIED_BY, MODIFIED_DT,      "
                           + " APPROVAL_STATUS, APPROVED_BY, APPROVED_DT, USER_ACC_NUM, LOCK_MODE, LOAN_ID, CERT_NO, BONUS_AMT, PENAL_INTT_RT,      "
                           + " BONUS_INTT_RT, TRANSFER_FLAG, TRANSFER_DT, AGENT_CD   "
                           + " VALUES({0},{1},{2},{3},{4},{5},{6},{7},to_date('{8}','dd-mm-yyyy' ),{9},{10},{11},{12},{13}, to_date('{14}','dd-mm-yyyy' ),"
                           + " {15},{16}, to_date('{17}','dd-mm-yyyy' ), to_date('{18}','dd-mm-yyyy' ),{19},{20},{21},{22},{23},{24}, "
                           + " {25},{26},{27},{28},{29}, to_date('{30}','dd-mm-yyyy' ),{31}, to_date('{32}','dd-mm-yyyy' ),{33}, "
                           + " to_date('{34}','dd-mm-yyyy' ),{35},{36}, to_date('{37}','dd-mm-yyyy' ),{38},{39},{40},{41},{42},{43},to_date('{44}','dd-mm-yyyy' ),{45})";

            using (var connection = OrclDbConnection.NewConnection)
            {
                using (var transaction = connection.BeginTransaction())
                {
                    try
                    {
                        _statement = string.Format(_query,
                       string.Concat("'", dep.brn_cd, "'"),
                       string.Concat("'", dep.acc_type_cd, "'"),
                       string.Concat("'", dep.acc_num, "'"),
                       string.Concat("'", dep.renew_id, "'"),
                       string.Concat("'", dep.cust_cd, "'"),
                       string.Concat("'", dep.intt_trf_type, "'"),
                       string.Concat("'", dep.constitution_cd, "'"),
                       string.Concat("'", dep.oprn_instr_cd, "'"),
                       string.IsNullOrWhiteSpace(dep.opening_dt.ToString()) ? null : string.Concat("'", dep.opening_dt.Value.ToString("dd/MM/yyyy"), "'"),
                       string.Concat("'", dep.prn_amt, "'"),
                       string.Concat("'", dep.intt_amt, "'"),
                       string.Concat("'", dep.dep_period, "'"),
                       string.Concat("'", dep.instl_amt, "'"),
                       string.Concat("'", dep.instl_no, "'"),
                       string.IsNullOrWhiteSpace(dep.mat_dt.ToString()) ? null : string.Concat("'", dep.mat_dt.Value.ToString("dd/MM/yyyy"), "'"),
                       string.Concat("'", dep.intt_rt, "'"),
                       string.Concat("'", dep.tds_applicable, "'"),
                       string.IsNullOrWhiteSpace(dep.last_intt_calc_dt.ToString()) ? null : string.Concat("'", dep.last_intt_calc_dt.Value.ToString("dd/MM/yyyy"), "'"),
                       string.IsNullOrWhiteSpace(dep.acc_close_dt.ToString()) ? null : string.Concat("'", dep.acc_close_dt.Value.ToString("dd/MM/yyyy"), "'"),
                       string.Concat("'", dep.closing_prn_amt, "'"),
                       string.Concat("'", dep.closing_intt_amt, "'"),
                       string.Concat("'", dep.penal_amt, "'"),
                       string.Concat("'", dep.ext_instl_tot, "'"),
                       string.Concat("'", dep.mat_status, "'"),
                       string.Concat("'", dep.acc_status, "'"),
                       string.Concat("'", dep.curr_bal, "'"),
                       string.Concat("'", dep.clr_bal, "'"),
                       string.Concat("'", dep.standing_instr_flag, "'"),
                       string.Concat("'", dep.cheque_facility_flag, "'"),
                       string.Concat("'", dep.created_by, "'"),
                       string.IsNullOrWhiteSpace(dep.created_dt.ToString()) ? null : string.Concat("'", dep.created_dt.Value.ToString("dd/MM/yyyy"), "'"),
                       string.Concat("'", dep.modified_by, "'"),
                       string.IsNullOrWhiteSpace(dep.modified_dt.ToString()) ? null : string.Concat("'", dep.modified_dt.Value.ToString("dd/MM/yyyy"), "'"),
                       string.Concat("'", dep.approval_status, "'"),
                       string.Concat("'", dep.approved_by, "'"),
                       string.IsNullOrWhiteSpace(dep.approved_dt.ToString()) ? null : string.Concat("'", dep.approved_dt.Value.ToString("dd/MM/yyyy"), "'"),
                       string.Concat("'", dep.user_acc_num, "'"),
                       string.Concat("'", dep.lock_mode, "'"),
                       string.Concat("'", dep.loan_id, "'"),
                       string.Concat("'", dep.cert_no, "'"),
                       string.Concat("'", dep.bonus_amt, "'"),
                       string.Concat("'", dep.penal_intt_rt, "'"),
                       string.Concat("'", dep.bonus_intt_rt, "'"),
                       string.Concat("'", dep.transfer_flag, "'"),
                       string.IsNullOrWhiteSpace(dep.transfer_dt.ToString()) ? null : string.Concat("'", dep.transfer_dt.Value.ToString("dd/MM/yyyy"), "'"),
                       string.Concat("'", dep.agent_cd, "'")
                                                    );

                        using (var command = OrclDbConnection.Command(connection, _statement))
                        {
                            command.ExecuteNonQuery();
                            transaction.Commit();
                        }

                    }
                    catch (Exception ex)
                    {
                        transaction.Rollback();
                        return -1;
                    }
                }
            }

            return 0;
        }
        internal int UpdateDeposit(tm_deposit dep)
        {
            int _ret = 0;

            string _query = " UPDATE TM_DEPOSIT SET "
               + "brn_cd               = NVL({0},  brn_cd              ),"
               + "acc_type_cd          = NVL({1},  acc_type_cd         ),"
               + "acc_num              = NVL({2},  acc_num             ),"
               + "renew_id             = NVL({3},  renew_id            ),"
               + "cust_cd              = NVL({4},  cust_cd             ),"
               + "intt_trf_type        = NVL({5},  intt_trf_type       ),"
               + "constitution_cd      = NVL({6},  constitution_cd     ),"
               + "oprn_instr_cd        = NVL({7},  oprn_instr_cd       ),"
               + "opening_dt           = NVL(to_date('{8}','dd-mm-yyyy' ),  opening_dt ),"
               + "prn_amt              = NVL({9},  prn_amt             ),"
               + "intt_amt             = NVL({10}, intt_amt            ),"
               + "dep_period           = NVL({11}, dep_period          ),"
               + "instl_amt            = NVL({12}, instl_amt           ),"
               + "instl_no             = NVL({13}, instl_no            ),"
               + "mat_dt               = NVL(to_date('{14}','dd-mm-yyyy' ), mat_dt ),"
               + "intt_rt              = NVL({15}, intt_rt             ),"
               + "tds_applicable       = NVL({16}, tds_applicable      ),"
               + "last_intt_calc_dt    = NVL(to_date('{17}','dd-mm-yyyy' ), last_intt_calc_dt ),"
               + "acc_close_dt         = NVL(to_date('{18}','dd-mm-yyyy' ), acc_close_dt ),"
               + "closing_prn_amt      = NVL({19}, closing_prn_amt     ),"
               + "closing_intt_amt     = NVL({20}, closing_intt_amt    ),"
               + "penal_amt            = NVL({21}, penal_amt           ),"
               + "ext_instl_tot        = NVL({22}, ext_instl_tot       ),"
               + "mat_status           = NVL({23}, mat_status          ),"
               + "acc_status           = NVL({24}, acc_status          ),"
               + "curr_bal             = NVL({25}, curr_bal            ),"
               + "clr_bal              = NVL({26}, clr_bal             ),"
               + "standing_instr_flag  = NVL({27}, standing_instr_flag ),"
               + "cheque_facility_flag = NVL({28}, cheque_facility_flag),"
               + "created_by           = NVL({29}, created_by          ),"
               + "created_dt           = NVL(to_date('{30}','dd-mm-yyyy' ), created_dt ),"
               + "modified_by          = NVL({31}, modified_by         ),"
               + "modified_dt          = NVL(to_date('{32}','dd-mm-yyyy' ), modified_dt ),"
               + "approval_status      = NVL({33}, approval_status     ),"
               + "approved_by          = NVL({34}, approved_by         ),"
               + "approved_dt          = NVL(to_date('{35}','dd-mm-yyyy' ), approved_dt ),"
               + "user_acc_num         = NVL({36}, user_acc_num        ),"
               + "lock_mode            = NVL({37}, lock_mode           ),"
               + "loan_id              = NVL({38}, loan_id             ),"
               + "cert_no              = NVL({39}, cert_no             ),"
               + "bonus_amt            = NVL({40}, bonus_amt           ),"
               + "penal_intt_rt        = NVL({41}, penal_intt_rt       ),"
               + "bonus_intt_rt        = NVL({42}, bonus_intt_rt       ),"
               + "transfer_flag        = NVL({43}, transfer_flag       ),"
               + "transfer_dt          = NVL(to_date('{44}','dd-mm-yyyy' ), transfer_dt ),"
               + "agent_cd             = NVL({45}, agent_cd            ) "
               + "WHERE brn_cd = NVL({46}, brn_cd) AND acc_num = NVL({47},  acc_num ) AND ACC_TYPE_CD={48}";

            using (var connection = OrclDbConnection.NewConnection)
            {
                using (var transaction = connection.BeginTransaction())
                {
                    try
                    {
                        _statement = string.Format(_query,
                       string.Concat("'", dep.brn_cd, "'"),
                       string.Concat("'", dep.acc_type_cd, "'"),
                       string.Concat("'", dep.acc_num, "'"),
                       string.Concat("'", dep.renew_id, "'"),
                       string.Concat("'", dep.cust_cd, "'"),
                       string.Concat("'", dep.intt_trf_type, "'"),
                       string.Concat("'", dep.constitution_cd, "'"),
                       string.Concat("'", dep.oprn_instr_cd, "'"),
                       string.IsNullOrWhiteSpace(dep.opening_dt.ToString()) ? null : string.Concat("'", dep.opening_dt.Value.ToString("dd/MM/yyyy"), ","),
                       string.Concat("'", dep.prn_amt, "'"),
                       string.Concat("'", dep.intt_amt, "'"),
                       string.Concat("'", dep.dep_period, "'"),
                       string.Concat("'", dep.instl_amt, "'"),
                       string.Concat("'", dep.instl_no, "'"),
                       string.IsNullOrWhiteSpace(dep.mat_dt.ToString()) ? null : string.Concat("'", dep.mat_dt.Value.ToString("dd/MM/yyyy"), "'"),
                       string.Concat("'", dep.intt_rt, "'"),
                       string.Concat("'", dep.tds_applicable, "'"),
                       string.IsNullOrWhiteSpace(dep.last_intt_calc_dt.ToString()) ? null : string.Concat("'", dep.last_intt_calc_dt.Value.ToString("dd/MM/yyyy"), "'"),
                       string.IsNullOrWhiteSpace(dep.acc_close_dt.ToString()) ? null : string.Concat("'", dep.acc_close_dt.Value.ToString("dd/MM/yyyy"), "'"),
                       string.Concat("'", dep.closing_prn_amt, "'"),
                       string.Concat("'", dep.closing_intt_amt, "'"),
                       string.Concat("'", dep.penal_amt, "'"),
                       string.Concat("'", dep.ext_instl_tot, "'"),
                       string.Concat("'", dep.mat_status, "'"),
                       string.Concat("'", dep.acc_status, "'"),
                       string.Concat("'", dep.curr_bal, "'"),
                       string.Concat("'", dep.clr_bal, "'"),
                       string.Concat("'", dep.standing_instr_flag, "'"),
                       string.Concat("'", dep.cheque_facility_flag, "'"),
                       string.Concat("'", dep.created_by, "'"),
                       string.IsNullOrWhiteSpace(dep.created_dt.ToString()) ? null : string.Concat("'", dep.created_dt.Value.ToString("dd/MM/yyyy"), "'"),
                       string.Concat("'", dep.modified_by, "'"),
                       string.IsNullOrWhiteSpace(dep.modified_dt.ToString()) ? null : string.Concat("'", dep.modified_dt.Value.ToString("dd/MM/yyyy"), "'"),
                       string.Concat("'", dep.approval_status, "'"),
                       string.Concat("'", dep.approved_by, "'"),
                       string.IsNullOrWhiteSpace(dep.approved_dt.ToString()) ? null : string.Concat("'", dep.approved_dt.Value.ToString("dd/MM/yyyy"), "'"),
                       string.Concat("'", dep.user_acc_num, "'"),
                       string.Concat("'", dep.lock_mode, "'"),
                       string.Concat("'", dep.loan_id, "'"),
                       string.Concat("'", dep.cert_no, "'"),
                       string.Concat("'", dep.bonus_amt, "'"),
                       string.Concat("'", dep.penal_intt_rt, "'"),
                       string.Concat("'", dep.bonus_intt_rt, "'"),
                       string.Concat("'", dep.transfer_flag, "'"),
                       string.IsNullOrWhiteSpace(dep.transfer_dt.ToString()) ? null : string.Concat("'", dep.transfer_dt.Value.ToString("dd/MM/yyyy"), "'"),
                       string.Concat("'", dep.agent_cd, "'"),
                       string.Concat("'", dep.brn_cd, "'"),
                       string.Concat("'", dep.acc_num, "'"),
                       string.Concat("'", dep.acc_type_cd != 0 ? Convert.ToString(dep.acc_type_cd) : "ACC_TYPE_CD", "'")
                       );

                        using (var command = OrclDbConnection.Command(connection, _statement))
                        {
                            command.ExecuteNonQuery();
                            transaction.Commit();
                            _ret = 0;
                        }
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
        internal int DeleteDeposit(tm_deposit dep)
        {
            int _ret = 0;

            string _query = " DELETE FROM TM_DEPOSIT "
                          + " WHERE brn_cd = {0} AND acc_num = {1} AND ACC_TYPE_CD={2} ";

            using (var connection = OrclDbConnection.NewConnection)
            {
                using (var transaction = connection.BeginTransaction())
                {
                    try
                    {
                        _statement = string.Format(_query,
                                             !string.IsNullOrWhiteSpace(dep.brn_cd) ? string.Concat("'", dep.brn_cd, "'") : "brn_cd",
                                             !string.IsNullOrWhiteSpace(dep.acc_num) ? string.Concat("'", dep.acc_num, "'") : "acc_num",
                                             dep.acc_type_cd != 0 ? Convert.ToString(dep.acc_type_cd) : "ACC_TYPE_CD"
                                              );

                        using (var command = OrclDbConnection.Command(connection, _statement))
                        {
                            command.ExecuteNonQuery();
                            transaction.Commit();
                            _ret = 0;
                        }
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
        internal List<tm_deposit> GetDepositView(tm_deposit dep)
        {
            List<tm_deposit> depoList = new List<tm_deposit>();

            string _query = " SELECT BRN_CD, ACC_TYPE_CD, ACC_NUM, RENEW_ID, CUST_CD, INTT_TRF_TYPE, CONSTITUTION_CD, "
                            + " OPRN_INSTR_CD, OPENING_DT, PRN_AMT, INTT_AMT, DEP_PERIOD, INSTL_AMT, INSTL_NO,   "
                            + " MAT_DT, INTT_RT, TDS_APPLICABLE, LAST_INTT_CALC_DT, ACC_CLOSE_DT,                "
                            + " CLOSING_PRN_AMT, CLOSING_INTT_AMT, PENAL_AMT, EXT_INSTL_TOT, MAT_STATUS, ACC_STATUS, "
                            + " CURR_BAL, CLR_BAL, STANDING_INSTR_FLAG, CHEQUE_FACILITY_FLAG,                         "
                            + " CREATED_BY, CREATED_DT, MODIFIED_BY, MODIFIED_DT, APPROVAL_STATUS, APPROVED_BY,       "
                            + " APPROVED_DT, USER_ACC_NUM, LOCK_MODE,  CERT_NO, BONUS_AMT, PENAL_INTT_RT,     "
                            + " BONUS_INTT_RT, TRANSFER_FLAG, TRANSFER_DT, AGENT_CD                                   "
                            + " FROM V_DEPOSIT                                                                  "
                            + " WHERE BRN_CD={0} AND ACC_NUM={1}                                                           ";

            using (var connection = OrclDbConnection.NewConnection)
            {
                _statement = string.Format(_query,
                                          !string.IsNullOrWhiteSpace(dep.brn_cd) ? string.Concat("'", dep.brn_cd, "'") : "brn_cd",
                                          !string.IsNullOrWhiteSpace(dep.acc_num) ? string.Concat("'", dep.acc_num, "'") : "acc_num"
                                           );
                using (var command = OrclDbConnection.Command(connection, _statement))
                {
                    using (var reader = command.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            {
                                while (reader.Read())
                                {
                                    var d = new tm_deposit();
                                    d.brn_cd = UtilityM.CheckNull<string>(reader["BRN_CD"]);
                                    d.acc_type_cd = UtilityM.CheckNull<int>(reader["ACC_TYPE_CD"]);
                                    d.acc_num = UtilityM.CheckNull<string>(reader["ACC_NUM"]);
                                    d.renew_id = UtilityM.CheckNull<int>(reader["RENEW_ID"]);
                                    d.cust_cd = UtilityM.CheckNull<decimal>(reader["CUST_CD"]);
                                    d.intt_trf_type = UtilityM.CheckNull<string>(reader["INTT_TRF_TYPE"]);
                                    d.constitution_cd = UtilityM.CheckNull<int>(reader["CONSTITUTION_CD"]);
                                    d.oprn_instr_cd = UtilityM.CheckNull<int>(reader["OPRN_INSTR_CD"]);
                                    d.opening_dt = UtilityM.CheckNull<DateTime>(reader["OPENING_DT"]);
                                    d.prn_amt = UtilityM.CheckNull<decimal>(reader["PRN_AMT"]);
                                    d.intt_amt = UtilityM.CheckNull<decimal>(reader["INTT_AMT"]);
                                    d.dep_period = UtilityM.CheckNull<string>(reader["DEP_PERIOD"]);
                                    d.instl_amt = UtilityM.CheckNull<decimal>(reader["INSTL_AMT"]);
                                    d.instl_no = UtilityM.CheckNull<decimal>(reader["INSTL_NO"]);
                                    d.mat_dt = UtilityM.CheckNull<DateTime>(reader["MAT_DT"]);
                                    d.intt_rt = UtilityM.CheckNull<decimal>(reader["INTT_RT"]);
                                    d.tds_applicable = UtilityM.CheckNull<string>(reader["TDS_APPLICABLE"]);
                                    d.last_intt_calc_dt = UtilityM.CheckNull<DateTime>(reader["LAST_INTT_CALC_DT"]);
                                    d.acc_close_dt = UtilityM.CheckNull<DateTime>(reader["ACC_CLOSE_DT"]);
                                    d.closing_prn_amt = UtilityM.CheckNull<decimal>(reader["CLOSING_PRN_AMT"]);
                                    d.closing_intt_amt = UtilityM.CheckNull<decimal>(reader["CLOSING_INTT_AMT"]);
                                    d.penal_amt = UtilityM.CheckNull<decimal>(reader["PENAL_AMT"]);
                                    d.ext_instl_tot = UtilityM.CheckNull<decimal>(reader["EXT_INSTL_TOT"]);
                                    d.mat_status = UtilityM.CheckNull<string>(reader["MAT_STATUS"]);
                                    d.acc_status = UtilityM.CheckNull<string>(reader["ACC_STATUS"]);
                                    d.curr_bal = UtilityM.CheckNull<decimal>(reader["CURR_BAL"]);
                                    d.clr_bal = UtilityM.CheckNull<decimal>(reader["CLR_BAL"]);
                                    d.standing_instr_flag = UtilityM.CheckNull<string>(reader["STANDING_INSTR_FLAG"]);
                                    d.cheque_facility_flag = UtilityM.CheckNull<string>(reader["CHEQUE_FACILITY_FLAG"]);
                                    d.created_by = UtilityM.CheckNull<string>(reader["CREATED_BY"]);
                                    d.created_dt = UtilityM.CheckNull<DateTime>(reader["CREATED_DT"]);
                                    d.modified_by = UtilityM.CheckNull<string>(reader["MODIFIED_BY"]);
                                    d.modified_dt = UtilityM.CheckNull<DateTime>(reader["MODIFIED_DT"]);
                                    d.approval_status = UtilityM.CheckNull<string>(reader["APPROVAL_STATUS"]);
                                    d.approved_by = UtilityM.CheckNull<string>(reader["APPROVED_BY"]);
                                    d.approved_dt = UtilityM.CheckNull<DateTime>(reader["APPROVED_DT"]);
                                    d.user_acc_num = UtilityM.CheckNull<string>(reader["USER_ACC_NUM"]);
                                    d.lock_mode = UtilityM.CheckNull<string>(reader["LOCK_MODE"]);
                                    d.cert_no = UtilityM.CheckNull<string>(reader["CERT_NO"]);
                                    d.bonus_amt = UtilityM.CheckNull<decimal>(reader["BONUS_AMT"]);
                                    d.penal_intt_rt = UtilityM.CheckNull<decimal>(reader["PENAL_INTT_RT"]);
                                    d.bonus_intt_rt = UtilityM.CheckNull<decimal>(reader["BONUS_INTT_RT"]);
                                    d.transfer_flag = UtilityM.CheckNull<string>(reader["TRANSFER_FLAG"]);
                                    d.transfer_dt = UtilityM.CheckNull<DateTime>(reader["TRANSFER_DT"]);
                                    d.agent_cd = UtilityM.CheckNull<string>(reader["AGENT_CD"]);

                                    depoList.Add(d);
                                }
                            }
                        }
                    }
                }
            }

            return depoList;
        }

        internal List<tm_depositall> GetDepositWithChild(tm_depositall dep)
        {
            List<tm_depositall> depoList = new List<tm_depositall>();

            string _query = " SELECT TD.BRN_CD BRN_CD, TD.ACC_TYPE_CD ACC_TYPE_CD, TD.ACC_NUM ACC_NUM, TD.RENEW_ID RENEW_ID, TD.CUST_CD CUST_CD, TD.INTT_TRF_TYPE INTT_TRF_TYPE, TD.CONSTITUTION_CD CONSTITUTION_CD, "
                            + " TD.OPRN_INSTR_CD OPRN_INSTR_CD, TD.OPENING_DT OPENING_DT, TD.PRN_AMT PRN_AMT, TD.INTT_AMT INTT_AMT, TD.DEP_PERIOD DEP_PERIOD, TD.INSTL_AMT INSTL_AMT, TD.INSTL_NO INSTL_NO,   "
                            + " TD.MAT_DT MAT_DT, TD.INTT_RT INTT_RT, TD.TDS_APPLICABLE TDS_APPLICABLE, TD.LAST_INTT_CALC_DT LAST_INTT_CALC_DT, TD.ACC_CLOSE_DT ACC_CLOSE_DT,                "
                            + " TD.CLOSING_PRN_AMT CLOSING_PRN_AMT, TD.CLOSING_INTT_AMT CLOSING_INTT_AMT, TD.PENAL_AMT PENAL_AMT, TD.EXT_INSTL_TOT EXT_INSTL_TOT, TD.MAT_STATUS MAT_STATUS, TD.ACC_STATUS ACC_STATUS, "
                            + " TD.CURR_BAL CURR_BAL, TD.CLR_BAL CLR_BAL, TD.STANDING_INSTR_FLAG STANDING_INSTR_FLAG, TD.CHEQUE_FACILITY_FLAG CHEQUE_FACILITY_FLAG,                         "
                            + " TD.CREATED_BY CREATED_BY, TD.CREATED_DT CREATED_DT, TD.MODIFIED_BY MODIFIED_BY, TD.MODIFIED_DT MODIFIED_DT, TD.APPROVAL_STATUS APPROVAL_STATUS, TD.APPROVED_BY APPROVED_BY,       "
                            + " TD.APPROVED_DT APPROVED_DT, TD.USER_ACC_NUM USER_ACC_NUM, TD.LOCK_MODE LOCK_MODE, TD.LOAN_ID LOAN_ID, TD.CERT_NO CERT_NO, TD.BONUS_AMT BONUS_AMT, TD.PENAL_INTT_RT PENAL_INTT_RT,     "
                            + " TD.BONUS_INTT_RT BONUS_INTT_RT, TD.TRANSFER_FLAG TRANSFER_FLAG, TD.TRANSFER_DT TRANSFER_DT, TD.AGENT_CD AGENT_CD,MC.CUST_NAME CUST_NAME,MC.PERMANENT_ADDRESS PERMANENT_ADDRESS,MC.PHONE PHONE,MC.GUARDIAN_NAME GUARDIAN_NAME,MC.DT_OF_BIRTH DT_OF_BIRTH,MC.SEX SEX,MC.OCCUPATION OCCUPATION,MC.PRESENT_ADDRESS PRESENT_ADDRESS,MC.CUST_TYPE CUST_TYPE,MC.AGE AGE  "
                           + " , MC.KYC_PHOTO_TYPE KYC_PHOTO_TYPE,MC.KYC_PHOTO_NO KYC_PHOTO_NO,MC.KYC_ADDRESS_TYPE KYC_ADDRESS_TYPE,MC.KYC_ADDRESS_NO KYC_ADDRESS_NO ,MCO.CONSTITUTION_DESC CONSTITUTION_DESC,MCO.CONSTITUTION_CD CONSTITUTION_CD,MCO.ACC_CD ACC_CD,MCO.INTT_ACC_CD INTT_ACC_CD,MCO.INTT_PROV_ACC_CD INTT_PROV_ACC_CD "
                            + " FROM TM_DEPOSIT TD,  MM_CUSTOMER MC,  MM_CONSTITUTION MCO   "
                            + " WHERE TD.BRN_CD={0} AND TD.ACC_NUM={1} AND TD.ACC_TYPE_CD={2} AND TD.CUST_CD=MC.CUST_CD AND TD.CONSTITUTION_CD =MCO.CONSTITUTION_CD AND TD.ACC_TYPE_CD=MCO.ACC_TYPE_CD ";

            using (var connection = OrclDbConnection.NewConnection)
            {
                _statement = string.Format(_query,
                                          !string.IsNullOrWhiteSpace(dep.brn_cd) ? string.Concat("'", dep.brn_cd, "'") : "brn_cd",
                                          !string.IsNullOrWhiteSpace(dep.acc_num) ? string.Concat("'", dep.acc_num, "'") : "acc_num",
                                          dep.acc_type_cd != 0 ? Convert.ToString(dep.acc_type_cd) : "ACC_TYPE_CD"
                                           );
                using (var command = OrclDbConnection.Command(connection, _statement))
                {
                    using (var reader = command.ExecuteReader())
                    {
                        try
                        {
                            if (reader.HasRows)
                            {
                                {
                                    while (reader.Read())
                                    {
                                        var d = new tm_depositall();
                                        d.brn_cd = UtilityM.CheckNull<string>(reader["BRN_CD"]);
                                        d.acc_type_cd = UtilityM.CheckNull<int>(reader["ACC_TYPE_CD"]);
                                        d.acc_num = UtilityM.CheckNull<string>(reader["ACC_NUM"]);
                                        d.renew_id = UtilityM.CheckNull<int>(reader["RENEW_ID"]);
                                        d.cust_cd = UtilityM.CheckNull<decimal>(reader["CUST_CD"]);
                                        d.intt_trf_type = UtilityM.CheckNull<string>(reader["INTT_TRF_TYPE"]);
                                        d.constitution_cd = UtilityM.CheckNull<int>(reader["CONSTITUTION_CD"]);
                                        d.oprn_instr_cd = UtilityM.CheckNull<int>(reader["OPRN_INSTR_CD"]);
                                        d.opening_dt = UtilityM.CheckNull<DateTime>(reader["OPENING_DT"]);
                                        d.prn_amt = UtilityM.CheckNull<decimal>(reader["PRN_AMT"]);
                                        d.intt_amt = UtilityM.CheckNull<decimal>(reader["INTT_AMT"]);
                                        d.dep_period = UtilityM.CheckNull<string>(reader["DEP_PERIOD"]);
                                        d.instl_amt = UtilityM.CheckNull<decimal>(reader["INSTL_AMT"]);
                                        d.instl_no = UtilityM.CheckNull<decimal>(reader["INSTL_NO"]);
                                        d.mat_dt = UtilityM.CheckNull<DateTime>(reader["MAT_DT"]);
                                        d.intt_rt = UtilityM.CheckNull<decimal>(reader["INTT_RT"]);
                                        d.tds_applicable = UtilityM.CheckNull<string>(reader["TDS_APPLICABLE"]);
                                        d.last_intt_calc_dt = UtilityM.CheckNull<DateTime>(reader["LAST_INTT_CALC_DT"]);
                                        d.acc_close_dt = UtilityM.CheckNull<DateTime>(reader["ACC_CLOSE_DT"]);
                                        d.closing_prn_amt = UtilityM.CheckNull<decimal>(reader["CLOSING_PRN_AMT"]);
                                        d.closing_intt_amt = UtilityM.CheckNull<decimal>(reader["CLOSING_INTT_AMT"]);
                                        d.penal_amt = UtilityM.CheckNull<decimal>(reader["PENAL_AMT"]);
                                        d.ext_instl_tot = UtilityM.CheckNull<decimal>(reader["EXT_INSTL_TOT"]);
                                        d.mat_status = UtilityM.CheckNull<string>(reader["MAT_STATUS"]);
                                        d.acc_status = UtilityM.CheckNull<string>(reader["ACC_STATUS"]);
                                        d.curr_bal = UtilityM.CheckNull<decimal>(reader["CURR_BAL"]);
                                        d.clr_bal = UtilityM.CheckNull<decimal>(reader["CLR_BAL"]);
                                        d.standing_instr_flag = UtilityM.CheckNull<string>(reader["STANDING_INSTR_FLAG"]);
                                        d.cheque_facility_flag = UtilityM.CheckNull<string>(reader["CHEQUE_FACILITY_FLAG"]);
                                        d.created_by = UtilityM.CheckNull<string>(reader["CREATED_BY"]);
                                        d.created_dt = UtilityM.CheckNull<DateTime>(reader["CREATED_DT"]);
                                        d.modified_by = UtilityM.CheckNull<string>(reader["MODIFIED_BY"]);
                                        d.modified_dt = UtilityM.CheckNull<DateTime>(reader["MODIFIED_DT"]);
                                        d.approval_status = UtilityM.CheckNull<string>(reader["APPROVAL_STATUS"]);
                                        d.approved_by = UtilityM.CheckNull<string>(reader["APPROVED_BY"]);
                                        d.approved_dt = UtilityM.CheckNull<DateTime>(reader["APPROVED_DT"]);
                                        d.user_acc_num = UtilityM.CheckNull<string>(reader["USER_ACC_NUM"]);
                                        d.lock_mode = UtilityM.CheckNull<string>(reader["LOCK_MODE"]);
                                        d.loan_id = Convert.ToString(UtilityM.CheckNull<decimal>(reader["LOAN_ID"]));
                                        d.cert_no = UtilityM.CheckNull<string>(reader["CERT_NO"]);
                                        d.bonus_amt = UtilityM.CheckNull<decimal>(reader["BONUS_AMT"]);
                                        d.penal_intt_rt = UtilityM.CheckNull<decimal>(reader["PENAL_INTT_RT"]);
                                        d.bonus_intt_rt = UtilityM.CheckNull<decimal>(reader["BONUS_INTT_RT"]);
                                        d.transfer_flag = UtilityM.CheckNull<string>(reader["TRANSFER_FLAG"]);
                                        d.transfer_dt = UtilityM.CheckNull<DateTime>(reader["TRANSFER_DT"]);
                                        d.agent_cd = UtilityM.CheckNull<string>(reader["AGENT_CD"]);
                                        d.cust_type = UtilityM.CheckNull<string>(reader["CUST_TYPE"]);
                                        //d.title = UtilityM.CheckNull<string>(reader["TITLE"]);
                                        //d.first_name = UtilityM.CheckNull<string>(reader["FIRST_NAME"]);
                                        //d.middle_name = UtilityM.CheckNull<string>(reader["MIDDLE_NAME"]);
                                        //d.last_name = UtilityM.CheckNull<string>(reader["LAST_NAME"]);
                                        d.cust_name = UtilityM.CheckNull<string>(reader["CUST_NAME"]);
                                        d.guardian_name = UtilityM.CheckNull<string>(reader["GUARDIAN_NAME"]);
                                        //d.cust_dt = UtilityM.CheckNull<DateTime>(reader["CUST_DT"]);
                                        d.dt_of_birth = UtilityM.CheckNull<DateTime>(reader["DT_OF_BIRTH"]);
                                        d.age = UtilityM.CheckNull<decimal>(reader["AGE"]);
                                        d.sex = UtilityM.CheckNull<string>(reader["SEX"]);
                                        //d.marital_status = UtilityM.CheckNull<string>(reader["MARITAL_STATUS"]);
                                        //d.catg_cd = UtilityM.CheckNull<int>(reader["CATG_CD"]);
                                        //d.community = UtilityM.CheckNull<decimal>(reader["COMMUNITY"]);
                                        //d.caste = UtilityM.CheckNull<decimal>(reader["CASTE"]);
                                        d.permanent_address = UtilityM.CheckNull<string>(reader["PERMANENT_ADDRESS"]);
                                        //d.ward_no = UtilityM.CheckNull<decimal>(reader["WARD_NO"]);
                                        //d.state = UtilityM.CheckNull<string>(reader["STATE"]);
                                        //d.dist = UtilityM.CheckNull<string>(reader["DIST"]);
                                        //d.pin = UtilityM.CheckNull<int>(reader["PIN"]);
                                        //d.vill_cd = UtilityM.CheckNull<string>(reader["VILL_CD"]);
                                        //d.block_cd = UtilityM.CheckNull<string>(reader["BLOCK_CD"]);
                                        //d.service_area_cd = UtilityM.CheckNull<string>(reader["SERVICE_AREA_CD"]);
                                        d.occupation = UtilityM.CheckNull<string>(reader["OCCUPATION"]);
                                        d.phone = UtilityM.CheckNull<string>(reader["PHONE"]);
                                        d.present_address = UtilityM.CheckNull<string>(reader["PRESENT_ADDRESS"]);
                                        d.kyc_photo_type = UtilityM.CheckNull<string>(reader["KYC_PHOTO_TYPE"]);
                                        d.kyc_photo_no = UtilityM.CheckNull<string>(reader["KYC_PHOTO_NO"]);
                                        d.kyc_address_type = UtilityM.CheckNull<string>(reader["KYC_ADDRESS_TYPE"]);
                                        d.kyc_address_no = UtilityM.CheckNull<string>(reader["KYC_ADDRESS_NO"]);
                                        d.constitution_desc = UtilityM.CheckNull<string>(reader["CONSTITUTION_DESC"]);
                                        d.constitution_cd = UtilityM.CheckNull<int>(reader["CONSTITUTION_CD"]);
                                        d.acc_cd = UtilityM.CheckNull<int>(reader["ACC_CD"]);
                                        d.intt_acc_cd = UtilityM.CheckNull<int>(reader["INTT_ACC_CD"]);
                                        d.intt_prov_acc_cd = UtilityM.CheckNull<int>(reader["INTT_PROV_ACC_CD"]);

                                        depoList.Add(d);
                                    }
                                }
                            }

                        }
                        catch (Exception ex)
                        {

                        }
                    }
                }
            }

            return depoList;
        }

        internal string ApproveAccountTranaction(p_gen_param pgp)
        {
            string _ret = null;

            using (var connection = OrclDbConnection.NewConnection)
            {
                using (var transaction = connection.BeginTransaction())
                {
                    try
                    {
                        DepTransactionDL _dl1 = new DepTransactionDL();
                        _ret = _dl1.P_UPDATE_TD_DEP_TRANS(connection, pgp);
                        if (_ret == "0")
                        {
                            DenominationDL _dl2 = new DenominationDL();
                            _ret = _dl2.P_UPDATE_DENOMINATION(connection, pgp);
                            if (_ret == "0")
                            {
                                string _query1 = "UPDATE TD_DEP_TRANS SET "
                                              + " MODIFIED_BY            =NVL({0},MODIFIED_BY    ),"
                                              + " MODIFIED_DT            =SYSDATE,"
                                              + " APPROVAL_STATUS        =NVL({1},APPROVAL_STATUS),"
                                              + " APPROVED_BY            =NVL({2},APPROVED_BY    ),"
                                              + " APPROVED_DT            =SYSDATE"
                                              + " WHERE (BRN_CD = {3}) AND "
                                              + " (TRANS_DT = to_date('{4}','dd-mm-yyyy' )) AND  "
                                              + " (  TRANS_CD = {5} ) ";
                                _statement = string.Format(_query1,
                                        string.Concat("'", pgp.gs_user_id, "'"),
                                        string.Concat("'", "A", "'"),
                                        string.Concat("'", pgp.gs_user_id, "'"),
                                        string.Concat("'", pgp.brn_cd, "'"),
                                        string.Concat(pgp.adt_trans_dt.ToString("dd/MM/yyyy")),
                                        string.Concat(pgp.ad_trans_cd)
                                        );
                                using (var command = OrclDbConnection.Command(connection, _statement))
                                {
                                    command.ExecuteNonQuery();
                                }
                                string _query2 = "UPDATE TD_DEP_TRANS_TRF SET "
                                          + " MODIFIED_BY            =NVL({0},MODIFIED_BY    ),"
                                          + " MODIFIED_DT            =SYSDATE,"
                                          + " APPROVAL_STATUS        =NVL({1},APPROVAL_STATUS),"
                                          + " APPROVED_BY            =NVL({2},APPROVED_BY    ),"
                                          + " APPROVED_DT            =SYSDATE"
                                          + " WHERE (BRN_CD = {3}) AND "
                                          + " (TRANS_DT = to_date('{4}','dd-mm-yyyy' )) AND  "
                                          + " (  TRANS_CD = {5} ) ";
                                _statement = string.Format(_query2,
                                        string.Concat("'", pgp.gs_user_id, "'"),
                                        string.Concat("'", "A", "'"),
                                        string.Concat("'", pgp.gs_user_id, "'"),
                                        string.Concat("'", pgp.brn_cd, "'"),
                                        string.Concat(pgp.adt_trans_dt.ToString("dd/MM/yyyy")),
                                        string.Concat(pgp.ad_trans_cd)
                                        );
                                using (var command = OrclDbConnection.Command(connection, _statement))
                                {
                                    command.ExecuteNonQuery();
                                }
                                string _query = "UPDATE TM_TRANSFER SET "
                                          + " APPROVAL_STATUS        =NVL({0},APPROVAL_STATUS),"
                                          + " APPROVED_BY            =NVL({1},APPROVED_BY    ),"
                                          + " APPROVED_DT            =SYSDATE"
                                          + " WHERE (BRN_CD = {2}) AND "
                                          + " (TRF_DT = to_date('{3}','dd-mm-yyyy' )) AND  "
                                          + " (  TRANS_CD = {4} ) ";
                                _statement = string.Format(_query,
                                        string.Concat("'", "A", "'"),
                                        string.Concat("'", pgp.gs_user_id, "'"),
                                        string.Concat("'", pgp.brn_cd, "'"),
                                        string.Concat(pgp.adt_trans_dt.ToString("dd/MM/yyyy")),
                                        string.Concat(pgp.ad_trans_cd)
                                        );
                                using (var command = OrclDbConnection.Command(connection, _statement))
                                {
                                    command.ExecuteNonQuery();
                                }
                                transaction.Commit();
                                return "0";

                            }
                            else
                            {
                                transaction.Rollback();
                                return _ret;
                            }
                        }
                        else
                        {
                            transaction.Rollback();
                            return _ret;
                        }
                    }
                    catch (Exception ex)
                    {

                        transaction.Rollback();
                        return ex.Message.ToString();
                    }

                }
            }
        }


        internal int isDormantAccount(tm_deposit dep)
        {
            int d = 0;

            string _query = " select  count(*)  CNT "
+ " from v_trans_dtls"
+ " where acc_type_cd = 1                                 "
+ " and acc_num = {0}                                  "
+ " and lower(particulars) not like '%interest%'       "
+ " and lower(particulars) not like '%insurance%'      "
+ " and lower(particulars) not like '%sms%' "
+ " and lower(particulars) not like '%dividend%'       "
+ " and lower(particulars) not like '%passbook%'       "
+ " and lower(particulars) not like '%insurance%'      "
+ " and lower(particulars) not like '%divident%' "
+ " and lower(particulars) not like '%TO SMS CHARGES%' "
+ " and lower(particulars) not like '%TO ATM CHARGES%' "
+ " and to_date(trans_dt,'dd/mm/yyyy') > to_date(to_date(f_getparamval('206'),'dd/mm/yyyy') - 365,'dd/mm/yyyy')  ";
            using (var connection = OrclDbConnection.NewConnection)
            {
                _statement = string.Format(_query,
                                          !string.IsNullOrWhiteSpace(dep.acc_num) ? string.Concat("'", dep.acc_num, "'") : "acc_num"
                                           );
                using (var command = OrclDbConnection.Command(connection, _statement))
                {
                    using (var reader = command.ExecuteReader())
                    {
                        try
                        {
                            if (reader.HasRows)
                            {
                                {
                                    while (reader.Read())
                                    {

                                        d = (int)UtilityM.CheckNull<decimal>(reader["CNT"]);
                                    }
                                }
                            }

                        }
                        catch (Exception ex)
                        {

                        }
                    }
                }
            }

            return d;
        }

        internal List<td_def_trans_trf> GetPrevTransaction(tm_deposit dep)
        {
            List<td_def_trans_trf> tdtRets = new List<td_def_trans_trf>();
            string _query = "SELECT V_TRANS_DTLS.TRANS_DT,V_TRANS_DTLS.TRANS_MODE, "
+ "V_TRANS_DTLS.ACC_TYPE_CD, "
+ "V_TRANS_DTLS.ACC_NUM, "
+ "V_TRANS_DTLS.TRANS_TYPE, "
+ "V_TRANS_DTLS.INSTRUMENT_NUM, "
+ "V_TRANS_DTLS.AMOUNT, "
+ "V_TRANS_DTLS.PARTICULARS, "
+ "V_TRANS_DTLS.TRANS_CD , "
+ "GM_SB_BALANCE.BALANCE_AMT "
+ " FROM V_TRANS_DTLS V_TRANS_DTLS, "
+ " GM_SB_BALANCE GM_SB_BALANCE "
+ " WHERE  V_TRANS_DTLS.ACC_TYPE_CD = GM_SB_BALANCE.ACC_TYPE_CD  and "
+ "  V_TRANS_DTLS.ACC_NUM = GM_SB_BALANCE.ACC_NUM  and "
+ "  GM_SB_BALANCE.BALANCE_DT = (SELECT MAX(BALANCE_DT) "
+ " FROM   GM_SB_BALANCE "
+ " WHERE  ACC_TYPE_CD = {0} "
+ " AND ACC_NUM = {1} "
+ " AND BALANCE_DT  <> to_date(f_getparamval('779'),'dd/mm/yyyy hh24:mi:ss'))  "
+ " AND  V_TRANS_DTLS.ACC_TYPE_CD = {3}  AND "
+ "  V_TRANS_DTLS.ACC_NUM = {4}  AND "
+ "  V_TRANS_DTLS.APPROVAL_STATUS IN  ({5},{6})  AND "
+ " V_TRANS_DTLS.TRANS_TYPE <> {7} ORDER BY  V_TRANS_DTLS.TRANS_DT DESC,V_TRANS_DTLS.TRANS_CD DESC";
            using (var connection = OrclDbConnection.NewConnection)
            {
                _statement = string.Format(_query,
                                            dep.acc_type_cd != 0 ? Convert.ToString(dep.acc_type_cd) : "ACC_TYPE_CD",
                                            !string.IsNullOrWhiteSpace(dep.acc_num) ? string.Concat("'", dep.acc_num, "'") : "acc_num",
                                             string.Concat("'", "779", "'"),
                                            dep.acc_type_cd != 0 ? Convert.ToString(dep.acc_type_cd) : "ACC_TYPE_CD",
                                            !string.IsNullOrWhiteSpace(dep.acc_num) ? string.Concat("'", dep.acc_num, "'") : "acc_num",
                                            string.Concat("'", "A", "'"),
                                            string.Concat("'", "B", "'"),
                                            string.Concat("'", "I", "'")
                                            );
                using (var command = OrclDbConnection.Command(connection, _statement))
                {
                    using (var reader = command.ExecuteReader())
                    {
                        try
                        {
                            if (reader.HasRows)
                            {
                                {
                                    while (reader.Read())
                                    {
                                        var tdtr = new td_def_trans_trf();
                                        tdtr.trans_dt = UtilityM.CheckNull<DateTime>(reader["TRANS_DT"]);
                                        tdtr.trans_mode = UtilityM.CheckNull<string>(reader["TRANS_MODE"]);
                                        tdtr.acc_type_cd = UtilityM.CheckNull<Int32>(reader["ACC_TYPE_CD"]);
                                        tdtr.acc_num = UtilityM.CheckNull<string>(reader["ACC_NUM"]);
                                        tdtr.trans_type = UtilityM.CheckNull<string>(reader["TRANS_TYPE"]);
                                        tdtr.instrument_num = UtilityM.CheckNull<Int64>(reader["INSTRUMENT_NUM"]);
                                        tdtr.amount = UtilityM.CheckNull<double>(reader["AMOUNT"]);
                                        tdtr.remarks = UtilityM.CheckNull<string>(reader["PARTICULARS"]);
                                        tdtr.trans_cd = UtilityM.CheckNull<Int64>(reader["TRANS_CD"]);
                                        tdtr.share_amt = UtilityM.CheckNull<decimal>(reader["BALANCE_AMT"]);
                                        tdtRets.Add(tdtr);
                                    }
                                }
                            }

                        }
                        catch (Exception ex)
                        {

                        }
                    }
                }
            }

            return tdtRets;
        }
        internal int UpdateDepositLockUnlock(tm_deposit dep)
        {
            int _ret = 0;

            string _query = " UPDATE TM_DEPOSIT SET "
               + " modified_by          = NVL({0}, modified_by ),"
               + " modified_dt          = SYSDATE,"
               + " lock_mode            = NVL({1}, lock_mode )"
               + " WHERE brn_cd = NVL({2}, brn_cd) "
               + " AND acc_num = {3} "
               + " AND ACC_TYPE_CD = {4}";

            using (var connection = OrclDbConnection.NewConnection)
            {
                using (var transaction = connection.BeginTransaction())
                {
                    try
                    {
                        _statement = string.Format(_query,
                        string.Concat("'", dep.modified_by, "'"),
                        string.Concat("'", dep.lock_mode, "'"),
                        string.Concat("'", dep.brn_cd, "'"),
                        string.Concat("'", dep.acc_num, "'"),
                        string.Concat("'", dep.acc_type_cd != 0 ? Convert.ToString(dep.acc_type_cd) : "ACC_TYPE_CD", "'")
                        );

                        using (var command = OrclDbConnection.Command(connection, _statement))
                        {
                            command.ExecuteNonQuery();
                            transaction.Commit();
                            _ret = 0;
                        }
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

        internal AccOpenDM GetDepositAddlInfo(tm_deposit td)
        {
            AccOpenDM AccOpenDMRet = new AccOpenDM();

            try
            {

                IntroducerDL introducerDL = new IntroducerDL();
                td_introducer td_Introducer = new td_introducer();
                td_Introducer.acc_num = td.acc_num;
                td_Introducer.acc_type_cd = td.acc_type_cd;
                td_Introducer.brn_cd = td.brn_cd;

                AccOpenDMRet.tdintroducer = introducerDL.GetIntroducer(td_Introducer);

                NomineeDL nomineeDL = new NomineeDL();
                td_nominee td_Nominee = new td_nominee();
                td_Nominee.acc_num = td.acc_num;
                td_Nominee.acc_type_cd = td.acc_type_cd;
                td_Nominee.brn_cd = td.brn_cd;

                AccOpenDMRet.tdnominee = nomineeDL.GetNominee(td_Nominee);

                SignatoryDL SignatoryDL = new SignatoryDL();
                td_signatory td_Signatory = new td_signatory();
                td_Signatory.acc_num = td.acc_num;
                td_Signatory.acc_type_cd = td.acc_type_cd;
                td_Signatory.brn_cd = td.brn_cd;


                AccOpenDMRet.tdsignatory = SignatoryDL.GetSignatory(td_Signatory);

                AccholderDL accholderDL = new AccholderDL();
                td_accholder td_Accholder = new td_accholder();
                td_Accholder.acc_num = td.acc_num;
                td_Accholder.acc_type_cd = td.acc_type_cd;
                td_Accholder.brn_cd = td.brn_cd;
                AccOpenDMRet.tdaccholder = accholderDL.GetAccholder(td_Accholder);
                return AccOpenDMRet;
            }
            catch (Exception ex)
            {
                // transaction.Rollback();
                return null;
            }


        }
        internal List<AccDtlsLov> GetAccDtls(p_gen_param prm)
        {
            List<AccDtlsLov> accDtlsLovs = new List<AccDtlsLov>();

            using (var connection = OrclDbConnection.NewConnection)
            {
                using (var command = OrclDbConnection.Command(connection, "P_GET_ACC_DTLS"))
                {
                    // ad_acc_type_cd NUMBER,as_cust_name VARCHAR2
                    command.CommandType = System.Data.CommandType.StoredProcedure;
                    var parm = new OracleParameter("ad_acc_type_cd", OracleDbType.Decimal, ParameterDirection.Input);
                    parm.Value = prm.ad_acc_type_cd;
                    command.Parameters.Add(parm);
                    parm = new OracleParameter("as_cust_name", OracleDbType.Varchar2, ParameterDirection.Input);
                    parm.Value = prm.as_cust_name;
                    command.Parameters.Add(parm);
                    parm = new OracleParameter("p_acc_refcur", OracleDbType.RefCursor, ParameterDirection.Output);
                    command.Parameters.Add(parm);
                    using (var reader = command.ExecuteReader())
                    {
                        try
                        {
                            if (reader.HasRows)
                            {
                                {
                                    while (reader.Read())
                                    {
                                        var accDtlsLov = new AccDtlsLov();
                                        accDtlsLov.acc_num = UtilityM.CheckNull<string>(reader["ACC_NUM"]);
                                        accDtlsLov.cust_name = UtilityM.CheckNull<string>(reader["CUST_NAME"]);
                                        accDtlsLov.guardian_name = UtilityM.CheckNull<string>(reader["GUARDIAN_NAME"]);
                                        accDtlsLov.present_address = UtilityM.CheckNull<string>(reader["PRESENT_ADDRESS"]);
                                        accDtlsLov.phone = UtilityM.CheckNull<string>(reader["PHONE"]);
                                        accDtlsLov.opening_dt = UtilityM.CheckNull<DateTime>(reader["OPENING_DT"]);

                                        accDtlsLovs.Add(accDtlsLov);
                                    }
                                }
                            }

                        }
                        catch (Exception ex)
                        {

                        }
                    }
                }
            }

            return accDtlsLovs;
        }

        internal List<mm_customer> GetCustDtls(p_gen_param prm)
        {
            List<mm_customer> customers = new List<mm_customer>();

            using (var connection = OrclDbConnection.NewConnection)
            {
                using (var command = OrclDbConnection.Command(connection, "P_GET_CUST_DTLS"))
                {
                    // ad_acc_type_cd NUMBER,as_cust_name VARCHAR2
                    command.CommandType = System.Data.CommandType.StoredProcedure;
                    var parm = new OracleParameter("as_cust_name", OracleDbType.Varchar2, ParameterDirection.Input);
                    parm.Value = prm.as_cust_name;
                    command.Parameters.Add(parm);
                    parm = new OracleParameter("p_acc_refcur", OracleDbType.RefCursor, ParameterDirection.Output);
                    command.Parameters.Add(parm);
                    using (var reader = command.ExecuteReader())
                    {
                        try
                        {
                            if (reader.HasRows)
                            {
                                {
                                    while (reader.Read())
                                    {
                                        var mc = new mm_customer();
                                        mc.brn_cd = UtilityM.CheckNull<string>(reader["BRN_CD"]);
                                        mc.cust_cd = UtilityM.CheckNull<decimal>(reader["CUST_CD"]);
                                        mc.cust_type = UtilityM.CheckNull<string>(reader["CUST_TYPE"]);
                                        mc.title = UtilityM.CheckNull<string>(reader["TITLE"]);
                                        mc.first_name = UtilityM.CheckNull<string>(reader["FIRST_NAME"]);
                                        mc.middle_name = UtilityM.CheckNull<string>(reader["MIDDLE_NAME"]);
                                        mc.last_name = UtilityM.CheckNull<string>(reader["LAST_NAME"]);
                                        mc.cust_name = UtilityM.CheckNull<string>(reader["CUST_NAME"]);
                                        mc.guardian_name = UtilityM.CheckNull<string>(reader["GUARDIAN_NAME"]);
                                        mc.cust_dt = UtilityM.CheckNull<DateTime>(reader["CUST_DT"]);
                                        mc.old_cust_cd = UtilityM.CheckNull<string>(reader["OLD_CUST_CD"]);
                                        mc.dt_of_birth = UtilityM.CheckNull<DateTime>(reader["DT_OF_BIRTH"]);
                                        mc.age = UtilityM.CheckNull<decimal>(reader["AGE"]);
                                        mc.sex = UtilityM.CheckNull<string>(reader["SEX"]);
                                        mc.marital_status = UtilityM.CheckNull<string>(reader["MARITAL_STATUS"]);
                                        mc.catg_cd = UtilityM.CheckNull<int>(reader["CATG_CD"]);
                                        mc.community = UtilityM.CheckNull<decimal>(reader["COMMUNITY"]);
                                        mc.caste = UtilityM.CheckNull<decimal>(reader["CASTE"]);
                                        mc.permanent_address = UtilityM.CheckNull<string>(reader["PERMANENT_ADDRESS"]);
                                        mc.ward_no = UtilityM.CheckNull<decimal>(reader["WARD_NO"]);
                                        mc.state = UtilityM.CheckNull<string>(reader["STATE"]);
                                        mc.dist = UtilityM.CheckNull<string>(reader["DIST"]);
                                        mc.pin = UtilityM.CheckNull<int>(reader["PIN"]);
                                        mc.vill_cd = UtilityM.CheckNull<string>(reader["VILL_CD"]);
                                        mc.block_cd = UtilityM.CheckNull<string>(reader["BLOCK_CD"]);
                                        mc.service_area_cd = UtilityM.CheckNull<string>(reader["SERVICE_AREA_CD"]);
                                        mc.occupation = UtilityM.CheckNull<string>(reader["OCCUPATION"]);
                                        mc.phone = UtilityM.CheckNull<string>(reader["PHONE"]);
                                        mc.present_address = UtilityM.CheckNull<string>(reader["PRESENT_ADDRESS"]);
                                        mc.farmer_type = UtilityM.CheckNull<string>(reader["FARMER_TYPE"]);
                                        mc.email = UtilityM.CheckNull<string>(reader["EMAIL"]);
                                        mc.monthly_income = UtilityM.CheckNull<decimal>(reader["MONTHLY_INCOME"]);
                                        mc.date_of_death = UtilityM.CheckNull<DateTime>(reader["DATE_OF_DEATH"]);
                                        mc.sms_flag = UtilityM.CheckNull<string>(reader["SMS_FLAG"]);
                                        mc.status = UtilityM.CheckNull<string>(reader["STATUS"]);
                                        mc.pan = UtilityM.CheckNull<string>(reader["PAN"]);
                                        mc.nominee = UtilityM.CheckNull<string>(reader["NOMINEE"]);
                                        mc.nom_relation = UtilityM.CheckNull<string>(reader["NOM_RELATION"]);
                                        mc.kyc_photo_type = UtilityM.CheckNull<string>(reader["KYC_PHOTO_TYPE"]);
                                        mc.kyc_photo_no = UtilityM.CheckNull<string>(reader["KYC_PHOTO_NO"]);
                                        mc.kyc_address_type = UtilityM.CheckNull<string>(reader["KYC_ADDRESS_TYPE"]);
                                        mc.kyc_address_no = UtilityM.CheckNull<string>(reader["KYC_ADDRESS_NO"]);
                                        mc.org_status = UtilityM.CheckNull<string>(reader["ORG_STATUS"]);
                                        mc.org_reg_no = UtilityM.CheckNull<decimal>(reader["ORG_REG_NO"]);
                                        mc.created_by = UtilityM.CheckNull<string>(reader["CREATED_BY"]);
                                        mc.created_dt = UtilityM.CheckNull<DateTime>(reader["CREATED_DT"]);
                                        mc.modified_by = UtilityM.CheckNull<string>(reader["MODIFIED_BY"]);
                                        mc.modified_dt = UtilityM.CheckNull<DateTime>(reader["MODIFIED_DT"]);

                                        customers.Add(mc);
                                    }
                                }
                            }

                        }
                        catch (Exception ex)
                        {

                        }
                    }
                }
            }

            return customers;
        }




       internal List<standing_instr> GetStandingInstr()
        {
            List<standing_instr> standingInstrList = new List<standing_instr>();

            string _alter = "ALTER SESSION SET NLS_DATE_FORMAT = 'DD/MM/YYYY HH24:MI:SS'";
            string _statement;
            string _query = " SELECT A.ACC_TYPE_FROM ACC_TYPE_FROM, A.ACC_NUM_FROM  ACC_NUM_FROM, "
                            + " B.ACC_TYPE_TO   ACC_TYPE_TO,   B.ACC_NUM_TO    ACC_NUM_TO,          "
                            + " A.INSTR_STATUS  INSTR_STATUS,  A.FIRST_TRF_DT  FIRST_TRF_DT,        "
                            + " A.PERIODICITY   PERIODICITY,   A.PRN_INTT_FLAG PRN_INTT_FLAG,       "
                            + " A.AMOUNT        AMOUNT,        A.SRL_NO        SRL_NO               "
                            + " FROM SM_STANDING_INSTRUCTION A, SD_STANDING_INSTRUCTION B           "
                            + " WHERE NVL(A.INSTR_STATUS, 'O') = 'O' AND A.SRL_NO = B.SRL_NO        "
                            + " ORDER BY A.ACC_TYPE_FROM, A.ACC_NUM_FROM                            ";
   
            using (var connection = OrclDbConnection.NewConnection)
            {
                using (var transaction = connection.BeginTransaction())
                {
                    try
                    {
                        using (var command = OrclDbConnection.Command(connection, _alter))
                        {
                            command.ExecuteNonQuery();
                        }

                        _statement = string.Format(_query);

                        using (var command = OrclDbConnection.Command(connection, _statement))
                        {
                            using (var reader = command.ExecuteReader())
                            {
                                if (reader.HasRows)
                                {
                                    while (reader.Read())
                                    {
                                        var stInstr = new standing_instr();

                                        stInstr.acc_type_from = UtilityM.CheckNull<int>(reader["ACC_TYPE_FROM"]);
                                        stInstr.acc_num_from = UtilityM.CheckNull<string>(reader["ACC_NUM_FROM"]);
                                        stInstr.acc_type_to = UtilityM.CheckNull<int>(reader["ACC_TYPE_TO"]);
                                        stInstr.acc_num_to = UtilityM.CheckNull<string>(reader["ACC_NUM_TO"]);
                                        stInstr.instr_status = UtilityM.CheckNull<string>(reader["INSTR_STATUS"]);
                                        stInstr.first_trf_dt = UtilityM.CheckNull<DateTime>(reader["FIRST_TRF_DT"]);
                                        stInstr.periodicity = UtilityM.CheckNull<string>(reader["PERIODICITY"]);
                                        stInstr.prn_intt_flag = UtilityM.CheckNull<string>(reader["PRN_INTT_FLAG"]);
                                        stInstr.amount = UtilityM.CheckNull<decimal>(reader["AMOUNT"]);
                                        stInstr.srl_no = UtilityM.CheckNull<int>(reader["SRL_NO"]);

                                        standingInstrList.Add(stInstr);
                                    }
                                    transaction.Commit();
                                }
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        transaction.Rollback();
                        standingInstrList = null;
                    }
                }
            }
            return standingInstrList;
        }


        internal List<standing_instr_exe> GetStandingInstrExe(p_report_param prp)
        {
            List<standing_instr_exe> standingInstrExeList = new List<standing_instr_exe>();

            string _alter = "ALTER SESSION SET NLS_DATE_FORMAT = 'DD/MM/YYYY HH24:MI:SS'";
            string _statement;
            string _query = " SELECT TT_EXECUTED_SI.TRANS_DT,   "
                             + " TT_EXECUTED_SI.DR_ACC_TYPE,     "
                             + " TT_EXECUTED_SI.DR_ACC_NUM,      "
                             + " TT_EXECUTED_SI.CUST_CD,         "
                             + " TT_EXECUTED_SI.CR_ACC_TYPE,     "
                             + " TT_EXECUTED_SI.CR_ACC_NUM,      "
                             + " TT_EXECUTED_SI.AMOUNT           "
                             + " FROM TT_EXECUTED_SI             "
                             + " WHERE TT_EXECUTED_SI.TRANS_DT = {0} ";

            using (var connection = OrclDbConnection.NewConnection)
            {
                using (var transaction = connection.BeginTransaction())
                {
                    try
                    {
                        using (var command = OrclDbConnection.Command(connection, _alter))
                        {
                            command.ExecuteNonQuery();
                        }

                        _statement = string.Format(_query,
                                                    string.Concat("to_date('", prp.to_dt.ToString("dd/MM/yyyy"), "','dd-mm-yyyy' )"));

                        using (var command = OrclDbConnection.Command(connection, _statement))
                        {
                            using (var reader = command.ExecuteReader())
                            {
                                if (reader.HasRows)
                                {
                                    while (reader.Read())
                                    {
                                        var stInstrExe = new standing_instr_exe();

                                        stInstrExe.trans_dt = UtilityM.CheckNull<DateTime>(reader["TRANS_DT"]);
                                        stInstrExe.dr_acc_type = UtilityM.CheckNull<Int32>(reader["DR_ACC_TYPE"]);
                                        stInstrExe.dr_acc_num = UtilityM.CheckNull<string>(reader["DR_ACC_NUM"]);
                                        stInstrExe.cust_cd = UtilityM.CheckNull<Int32>(reader["CUST_CD"]);
                                        stInstrExe.cr_acc_type = UtilityM.CheckNull<Int32>(reader["CR_ACC_TYPE"]);
                                        stInstrExe.cr_acc_num = UtilityM.CheckNull<string>(reader["CR_ACC_NUM"]);
                                        stInstrExe.amount = UtilityM.CheckNull<decimal>(reader["AMOUNT"]);

                                        standingInstrExeList.Add(stInstrExe);
                                    }
                                    transaction.Commit();
                                }
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        transaction.Rollback();
                        standingInstrExeList = null;
                    }
                }
            }
            return standingInstrExeList;
        }

        internal List<tm_deposit> GetDepositDtls(mm_customer pmc)
        {
            List<tm_deposit> depo = new List<tm_deposit>();
            string _query =  " SELECT TM_DEPOSIT.ACC_TYPE_CD, TM_DEPOSIT.ACC_NUM, "
                           + " Decode(TM_DEPOSIT.ACC_TYPE_CD, 1,TM_DEPOSIT.CLR_BAL,7,TM_DEPOSIT.CLR_BAL,8,TM_DEPOSIT.CLR_BAL,6, f_get_rd_prn (TM_DEPOSIT.ACC_NUM,SYSDATE),TM_DEPOSIT.PRN_AMT) Balance, "
                           + " TM_DEPOSIT.CUST_CD,MM_CUSTOMER.CUST_NAME,TM_DEPOSIT.ACC_STATUS,  "
                           + " TM_DEPOSIT.PRN_AMT PRN_AMT, TM_DEPOSIT.INTT_AMT INTT_AMT, "
                           + " TM_DEPOSIT.INSTL_AMT INSTL_AMT "
                           + " FROM  TM_DEPOSIT,MM_CUSTOMER   "
                           + " WHERE  TM_DEPOSIT.CUST_CD = MM_CUSTOMER.CUST_CD   "
                           + " And    TM_DEPOSIT.CUST_CD = {0}  "
                           + " And    TM_DEPOSIT.BRN_CD = {1}  "
                           + " Order By TM_DEPOSIT.ACC_TYPE_CD  ";
            using (var connection = OrclDbConnection.NewConnection)
            {

                _statement = string.Format(_query,
                                            !string.IsNullOrWhiteSpace(pmc.cust_cd.ToString()) ? string.Concat("'", pmc.cust_cd, "'") : "MM_CUSTOMER.CUST_CD",
                                            !string.IsNullOrWhiteSpace(pmc.brn_cd) ? string.Concat("'", pmc.brn_cd, "'") : "MM_CUSTOMER.BRN_CD"
                                            );
                using (var command = OrclDbConnection.Command(connection, _statement))
                {
                    using (var reader = command.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                var mc = new tm_deposit();
                                mc.acc_type_cd = UtilityM.CheckNull<int>(reader["ACC_TYPE_CD"]);
                                mc.acc_num = UtilityM.CheckNull<string>(reader["ACC_NUM"]);
                                mc.clr_bal = UtilityM.CheckNull<decimal>(reader["Balance"]);
                                mc.cust_cd = UtilityM.CheckNull<decimal>(reader["CUST_CD"]);
                                mc.created_by = UtilityM.CheckNull<string>(reader["CUST_NAME"]);
                                mc.acc_status = UtilityM.CheckNull<string>(reader["ACC_STATUS"]);
                                mc.prn_amt = UtilityM.CheckNull<decimal>(reader["PRN_AMT"]);
                                mc.intt_amt = UtilityM.CheckNull<decimal>(reader["INTT_AMT"]);
                                mc.instl_amt = UtilityM.CheckNull<decimal>(reader["INSTL_AMT"]);

                                depo.Add(mc);
                            }
                        }
                    }
                }
            }
            return depo;
        }

            internal List<tm_daily_deposit> GetDailyDeposit(tm_deposit dep)
        {
            List<tm_daily_deposit> depoList = new List<tm_daily_deposit>();

            string _query = " select d.brn_cd,d.acc_num,d.trans_type,d.paid_dt,d.paid_amt,d.agent_cd,d.approval_status,  "
                            +" d.balance_amt,d.trans_cd, a.agent_name from tm_daily_deposit d, mm_agent a where a.agent_cd = d.agent_cd and d.brn_cd={0} and d.acc_num={1}";

            using (var connection = OrclDbConnection.NewConnection)
            {
                _statement = string.Format(_query,
                                          !string.IsNullOrWhiteSpace(dep.brn_cd) ? string.Concat("'", dep.brn_cd, "'") : "brn_cd",
                                          !string.IsNullOrWhiteSpace(dep.acc_num) ? string.Concat("'", dep.acc_num, "'") : "acc_num");
                using (var command = OrclDbConnection.Command(connection, _statement))
                {
                    using (var reader = command.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            {
                                while (reader.Read())
                                {
                                    var d = new tm_daily_deposit();
                                    d.brn_cd = UtilityM.CheckNull<decimal>(reader["brn_cd"]).ToString();
                                    d.trans_type = UtilityM.CheckNull<string>(reader["trans_type"]);
                                    d.acc_num = UtilityM.CheckNull<string>(reader["acc_num"]);
                                    d.approval_status = UtilityM.CheckNull<string>(reader["approval_status"]);
                                    d.agent_cd = UtilityM.CheckNull<string>(reader["agent_cd"]);
                                    d.agent_name = UtilityM.CheckNull<string>(reader["agent_name"]);
                                    d.paid_dt = UtilityM.CheckNull<DateTime>(reader["paid_dt"]);
                                    d.paid_amt = UtilityM.CheckNull<decimal>(reader["paid_amt"]);
                                    d.balance_amt = UtilityM.CheckNull<decimal>(reader["balance_amt"]);
                                    d.trans_cd = UtilityM.CheckNull<decimal>(reader["trans_cd"]);

                                    depoList.Add(d);
                                }
                            }
                        }
                    }
                }
            }

            return depoList;
        }
        

                

    }
}
