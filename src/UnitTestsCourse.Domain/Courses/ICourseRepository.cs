using UnitTestsCourse.Domain.Courses;

namespace UnitTestsCourse.Tests.Courses;

public interface ICourseRepository
{
    Task<Course> GetByNameAsync(string courseName);
    Task SaveAsync(Course course);
}