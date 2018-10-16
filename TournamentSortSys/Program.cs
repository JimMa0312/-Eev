using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using DevExpress.UserSkins;
using DevExpress.Skins;
using DevExpress.XtraSplashScreen;
using DevExpress.LookAndFeel;
using System.Threading;
using System.Drawing;

namespace TournamentSortSys
{

    static class Program
    {

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            //界面汉化
            System.Threading.Thread.CurrentThread.CurrentUICulture = new System.Globalization.CultureInfo("zh-Hans");

            BonusSkins.Register();
            SkinManager.EnableFormSkins();

            SplashScreenManager.ShowForm(typeof(SplashScreen1));
            Thread.Sleep(3000);
            Application.Run(new MainForm() { Icon = AppIcon });
        }

        static bool? isTable = null;

        public static bool IsTable
        {
            get
            {
                if(isTable==null)
                {
                    
                }

                return false;
            }
        }

        public static Icon AppIcon { get { return DevExpress.Utils.ResourceImageHelper.CreateIconFromResourcesEx("TournamentSortSys.asset.system.icon", typeof(MainForm).Assembly); } }
        public static MainForm MainForm { get; private set; }
        public static void SetupAsTablet()
        {
        }

    }
}
