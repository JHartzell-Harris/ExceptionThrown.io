using ExceptionThrown.IO.API.Application.Models;
using Nest;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ExceptionThrown.IO.API.Application.Queries
{
    public class PostQueries : IPostQueries
    {
        public ElasticClient Client { get; }

        public PostQueries(ElasticClient client) =>
            (Client) = (client);

        public async Task<IEnumerable<Post>> GetPosts()
        {
            var query = await Client.SearchAsync<Post>(x =>
                x.MatchAll()
            );

            return query.Documents;
        }
    }
}
