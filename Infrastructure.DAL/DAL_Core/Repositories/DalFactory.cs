
using Infrastructure.DAL.DAL_Core.EF;
using Infrastructure.DAL.DAL_Core.Interfaces;
using Infrastructure.DAL.DAL_Db.Interfaces;
using Infrastructure.DAL.DAL_Db.Repositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.DAL.DAL_Core.Repositories
{
    public class DalFactory : IDalFactory
    {
        private EntitiesContext _dbContext;
        private readonly IDbFactory _dbFactory;

        IDetailDal _detailDal;
        IStorekeeperDal _storekeeperDal;


        public DalFactory(IDbFactory dbFactory)
        {
            _dbFactory = dbFactory;
        }

        public IDetailDal DetailDal
        {
            get { return _detailDal ?? (_detailDal = new DetailDal(_dbFactory)); }
        }
        public IStorekeeperDal StorekeeperDal
        {
            get { return _storekeeperDal ?? (_storekeeperDal = new StorekeeperDal(_dbFactory)); }
        }



        public EntitiesContext DbContext => throw new NotImplementedException();
    }
}
