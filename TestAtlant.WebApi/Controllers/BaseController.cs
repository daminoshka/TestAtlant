using Infrastructure.BLL.BLL_Core.Factory.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace bitocheck24Core.WebApi.Controllers
{
    [ApiController]
    public abstract class BaseController : ControllerBase
    {
        /// <summary>
        /// </summary>
        protected IBllFactory _bllFactory;
        /// <summary>
        /// </summary>
        /// <param name="factoryBll"></param>
        public BaseController(IBllFactory factoryBll)
        {
            _bllFactory = factoryBll;
        }

    }
}
