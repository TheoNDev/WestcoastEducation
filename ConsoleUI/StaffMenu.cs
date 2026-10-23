using System;
using WestcoastEducation.Enums;
using WestcoastEducation.InputModels;
using WestcoastEducation.Models;

namespace WestcoastEducation.ConsoleUI;

public class StaffMenu(School school)
{
    public void Run()
    {
        OpenStaffMenu();
    }
    private void OpenStaffMenu()
    {
        Console.Clear();
        Console.WriteLine(" ");
        Console.WriteLine("PERSONALHANTERING");
        Console.WriteLine("----------------------------");
        Console.WriteLine("1. Visa all personal");
        Console.WriteLine("2. Lägg till personal");
        Console.WriteLine("3. Ta bort personal");
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
                    ListStaff();
                    return;
                case ConsoleKey.D2:
                    Role? role = StaffRoleMenu();
                    if (role is not null)
                    {
                        RegisterStaff((Role)role);
                    }
                    return;
                case ConsoleKey.D3:
                    RemoveStaff();
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
    private static Role? StaffRoleMenu()
    {
        Console.WriteLine(" ");
        Console.WriteLine("VÄLJ PERSONALROLL");
        Console.WriteLine("----------------------------");
        Console.WriteLine("1. Lärare");
        Console.WriteLine("2. Utbildningsledare");
        Console.WriteLine("3. Administratör");
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
                    return Role.Lärare;
                case ConsoleKey.D2:
                    return Role.UtbildningsLedare;
                case ConsoleKey.D3:
                    return Role.Administratör;
                case ConsoleKey.D0:
                    return null;
                default:
                    Console.WriteLine(" ");
                    Console.WriteLine("Måste välja ett av alternativen. Försök igen.");
                    Console.WriteLine(" ");
                    break;
            }
        }
    }
    private void ListStaff()
    {
        IReadOnlyList<Staff> staff = school.GetStaff();
        foreach (var s in staff)
        {
            Console.WriteLine(" ");
            Console.WriteLine(s);
            Console.WriteLine(" ");
        }
    }
    private void RegisterStaff(Role role)
    {
        PersonalInfo info = InputHelper.ReadPersonalInfo();
        string fieldOfKnowledge = InputHelper.ReadFieldOfKnowledge();

        switch (role)
        {
            case Role.Lärare:
                {
                    List<Course> courses = SelectCourses(school.GetCourses());
                    Teacher teacher = new(info.FirstName, info.LastName, info.PhoneNumber, info.SocialSecurityNumber, info.Address, fieldOfKnowledge, courses);
                    school.AddPerson(teacher);
                    break;
                }
            case Role.UtbildningsLedare:
                {
                    DateTime employmentDate = InputHelper.ReadDate("Anställningsdatum");
                    List<Course> courses = SelectCourses(school.GetCourses());
                    EducationManager em = new(info.FirstName, info.LastName, info.PhoneNumber, info.SocialSecurityNumber, info.Address, fieldOfKnowledge, courses, employmentDate);
                    school.AddPerson(em);
                    break;
                }
            case Role.Administratör:
                {
                    DateTime employmentDate = InputHelper.ReadDate("Anställningsdatum");
                    List<Course> courses = SelectCourses(school.GetCourses());
                    Administrator admin = new(info.FirstName, info.LastName, info.PhoneNumber, info.SocialSecurityNumber, info.Address, fieldOfKnowledge, courses, employmentDate);
                    school.AddPerson(admin);
                    break;
                }
            default:
                break;
        }
    }

    private void RemoveStaff()
    {
        Console.WriteLine(" ");
        Console.WriteLine("TA BORT PERSONAL");
        Console.WriteLine("----------------------------");
        IReadOnlyList<Staff> staff = school.GetStaff();
        int i = 1;
        foreach (var s in staff)
        {
            Console.WriteLine($"{i}. {s.FirstName} {s.LastName}    {s.GetRole}");
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
                Staff staffMember = staff[result - 1];
                Console.WriteLine(" ");
                Console.WriteLine("Du har valt:");
                Console.WriteLine($"{staffMember.FirstName} {staffMember.LastName} - {staffMember.GetRole}");
                Console.WriteLine(" ");

                Console.WriteLine("Är du säker på att du vill ta bort personen? (j/n):");
                var keyConfirm = Console.ReadKey(true);

                switch (keyConfirm.Key)
                {
                    case ConsoleKey.J:
                        school.DeletePerson(staffMember);
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

    private static Course? SelectCourse(IReadOnlyList<Course> courses)
    {
        while (true)
        {
            Console.WriteLine(" ");
            Console.Write("Välj kurs: ");
            var key = Console.ReadKey(true);
            Console.WriteLine(" ");

            if (key.Key == ConsoleKey.D0)
            {
                return null;
            }

            if (Int32.TryParse(key.KeyChar.ToString(), out int result))
            {
                Console.WriteLine(" ");
                Console.WriteLine($"{courses[result - 1].Title} har lags till.");
                Console.WriteLine(" ");
                return courses[result - 1];
            }

            Console.WriteLine(" ");
            Console.WriteLine("Måste välja ett av alternativen. Försök igen.");
            Console.WriteLine(" ");
        }
    }
    private static List<Course> SelectCourses(IReadOnlyList<Course> courses)
    {
        Console.WriteLine(" ");
        Console.WriteLine("VÄLJ KURS");
        Console.WriteLine("----------------------------");

        int i = 1;
        foreach (var c in courses)
        {
            Console.WriteLine($"{i}. {c.Title} {c.CourseNumber}");
            i++;
        }

        Console.WriteLine(" ");
        Console.WriteLine("0. Klar");
        Console.WriteLine(" ");

        List<Course> selectedCourses = [];

        while (true)
        {
            Course? course = SelectCourse(courses);
            if (course is not null)
            {
                selectedCourses.Add(course);
            }
            else
            {
                return selectedCourses;
            }
        }
    }
}
