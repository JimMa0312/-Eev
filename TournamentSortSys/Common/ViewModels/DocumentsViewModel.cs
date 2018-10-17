using DevExpress.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TournamentSortSys.Common.ViewModels
{
    public abstract class DocumentsViewModel<TModule, TUnitOfWork> : ISupportLogicalLayout
        where TModule:ModuleDescription<TModule>
        where TUnitOfWork:IUnitOfWork
    {
        public bool CanSerialize => throw new NotImplementedException();

        public IDocumentManagerService DocumentManagerService => throw new NotImplementedException();

        public IEnumerable<object> LookupViewModels => throw new NotImplementedException();
    }

    public abstract partial class ModuleDescription<TModule>
        where TModule : ModuleDescription<TModule>
    {

    }
}
