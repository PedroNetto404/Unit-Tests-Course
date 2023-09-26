namespace UnitTestsCourse.Tests.Courses;

public record CourseDto(
    string Name,
    string Description,
    double Hours,
    string TargetGroup,
    decimal Price);
