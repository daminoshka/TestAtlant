using Infrastructure.BLL.BLL_Core.Factory.Interfaces;
using Infrastructure.BLL.BLL_Core.Repositories.DetailBll.Interfaces;
using Infrastructure.BLL.ViewModel;
using Infrastructure.DAL.DAL_Core.Interfaces;
using Infrastructure.DAL.DAL_Core.Model;
using Infrastructure.DAL.DAL_Core.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace Infrastructure.BLL.BLL_Core.Repositories.DetailBll
{
    public class DetailBll : IDetailBll
    {
        private readonly IDalFactory _dalFactory;
        private readonly IBllFactory _bllFactory;

        public DetailBll(DalFactory dalFactory, IBllFactory bllFactory)
        {
            _dalFactory = dalFactory;
            _bllFactory = bllFactory;
        }

        public List<DetailDto> GetAllDetails(){

            List<Detail> allDetails = _dalFactory.DetailDal.Find(x => x.DateDelete == null).ToList();

            var result = new List<DetailDto>();

            foreach (var item in allDetails)
            {
                result.Add(new DetailDto
                {
                    Id = item.ID,
                    Name = item.Name,
                    Amount = item.Amount,
                    NomCode = item.NomCode,
                    Storekeeper = new StorekeeperDto
                    {
                        Id = item.Storekeeper.ID,
                        Name = item.Storekeeper.FullName,
                        EmployeeNumber = item.Storekeeper.EmployeeNumber,
                    }
                });
            }

            return result;
        }

        public int AddDetail(DetailDto detail)
        {
            var newItem = _dalFactory.DetailDal.AddWithReturn(new Detail
            {
                Name = detail.Name,
                Amount=detail.Amount,
                NomCode=detail.NomCode,
                Storekeeper_ID = detail.Storekeeper.Id,
                DateCreate = DateTime.Now
            });

            return newItem.ID;
        }

        public void DeleteDetail(DetailDto detail)
        {
            Detail oldDetail = _dalFactory.DetailDal.First(x => x.ID == detail.Id);

            oldDetail.DateDelete = DateTime.Now;

            _dalFactory.DetailDal.UpdateVoid(oldDetail, oldDetail.ID);
        }

        public void UpdateDetail(DetailDto detail)
        {
            Detail oldDetail = _dalFactory.DetailDal.First(x => x.ID == detail.Id);

            oldDetail.Name = detail.Name;
            oldDetail.Amount = detail.Amount;
            oldDetail.NomCode = detail.NomCode;
            oldDetail.Storekeeper_ID = detail.Storekeeper.Id;
            oldDetail.DateUpdate = DateTime.Now;

            _dalFactory.DetailDal.UpdateVoid(oldDetail, oldDetail.ID);
        }
    }
}
