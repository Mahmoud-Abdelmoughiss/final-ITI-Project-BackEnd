using EcommerseApplication.DTO;
using EcommerseApplication.Models;

namespace EcommerseApplication.Repository
{
    public interface Ishipper
    {
        public void insert(Shipper shipper);
        public void update(int id,Shipper shipper);
        public Shipper getByID(int id);
        public Shipper getByName(string name);
        public List<Shipper> getAll();
        public void delete(int id);
        public Shipper getByIdentityID(string IdentityID);
        void insert(shiperDto model);
    }
}
