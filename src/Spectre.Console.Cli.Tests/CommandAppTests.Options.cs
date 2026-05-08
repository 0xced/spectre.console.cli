namespace Spectre.Console.Tests.Unit.Cli;

public sealed partial class CommandAppTests
{
    public sealed class Options
    {
        [Fact]
        public void Should_Throw_If_Required_Option_Is_Missing()
        {
            // Given
            var fixture = new CommandAppTester();
            fixture.Configure(config =>
            {
                config.AddCommand<GenericCommand<RequiredOptionsSettings>>("test");
                config.PropagateExceptions();
            });

            // When
            var result = Record.Exception(() => fixture.Run("test"));

            // Then
            result.ShouldBeOfType<CommandRuntimeException>()
                .And(ex =>
                    ex.Message.ShouldBe("Command 'test' is missing required option 'foo'."));
        }

        [Fact]
        public void Should_Throw_If_Required_Option_Is_Missing_For_Default_Command()
        {
            // Given
            var fixture = new CommandAppTester();
            fixture.SetDefaultCommand<GenericCommand<RequiredOptionsSettings>>();
            fixture.Configure(config =>
            {
                config.PropagateExceptions();
            });

            // When
            var result = Record.Exception(() => fixture.Run());

            // Then
            result.ShouldBeOfType<CommandRuntimeException>()
                .And(ex =>
                    ex.Message.ShouldBe("Missing required option 'foo'."));
        }
    }
}