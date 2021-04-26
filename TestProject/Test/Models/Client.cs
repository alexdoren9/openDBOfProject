using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace Test.Models
{
    public class Client
    {
        [Key]
        public int Id { get; set; } // ID клиента - первичный ключ
        [Required(ErrorMessage = "Введите имя клиента")]
        [StringLength(30, MinimumLength = 1, ErrorMessage = "Введите имя от 1 до 30 символов")]
        public string Name { get; set; } //Название клиента
        [Required(ErrorMessage = "Введите адрес клиента")]
        [StringLength(100, MinimumLength = 3, ErrorMessage = "Введите имя от 3 до 100 символов")]
        public string Address { get; set; } //Адрес клиента
        public bool VIP { get; set; } //Наличие VIP-статуса
        public virtual IEnumerable<Order> Orders { get; set; } //Поле привязки клиента к его заказам
    }
}