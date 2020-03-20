using ExceptionThrown.IO.API.Application.Commands;
using ExceptionThrown.IO.API.BuildingBlocks;
using ExceptionThrown.IO.API.Domain.PostAggregate;
using ExceptionThrown.IO.API.Infrastructure;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace ExceptionThrown.IO.API.Application.CommandHandlers
{
    public class CreatePostCommandHandler : IRequestHandler<CreatePostCommand, bool>
    {
        private IPostRepository Repository { get; }

        public CreatePostCommandHandler(IPostRepository repository)
        {
            Repository = repository;
        }

        public async Task<bool> Handle(CreatePostCommand command, CancellationToken cancellationToken)
        {
            var post = Post.CreatePost(
                command.Id,
                command.Body,
                command.Title
            );

            await Repository.Save(post);

            return true;
        }
    }
}
