using ASP_Core_EF.Repository;
using CourseProjectPlanner.Services;

namespace CourseProjectPlanner.Repository
{
    public class UserRepository:IUser
    {
        private DBContext db;
        public UserRepository(DBContext db)
        {
            this.db = db;
        }

        public IEnumerable<User> GetUsers => db.Users;

        public void Add(User _User)
        {
            db.Users.Add(_User);
            db.SaveChanges();
        }

        public User GetUser(int id)
        {
            User dbEntity = db.Users.Find(id);
            return dbEntity;
        }

        public void Remove(int id)
        {
            User dbEntity = db.Users.Find(id);
            db.Remove(dbEntity);
            db.SaveChanges();
        }
    }
}
