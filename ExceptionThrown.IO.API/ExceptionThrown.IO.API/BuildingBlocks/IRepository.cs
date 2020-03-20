using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ExceptionThrown.IO.API.BuildingBlocks
{
    public interface IRepository<TAggregate> where TAggregate : IAggregateRoot
    {
        Task Save(TAggregate aggregate);
    }
}
