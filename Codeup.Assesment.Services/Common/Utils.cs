using Codeup.Assesment.DTOs;
using Codeup.Assesment.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Codeup.Assesment.Services.Common
{
    public static class Utils
    {
        public static IEnumerable<GetOrderDto> ToDto(this IEnumerable<Order> orders)
        {
            List<GetOrderDto> dtoList = new List<GetOrderDto>();
            foreach (var order in orders)
            {
                dtoList.Add(order.ToDto());
            }
            return dtoList;
        }
        public static GetOrderDto ToDto(this Order order)
        {
            GetOrderDto getOrderDto = new GetOrderDto();
            getOrderDto.Id = order.Id;
            getOrderDto.Status = order.Status;
            getOrderDto.UserId = order.UserId;
            if (order.OrderItems != null)
                getOrderDto.OrderItems = order.OrderItems.ToDto();
            return getOrderDto;
        }
        public static IEnumerable<GetOrderItemsDto> ToDto(this IEnumerable<OrderItem> ordersItems)
        {
            List<GetOrderItemsDto> dtoList = new List<GetOrderItemsDto>();
            foreach (var item in ordersItems)
            {
                dtoList.Add(item.ToDto());
            }
            return dtoList;
        }
        public static GetOrderItemsDto ToDto(this OrderItem ordersItem)
        {
            GetOrderItemsDto dto = new GetOrderItemsDto();
            dto.ProductId = ordersItem.ProductId;
            dto.OrderId = ordersItem.OrderId;
            dto.Quantity = ordersItem.Quantity;
            if (ordersItem.Product != null)
                dto.Product = ordersItem.Product.ToDto();
            return dto;
        }
        public static ProductDto ToDto(this Product product)
        {
            ProductDto dto = new ProductDto();
            dto.Id = product.Id;
            dto.Name = product.Name;
            dto.Price = product.Price;
            dto.Status = (DTOs.ProductStatus)product.Status;
            dto.Price = product.Price;
            dto.CreatedAt = product.CreatedAt;
            if (product.Merchant != null)
                dto.Merchant = product.Merchant.ToDto();
            return dto;
        }
        public static MerchantDto ToDto(this Merchant merchant)
        {
            MerchantDto dto = new MerchantDto();
            dto.Id = merchant.Id;
            dto.MerchantName = merchant.MerchantName;
            dto.CountryCode = merchant.CountryCode;
            dto.CreatedAt = merchant.CreatedAt;
            return dto;
        }

        public static Order ToEntity(this CreateOrderDto dto)
        {
            Order order = new Order();
            order.Status = "NEW";
            order.UserId = 1;// should be logged in userId from jwt token
            order.OrderItems = dto.OrderItems.ToEntity(order);
            return order;
        }
        public static Order ToEntity(this UpdateOrderDto dto)
        {
            Order order = new Order();
            order.Id = dto.Id;
            order.OrderItems = dto.OrderItems.ToEntity(order);
            return order;
        }
        public static OrderItem ToEntity(this CreateOrderItemDto dto, Order order)
        {
            OrderItem orderItem = new OrderItem();
            orderItem.OrderId = order.Id;
            orderItem.ProductId = dto.ProductId;
            orderItem.Quantity = dto.Quantity;
            return orderItem;
        }
        public static IEnumerable<OrderItem> ToEntity(this IEnumerable<CreateOrderItemDto> orderItemDtos, Order order)
        {
            List<OrderItem> entityList = new List<OrderItem>();
            foreach (var item in orderItemDtos)
            {
                entityList.Add(item.ToEntity(order));
            }
            return entityList;
        }
    }
}
