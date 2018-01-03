using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using DBSync.Enumerations;
using MySql.Data.MySqlClient;

namespace DBSync.Services
{
    public class SQLHelper
    {
        private static DataSet cdsConfig;
        private static IDbConnection GetConnection(DatabaseConfig dbconf)
        {
            IDbConnection con=null;
            DbConnectionStringBuilder dcsb = null;
            switch (dbconf.ServerType)
            {
                case DatabaseServerType.SqlServer:
                    con=new SqlConnection();
                    dcsb=new SqlConnectionStringBuilder();
                    break;
                case DatabaseServerType.MySQL:
                    con=new MySqlConnection();
                    dcsb = new MySqlConnectionStringBuilder();
                    break;
            }

            if (dcsb is SqlConnectionStringBuilder)
            {
                var csb = (SqlConnectionStringBuilder) dcsb;
                csb.DataSource = dbconf.DataSource;
                csb.InitialCatalog = dbconf.Database;
                if (dbconf.AuthType == DatabaseAuthType.Windows)
                {
                    csb.IntegratedSecurity = true;
                }
                else
                {
                    csb.UserID = dbconf.UserID;
                    csb.Password = dbconf.Password;
                }
                con.ConnectionString = csb.ConnectionString;
            }
            else if (dcsb is MySqlConnectionStringBuilder)
            {
                var csb = (MySqlConnectionStringBuilder) dcsb;
                csb.Database = dbconf.Database;
                csb.Server = dbconf.DataSource;
                csb.Port = (uint)dbconf.Port;
                csb.AllowBatch = true;
                csb.AllowUserVariables = true;
                if (dbconf.AuthType == DatabaseAuthType.Windows)
                {
                    csb.IntegratedSecurity = true;
                }
                else
                {
                    csb.UserID = dbconf.UserID;
                    csb.Password = dbconf.Password;
                }
                con.ConnectionString = csb.ConnectionString;
            }
            return con;
        }

        public static bool TryConnection(DatabaseConfig dbconf,out string msg)
        {
            msg = "";
            var con = GetConnection(dbconf);
            if (con == null)
            {
                msg = "不支持的数据库类型";
                return false;
            }
            try
            {
                con.Open();
                con.Close();
                msg = "数据库连接成功";
                return true;
            }
            catch (Exception ex)
            {
                msg = ex.Message;
                return false;
            }
        }

        private static void GetCommonParas(ref IDbCommand cmd)
        {
            cdsConfig = new DataSet();
            cdsConfig.ReadXml($@"{Program.basePath}\config.xml");
            foreach (DataRow dr in cdsConfig.Tables["global_var"].Rows)
            {
                string var_name = "gv_" + dr["var_name"].ToString();
                object var_value = dr["var_value"];
                if (cmd is SqlCommand)
                {
                    (cmd as SqlCommand).Parameters.AddWithValue(var_name, var_value);
                }
                else if (cmd is MySqlCommand)
                {
                    (cmd as MySqlCommand).Parameters.AddWithValue(var_name, var_value);
                }
            }
        }
        private static void GetCommonParasSqlServer(ref SqlCommand cmd)
        {
            IDbCommand scmd = cmd;
            GetCommonParas(ref scmd);
        }

        private static void GetCommonParasMySql(ref MySqlCommand cmd) {
            IDbCommand mcmd = cmd;
            GetCommonParas(ref mcmd);
        }

        public static DataSet QueryDataSet(DatabaseConfig dbconf, string sql, List<IDbDataParameter> parameters,
            out string msg)
        {
            var con = GetConnection(dbconf);
            msg = "";
            DataSet dataSet=new DataSet();
            if (con is SqlConnection)
            {
                SqlCommand cmd = new SqlCommand(sql, con as SqlConnection);
                if (parameters != null)
                {
                    foreach (IDbDataParameter parameter in parameters)
                    {
                        cmd.Parameters.Add(parameter);
                    }
                }
                GetCommonParasSqlServer(ref cmd);
                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                con.Open();
                sda.Fill(dataSet);
                con.Close();
                return dataSet;
            }
            else if (con is MySqlConnection)
            {
                MySqlCommand cmd = new MySqlCommand(sql,con as MySqlConnection);
                if (parameters != null)
                {
                    foreach (IDbDataParameter parameter in parameters)
                    {
                        cmd.Parameters.Add(parameter);
                    }
                }
                GetCommonParasMySql(ref cmd);
                MySqlDataAdapter mda=new MySqlDataAdapter(cmd);
                con.Open();
                mda.Fill(dataSet);
                con.Close();
                return dataSet;
            }
            else
            {
                msg = "不支持的数据库类型";
                return null;
            }
        }

        public static int NonQuery(DatabaseConfig dbconf, string sql, List<IDbDataParameter> parameters,
           out string msg)
        {
            var con = GetConnection(dbconf);
            msg = "";
            DataSet dataSet = new DataSet();
            con.Open();
            if (con is SqlConnection)
            {
                SqlCommand cmd = new SqlCommand(sql, con as SqlConnection);
                if (parameters != null)
                {
                    foreach (IDbDataParameter parameter in parameters)
                    {
                        cmd.Parameters.Add(parameter);
                    }
                }
                GetCommonParasSqlServer(ref cmd);
                int ret= cmd.ExecuteNonQuery();
                con.Close();
                return ret;
            }
            else if (con is MySqlConnection)
            {
                MySqlCommand cmd = new MySqlCommand(sql, con as MySqlConnection);
                if (parameters != null)
                {
                    foreach (IDbDataParameter parameter in parameters)
                    {
                        cmd.Parameters.Add(parameter);
                    }
                }
                GetCommonParasMySql(ref cmd);
                int ret = cmd.ExecuteNonQuery();
                con.Close();
                return ret;
            }
            else
            {
                msg = "不支持的数据库类型";
                return 0;
            }
        }

        public static IDbDataParameter CreateParameter(DatabaseConfig dbconf, string name,object value,out string msg)
        {
            var con = GetConnection(dbconf);
            msg = "";
            DataSet dataSet = new DataSet();
            if (con is SqlConnection)
            {
                return new SqlParameter(name, value);
            }
            else if (con is MySqlConnection)
            {
                return new MySqlParameter(name, value);
            }
            else
            {
                msg = "不支持的数据库类型";
                return null;
            }
        }
    }
}