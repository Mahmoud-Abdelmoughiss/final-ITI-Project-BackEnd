using EcommerseApplication.Models;

namespace EcommerseApplication.Repository
{
    public class ShipperRequestRepository : IshipperRequest
    {
        Context context;
        public ShipperRequestRepository(Context _context)
        {
            this.context = _context;
        }
        public void Add(ShipperRequest shipperRequest)
        {
            context.shipperRequests.Add(shipperRequest);
            context.SaveChanges();
        }

        public ShipperRequest Get(int id)
        {
            return context.shipperRequests.FirstOrDefault(e => e.Id == id);
        }
        public ShipperRequest GetByIntityId(string id)
        {
            return context.shipperRequests.FirstOrDefault(e => e.AccountID == id);
        }

        public List<ShipperRequest> GetAll()
        {
            return context.shipperRequests.ToList();
        }

        public void remove(int id)
        {
            ShipperRequest shipperR=  context.shipperRequests.FirstOrDefault(e => e.Id == id);
            context.shipperRequests.Remove(shipperR);
            context.SaveChanges();
        }

        public ShipperRequest Get(string id)
        {
            throw new NotImplementedException();
        }
    }
}
