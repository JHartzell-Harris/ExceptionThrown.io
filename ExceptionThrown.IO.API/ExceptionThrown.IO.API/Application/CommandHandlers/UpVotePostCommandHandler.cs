using ExceptionThrown.IO.API.Application.Commands;
using ExceptionThrown.IO.API.Application.Events;
using ExceptionThrown.IO.API.Infrastructure;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace ExceptionThrown.IO.API.Application.CommandHandlers
{
    public class UpVotePostCommandHandler : IRequestHandler<UpVotePostCommand, bool>
    {
        public IPostRepository Repository { get; }

        public UpVotePostCommandHandler(IPostRepository repository)
        {
            Repository = repository;
        }

        public async Task<bool> Handle(UpVotePostCommand command, CancellationToken cancellationToken)
        {
            var post = await Repository.GetById(command.PostId);

            post.UpVoteQuestion();

            await Repository.Save(post);

            return true;
        }
    }
}
