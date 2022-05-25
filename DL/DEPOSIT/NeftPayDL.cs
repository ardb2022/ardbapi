using System;
using System.Collections.Generic;
using SBWSDepositApi.Models;
using SBWSFinanceApi.Config;
using SBWSFinanceApi.Models;
using SBWSFinanceApi.Utility;
using System.Data;
using System.Data.Common;
using Oracle.ManagedDataAccess.Client;

namespace SBWSDepositApi.Deposit
{
    public class NeftPayDL
    {
        string _statement;
        internal List<td_outward_payment> GetNeftOutDtls(td_outward_payment nom)
        {
            List<td_outward_payment> nomList = new List<td_outward_payment>();

        string _query=" SELECT TD_OUTWARD_PAYMENT.BRN_CD, "
+" TD_OUTWARD_PAYMENT.TRANS_DT, "
+" TD_OUTWARD_PAYMENT.TRANS_CD, "
+" TD_OUTWARD_PAYMENT.PAYMENT_TYPE, "
+" TD_OUTWARD_PAYMENT.BENE_NAME, "
+" TD_OUTWARD_PAYMENT.BENE_CODE, "
+" TD_OUTWARD_PAYMENT.AMOUNT, "
+" TD_OUTWARD_PAYMENT.CHARGE_DED, "
+" TD_OUTWARD_PAYMENT.DATE_OF_PAYMENT, "
+" TD_OUTWARD_PAYMENT.BENE_ACC_NO, "
+" TD_OUTWARD_PAYMENT.BENE_IFSC_CODE, "
+" TD_OUTWARD_PAYMENT.DR_ACC_NO, "
+" TD_OUTWARD_PAYMENT.BENE_EMAIL_ID, "
+" TD_OUTWARD_PAYMENT.BENE_MOBILE_NO, "
+" TD_OUTWARD_PAYMENT.BANK_DR_ACC_TYPE, "
+" TD_OUTWARD_PAYMENT.BANK_DR_ACC_NO, "
+" TD_OUTWARD_PAYMENT.BANK_DR_ACC_NAME, "
+" TD_OUTWARD_PAYMENT.CREDIT_NARRATION, "
+" TD_OUTWARD_PAYMENT.PAYMENT_REF_NO, "
+" TD_OUTWARD_PAYMENT.STATUS, "
+" TD_OUTWARD_PAYMENT.REJECTION_REASON, "
+" TD_OUTWARD_PAYMENT.PROCESSING_REMARKS, "
+" TD_OUTWARD_PAYMENT.CUST_REF_NO, "
+" TD_OUTWARD_PAYMENT.VALUE_DATE, "
+" TD_OUTWARD_PAYMENT.CREATED_BY, "
+" TD_OUTWARD_PAYMENT.CREATED_DT, "
+" TD_OUTWARD_PAYMENT.MODIFIED_BY, "
+" TD_OUTWARD_PAYMENT.MODIFIED_DT, "
+" TD_OUTWARD_PAYMENT.APPROVED_BY, "
+" TD_OUTWARD_PAYMENT.APPROVED_DT, "
+" TD_OUTWARD_PAYMENT.APPROVAL_STATUS "
+" FROM TD_OUTWARD_PAYMENT "
+" WHERE ( TD_OUTWARD_PAYMENT.BRN_CD = {0} ) AND "
+" ( TD_OUTWARD_PAYMENT.TRANS_CD = {1} ) ";

            using (var connection = OrclDbConnection.NewConnection)
            {
                _statement = string.Format(_query,
                                          !string.IsNullOrWhiteSpace(nom.brn_cd) ? string.Concat("'", nom.brn_cd, "'")   : "brn_cd",
                                          nom.trans_cd>0 ? string.Concat("'", nom.trans_cd.ToString(), "'") : "trans_cd"
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
                                    var n = new td_outward_payment();
                                  n.brn_cd            =  UtilityM.CheckNull<string>(reader["BRN_CD"]);
n.trans_dt          = UtilityM.CheckNull<DateTime>(reader["TRANS_DT"]);
n.trans_cd          = UtilityM.CheckNull<Int64>(reader["TRANS_CD"]);
n.payment_type      = UtilityM.CheckNull<string>(reader["PAYMENT_TYPE"]);
n.bene_name         = UtilityM.CheckNull<string>(reader["BENE_NAME"]);
n.bene_code         = UtilityM.CheckNull<string>(reader["BENE_CODE"]);
n.amount            = UtilityM.CheckNull<decimal>(reader["AMOUNT"]);
n.charge_ded        = UtilityM.CheckNull<double>(reader["CHARGE_DED"]);
n.date_of_payment   = UtilityM.CheckNull<DateTime>(reader["DATE_OF_PAYMENT"]);
n.bene_acc_no       = UtilityM.CheckNull<string>(reader["BENE_ACC_NO"]);
n.bene_ifsc_code    = UtilityM.CheckNull<string>(reader["BENE_IFSC_CODE"]);
n.dr_acc_no         = UtilityM.CheckNull<Int64>(reader["DR_ACC_NO"]);
n.bene_email_id     = UtilityM.CheckNull<string>(reader["BENE_EMAIL_ID"]);
n.bene_mobile_no    = UtilityM.CheckNull<Int64>(reader["BENE_MOBILE_NO"]);
n.bank_dr_acc_type  = UtilityM.CheckNull<Int32>(reader["BANK_DR_ACC_TYPE"]);
n.bank_dr_acc_no    = UtilityM.CheckNull<string>(reader["BANK_DR_ACC_NO"]);
n.bank_dr_acc_name  = UtilityM.CheckNull<string>(reader["BANK_DR_ACC_NAME"]);
n.credit_narration  = UtilityM.CheckNull<string>(reader["CREDIT_NARRATION"]);
n.payment_ref_no    = UtilityM.CheckNull<string>(reader["PAYMENT_REF_NO"]);
n.status            = UtilityM.CheckNull<string>(reader["STATUS"]);
n.rejection_reason  = UtilityM.CheckNull<string>(reader["REJECTION_REASON"]);
n.processing_remarks= UtilityM.CheckNull<string>(reader["PROCESSING_REMARKS"]);
n.cust_ref_no       = UtilityM.CheckNull<string>(reader["CUST_REF_NO"]);
n.value_date        = UtilityM.CheckNull<DateTime>(reader["VALUE_DATE"]);
n.created_by        = UtilityM.CheckNull<string>(reader["CREATED_BY"]);
n.created_dt        = UtilityM.CheckNull<DateTime>(reader["CREATED_DT"]);
n.modified_by       = UtilityM.CheckNull<string>(reader["MODIFIED_BY"]);
n.modified_dt       = UtilityM.CheckNull<DateTime>(reader["MODIFIED_DT"]);
n.approved_by       = UtilityM.CheckNull<string>(reader["APPROVED_BY"]);
n.approved_dt       = UtilityM.CheckNull<DateTime>(reader["APPROVED_DT"]);
n.approval_status   = UtilityM.CheckNull<string>(reader["APPROVAL_STATUS"]);
                           
                                    nomList.Add(n);
                                }
                            }
                        }
                    }
                }
            }

            return nomList;
        }

        internal int GetTransCDMaxId(DbConnection connection)
        {
            int maxTransCD = 0;
            string _query = "Select Nvl(max(trans_cd) + 1, 1) max_trans_cd"
                            + " From   TD_OUTWARD_PAYMENT";
            _statement = string.Format(_query);
            using (var command = OrclDbConnection.Command(connection, _statement))
            {
                using (var reader = command.ExecuteReader())
                {
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            maxTransCD = Convert.ToInt32(UtilityM.CheckNull<decimal>(reader["MAX_TRANS_CD"]));
                        }
                    }
                }
            }
            return maxTransCD;
        }

        internal decimal GetNeftCharge(p_gen_param pgp)
        {
            decimal rate = 0;
            string _query = " SELECT min(rate) RATE "
		                    +" FROM MM_SERVICE_TAX_SLAB  "
		                    + " WHERE {0} < AMOUNT ";
            _statement = string.Format(_query,
             string.Concat( pgp.ad_prn_amt       ));
            using (var connection = OrclDbConnection.NewConnection)
            {
            using (var command = OrclDbConnection.Command(connection, _statement))
            {
                using (var reader = command.ExecuteReader())
                {
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            rate = Convert.ToInt32(UtilityM.CheckNull<decimal>(reader["RATE"]));
                        }
                    }
                }
            }
            }
            return rate;
        }

       internal int InsertNeftOutDtls(td_outward_payment nom)
        {
            string _query=" INSERT INTO TD_OUTWARD_PAYMENT (BRN_CD,TRANS_DT,TRANS_CD,PAYMENT_TYPE,BENE_NAME,BENE_CODE,AMOUNT,CHARGE_DED, "
+ " DATE_OF_PAYMENT,BENE_ACC_NO,BENE_IFSC_CODE,DR_ACC_NO,BENE_EMAIL_ID,BENE_MOBILE_NO,BANK_DR_ACC_TYPE, "
+ " BANK_DR_ACC_NO,BANK_DR_ACC_NAME,CREDIT_NARRATION,PAYMENT_REF_NO,STATUS,REJECTION_REASON,PROCESSING_REMARKS, "
+ " CUST_REF_NO,VALUE_DATE,CREATED_BY,CREATED_DT,MODIFIED_BY,MODIFIED_DT,APPROVED_BY,APPROVED_DT,APPROVAL_STATUS) " 
+ " VALUES({0},{1},{2},{3},{4},{5},{6},{7},{8},{9},{10},{11},{12},{13}, {14},"
+ " {15},{16}, {17}, {18},{19},{20},{21},{22},{23},{24}, "
+ " SYSDATE,{25},SYSDATE,{26},{27},{28} )";  

            using (var connection = OrclDbConnection.NewConnection)
            {
                using (var transaction = connection.BeginTransaction())
                {
                    try
                    {
                        int transCd=GetTransCDMaxId(connection);
                        _statement = string.Format(_query,
                                                  string.Concat("'", nom.brn_cd              , "'"), 
string.IsNullOrWhiteSpace( nom.trans_dt.ToString()) ? string.Concat("null") : string.Concat("to_date('",  nom.trans_dt.Value.ToString("dd/MM/yyyy"), "','dd-mm-yyyy' )"),
string.Concat("'", transCd            , "'"),
string.Concat("'", nom.payment_type        , "'"),
string.Concat("'", nom.bene_name           , "'"),
string.Concat("'", nom.bene_code           , "'"),
string.Concat("'", nom.amount              , "'"),
string.Concat("'", nom.charge_ded          , "'"),
string.IsNullOrWhiteSpace(nom.date_of_payment.ToString()) ? string.Concat("null") : string.Concat("to_date('", nom.date_of_payment.Value.ToString("dd/MM/yyyy"), "','dd-mm-yyyy' )"),
string.Concat("'", nom.bene_acc_no         , "'"),
string.Concat("'", nom.bene_ifsc_code      , "'"),
string.Concat("'", nom.dr_acc_no           , "'"),
string.Concat("'", nom.bene_email_id       , "'"),
string.Concat("'", nom.bene_mobile_no      , "'"),
string.Concat("'", nom.bank_dr_acc_type    , "'"),
string.Concat("'", nom.bank_dr_acc_no      , "'"),
string.Concat("'", nom.bank_dr_acc_name    , "'"),
string.Concat("'", nom.credit_narration    , "'"),
string.Concat("'", nom.payment_ref_no      , "'"),
string.Concat("'", nom.status              , "'"),
string.Concat("'", nom.rejection_reason    , "'"),
string.Concat("'", nom.processing_remarks  , "'"),
string.Concat("'", nom.cust_ref_no         , "'"),
//string.Concat("'", nom.value_date          , "'"),
string.IsNullOrWhiteSpace( nom.value_date.ToString()) ? string.Concat("null") : string.Concat("to_date('",  nom.value_date.Value.ToString("dd/MM/yyyy"), "','dd-mm-yyyy' )"),
string.Concat("'", nom.created_by          , "'"),
string.Concat("'", nom.modified_by         , "'"),
string.Concat("'", nom.approved_by         , "'"),
//string.Concat("'", nom.approved_dt         , "'"),
string.IsNullOrWhiteSpace( nom.approved_dt.ToString()) ? string.Concat("null") : string.Concat("to_date('",  nom.approved_dt.Value.ToString("dd/MM/yyyy"), "','dd-mm-yyyy' )"),
string.Concat("'", nom.approval_status     , "'") );

                            using (var command = OrclDbConnection.Command(connection, _statement))
                        {                   
                            command.ExecuteNonQuery();
                            transaction.Commit();
                            return transCd;
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
        
        internal List<mm_ifsc_code> GetIfscCode(string ifsc)
        {
            List<mm_ifsc_code> nomList = new List<mm_ifsc_code>();

         string _query=" SELECT BANK,IFSC,BRANCH,ADDRESS,PHONE,CITY,DIST,STATE "  
                      + " FROM MM_IFSC_LIST WHERE  IFSC LIKE  {0}";

            using (var connection = OrclDbConnection.NewConnection)
            {
                _statement = string.Format(_query,
                                          !string.IsNullOrWhiteSpace(ifsc) ? string.Concat("'%", ifsc, "%'")   : "IFSC"
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
                                    var n = new mm_ifsc_code();
                                  n.bank            =  UtilityM.CheckNull<string>(reader["BANK"]);
n.ifsc          = UtilityM.CheckNull<string>(reader["IFSC"]);
n.branch          = UtilityM.CheckNull<string>(reader["BRANCH"]);
n.address      = UtilityM.CheckNull<string>(reader["ADDRESS"]);
n.phone         = UtilityM.CheckNull<string>(reader["PHONE"]);
n.city         = UtilityM.CheckNull<string>(reader["CITY"]);
n.dist            = UtilityM.CheckNull<string>(reader["DIST"]);
n.state        = UtilityM.CheckNull<string>(reader["STATE"]);
                                    nomList.Add(n);
                                }
                            }
                        }
                    }
                }
            }

            return nomList;
        }
        
        
        internal int UpdateNeftOutDtls(td_outward_payment nom)
        {
            int _ret=0;   

            string _query= " UPDATE TD_OUTWARD_PAYMENT SET                       "
 +" PAYMENT_TYPE      = NVL({0}  ,PAYMENT_TYPE      ),  "
 +" BENE_NAME         = NVL({1}  ,BENE_NAME         ),  "
 +" BENE_CODE         = NVL({2}  ,BENE_CODE         ),  "
 +" AMOUNT            = NVL({3}  ,AMOUNT            ),  "
 +" CHARGE_DED        = NVL({4}  ,CHARGE_DED        ),  "
 +" DATE_OF_PAYMENT   = NVL({5}  ,DATE_OF_PAYMENT   ),  "
 +" BENE_ACC_NO       = NVL({6}  ,BENE_ACC_NO       ),  "
 +" BENE_IFSC_CODE    = NVL({7}  ,BENE_IFSC_CODE    ), "
 +" DR_ACC_NO         = NVL({8}  ,DR_ACC_NO         ),  "
 +" BENE_EMAIL_ID     = NVL({9}  ,BENE_EMAIL_ID     ), "
 +" BENE_MOBILE_NO    = NVL({10}  ,BENE_MOBILE_NO    ), "
 +" BANK_DR_ACC_TYPE  = NVL({11}  ,BANK_DR_ACC_TYPE  ), "
 +" BANK_DR_ACC_NO    = NVL({12}  ,BANK_DR_ACC_NO    ), "
 +" BANK_DR_ACC_NAME  = NVL({13}  ,BANK_DR_ACC_NAME  ), "
 +" CREDIT_NARRATION  = NVL({14}  ,CREDIT_NARRATION  ), "
 +" PAYMENT_REF_NO    = NVL({15}  ,PAYMENT_REF_NO    ), "
 +" STATUS            = NVL({16}  ,STATUS            ), "
 +" REJECTION_REASON  = NVL({17}  ,REJECTION_REASON  ), "
 +" PROCESSING_REMARKS= NVL({18}  ,PROCESSING_REMARKS), "
 +" CUST_REF_NO       = NVL({19}  ,CUST_REF_NO       ), "
 +" VALUE_DATE        = NVL({20}  ,VALUE_DATE        ), "
 +" MODIFIED_BY       = NVL({21}  ,MODIFIED_BY       ), "
 +" MODIFIED_DT       = SYSDATE ,                       "
 +" APPROVED_BY       = NVL({22}  ,APPROVED_BY       ), "
 +" APPROVED_DT       = NVL({23}  ,APPROVED_DT       ), "
 +" APPROVAL_STATUS   = NVL({24}  ,APPROVAL_STATUS   )  "
 +" WHERE ( BRN_CD = {25} ) AND                         "
 +" ( TRANS_CD = {26} )                                 ";

            using (var connection = OrclDbConnection.NewConnection)
            {              
                 using (var transaction = connection.BeginTransaction())
                {               
                    try
                    {
                     _statement = string.Format(_query,
                                                  string.Concat("'", nom.payment_type        , "'"),
string.Concat("'", nom.bene_name           , "'"),
string.Concat("'", nom.bene_code           , "'"),
string.Concat("'", nom.amount              , "'"),
string.Concat("'", nom.charge_ded          , "'"),
string.IsNullOrWhiteSpace(nom.date_of_payment.ToString()) ? string.Concat("null") : string.Concat("to_date('", nom.date_of_payment.Value.ToString("dd/MM/yyyy"), "','dd-mm-yyyy' )"),
string.Concat("'", nom.bene_acc_no         , "'"),
string.Concat("'", nom.bene_ifsc_code      , "'"),
string.Concat("'", nom.dr_acc_no           , "'"),
string.Concat("'", nom.bene_email_id       , "'"),
string.Concat("'", nom.bene_mobile_no      , "'"),
string.Concat("'", nom.bank_dr_acc_type    , "'"),
string.Concat("'", nom.bank_dr_acc_no      , "'"),
string.Concat("'", nom.bank_dr_acc_name    , "'"),
string.Concat("'", nom.credit_narration    , "'"),
string.Concat("'", nom.payment_ref_no      , "'"),
string.Concat("'", nom.status              , "'"),
string.Concat("'", nom.rejection_reason    , "'"),
string.Concat("'", nom.processing_remarks  , "'"),
string.Concat("'", nom.cust_ref_no         , "'"),
string.IsNullOrWhiteSpace( nom.value_date.ToString()) ? string.Concat("null") : string.Concat("to_date('",  nom.value_date.Value.ToString("dd/MM/yyyy"), "','dd-mm-yyyy' )"),
string.Concat("'", nom.modified_by         , "'"),
string.Concat("'", nom.approved_by         , "'"),
string.IsNullOrWhiteSpace( nom.approved_dt.ToString()) ? string.Concat("null") : string.Concat("to_date('",  nom.approved_dt.Value.ToString("dd/MM/yyyy"), "','dd-mm-yyyy' )"),
string.Concat("'", nom.approval_status     , "'"),
 string.Concat("'", nom.brn_cd              , "'"), 
string.Concat("'", nom.trans_cd            , "'") );

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
internal int ApproveNeftPaymentTrans(td_outward_payment nom)
        {
            int _ret=-1; 
            string _alter="ALTER SESSION SET NLS_DATE_FORMAT = 'DD/MM/YYYY HH24:MI:SS'";
            string _query1="P_EXPORT";
            string _query2="P_POST_OUTWARD_PAYMENT";
            string _query3= " UPDATE TD_OUTWARD_PAYMENT SET                       "
 +" APPROVED_BY       = NVL({0}  ,APPROVED_BY       ), "
 +" APPROVED_DT       = NVL({1}  ,APPROVED_DT       ), "
 +" APPROVAL_STATUS   = NVL({2}  ,APPROVAL_STATUS   )  "
 +" WHERE ( BRN_CD = {3} ) AND                         "
 +" ( TRANS_CD = {4} )                                 ";
            using (var connection = OrclDbConnection.NewConnection)
            {   
                using (var transaction = connection.BeginTransaction())
                {   
                    try{    
                            using (var command = OrclDbConnection.Command(connection, _alter))
                            {
                                    command.ExecuteNonQuery();
                             }     
                            _statement = string.Format(_query1);
                            using (var command = OrclDbConnection.Command(connection, _statement))
                            {
                                    command.CommandType = System.Data.CommandType.StoredProcedure;
                                    var parm1 = new OracleParameter("AS_BRN_CD", OracleDbType.Varchar2, ParameterDirection.Input);
                                    parm1.Value = nom.brn_cd;
                                    command.Parameters.Add(parm1);
                                    var parm2 = new OracleParameter("ADT_DT", OracleDbType.Date, ParameterDirection.Input);
                                    parm2.Value = nom.trans_dt;
                                    command.Parameters.Add(parm2);
                                     var parm3 = new OracleParameter("AD_TRANS_CD", OracleDbType.Int32, ParameterDirection.Input);
                                    parm3.Value = nom.trans_cd;
                                    command.Parameters.Add(parm3);
                                    command.ExecuteNonQuery();
                                    //transaction.Commit();
                            }
                             _statement = string.Format(_query2);
                            using (var command = OrclDbConnection.Command(connection, _statement))
                            {
                                    command.CommandType = System.Data.CommandType.StoredProcedure;
                                    var parm1 = new OracleParameter("ad_trans_cd", OracleDbType.Int32, ParameterDirection.Input);
                                    parm1.Value = nom.trans_cd;
                                    command.Parameters.Add(parm1);
                                    var parm2 = new OracleParameter("adt_trans_dt", OracleDbType.Date, ParameterDirection.Input);
                                    parm2.Value = nom.trans_dt;
                                    command.Parameters.Add(parm2);
                                    command.ExecuteNonQuery();
                                    //transaction.Commit();
                            }
                             _statement = string.Format(_query3,
                                                string.Concat("'", nom.approved_by         , "'"),
                                                string.IsNullOrWhiteSpace( nom.approved_dt.ToString()) ? string.Concat("null") : string.Concat("to_date('",  nom.approved_dt.Value.ToString("dd/MM/yyyy"), "','dd-mm-yyyy' )"),
                                                string.Concat("'", nom.approval_status     , "'"),
                                                string.Concat("'", nom.brn_cd              , "'"), 
                                                string.Concat("'", nom.trans_cd            , "'") );
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
      internal int DeleteNeftOutDtls(td_outward_payment nom)
        {
            int _ret=0;   

            string _query= " DELETE FROM TD_OUTWARD_PAYMENT               "
 +" WHERE ( BRN_CD = {0} ) AND                         "
 +" ( TRANS_CD = {1} )                                 ";

            using (var connection = OrclDbConnection.NewConnection)
            {              
                 using (var transaction = connection.BeginTransaction())
                {               
                    try
                    {
                     _statement = string.Format(_query,
                                           
 string.Concat("'", nom.brn_cd              , "'"), 
string.Concat("'", nom.trans_cd            , "'") );

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
