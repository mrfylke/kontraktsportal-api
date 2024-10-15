using System.Reflection;
using FluentAssertions;
using MediatR;
using NetArchTest.Rules;
using Xunit.Abstractions;

namespace Other;

public class ArchitectureTest(ITestOutputHelper testOutputHelper)
{
    private static readonly Assembly[] Assemblies =
    [
        Assembly.Load("Api"),
        Assembly.Load("Application"),
        Assembly.Load("Domain"),
        Assembly.Load("Infrastructure")
    ];

    [Fact]
    public void IRequests_Should_HaveNameEndingWith_Command_Or_Query()
    {
        var result = Types
            .InAssemblies(Assemblies)
            .That()
            .ImplementInterface(typeof(IRequest<>))
            .Should()
            .HaveNameEndingWith("Command")
            .Or()
            .HaveNameEndingWith("Query")
            .GetResult();
        testOutputHelper.WriteLine($"{result}");

        result.IsSuccessful.Should().BeTrue();
    }

    [Fact]
    public void Api_ShouldNot_Depend_On_Any_Layer()
    {
        var result = Types
            .InAssemblies(Assemblies)
            .That()
            .ResideInNamespace("Api")
            .ShouldNot()
            .HaveDependencyOnAny("Application", "Domain", "Infrastructure")
            .GetResult();

        // Note: Program.cs has top-level statements that are not in the namespace Api. However, Api depends on
        // Infrastructure one time, and one time only, for setting up dependency injection.

        LogFailingTypes(result);

        result.IsSuccessful.Should().BeTrue();
    }

    [Fact]
    public void Application_ShouldNot_Depend_On_Api_Or_Infrastructure()
    {
        var result = Types
            .InAssemblies(Assemblies)
            .That()
            .ResideInNamespace("Application")
            .ShouldNot()
            .HaveDependencyOnAny("Api", "Infrastructure")
            .GetResult();

        LogFailingTypes(result);

        result.IsSuccessful.Should().BeTrue();
    }

    [Fact]
    public void Domain_ShouldNot_Depend_On_Any_Layer()
    {
        var result = Types
            .InAssemblies(Assemblies)
            .That()
            .ResideInNamespace("Domain")
            .ShouldNot()
            .HaveDependencyOnAny("Api", "Application", "Infrastructure")
            .GetResult();

        LogFailingTypes(result);

        result.IsSuccessful.Should().BeTrue();
    }

    private void LogFailingTypes(TestResult result)
    {
        if (result.FailingTypes == null) return;

        testOutputHelper.WriteLine("These types failed:");
        foreach (var failingType in result.FailingTypes)
            testOutputHelper.WriteLine($"{failingType}");
    }
}