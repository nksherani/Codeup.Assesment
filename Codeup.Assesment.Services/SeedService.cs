using Codeup.Assesment.Data;
using Codeup.Assesment.Data.Repository;
using Codeup.Assesment.Entities;
using Codeup.Assesment.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Codeup.Assesment.Services
{
    public class SeedService : ISeedService
    {
        private readonly OrdersDbContext _dbContext;
        private readonly IRepository<Country> _countriesRepository;
        private readonly IRepository<User> _usersRepository;
        private readonly IRepository<Product> _productsRepository;
        private readonly IRepository<Merchant> _merchantsRepository;
        private readonly IRepository<MerchantPeriod> _merchantPeriodsRepository;
        private readonly IRepository<Order> _ordersRepository;
        private readonly IRepository<OrderItem> _orderItemsRepository;

        public SeedService(
            OrdersDbContext dbContext,
            IRepository<Country> countriesRepository,
            IRepository<User> usersRepository,
            IRepository<Product> productsRepository,
            IRepository<Merchant> merchantsRepository,
            IRepository<MerchantPeriod> merchantPeriodsRepository,
            IRepository<Order> ordersRepository,
            IRepository<OrderItem>orderItemsRepository
            )
        {
            this._dbContext = dbContext;
            this._countriesRepository = countriesRepository;
            this._usersRepository = usersRepository;
            this._productsRepository = productsRepository;
            this._merchantsRepository = merchantsRepository;
            this._merchantPeriodsRepository = merchantPeriodsRepository;
            this._ordersRepository = ordersRepository;
            this._orderItemsRepository = orderItemsRepository;
        }


        public async Task SeedAsync()
        {
            

            using (var transaction = _dbContext.Database.BeginTransaction())
            {
                try
                {
                    Country country = await _countriesRepository.InsertAsync(new Country() { ContinentName = "North America", Name = "USA" });
                    await _dbContext.SaveChangesAsync();
                    User user = await _usersRepository.InsertAsync(new User() { FullName = "Naveed", CountryCode = country.Code });
                    await _dbContext.SaveChangesAsync();

                    Merchant merchant = await _merchantsRepository.InsertAsync(new Merchant() { MerchantName = "Naveed", CountryCode = country.Code, AdminId = user.Id });
                    await _dbContext.SaveChangesAsync();

                    Product product1 = await _productsRepository.InsertAsync(new Product() { MerchantId = merchant.Id, Name = "Olive", Price = 20, Status = ProductStatus.Active });
                    Product product2 = await _productsRepository.InsertAsync(new Product() { MerchantId = merchant.Id, Name = "Canola", Price = 5, Status = ProductStatus.Active });
                    await _dbContext.SaveChangesAsync();



                    Order newOrder1 = new Order() { Status = "NEW", UserId = user.Id };
                    List<OrderItem> orderItems1 = new List<OrderItem>();
                    orderItems1.Add(new OrderItem() { OrderId = newOrder1.Id, ProductId = product1.Id, Quantity = 1 });
                    orderItems1.Add(new OrderItem() { OrderId = newOrder1.Id, ProductId = product2.Id, Quantity = 2 });
                    newOrder1.OrderItems = orderItems1;

                    Order newOrder2 = new Order() { Status = "NEW", UserId = user.Id };
                    List<OrderItem> orderItems2 = new List<OrderItem>();
                    orderItems2.Add(new OrderItem() { OrderId = newOrder2.Id, ProductId = product1.Id, Quantity = 1 });
                    orderItems2.Add(new OrderItem() { OrderId = newOrder2.Id, ProductId = product2.Id, Quantity = 2 });
                    newOrder2.OrderItems = orderItems2;

                    Order order1 = await _ordersRepository.InsertAsync(newOrder1);
                    Order order2 = await _ordersRepository.InsertAsync(newOrder2);

                    await _dbContext.SaveChangesAsync();

                    transaction.Commit();
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    throw;
                }
            }
            

        }
    }
}
