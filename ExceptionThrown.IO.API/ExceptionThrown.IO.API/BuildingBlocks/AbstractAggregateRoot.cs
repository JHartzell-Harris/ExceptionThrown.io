using ExceptionThrown.IO.API.Utils;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ExceptionThrown.IO.API.BuildingBlocks
{
    public abstract class AbstractAggregateRoot : IAggregateRoot
    {
        protected virtual List<IEvent> UncommittedEvents { get; set; } = new List<IEvent>();

        public virtual int Version { get; protected set; }

        public abstract Guid Id { get; protected set; }

        public IReadOnlyCollection<IEvent> GetUncommittedEvents() => UncommittedEvents.AsReadOnly();

        public void ClearUncommittedEvents()
        {
            UncommittedEvents.Clear();
        }
    }
}
