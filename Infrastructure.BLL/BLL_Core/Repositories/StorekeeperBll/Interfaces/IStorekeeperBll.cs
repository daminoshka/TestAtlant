using Infrastructure.BLL.ViewModel;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.BLL.BLL_Core.Repositories.DetailBll.Interfaces
{
    public interface IStorekeeperBll
    {
        List<StorekeeperDto> GetAllStorekeepers();

        int AddStorekeeper(StorekeeperDto storekeeper);
        void UpdateStorekeeper(StorekeeperDto storekeeper);
        void DeleteStorekeeper(StorekeeperDto storekeeper);
        int GetDetailCount(int storekeeperId);
    }
}
