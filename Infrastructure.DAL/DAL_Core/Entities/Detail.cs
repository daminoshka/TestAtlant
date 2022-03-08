using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Infrastructure.DAL.DAL_Core.Model
{
    /// <summary>
    /// Детали
    /// </summary>
    public class Detail
    {
        public int ID { get; set; }
        /// <summary>
        /// Номенклатурный код
        /// </summary>
        public string NomCode { get; set; }
        /// <summary>
        /// Наименование
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// Количество
        /// </summary>
        public decimal Amount { get; set; }
        public int Storekeeper_ID { get; set; }
        [ForeignKey("Storekeeper_ID")]
        public virtual Storekeeper Storekeeper { get; set; }
        public DateTime DateCreate { get; set; }
        public DateTime? DateUpdate { get; set; }
        public DateTime? DateDelete { get; set; }
    }
}
