using ASP_Core_EF.Repository;
using CourseProjectPlanner.Models;
using CourseProjectPlanner.Services;
using Microsoft.AspNetCore.Mvc;

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

        public void Edit(Spend _Spend) 
        {
			Spend dbEntity = db.Spends.Find(_Spend.SpendId);
            dbEntity.Price = _Spend.Price;
            dbEntity.Description= _Spend.Description;
            dbEntity.CategoryId = _Spend.CategoryId;
			dbEntity.Currency = _Spend.Currency;
			db.SaveChanges();
        }
    }
}
