using EcommerseApplication.Models;
using EcommerseApplication.DTO;

namespace EcommerseApplication.Repository
{
    public interface IshippingDetails
    {
        public void insert(shippingDetails shippingDetails);
        public void update(int id, shippingDetails shippingDetails);
        public shippingDetails getByID(int id);
        public shippingDetails getByUserAndOrder(int UserID,int OrderID);
        public shippingDetails getByName(string name);
        public List<shippingDetails> getAll();
        public List<shippingDetails> getAllbyUserID(int id);
        public List<shippingDetails> getAllbyShipperID(int id);
        public void delete(int id);
        public void updateState(int id, string shippingstate);
        public void updateShippingDetails(int id,shippingDetails shippingDetails);
        public void updateStatewithDTo(UpdateshippingDTO updateshippingDTO );


    }
}
