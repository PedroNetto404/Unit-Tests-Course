using Bogus;
using Moq;
using UnitTestsCourse.Domain.Courses;
using UnitTestsCourse.Domain.Courses.Services;
using UnitTestsCourse.Tests.Builders;
using Xunit;

namespace UnitTestsCourse.Tests.Courses;

public class CourseSaverTests
{
    #region Setup

    private readonly CourseDto _courseDto;

    public CourseSaverTests()
    {
        var faker = new Faker();

        _courseDto = new(
            faker.Random.Word(),
            faker.Lorem.Paragraph(),
            faker.Random.Double(1, 100),
            faker.PickRandom<TargetGroup>().ToString(),
            faker.Random.Decimal(100, 1000));
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