using ExceptionThrown.IO.API.Application.Events;
using ExceptionThrown.IO.API.Application.Models;
using MediatR;
using Nest;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace ExceptionThrown.IO.API.Application.EventHandlers
{
    public class PostDomainEventHandler :
        INotificationHandler<PostCreatedEvent>,
        INotificationHandler<AnswerAddedEvent>,
        INotificationHandler<PostUpVotedEvent>
    {
        protected ElasticClient Client { get; }

        public PostDomainEventHandler(ElasticClient client) => 
            (Client) = (client);

        public async Task Handle(PostCreatedEvent @event, CancellationToken cancellationToken)
        {
            var post = new Post
            {
                Id = @event.Id,
                Body = @event.Body,
                LastModified = @event.Created,
                Title = @event.Title,
                Votes = @event.Votes
            };

            var response = await Client.IndexDocumentAsync(post);

            if (!response.IsValid)
            {
                // oops.....
            }
        }

        public async Task Handle(AnswerAddedEvent @event, CancellationToken cancellationToken)
        {
            var getResponse = await Client.GetAsync<Post>(@event.PostId);
            var post = getResponse.Source;

            post.Answers.Add(new Answer
            {
                Body = @event.Body,
                Id = @event.Id,
                Votes = 0
            });

            post.LastModified = @event.Created;

            var response = await Client.IndexDocumentAsync(post);

            if (!response.IsValid)
            {
                // oops....
            }
        }

        public async Task Handle(PostUpVotedEvent @event, CancellationToken cancellationToken)
        {
            var getResponse = await Client.GetAsync<Post>(@event.PostId);
            var post = getResponse.Source;

            post.LastModified = @event.Created;
            post.Votes++;

            var response = await Client.IndexDocumentAsync(post);

            if (!response.IsValid)
            {
                // oops.....
            }
        }
    }
}
