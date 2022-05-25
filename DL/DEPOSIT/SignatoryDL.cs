using System;
using System.Collections.Generic;
using SBWSDepositApi.Models;
using SBWSFinanceApi.Config;
using SBWSFinanceApi.Utility;

namespace SBWSDepositApi.Deposit
{
    public class SignatoryDL
    {
        string _statement;
        internal List<td_signatory> GetSignatoryTemp(td_signatory sig)
        {
            List<td_signatory> sigList = new List<td_signatory>();

            string _query = "SELECT BRN_CD,"
             +" ACC_TYPE_CD,"
             +" ACC_NUM,"
             +" SIGNATORY_NAME"
             +" FROM TD_SIGNATORY_TEMP"
             +" WHERE BRN_CD = {0} AND ACC_NUM = {1}";

            using (var connection = OrclDbConnection.NewConnection)
            {
                _statement = string.Format(_query,
                                          !string.IsNullOrWhiteSpace(sig.brn_cd) ? string.Concat("'", sig.brn_cd, "'") : "brn_cd",
                                          !string.IsNullOrWhiteSpace(sig.acc_num) ? string.Concat("'", sig.acc_num, "'") : "acc_num"
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
                                    var s = new td_signatory();
                                    s.brn_cd = UtilityM.CheckNull<string>(reader["BRN_CD"]);
                                    s.acc_type_cd = UtilityM.CheckNull<int>(reader["ACC_TYPE_CD"]);
                                    s.acc_num = UtilityM.CheckNull<string>(reader["ACC_NUM"]);
                                    s.signatory_name = UtilityM.CheckNull<string>(reader["SIGNATORY_NAME"]);

                                    sigList.Add(s);
                                }
                            }
                        }
                    }
                }
            }

            return sigList;
        }
      internal decimal InsertSignatoryTemp(td_signatory sig)
        {
            List<td_signatory> sigList = new List<td_signatory>();

           string _query= "INSERT INTO TD_SIGNATORY_TEMP ( brn_cd, acc_type_cd, acc_num, signatory_name) "
                         +" VALUES( {0},{1},{2},{3}) ";

            using (var connection = OrclDbConnection.NewConnection)
            {
                using (var transaction = connection.BeginTransaction())
                {
                    try
                    {
                        _statement = string.Format(_query,
                                                   string.Concat("'", sig.brn_cd, "'"),
                                                   sig.acc_type_cd ,
                                                   string.Concat("'", sig.acc_num, "'"),
                                                   string.Concat("'", sig.signatory_name, "'")
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
        internal int UpdateSignatoryTemp(td_signatory sig)
        {
            int _ret=0;   

            string _query=" UPDATE TD_SIGNATORY_TEMP " 
             +" SET brn_cd     = {0}  ,  "
             +" acc_type_cd    = {1}  ,  "
             +" acc_num        = {2}  ,  "
             +" signatory_name = {3}     "
            +" WHERE brn_cd = {0} AND acc_num = {1}  ";

            using (var connection = OrclDbConnection.NewConnection)
            {              
                 using (var transaction = connection.BeginTransaction())
                {               
                    try
                    {
                     _statement = string.Format(_query,
                                          !string.IsNullOrWhiteSpace(sig.brn_cd) ? string.Concat("'", sig.brn_cd, "'") : "brn_cd",
                                          !string.IsNullOrWhiteSpace(sig.acc_type_cd.ToString()) ? string.Concat("'", sig.acc_type_cd, "'") : "acc_type_cd",
                                          !string.IsNullOrWhiteSpace(sig.acc_num) ? string.Concat("'", sig.acc_num, "'") : "acc_num",
                                          !string.IsNullOrWhiteSpace(sig.signatory_name) ? string.Concat("'", sig.signatory_name, "'") : "signatory_name",
                                          !string.IsNullOrWhiteSpace(sig.brn_cd) ? string.Concat("'", sig.brn_cd, "'") : "brn_cd",
                                          !string.IsNullOrWhiteSpace(sig.acc_num) ? string.Concat("'", sig.acc_num, "'") : "acc_num"
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
       internal int DeleteSignatoryTemp(td_signatory sig)
        {
            int _ret=0;

            string _query=" DELETE FROM TD_SIGNATORY_TEMP  "
             +" WHERE brn_cd = {0} AND acc_num = {1} AND acc_type_cd={2}";

            using (var connection = OrclDbConnection.NewConnection)
            {              
                 using (var transaction = connection.BeginTransaction())
                {               
                    try
                    {
                     _statement = string.Format(_query,
                                          !string.IsNullOrWhiteSpace(sig.brn_cd) ? string.Concat("'", sig.brn_cd, "'") : "brn_cd",
                                          !string.IsNullOrWhiteSpace(sig.acc_num) ? string.Concat("'", sig.acc_num, "'") : "acc_num",
                                          !string.IsNullOrWhiteSpace(sig.acc_type_cd.ToString()) ? string.Concat("'", sig.acc_type_cd, "'") : "acc_type_cd"
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
        internal List<td_signatory> GetSignatory(td_signatory sig)
        {
            List<td_signatory> sigList = new List<td_signatory>();

            string _query = "SELECT BRN_CD,"
             +" ACC_TYPE_CD,"
             +" ACC_NUM,"
             +" SIGNATORY_NAME"
             +" FROM TD_SIGNATORY"
             +" WHERE BRN_CD = {0} AND ACC_NUM = {1}";

            using (var connection = OrclDbConnection.NewConnection)
            {
                _statement = string.Format(_query,
                                          !string.IsNullOrWhiteSpace(sig.brn_cd) ? string.Concat("'", sig.brn_cd, "'") : "brn_cd",
                                          !string.IsNullOrWhiteSpace(sig.acc_num) ? string.Concat("'", sig.acc_num, "'") : "acc_num"
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
                                    var s = new td_signatory();
                                    s.brn_cd = UtilityM.CheckNull<string>(reader["BRN_CD"]);
                                    s.acc_type_cd = UtilityM.CheckNull<int>(reader["ACC_TYPE_CD"]);
                                    s.acc_num = UtilityM.CheckNull<string>(reader["ACC_NUM"]);
                                    s.signatory_name = UtilityM.CheckNull<string>(reader["SIGNATORY_NAME"]);

                                    sigList.Add(s);
                                }
                            }
                        }
                    }
                }
            }

            return sigList;
        }
      internal decimal InsertSignatory(td_signatory sig)
        {
            List<td_signatory> sigList = new List<td_signatory>();

           string _query= "INSERT INTO TD_SIGNATORY ( brn_cd, acc_type_cd, acc_num, signatory_name) "
                         +" VALUES( {0},{1},{2},{3}) ";

            using (var connection = OrclDbConnection.NewConnection)
            {
                using (var transaction = connection.BeginTransaction())
                {
                    try
                    {
                        _statement = string.Format(_query,
                                                   string.Concat("'", sig.brn_cd, "'"),
                                                   sig.acc_type_cd ,
                                                   string.Concat("'", sig.acc_num, "'"),
                                                   string.Concat("'", sig.signatory_name, "'")
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
        internal int UpdateSignatory(td_signatory sig)
        {
            int _ret=0;   

            string _query=" UPDATE TD_SIGNATORY " 
             +" SET brn_cd     = {0}  ,  "
             +" acc_type_cd    = {1}  ,  "
             +" acc_num        = {2}  ,  "
             +" signatory_name = {3}     "
            +" WHERE brn_cd = {0} AND acc_num = {1}  ";

            using (var connection = OrclDbConnection.NewConnection)
            {              
                 using (var transaction = connection.BeginTransaction())
                {               
                    try
                    {
                     _statement = string.Format(_query,
                                          !string.IsNullOrWhiteSpace(sig.brn_cd) ? string.Concat("'", sig.brn_cd, "'") : "brn_cd",
                                          !string.IsNullOrWhiteSpace(sig.acc_type_cd.ToString()) ? string.Concat("'", sig.acc_type_cd, "'") : "acc_type_cd",
                                          !string.IsNullOrWhiteSpace(sig.acc_num) ? string.Concat("'", sig.acc_num, "'") : "acc_num",
                                          !string.IsNullOrWhiteSpace(sig.signatory_name) ? string.Concat("'", sig.signatory_name, "'") : "signatory_name",
                                          !string.IsNullOrWhiteSpace(sig.brn_cd) ? string.Concat("'", sig.brn_cd, "'") : "brn_cd",
                                          !string.IsNullOrWhiteSpace(sig.acc_num) ? string.Concat("'", sig.acc_num, "'") : "acc_num"
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
       internal int DeleteSignatory(td_signatory sig)
        {
            int _ret=0;

            string _query=" DELETE FROM TD_SIGNATORY  "
             +" WHERE brn_cd = {0} AND acc_num = {1} AND  acc_type_cd={2}";

            using (var connection = OrclDbConnection.NewConnection)
            {              
                 using (var transaction = connection.BeginTransaction())
                {               
                    try
                    {
                     _statement = string.Format(_query,
                                          !string.IsNullOrWhiteSpace(sig.brn_cd) ? string.Concat("'", sig.brn_cd, "'") : "brn_cd",
                                          !string.IsNullOrWhiteSpace(sig.acc_num) ? string.Concat("'", sig.acc_num, "'") : "acc_num",
                                          !string.IsNullOrWhiteSpace(sig.acc_type_cd.ToString()) ? string.Concat("'", sig.acc_type_cd, "'") : "acc_type_cd"
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
