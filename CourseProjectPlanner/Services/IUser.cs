namespace CourseProjectPlanner.Services
{
    public interface IUser
    {
        IEnumerable<User> GetUsers { get; }
        User GetUser(int id);
        void Add (User _User);
        void Remove(int id);
    }
}
