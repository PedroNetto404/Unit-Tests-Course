using ExpectedObjects;
using UnitTestsCourse.Domain.Courses;
using UnitTestsCourse.Tests.Builders;
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

        var course = 
            CourseBuilder.Instance
                .WithName(expectedCourse.Name)
                .WithHours(expectedCourse.Hours)
                .WithTargetGroup(expectedCourse.TargetGroup)
                .WithPrice(expectedCourse.Price)
                .Build();

        expectedCourse.ToExpectedObject().ShouldMatch(course);
    }

    [Theory]
    [InlineData("")]
    [InlineData(null)]
    public void CourseCannotHaveNullOrEmptyName(string invalidName)
    {
        Assert.Throws<ArgumentException>(() =>
        {
            var course = 
                CourseBuilder.Instance
                    .WithName(invalidName)
                    .Build();
        });
    }

    [Theory]
    [InlineData(0)]
    [InlineData(-1)]
    public void CourseCannotHaveHoursLessThanOne(double invalidHours)
    {
        Assert.Throws<ArgumentException>(() =>
        {
            var course = 
                CourseBuilder.Instance
                    .WithHours(invalidHours)
                    .Build();
        });
    }
}