using EcommerseApplication.Models;

namespace EcommerseApplication.Repository
{
    public interface Ipartener
    {
        public void insert(Partener partener);
        public void update(int id, Partener partener );
        public Partener getByID(int id);
        public Partener getByName(string name);
        public List<Partener> getAll();
        public void delete(int id);
        public Partener getByUserID(int id);
        public User getByIDentity(string identityId);
    }
}
