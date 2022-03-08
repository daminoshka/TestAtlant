using Infrastructure.BLL.BLL_Core.Factory.Interfaces;
using Infrastructure.BLL.BLL_Core.Repositories.DetailBll;
using Infrastructure.BLL.BLL_Core.Repositories.DetailBll.Interfaces;
using Infrastructure.DAL.DAL_Core.Repositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.BLL.BLL_Core.Factory
{
    public class BllFactory : IBllFactory
    {
        private readonly DalFactory _dalFactory;

        private IDetailBll _detailBll;
        private IStorekeeperBll _storekeeperBll;

        public BllFactory()
        {
            _dalFactory = new DalFactory(new DbFactory());
        }

        public IDetailBll DetailBll
        {
            get { return _detailBll ?? (_detailBll = new DetailBll(_dalFactory, this)); }
        }

        public IStorekeeperBll StorekeeperBll
        {
            get { return _storekeeperBll ?? (_storekeeperBll = new StorekeeperBll(_dalFactory, this)); }
        }
    }
}
