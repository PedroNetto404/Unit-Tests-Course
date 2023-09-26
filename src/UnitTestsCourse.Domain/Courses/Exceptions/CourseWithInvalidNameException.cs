using UnitTestsCourse.Domain.Abstractions.Exceptions;

namespace UnitTestsCourse.Domain.Courses.Exceptions;

public class CourseWithInvalidNameException : DomainException
{
    public CourseWithInvalidNameException() : base("Cannot create course with invalid name") { }

    public static void ThrowIfInvalid(string name)
    {
        if (string.IsNullOrEmpty(name))
        {
            throw new CourseWithInvalidNameException(); 
        }
    }
}