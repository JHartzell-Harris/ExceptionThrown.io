using ExceptionThrown.IO.API.Application.Events;
using ExceptionThrown.IO.API.BuildingBlocks;
using ExceptionThrown.IO.API.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ExceptionThrown.IO.API.Domain.PostAggregate
{
    /**
     * TODO:
     *     - Inherit IAggregateRoot 
     */
    public class Post : AbstractAggregateRoot
    {
        private List<Answer> _answers = new List<Answer>();

        public DateTimeOffset LastModified { get; protected set; }

        public Question Question { get; protected set; }

        public override Guid Id { get; protected set; }

        public string Title { get; protected set; }

        private Post() { }

        public static Post CreatePost(Guid id, string body, string title)
        {
            if (id.Equals(Guid.Empty))
                throw new InvalidOperationException($"{nameof(id)} must be a non-empty GUID");

            if (string.IsNullOrWhiteSpace(body))
                throw new InvalidOperationException($"{nameof(body)} must be a non-empty, not null string");

            if (string.IsNullOrWhiteSpace(title))
                throw new InvalidOperationException($"{nameof(title)} must be a non-empty, not null string");

            var post = new Post();

            post.Publish(new PostCreatedEvent(id, body, title));

            return post;
        }

        public static Post Rehydrate(IEnumerable<IEvent> events)
        {
            var post = new Post();

            foreach (var e in events)
                post.Apply(e);

            return post;
        }

        public void AddAnswer(Guid id, string body)
        {
            if (id.Equals(Guid.Empty))
                throw new InvalidOperationException($"{nameof(id)} must be a non-empty GUID");

            if (string.IsNullOrWhiteSpace(body))
                throw new InvalidOperationException($"{nameof(body)} must be a non-empy, not null string");

            if (_answers.Any(x => x.Id.Equals(id)))
                throw new InvalidOperationException($"There is already an answer with the ID {id}");

            Publish(new AnswerAddedEvent(id, body, Id));
        }

        public void AddCommentToQuestion() { }

        public void AddCommentToAnswer() { }

        public void UpVoteQuestion() => Publish(new PostUpVotedEvent(Id));

        public void UpVoteAnswer() { }

        public void DownVoteQuestion() { }

        public void DownVoteAnswer() { }

        private void Publish(IEvent e)
        {
            UncommittedEvents.Add(e);
            Apply(e);
        }

        private void Apply(IEvent e)
        {
            Version++;
            RedirectToWhen.InvokeCommand(this, e);
        }

        private void When(PostUpVotedEvent e)
        {
            Question.Votes++;
            LastModified = e.Created;
        }

        private void When(AnswerAddedEvent e)
        {
            LastModified = e.Created;
            _answers.Add(new Answer
            {
                Id = e.Id,
                Body = e.Body,
                Votes = 0,
                Post = this
            });
        }

        private void When(PostCreatedEvent e)
        {
            Id = e.Id;
            Title = e.Title;
            LastModified = e.Created;
            Question = new Question
            {
                Id = e.QuestionId,
                Body = e.Body,
                Votes = 0,
                Post = this
            };
        }
    }

    public class Question : IEntity 
    {
        public int Votes { get; set; }

        public Guid Id { get; set; }

        public string Body { get; set; }

        public Post Post { get; set; }
    }

    public class Answer: IEntity
    {
        public int Votes { get; set; }

        public Guid Id { get; set; }

        public string Body { get; set; }

        public Post Post { get; set; }
    }
}
