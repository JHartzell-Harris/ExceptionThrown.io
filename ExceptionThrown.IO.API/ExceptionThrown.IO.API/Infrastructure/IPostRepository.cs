using ExceptionThrown.IO.API.BuildingBlocks;
using ExceptionThrown.IO.API.Domain.PostAggregate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ExceptionThrown.IO.API.Infrastructure
{
    public interface IPostRepository : IRepository<Post>
    {
        Task<Post> GetById(Guid id);
    }
}
