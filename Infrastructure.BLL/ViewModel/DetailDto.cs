using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.BLL.ViewModel
{
    /// <summary>
    /// Модель Detail для представления
    /// </summary>
    public class DetailDto
    {
        public int Id { get; set; }
        public string NomCode { get; set; }
        /// <summary>
        /// Наименование
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// Количество
        /// </summary>
        public decimal Amount { get; set; }
        public virtual StorekeeperDto Storekeeper { get; set; }
    }
}
