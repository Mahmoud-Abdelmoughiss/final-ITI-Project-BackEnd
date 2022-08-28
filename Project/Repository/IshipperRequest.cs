using EcommerseApplication.Models;

namespace EcommerseApplication.Repository
{
    public interface IshipperRequest
    {
        public void Add(ShipperRequest shipperRequest);
        public List<ShipperRequest> GetAll();
        public ShipperRequest Get(int id);
        public ShipperRequest Get(string id);
        public void remove(int id);
        public ShipperRequest GetByIntityId(string id);
    }
}
