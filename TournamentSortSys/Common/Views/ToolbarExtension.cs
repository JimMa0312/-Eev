using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace TournamentSortSys.Common.Views
{
    internal static class ToolbarExtension
    {
        public static void BindCommandAndImage<TViewModel>(this DevExpress.Utils.MVVM.MVVMContextFluentAPI<TViewModel> fluentAPI, DevExpress.XtraEditors.ButtonPanel.IBaseButton button, Expression<Action<TViewModel>> commandSelector, string imageName=null)
            where TViewModel:class
        {

        }
    }
}
