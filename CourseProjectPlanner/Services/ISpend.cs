namespace CourseProjectPlanner.Services
{
    public interface ISpend
    {
        IEnumerable<Spend> GetSpends {  get; }
        Spend GetSpend(int id);
        void Add(Spend _Spend);
        void Remove(int id);  
        void Edit(Spend _Spend);
    }
}
