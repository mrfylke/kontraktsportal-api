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
    public void Api_ShouldNot_Depend_On_Application()
    {
        var result = Types
            .InAssemblies(Assemblies)
            .That()
            .ResideInNamespace("Api")
            .ShouldNot()
            .HaveDependencyOn("Application")
            .GetResult();

        LogFailingTypes(result);

        result.IsSuccessful.Should().BeTrue();
    }

    [Fact]
    public void Api_ShouldNot_Depend_On_Domain()
    {
        var result = Types
            .InAssemblies(Assemblies)
            .That()
            .ResideInNamespace("Api")
            .ShouldNot()
            .HaveDependencyOn("Domain")
            .GetResult();

        LogFailingTypes(result);

        result.IsSuccessful.Should().BeTrue();
    }

    [Fact]
    public void Api_ShouldNot_Depend_On_Infrastructure()
    {
        var result = Types
            .InAssemblies(Assemblies)
            .That()
            .ResideInNamespace("Api")
            .ShouldNot()
            .HaveDependencyOn("Infrastructure")
            .GetResult();

        LogFailingTypes(result);

        result.IsSuccessful.Should().BeTrue();
    }

    [Fact]
    public void Application_ShouldNot_Depend_On_Infrastructure()
    {
        var result = Types
            .InAssemblies(Assemblies)
            .That()
            .ResideInNamespace("Application")
            .ShouldNot()
            .HaveDependencyOn("Infrastructure")
            .GetResult();

        LogFailingTypes(result);

        result.IsSuccessful.Should().BeTrue();
    }

    [Fact]
    public void Domain_ShouldNot_Depend_On_Infrastructure()
    {
        var result = Types
            .InAssemblies(Assemblies)
            .That()
            .ResideInNamespace("Domain")
            .ShouldNot()
            .HaveDependencyOn("Infrastructure")
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