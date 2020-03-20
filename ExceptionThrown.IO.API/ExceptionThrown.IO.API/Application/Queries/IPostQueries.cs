using ExceptionThrown.IO.API.Application.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ExceptionThrown.IO.API.Application.Queries
{
    public interface IPostQueries
    {
        Task<IEnumerable<Post>> GetPosts();
    }
}
