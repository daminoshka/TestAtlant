using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Infrastructure.DAL.DAL_Core.Model
{
    /// <summary>
    /// Кладовщик
    /// </summary>
    public class Storekeeper
    {
        public int ID { get; set; }
        /// <summary>
        /// Имя кладовщика
        /// </summary>
        public string Firstname{ get; set; }
        /// <summary>
        /// Фамилия кладовщика
        /// </summary>
        public string Surname { get; set; }
        /// <summary>
        /// Отчество кладовщика
        /// </summary>
        public string Lastname { get; set; }
        public string FullName { get => Firstname + " " + Surname + " " + Lastname; }
        /// <summary>
        /// Табельный номер кладовщика
        /// </summary>
        public string EmployeeNumber { get; set; }
        public DateTime DateCreate { get; set; }
        public DateTime? DateUpdate { get; set; }
        public DateTime? DateDelete { get; set; }
    }
}
