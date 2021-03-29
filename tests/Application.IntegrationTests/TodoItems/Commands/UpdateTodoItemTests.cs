using clean_architecture.Application.Common.Exceptions;
using clean_architecture.Application.TodoItems.Commands.CreateTodoItem;
using clean_architecture.Application.TodoItems.Commands.UpdateTodoItem;
using clean_architecture.Application.TodoLists.Commands.CreateTodoList;
using clean_architecture.Domain.Entities;
using FluentAssertions;
using System.Threading.Tasks;
using NUnit.Framework;
using System;

namespace clean_architecture.Application.IntegrationTests.TodoItems.Commands
{
    using static Testing;

    public class UpdateTodoItemTests : TestBase
    {
        [Test]
        public void ShouldRequireValidTodoItemId()
        {
            var command = new UpdateTodoItemCommand
            {
                Id = 99,
                Title = "New Title"
            };

            FluentActions.Invoking(() =>
                SendAsync(command)).Should().Throw<NotFoundException>();
        }

        [Test]
        public async Task ShouldUpdateTodoItem()
        {
            var userId = await RunAsDefaultUserAsync();

            var listId = await SendAsync(new CreateTodoListCommand
            {
                Title = "New List"
            });

            var itemId = await SendAsync(new CreateTodoItemCommand
            {
                ListId = listId,
                Title = "New Item"
            });

            var command = new UpdateTodoItemCommand
            {
                Id = itemId,
                Title = "Updated Item Title"
            };

            await SendAsync(command);

            var item = await FindAsync<TodoItem>(itemId);

            item.Title.Should().Be(command.Title);
            item.LastModifiedBy.Should().NotBeNull();
            item.LastModifiedBy.Should().Be(userId);
            item.LastModified.Should().NotBeNull();
            item.LastModified.Should().BeCloseTo(DateTime.Now, 1000);
        }
    }
}
