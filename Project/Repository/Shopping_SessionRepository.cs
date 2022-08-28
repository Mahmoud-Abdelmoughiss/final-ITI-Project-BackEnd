using EcommerseApplication.Models;

namespace EcommerseApplication.Repository
{
    public class Shopping_SessionRepository : IShopping_SessionRepository
    {
        Context context;
        public Shopping_SessionRepository(Context _context)
        {
            context = _context;
        }
        public List<Shopping_Session> GetAllShopping_Session()
        {
            List<Shopping_Session> shopping_sessions = context.Shopping_Sessions.ToList();
            return shopping_sessions;
        }
        public Shopping_Session GetShopping_SessionById(int Id)
        {
            Shopping_Session shopping_Session = context.Shopping_Sessions.FirstOrDefault(x => x.Id == Id);
            return shopping_Session;
        }
        public int AddShopping_Session(Shopping_Session NewShopping_Session)
        {
            if (NewShopping_Session != null)
            {
                context.Shopping_Sessions.Add(NewShopping_Session);
                context.SaveChanges();
            }
            return 0;

        }
        public int DeleteShopping_SessionById(int Id)
        {
            Shopping_Session shopping_Session = context.Shopping_Sessions.FirstOrDefault(x => x.Id == Id);
            if (shopping_Session != null)
            {
                try
                {
                    context.Shopping_Sessions.Remove(shopping_Session);
                    return context.SaveChanges();
                }
                catch (Exception ex)
                {

                }
            }
            return 0;
        }

        public List<Shopping_Session> GetByUserId(int UserId)
        {
            return context.Shopping_Sessions.Where(s => s.UserID == UserId).ToList();
        }
        
        public void ClearTotal(int SessionId)
        {
            Shopping_Session sh = context.Shopping_Sessions.FirstOrDefault(s => s.Id == SessionId);
            sh.Total = 0;
            context.SaveChanges();
        }


    }
}
