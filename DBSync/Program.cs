using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Security.Claims;
using System.Security.Principal;
using System.ServiceProcess;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using DBSync.Logics;
using DBSync.Services;
using Newtonsoft.Json;

namespace DBSync
{
    public static class Program
    {
        public static frmMain MainForm;
        public static string exePath;
        public static string basePath;

        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        public static void Main(string[] args)
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            bool debug = false;
            bool srv = false;
            if (args.Contains("--debug") || args.Contains("-d") || args.Contains("/d"))
            {
                debug=true;
            }
            if (args.Contains("--service") || args.Contains("-k") || args.Contains("/k"))
            {
                srv = true;
            }
            if (!srv)
            {
                Program.exePath = Application.ExecutablePath;
                Program.basePath = Application.StartupPath;
                MainForm = new frmMain(debug);
                Application.Run(MainForm);
            }
            else
            {
                if (args.Contains("install"))
                {
                    DoInstall();
                }
                else if (args.Contains("uninstall"))
                {
                    DoUninstall();
                }
                else
                {
                    ServiceBase service=new ServiceBase();
                    if (args.Contains("DBSyncService"))
                    {
                        service = new DBSyncService();
                    }
                    else
                    {
                        MessageBox.Show($@"没有这个服务:{args[1]??"<NULL>"}");
                        return;
                    }
                    ServiceBase[] serviceToRun = {
                        service
                    };
                    ServiceBase.Run(serviceToRun);
                }

            }
        }

        private static bool CheckIfAdministrator()
        {
            AppDomain myDomain = Thread.GetDomain();
            myDomain.SetPrincipalPolicy(PrincipalPolicy.WindowsPrincipal);

            if (!(Thread.CurrentPrincipal as WindowsPrincipal).IsInRole(WindowsBuiltInRole.Administrator))
            {
                MessageBox.Show(@"请用管理员身份运行");
                return false;
            }
            return true;
        }

        private static void DoInstall()
        {
            if (!CheckIfAdministrator()) return;
            ProcessStartInfo psi=new ProcessStartInfo("cmd.exe");
            psi.UseShellExecute = false;
            psi.CreateNoWindow = true;
            psi.RedirectStandardInput = true;
            Process p=Process.Start(psi);
            p.StandardInput.Write("sc.exe {0}\r\n", $@"create DBSyncService binPath=""{Application.ExecutablePath} -k DBSyncService"" start=auto DisplayName=数据同步服务");
            p.StandardInput.Flush();
            p.StandardInput.Write("exit\r\n");
            p.StandardInput.Flush();
            MessageBox.Show(@"服务安装成功");
        }

        private static void DoUninstall()
        {
            if (!CheckIfAdministrator()) return;
            ProcessStartInfo psi = new ProcessStartInfo("cmd.exe");
            psi.UseShellExecute = false;
            psi.CreateNoWindow = true;
            psi.RedirectStandardInput = true;
            Process p = Process.Start(psi);
            p.StandardInput.Write("sc.exe {0}\r\n", $@"delete DBSyncService");
            p.StandardInput.Flush();
            p.StandardInput.Write("exit\r\n");
            p.StandardInput.Flush();
            MessageBox.Show(@"服务移除成功");
        }
    }
}
