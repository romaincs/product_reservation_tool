using ProductReservationTool.Domain.Entities;
using ProductReservationTool.Domain.Interfaces;

namespace ProductReservationTool.Data
{
    public class InventoryMemoryRepository : IInventoryRepository
    {
        DBMemoryContext db;

        public InventoryMemoryRepository()
        {
            db = DBMemoryContext.GetInstance();
        }

        public void InsertProduct(Product product)
        {
            db.Products.Add(product);
        }

        public IQueryable<Product> GetProducts()
        {
            return db.Products.AsQueryable();
        }

        public Product? GetProduct()
        {
            if (db.Products.Count == 0)
                return null;

            return db.Products.OrderByDescending(o => int.Parse(o.ProductId)).FirstOrDefault();
        }

        public Product? GetProduct(string id)
        {
            return db.Products.Where(r => r.ProductId == id).FirstOrDefault();
        }

        public void UpdateProduct(Product product)
        {
            for (int i = 0; i < db.Products.Count; i++)
            {
                if (product.ProductId == product.ProductId)
                {
                    db.Products[i].Quantity = product.Quantity;
                    return;
                }
            }
        }

        public IQueryable<Reservation> GetReservations()
        {
            return db.Reservations.OrderBy(r => r.CreatedAt).AsQueryable();
        }

        public void InsertReservation(Reservation reservation)
        {
            db.Reservations.Add(reservation);
        }

        public Reservation? GetReservation()
        {
            return db.Reservations.OrderByDescending(r => int.Parse(r.ReservationId)).FirstOrDefault();
        }

        public Reservation? GetReservation(string id)
        {
            return db.Reservations.Where(r => r.ReservationId == id).FirstOrDefault();
        }

        public void UpdateReservation(Reservation reservation)
        {
            for (int i = 0; i < db.Reservations.Count; i++)
            {
                if (reservation.ReservationId == reservation.ReservationId)
                {
                    db.Reservations[i].OrderLines = reservation.OrderLines;
                    db.Reservations[i].IsAvailable = reservation.IsAvailable;
                    return;
                }
            }
        }

        public void InsertOrderLine(OrderLine order)
        {
            db.Orders.Add(order);
        }
    }
}
