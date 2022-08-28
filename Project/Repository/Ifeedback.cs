using EcommerseApplication.Models;
namespace EcommerseApplication.Repository
{
    public interface Ifeedback
    {
        public void insert(feedback feedback);
        public void update(int id, feedback feedback);
        public feedback getByID(int id);
        public List<feedback> getAll();
        public void delete(int id);
        public feedback  getByUserID(int id);
        public feedback getByProductID(int id);
        public List<feedback> getByfeedbackProductID(int Id);
        public decimal getratepartnerbyId(int Id);
    }
}
