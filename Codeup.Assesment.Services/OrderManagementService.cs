using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Codeup.Assesment.Data;
using Codeup.Assesment.Data.Repository;
using Codeup.Assesment.DTOs;
using Codeup.Assesment.Entities;
using Codeup.Assesment.Services.Common;
using Codeup.Assesment.Services.Interfaces;

namespace Codeup.Assesment.Services
{
    public class OrderManagementService : IOrderManagementService
    {
        private readonly OrdersDbContext _dbContext;
        private readonly IRepository<Country> _countriesRepository;
        private readonly IRepository<User> _usersRepository;
        private readonly IRepository<Product> _productsRepository;
        private readonly IRepository<Merchant> _merchantsRepository;
        private readonly IRepository<MerchantPeriod> _merchantPeriodsRepository;
        private readonly IRepository<Order> _ordersRepository;
        private readonly IRepository<OrderItem> _orderItemsRepository;

        public OrderManagementService(
            OrdersDbContext dbContext,
            IRepository<Country> countriesRepository,
            IRepository<User> usersRepository,
            IRepository<Product> productsRepository,
            IRepository<Merchant> merchantsRepository,
            IRepository<MerchantPeriod> merchantPeriodsRepository,
            IRepository<Order> ordersRepository,
            IRepository<OrderItem> orderItemsRepository
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

        public async Task<IEnumerable<GetOrderDto>> GetAllOrders()
        {
            IEnumerable<Order> orders = await _ordersRepository.GetAllAsync();
            if (orders == null)
                throw new NotFoundException();
            return orders.ToDto();
        }
        public async Task<GetOrderDto> GetOrderById(int orderId)
        {
            Order order = await _ordersRepository.GetByIdAsync(orderId);
            if (order == null)
                throw new NotFoundException();
            return order.ToDto();
        }
        public async Task<GetOrderDto> CreateOrder(CreateOrderDto dto)
        {
            Order order = await _ordersRepository.InsertAsync(dto.ToEntity());
            await _dbContext.SaveChangesAsync();
            return order.ToDto();
        }
        public async Task UpdateOrder(int orderId, UpdateOrderDto dto)
        {
            Order existingOrder = await _ordersRepository.GetByIdAsync(orderId);
            if (existingOrder == null)
                throw new NotFoundException();
            List<int> updatedProducts = new List<int>();
            foreach (var item in dto.OrderItems)
            {
                updatedProducts.Add(item.ProductId);
                OrderItem existingItem = existingOrder.OrderItems.FirstOrDefault(x => x.ProductId == item.ProductId);
                if(existingItem != null)
                {
                    existingItem.Quantity = item.Quantity;
                }
                else
                {
                    existingOrder.OrderItems = existingOrder.OrderItems.Append( new OrderItem() { OrderId = orderId, ProductId = item.ProductId, Quantity = item.Quantity } ).ToList();
                }
            }
            existingOrder.OrderItems = existingOrder.OrderItems.Where(x=>x.Quantity > 0 && updatedProducts.Contains(x.ProductId)).ToList();
            _ordersRepository.Update(existingOrder);
            await _dbContext.SaveChangesAsync();
        }
        public async Task RemoveOrder(int orderId)
        {
            Order existingOrder = await _ordersRepository.GetByIdAsync(orderId);
            if (existingOrder == null)
                throw new NotFoundException();
            _ordersRepository.Delete(orderId);
            await _dbContext.SaveChangesAsync();
        }
    }
}
