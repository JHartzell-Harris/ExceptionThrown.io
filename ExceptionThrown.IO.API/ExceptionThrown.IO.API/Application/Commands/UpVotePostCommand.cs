using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;

namespace ExceptionThrown.IO.API.Application.Commands
{
    [DataContract]
    public class UpVotePostCommand : IRequest<bool>
    {
        [DataMember]
        public Guid PostId { get; private set; }

        public UpVotePostCommand() { }
    }
}
