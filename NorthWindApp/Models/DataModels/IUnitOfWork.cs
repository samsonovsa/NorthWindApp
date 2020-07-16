using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NorthWindApp.Models.DataModels
{
    interface IUnitOfWork
    {
        void Save();
        void Begin();
        void Commit();
        void Rollback();
    }
}
