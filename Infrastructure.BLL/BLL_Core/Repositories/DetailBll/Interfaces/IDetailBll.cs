using Infrastructure.BLL.ViewModel;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.BLL.BLL_Core.Repositories.DetailBll.Interfaces
{
    public interface IDetailBll
    {
        /// <summary>
        /// Получить всех контрагентов
        /// </summary>
        /// <returns>Список отраслей страхования</returns>
        List<DetailDto> GetAllDetails();

        int AddDetail(DetailDto detail);
        void UpdateDetail(DetailDto detail);
        void DeleteDetail(DetailDto detail);
    }
}
