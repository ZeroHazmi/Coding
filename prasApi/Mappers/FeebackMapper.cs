using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using prasApi.Dtos.Feedback;

namespace prasApi.Mappers
{
    public static class FeebackMapper
    {
        public static FeedbackDto ToFeedbackDto(this Models.Feedback feedback)
        {
            return new FeedbackDto
            {
                Id = feedback.Id,
                UserId = feedback.UserId,
                Rating = feedback.Rating,
                Comment = feedback.Comment,
                CreatedAt = feedback.CreatedAt
            };
        }
    }
}