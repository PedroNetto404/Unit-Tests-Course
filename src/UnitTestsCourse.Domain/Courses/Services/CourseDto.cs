namespace UnitTestsCourse.Domain.Courses.Services;

public record CourseDto(
    string Name,
    string Description,
    double Hours,
    string TargetGroup,
    decimal Price);
