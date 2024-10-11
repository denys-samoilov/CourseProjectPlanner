using ASP_Core_EF.Repository;
using CourseProjectPlanner.Services;

namespace CourseProjectPlanner.Repository
{
    public class CategoryRepository : ICategory
    {
        private DBContext db;
        public CategoryRepository(DBContext _db)
        {
            this.db = _db;
        }
        public IEnumerable<Category> GetCategories => db.Categories;

        public void Add(Category _Category)
        {
           db.Categories.Add(_Category);
           db.SaveChanges();

        }

        public Category GetCategory(int id)
        {
            Category dbEntity = db.Categories.Find(id);
            return dbEntity;
        }

        public void Remove(int id)
        {
            Category dbEntity = db.Categories.Find(id);
            db.Categories.Remove(dbEntity);
            db.SaveChanges();

        }
    }
}
