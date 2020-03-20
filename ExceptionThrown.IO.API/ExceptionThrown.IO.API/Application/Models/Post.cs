using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ExceptionThrown.IO.API.Application.Models
{
    public class Post
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Body { get; set; }
        public int Votes { get; set; }
        public DateTimeOffset LastModified { get; set; }

        public List<Answer> Answers { get; set; } = new List<Answer>();
    }

    public class Answer
    {
        public Guid Id { get; set; }
        public string Body { get; set; }
        public int Votes { get; set; }
    }
}
