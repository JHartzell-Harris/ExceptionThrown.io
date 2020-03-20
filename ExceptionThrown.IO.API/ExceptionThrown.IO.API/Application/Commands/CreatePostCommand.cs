using ExceptionThrown.IO.API.BuildingBlocks;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;

namespace ExceptionThrown.IO.API.Application.Commands
{
    /** TODO:
     *      - Add user
     */

    [DataContract]
    public class CreatePostCommand : IRequest<bool>
    {
        [DataMember]
        public string Title { get; private set; }

        [DataMember]
        public string Body { get; private set; }

        [DataMember]
        public Guid Id { get; private set; }

        public CreatePostCommand(Guid id, string body, string title) =>
            (Id, Body, Title) = (id, body, title);

        public CreatePostCommand() { }
    }
}
