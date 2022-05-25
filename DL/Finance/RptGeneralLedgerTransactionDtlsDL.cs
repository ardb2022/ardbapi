using System;
using System.Collections.Generic;
using System.Data;
using Oracle.ManagedDataAccess.Client;
using SBWSFinanceApi.Config;
using SBWSFinanceApi.Models;
using SBWSFinanceApi.Utility;

namespace SBWSFinanceApi.DL
{
    internal sealed class RptGeneralLedgerTransactionDtlsDL
    {
        string _statement;
        internal List<tt_gl_trans> getGeneralLedgerTransactionDtls(p_report_param prm,
        bool orderbyVoucherId = false)
        {
            List<tt_gl_trans> genLdgrTranDtl = new List<tt_gl_trans>();
            string _spName = "P_GL_TRANS_DTLS";
            string _query = " SELECT  TT_GL_TRANS . ACC_CD , " +
                                " TT_GL_TRANS . VOUCHER_DT ,  " +
                                " SUM( TT_GL_TRANS . DR_AMT )  DR_AMT ,  " +
                                " SUM( TT_GL_TRANS . CR_AMT )  CR_AMT , " +
                                " to_number(to_char( TT_GL_TRANS . VOUCHER_DT , 'MM')) trans_month,   " +
                                " to_number(to_char( TT_GL_TRANS . VOUCHER_DT , 'YYYY')) trans_year,  " +
                                " TT_GL_TRANS . OPNG_BAL    " +
                                " FROM  TT_GL_TRANS         " +
                                " WHERE  TT_GL_TRANS . VOUCHER_DT  Between to_date('{0}','dd-mm-yyyy' ) And to_date('{1}','dd-mm-yyyy' )  " +
                                        " AND  TT_GL_TRANS . ACC_CD   Between {2} And {3}  " +
                                " GROUP BY  TT_GL_TRANS . ACC_CD ,   " +
                                        " TT_GL_TRANS . VOUCHER_DT , " +
                                        " TT_GL_TRANS . OPNG_BAL     " +
                                " ORDER BY  TT_GL_TRANS . ACC_CD , " +
                                        "TT_GL_TRANS . VOUCHER_DT ";
            if (orderbyVoucherId)
            {
                _query += " TT_GL_TRANS.VOUCHER_ID ";
            }
            using (var connection = OrclDbConnection.NewConnection)
            {
                using (var transaction = connection.BeginTransaction())
                {
                    try
                    {
                        // _statement = string.Format(_query);
                        using (var command = OrclDbConnection.Command(connection, _spName))
                        {
                            command.CommandType = System.Data.CommandType.StoredProcedure;
                            var parm = new OracleParameter("adt_from_dt", OracleDbType.Date, ParameterDirection.Input);
                            parm.Value = prm.from_dt;
                            command.Parameters.Add(parm);

                            parm = new OracleParameter("adt_to_dt", OracleDbType.Date, ParameterDirection.Input);
                            parm.Value = prm.to_dt;
                            command.Parameters.Add(parm);

                            parm = new OracleParameter("ad_from_acc_cd", OracleDbType.Int32, ParameterDirection.Input);
                            parm.Value = prm.ad_from_acc_cd;
                            command.Parameters.Add(parm);

                            parm = new OracleParameter("ad_to_acc_Cd", OracleDbType.Int32, ParameterDirection.Input);
                            parm.Value = prm.ad_to_acc_cd;
                            command.Parameters.Add(parm);

                            parm = new OracleParameter("as_brn_cd", OracleDbType.Varchar2, ParameterDirection.Input);
                            parm.Value = prm.brn_cd;
                            command.Parameters.Add(parm);

                            command.ExecuteNonQuery();
                        }
                        _statement = string.Format(_query,
                                            prm.from_dt!= null ? prm.from_dt.ToString("dd/MM/yyyy"): "from_dt",
                                            prm.to_dt!= null ? prm.to_dt.ToString("dd/MM/yyyy"): "to_dt",
                                            prm.ad_from_acc_cd !=0 ? Convert.ToString(prm.ad_from_acc_cd) : "ad_from_acc_cd",
                                            prm.ad_to_acc_cd !=0 ? Convert.ToString(prm.ad_to_acc_cd) : "ad_to_acc_cd");
                        using (var command = OrclDbConnection.Command(connection, _statement))
                        {
                            using (var reader = command.ExecuteReader())
                            {
                                if (reader.HasRows)
                                {
                                    while (reader.Read())
                                    {
                                        var tGenTran = new tt_gl_trans();
                                        tGenTran.acc_cd = UtilityM.CheckNull<int>(reader["ACC_CD"]);
                                        tGenTran.voucher_dt = UtilityM.CheckNull<DateTime>(reader["VOUCHER_DT"]);
                                        tGenTran.dr_amt = UtilityM.CheckNull<decimal>(reader["DR_AMT"]);
                                        tGenTran.cr_amt = UtilityM.CheckNull<decimal>(reader["CR_AMT"]);
                                        tGenTran.trans_month = UtilityM.CheckNull<decimal>(reader["trans_month"]);
                                        tGenTran.trans_year = UtilityM.CheckNull<decimal>(reader["trans_year"]);
                                        tGenTran.opng_bal = UtilityM.CheckNull<decimal>(reader["OPNG_BAL"]);
                                        genLdgrTranDtl.Add(tGenTran);
                                    }
                                }
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        transaction.Rollback();
                        genLdgrTranDtl = null;
                    }
                }
            }
            return genLdgrTranDtl;
        }
    }
}