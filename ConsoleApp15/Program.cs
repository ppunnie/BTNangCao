using System;
using System.Collections.Generic;

public interface IStudentManager<T>
{
    List<T> FindTopStudent();
    List<T> FilterStudents(StudentFilter<T> filter);
}

public class Student
{
    private string name;
    private string id;
    private double GPA;

    public Student(string name, string id, double gPA)
    {
        this.name = name;
        this.id = id;
        GPA = gPA;
    }
    public string GetName()
    {
        return name;
    }
    public string GetID()
    {
        return id;
    }
    public double GetGPA()
    {
        return GPA;
    }



}

public delegate bool StudentFilter<T>(T student);
public class StudentManager<T> : IStudentManager<T> where T : Student
{
    private List<T> students = new List<T>();

    public void AddStudent(T student)
    {
        students.Add(student);
    }
    public List<T> FilterStudents(StudentFilter<T> filter)
    {
        List<T> filteredStudents = new List<T>();
        foreach (var student in students)
        {
            if (filter(student))
            {
                filteredStudents.Add(student);
            }
        }
        return filteredStudents;
    }

    public List<T> FindTopStudent()
    {
        List<T> TopStudent = new List<T>();
        double topGPA = students[0].GetGPA();
        foreach (var student in students)
        {
            if (student.GetGPA() > topGPA)
            {
                TopStudent.Clear();
                TopStudent.Add(student);
                topGPA = student.GetGPA();
            }
            else if (student.GetGPA() == topGPA)
            {
                TopStudent.Add(student);
            }
        }
        return TopStudent;
    }
}

class Program
{
    static void Main(string[] args)
    {
        var manager = new StudentManager<Student>();

        Student student1 = new Student("Tan", "1", 7.0);
        Student student2 = new Student("Phat", "2", 8.0);
        Student student3 = new Student("Phuoc", "3", 8.0);
        Student student4 = new Student("Quan", "4", 8.5);
        Student student5 = new Student("Huy", "5", 8.5);


        manager.AddStudent(student1);
        manager.AddStudent(student2);
        manager.AddStudent(student3);
        manager.AddStudent(student4);
        manager.AddStudent(student5);


        Console.WriteLine("\nFiltering by GPA: ");
        var filteredStudents = manager.FilterStudents(student => student.GetGPA() >= 8.0);
        foreach (var student in filteredStudents)
        {
            Console.WriteLine("Name: {0}, ID: {1}, GPA: {2}", student.GetName(), student.GetID(), student.GetGPA());
        }

        Console.WriteLine("\nFinding top student:");
        var topStudent = manager.FindTopStudent();
        foreach (var student in topStudent)
        {
            Console.WriteLine("Name: {0}, ID:{1}, GPA: {2}", student.GetName(), student.GetID(), student.GetGPA());
        }
        Console.ReadKey();
    }
}
