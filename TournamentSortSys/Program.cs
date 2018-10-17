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
            MainForm = new MainForm() { Icon = AppIcon };

            if(Program.IsTable)
            {
                SetupAsTablet();
            }

            Thread.Sleep(3000);
            Application.Run(MainForm);

        }

        static bool? isTable = null;

        public static bool IsTable
        {
            get
            {
                if(isTable==null)
                {
                    isTable = TournamentSortSys.Common.DeviceDetector.IsTablet;
                }

                return isTable.Value;
            }
        }

        public static Icon AppIcon { get { return DevExpress.Utils.ResourceImageHelper.CreateIconFromResourcesEx("TournamentSortSys.asset.system.icon", typeof(MainForm).Assembly); } }
        public static MainForm MainForm { get; private set; }
        public static void SetupAsTablet()
        {
            MainForm.ShowTitleNavPane();
            MainForm.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            MainForm.WindowState = FormWindowState.Maximized;
            DevExpress.XtraEditors.WindowsFormsSettings.PopupMenuStyle = DevExpress.XtraEditors.Controls.PopupMenuStyle.RadialMenu;
            DevExpress.Utils.TouchHelpers.TouchKeyboardSupport.EnableTouchKeyboard = true;
        }

    }
}
