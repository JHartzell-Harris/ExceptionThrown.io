using ExceptionThrown.IO.API.Application.Commands;
using ExceptionThrown.IO.API.Infrastructure;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace ExceptionThrown.IO.API.Application.CommandHandlers
{
    public class AddAnswerCommandHandler : IRequestHandler<AddAnswerCommand, bool>
    {
        public IPostRepository Repository { get; }

        public AddAnswerCommandHandler(IPostRepository repository)
        {
            Repository = repository;
        }

        public async Task<bool> Handle(AddAnswerCommand request, CancellationToken cancellationToken)
        {
            var post = await Repository.GetById(request.PostId);

            post.AddAnswer(request.Id, request.Body);

            await Repository.Save(post);

            return true;
        }
    }
}
