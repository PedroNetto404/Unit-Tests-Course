using UnitTestsCourse.Domain.Abstractions.Exceptions;

namespace UnitTestsCourse.Domain.Courses.Exceptions;

public class CourseWithInvalidHoursException : DomainException
{
    public CourseWithInvalidHoursException() : base(
        "Course cannot have hours less than one"
        )
    {
    }

    public static void ThrowIfInvalid(double hours)
    {
        if (hours < 1)
        {
            throw new CourseWithInvalidHoursException();
        }
    }
}