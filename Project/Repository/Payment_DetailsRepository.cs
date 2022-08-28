using EcommerseApplication.Models;

namespace EcommerseApplication.Repository
{
    public class Payment_DetailsRepository : IPayment_DetailsRepository
    {
        Context context;
        public Payment_DetailsRepository(Context _context)
        {
            context = _context;
        }
        public List<Payment_Details> GetAllPayment_Details()
        {
            List<Payment_Details> payment_Details = context.Payment_Details.ToList();
            return payment_Details;
        }
        public Payment_Details GetPayment_Details(int Id)
        {
            Payment_Details payment_Details = context.Payment_Details.FirstOrDefault(x => x.ID == Id);
            return payment_Details;
        }
        public int AddPayment_Details(Payment_Details NewPayment_Details)
        {
            if (NewPayment_Details != null)
            {
                NewPayment_Details.CreatedAt = DateTime.Now;
                context.Payment_Details.Add(NewPayment_Details);
                context.SaveChanges();
            }
            return 0;

        }
        public int DeletePayment_DetailsId(int Id)
        {
            Payment_Details payment_Details = context.Payment_Details.FirstOrDefault(x => x.ID == Id);
            if (payment_Details != null)
            {
                try
                {
                    context.Payment_Details.Remove(payment_Details);
                    return context.SaveChanges();
                }
                catch (Exception ex)
                {

                }
            }
            return 0;
        }
        public int UpdatePayment_Details(int Id, Payment_Details Newpayment_Details)
        {
            Payment_Details oldPayment_Details = context.Payment_Details.FirstOrDefault(ci => ci.ID == Id);
            if (oldPayment_Details != null)
            {
                oldPayment_Details.Status = Newpayment_Details.Status;
                oldPayment_Details.Provider = Newpayment_Details.Provider;
                oldPayment_Details.Amount = Newpayment_Details.Amount;
                oldPayment_Details.UpdatedAt = DateTime.Now;
                return context.SaveChanges();
            }
            return 0;
        }
    }
}
