using System;
using System.Text.Json;
using System.Collections.Generic;
using System.IO;
using SBWSFinanceApi.Models;
using System.Data.Common;
using SBWSFinanceApi.Config;
using SBWSFinanceApi.Utility;
using SBWSDepositApi.Models;
using SBWSDepositApi.Deposit;
using Oracle.ManagedDataAccess.Client;
using System.Data;

namespace SBWSFinanceApi.DL
{
    public class LoanOpenDL
    {
        string _statement;
        AccountOpenDL _dac = new AccountOpenDL();

        internal p_loan_param CalculateLoanInterest(p_loan_param prp)
        {
            p_loan_param tcaRet = new p_loan_param();
            string _alter = "ALTER SESSION SET NLS_DATE_FORMAT = 'DD/MM/YYYY HH24:MI:SS'";
            string _query1 = "P_LOAN_DAY_PRODUCT";
            string _query2 = "P_CAL_LOAN_INTT";
            string _query3 = "p_recovery";
            string _query4 = "Select Nvl(curr_prn, 0) curr_prn,"
				            +" Nvl(ovd_prn, 0) ovd_prn,"
				            +" Nvl(curr_intt, 0) curr_intt,"
				            +" Nvl(ovd_intt, 0) ovd_intt"
				            +" From   tm_loan_all where loan_id={0}";

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
                        _statement = string.Format(_query1);
                        using (var command = OrclDbConnection.Command(connection, _statement))
                        {
                            command.CommandType = System.Data.CommandType.StoredProcedure;
                            var parm1 = new OracleParameter("as_loan_id", OracleDbType.Varchar2, ParameterDirection.Input);
                            parm1.Value = prp.loan_id;
                            command.Parameters.Add(parm1);
                            var parm2 = new OracleParameter("adt_to_dt", OracleDbType.Date, ParameterDirection.Input);
                            parm2.Value = prp.intt_dt;
                            command.Parameters.Add(parm2);
                            command.ExecuteNonQuery();
                        }
                        _statement = string.Format(_query2);
                        using (var command = OrclDbConnection.Command(connection, _statement))
                        {
                            command.CommandType = System.Data.CommandType.StoredProcedure;
                            var parm1 = new OracleParameter("as_loan_id", OracleDbType.Varchar2, ParameterDirection.Input);
                            parm1.Value = prp.loan_id;
                            command.Parameters.Add(parm1);
                            var parm2 = new OracleParameter("adt_dt", OracleDbType.Date, ParameterDirection.Input);
                            parm2.Value = prp.intt_dt;
                            command.Parameters.Add(parm2);
                            var parm3 = new OracleParameter("as_brn_cd", OracleDbType.Varchar2, ParameterDirection.Input);
                            parm3.Value = prp.brn_cd;
                            command.Parameters.Add(parm3);
                            var parm4 = new OracleParameter("as_user", OracleDbType.Varchar2, ParameterDirection.Input);
                            parm4.Value = prp.gs_user_id;
                            command.Parameters.Add(parm4);
                            command.ExecuteNonQuery();

                        }
                         if (prp.commit_roll_flag == 2)
                         {
                        _statement = string.Format(_query3);
                        using (var command = OrclDbConnection.Command(connection, _statement))
                        {
                            command.CommandType = System.Data.CommandType.StoredProcedure;
                            var parm1 = new OracleParameter("as_loan_id", OracleDbType.Varchar2, ParameterDirection.Input);
                            parm1.Value = prp.loan_id;
                            command.Parameters.Add(parm1);
                            var parm2 = new OracleParameter("ad_recov_amt", OracleDbType.Decimal, ParameterDirection.Input);
                            parm2.Value = prp.recov_amt;
                            command.Parameters.Add(parm2);
                            var parm3 = new OracleParameter("as_curr_intt_rate", OracleDbType.Decimal, ParameterDirection.Input);
                            parm3.Value = prp.curr_intt_rate;
                            command.Parameters.Add(parm3);
                            var parm4 = new OracleParameter("ad_curr_prn_recov", OracleDbType.Decimal, ParameterDirection.Output);
                            command.Parameters.Add(parm4);
                            var parm5 = new OracleParameter("ad_ovd_prn_recov", OracleDbType.Decimal, ParameterDirection.Output);
                            command.Parameters.Add(parm5);
                            var parm6 = new OracleParameter("ad_curr_intt_recov", OracleDbType.Decimal, ParameterDirection.Output);
                            command.Parameters.Add(parm6);
                            var parm7 = new OracleParameter("ad_ovd_intt_recov", OracleDbType.Decimal, ParameterDirection.Output);
                            command.Parameters.Add(parm7);
                            command.ExecuteNonQuery();
                            tcaRet.curr_prn_recov = UtilityM.CheckNull<Decimal>(Convert.ToDecimal(parm4.Value.ToString()));
                            tcaRet.ovd_prn_recov = UtilityM.CheckNull<Decimal>(Convert.ToDecimal(parm5.Value.ToString()));
                            tcaRet.curr_intt_recov = UtilityM.CheckNull<Decimal>(Convert.ToDecimal(parm6.Value.ToString()));
                            tcaRet.ovd_intt_recov = UtilityM.CheckNull<Decimal>(Convert.ToDecimal(parm7.Value.ToString()));
                        }
                         }
                         else
                         {
                        _statement = string.Format(_query4,
                                                    string.Concat("'", prp.loan_id, "'"));
                         using (var command = OrclDbConnection.Command(connection, _statement))
                        {
                            using (var reader = command.ExecuteReader())
                            {
                                if (reader.HasRows)
                                {
                                    while (reader.Read())
                                    {
                                        tcaRet.curr_prn_recov = UtilityM.CheckNull<decimal>(reader["curr_prn"]);
                                        tcaRet.ovd_prn_recov = UtilityM.CheckNull<decimal>(reader["ovd_prn"]);
                                        tcaRet.curr_intt_recov = UtilityM.CheckNull<decimal>(reader["curr_intt"]);
                                        tcaRet.ovd_intt_recov = UtilityM.CheckNull<decimal>(reader["ovd_intt"]);
                                    }
                                }
                            }
                        }
                        }
                        if (prp.commit_roll_flag == 1)
                            transaction.Commit();
                        else
                            transaction.Rollback();


                    }
                    catch (Exception ex)
                    {
                        transaction.Rollback();
                        tcaRet = null;
                    }
                }
            }
            return tcaRet;
        }

        internal List<p_loan_param> CalculateLoanAccWiseInterest(List<p_loan_param> prp)
        {
            List<p_loan_param> tcaRetList = new List<p_loan_param>();
            string _alter = "ALTER SESSION SET NLS_DATE_FORMAT = 'DD/MM/YYYY HH24:MI:SS'";
            string _query1 = "p_calc_loanaccwise_intt";


            using (var connection = OrclDbConnection.NewConnection)
            {
                for (int i = 0; i < prp.Count; i++)
                {
                    var tcaRet = new p_loan_param();
                    using (var transaction = connection.BeginTransaction())
                    {
                        try
                        {
                            using (var command = OrclDbConnection.Command(connection, _alter))
                            {
                                command.ExecuteNonQuery();
                            }
                            _statement = string.Format(_query1);
                            using (var command = OrclDbConnection.Command(connection, _statement))
                            {
                                command.CommandType = System.Data.CommandType.StoredProcedure;
                                var parm1 = new OracleParameter("ad_acc_cd", OracleDbType.Varchar2, ParameterDirection.Input);
                                parm1.Value = prp[i].acc_cd;
                                command.Parameters.Add(parm1);
                                var parm2 = new OracleParameter("adt_dt", OracleDbType.Date, ParameterDirection.Input);
                                parm2.Value = prp[i].intt_dt;
                                command.Parameters.Add(parm2);
                                command.ExecuteNonQuery();
                            }
                            transaction.Commit();
                            tcaRet.acc_cd = prp[i].acc_cd;
                            tcaRet.status = 0;
                            tcaRetList.Add(tcaRet);
                        }
                        catch (Exception ex)
                        {
                            transaction.Rollback();
                            tcaRet.acc_cd = prp[i].acc_cd;
                            tcaRet.status = 1;
                            tcaRetList.Add(tcaRet);
                        }
                    }
                }
            }
            return tcaRetList;
        }

        internal p_loan_param PopulateCropAmtDueDt(p_loan_param prp)
        {
            p_loan_param tcaRet = new p_loan_param();
            string _alter = "ALTER SESSION SET NLS_DATE_FORMAT = 'DD/MM/YYYY HH24:MI:SS'";
            string _query1 = "W_Pop_CropAmtDueDt";


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
                        _statement = string.Format(_query1);
                        using (var command = OrclDbConnection.Command(connection, _statement))
                        {
                            command.CommandType = System.Data.CommandType.StoredProcedure;
                            var parm1 = new OracleParameter("ls_crop_cd", OracleDbType.Varchar2, ParameterDirection.Input);
                            parm1.Value = prp.crop_cd;
                            command.Parameters.Add(parm1);
                            var parm2 = new OracleParameter("ls_party_cd", OracleDbType.Int64, ParameterDirection.Input);
                            parm2.Value = prp.cust_cd;
                            command.Parameters.Add(parm2);
                            var parm3 = new OracleParameter("ad_ldt_due_dt", OracleDbType.Date, ParameterDirection.Output);
                            command.Parameters.Add(parm3);
                            var parm4 = new OracleParameter("ad_sanc_amt", OracleDbType.Decimal, ParameterDirection.Output);
                            command.Parameters.Add(parm4);
                            var parm5 = new OracleParameter("ad_status", OracleDbType.Int64, ParameterDirection.Output);
                            command.Parameters.Add(parm5);
                            command.ExecuteNonQuery();
                            tcaRet.due_dt = (parm3.Status == OracleParameterStatus.NullFetched) ? (DateTime?)null : Convert.ToDateTime(parm3.Value.ToString());
                            tcaRet.recov_amt = (parm4.Status == OracleParameterStatus.NullFetched) ? (Int32)0 : Convert.ToInt32(parm4.Value.ToString());
                            tcaRet.status = (parm5.Status == OracleParameterStatus.NullFetched) ? (Int32)0 : Convert.ToInt32(parm5.Value.ToString());
                        }
                        if (prp.commit_roll_flag == 1)
                            transaction.Commit();
                        else
                            transaction.Rollback();


                    }
                    catch (Exception ex)
                    {
                        transaction.Rollback();
                        tcaRet = null;
                    }
                }
            }
            return tcaRet;
        }

        internal decimal F_GET_EFF_INTT_RT(p_loan_param prp)
        {
            decimal intt_rt = 0;
            string _alter = "ALTER SESSION SET NLS_DATE_FORMAT = 'DD/MM/YYYY HH24:MI:SS'";
            string _query = "SELECT W_F_GET_EFF_INTT_RT({0},{1},{2}) INTT_RT FROM DUAL";
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
                                         string.Concat("'", prp.loan_id, "'"),
                                         string.Concat("'", prp.acc_type_cd, "'"),
                                         string.IsNullOrWhiteSpace(prp.intt_dt.ToString()) ? string.Concat("null") : string.Concat("to_date('", prp.intt_dt.Value.ToString("dd/MM/yyyy"), "','dd-mm-yyyy' )")
                                        );
                        using (var command = OrclDbConnection.Command(connection, _statement))
                        {
                            using (var reader = command.ExecuteReader())
                            {
                                if (reader.HasRows)
                                {
                                    while (reader.Read())
                                    {
                                        intt_rt = UtilityM.CheckNull<decimal>(reader["INTT_RT"]);
                                    }
                                }
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        transaction.Rollback();
                        intt_rt = 0;
                    }
                }
            }
            return intt_rt;
        }

        internal string PopulateLoanAccountNumber(p_gen_param prp)
        {
            string accNum = "";

            string _query = "Select nvl(max(to_number(substr(loan_id,4))) + 1, 1) ACC_NUM "
                           + " From  TM_LOAN_ALL "
                           + " Where brn_cd = {0} ";
            using (var connection = OrclDbConnection.NewConnection)
            {
                using (var transaction = connection.BeginTransaction())
                {
                    try
                    {

                        _statement = string.Format(_query,
                                         string.Concat("'", prp.brn_cd, "'")
                                        );
                        using (var command = OrclDbConnection.Command(connection, _statement))
                        {
                            using (var reader = command.ExecuteReader())
                            {
                                if (reader.HasRows)
                                {
                                    while (reader.Read())
                                    {
                                        accNum = UtilityM.CheckNull<decimal>(reader["ACC_NUM"]).ToString();
                                    }
                                }
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        transaction.Rollback();
                        accNum = "";
                    }
                }
            }
            return prp.brn_cd + accNum;
        }
        internal LoanOpenDM GetLoanData(tm_loan_all loan)
        {
            LoanOpenDM AccOpenDMRet = new LoanOpenDM();
            using (var connection = OrclDbConnection.NewConnection)
            {
                using (var transaction = connection.BeginTransaction())
                {
                    try
                    {
                        tm_deposit td = new tm_deposit();
                        td.brn_cd = loan.brn_cd;
                        td.acc_num = loan.loan_id;
                        td.acc_type_cd = loan.acc_cd;
                        AccOpenDMRet.tmloanall = GetLoanAll(connection, loan);
                        AccOpenDMRet.tmguaranter = GetGuaranter(connection, loan.loan_id);
                        AccOpenDMRet.tmlaonsanction = GetLoanSanction(connection, loan.loan_id);
                        AccOpenDMRet.tdaccholder = GetAccholderTemp(connection, loan.brn_cd, loan.loan_id, loan.acc_cd);
                        AccOpenDMRet.tmlaonsanctiondtls = GetLoanSanctionDtls(connection, loan.loan_id);
                        AccOpenDMRet.tddeftrans = _dac.GetDepTrans(connection, td);
                        if (!String.IsNullOrWhiteSpace(AccOpenDMRet.tddeftrans.trans_cd.ToString()) && AccOpenDMRet.tddeftrans.trans_cd > 0)
                        {
                            td.trans_cd = AccOpenDMRet.tddeftrans.trans_cd;
                            td.trans_dt = AccOpenDMRet.tddeftrans.trans_dt;
                            AccOpenDMRet.tmdenominationtrans = _dac.GetDenominationDtls(connection, td);
                            AccOpenDMRet.tmtransfer = _dac.GetTransfer(connection, td);
                            AccOpenDMRet.tddeftranstrf = _dac.GetDepTransTrf(connection, td);
                        }

                        AccOpenDMRet.tdloansancsetlist = GetTdLoanSanctionDtls(connection, loan.loan_id , AccOpenDMRet.tmloanall.acc_cd);

                        return AccOpenDMRet;
                    }
                    catch (Exception ex)
                    {
                        // transaction.Rollback();
                        return null;
                    }

                }
            }
        }
        internal string InsertLoanTransactionData(LoanOpenDM acc)
        {
            string _section = null;

            using (var connection = OrclDbConnection.NewConnection)
            {
                using (var transaction = connection.BeginTransaction())
                {
                    try
                    {
                        _section = "GetTransCDMaxId";
                        int maxTransCD = _dac.GetTransCDMaxId(connection, acc.tddeftrans);
                        _section = "InsertDenominationDtls";
                        if (acc.tmdenominationtrans.Count > 0)
                            _dac.InsertDenominationDtls(connection, acc.tmdenominationtrans, maxTransCD);
                        _section = "InsertTransfer";
                        if (acc.tmtransfer.Count > 0)
                            _dac.InsertTransfer(connection, acc.tmtransfer, maxTransCD);
                        _section = "InsertDepTransTrf";
                        if (acc.tddeftranstrf.Count > 0)
                            _dac.InsertDepTransTrf(connection, acc.tddeftranstrf, maxTransCD);
                        _section = "InsertDepTrans";
                        if (!String.IsNullOrWhiteSpace(maxTransCD.ToString()) && maxTransCD != 0)
                            _dac.InsertDepTrans(connection, acc.tddeftrans, maxTransCD);
                        transaction.Commit();
                        return maxTransCD.ToString();
                    }
                    catch (Exception ex)
                    {

                        transaction.Rollback();
                        return _section + " : " + ex.Message;
                    }

                }
            }
        }

        internal String InsertLoanAccountOpeningData(LoanOpenDM acc)
        {
            string _section = null;

            using (var connection = OrclDbConnection.NewConnection)
            {
                using (var transaction = connection.BeginTransaction())
                {
                    try
                    {
                        _section = "UpdateLoanAll";
                        if (!String.IsNullOrWhiteSpace(acc.tmloanall.loan_id))
                            UpdateLoanAll(connection, acc.tmloanall);
                        _section = "UpdateGuaranter";
                        if (!String.IsNullOrWhiteSpace(acc.tmguaranter.loan_id))
                            UpdateGuaranter(connection, acc.tmguaranter);
                        _section = "UpdateLoanSanction";
                        if (acc.tmlaonsanction.Count > 0)
                            UpdateLoanSanction(connection, acc.tmlaonsanction);
                        _section = "UpdateAccholder";
                        if (acc.tdaccholder.Count > 0)
                            UpdateAccholder(connection, acc.tdaccholder);
                        _section = "UpdateLoanSanctionDtls";
                        if (acc.tmlaonsanctiondtls.Count > 0)
                            UpdateLoanSanctionDtls(connection, acc.tmlaonsanctiondtls);

                        if(acc.tdloansancsetlist.Count > 0)
                        {
                            var td_loan_sanc_list =
                             SerializeEntireLoanSancList(acc.tdloansancsetlist);
                             InsertLoanSecurityDtls(connection, td_loan_sanc_list);
                        }

                        transaction.Commit();
                        return null;
                    }
                    catch (Exception ex)
                    {

                        transaction.Rollback();
                        return _section + " : " + ex.Message;
                    }

                }
            }
        }

        internal int UpdateLoanAccountOpeningData(LoanOpenDM acc)
        {
            using (var connection = OrclDbConnection.NewConnection)
            {
                using (var transaction = connection.BeginTransaction())
                {
                    try
                    {
                        if (!String.IsNullOrWhiteSpace(acc.tmloanall.loan_id))
                            UpdateLoanAll(connection, acc.tmloanall);
                        if (!String.IsNullOrWhiteSpace(acc.tmguaranter.loan_id))
                            UpdateGuaranter(connection, acc.tmguaranter);
                        if (acc.tmlaonsanction.Count > 0)
                            UpdateLoanSanction(connection, acc.tmlaonsanction);
                        if (acc.tdaccholder.Count > 0)
                            UpdateAccholder(connection, acc.tdaccholder);
                        if (acc.tmlaonsanctiondtls.Count > 0)
                            UpdateLoanSanctionDtls(connection, acc.tmlaonsanctiondtls);
                        if (acc.tmdenominationtrans.Count > 0)
                            _dac.UpdateDenominationDtls(connection, acc.tmdenominationtrans);
                        if (acc.tmtransfer.Count > 0)
                            _dac.UpdateTransfer(connection, acc.tmtransfer);
                        if (acc.tddeftranstrf.Count > 0)
                            _dac.UpdateDepTransTrf(connection, acc.tddeftranstrf);
                        if (!String.IsNullOrWhiteSpace(acc.tddeftrans.trans_cd.ToString()))
                            _dac.UpdateDepTrans(connection, acc.tddeftrans);
                        transaction.Commit();
                        return 0;
                    }
                    catch (Exception ex)
                    {
                        transaction.Rollback();
                        return -1;
                    }

                }
            }
        }

        internal tm_loan_all GetLoanAll(DbConnection connection, tm_loan_all loan)
        {
            tm_loan_all loanRet = new tm_loan_all();
            string _query = " SELECT MC.BRN_CD,TL.PARTY_CD,TL.ACC_CD,TL.LOAN_ID,TL.LOAN_ACC_NO,TL.PRN_LIMIT,TL.DISB_AMT,TL.DISB_DT, "
                     + " TL.CURR_PRN,TL.OVD_PRN,TL.CURR_INTT,TL.OVD_INTT,TL.PRE_EMI_INTT,TL.OTHER_CHARGES,TL.CURR_INTT_RATE,TL.OVD_INTT_RATE,TL.DISB_STATUS,TL.PIRIODICITY,TL.TENURE_MONTH,   "
                     + " TL.INSTL_START_DT,TL.CREATED_BY,TL.CREATED_DT,TL.MODIFIED_BY,TL.MODIFIED_DT,TL.LAST_INTT_CALC_DT,TL.OVD_TRF_DT,TL.APPROVAL_STATUS,TL.CC_FLAG,TL.CHEQUE_FACILITY,TL.INTT_CALC_TYPE, "
                     + " TL.EMI_FORMULA_NO,TL.REP_SCH_FLAG,TL.LOAN_CLOSE_DT,TL.LOAN_STATUS,TL.INSTL_AMT,TL.INSTL_NO,ACTIVITY_CD,ACTIVITY_DTLS,SECTOR_CD,FUND_TYPE,COMP_UNIT_NO "
                     + " ,MC.CUST_NAME , (Select sum(tot_share_holding) From   tm_party_share Where  party_cd = TL.PARTY_CD ) tot_share_holding   "
                     + " FROM TM_LOAN_ALL TL,MM_CUSTOMER MC  "
                     + " WHERE TL.BRN_CD={0} AND TL.LOAN_ID={1} AND TL.ACC_CD ={2}  "
                     + " AND  TL.PARTY_CD=MC.CUST_CD(+) AND TL.BRN_CD=MC.BRN_CD(+)   ";

            /*string _query = " SELECT BRN_CD,PARTY_CD,ACC_CD,LOAN_ID,LOAN_ACC_NO,PRN_LIMIT,DISB_AMT,DISB_DT,"
                           + "CURR_PRN,OVD_PRN,CURR_INTT,OVD_INTT,PRE_EMI_INTT,OTHER_CHARGES,CURR_INTT_RATE,OVD_INTT_RATE,DISB_STATUS,PIRIODICITY,TENURE_MONTH,"
                           + "INSTL_START_DT,CREATED_BY,CREATED_DT,MODIFIED_BY,MODIFIED_DT,LAST_INTT_CALC_DT,OVD_TRF_DT,APPROVAL_STATUS,CC_FLAG,CHEQUE_FACILITY,INTT_CALC_TYPE,"
                           + "EMI_FORMULA_NO,REP_SCH_FLAG,LOAN_CLOSE_DT,LOAN_STATUS,INSTL_AMT,INSTL_NO,ACTIVITY_CD,ACTIVITY_DTLS,SECTOR_CD,FUND_TYPE,COMP_UNIT_NO"	 
                           + " FROM TM_LOAN_ALL WHERE BRN_CD={0} AND LOAN_ID={1} ";*/

            _statement = string.Format(_query,
                                          !string.IsNullOrWhiteSpace(loan.brn_cd) ? string.Concat("'", loan.brn_cd, "'") : "brn_cd",
                                          !string.IsNullOrWhiteSpace(loan.loan_id) ? string.Concat("'", loan.loan_id, "'") : "LOAN_ID",
                                          loan.acc_cd>0 ? loan.acc_cd.ToString() : "ACC_CD"
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
                                var d = new tm_loan_all();
                                d.brn_cd = UtilityM.CheckNull<string>(reader["BRN_CD"]);
                                d.party_cd = UtilityM.CheckNull<decimal>(reader["PARTY_CD"]);
                                d.acc_cd = UtilityM.CheckNull<Int32>(reader["ACC_CD"]);
                                d.loan_id = UtilityM.CheckNull<string>(reader["LOAN_ID"]);
                                d.loan_acc_no = UtilityM.CheckNull<string>(reader["LOAN_ACC_NO"]);
                                d.prn_limit = UtilityM.CheckNull<decimal>(reader["PRN_LIMIT"]);
                                d.disb_amt = UtilityM.CheckNull<decimal>(reader["DISB_AMT"]);
                                d.disb_dt = UtilityM.CheckNull<DateTime>(reader["DISB_DT"]);
                                d.curr_prn = UtilityM.CheckNull<decimal>(reader["CURR_PRN"]);
                                d.ovd_prn = UtilityM.CheckNull<decimal>(reader["OVD_PRN"]);
                                d.curr_intt = UtilityM.CheckNull<decimal>(reader["CURR_INTT"]);
                                d.ovd_intt = UtilityM.CheckNull<decimal>(reader["OVD_INTT"]);
                                d.pre_emi_intt = UtilityM.CheckNull<decimal>(reader["PRE_EMI_INTT"]);
                                d.other_charges = UtilityM.CheckNull<decimal>(reader["OTHER_CHARGES"]);
                                d.curr_intt_rate = UtilityM.CheckNull<double>(reader["CURR_INTT_RATE"]);
                                d.ovd_intt_rate = UtilityM.CheckNull<double>(reader["OVD_INTT_RATE"]);
                                d.disb_status = UtilityM.CheckNull<string>(reader["DISB_STATUS"]);
                                d.piriodicity = UtilityM.CheckNull<string>(reader["PIRIODICITY"]);
                                d.tenure_month = UtilityM.CheckNull<int>(reader["TENURE_MONTH"]);
                                d.instl_start_dt = UtilityM.CheckNull<DateTime>(reader["INSTL_START_DT"]);
                                d.created_by = UtilityM.CheckNull<string>(reader["CREATED_BY"]);
                                d.created_dt = UtilityM.CheckNull<DateTime>(reader["CREATED_DT"]);
                                d.modified_by = UtilityM.CheckNull<string>(reader["MODIFIED_BY"]);
                                d.modified_dt = UtilityM.CheckNull<DateTime>(reader["MODIFIED_DT"]);
                                d.last_intt_calc_dt = UtilityM.CheckNull<DateTime>(reader["LAST_INTT_CALC_DT"]);
                                d.ovd_trf_dt = UtilityM.CheckNull<DateTime>(reader["OVD_TRF_DT"]);
                                d.approval_status = UtilityM.CheckNull<string>(reader["APPROVAL_STATUS"]);
                                d.cc_flag = UtilityM.CheckNull<string>(reader["CC_FLAG"]);
                                d.cheque_facility = UtilityM.CheckNull<string>(reader["CHEQUE_FACILITY"]);
                                d.intt_calc_type = UtilityM.CheckNull<string>(reader["INTT_CALC_TYPE"]);
                                d.emi_formula_no = UtilityM.CheckNull<int>(reader["EMI_FORMULA_NO"]);
                                d.rep_sch_flag = UtilityM.CheckNull<string>(reader["REP_SCH_FLAG"]);
                                d.loan_close_dt = UtilityM.CheckNull<DateTime>(reader["LOAN_CLOSE_DT"]);
                                d.loan_status = UtilityM.CheckNull<string>(reader["LOAN_STATUS"]);
                                d.instl_amt = UtilityM.CheckNull<decimal>(reader["INSTL_AMT"]);
                                d.instl_no = UtilityM.CheckNull<int>(reader["INSTL_NO"]);
                                d.activity_cd = UtilityM.CheckNull<string>(reader["ACTIVITY_CD"]);
                                d.activity_dtls = UtilityM.CheckNull<string>(reader["ACTIVITY_DTLS"]);
                                d.sector_cd = UtilityM.CheckNull<string>(reader["SECTOR_CD"]);
                                d.fund_type = UtilityM.CheckNull<string>(reader["FUND_TYPE"]);
                                d.comp_unit_no = UtilityM.CheckNull<decimal>(reader["COMP_UNIT_NO"]);
                                d.cust_name = UtilityM.CheckNull<string>(reader["CUST_NAME"]);
                                d.tot_share_holding = UtilityM.CheckNull<decimal>(reader["tot_share_holding"]);
                                loanRet = d;
                            }
                        }
                    }
                }
            }
            return loanRet;
        }
        internal tm_guaranter GetGuaranter(DbConnection connection, string loan_id)
        {
            tm_guaranter loanRet = new tm_guaranter();
            string _query = " SELECT LOAN_ID,ACC_CD,GUA_TYPE,GUA_ID,GUA_NAME,GUA_ADD,OFFICE_NAME, "
                            + " SHARE_ACC_NUM,SHARE_TYPE,OPENING_DT,SHARE_BAL,DEPART,DESIG,  "
                            + " SALARY,SEC_58,MOBILE,SRL_NO "
                            + " FROM TM_GUARANTER WHERE    LOAN_ID = {0}";

            _statement = string.Format(_query,
                                          !string.IsNullOrWhiteSpace(loan_id) ? string.Concat("'", loan_id, "'") : "LOAN_ID"
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
                                var d = new tm_guaranter();
                                d.loan_id = UtilityM.CheckNull<string>(reader["LOAN_ID"]);
                                d.acc_cd = UtilityM.CheckNull<Decimal>(reader["ACC_CD"]);
                                d.gua_type = UtilityM.CheckNull<string>(reader["GUA_TYPE"]);
                                d.gua_id = UtilityM.CheckNull<string>(reader["GUA_ID"]);
                                d.gua_name = UtilityM.CheckNull<string>(reader["GUA_NAME"]);
                                d.gua_add = UtilityM.CheckNull<string>(reader["GUA_ADD"]);
                                d.office_name = UtilityM.CheckNull<string>(reader["OFFICE_NAME"]);
                                d.share_acc_num = UtilityM.CheckNull<string>(reader["SHARE_ACC_NUM"]);
                                d.share_type = UtilityM.CheckNull<Int64>(reader["SHARE_TYPE"]);
                                d.opening_dt = UtilityM.CheckNull<DateTime>(reader["OPENING_DT"]);
                                d.share_bal = UtilityM.CheckNull<decimal>(reader["SHARE_BAL"]);
                                d.depart = UtilityM.CheckNull<string>(reader["DEPART"]);
                                d.desig = UtilityM.CheckNull<string>(reader["DESIG"]);
                                d.salary = UtilityM.CheckNull<decimal>(reader["SALARY"]);
                                d.sec_58 = UtilityM.CheckNull<string>(reader["SEC_58"]);
                                d.mobile = UtilityM.CheckNull<string>(reader["MOBILE"]);
                                d.srl_no = UtilityM.CheckNull<Int64>(reader["SRL_NO"]);
                                loanRet = d;
                            }
                        }
                    }
                }
            }
            return loanRet;
        }
        internal List<tm_loan_sanction> GetLoanSanction(DbConnection connection, string loan_id)
        {
            List<tm_loan_sanction> loanRet = new List<tm_loan_sanction>();
            string _query = " SELECT     LOAN_ID , SANC_NO ,   SANC_DT , CREATED_BY , "
                            + " CREATED_DT ,  MODIFIED_BY ,  MODIFIED_DT , "
                            + " APPROVAL_STATUS ,   APPROVED_BY , APPROVED_DT , "
                            + " MEMO_NO    FROM  TM_LOAN_SANCTION   "
                            + " WHERE   LOAN_ID  = {0} ";

            _statement = string.Format(_query,
                                          !string.IsNullOrWhiteSpace(loan_id) ? string.Concat("'", loan_id, "'") : "LOAN_ID"
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
                                var d = new tm_loan_sanction();
                                d.loan_id = UtilityM.CheckNull<string>(reader["LOAN_ID"]);
                                d.sanc_no = UtilityM.CheckNull<decimal>(reader["SANC_NO"]);
                                d.sanc_dt = UtilityM.CheckNull<DateTime>(reader["SANC_DT"]);
                                d.created_by = UtilityM.CheckNull<string>(reader["CREATED_BY"]);
                                d.created_dt = UtilityM.CheckNull<DateTime>(reader["CREATED_DT"]);
                                d.modified_by = UtilityM.CheckNull<string>(reader["MODIFIED_BY"]);
                                d.modified_dt = UtilityM.CheckNull<DateTime>(reader["MODIFIED_DT"]);
                                d.approval_status = UtilityM.CheckNull<string>(reader["APPROVAL_STATUS"]);
                                d.approved_by = UtilityM.CheckNull<string>(reader["APPROVED_BY"]);
                                d.approved_dt = UtilityM.CheckNull<DateTime>(reader["APPROVED_DT"]);
                                d.memo_no = UtilityM.CheckNull<string>(reader["MEMO_NO"]);
                                loanRet.Add(d);
                            }
                        }
                    }
                }
            }
            return loanRet;
        }
        internal List<td_accholder> GetAccholderTemp(DbConnection connection, string brn_cd, string acc_num, Int32 acc_type_cd)
        {
            List<td_accholder> accList = new List<td_accholder>();

            dynamic _query = " SELECT TA.BRN_CD,TA.ACC_TYPE_CD,TA.ACC_NUM,TA.ACC_HOLDER,"
                  + " TA.RELATION,TA.CUST_CD,MC.CUST_NAME                      "
                  + " FROM TD_ACCHOLDER TA,MM_CUSTOMER MC                      "
                  + " WHERE TA.BRN_CD = {0}                                  "
                  + " AND   ACC_NUM = {1}                              "
                  + " AND   TA.CUST_CD=MC.CUST_CD (+)                          "
                  + " AND   TA.BRN_CD=MC.BRN_CD (+)                            "
                  + " AND   ACC_TYPE_CD = {2}                                  ";

            var v1 = !string.IsNullOrWhiteSpace(brn_cd) ? string.Concat("'", brn_cd, "'") : "brn_cd";
            var v2 = !string.IsNullOrWhiteSpace(acc_num) ? string.Concat("'", acc_num, "'") : "acc_num";
            dynamic v3 = (acc_type_cd > 0) ? acc_type_cd.ToString() : "ACC_TYPE_CD";
            _statement = string.Format(_query, v1, v2, v3);


            using (var command = OrclDbConnection.Command(connection, _statement))
            {
                using (var reader = command.ExecuteReader())
                {
                    if (reader.HasRows)
                    {
                        {
                            while (reader.Read())
                            {
                                var a = new td_accholder();
                                a.brn_cd = UtilityM.CheckNull<string>(reader["BRN_CD"]);
                                a.acc_type_cd = UtilityM.CheckNull<int>(reader["ACC_TYPE_CD"]);
                                a.acc_num = UtilityM.CheckNull<string>(reader["ACC_NUM"]);
                                a.acc_holder = UtilityM.CheckNull<string>(reader["ACC_HOLDER"]);
                                a.relation = UtilityM.CheckNull<string>(reader["RELATION"]);
                                a.cust_cd = UtilityM.CheckNull<decimal>(reader["CUST_CD"]);
                                a.cust_name = UtilityM.CheckNull<string>(reader["CUST_NAME"]);
                                accList.Add(a);
                            }
                        }
                    }
                }
            }
            return accList;
        }
        internal List<tm_loan_sanction_dtls> GetLoanSanctionDtls(DbConnection connection, string loan_id)
        {
            List<tm_loan_sanction_dtls> loanRet = new List<tm_loan_sanction_dtls>();
            string _query = " SELECT  LS.SECTOR_CD ,  LS.ACTIVITY_CD ,   LS.CROP_CD ,  SANC_AMT ,      "
                     + " DUE_DT ,  LOAN_ID , SANC_NO , SANC_STATUS ,                              "
                     + " SRL_NO ,  APPROVAL_STATUS ,MC.CROP_DESC,MA.ACTIVITY_DESC,MS.SECTOR_DESC  "
                     + " FROM  TM_LOAN_SANCTION_DTLS   LS,                                        "
                     + " MM_CROP MC,MM_ACTIVITY  MA,                                              "
                     + " MM_SECTOR MS                                                             "
                     + " WHERE LS.SECTOR_CD=MS.SECTOR_CD(+) AND LS.CROP_CD=MC.CROP_CD(+)          "
                     + " AND LS.ACTIVITY_CD=MA.ACTIVITY_CD(+)                                     "
                     + " AND MA.SECTOR_CD=MS.SECTOR_CD AND    LS.LOAN_ID  = {0}    ";

            _statement = string.Format(_query,
                                          !string.IsNullOrWhiteSpace(loan_id) ? string.Concat("'", loan_id, "'") : "LOAN_ID"
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
                                var d = new tm_loan_sanction_dtls();
                                d.loan_id = UtilityM.CheckNull<string>(reader["LOAN_ID"]);
                                d.sanc_no = UtilityM.CheckNull<decimal>(reader["SANC_NO"]);
                                d.sector_cd = UtilityM.CheckNull<string>(reader["SECTOR_CD"]);
                                d.activity_cd = UtilityM.CheckNull<string>(reader["ACTIVITY_CD"]);
                                d.crop_cd = UtilityM.CheckNull<string>(reader["CROP_CD"]);
                                d.sanc_amt = UtilityM.CheckNull<decimal>(reader["SANC_AMT"]);
                                d.due_dt = UtilityM.CheckNull<DateTime>(reader["DUE_DT"]);
                                d.sanc_status = UtilityM.CheckNull<string>(reader["SANC_STATUS"]);
                                d.srl_no = UtilityM.CheckNull<decimal>(reader["SRL_NO"]);
                                d.approval_status = UtilityM.CheckNull<string>(reader["APPROVAL_STATUS"]);
                                d.crop_desc = UtilityM.CheckNull<string>(reader["CROP_DESC"]);
                                d.activity_desc = UtilityM.CheckNull<string>(reader["ACTIVITY_DESC"]);
                                d.sector_desc = UtilityM.CheckNull<string>(reader["SECTOR_DESC"]);
                                loanRet.Add(d);
                            }
                        }
                    }
                }
            }
            return loanRet;
        }

      internal List<td_loan_sanc_set> GetTdLoanSanctionDtls(DbConnection connection, string loan_id , int acc_cd)
        {
            List<td_loan_sanc_set> tdLoanSancSetList = new List<td_loan_sanc_set>();
            List<td_loan_sanc> tdLoanSancList = new List<td_loan_sanc>();
            td_loan_sanc_set loanSancSet = new td_loan_sanc_set();

            int prevDataSet = 1;

            string _query = " SELECT T.LOAN_ID , T.SANC_NO , T.PARAM_CD , T.PARAM_VALUE , UPPER(T.PARAM_TYPE) PARAM_TYPE , T.DATASET_NO , T.FIELD_NAME , S.PARAM_DESC"
                           + " FROM TD_LOAN_SANC  T, SM_LOAN_SANCTION S "
                           + " WHERE T.LOAN_ID = {0}            "
                           +" AND S.PARAM_CD = T.PARAM_CD     "
                           +" AND S.FIELD_NAME = T.FIELD_NAME "
                           +" AND S.ACC_CD = {1}              "
                           +" ORDER BY DATASET_NO , PARAM_CD ";

            _statement = string.Format(_query,
                                          !string.IsNullOrWhiteSpace(loan_id) ? string.Concat("'", loan_id, "'") : "1" ,
                                           string.Concat("'", acc_cd , "'") 
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
                                var d = new td_loan_sanc();
                                d.loan_id = UtilityM.CheckNull<decimal>(reader["LOAN_ID"]).ToString();
                                d.sanc_no = UtilityM.CheckNull<decimal>(reader["SANC_NO"]);                                
                                d.param_cd = UtilityM.CheckNull<string>(reader["PARAM_CD"]);
                                d.param_value = UtilityM.CheckNull<string>(reader["PARAM_VALUE"]);
                                d.param_type = UtilityM.CheckNull<string>(reader["PARAM_TYPE"]);
                                // if (d.param_type == "DATE")
                                // {
                                //     d.param_value_dt =  UtilityM.CheckNull<DateTime>(reader["PARAM_VALUE_DT"]);
                                // }
                                // else
                                // d.param_value_dt =null;
                                
                                d.dataset_no = UtilityM.CheckNull<Int32>(reader["DATASET_NO"]);
                                d.field_name = UtilityM.CheckNull<string>(reader["FIELD_NAME"]);
                                d.param_desc = UtilityM.CheckNull<string>(reader["PARAM_DESC"]);                                

                                if (prevDataSet != d.dataset_no)
                                {
                                    prevDataSet = d.dataset_no;
                                    loanSancSet.tdloansancset = tdLoanSancList;
                                    tdLoanSancSetList.Add( loanSancSet);

                                    loanSancSet = new td_loan_sanc_set();
                                    tdLoanSancList = new List<td_loan_sanc>();
                                }

                                tdLoanSancList.Add(d);
                            }

                            if(tdLoanSancList.Count > 0)
                            {
                                // td_loan_sanc_set loanSancSet = new td_loan_sanc_set();
                                // loanSancSet.tdloansancset = tdLoanSancList;
                                // tdLoanSancSetList.Add( loanSancSet);

                                loanSancSet.tdloansancset = tdLoanSancList;
                                tdLoanSancSetList.Add( loanSancSet);
                            }

                        }
                    }
                }
            }

            return tdLoanSancSetList;
        }





        internal bool InsertLoanAll(DbConnection connection, tm_loan_all loan)
        {
            string _query = "INSERT INTO TM_LOAN_ALL (BRN_CD, PARTY_CD, ACC_CD, LOAN_ID, LOAN_ACC_NO, PRN_LIMIT, DISB_AMT, DISB_DT,   "
                            + "CURR_PRN, OVD_PRN, CURR_INTT, OVD_INTT, PRE_EMI_INTT, OTHER_CHARGES, CURR_INTT_RATE, OVD_INTT_RATE,    "
                            + "DISB_STATUS, PIRIODICITY, TENURE_MONTH, INSTL_START_DT, CREATED_BY, CREATED_DT, MODIFIED_BY, MODIFIED_DT,"
                            + "LAST_INTT_CALC_DT, OVD_TRF_DT, APPROVAL_STATUS, CC_FLAG, CHEQUE_FACILITY, INTT_CALC_TYPE, EMI_FORMULA_NO,  "
                            + "REP_SCH_FLAG, LOAN_CLOSE_DT, LOAN_STATUS, INSTL_AMT, INSTL_NO, ACTIVITY_CD, ACTIVITY_DTLS, SECTOR_CD,  "
                            + "FUND_TYPE, COMP_UNIT_NO)    "
                            + " VALUES ({0},{1},{2},{3},{4},{5},{6},{7},{8},{9},{10},{11},{12},{13}, {14},"
                            + " {15},{16}, {17}, {18},{19},{20},SYSDATE,{21},SYSDATE, "
                            + " {22},{23},{24},{25},{26},{27},{28},{29}, "
                            + " {30},{31},{32},{33},{34}, {35},{36},{37},"
                            + " {38} )   ";

            _statement = string.Format(_query,
               string.Concat("'", loan.brn_cd, "'"),
                string.Concat("'", loan.party_cd, "'"),
                string.Concat("'", loan.acc_cd, "'"),
                string.Concat("'", loan.loan_id, "'"),
                string.Concat("'", loan.loan_acc_no, "'"),
                string.Concat("'", loan.prn_limit, "'"),
                string.Concat("'", loan.disb_amt, "'"),
                string.IsNullOrWhiteSpace(loan.disb_dt.ToString()) ? string.Concat("null") : string.Concat("to_date('", loan.disb_dt.Value.ToString("dd/MM/yyyy"), "','dd-mm-yyyy' )"),
                string.Concat("'", loan.curr_prn, "'"),
                string.Concat("'", loan.ovd_prn, "'"),
                string.Concat("'", loan.curr_intt, "'"),
                string.Concat("'", loan.ovd_intt, "'"),
                string.Concat("'", loan.pre_emi_intt, "'"),
                string.Concat("'", loan.other_charges, "'"),
                string.Concat("'", loan.curr_intt_rate, "'"),
                string.Concat("'", loan.ovd_intt_rate, "'"),
                string.Concat("'", loan.disb_status, "'"),
                string.Concat("'", loan.piriodicity, "'"),
                string.Concat("'", loan.tenure_month, "'"),
                string.IsNullOrWhiteSpace(loan.instl_start_dt.ToString()) ? string.Concat("null") : string.Concat("to_date('", loan.instl_start_dt.Value.ToString("dd/MM/yyyy"), "','dd-mm-yyyy' )"),
                string.Concat("'", loan.created_by, "'"),
                string.Concat("'", loan.modified_by, "'"),
                string.IsNullOrWhiteSpace(loan.last_intt_calc_dt.ToString()) ? string.Concat("null") : string.Concat("to_date('", loan.last_intt_calc_dt.Value.ToString("dd/MM/yyyy"), "','dd-mm-yyyy' )"),
                string.IsNullOrWhiteSpace(loan.ovd_trf_dt.ToString()) ? string.Concat("null") : string.Concat("to_date('", loan.ovd_trf_dt.Value.ToString("dd/MM/yyyy"), "','dd-mm-yyyy' )"),
                string.Concat("'", loan.approval_status, "'"),
                string.Concat("'", loan.cc_flag, "'"),
                string.Concat("'", loan.cheque_facility, "'"),
                string.Concat("'", loan.intt_calc_type, "'"),
                string.Concat("'", loan.emi_formula_no, "'"),
                string.Concat("'", loan.rep_sch_flag, "'"),
                string.IsNullOrWhiteSpace(loan.loan_close_dt.ToString()) ? string.Concat("null") : string.Concat("to_date('", loan.loan_close_dt.Value.ToString("dd/MM/yyyy"), "','dd-mm-yyyy' )"),
                string.Concat("'", loan.loan_status, "'"),
                string.Concat("'", loan.instl_amt, "'"),
                string.Concat("'", loan.instl_no, "'"),
                string.Concat("'", loan.activity_cd, "'"),
                string.Concat("'", loan.activity_dtls, "'"),
                string.Concat("'", loan.sector_cd, "'"),
                string.Concat("'", loan.fund_type, "'"),
                string.Concat("'", loan.comp_unit_no, "'")
                );
            using (var command = OrclDbConnection.Command(connection, _statement))
            {
                command.ExecuteNonQuery();
            }
            return true;
        }
        internal bool Insertguaranter(DbConnection connection, tm_guaranter loan)
        {
            string _query = "INSERT INTO TM_GUARANTER (LOAN_ID, ACC_CD, GUA_TYPE, GUA_ID, GUA_NAME, GUA_ADD, OFFICE_NAME, "
                           + " SHARE_ACC_NUM, SHARE_TYPE, OPENING_DT, SHARE_BAL, DEPART, DESIG, SALARY, SEC_58, MOBILE, SRL_NO) "
                           + " VALUES ({0},{1},{2},{3},{4},{5},{6},{7},{8},{9},{10},{11},{12},{13}, {14},"
                           + " {15},{16} ) ";

            _statement = string.Format(_query,
                String.Concat("'", loan.loan_id, "'"),
                String.Concat("'", loan.acc_cd, "'"),
                String.Concat("'", loan.gua_type, "'"),
                String.Concat("'", loan.gua_id, "'"),
                String.Concat("'", loan.gua_name, "'"),
                String.Concat("'", loan.gua_add, "'"),
                String.Concat("'", loan.office_name, "'"),
                String.Concat("'", loan.share_acc_num, "'"),
                String.Concat("'", loan.share_type, "'"),
                String.Concat("'", loan.opening_dt, "'"),
                String.Concat("'", loan.share_bal, "'"),
                String.Concat("'", loan.depart, "'"),
                String.Concat("'", loan.desig, "'"),
                String.Concat("'", loan.salary, "'"),
                String.Concat("'", loan.sec_58, "'"),
                String.Concat("'", loan.mobile, "'"),
                String.Concat("'", loan.srl_no, "'")
                );
            using (var command = OrclDbConnection.Command(connection, _statement))
            {
                command.ExecuteNonQuery();
            }
            return true;
        }
        internal bool InsertAccholder(DbConnection connection, td_accholder acc)
        {
            string _query = "INSERT INTO TD_ACCHOLDER ( brn_cd, acc_type_cd, acc_num, acc_holder, relation, cust_cd ) "
                         + " VALUES( {0},{1},{2},{3}, {4}, {5} ) ";

            _statement = string.Format(_query,
                                                   string.Concat("'", acc.brn_cd, "'"),
                                                   acc.acc_type_cd,
                                                   string.Concat("'", acc.acc_num, "'"),
                                                   string.Concat("'", acc.acc_holder, "'"),
                                                   string.Concat("'", acc.relation, "'"),
                                                   acc.cust_cd
                                                    );

            using (var command = OrclDbConnection.Command(connection, _statement))
            {
                command.ExecuteNonQuery();
            }
            return true;
        }
        internal bool InsertLoanSanction(DbConnection connection, tm_loan_sanction acc)
        {

            string _query = "INSERT INTO TM_LOAN_SANCTION (LOAN_ID, SANC_NO, SANC_DT, CREATED_BY, CREATED_DT, MODIFIED_BY, MODIFIED_DT, APPROVAL_STATUS, APPROVED_BY, APPROVED_DT, MEMO_NO) "
                            + "VALUES ({0}, {1}, {2}, {3},SYSDATE, {4}, SYSDATE,{5}, {6}, {7}, {8})";

            _statement = string.Format(_query,
                         string.Concat("'", acc.loan_id, "'"),
                         string.Concat("'", acc.sanc_no, "'"),
                         string.IsNullOrWhiteSpace(acc.sanc_dt.ToString()) ? string.Concat("null") : string.Concat("to_date('", acc.sanc_dt.Value.ToString("dd/MM/yyyy"), "','dd-mm-yyyy' )"),
                         string.Concat("'", acc.created_by, "'"),
                         string.Concat("'", acc.modified_by, "'"),
                         string.Concat("'", acc.approval_status, "'"),
                         string.Concat("'", acc.approved_by, "'"),
                         string.IsNullOrWhiteSpace(acc.approved_dt.ToString()) ? string.Concat("null") : string.Concat("to_date('", acc.approved_dt.Value.ToString("dd/MM/yyyy"), "','dd-mm-yyyy' )"),
                         string.Concat("'", acc.memo_no, "'"));

            using (var command = OrclDbConnection.Command(connection, _statement))
            {
                command.ExecuteNonQuery();
            }
            return true;
        }
        internal bool InsertLoanSanctionDtls(DbConnection connection, tm_loan_sanction_dtls acc)
        {

            string _query = "INSERT INTO TM_LOAN_SANCTION_DTLS (LOAN_ID, SANC_NO, SECTOR_CD, ACTIVITY_CD, CROP_CD, SANC_AMT, DUE_DT, SANC_STATUS, SRL_NO, APPROVAL_STATUS)"
                            + " VALUES ({0}, {1}, {2}, {3}, {4}, {5}, {6}, {7}, {8}, {9})";


            _statement = string.Format(_query,
      string.Concat("'", acc.loan_id, "'"),
      string.Concat("'", acc.sanc_no, "'"),
      string.Concat("'", acc.sector_cd, "'"),
      string.Concat("'", acc.activity_cd, "'"),
      string.Concat("'", acc.crop_cd, "'"),
      string.Concat("'", acc.sanc_amt, "'"),
      string.IsNullOrWhiteSpace(acc.due_dt.ToString()) ? string.Concat("null") : string.Concat("to_date('", acc.due_dt.Value.ToString("dd/MM/yyyy"), "','dd-mm-yyyy' )"),
      string.Concat("'", acc.sanc_status, "'"),
      string.Concat("'", acc.srl_no, "'"),
      string.Concat("'", acc.approval_status, "'"));

            using (var command = OrclDbConnection.Command(connection, _statement))
            {
                command.ExecuteNonQuery();
            }
            return true;
        }

        internal bool UpdateLoanAll(DbConnection connection, tm_loan_all loan)
        {
            string _query = " UPDATE TM_LOAN_ALL "
+ " SET   BRN_CD            = NVL({0},  BRN_CD        )"
+ ", PARTY_CD          = NVL({1},  PARTY_CD         )"
+ ", ACC_CD            = NVL({2},  ACC_CD           )"
+ ", LOAN_ID           = NVL({3},  LOAN_ID          )"
+ ", LOAN_ACC_NO       = NVL({4},  LOAN_ACC_NO      )"
+ ", PRN_LIMIT         = NVL({5},  PRN_LIMIT        )"
+ ", DISB_AMT          = NVL({6},  DISB_AMT         )"
+ ", DISB_DT           = NVL({7},  DISB_DT          )"
+ ", CURR_PRN          = NVL({8},  CURR_PRN         )"
+ ", OVD_PRN           = NVL({9}, OVD_PRN          )"
+ ", CURR_INTT         = NVL({10}, CURR_INTT        )"
+ ", OVD_INTT          = NVL({11}, OVD_INTT         )"
+ ", PRE_EMI_INTT      = NVL({12}, PRE_EMI_INTT     )"
+ ", OTHER_CHARGES     = NVL({13}, OTHER_CHARGES    )"
+ ", CURR_INTT_RATE    = NVL({14}, CURR_INTT_RATE   )"
+ ", OVD_INTT_RATE     = NVL({15}, OVD_INTT_RATE    )"
+ ", DISB_STATUS       = NVL({16}, DISB_STATUS      )"
+ ", PIRIODICITY       = NVL({17}, PIRIODICITY      )"
+ ", TENURE_MONTH      = NVL({18}, TENURE_MONTH     )"
+ ", INSTL_START_DT    = NVL({19}, INSTL_START_DT   )"
+ ", MODIFIED_BY       = NVL({20}, MODIFIED_BY      )"
+ ", MODIFIED_DT       = SYSDATE                     "
+ ", LAST_INTT_CALC_DT = NVL({21}, LAST_INTT_CALC_DT)"
+ ", OVD_TRF_DT        = NVL({22}, OVD_TRF_DT       )"
+ ", APPROVAL_STATUS   = NVL({23}, APPROVAL_STATUS  )"
+ ", CC_FLAG           = NVL({24}, CC_FLAG          )"
+ ", CHEQUE_FACILITY   = NVL({25}, CHEQUE_FACILITY  )"
+ ", INTT_CALC_TYPE    = NVL({26}, INTT_CALC_TYPE   )"
+ ", EMI_FORMULA_NO    = NVL({27}, EMI_FORMULA_NO   )"
+ ", REP_SCH_FLAG      = NVL({28}, REP_SCH_FLAG     )"
+ ", LOAN_CLOSE_DT     = NVL({29}, LOAN_CLOSE_DT    )"
+ ", LOAN_STATUS       = NVL({30}, LOAN_STATUS      )"
+ ", INSTL_AMT         = NVL({31}, INSTL_AMT        )"
+ ", INSTL_NO          = NVL({32}, INSTL_NO         )"
+ ", ACTIVITY_CD       = NVL({33}, ACTIVITY_CD      )"
+ ", ACTIVITY_DTLS     = NVL({34}, ACTIVITY_DTLS    )"
+ ", SECTOR_CD         = NVL({35}, SECTOR_CD        )"
+ ", FUND_TYPE         = NVL({36}, FUND_TYPE        )"
+ ", COMP_UNIT_NO      = NVL({37}, COMP_UNIT_NO     )"
+ " WHERE LOAN_ID           = {38} "
+ " AND   BRN_CD            = {39} ";

            _statement = string.Format(_query,
             string.Concat("'", loan.brn_cd, "'"),
              string.Concat("'", loan.party_cd, "'"),
              string.Concat("'", loan.acc_cd, "'"),
              string.Concat("'", loan.loan_id, "'"),
              string.Concat("'", loan.loan_acc_no, "'"),
              string.Concat("'", loan.prn_limit, "'"),
              string.Concat("'", loan.disb_amt, "'"),
              string.IsNullOrWhiteSpace(loan.disb_dt.ToString()) ? string.Concat("null") : string.Concat("to_date('", loan.disb_dt.Value.ToString("dd/MM/yyyy"), "','dd-mm-yyyy' )"),
              string.Concat("'", loan.curr_prn, "'"),
              string.Concat("'", loan.ovd_prn, "'"),
              string.Concat("'", loan.curr_intt, "'"),
              string.Concat("'", loan.ovd_intt, "'"),
              string.Concat("'", loan.pre_emi_intt, "'"),
              string.Concat("'", loan.other_charges, "'"),
              string.Concat("'", loan.curr_intt_rate, "'"),
              string.Concat("'", loan.ovd_intt_rate, "'"),
              string.Concat("'", loan.disb_status, "'"),
              string.Concat("'", loan.piriodicity, "'"),
              string.Concat("'", loan.tenure_month, "'"),
              string.IsNullOrWhiteSpace(loan.instl_start_dt.ToString()) ? string.Concat("null") : string.Concat("to_date('", loan.instl_start_dt.Value.ToString("dd/MM/yyyy"), "','dd-mm-yyyy' )"),
              string.Concat("'", loan.modified_by, "'"),
              string.IsNullOrWhiteSpace(loan.last_intt_calc_dt.ToString()) ? string.Concat("null") : string.Concat("to_date('", loan.last_intt_calc_dt.Value.ToString("dd/MM/yyyy"), "','dd-mm-yyyy' )"),
              string.IsNullOrWhiteSpace(loan.ovd_trf_dt.ToString()) ? string.Concat("null") : string.Concat("to_date('", loan.ovd_trf_dt.Value.ToString("dd/MM/yyyy"), "','dd-mm-yyyy' )"),
              string.Concat("'", loan.approval_status, "'"),
              string.Concat("'", loan.cc_flag, "'"),
              string.Concat("'", loan.cheque_facility, "'"),
              string.Concat("'", loan.intt_calc_type, "'"),
              string.Concat("'", loan.emi_formula_no, "'"),
              string.Concat("'", loan.rep_sch_flag, "'"),
              string.IsNullOrWhiteSpace(loan.loan_close_dt.ToString()) ? string.Concat("null") : string.Concat("to_date('", loan.loan_close_dt.Value.ToString("dd/MM/yyyy"), "','dd-mm-yyyy' )"),
              string.Concat("'", loan.loan_status, "'"),
              string.Concat("'", loan.instl_amt, "'"),
              string.Concat("'", loan.instl_no, "'"),
              string.Concat("'", loan.activity_cd, "'"),
              string.Concat("'", loan.activity_dtls, "'"),
              string.Concat("'", loan.sector_cd, "'"),
              string.Concat("'", loan.fund_type, "'"),
              string.Concat("'", loan.comp_unit_no, "'"),
               string.Concat("'", loan.loan_id, "'"),
              string.Concat("'", loan.brn_cd, "'")
              );
            using (var command = OrclDbConnection.Command(connection, _statement))
            {
                int count = command.ExecuteNonQuery();
                if (count.Equals(0))
                    InsertLoanAll(connection, loan);
            }
            return true;
        }

        internal bool UpdateGuaranter(DbConnection connection, tm_guaranter loan)
        {
            string _query = " UPDATE TM_GUARANTER "
+ " SET   LOAN_ID        = NVL({0}, LOAN_ID      )"
+ ", ACC_CD         = NVL({1}, ACC_CD       )"
+ ", GUA_TYPE       = NVL({2}, GUA_TYPE     )"
+ ", GUA_ID         = NVL({3}, GUA_ID       )"
+ ", GUA_NAME       = NVL({4}, GUA_NAME     )"
+ ", GUA_ADD        = NVL({5}, GUA_ADD      )"
+ ", OFFICE_NAME    = NVL({6}, OFFICE_NAME  )"
+ ", SHARE_ACC_NUM  = NVL({7}, SHARE_ACC_NUM)"
+ ", SHARE_TYPE     = NVL({8}, SHARE_TYPE   )"
+ ", OPENING_DT     = NVL({9}, OPENING_DT   )"
+ ", SHARE_BAL      = NVL({10}, SHARE_BAL    )"
+ ", DEPART         = NVL({11}, DEPART       )"
+ ", DESIG          = NVL({12}, DESIG        )"
+ ", SALARY         = NVL({13}, SALARY       )"
+ ", SEC_58         = NVL({14}, SEC_58       )"
+ ", MOBILE         = NVL({15}, MOBILE       )"
+ ", SRL_NO         = NVL({16}, SRL_NO       )"
+ " WHERE LOAN_ID = {17} AND ACC_CD = {18}      "
+ " AND SRL_NO = 1 ";
            _statement = string.Format(_query,
               String.Concat("'", loan.loan_id, "'"),
               String.Concat("'", loan.acc_cd, "'"),
               String.Concat("'", loan.gua_type, "'"),
               String.Concat("'", loan.gua_id, "'"),
               String.Concat("'", loan.gua_name, "'"),
               String.Concat("'", loan.gua_add, "'"),
               String.Concat("'", loan.office_name, "'"),
               String.Concat("'", loan.share_acc_num, "'"),
               String.Concat("'", loan.share_type, "'"),
               String.Concat("'", loan.opening_dt, "'"),
               String.Concat("'", loan.share_bal, "'"),
               String.Concat("'", loan.depart, "'"),
               String.Concat("'", loan.desig, "'"),
               String.Concat("'", loan.salary, "'"),
               String.Concat("'", loan.sec_58, "'"),
               String.Concat("'", loan.mobile, "'"),
               String.Concat("'", loan.srl_no, "'"),
               String.Concat("'", loan.loan_id, "'"),
               String.Concat("'", loan.acc_cd, "'")
               );

            using (var command = OrclDbConnection.Command(connection, _statement))
            {
                int count = command.ExecuteNonQuery();
                if (count.Equals(0))
                    Insertguaranter(connection, loan);
            }
            return true;

        }

        internal bool UpdateLoanSanction(DbConnection connection, List<tm_loan_sanction> acc)
        {
            string _query = " UPDATE TM_LOAN_SANCTION               "
+ " SET   LOAN_ID           = NVL({0},LOAN_ID         )"
+ ", SANC_NO           = NVL({1},SANC_NO         )"
+ ", SANC_DT           = NVL({2},SANC_DT         )"
+ ", MODIFIED_BY       = NVL({3},MODIFIED_BY     )"
+ ", MODIFIED_DT       = SYSDATE                  "
+ ", APPROVAL_STATUS   = NVL({4},APPROVAL_STATUS )"
+ ", APPROVED_BY       = NVL({5},APPROVED_BY     )"
+ ", APPROVED_DT       = NVL({6},APPROVED_DT     )"
+ ", MEMO_NO           = NVL({7},MEMO_NO         )"
+ " WHERE LOAN_ID = {8} AND SANC_NO = {9}";
            for (int i = 0; i < acc.Count; i++)
            {
                _statement = string.Format(_query,
                             string.Concat("'", acc[i].loan_id, "'"),
                             string.Concat("'", acc[i].sanc_no, "'"),
                             string.IsNullOrWhiteSpace(acc[i].sanc_dt.ToString()) ? string.Concat("null") : string.Concat("to_date('", acc[i].sanc_dt.Value.ToString("dd/MM/yyyy"), "','dd-mm-yyyy' )"),
                              string.Concat("'", acc[i].modified_by, "'"),
                             string.Concat("'", acc[i].approval_status, "'"),
                             string.Concat("'", acc[i].approved_by, "'"),
                             string.IsNullOrWhiteSpace(acc[i].approved_dt.ToString()) ? string.Concat("null") : string.Concat("to_date('", acc[i].approved_dt.Value.ToString("dd/MM/yyyy"), "','dd-mm-yyyy' )"),
                             string.Concat("'", acc[i].memo_no, "'"),
                              string.Concat("'", acc[i].loan_id, "'"),
                             string.Concat("'", acc[i].sanc_no, "'"));

                using (var command = OrclDbConnection.Command(connection, _statement))
                {
                    int count = command.ExecuteNonQuery();
                    if (count.Equals(0))
                        InsertLoanSanction(connection, acc[i]);
                }
            }
            return true;

        }

        internal bool UpdateAccholder(DbConnection connection, List<td_accholder> acc)
        {
            string _query = " UPDATE td_accholder   "
                 + " SET brn_cd     = {0}, "
                 + " acc_type_cd    = {1}, "
                 + " acc_num        = {2}, "
                 + " acc_holder     = {3}, "
                 + " relation       = {4}, "
                 + " cust_cd        = {5} "
                + " WHERE brn_cd = {6} AND acc_num = {7} AND acc_type_cd=NVL({8},  acc_type_cd )  ";

            for (int i = 0; i < acc.Count; i++)
            {
                _statement = string.Format(_query,
                                 !string.IsNullOrWhiteSpace(acc[i].brn_cd) ? string.Concat("'", acc[i].brn_cd, "'") : "brn_cd",
                                 !string.IsNullOrWhiteSpace(acc[i].acc_type_cd.ToString()) ? string.Concat("'", acc[i].acc_type_cd, "'") : "acc_type_cd",
                                 !string.IsNullOrWhiteSpace(acc[i].acc_num) ? string.Concat("'", acc[i].acc_num, "'") : "acc_num",
                                 !string.IsNullOrWhiteSpace(acc[i].acc_holder) ? string.Concat("'", acc[i].acc_holder, "'") : "acc_holder",
                                 !string.IsNullOrWhiteSpace(acc[i].relation) ? string.Concat("'", acc[i].relation, "'") : "relation",
                                 !string.IsNullOrWhiteSpace(acc[i].cust_cd.ToString()) ? string.Concat("'", acc[i].cust_cd, "'") : "cust_cd",
                                 !string.IsNullOrWhiteSpace(acc[i].brn_cd) ? string.Concat("'", acc[i].brn_cd, "'") : "brn_cd",
                                 !string.IsNullOrWhiteSpace(acc[i].acc_num) ? string.Concat("'", acc[i].acc_num, "'") : "acc_num",
                                 string.Concat("'", acc[i].acc_type_cd, "'")
                                 );
                using (var command = OrclDbConnection.Command(connection, _statement))
                {
                    int count = command.ExecuteNonQuery();
                    if (count.Equals(0))
                        InsertAccholder(connection, acc[i]);

                }
            }
            return true;
        }

        internal bool UpdateLoanSanctionDtls(DbConnection connection, List<tm_loan_sanction_dtls> acc)
        {
            string _query = " UPDATE TM_LOAN_SANCTION_DTLS           "
+ " SET   LOAN_ID         = NVL({0}, LOAN_ID        )"
+ ", SANC_NO         = NVL({1}, SANC_NO        )"
+ ", SECTOR_CD       = NVL({2}, SECTOR_CD      )"
+ ", ACTIVITY_CD     = NVL({3}, ACTIVITY_CD    )"
+ ", CROP_CD         = NVL({4}, CROP_CD        )"
+ ", SANC_AMT        = NVL({5}, SANC_AMT       )"
+ ", DUE_DT          = NVL({6}, DUE_DT         )"
+ ", SANC_STATUS     = NVL({7}, SANC_STATUS    )"
+ ", SRL_NO          = NVL({8}, SRL_NO         )"
+ ", APPROVAL_STATUS = NVL({9}, APPROVAL_STATUS)"
+ " WHERE LOAN_ID = {10} AND SANC_NO = {11} AND SRL_NO = {12}";
            for (int i = 0; i < acc.Count; i++)
            {
                _statement = string.Format(_query,
          string.Concat("'", acc[i].loan_id, "'"),
          string.Concat("'", acc[i].sanc_no, "'"),
          string.Concat("'", acc[i].sector_cd, "'"),
          string.Concat("'", acc[i].activity_cd, "'"),
          string.Concat("'", acc[i].crop_cd, "'"),
          string.Concat("'", acc[i].sanc_amt, "'"),
          string.IsNullOrWhiteSpace(acc[i].due_dt.ToString()) ? string.Concat("null") : string.Concat("to_date('", acc[i].due_dt.Value.ToString("dd/MM/yyyy"), "','dd-mm-yyyy' )"),
          string.Concat("'", acc[i].sanc_status, "'"),
          string.Concat("'", acc[i].srl_no, "'"),
          string.Concat("'", acc[i].approval_status, "'"),
          string.Concat("'", acc[i].loan_id, "'"),
          string.Concat("'", acc[i].sanc_no, "'"),
         string.Concat("'", acc[i].srl_no, "'")
          );

                using (var command = OrclDbConnection.Command(connection, _statement))
                {
                    int count = command.ExecuteNonQuery();
                    if (count.Equals(0))
                        InsertLoanSanctionDtls(connection, acc[i]);
                }
            }
            return true;

        }


        internal tm_loan_all GetLoanAllWithChild(tm_loan_all loan)
        {
            tm_loan_all loanRet = new tm_loan_all();
            string _query = " SELECT MC.BRN_CD,TL.PARTY_CD,TL.ACC_CD,TL.LOAN_ID,TL.LOAN_ACC_NO,TL.PRN_LIMIT,TL.DISB_AMT,TL.DISB_DT, "
                          + "  TL.CURR_PRN,TL.OVD_PRN,TL.CURR_INTT,TL.OVD_INTT,TL.PRE_EMI_INTT,TL.OTHER_CHARGES,TL.CURR_INTT_RATE,TL.OVD_INTT_RATE,TL.DISB_STATUS,TL.PIRIODICITY,TL.TENURE_MONTH, "
                          + " TL.INSTL_START_DT,TL.CREATED_BY,TL.CREATED_DT,TL.MODIFIED_BY,TL.MODIFIED_DT,TL.LAST_INTT_CALC_DT,TL.OVD_TRF_DT,TL.APPROVAL_STATUS,TL.CC_FLAG,TL.CHEQUE_FACILITY,TL.INTT_CALC_TYPE, "
                          + " TL.EMI_FORMULA_NO,TL.REP_SCH_FLAG,TL.LOAN_CLOSE_DT,TL.LOAN_STATUS,TL.INSTL_AMT,TL.INSTL_NO,ACTIVITY_CD,ACTIVITY_DTLS,SECTOR_CD,FUND_TYPE,COMP_UNIT_NO	 "
                          + " ,MC.CUST_NAME "
                          + " FROM TM_LOAN_ALL TL,MM_CUSTOMER MC WHERE TL.BRN_CD={0} AND TL.LOAN_ID={1} "
                          + " AND  TL.PARTY_CD=MC.CUST_CD AND TL.BRN_CD=MC.BRN_CD";

            _statement = string.Format(_query,
                                          !string.IsNullOrWhiteSpace(loan.brn_cd) ? string.Concat("'", loan.brn_cd, "'") : "brn_cd",
                                          !string.IsNullOrWhiteSpace(loan.loan_id) ? string.Concat("'", loan.loan_id, "'") : "LOAN_ID"
                                           );
            using (var connection = OrclDbConnection.NewConnection)
            {
                using (var command = OrclDbConnection.Command(connection, _statement))
                {
                    using (var reader = command.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            {
                                while (reader.Read())
                                {
                                    var d = new tm_loan_all();
                                    d.brn_cd = UtilityM.CheckNull<string>(reader["BRN_CD"]);
                                    d.party_cd = UtilityM.CheckNull<decimal>(reader["PARTY_CD"]);
                                    d.acc_cd = UtilityM.CheckNull<Int32>(reader["ACC_CD"]);
                                    d.loan_id = UtilityM.CheckNull<string>(reader["LOAN_ID"]);
                                    d.loan_acc_no = UtilityM.CheckNull<string>(reader["LOAN_ACC_NO"]);
                                    d.prn_limit = UtilityM.CheckNull<decimal>(reader["PRN_LIMIT"]);
                                    d.disb_amt = UtilityM.CheckNull<decimal>(reader["DISB_AMT"]);
                                    d.disb_dt = UtilityM.CheckNull<DateTime>(reader["DISB_DT"]);
                                    d.curr_prn = UtilityM.CheckNull<decimal>(reader["CURR_PRN"]);
                                    d.ovd_prn = UtilityM.CheckNull<decimal>(reader["OVD_PRN"]);
                                    d.curr_intt = UtilityM.CheckNull<decimal>(reader["CURR_INTT"]);
                                    d.ovd_intt = UtilityM.CheckNull<decimal>(reader["OVD_INTT"]);
                                    d.pre_emi_intt = UtilityM.CheckNull<decimal>(reader["PRE_EMI_INTT"]);
                                    d.other_charges = UtilityM.CheckNull<decimal>(reader["OTHER_CHARGES"]);
                                    d.curr_intt_rate = UtilityM.CheckNull<double>(reader["CURR_INTT_RATE"]);
                                    d.ovd_intt_rate = UtilityM.CheckNull<double>(reader["OVD_INTT_RATE"]);
                                    d.disb_status = UtilityM.CheckNull<string>(reader["DISB_STATUS"]);
                                    d.piriodicity = UtilityM.CheckNull<string>(reader["PIRIODICITY"]);
                                    d.tenure_month = UtilityM.CheckNull<int>(reader["TENURE_MONTH"]);
                                    d.instl_start_dt = UtilityM.CheckNull<DateTime>(reader["INSTL_START_DT"]);
                                    d.created_by = UtilityM.CheckNull<string>(reader["CREATED_BY"]);
                                    d.created_dt = UtilityM.CheckNull<DateTime>(reader["CREATED_DT"]);
                                    d.modified_by = UtilityM.CheckNull<string>(reader["MODIFIED_BY"]);
                                    d.modified_dt = UtilityM.CheckNull<DateTime>(reader["MODIFIED_DT"]);
                                    d.last_intt_calc_dt = UtilityM.CheckNull<DateTime>(reader["LAST_INTT_CALC_DT"]);
                                    d.ovd_trf_dt = UtilityM.CheckNull<DateTime>(reader["OVD_TRF_DT"]);
                                    d.approval_status = UtilityM.CheckNull<string>(reader["APPROVAL_STATUS"]);
                                    d.cc_flag = UtilityM.CheckNull<string>(reader["CC_FLAG"]);
                                    d.cheque_facility = UtilityM.CheckNull<string>(reader["CHEQUE_FACILITY"]);
                                    d.intt_calc_type = UtilityM.CheckNull<string>(reader["INTT_CALC_TYPE"]);
                                    d.emi_formula_no = UtilityM.CheckNull<int>(reader["EMI_FORMULA_NO"]);
                                    d.rep_sch_flag = UtilityM.CheckNull<string>(reader["REP_SCH_FLAG"]);
                                    d.loan_close_dt = UtilityM.CheckNull<DateTime>(reader["LOAN_CLOSE_DT"]);
                                    d.loan_status = UtilityM.CheckNull<string>(reader["LOAN_STATUS"]);
                                    d.instl_amt = UtilityM.CheckNull<decimal>(reader["INSTL_AMT"]);
                                    d.instl_no = UtilityM.CheckNull<int>(reader["INSTL_NO"]);
                                    d.activity_cd = UtilityM.CheckNull<string>(reader["ACTIVITY_CD"]);
                                    d.activity_dtls = UtilityM.CheckNull<string>(reader["ACTIVITY_DTLS"]);
                                    d.sector_cd = UtilityM.CheckNull<string>(reader["SECTOR_CD"]);
                                    d.fund_type = UtilityM.CheckNull<string>(reader["FUND_TYPE"]);
                                    d.comp_unit_no = UtilityM.CheckNull<decimal>(reader["COMP_UNIT_NO"]);
                                    d.cust_name = UtilityM.CheckNull<string>(reader["CUST_NAME"]);
                                    loanRet = d;
                                }
                            }
                        }
                    }
                }
            }
            return loanRet;
        }


        internal List<sm_kcc_param> GetSmKccParam()
        {
            List<sm_kcc_param> mamRets = new List<sm_kcc_param>();
            string _query = " Select PARAM_CD,PARAM_DESC,PARAM_VALUE   from  SM_KCC_PARAMS";
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
                                var mam = new sm_kcc_param();
                                mam.param_cd = UtilityM.CheckNull<string>(reader["PARAM_CD"]);
                                mam.param_desc = UtilityM.CheckNull<string>(reader["PARAM_DESC"]).ToString();
                                mam.param_value = UtilityM.CheckNull<string>(reader["PARAM_VALUE"]);
                                mamRets.Add(mam);
                            }
                        }
                    }
                }

            }
            return mamRets;
        }

        internal string ApproveLoanAccountTranaction(p_gen_param pgp)
        {
            string _ret = null;

            using (var connection = OrclDbConnection.NewConnection)
            {
                using (var transaction = connection.BeginTransaction())
                {
                    try
                    {
                        var updateTdDepTransSuccess = 0;
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
                            updateTdDepTransSuccess = command.ExecuteNonQuery();
                        }
                        if (updateTdDepTransSuccess > 0)
                        {
                            DepTransactionDL _dl1 = new DepTransactionDL();
                            _ret = _dl1.P_TD_DEP_TRANS_LOAN(connection, pgp);
                            if (_ret == "0")
                            {
                                DenominationDL _dl2 = new DenominationDL();
                                _ret = _dl2.P_UPDATE_DENOMINATION(connection, pgp);
                                if (_ret == "0")
                                {
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

        internal List<sm_loan_sanction> GetSmLoanSanctionList()
        {
            List<sm_loan_sanction> smLoanSancList =new List<sm_loan_sanction>();

            string _query=" SELECT ACC_CD , PARAM_CD , PARAM_DESC, PARAM_TYPE, FIELD_NAME , DATASET_NO FROM SM_LOAN_SANCTION ORDER BY PARAM_CD";

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
                               var smLoanSanc = new sm_loan_sanction();

                               smLoanSanc.acc_cd = UtilityM.CheckNull<Int32>(reader["ACC_CD"]);
                               smLoanSanc.param_cd = UtilityM.CheckNull<string>(reader["PARAM_CD"]);
                               smLoanSanc.param_desc = UtilityM.CheckNull<string>(reader["PARAM_DESC"]);
                               smLoanSanc.param_type = UtilityM.CheckNull<string>(reader["PARAM_TYPE"]);
                               smLoanSanc.field_name = UtilityM.CheckNull<string>(reader["FIELD_NAME"]);
                               smLoanSanc.dataset_no = UtilityM.CheckNull<Int32>(reader["DATASET_NO"]);

                               smLoanSancList.Add(smLoanSanc);
                            }
                        }
                    }
                }
            
            }
            return smLoanSancList;
        }


        internal List<td_loan_sanc> SerializeEntireLoanSancList(List<td_loan_sanc_set> tdLoanSacnSetList) 
        {

            List<td_loan_sanc> tdLoanSancList = new List<td_loan_sanc>();
            td_loan_sanc tdLoanSanc = new td_loan_sanc();

            for (int i = 0; i < tdLoanSacnSetList.Count; i++)
            {
                 tdLoanSancList.AddRange(tdLoanSacnSetList[i].tdloansancset);
            }

            return tdLoanSancList;

            
        }

        internal bool InsertLoanSecurityDtls(DbConnection connection, List<td_loan_sanc> tdLoanSancList )
             {
                     string _queryD=" DELETE FROM TD_LOAN_SANC WHERE  LOAN_ID = {0}";
                    _statement = string.Format(_queryD,
                                               !string.IsNullOrWhiteSpace( tdLoanSancList[0].loan_id ) ? string.Concat("'", tdLoanSancList[0].loan_id, "'") : "-1");
                 
                     using (var command = OrclDbConnection.Command(connection, _statement))
                          {                   
                             command.ExecuteNonQuery();
                           }                        
                     
                     string _queryI = "INSERT INTO TD_LOAN_SANC( LOAN_ID , SANC_NO , PARAM_CD , PARAM_VALUE , PARAM_TYPE , DATASET_NO , FIELD_NAME)"
                                    + " VALUES ( {0}, {1}, {2}, {3}, {4}, {5}, {6} )";
                    
                     for (int i = 0; i < tdLoanSancList.Count; i++)
                     {
                       _statement = string.Format(_queryI,
                                                  string.Concat("'", tdLoanSancList[i].loan_id, "'"),
                                                  tdLoanSancList[i].sanc_no ,
                                                  string.Concat("'", tdLoanSancList[i].param_cd , "'"),
                                                  string.Concat("'", tdLoanSancList[i].param_value , "'"),
                                                  string.Concat("'", tdLoanSancList[i].param_type , "'"),
                                                  string.Concat("'", tdLoanSancList[i].dataset_no , "'"),
                                                  string.Concat("'", tdLoanSancList[i].field_name , "'"));
                 
                         using (var command = OrclDbConnection.Command(connection, _statement))
                         {
                             command.ExecuteNonQuery();
                 
                         }
                     }
                 
                     return true;
                 }

internal List<AccDtlsLov> GetLoanDtls(p_gen_param prm)
        {
            List<AccDtlsLov> accDtlsLovs = new List<AccDtlsLov>();

            using (var connection = OrclDbConnection.NewConnection)
            {
                using (var command = OrclDbConnection.Command(connection, "P_GET_LOAN_DTLS"))
                {
                    // ad_acc_type_cd NUMBER,as_cust_name VARCHAR2
                    command.CommandType = System.Data.CommandType.StoredProcedure;
                    var parm = new OracleParameter("ad_loan_type", OracleDbType.Decimal, ParameterDirection.Input);
                    parm.Value = prm.ad_acc_type_cd;
                    command.Parameters.Add(parm);
                    parm = new OracleParameter("as_cust_name", OracleDbType.Varchar2, ParameterDirection.Input);
                    parm.Value = prm.as_cust_name;
                    command.Parameters.Add(parm);
                    parm = new OracleParameter("p_loan_refcur", OracleDbType.RefCursor, ParameterDirection.Output);
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
                                        accDtlsLov.loan_id = UtilityM.CheckNull<string>(reader["LOAN_ID"]);
                                        accDtlsLov.cust_name = UtilityM.CheckNull<string>(reader["CUST_NAME"]);
                                        accDtlsLov.guardian_name = UtilityM.CheckNull<string>(reader["GUARDIAN_NAME"]);
                                        accDtlsLov.present_address = UtilityM.CheckNull<string>(reader["PRESENT_ADDRESS"]);
                                        accDtlsLov.phone = UtilityM.CheckNull<string>(reader["PHONE"]);
                                        accDtlsLov.disb_dt = UtilityM.CheckNull<DateTime>(reader["DISB_DT"]);

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

        internal List<AccDtlsLov> GetLoanDtlsByID(p_gen_param prm)
        {
            List<AccDtlsLov> accDtlsLovs = new List<AccDtlsLov>();

            using (var connection = OrclDbConnection.NewConnection)
            {
                using (var command = OrclDbConnection.Command(connection, "P_GET_LOAN_ID_DTLS"))
                {
                    // ad_acc_type_cd NUMBER,as_cust_name VARCHAR2
                    command.CommandType = System.Data.CommandType.StoredProcedure;
                    var parm = new OracleParameter("as_cust_name", OracleDbType.Varchar2, ParameterDirection.Input);
                    parm.Value = prm.as_cust_name;
                    command.Parameters.Add(parm);
                    parm = new OracleParameter("p_loan_refcur", OracleDbType.RefCursor, ParameterDirection.Output);
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
                                        accDtlsLov.loan_id = UtilityM.CheckNull<string>(reader["LOAN_ID"]);
                                        accDtlsLov.cust_name = UtilityM.CheckNull<string>(reader["CUST_NAME"]);
                                        accDtlsLov.guardian_name = UtilityM.CheckNull<string>(reader["GUARDIAN_NAME"]);
                                        accDtlsLov.present_address = UtilityM.CheckNull<string>(reader["PRESENT_ADDRESS"]);
                                        accDtlsLov.phone = UtilityM.CheckNull<string>(reader["PHONE"]);
                                        accDtlsLov.disb_dt = UtilityM.CheckNull<DateTime>(reader["DISB_DT"]);

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

     internal List<tt_rep_sch> PopulateLoanRepSch(p_loan_param prp)
        {
            List<tt_rep_sch> loanrepschList = new List<tt_rep_sch>();

            string _alter = "ALTER SESSION SET NLS_DATE_FORMAT = 'DD/MM/YYYY HH24:MI:SS'";
            string _statement;
            string _procedure = "p_generate_schedule";
            string _query = " SELECT LOAN_ID,REP_ID,DUE_DT,INSTL_PRN,INSTL_PAID,STATUS "
                            +" FROM TT_REP_SCH WHERE LOAN_ID={0}";

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

                        _statement = string.Format(_procedure);
                        using (var command = OrclDbConnection.Command(connection, _statement))
                        {
                            command.CommandType = System.Data.CommandType.StoredProcedure;

                            var parm1 = new OracleParameter("as_loan_id", OracleDbType.Varchar2, ParameterDirection.Input);
                            parm1.Value = prp.loan_id;
                            command.Parameters.Add(parm1);

                           
                            command.ExecuteNonQuery();

                        }

                        _statement = string.Format(_query,
                         string.Concat("'", prp.loan_id, "'"));

                        using (var command = OrclDbConnection.Command(connection, _statement))
                        {
                            using (var reader = command.ExecuteReader())
                            {
                                if (reader.HasRows)
                                {
                                    while (reader.Read())
                                    {
                                        var loanrepsch = new tt_rep_sch();

                                        loanrepsch.rep_id = Convert.ToInt32(UtilityM.CheckNull<decimal>(reader["REP_ID"]));
                                        loanrepsch.loan_id = UtilityM.CheckNull<string>(reader["LOAN_ID"]);
                                        loanrepsch.due_dt = UtilityM.CheckNull<DateTime>(reader["DUE_DT"]);
                                        loanrepsch.instl_prn = UtilityM.CheckNull<decimal>(reader["INSTL_PRN"]);
                                        loanrepsch.instl_paid = UtilityM.CheckNull<decimal>(reader["INSTL_PAID"]);
                                        loanrepsch.status = UtilityM.CheckNull<string>(reader["STATUS"]);
                                        loanrepschList.Add(loanrepsch);
                                    }
                                    transaction.Commit();
                                }
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        transaction.Rollback();
                        loanrepschList = null;
                    }
                }
            }
            return loanrepschList;
        }

        internal List<tt_detailed_list_loan> GetDefaultList(p_gen_param prp)
        {
            List<tt_detailed_list_loan> dtlListLoanList = new List<tt_detailed_list_loan>();

            string _alter = "ALTER SESSION SET NLS_DATE_FORMAT = 'DD/MM/YYYY HH24:MI:SS'";
            string _statement;
            string _procedure = "p_detailed_list_loan";
            string _query = " SELECT TT_DETAILED_LIST_LOAN.ACC_CD,             "
                           + " TT_DETAILED_LIST_LOAN.PARTY_NAME,               "
                           + " TT_DETAILED_LIST_LOAN.CURR_INTT_RATE,           "
                           + " TT_DETAILED_LIST_LOAN.OVD_INTT_RATE,            "
                           + " TT_DETAILED_LIST_LOAN.CURR_PRN,                 "
                           + " TT_DETAILED_LIST_LOAN.OVD_PRN,                  "
                           + " TT_DETAILED_LIST_LOAN.CURR_INTT,                "
                           + " TT_DETAILED_LIST_LOAN.OVD_INTT,                 "
                           + " TT_DETAILED_LIST_LOAN.ACC_NAME,                 "
                           + " TT_DETAILED_LIST_LOAN.ACC_NUM,                  "
                           + " TT_DETAILED_LIST_LOAN.BLOCK_NAME,               "
                           + " TT_DETAILED_LIST_LOAN.COMPUTED_TILL_DT          "
                           + " FROM TT_DETAILED_LIST_LOAN                      "
                           + " WHERE   ( TT_DETAILED_LIST_LOAN.ACC_CD = {0} )  "
                           + " AND     ( TT_DETAILED_LIST_LOAN.OVD_PRN > 0  )  ";

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

                        _statement = string.Format(_procedure);
                        using (var command = OrclDbConnection.Command(connection, _statement))
                        {
                            command.CommandType = System.Data.CommandType.StoredProcedure;

                            var parm1 = new OracleParameter("as_brn_cd", OracleDbType.Varchar2, ParameterDirection.Input);
                            parm1.Value = prp.brn_cd;
                            command.Parameters.Add(parm1);

                            var parm2 = new OracleParameter("ad_acc_cd", OracleDbType.Int16, ParameterDirection.Input);
                            parm2.Value = prp.acc_cd;
                            command.Parameters.Add(parm2);

                            var parm3 = new OracleParameter("adt_dt", OracleDbType.Date, ParameterDirection.Input);
                            parm3.Value = prp.to_dt;
                            command.Parameters.Add(parm3);

                            command.ExecuteNonQuery();
                        }

                        _statement = string.Format(_query,
                        string.Concat("'", prp.acc_cd, "'"));

                        using (var command = OrclDbConnection.Command(connection, _statement))
                        {
                            using (var reader = command.ExecuteReader())
                            {
                                if (reader.HasRows)
                                {
                                    while (reader.Read())
                                    {
                                        var dtl = new tt_detailed_list_loan();

                                        dtl.acc_cd = UtilityM.CheckNull<Int16>(reader["ACC_CD"]);
                                        dtl.party_name = UtilityM.CheckNull<string>(reader["PARTY_NAME"]);
                                        dtl.curr_intt_rate = UtilityM.CheckNull<decimal>(reader["CURR_INTT_RATE"]);
                                        dtl.ovd_intt_rate = UtilityM.CheckNull<decimal>(reader["OVD_INTT_RATE"]);
                                        dtl.curr_prn = UtilityM.CheckNull<decimal>(reader["CURR_PRN"]);
                                        dtl.ovd_prn = UtilityM.CheckNull<decimal>(reader["OVD_PRN"]);
                                        dtl.curr_intt = UtilityM.CheckNull<decimal>(reader["OVD_PRN"]);
                                        dtl.ovd_intt = UtilityM.CheckNull<decimal>(reader["OVD_PRN"]);
                                        dtl.acc_name = UtilityM.CheckNull<string>(reader["ACC_NAME"]);
                                        dtl.acc_num = UtilityM.CheckNull<string>(reader["ACC_NUM"]);
                                        dtl.block_name = UtilityM.CheckNull<string>(reader["BLOCK_NAME"]);
                                        dtl.computed_till_dt = UtilityM.CheckNull<DateTime>(reader["COMPUTED_TILL_DT"]);

                                        dtlListLoanList.Add(dtl);
                                    }                                    
                                }

                                transaction.Commit();
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        transaction.Rollback();
                        dtlListLoanList = null;
                    }
                }
            }
            return dtlListLoanList;
        }
 


    }
}

