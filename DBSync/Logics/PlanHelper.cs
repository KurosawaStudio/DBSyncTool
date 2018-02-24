using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlServerCe;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Serialization;

namespace DBSync.Logics
{
    public class PlanHelper
    {
        private static readonly string PlanDatabasePath = $@"{Program.basePath}\Plan.sdf";
        private static readonly string ConnectionString = $@"Data Source={PlanDatabasePath}";
        private SqlCeConnection con;

        private PlanHelper()
        {
            InitConnection();
        }

        public static PlanHelper Create()=>new PlanHelper();

        private void InitConnection()
        {
            con = new SqlCeConnection(ConnectionString);
            con.InfoMessage += InfoMessage;
        }

        private void InfoMessage(object sender, SqlCeInfoMessageEventArgs e)
        {
            
        }

        private void OpenConnection() => con.Open();

        private void CloseConnection() => con.Close();

        public List<PlanData> GetPlanData()
        {
            List<PlanData> list = new List<PlanData>();
            DataSet ds=new DataSet();
            try
            {
                OpenConnection();
                SqlCeCommand cmd = new SqlCeCommand("select * from [Plan] order by ID asc", con);
                SqlCeDataAdapter da=new SqlCeDataAdapter(cmd);
                ds=new DataSet();
                da.Fill(ds);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    list.Add(new PlanData(dr));
                }
            }
            catch(Exception ex)
            {
                //Suppress All Exceptions
            }
            finally
            {
                CloseConnection();
            }

            return list;
        }

