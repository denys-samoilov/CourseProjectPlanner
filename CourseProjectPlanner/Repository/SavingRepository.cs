using ASP_Core_EF.Repository;
using CourseProjectPlanner.Models;
using CourseProjectPlanner.Services;
using Microsoft.AspNetCore.Mvc;

namespace CourseProjectPlanner.Repository
{
	public class SavingRepository : ISaving
	{
		private DBContext db;

        public SavingRepository(DBContext _db)
        {
            this.db = _db;
        }
        public IEnumerable<Saving> GetSavings => db.Savings;

        public void Add(Saving _Saving)
        {
            db.Savings.Add(_Saving);
            db.SaveChanges();
        }

        public Saving GetSaving(int id)
        {
			Saving dbEntity = db.Savings.Find(id);
            return dbEntity;
        }

        public void Remove(int id)
        {
			Saving dbEntity = db.Savings.Find(id);
            db.Savings.Remove(dbEntity);
            db.SaveChanges();
        }

        public void Edit(Saving _Saving) 
        {
			Saving dbEntity = db.Savings.Find(_Saving.SavingId);
            dbEntity.Price = _Saving.Price;
            dbEntity.Description= _Saving.Description;
            dbEntity.CategoryId = _Saving.CategoryId;
            dbEntity.Currency = _Saving.Currency;
            db.SaveChanges();
        }
	}
}
