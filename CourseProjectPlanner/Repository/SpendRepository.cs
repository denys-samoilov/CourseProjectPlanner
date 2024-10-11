using ASP_Core_EF.Repository;
using CourseProjectPlanner.Models;
using CourseProjectPlanner.Services;

namespace CourseProjectPlanner.Repository
{
    public class SpendRepository : ISpend
    {

        private DBContext db;

        public SpendRepository(DBContext _db)
        {
            this.db = _db;
        }
        public IEnumerable<Spend> GetSpends => db.Spends;

        public void Add(Spend _Spend)
        {
            db.Spends.Add(_Spend);
            db.SaveChanges();
        }

        public Spend GetSpend(int id)
        {
            Spend dbEntity = db.Spends.Find(id);
            return dbEntity;
        }

        public void Remove(int id)
        {
            Spend dbEntity = db.Spends.Find(id);
            db.Spends.Remove(dbEntity);
            db.SaveChanges();
        }
    }
}
