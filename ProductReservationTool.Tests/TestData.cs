using ProductReservationTool.Model;
using ProductReservationTool.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductReservationTool.Tests
{
    static class TestData
    {
        public static List<Product> products = new List<Product>()
        {
            new Product()
            {
                ProductId = "1",
                Quantity = 10,
            },
            new Product()
            {
                ProductId = "2",
                Quantity = 4,
            },
            new Product()
            {
                ProductId = "3",
                Quantity = 0,
            }
        };

        public static List<OrderLine> orders = new List<OrderLine>()
        {
            new OrderLine()
            {
                ProductId = "1",
                Quantity = 3,
            },
            new OrderLine()
            {
                ProductId = "2",
                Quantity = 6,
            },

            new OrderLine()
            {
                ProductId = "3",
                Quantity = 12,
            },

            new OrderLine()
            {
                ProductId = "1",
                Quantity = 2,
            },
            new OrderLine()
            {
                ProductId = "2",
                Quantity = 7,
            },
            new OrderLine()
            {
                ProductId = "3",
                Quantity = 12,
            },

            new OrderLine()
            {
                ProductId = "3",
                Quantity = 5,
            },
        };

        public static List<Reservation> reservations = new List<Reservation>()
        {
            new Reservation()
            {
                ReservationId = "1",
                CreatedAt = DateTime.Now.AddMinutes(-18),
                IsAvailable = true,
                OrderLines = new List<OrderLine>()
                {
                    new OrderLine()
                    {
                        ProductId = "1",
                        Quantity = 3,
                    },
                    new OrderLine()
                    {
                        ProductId = "2",
                        Quantity = 6,
                    }
                }
            },
            new Reservation()
            {
                ReservationId = "2",
                CreatedAt = DateTime.Now.AddMinutes(-20),
                IsAvailable = true,
                OrderLines = new List<OrderLine>()
                {
                    new OrderLine()
                    {
                        ProductId = "3",
                        Quantity = 12,
                    }
                }
            },
            new Reservation()
            {
                ReservationId = "3",
                CreatedAt = DateTime.Now.AddMinutes(-1),
                IsAvailable = true,
                OrderLines = new List<OrderLine>()
                {
                    new OrderLine()
                    {
                        ProductId = "1",
                        Quantity = 2,
                    },
                    new OrderLine()
                    {
                        ProductId = "2",
                        Quantity = 7,
                    },
                    new OrderLine()
                    {
                        ProductId = "3",
                        Quantity = 12,
                    },
                }
            },
            new Reservation()
            {
                ReservationId = "4",
                CreatedAt = DateTime.Now,
                IsAvailable = false,
                OrderLines = new List<OrderLine>()
                {
                    new OrderLine()
                    {
                        ProductId = "3",
                        Quantity = 5,
                    },
                }
            },
        };
    }
}
