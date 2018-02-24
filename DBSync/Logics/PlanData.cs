using System;
using System.Data;

namespace DBSync.Logics
{
    public class PlanData
    {
        public int ID { get; set; }

        public string Name { get; set; }

        public PlanDateModel PlanDateModel { get; set; }

        public DateTime PlanDate { get; set; }

        public int PlanDateStep { get; set; }

        public PlanTimeModel PlanTimeModel { get; set; }

        public DateTime PlanTime { get; set; }

        public int PlanTimeStep { get; set; }

        public bool Enable { get; set; }

        public int PlanDayStep { get; set; }

        public int PlanWeek { get; set; }

        private PlanData()
        {
            Name = "新计划";
            PlanDateModel = PlanDateModel.执行一次;
            PlanDate = DateTime.Now.Date;
            PlanDateStep = 0;
            PlanTimeModel = PlanTimeModel.每天;
            PlanTime=DateTime.Now;
            PlanTimeStep = 1;
            Enable = true;
            PlanDayStep = 0;
            PlanWeek = 0;
        }

        public static PlanData Create()=>new PlanData();

        public PlanData(DataRow dr):this()
        {
            ID = (int) dr["ID"];
            Name = dr["Name"].ToString();
            Enable = (bool)dr["Enable"];
            PlanDateModel = (PlanDateModel)(int)(dr["PlanDateModel"]);
            PlanDate = (DateTime)dr["PlanDate"];
            PlanTime = (DateTime)dr["PlanTime"];
            PlanTimeModel = (PlanTimeModel)(int)(dr["PlanTimeModel"]);
            PlanDayStep = (int)dr["PlanDayStep"];
            PlanTime = (DateTime) dr["PlanTime"];
            PlanTimeStep = (int)dr["PlanTimeStep"];
            PlanWeek = (int)dr["PlanWeek"];

        }
    }
}