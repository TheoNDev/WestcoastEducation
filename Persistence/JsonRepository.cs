using System.Text.Json;
using WestcoastEducation.Interfaces;

namespace WestcoastEducation.Persistence;

public class JsonRepository<T> : IRepository<T>
    where T : IIdentifiable
{
    private readonly List<T> _entities = [];
    private readonly string _filePath;
    private readonly JsonSerializerOptions _options = new()
    {
        WriteIndented = true
    };

    public JsonRepository(string filePath)
    {
        _filePath = filePath;
        _entities = ReadJson();
    }


    public void Add(T entity)
    {
        _entities.Add(entity);
        WriteJson();
    }
    public void DeleteById(string id)
    {
        T? entity = _entities.Find(e => e.Id == id);

        if (entity is not null && _entities.Remove(entity))
        {
            WriteJson();
        }
        else
        {
            throw new Exception($"{typeof(T)} kunde inte tas bort");
        }
    }
    public T? GetById(string id)
    {
        return _entities.Find(e => e.Id == id);
    }
    public IReadOnlyList<T> GetCollection()
    {
        return _entities;
    }
    public void Update(T entity)
    {
        throw new NotImplementedException();
    }

    private List<T> ReadJson()
    {
        if (!File.Exists(_filePath))
        {
            return _entities;
        }

        string json = File.ReadAllText(_filePath);

        return JsonSerializer.Deserialize<List<T>>(json) ?? [];
    }
    private void WriteJson()
    {

        string json = JsonSerializer.Serialize(_entities, _options);

        string? directory = Path.GetDirectoryName(_filePath);
        if (!string.IsNullOrEmpty(directory))
        {
            Directory.CreateDirectory(directory);
        }

        File.WriteAllText(_filePath, json);
    }

}