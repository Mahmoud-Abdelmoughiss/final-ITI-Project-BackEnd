using EcommerseApplication.Models;

namespace EcommerseApplication.Repository
{
    public class feedbackRepository : Ifeedback
    {
        Context context;
        public feedbackRepository(Context _context)
        {
            this.context = _context;
        }
        public void delete(int id)
        {
           feedback feedback =context.feedbacks.FirstOrDefault(feed=>feed.Id==id);
            context.feedbacks.Remove(feedback);
            context.SaveChanges();
        }
        public List<feedback> getAll()
        {
            return context.feedbacks.ToList();
        }

        public feedback getByID(int id)
        {
            return context.feedbacks.FirstOrDefault(feed=>feed.Id==id);
        }

       

        public feedback getByUserID(int id)
        {
            return context.feedbacks.FirstOrDefault(feed => feed.UserID==id);
        }

        public void insert(feedback feedback)
        {
            context.feedbacks.Add(feedback);

            context.SaveChanges();
        }

        public void update(int id, feedback feedback)
        {
            feedback old= context.feedbacks.FirstOrDefault(feed => feed.Id == id);
            old.Rate=feedback.Rate;
            old.Comment=feedback.Comment;
            old.productID = feedback.productID;
            context.SaveChanges();
        }
        public feedback getByProductID(int id)
        {
            return context.feedbacks.FirstOrDefault(feed => feed.productID == id);
        }
        public List<feedback> getByfeedbackProductID(int Id)
        {
                List<feedback> feedbackList = context.feedbacks.Where(feed => feed.productID == Id).ToList();
                return feedbackList;
        }
        public decimal getratepartnerbyId(int Id)
        {
           
            List<int> productsID = context.Products.Where(p=> p.PartenerID == Id).Select(p=>p.ID).ToList();
            List<Decimal> partnerRate = new List<decimal>();
            int count = 0;
            foreach (int product in productsID)
            {
                List<Decimal> ratOfEachProduct = context.feedbacks.Where(f => f.productID == product).Select(f => f.Rate).ToList();
                 count+= ratOfEachProduct.Count;
                partnerRate.Add(ratOfEachProduct.Sum());
                
            }
            decimal TotalRate = partnerRate.Sum();
            decimal PartnerRate=0;
            if (count > 0)
            {
                PartnerRate = TotalRate/count;
            }
            return PartnerRate;
        }
    }
}
