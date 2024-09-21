using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using prasApi.Dtos.Feedback;
using prasApi.Interfaces;
using prasApi.Mappers;

namespace prasApi.Controllers
{
    [ApiController]
    [Route("api/feedback")]
    public class FeedbackController : ControllerBase
    {
        private readonly IFeedbackRepository _feedbackRepository;
        public FeedbackController(IFeedbackRepository feedbackRepository)
        {
            _feedbackRepository = feedbackRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var feedbacks = await _feedbackRepository.GetAllAsync();
            var feedbackDto = feedbacks.Select(x => x.ToFeedbackDto());
            return Ok(feedbackDto);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var feedback = await _feedbackRepository.GetByIdAsync(id);
            if (feedback == null)
            {
                return NotFound();
            }

            return Ok(feedback);
        }

        [HttpPost("create")]
        public async Task<IActionResult> Create([FromBody] FeedbackCreateDto feedbackCreateDto)
        {
            var feedback = new Models.Feedback
            {
                UserId = feedbackCreateDto.UserId,
                Rating = feedbackCreateDto.Rating,
                Comment = feedbackCreateDto.Comment
            };

            var createdFeedback = await _feedbackRepository.CreateAsync(feedback);
            return CreatedAtAction(nameof(GetById), new { id = createdFeedback.Id }, createdFeedback);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] FeedbackUpdateDto feedbackUpdateDto)
        {
            var feedback = await _feedbackRepository.GetByIdAsync(id);
            if (feedback == null)
            {
                return NotFound();
            }

            feedback.UserId = feedbackUpdateDto.UserId;
            feedback.Rating = feedbackUpdateDto.Rating;
            feedback.Comment = feedbackUpdateDto.Comment;

            var updatedFeedback = await _feedbackRepository.UpdateAsync(id, feedback);
            return Ok(updatedFeedback);
        }

    }
}