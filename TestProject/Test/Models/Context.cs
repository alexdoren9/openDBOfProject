using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MySql.Data.Entity; //Установлено из NuGet
using System.Data.Entity; //Автоматически добавлена при установке MySql.Data.Entity


namespace Test.Models
{
    public class Context : DbContext //Наследование от класса DbContext для создания контекста
    {
        public DbSet<Order> Orders { get; set; } //Получение из БД набора объектов Order
        public DbSet<Client> Clients { get; set; } //Получение из БД набора объектов Customer
    }
}