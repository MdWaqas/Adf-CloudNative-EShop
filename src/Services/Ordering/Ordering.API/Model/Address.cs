using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ordering.API.Model
{
    public class OrderSummaryDto
    {
        public int ordernumber { get; set; }
        public DateTime date { get; set; }
        public string status { get; set; }
        public decimal total { get; set; }

        public static OrderSummaryDto FromOrderSummary(OrderSummary orderSummary)
        {
            return new OrderSummaryDto
            {
                ordernumber = orderSummary.OrderNumber,
                date = orderSummary.OrderDate,
                status = orderSummary.OrderStatus,
                total = orderSummary.Total
            };
        }
    }
    public class OrderSummary
    {
        public Guid Id { get; set; }
        public int OrderNumber { get; set; }
        public DateTime OrderDate { get; set; }
        public string OrderStatus { get; set; }
        public decimal Total { get; set; }
    }
    public class OrderItemDto
    {
        public string productname { get; set; }
        public int units { get; set; }
        public decimal unitprice { get; set; }
        public string pictureurl { get; set; }

        public static OrderItemDto FromOrderItem(OrderItem orderItem)
        {
            return new OrderItemDto
            {
                productname = orderItem.ProductName,
                units = orderItem.Units,
                unitprice = orderItem.UnitPrice,
                pictureurl = orderItem.PictureUrl
            };
        }
    }
    public class OrderItem
    {
        public int Id { get; set; }
        public Guid OrderId { get; set; }
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public decimal UnitPrice { get; set; }
        public int Units { get; set; }
        public string PictureUrl { get; set; }

        public static OrderItem FromActorState(Actors.OrderItem orderItem)
        {
            return new OrderItem
            {
                ProductId = orderItem.ProductId,
                ProductName = orderItem.ProductName,
                UnitPrice = orderItem.UnitPrice,
                Units = orderItem.Units,
                PictureUrl = orderItem.PictureUrl
            };
        }
    }
    public class OrderDto
    {
        public int ordernumber { get; set; }
        public DateTime date { get; set; }
        public string status { get; set; }
        public string description { get; set; }
        public string street { get; set; }
        public string city { get; set; }
        public string zipcode { get; set; }
        public string country { get; set; }
        public List<OrderItemDto> orderitems { get; set; }
        public decimal subtotal { get; set; }
        public decimal total { get; set; }

        public static OrderDto FromOrder(Order order)
        {
            return new OrderDto
            {
                ordernumber = order.OrderNumber,
                date = order.OrderDate,
                status = order.OrderStatus,
                description = order.Description,
                street = order.Address.Street,
                city = order.Address.City,
                zipcode = order.Address.ZipCode,
                country = order.Address.Country,
                orderitems = order.OrderItems
                    .Select(OrderItemDto.FromOrderItem)
                    .ToList(),
                subtotal = order.GetTotal(),
                total = order.GetTotal()
            };
        }
    }
    public class Order
    {
        public Guid Id { get; set; }
        public int OrderNumber { get; set; }
        public DateTime OrderDate { get; set; }
        public string OrderStatus { get; set; }
        public string Description { get; set; }
        public Address Address { get; set; }
        public string BuyerId { get; set; }
        public string BuyerName { get; set; }
        public List<OrderItem> OrderItems { get; set; }

        public decimal GetTotal()
        {
            var result = OrderItems.Sum(o => o.Units * o.UnitPrice);

            return result < 0 ? 0 : result;
        }

        public static Order FromActorState(Guid orderId, Actors.Order order)
        {
            return new Order
            {
                Id = orderId,
                OrderDate = order.OrderDate,
                OrderStatus = order.OrderStatus.Name,
                BuyerId = order.UserId,
                BuyerName = order.UserName,
                Address = new Address
                {
                    Street = order.Address.Street,
                    City = order.Address.City,
                    ZipCode = order.Address.ZipCode,
                    State = order.Address.State,
                    Country = order.Address.Country
                },
                OrderItems = order.OrderItems
                    .Select(OrderItem.FromActorState)
                    .ToList()
            };
        }
    }
    public interface IOrderRepository
    {
        Task<Order> GetOrderByIdAsync(Guid orderId);
        Task<Order> GetOrderByOrderNumberAsync(int orderNumber);
        Task<Order> AddOrGetOrderAsync(Order order);
        Task UpdateOrderAsync(Order order);
        Task<IEnumerable<OrderSummary>> GetOrdersFromBuyerAsync(string buyerId);
        Task<IEnumerable<CardType>> GetCardTypesAsync();
    }
    public interface IEmailService
    {
        Task SendOrderConfirmation(Order order);
    }
    public class CustomerBasket
    {
        public string BuyerId { get; set; }
        public List<BasketItem> Items { get; set; }
    }
    public class CardTypeDto
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public static CardTypeDto FromCardType(CardType cardType)
        {
            return new CardTypeDto
            {
                Id = cardType.Id,
                Name = cardType.Name
            };
        }
    }
    public class CardType
    {
        public static readonly CardType Amex = new CardType(1, "Amex");
        public static readonly CardType Visa = new CardType(2, "Visa");
        public static readonly CardType MasterCard = new CardType(3, "MasterCard");

        public CardType(int id, string name)
        {
            Id = id;
            Name = name;
        }

        public int Id { get; set; }
        public string Name { get; set; }
    }
    public class Address
    {
        public string Street { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Country { get; set; }
        public string ZipCode { get; set; }
    }

    public class BasketItem
    {
        public string Id { get; set; }
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal OldUnitPrice { get; set; }
        public int Quantity { get; set; }
        public string PictureUrl { get; set; }
    }
}
