using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBSync.LogicClasses
{
    public class PlanDataItem
    {
        public int PlanDataID { get; set; }

        public int PlanID { get; set; }

        public string PlanDataName{ get; set; }

        public string PlanSql{ get; set; }

        public FailMode FailMode{ get; set; }

        public int FailModeInt
        {
            get => (int)FailMode;
            set => FailMode = (FailMode)value;
        }

        public int Index{ get; set; }

        private PlanDataItem()
        {
            PlanDataID = 0;
            PlanID = 0;
            PlanDataName = "";
            PlanSql = "";
            FailMode = FailMode.退出执行;
            Index = 0;
        }

        public static PlanDataItem Create()=>new PlanDataItem();

        public PlanDataItem(DataRow dr):this()
        {
            PlanDataID = (int) dr["PlanDataID"];
            PlanID = (int) dr["PlanID"];
            PlanDataName = dr["PlanDataName"].ToString();
            PlanSql = dr["PlanSql"].ToString();
            FailMode = (FailMode)(int)(dr["FailMode"]);
            Index = (int)dr["Index"];
        }
    }
}
