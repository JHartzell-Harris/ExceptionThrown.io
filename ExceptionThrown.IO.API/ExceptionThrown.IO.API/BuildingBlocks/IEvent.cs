using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ExceptionThrown.IO.API.BuildingBlocks
{
    public interface IEvent : INotification
    {
        DateTimeOffset Created { get; }
    }
}
