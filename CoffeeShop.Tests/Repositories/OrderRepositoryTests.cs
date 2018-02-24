using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using CoffeeShop.v2.Data;
using CoffeeShop.v2.Helpers;
using CoffeeShop.v2.Models;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace CoffeeShop.Tests.Repositories
{
    public class OrderRepositoryTests
    {
        private readonly IMapper _mapper;
        public OrderRepositoryTests()
        {
            _mapper = new Mapper(new MapperConfiguration(cfg => cfg.AddProfile<AutoMapperProfiles>()));

        }

        private void InitDbContext(DataContext context)
        {
            context.Orders.Add(new Order());
            context.Orders.Add(new Order());
            context.Orders.Add(new Order());
            context.SaveChanges();
        }

        [Fact(DisplayName = "get_all_orders_returns_order_lists")]
        public async Task Test1()
        {
            var dbContextOptions = new DbContextOptionsBuilder<DataContext>();
            dbContextOptions.UseInMemoryDatabase(
               databaseName: "get_all_orders_returns_order_lists");

            var context = new DataContext(dbContextOptions.Options);
            InitDbContext(context);

            var repo = new OrderRepository(context);
            var orders = await repo.GetOrders();

            Assert.NotEmpty(orders);

        }

        [Fact(DisplayName = "get_order_int_id_returns_order")]
        public async Task Test2()
        {
            var dbContextOptions = new DbContextOptionsBuilder<DataContext>();
            dbContextOptions.UseInMemoryDatabase(
               databaseName: "get_order_int_id_returns_order");

            var context = new DataContext(dbContextOptions.Options);
            InitDbContext(context);

            var repo = new OrderRepository(context);
            var allOrders = await repo.GetOrders();
            int[] ids = new int[10];
            int i = 0;
            foreach (var item in allOrders)
            {
                ids[i] = item.Id;
                i++;
            }
            var order = await repo.GetOrder(ids[0]);

            Assert.NotNull(order);
        }
    }
}
