using bitocheck24Core.WebApi.Controllers;
using Infrastructure.BLL.BLL_Core.Factory.Interfaces;
using Infrastructure.BLL.Exceptions;
using Infrastructure.BLL.ViewModel;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;

namespace bitocheck24Core.Web.Controllers
{
    
    [Route("api/[controller]")]
    [ApiController]
    public class StorekeeperController : BaseController
    {
        public StorekeeperController(IBllFactory factoryBll) : base(factoryBll)
        {
            _bllFactory = factoryBll;
        }

        [Route("GetAllStorekeepers")]
        [HttpGet]
        public List<StorekeeperDto> GetAllStorekeepers()
        {
            try
            {
                List<StorekeeperDto> result = _bllFactory.StorekeeperBll.GetAllStorekeepers();
                return result;
            }
            catch (Exception ex)
            {
                throw ErrorOwnException.ThrowException("Ошибка при получении списка всех кладовщиков", ex);
            }
        }

        [Route("AddStorekeeper")]
        [HttpPost]
        public int AddStorekeeper(StorekeeperDto storekeeper)
        {
            try
            {
                int result = _bllFactory.StorekeeperBll.AddStorekeeper(storekeeper);
                return result;
            }
            catch (Exception ex)
            {
                throw ErrorOwnException.ThrowException("Ошибка при добавлении кладовщика", ex);
            }
        }

        [Route("UpdateStorekeeper")]
        [HttpPost]
        public IActionResult UpdateStorekeeper(StorekeeperDto storekeeper)
        {
            try
            {
                _bllFactory.StorekeeperBll.UpdateStorekeeper(storekeeper);
                return Ok();
            }
            catch (Exception ex)
            {
                throw ErrorOwnException.ThrowException("Ошибка при обновлении кладовщика", ex);
            }
        }

        [Route("DeleteStorekeeper")]
        [HttpPost]
        public IActionResult DeleteStorekeeper(StorekeeperDto storekeeper)
        {
            try
            {
                _bllFactory.StorekeeperBll.DeleteStorekeeper(storekeeper);
                return Ok();
            }
            catch (Exception ex)
            {
                throw ErrorOwnException.ThrowException("Ошибка при удалении кладовщика", ex);
            }
        }

        [Route("GetDetailCount")]
        [HttpGet]
        public int GetDetailCount(int storekeeperId)
        {
            try
            {
                int result = _bllFactory.StorekeeperBll.GetDetailCount(storekeeperId);
                return result;
            }
            catch (Exception ex)
            {
                throw ErrorOwnException.ThrowException("Ошибка при получении количества деталей кладовщика", ex);
            }
        }
    }
}
