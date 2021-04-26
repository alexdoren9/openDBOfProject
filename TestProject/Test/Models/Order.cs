using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace Test.Models
{
    public class Order
    {
        [Key]
        public int Id { get; set; } //ID заказа - первичный ключ
        public int? ClientId { get; set; } //ID клиента для заказа
        [Required(ErrorMessage = "Введите уникальный номер заказа")]
        [RegularExpression (@"^\d+", ErrorMessage = "Введите корректный номер заказа")]
        public int Number { get; set; } //Уникальный идентификационный номер заказа
        [Required(ErrorMessage = "Введите описание заказа")]
        [StringLength(100, MinimumLength =5, ErrorMessage ="Введите описание от 5 до 100 символов")]
        public string Description { get; set; } //Описание заказа
        public virtual Client Client { get; set; } //Поле необходимое для создания связи один-ко-многим
    }
}