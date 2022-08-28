using EcommerseApplication.DTO;
using EcommerseApplication.Models;
using Microsoft.EntityFrameworkCore;

namespace EcommerseApplication.Repository
{
    public class shipperRepository : Ishipper
    {
        Context context;
        public shipperRepository(Context _context)
        {
            this.context = _context;
        }
        public void delete(int id)
        {
            Shipper shipper = context.shippers.FirstOrDefault(e=>e.ID==id);
            shipper.deletedAt=DateTime.Now;


            context.shippers.Remove(shipper);
            context.SaveChanges();
        }
        public Shipper getByIdentityID(string IdentityID)
        {
            return context.shippers.FirstOrDefault(s => s.IdentityId == IdentityID);
        }

        public List<Shipper> getAll()
        {
         return  context.shippers.Include(shipper => shipper.Identity).ToList();
        }

        public Shipper getByID(int id)
        {
            return context.shippers.FirstOrDefault(e => e.ID == id);
        }

        public Shipper getByName(string name)
        {
            return context.shippers.FirstOrDefault(e => e.Name==name);
        }

        public void insert(Shipper shipper)
        {
            context.shippers.Add(shipper);
            context.SaveChanges();
        }

        public void insert(shiperDto model)
        {
            Shipper shipper = new Shipper();
            shipper.officePhone = model.officePhone;
            shipper.Name = model.Name;
            context.shippers.Add(shipper);
            context.SaveChanges();
        }

        public void update(int id, Shipper shipper)
        {
            Shipper old = context.shippers.FirstOrDefault(e => e.ID == id);
            old.Name = shipper.Name;
            old.arabicName = shipper.arabicName;
            old.officePhone = shipper.officePhone;
            context.SaveChanges();

        }
    }
}
