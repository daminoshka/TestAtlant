using Infrastructure.DAL.DAL_Core.EF;
using Infrastructure.DAL.DAL_Db.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.DAL.DAL_Core.Interfaces
{
    public interface IDalFactory
    {
        IDetailDal DetailDal { get; }
        IStorekeeperDal StorekeeperDal { get; }

        EntitiesContext DbContext { get; }
    }
}
