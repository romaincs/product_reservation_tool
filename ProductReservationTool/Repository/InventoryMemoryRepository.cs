using ProductReservationTool.Model;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductReservationTool.Repository
{
    public class InventoryMemoryRepository : IInventoryRepository
    {
        static List<Reservation> reservations;
        static List<Product> products;
        static List<OrderLine> orders;

        public InventoryMemoryRepository()
        {
            if(reservations == null)
                reservations = new List<Reservation>();
            if (products == null)
                products = new List<Product>();
            if(orders == null)
                orders = new List<OrderLine>();
        }

        public void InsertProduct(Product product)
        {
            products.Add(product);
        }

        public IQueryable<Product> GetProducts()
        {
            return products.AsQueryable();
        }

        public Product? GetProduct()
        {
            if (products.Count == 0)
                return null;

            return products.OrderByDescending(o => o.ProductId).FirstOrDefault();
        }

        public Product? GetProduct(int id)
        {
            return products.Where(r => r.ProductId == id).FirstOrDefault();
        }

        public void UpdateProduct(Product product)
        {
            for(int i = 0; i < products.Count; i++)
            {
                if(product.ProductId == product.ProductId)
                {
                    products[i] = product;
                    return;
                }
            }
        }

        #region RESERVATIONS --------------------------------------------------------------------------------
        public IQueryable<Reservation> GetReservations()
        {
            return reservations.AsQueryable();
        }

        public void InsertReservation(Reservation reservation)
        {
            reservations.Add(reservation);
        }

        public Reservation? GetReservation()
        {
            if (reservations.Count == 0)
                return null;

            return reservations.OrderByDescending(r => r.ReservationId).FirstOrDefault();
        }

        public Reservation? GetReservation(int id)
        {
            return reservations.Where(r => r.ReservationId == id).FirstOrDefault();
        }
        #endregion

        #region ORDER LINES ---------------------------------------------------------------------------------
        public void InsertOrderLine(OrderLine order)
        {
            orders.Add(order);
        } 
        #endregion
    }
}
