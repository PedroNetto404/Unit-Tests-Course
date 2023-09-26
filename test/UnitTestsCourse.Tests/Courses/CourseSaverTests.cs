using Bogus;
using Moq;
using UnitTestsCourse.Domain.Courses;
using UnitTestsCourse.Tests.Builders;
using Xunit;

namespace UnitTestsCourse.Tests.Courses;

public class CourseSaverTests
{
    private readonly CourseDto _courseDto;

    #region Setup

    public CourseSaverTests()
    {
        var faker = new Faker();

        _courseDto = new CourseDto
        {
            Description = faker.Lorem.Paragraph(),
            Hours = faker.Random.Double(1, 100),
            Name = faker.Random.Word(),
            Price = faker.Random.Decimal(100, 1000),
            TargetGroup = faker.PickRandom<TargetGroup>().ToString()
        };
    }

    #endregion

    [Fact]
    public async Task MustNotSaveCourseWithPreviouslyRegisteredName()
    {
        // Arrange
        var course = CourseBuilder.Instance
                .WithName(_courseDto.Name)
                .Build();

        var courseRepository = new Mock<ICourseRepository>();
        courseRepository.Setup(r => r.GetByNameAsync(_courseDto.Name)).ReturnsAsync(course);

        var courseSaver = new CourseSaver(courseRepository.Object);

        // Act & Assert
        await Assert.ThrowsAsync<ArgumentException>(() => courseSaver.SaveAsync(_courseDto));
        courseRepository.Verify(r => r.GetByNameAsync(course.Name), Times.Once);
        courseRepository.Verify(r => r.SaveAsync(It.IsAny<Course>()), Times.Never);
    }
}