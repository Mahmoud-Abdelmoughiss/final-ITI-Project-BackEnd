using EcommerseApplication.Models;

namespace EcommerseApplication.Repository
{
    public interface IPayment_DetailsRepository
    {
        int AddPayment_Details(Payment_Details NewPayment_Details);
        int DeletePayment_DetailsId(int Id);
        List<Payment_Details> GetAllPayment_Details();
        Payment_Details GetPayment_Details(int Id);
        int UpdatePayment_Details(int Id, Payment_Details Newpayment_Details);
    }
}