using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.BLL.ViewModel
{
    /// <summary>
    /// Модель для представления Storekeeper (Кладовщик)
    /// </summary>
    public class StorekeeperDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        /// <summary>
        /// Табельный номер
        /// </summary>
        public string EmployeeNumber { get; set; }
        public string Firstname { get; set; }
        public string Surname { get; set; }
        public string Lastname { get; set; }
    }
}
