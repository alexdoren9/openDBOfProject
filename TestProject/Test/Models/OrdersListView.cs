using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Test.Models;

namespace Test.Models
{
    public class OrdersListView
    {
        public IEnumerable<Order> Orders { get; set; }
        public SelectList Clients { get; set; }
    }
}