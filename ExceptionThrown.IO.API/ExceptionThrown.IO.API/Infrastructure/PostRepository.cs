using EventStore.ClientAPI;
using ExceptionThrown.IO.API.BuildingBlocks;
using ExceptionThrown.IO.API.Domain.PostAggregate;
using ExceptionThrown.IO.API.Utils;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ExceptionThrown.IO.API.Infrastructure
{
    public class PostRepository : AbstractRepository<Post>, IPostRepository
    {
		public PostRepository(IEventStoreConnection eventStoreConnection, IMediator mediator) : base(eventStoreConnection, mediator)
		{
		}

		public async Task<Post> GetById(Guid id)
		{
			var events = new List<IEvent>();
			StreamEventsSlice currentSlice;
			var nextSliceStart = (long)StreamPosition.Start;
			var streamName = GetStreamName<Post>(id);
			do
			{
				currentSlice = await EventStoreConnection
					.ReadStreamEventsForwardAsync(streamName, nextSliceStart, 200, false);
				nextSliceStart = currentSlice.NextEventNumber;
				events.AddRange(currentSlice.Events.Select(x => x.DeserializeEvent()));
			} while (!currentSlice.IsEndOfStream);
			var aggregate = Post.Rehydrate(events);
			return aggregate;
		}
	}
}
