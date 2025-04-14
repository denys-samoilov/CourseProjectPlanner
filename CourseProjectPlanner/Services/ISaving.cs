namespace CourseProjectPlanner.Services
{
	public interface ISaving
	{
		IEnumerable<Saving> GetSavings { get; }
		Saving GetSaving(int id);
		void Add(Saving _Saving);
		void Remove(int id);
		void Edit(Saving _Saving);
	}
}
