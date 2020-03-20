using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;

namespace ExceptionThrown.IO.API.Application.Commands
{
    [DataContract]
    public class AddAnswerCommand : IRequest<bool>
    {
        [DataMember]
        public Guid PostId { get; private set; }

        [DataMember]
        public Guid Id { get; private set; }

        [DataMember]
        public string Body { get; private set; }

        public AddAnswerCommand() { }
    }
}
