using Infrastructure.DAL.DAL_Db.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using Infrastructure.BLL.BLL_Core.Repositories.DetailBll.Interfaces;

namespace Infrastructure.BLL.BLL_Core.Factory.Interfaces
{
    public interface IBllFactory
    {
        IDetailBll DetailBll { get; }
        IStorekeeperBll StorekeeperBll { get; }
    }
}
