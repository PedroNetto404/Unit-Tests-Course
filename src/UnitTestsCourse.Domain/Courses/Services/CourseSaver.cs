using UnitTestsCourse.Domain.Courses;
using UnitTestsCourse.Tests.Courses;

namespace UnitTestsCourse.Domain.Courses.Services;

public class CourseSaver
{
    private readonly ICourseRepository _courseRepository;

    public CourseSaver(ICourseRepository courseRepository) =>
        _courseRepository = courseRepository;


    public async Task SaveAsync(CourseDto courseDto)
    {
        if (!Enum.TryParse(typeof(TargetGroup), courseDto.TargetGroup, out var targetGroup))
        {
            throw new ArgumentException("Invalid target group");
        }

        var existingCourseWithName = await _courseRepository.GetByNameAsync(courseDto.Name);
        if (existingCourseWithName != null)
        {
            throw new ArgumentException("Course with this name already exists");
        }

        var course = new Course(
            courseDto.Name,
            courseDto.Description,
            courseDto.Hours,
            (TargetGroup)targetGroup!,
            courseDto.Price);

        await _courseRepository.SaveAsync(course);
    }
}