using DevExpress.XtraSplashScreen;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace TournamentSortSys
{
    public partial class Form1 : DevExpress.XtraBars.Ribbon.RibbonForm
    {

        private wenke frm_wenke = null;
    
        public Form1()
        {
            InitializeComponent();
        }
        protected override CreateParams CreateParams
        {
            get
            {
                CreateParams cp = base.CreateParams;
                cp.ExStyle |= 0x02000000;  // Turn on WS_EX_COMPOSITED  
                if (this.IsXpOr2003 == true)
                {
                    cp.ExStyle |= 0x00080000;
                    // Turn on WS_EX_LAYERED   
                    this.Opacity = 1;
                }
                //最大化窗口
                int WS_CAPTION = 0xC00000;
                int WS_BORDER = 0x800000;
                cp.Style &= ~WS_CAPTION | WS_BORDER;
                return cp;
            }
        }  //防止闪烁    
        private Boolean IsXpOr2003
        {
            get
            {
                OperatingSystem os = Environment.OSVersion;
                Version vs = os.Version;
                if (os.Platform == PlatformID.Win32NT)
                    if ((vs.Major == 5) && (vs.Minor != 0))
                        return true;
                    else
                        return false;
                else
                    return false;
            }
        }




        private void Form1_Load(object sender, EventArgs e)
        {
            SplashScreenManager.CloseForm(true);
        }

        private void backstageViewTabItem5_SelectedChanged(object sender, DevExpress.XtraBars.Ribbon.BackstageViewItemEventArgs e)
        {

        }

        private void backstageViewTabItem2_SelectedChanged(object sender, DevExpress.XtraBars.Ribbon.BackstageViewItemEventArgs e)
        {
     
        }

        private void backstageViewTabItem4_SelectedChanged(object sender, DevExpress.XtraBars.Ribbon.BackstageViewItemEventArgs e)
        {

        }

        private void barEditItem1_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {

        }

        private void ribbonControl1_Click(object sender, EventArgs e)
        {

        }

        private void backstageViewControl1_Click(object sender, EventArgs e)
        {

        }

        private void ribbonControl1_Click_1(object sender, EventArgs e)
        {

        }

        private void backstageViewTabItem1_SelectedChanged(object sender, DevExpress.XtraBars.Ribbon.BackstageViewItemEventArgs e)
        {
        
        }
    }
}
