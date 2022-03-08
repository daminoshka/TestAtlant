using Infrastructure.BLL.BLL_Core.Factory.Interfaces;
using Infrastructure.BLL.BLL_Core.Repositories.DetailBll.Interfaces;
using Infrastructure.BLL.ViewModel;
using Infrastructure.DAL.DAL_Core.Interfaces;
using Infrastructure.DAL.DAL_Core.Model;
using Infrastructure.DAL.DAL_Core.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Infrastructure.BLL.BLL_Core.Repositories.DetailBll
{
    public class StorekeeperBll : IStorekeeperBll
    {
        private readonly IDalFactory _dalFactory;
        private readonly IBllFactory _bllFactory;

        public StorekeeperBll(DalFactory dalFactory, IBllFactory bllFactory)
        {
            _dalFactory = dalFactory;
            _bllFactory = bllFactory;
        }

        public List<StorekeeperDto> GetAllStorekeepers(){

            List<Storekeeper> allStorekeepers = _dalFactory.StorekeeperDal.Find(x => x.DateDelete == null).ToList();

            var result = new List<StorekeeperDto>();

            foreach (var item in allStorekeepers)
            {
                result.Add(new StorekeeperDto
                {
                    Id = item.ID,
                    Firstname = item.Firstname,
                    Surname = item.Surname,
                    Lastname = item.Lastname,
                    Name = item.FullName,
                    EmployeeNumber = item.EmployeeNumber,
                });
            }

            return result;
        }

        public int AddStorekeeper(StorekeeperDto storekeeper)
        {
            var newItem = _dalFactory.StorekeeperDal.AddWithReturn(new Storekeeper
            {
                Firstname = storekeeper.Firstname,
                Surname = storekeeper.Surname,
                Lastname = storekeeper.Lastname,
                EmployeeNumber = storekeeper.EmployeeNumber,
                DateUpdate = DateTime.Now
            });

            return newItem.ID;
        }

        public void DeleteStorekeeper(StorekeeperDto storekeeper)
        {
            Storekeeper oldStorekeeper = _dalFactory.StorekeeperDal.First(x => x.ID == storekeeper.Id);

            oldStorekeeper.DateDelete = DateTime.Now;

            _dalFactory.StorekeeperDal.UpdateVoid(oldStorekeeper, oldStorekeeper.ID);
        }

        public void UpdateStorekeeper(StorekeeperDto storekeeper)
        {
            Storekeeper oldStorekeeper = _dalFactory.StorekeeperDal.First(x => x.ID == storekeeper.Id);

            oldStorekeeper.Firstname = storekeeper.Firstname;
            oldStorekeeper.Surname = storekeeper.Surname;
            oldStorekeeper.Lastname = storekeeper.Lastname;
            oldStorekeeper.EmployeeNumber = storekeeper.EmployeeNumber;
            oldStorekeeper.DateUpdate = DateTime.Now;

            _dalFactory.StorekeeperDal.UpdateVoid(oldStorekeeper, oldStorekeeper.ID);
        }

        public int GetDetailCount(int storekeeperId)
        {
            Storekeeper oldStorekeeper = _dalFactory.StorekeeperDal.First(x => x.ID == storekeeperId);

            List<Detail> allDetails = _dalFactory.DetailDal.Find(x => x.Storekeeper_ID == storekeeperId).ToList();

            return allDetails.Count;
        }
    }
}