        public void UpdatePlanData(PlanData data)
        {
            try
            {
                OpenConnection();
                SqlCeCommand cmd = new SqlCeCommand(@"UPDATE [Plan] 
                       SET [Name] = @Name
                          ,[PlanDateModel] = @PlanDateModel
                          ,[PlanDate] = @PlanDate
                          ,[PlanDateStep] = @PlanDateStep
                          ,[PlanTimeModel] = @PlanTimeModel
                          ,[PlanTime] = @PlanTime
                          ,[PlanTimeStep] = @PlanTimeStep
                          ,[Enable] = @Enable
                          ,[PlanDayStep] = @PlanDayStep
                          ,[PlanWeek] = @PlanWeek
                     WHERE ID=@ID", con);
                cmd.Parameters.AddWithValue("ID", data.ID);
                cmd.Parameters.AddWithValue("Name", data.Name);
                cmd.Parameters.AddWithValue("PlanDateModel", (int)data.PlanDateModel);
                cmd.Parameters.AddWithValue("PlanDate", data.PlanDate);
                cmd.Parameters.AddWithValue("PlanDateStep", data.PlanDateStep);
                cmd.Parameters.AddWithValue("PlanTimeModel", (int)data.PlanTimeModel);
                cmd.Parameters.AddWithValue("PlanTime", data.PlanTime);
                cmd.Parameters.AddWithValue("PlanTimeStep", data.PlanTimeStep);
                cmd.Parameters.AddWithValue("Enable", data.Enable);
                cmd.Parameters.AddWithValue("PlanDayStep", data.PlanDayStep);
                cmd.Parameters.AddWithValue("PlanWeek", data.PlanWeek);

                cmd.ExecuteNonQuery();
            }
            catch(Exception ex)
            {
                //Suppress All Exceptions
            }
            finally
            {
                CloseConnection();
            }
        }

        public void AddPlanData(PlanData data)
        {
            try
            {
                OpenConnection();
                SqlCeCommand cmd = new SqlCeCommand(@"INSERT INTO [Plan]
                ([Name],[PlanDateModel],[PlanDate],[PlanDateStep],[PlanTimeModel],[PlanTime],
                [PlanTimeStep],[Enable],[PlanDayStep],[PlanWeek])
                VALUES(@Name,@PlanDateModel,@PlanDate,
                @PlanDateStep,@PlanTimeModel,@PlanTime,@PlanTimeStep,
                @Enable,@PlanDayStep,@PlanWeek);", con);
                cmd.Parameters.AddWithValue("Name", data.Name);
                cmd.Parameters.AddWithValue("PlanDateModel", (int)data.PlanDateModel);
                cmd.Parameters.AddWithValue("PlanDate", data.PlanDate);
                cmd.Parameters.AddWithValue("PlanDateStep", data.PlanDateStep);
                cmd.Parameters.AddWithValue("PlanTimeModel", (int)data.PlanTimeModel);
                cmd.Parameters.AddWithValue("PlanTime", data.PlanTime);
                cmd.Parameters.AddWithValue("PlanTimeStep", data.PlanTimeStep);
                cmd.Parameters.AddWithValue("Enable", data.Enable);
                cmd.Parameters.AddWithValue("PlanDayStep", data.PlanDayStep);
                cmd.Parameters.AddWithValue("PlanWeek", data.PlanWeek);

                cmd.ExecuteNonQuery();
            }
            catch(Exception ex)
            {
                //Suppress All Exceptions
            }
            finally
            {
                CloseConnection();
            }
        }

        public void DeletePlanData(PlanData data)
        {
            try
            {
                OpenConnection();
                SqlCeCommand cmd = new SqlCeCommand(@"DELETE FROM [Plan] WHERE [ID]=@ID;", con);
                cmd.Parameters.AddWithValue("ID", data.ID);

                cmd.ExecuteNonQuery();
            }
            catch(Exception ex)
            {
                //Suppress All Exceptions
            }
            finally
            {
                CloseConnection();
            }
        }

        public DataSet GetPlanItemData(int PlanID)
        {
            DataSet ds=new DataSet();
            try
            {
                OpenConnection();
                SqlCeCommand cmd = new SqlCeCommand("select [PlanDataID],[PlanDataName],[PlanSql],[FailMode],[Index]" +
                                                    " from [PlanData] where PlanID=@PlanID order by PlanDataID asc", con);
                cmd.Parameters.AddWithValue("PlanID", PlanID);
                SqlCeDataAdapter da=new SqlCeDataAdapter(cmd);
                ds=new DataSet();
                da.Fill(ds);
                /*foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    list.Add(new PlanData(dr));
                }*/
            }
            catch(Exception ex)
            {
                //Suppress All Exceptions
            }
            finally
            {
                CloseConnection();
            }

            return ds;
        }

        public void AddPlanItemData(PlanDataItem data)
        {
            try
            {
                OpenConnection();
                SqlCeCommand cmd = new SqlCeCommand(@"INSERT INTO [PlanData]
                ([PlanID],[PlanDataName],[PlanSql],[FailMode],[Index])
                VALUES(@PlanID,@PlanDataName,@PlanSql,@FailMode,@Index);", con);
                cmd.Parameters.AddWithValue("PlanID", data.PlanID);
                cmd.Parameters.AddWithValue("PlanDataName", data.PlanDataName);
                cmd.Parameters.AddWithValue("PlanSql", data.PlanSql);
                cmd.Parameters.AddWithValue("FailMode", (int)data.FailMode);
                cmd.Parameters.AddWithValue("Index", data.Index);
                cmd.ExecuteNonQuery();
            }
            catch(Exception ex)
            {
                //Suppress All Exceptions
            }
            finally
            {
                CloseConnection();
            }
        }

        public void DeletePlanItemData(int PlanDataID)
        {
            try
            {
                OpenConnection();
                SqlCeCommand cmd = new SqlCeCommand(@"DELETE FROM [PlanData] WHERE [PlanDataID]=@PlanDataID;", con);
                cmd.Parameters.AddWithValue("PlanDataID", PlanDataID);

                cmd.ExecuteNonQuery();
            }
            catch(Exception ex)
            {
                //Suppress All Exceptions
            }
            finally
            {
                CloseConnection();
            }
        }

        public void UpdatePlanItemData(PlanDataItem data)
        {
            try
            {
                OpenConnection();
                SqlCeCommand cmd = new SqlCeCommand(@"UPDATE [PlanData] 
                   SET [PlanID] = @PlanID
                      ,[PlanDataName] = @PlanDataName
                      ,[PlanSql] = @PlanSql
                      ,[FailMode] = @FailMode
                      ,[Index] = @Index
                 WHERE [PlanDataID]=@PlanDataID;", con);
                cmd.Parameters.AddWithValue("PlanDataID", data.PlanDataID);
                cmd.Parameters.AddWithValue("PlanID", data.PlanID);
                cmd.Parameters.AddWithValue("PlanDataName", data.PlanDataName);
                cmd.Parameters.AddWithValue("PlanSql", data.PlanSql);
                cmd.Parameters.AddWithValue("FailMode", (int)data.FailMode);
                cmd.Parameters.AddWithValue("Index", data.Index);

                cmd.ExecuteNonQuery();
            }
            catch(Exception ex)
            {
                //Suppress All Exceptions
            }
            finally
            {
                CloseConnection();
            }
        }
    }
}
