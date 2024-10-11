using CourseProjectPlanner.Models;

namespace CourseProjectPlanner.Services
{
    public interface ICategory
    {
        IEnumerable<Category> GetCategories {  get; }
        Category GetCategory(int id);
        void Add(Category _Category);
        void Remove(int id);    
    }
}
