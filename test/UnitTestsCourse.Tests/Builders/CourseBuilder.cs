using Bogus;
using UnitTestsCourse.Domain.Courses;

namespace UnitTestsCourse.Tests.Builders;

internal class CourseBuilder
{
    public CourseBuilder()
    {
        var faker = new Faker();

        _name = faker.Random.Word();
        _description = faker.Lorem.Paragraph();
        _hours = faker.Random.Double(1, 100);
        _targetGroup = faker.PickRandom<TargetGroup>();
        _price = faker.Random.Decimal(100, 1000);
    }

    private string _name;
    private string _description;
    private double _hours;
    private TargetGroup _targetGroup;
    private decimal _price;

    public static CourseBuilder Instance => new();

    public CourseBuilder WithName(string name)
    {
        _name = name;
        return this;
    }

    public CourseBuilder WithDescription(string description)
    {
        _description = description;
        return this;
    }

    public CourseBuilder WithHours(double hours)
    {
        _hours = hours;
        return this;
    }

    public CourseBuilder WithTargetGroup(TargetGroup targetGroup)
    {
        _targetGroup = targetGroup;
        return this;
    }

    public CourseBuilder WithPrice(decimal price)
    {
        _price = price;
        return this;
    }

    public Course Build()
    {
        return new(
            _name, 
            _description, 
            _hours, 
            _targetGroup, 
            _price);
    }
}