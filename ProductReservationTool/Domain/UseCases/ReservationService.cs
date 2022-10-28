using ProductReservationTool.Domain.Entities;
using ProductReservationTool.Domain.Interfaces;
using ProductReservationTool.Domain.Exceptions;

namespace ProductReservationTool.Domain.UseCases
{
    public class ReservationService
    {
        IInventoryRepository repository;

        public ReservationService(IInventoryRepository repo)
        {
            repository = repo;
        }

        public Reservation Create(List<OrderLine> orders)
        {
            CheckDuplicates(orders);
            CheckProductsExits(orders);

            foreach (OrderLine order in orders)
            {
                repository.InsertOrderLine(order);
            }

            var resa = new Reservation() { ReservationId = GetNewReservationID(), CreatedAt = DateTime.Now, OrderLines = orders };
            repository.InsertReservation(resa);
            return resa;
        }

        private void CheckDuplicates(List<OrderLine> orders)
        {
            var duplicates = orders.GroupBy(r => r.ProductId)
                      .Where(r => r.Count() > 1)
                      .Select(r => r.Key)
                      .ToList();

            if (duplicates.Count > 0)
                throw new DuplicateProductException();
        }

        private void CheckProductsExits(List<OrderLine> orders)
        {
            var prodService = new ProductService(repository);
            foreach (var order in orders)
            {
                var product = prodService.GetByID(order.ProductId);
                if (product == null)
                    throw new UnknownProductException(order.ProductId);
            }
        }

        public string GetNewReservationID()
        {
            Reservation? lastResa = repository.GetReservation();
            var newID = lastResa != null ? int.Parse(lastResa.ReservationId) + 1 : 1;
            return newID.ToString();
        }

        public Reservation? GetByID(string id)
        {
            return repository.GetReservation(id);
        }

        public IQueryable<Reservation> Get(int cursor, int limit)
        {
            return repository.GetReservations().Skip(cursor).Take(limit);
        }

        public IQueryable<Reservation> GetAll()
        {
            return repository.GetReservations();
        }

        public void UpdateAvailibility(Reservation reservation, bool avaibility)
        {
            reservation.IsAvailable = avaibility;
            repository.UpdateReservation(reservation);
        }

        public List<Reservation> GetReservationsForProduct(string id)
        {
            var reservations = new List<Reservation>();
            foreach (var reservation in GetAll())
            {
                var products = reservation.OrderLines.Where(o => o.ProductId == id);
                if (products != null && products.Count() > 0)
                    reservations.Add(reservation);
            }
            return reservations;
        }
    }
}
