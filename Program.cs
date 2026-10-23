using WestcoastEducation.ConsoleUI;
using WestcoastEducation.Interfaces;
using WestcoastEducation.Models;
using WestcoastEducation.Persistence;

namespace WestcoastEducation;

class Program
{
    static readonly string dataPath = Path.Combine(AppContext.BaseDirectory, "Data");
    static readonly IRepository<Course> courseRepo = new JsonRepository<Course>(
        Path.Combine(dataPath, "courses.json")
    );
    static readonly IRepository<Student> studentRepo = new JsonRepository<Student>(
        Path.Combine(dataPath, "students.json")
    );
    static readonly IRepository<Staff> staffRepo = new JsonRepository<Staff>(
        Path.Combine(dataPath, "staff.json")
    );
    static readonly School school = new(courseRepo, studentRepo, staffRepo);
    static void Main()
    {
        MainMenu mainMenu = new(school);
        while (true)
        {
            mainMenu.Run();
        }
    }
}
