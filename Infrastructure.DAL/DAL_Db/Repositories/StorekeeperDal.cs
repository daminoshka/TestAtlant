using Infrastructure.DAL.DAL_Core.Model;
using Infrastructure.DAL.DAL_Core.Interfaces;
using Infrastructure.DAL.DAL_Db.Interfaces;
using Infrastructure.DAL.GenericRepository;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.DAL.DAL_Db.Repositories
{
    public class StorekeeperDal : GenericRepository<Storekeeper>, IStorekeeperDal
    {
        public StorekeeperDal (IDbFactory dbFactory) : base(dbFactory)
        {

        }
    }
}
