using WestcoastEducation.InputModels;
using WestcoastEducation.Models;

namespace WestcoastEducation.ConsoleUI;

public class StudentMenu(School school)
{
    public void Run()
    {
        OpenStudentMenu();
    }
    private void OpenStudentMenu()
    {
        Console.Clear();
        Console.WriteLine(" ");
        Console.WriteLine("STUDERANDE");
        Console.WriteLine("----------------------------");
        Console.WriteLine("1. Visa alla studerande");
        Console.WriteLine("2. Lägg till studerande");
        Console.WriteLine("3. Ta bort studerande");
        Console.WriteLine(" ");
        Console.WriteLine("0. Tillbaka");
        Console.WriteLine("----------------------------");
        Console.WriteLine(" ");
        while (true)
        {
            var key = Console.ReadKey(true);

            switch (key.Key)
            {
                case ConsoleKey.D1:
                    ListStudents();
                    return;
                case ConsoleKey.D2:
                    RegisterStudent();
                    return;
                case ConsoleKey.D3:
                    RemoveStudent();
                    return;
                case ConsoleKey.D0:
                    return;
                default:
                    Console.WriteLine(" ");
                    Console.WriteLine("Måste välja ett av alternativen. Försök igen.");
                    Console.WriteLine(" ");
                    break;
            }
        }
    }
    private void ListStudents()
    {
        IReadOnlyList<Student> students = school.GetStudents();
        foreach (var s in students)
        {
            Console.WriteLine(s);
        }
    }
    private void RegisterStudent()
    {
        PersonalInfo info = InputHelper.ReadPersonalInfo();
        Student student = new(info.FirstName, info.LastName, info.PhoneNumber, info.SocialSecurityNumber, info.Address);
        school.AddPerson(student);
    }
    private void RemoveStudent()
    {
        Console.WriteLine(" ");
        Console.WriteLine("TA BORT STUDERANDE");
        Console.WriteLine("----------------------------");
        IReadOnlyList<Student> students = school.GetStudents();
        int i = 1;
        foreach (var s in students)
        {
            Console.WriteLine($"{i}. {s.FirstName} {s.LastName}");
            i++;
        }
        Console.WriteLine(" ");
        Console.WriteLine("0. Avbryt");
        Console.WriteLine(" ");

        var key = Console.ReadKey(true);

        if (key.Key == ConsoleKey.D0)
        {
            return;
        }

        if (Int32.TryParse(key.KeyChar.ToString(), out int result))
        {
            while (true)
            {
                Student student = students[result - 1];
                Console.WriteLine(" ");
                Console.WriteLine("Du har valt:");
                Console.WriteLine($"{student.FirstName} {student.LastName}");
                Console.WriteLine(" ");

                Console.WriteLine("Är du säker på att du vill ta bort personen? (j/n):");
                var keyConfirm = Console.ReadKey(true);

                switch (keyConfirm.Key)
                {
                    case ConsoleKey.J:
                        school.DeletePerson(student);
                        Console.WriteLine("Personen är borttagen.");
                        return;
                    case ConsoleKey.N:
                        return;
                    default:
                        Console.WriteLine(" ");
                        Console.WriteLine("Måste välja ett av alternativen. Försök igen.");
                        Console.WriteLine(" ");
                        break;
                }

            }
        }
    }
}
