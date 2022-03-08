using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.BLL
{
    public static class Helper
    {
        public enum CryptoWalletTypes
        {
            [Description("BTC")]
            BTC = 1,
            //    [Description("Сырье и материалы")]
            //Materials,
            //    [Description("Ипотека")]
            //Mortgage,
            //    [Description("Оборудование")]
            //Equipment,
            //    [Description("Имущественные права")]
            //PropertyRights
        }

        public enum CURRENCY_PAIRS
        {
            [Description("BTC_USD")]
            BTC_USD = 1,
            [Description("USD_BTC")]
            USD_BTC = 4
            //    [Description("Сырье и материалы")]
            //Materials,
            //    [Description("Ипотека")]
            //Mortgage,
            //    [Description("Оборудование")]
            //Equipment,
            //    [Description("Имущественные права")]
            //PropertyRights
        }      

        public enum STATUS_ID
        {
            [Description("Created")]
            Created = 1,
            [Description("Closed paid")]
            Closed_Paid = 2,
            [Description("Closed not paid")]
            Closed_not_paid = 3,
            [Description("Paid")]
            Paid = 4,
            [Description("Not paid")]
            Not_paid = 5,
            [Description("Banned")]
            Banned = 6,
        }        
    }

    public static class Helper_Functions
    {
        /// <summary>
        /// Получение описание по значению enum
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string GetDescriptionString(Enum value)
        {
            DescriptionAttribute[] attributes = (DescriptionAttribute[])value.GetType().GetField(value.ToString()).GetCustomAttributes(typeof(DescriptionAttribute), false);
            return attributes.Length > 0 ? attributes[0].Description : string.Empty;
        }

        /// <summary>
        /// Присваивание ID в БД значения null когда свойство объекта Id равно 0
        /// </summary>
        /// <param name="val"></param>
        /// <returns></returns>
        public static decimal? GetNullableDecimalValue(this decimal val)
        {
            return val == 0 ? null : (decimal?)val;
        }
    }
}
