using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ExceptionThrown.IO.API.BuildingBlocks
{
    public interface IAggregateRoot
    {
        public int Version { get; }
        public IReadOnlyCollection<IEvent> GetUncommittedEvents();
        public void ClearUncommittedEvents();
        public Guid Id { get; }
    }
}
