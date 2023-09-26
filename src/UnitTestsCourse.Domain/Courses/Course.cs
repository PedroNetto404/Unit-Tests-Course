using UnitTestsCourse.Domain.Courses.Exceptions;

namespace UnitTestsCourse.Domain.Courses;

public class Course
{
    public Course(
        string name,
        string description,
        double hours,
        TargetGroup targetGroup,
        decimal price)
    {
        CourseWithInvalidHoursException.ThrowIfInvalid(hours);
        CourseWithInvalidNameException.ThrowIfInvalid(name);

        Name = name;
        Description = description;
        Hours = hours;
        TargetGroup = targetGroup;
        Price = price;
    }

    public string Name { get; private set; }
    public string Description { get; set; }
    public double Hours { get; private set; }
    public TargetGroup TargetGroup { get; private set; }
    public decimal Price { get; private set; }
}