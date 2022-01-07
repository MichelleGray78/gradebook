using System.Collections.Generic;
namespace GradeBook
{
    //Fires a message stating that the grade was added as long as the grade is valid
    public delegate void GradeAddedDelegate(object sender, EventArgs args);

    public interface IBook
    {
        //Things that book needs - building blocks for book
        void AddGrade(double grade); //Need to be able to add a grade
        Statistics GetStatistics(); //Need to be able to work out stats
        string Name { get; } //Needs a name
        event GradeAddedDelegate GradeAdded; //Lets user know when a grade has been added
    }


    //Inherits from NamedObject and NamedObject MUST have a name
    public abstract class Book : NamedObject, IBook
    {
        //Tells base (NamedObject) that a name is there
        public Book(string name) : base(name)
        {
        }

        public abstract event GradeAddedDelegate GradeAdded;

        //Abstract method (Building block) - We don't know what will happen with this in each implementation we just know that it's required.
        public abstract void AddGrade(double grade);

        public abstract Statistics GetStatistics();
    }


    //Inherits from Book and must have a name
    public class InMemoryBook : Book
    {
        //Constructors
        //here the name is defined to satisfy the base class requirements
        public InMemoryBook(string name) : base(name) 
        {
            //A list is defined to store the grades
            grades = new List<double>();
            Name = name;
        }

        //Methods
        public void AddGrade(char letter)
        {
            switch (letter)
            {
                case 'A':
                    AddGrade(90);
                    break;
                case 'B':
                    AddGrade(80);
                    break;
                case 'C':
                    AddGrade(70);
                    break;
                case 'D':
                    AddGrade(60);
                    break;
                default:
                    AddGrade(0);
                    break;
            }
        }
        //Overrides the AddGrade method in the base class with its own implementation methods
        public override void AddGrade(double grade)
        {
            if (grade < 0 || grade > 100)
            {
                throw new ArgumentException($"Invalid {nameof(grade)}");
            }
            else
            {
                //Grades are added to the list
                grades.Add(grade);
                if (GradeAdded != null)
                {
                    //Event is fired to show the user the grade has been added - only if the grade is not null
                    GradeAdded(this, new EventArgs());
                }
            }

        }

        public override event GradeAddedDelegate GradeAdded;

        public override Statistics GetStatistics()
        {
            var result = new Statistics();
        
            for (var i = 0; i < grades.Count; i++)
            {
                result.Add(grades[i]);
            }

            return result;

        }
        private List<double> grades;

        public const string CATEGORY = "Science";
    }
}