using ExceptionThrown.IO.API.BuildingBlocks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ExceptionThrown.IO.API.Application.Events
{
    public class PostUpVotedEvent : IEvent
    {
        public Guid PostId { get; }
        public DateTimeOffset Created { get; }

        public PostUpVotedEvent(Guid postId) : this() =>
            (PostId) = (postId);

        private PostUpVotedEvent()
        {
            Created = DateTimeOffset.UtcNow;
        }
    }
}
