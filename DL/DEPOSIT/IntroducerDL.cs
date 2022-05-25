using System;
using System.Collections.Generic;
using SBWSDepositApi.Models;
using SBWSFinanceApi.Config;
using SBWSFinanceApi.Utility;

namespace SBWSDepositApi.Deposit
{
    public class IntroducerDL
    {
        string _statement;
        internal List<td_introducer> GetIntroducerTemp(td_introducer ind)
        {
            List<td_introducer> indList = new List<td_introducer>();

        string _query="SELECT BRN_CD, "
             +" ACC_TYPE_CD,        "
             +" ACC_NUM,            "
             +" SRL_NO,             "
             +" INTRODUCER_NAME,    "
             +" INTRODUCER_ACC_TYPE,"
             +" INTRODUCER_ACC_NUM  "
             +" FROM TD_INTRODUCER_TEMP  "
             +" WHERE BRN_CD = {0} AND ACC_NUM = {1} ";

            using (var connection = OrclDbConnection.NewConnection)
            {
                _statement = string.Format(_query,
                                          !string.IsNullOrWhiteSpace(ind.brn_cd) ? string.Concat("'", ind.brn_cd, "'")   : "brn_cd",
                                          !string.IsNullOrWhiteSpace(ind.acc_num) ? string.Concat("'", ind.acc_num, "'") : "acc_num"                                          
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
                                    var i = new td_introducer();
                                    i.brn_cd = UtilityM.CheckNull<string>(reader["BRN_CD"]);
                                    i.acc_type_cd = UtilityM.CheckNull<int>(reader["ACC_TYPE_CD"]);
                                    i.acc_num = UtilityM.CheckNull<string>(reader["ACC_NUM"]);

                                    i.srl_no = UtilityM.CheckNull<Int16>(reader["SRL_NO"]);
                                    i.introducer_name = UtilityM.CheckNull<string>(reader["INTRODUCER_NAME"]);
                                    i.introducer_acc_type = UtilityM.CheckNull<int>(reader["INTRODUCER_ACC_TYPE"]);
                                    i.introducer_acc_num = UtilityM.CheckNull<string>(reader["INTRODUCER_ACC_NUM"]);                                 

                                    indList.Add(i);
                                }
                            }
                        }
                    }
                }
            }

            return indList;
        }
       internal decimal InsertIntroducerTemp(td_introducer ind)
        {
            string _query= "INSERT INTO TD_INTRODUCER_TEMP ( brn_cd, acc_type_cd, acc_num, srl_no, introducer_name, introducer_acc_type, introducer_acc_num) "
                         +" VALUES( {0},{1},{2},{3}, {4}, {5} , {6} ) ";

            using (var connection = OrclDbConnection.NewConnection)
            {
                using (var transaction = connection.BeginTransaction())
                {
                    try
                    {
                        _statement = string.Format(_query,
                                                   string.Concat("'", ind.brn_cd, "'"),
                                                   ind.acc_type_cd ,
                                                   string.Concat("'", ind.acc_num, "'"),
                                                   ind.srl_no,
                                                   string.Concat("'", ind.introducer_name, "'"),
                                                   string.Concat("'", ind.introducer_acc_type, "'"),
                                                   string.Concat("'", ind.introducer_acc_num, "'")            
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
        internal int UpdateIntroducerTemp(td_introducer ind)
        {
            int _ret=0;   

            string _query=" UPDATE TD_INTRODUCER_TEMP " 
             +" SET brn_cd          = {0} , "
             +" acc_type_cd         = {1} , "
             +" acc_num             = {2} , "
             +" srl_no              = {3} , "
             +" introducer_name     = {4} , "
             +" introducer_acc_type = {5} , "
             +" introducer_acc_num  = {6}   "             
            +"  WHERE brn_cd = {0} AND acc_num = {1}  ";

            using (var connection = OrclDbConnection.NewConnection)
            {              
                 using (var transaction = connection.BeginTransaction())
                {               
                    try
                    {
                     _statement = string.Format(_query,
                                          !string.IsNullOrWhiteSpace(ind.brn_cd) ? string.Concat("'", ind.brn_cd, "'") : "brn_cd",
                                          !string.IsNullOrWhiteSpace(ind.acc_type_cd.ToString()) ? string.Concat("'", ind.acc_type_cd, "'") : "acc_type_cd",
                                          !string.IsNullOrWhiteSpace(ind.acc_num) ? string.Concat("'", ind.acc_num, "'") : "acc_num",
                                          !string.IsNullOrWhiteSpace(ind.srl_no.ToString()) ? string.Concat("'", ind.srl_no, "'") : "srl_no",
                                          !string.IsNullOrWhiteSpace(ind.introducer_name) ? string.Concat("'", ind.introducer_name, "'") : "introducer_name",
                                          !string.IsNullOrWhiteSpace(ind.introducer_acc_type.ToString()) ? string.Concat("'", ind.introducer_acc_type, "'") : "introducer_acc_type",
                                          !string.IsNullOrWhiteSpace(ind.introducer_acc_num) ? string.Concat("'", ind.introducer_acc_num, "'") : "introducer_acc_num"                                          
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
       internal int DeleteIntroducerTemp(td_introducer ind)
        {
            int _ret=0;

            string _query=" DELETE FROM TD_INTRODUCER_TEMP "
             +" WHERE brn_cd = {0} AND acc_num = {1} AND  ACC_TYPE_CD = {2}";

            using (var connection = OrclDbConnection.NewConnection)
            {              
                 using (var transaction = connection.BeginTransaction())
                {               
                    try
                    {
                     _statement = string.Format(_query,
                                          !string.IsNullOrWhiteSpace(ind.brn_cd) ? string.Concat("'", ind.brn_cd, "'") : "brn_cd",
                                          !string.IsNullOrWhiteSpace(ind.acc_num) ? string.Concat("'", ind.acc_num, "'") : "acc_num",
                                          (ind.acc_type_cd > 0) ? ind.acc_type_cd.ToString() : "ACC_TYPE_CD"
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
  internal List<td_introducer> GetIntroducer(td_introducer ind)
        {
            List<td_introducer> indList = new List<td_introducer>();

        string _query="SELECT BRN_CD, "
             +" ACC_TYPE_CD,        "
             +" ACC_NUM,            "
             +" SRL_NO,             "
             +" INTRODUCER_NAME,    "
             +" INTRODUCER_ACC_TYPE,"
             +" INTRODUCER_ACC_NUM  "
             +" FROM TD_INTRODUCER  "
             +" WHERE BRN_CD = {0} AND ACC_NUM = {1} ";

            using (var connection = OrclDbConnection.NewConnection)
            {
                _statement = string.Format(_query,
                                          !string.IsNullOrWhiteSpace(ind.brn_cd) ? string.Concat("'", ind.brn_cd, "'")   : "brn_cd",
                                          !string.IsNullOrWhiteSpace(ind.acc_num) ? string.Concat("'", ind.acc_num, "'") : "acc_num"                                          
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
                                    var i = new td_introducer();
                                    i.brn_cd = UtilityM.CheckNull<string>(reader["BRN_CD"]);
                                    i.acc_type_cd = UtilityM.CheckNull<int>(reader["ACC_TYPE_CD"]);
                                    i.acc_num = UtilityM.CheckNull<string>(reader["ACC_NUM"]);

                                    i.srl_no = UtilityM.CheckNull<Int16>(reader["SRL_NO"]);
                                    i.introducer_name = UtilityM.CheckNull<string>(reader["INTRODUCER_NAME"]);
                                    i.introducer_acc_type = UtilityM.CheckNull<int>(reader["INTRODUCER_ACC_TYPE"]);
                                    i.introducer_acc_num = UtilityM.CheckNull<string>(reader["INTRODUCER_ACC_NUM"]);                                 

                                    indList.Add(i);
                                }
                            }
                        }
                    }
                }
            }

            return indList;
        }
       internal decimal InsertIntroducer(td_introducer ind)
        {
            string _query= "INSERT INTO TD_INTRODUCER ( brn_cd, acc_type_cd, acc_num, srl_no, introducer_name, introducer_acc_type, introducer_acc_num) "
                         +" VALUES( {0},{1},{2},{3}, {4}, {5} , {6} ) ";

            using (var connection = OrclDbConnection.NewConnection)
            {
                using (var transaction = connection.BeginTransaction())
                {
                    try
                    {
                        _statement = string.Format(_query,
                                                   string.Concat("'", ind.brn_cd, "'"),
                                                   ind.acc_type_cd ,
                                                   string.Concat("'", ind.acc_num, "'"),
                                                   ind.srl_no,
                                                   string.Concat("'", ind.introducer_name, "'"),
                                                   string.Concat("'", ind.introducer_acc_type, "'"),
                                                   string.Concat("'", ind.introducer_acc_num, "'")            
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
        internal int UpdateIntroducer(td_introducer ind)
        {
            int _ret=0;   

            string _query=" UPDATE TD_INTRODUCER " 
             +" SET brn_cd          = {0} , "
             +" acc_type_cd         = {1} , "
             +" acc_num             = {2} , "
             +" srl_no              = {3} , "
             +" introducer_name     = {4} , "
             +" introducer_acc_type = {5} , "
             +" introducer_acc_num  = {6}   "             
            +"  WHERE brn_cd = {0} AND acc_num = {1}  ";

            using (var connection = OrclDbConnection.NewConnection)
            {              
                 using (var transaction = connection.BeginTransaction())
                {               
                    try
                    {
                     _statement = string.Format(_query,
                                          !string.IsNullOrWhiteSpace(ind.brn_cd) ? string.Concat("'", ind.brn_cd, "'") : "brn_cd",
                                          !string.IsNullOrWhiteSpace(ind.acc_type_cd.ToString()) ? string.Concat("'", ind.acc_type_cd, "'") : "acc_type_cd",
                                          !string.IsNullOrWhiteSpace(ind.acc_num) ? string.Concat("'", ind.acc_num, "'") : "acc_num",
                                          !string.IsNullOrWhiteSpace(ind.srl_no.ToString()) ? string.Concat("'", ind.srl_no, "'") : "srl_no",
                                          !string.IsNullOrWhiteSpace(ind.introducer_name) ? string.Concat("'", ind.introducer_name, "'") : "introducer_name",
                                          !string.IsNullOrWhiteSpace(ind.introducer_acc_type.ToString()) ? string.Concat("'", ind.introducer_acc_type, "'") : "introducer_acc_type",
                                          !string.IsNullOrWhiteSpace(ind.introducer_acc_num) ? string.Concat("'", ind.introducer_acc_num, "'") : "introducer_acc_num"                                          
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
       internal int DeleteIntroducer(td_introducer ind)
        {
            int _ret=0;

            string _query=" DELETE FROM TD_INTRODUCER "
             +" WHERE brn_cd = {0} AND acc_num = {1} AND  ACC_TYPE_CD = {2}";

            using (var connection = OrclDbConnection.NewConnection)
            {              
                 using (var transaction = connection.BeginTransaction())
                {               
                    try
                    {
                     _statement = string.Format(_query,
                                          !string.IsNullOrWhiteSpace(ind.brn_cd) ? string.Concat("'", ind.brn_cd, "'") : "brn_cd",
                                          !string.IsNullOrWhiteSpace(ind.acc_num) ? string.Concat("'", ind.acc_num, "'") : "acc_num",
                                          (ind.acc_type_cd > 0) ? ind.acc_type_cd.ToString() : "ACC_TYPE_CD"
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
