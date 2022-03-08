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
    public class DetailController : BaseController
    {
        public DetailController(IBllFactory factoryBll) : base(factoryBll)
        {
            _bllFactory = factoryBll;
        }

        [Route("GetAllDetails")]
        [HttpGet]
        public List<DetailDto> GetAllDetails()
        {
            try
            {
                List<DetailDto> result = _bllFactory.DetailBll.GetAllDetails();
                return result;
            }
            catch (Exception ex)
            {
                throw ErrorOwnException.ThrowException("Ошибка при получении списка всех деталей", ex);
            }
        }

        [Route("AddDetail")]
        [HttpPost]
        public int AddDetail(DetailDto detail)
        {
            try
            {
                int result = _bllFactory.DetailBll.AddDetail(detail);
                return result;
            }
            catch (Exception ex)
            {
                throw ErrorOwnException.ThrowException("Ошибка при добавлении детали", ex);
            }
        }

        [Route("UpdateDetail")]
        [HttpPost]
        public IActionResult UpdateDetail(DetailDto detail)
        {
            try
            {
                _bllFactory.DetailBll.UpdateDetail(detail);
                return Ok();
            }
            catch (Exception ex)
            {
                throw ErrorOwnException.ThrowException("Ошибка при обновлении детали", ex);
            }
        }

        [Route("DeleteDetail")]
        [HttpPost]
        public IActionResult DeleteDetail(DetailDto detail)
        {
            try
            {
                _bllFactory.DetailBll.DeleteDetail(detail);
                return Ok();
            }
            catch (Exception ex)
            {
                throw ErrorOwnException.ThrowException("Ошибка при удалении детали", ex);
            }
        }
    }
}
