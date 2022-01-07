namespace GradeBook
{
    public class Student : NamedObject
    {
        public Student(string name, int age, string subject): base(name){
            Console.WriteLine($"Student name is {name}, age is {age}, subject is {subject}");
        }
    }
}