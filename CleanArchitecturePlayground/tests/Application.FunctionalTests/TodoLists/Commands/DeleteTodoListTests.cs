using CleanArchitecturePlayground.Application.TodoLists.Commands.CreateTodoList;
using CleanArchitecturePlayground.Application.TodoLists.Commands.DeleteTodoList;
using CleanArchitecturePlayground.Domain.Entities;

using static CleanArchitecturePlayground.Application.FunctionalTests.Testing;

namespace CleanArchitecturePlayground.Application.FunctionalTests.TodoLists.Commands;
public class DeleteTodoListTests : BaseTestFixture
{
    [Test]
    public async Task ShouldRequireValidTodoListId()
    {
        var command = new DeleteTodoListCommand(99);
        await FluentActions.Invoking(() => SendAsync(command)).Should().ThrowAsync<NotFoundException>();
    }

    [Test]
    public async Task ShouldDeleteTodoList()
    {
        var listId = await SendAsync(new CreateTodoListCommand
        {
            Title = "New List"
        });

        await SendAsync(new DeleteTodoListCommand(listId));

        var list = await FindAsync<TodoList>(listId);

        list.Should().BeNull();
    }
}
