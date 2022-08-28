using EcommerseApplication.Models;
using Microsoft.EntityFrameworkCore;

namespace EcommerseApplication.Repository
{
    public class RequestRepository : IRequest
    {
        private readonly Context context;

        public RequestRepository(Context _con)
        {
            context = _con;
        }

        public List<Requests> GetAllRequests()
        {
            List<Requests> All=context.Requests.Include(data=>data.identity).ToList();
            return All;
        }

        public void RequestTobePartner(Requests request)
        {
         context.Requests.Add(request);
         context.SaveChanges();
        }
        public Requests GetPartnerById(int id)
        {
            return context.Requests.FirstOrDefault(i => i.RequestId == id);
            
        }
        public Requests GetRequestByIdentityId(string id)
        {
            return context.Requests.FirstOrDefault(i => i.IdentityId==id);
        }
        public void RemoveRequest(Requests request)
        {
            context.Remove(request);
            context.SaveChanges();
        }


    }
}

