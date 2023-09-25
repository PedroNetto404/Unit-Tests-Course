using ExpectedObjects;
using Xunit;

namespace UnitTestsCourse.Tests.Courses;

public class CoursesTests
{
    [Fact]
    public void MustCreateCourse()
    {
        var expectedCourse = new
        {
            Name = "C# Fundamentals",
            Hours = (double)80,
            TargetGroup = TargetGroup.Programmer,
            Price = (decimal)1000
        };

        var course = new Course(
            expectedCourse.Name,
            expectedCourse.Hours,
            expectedCourse.TargetGroup,
            expectedCourse.Price);

        expectedCourse.ToExpectedObject().ShouldMatch(course);
    }

    [Theory]
    [InlineData("")]
    [InlineData(null)]
    public void CourseCannotHaveNullOrEmptyName(string invalidName)
    {
        Assert.Throws<ArgumentException>(() =>
        {
            var course = new Course(
                invalidName,
                (double)80,
                TargetGroup.Programmer,
                (decimal)1000);
        });
    }

    [Theory]
    [InlineData(0)]
    [InlineData(-1)]
    public void CourseCannotHaveHoursLessThanOne(double invalidHours)
    {
        Assert.Throws<ArgumentException>(() =>
        {
            var course = new Course(
                "C# Fundamentals",
                 invalidHours,
                TargetGroup.Programmer,
                (decimal)1000);
        });
    }
}

public enum TargetGroup
{
    Programmer,
    Designer,
    Manager
}

public class Course
{
    public Course(
        string name,
        double hours,
        TargetGroup targetGroup,
        decimal price)
    {
        if (string.IsNullOrEmpty(name))
        {
            throw new ArgumentException("Invalid name");
        }

        if (hours < 1)
        {
            throw new ArgumentException("Invalid hours");
        }

        Name = name;
        Hours = hours;
        TargetGroup = targetGroup;
        Price = price;
    }

    public string Name { get; private set; }
    public double Hours { get; private set; }
    public TargetGroup TargetGroup { get; private set; }
    public decimal Price { get; private set; }
}