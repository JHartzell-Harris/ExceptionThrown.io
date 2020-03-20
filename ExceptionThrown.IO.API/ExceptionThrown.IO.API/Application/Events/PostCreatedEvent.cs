using ExceptionThrown.IO.API.BuildingBlocks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ExceptionThrown.IO.API.Application.Events
{
    public class PostCreatedEvent : IEvent
    {
        public string Title { get; }

        public string Body { get; }

        public Guid Id { get; }

        public Guid QuestionId { get; }

        public int Votes { get; }

        public DateTimeOffset Created { get; }

        public PostCreatedEvent(Guid id, string body, string title) : this() =>
            (Id, Body, Title) = (id, body, title);

        private PostCreatedEvent()
        {
            QuestionId = Guid.NewGuid();
            Votes = 0;
            Created = DateTimeOffset.UtcNow;
        }
    }
}
