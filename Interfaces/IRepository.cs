namespace WestcoastEducation.Interfaces;

public interface IRepository<T>
{
    public void Add(T entity);
    public T? GetById(string id);
    public IReadOnlyList<T> GetCollection();
    public void Update(T entity);
    public void DeleteById(string id);
}