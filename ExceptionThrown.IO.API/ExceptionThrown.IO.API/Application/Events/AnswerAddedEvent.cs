using ExceptionThrown.IO.API.BuildingBlocks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ExceptionThrown.IO.API.Application.Events
{
    public class AnswerAddedEvent : IEvent
    {
        public string Body { get; }

        public Guid Id { get; }

        public Guid PostId { get; }
        
        public int Votes { get; }

        public DateTimeOffset Created { get; }

        public AnswerAddedEvent(Guid id, string body, Guid postId) : this() =>
            (Id, Body, PostId) = (id, body, postId);

        protected AnswerAddedEvent()
        {
            Created = DateTimeOffset.UtcNow;
            Votes = 0;
        }
    }
}
