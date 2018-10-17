using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TournamentSortSys.Common.ViewModels
{
    public interface IUnitOfWork
    {
        void SaveChanges();
        bool HasChanges();
    }
}
