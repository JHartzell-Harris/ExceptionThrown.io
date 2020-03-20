using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ExceptionThrown.IO.API.Application.Commands;
using ExceptionThrown.IO.API.Application.Queries;
using ExceptionThrown.IO.API.BuildingBlocks;
using ExceptionThrown.IO.API.Domain.PostAggregate;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ExceptionThrown.IO.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PostsController : ControllerBase
    {
        public IMediator Mediator { get; }

        public IPostQueries PostQueries { get; }

        public PostsController(IMediator mediator, IPostQueries postQueries)
        {
            Mediator = mediator;
            PostQueries = postQueries;
        }

        [HttpPost("create-post")]
        public async Task<IActionResult> CreatePost([FromBody] CreatePostCommand command)
        {
            await Mediator.Send(command);

            return Ok();
        }

        [HttpPost("add-answer")]
        public async Task<IActionResult> AddAnswer([FromBody] AddAnswerCommand command)
        {
            await Mediator.Send(command);

            return Ok();
        }

        [HttpPost("up-vote-post")]
        public async Task<IActionResult> UpVotePost([FromBody] UpVotePostCommand command)
        {
            await Mediator.Send(command);

            return Ok();
        }

        [HttpGet]
        public async Task<IActionResult> GetPosts()
        {
            var posts = await PostQueries.GetPosts();

            return Ok(posts);
        }
    }
}