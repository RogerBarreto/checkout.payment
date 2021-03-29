using clean_architecture.Domain.Common;
using System.Threading.Tasks;

namespace clean_architecture.Application.Common.Interfaces
{
    public interface IDomainEventService
    {
        Task Publish(DomainEvent domainEvent);
    }
}
