using EventStore.ClientAPI;
using ExceptionThrown.IO.API.Utils;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ExceptionThrown.IO.API.BuildingBlocks
{
    public abstract class AbstractRepository<TAggregate> : IRepository<TAggregate> where TAggregate : IAggregateRoot
    {
        public IEventStoreConnection EventStoreConnection { get; }

        public IMediator Mediator { get; }

        protected AbstractRepository(IEventStoreConnection eventStoreConnection, IMediator mediator) =>
            (EventStoreConnection, Mediator) = (eventStoreConnection, mediator);

        public async Task Save(TAggregate aggregate)
        {
            var commitId = Guid.NewGuid();
            var events = aggregate.GetUncommittedEvents().ToArray();
            if (events.Any() == false)
                return;
            var streamName = GetStreamName(aggregate.GetType(), aggregate.Id);
            var originalVersion = aggregate.Version - events.Count();
            var expectedVersion = originalVersion == 0 ? ExpectedVersion.NoStream : originalVersion - 1;
            var commitHeaders = new Dictionary<string, object>
            {
                {"CommitId", commitId},
                {"AggregateClrType", aggregate.GetType().AssemblyQualifiedName}
            };
            var eventsToSave = events.Select(e => e.ToEventData(commitHeaders)).ToList();
            await EventStoreConnection.AppendToStreamAsync(streamName, expectedVersion, eventsToSave);

            await DispatchEvents(events);

            aggregate.ClearUncommittedEvents();
        }

        protected string GetStreamName<T>(Guid id)
        {
            return GetStreamName(typeof(T), id);
        }

        protected string GetStreamName(Type type, Guid id)
        {
            return string.Format("{0}-{1}", type.Name, id);
        }

        protected async Task DispatchEvents(IEnumerable<INotification> events)
        {
            foreach (var @event in events)
                await Mediator.Publish(@event);
        }
    }
}
