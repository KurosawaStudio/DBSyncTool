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
               
                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                con.Open();
                sda.Fill(dataSet);
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
                MySqlDataAdapter mda=new MySqlDataAdapter(cmd);
                con.Open();
                mda.Fill(dataSet);
                return dataSet;
            }
            else
            {
                msg = "不支持的数据库类型";
                return null;
            }
        }
    }
}