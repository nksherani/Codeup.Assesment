using Codeup.Assesment.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Codeup.Assesment.Services.Interfaces
{
    public interface IOrderManagementService
    {
        Task<GetOrderDto> CreateOrder(CreateOrderDto dto);
        Task<IEnumerable<GetOrderDto>> GetAllOrders();
        Task<GetOrderDto> GetOrderById(int orderId);
        Task RemoveOrder(int orderId);
        Task UpdateOrder(int orderId, UpdateOrderDto dto);
    }
}
