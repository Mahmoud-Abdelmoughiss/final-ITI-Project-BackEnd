using EcommerseApplication.Models;
using EcommerseApplication.DTO;

namespace EcommerseApplication.Repository
{
    public class shippingDetailsRepository : IshippingDetails
    {
        Context context;
        public shippingDetailsRepository(Context _context)
        {
            this.context = _context;
        }
        public void delete(int id)
        {
            shippingDetails shipping=context.shippingDetails.FirstOrDefault(ship=>ship.ID==id);
            shipping.deletedAt = DateTime.Now;

            context.shippingDetails.Remove(shipping);
            context.SaveChanges();
        }

        public List<shippingDetails> getAll()
        {
           return context.shippingDetails.ToList();
        }

        public shippingDetails getByID(int id)
        {
            return context.shippingDetails.FirstOrDefault(ship => ship.ID == id);
        }

        public shippingDetails getByName(string name)
        {
            return context.shippingDetails.FirstOrDefault(ship => ship.shipName == name);
        }

        public void insert(shippingDetails shippingDetails)
        {
            context.shippingDetails.Add(shippingDetails);
            context.SaveChanges();
        }

        public void update(int id, shippingDetails shippingDetails)
        {
            shippingDetails old = context.shippingDetails.FirstOrDefault(ship => ship.ID == id);
            old.shippingstate = shippingDetails.shippingstate;
            old.addressID = shippingDetails.addressID;
            old.ALLaddress=shippingDetails.ALLaddress;
            old.arabicshippingstate= shippingDetails.arabicshippingstate;
            old.shipperID=shippingDetails.shipperID;
           
            old.shipName=shippingDetails.shipName;
            context.SaveChanges();

        }
        public void updateState(int id, string shippingstate)
        {
            shippingDetails old = context.shippingDetails.FirstOrDefault(ship => ship.ID == id);
            old.shippingstate = shippingstate;
            context.SaveChanges();

        }
        public List<shippingDetails> getAllbyUserID(int id)
        {
            return context.shippingDetails.Where(ship=>ship.userID==id).ToList();
        }
        public void updateShippingDetails(int id, shippingDetails shippingDetails)
        {
           shippingDetails old= context.shippingDetails.FirstOrDefault(e => e.ID == id);
            old.shippingstate = shippingDetails.shippingstate;
           // old.ALLaddress = shippingDetails.ALLaddress;
            old.arabicshippingstate = shippingDetails.arabicshippingstate;
            //old.addressID=shippingDetails.addressID;
            context.SaveChanges();

        }
        public List<shippingDetails> getAllbyShipperID(int id)
        {
            return context.shippingDetails.Where(ship => ship.shipperID == id).ToList();
        }

        public shippingDetails getByUserAndOrder(int UserID, int OrderID)
        {
            return context.shippingDetails.Where(s => s.orderDetailsID == OrderID).FirstOrDefault(ship => ship.userID == UserID);
        }
        public void updateStatewithDTo(UpdateshippingDTO updateshippingDTO)
        {
           shippingDetails shippingDetails =context.shippingDetails.FirstOrDefault(e => e.ID == updateshippingDTO.shipID);
            shippingDetails.shippingstate= updateshippingDTO.shippingState;
            shippingDetails.arabicshippingstate = updateshippingDTO.arabicshippingState;
            context.SaveChanges();
        }
    }
}
