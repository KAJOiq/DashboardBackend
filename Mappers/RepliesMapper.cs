using Ticket.Dtos.RepliesDto;
using Ticket.Models;

namespace Ticket.Mappers;
public static class RepliesMapper
{
    public static GetRepliesDto ToRepliesDto(this Replies repliesModel)
    {
        return new GetRepliesDto(
            repliesModel.Id,
            repliesModel.TicketId,
            repliesModel.Description,
            repliesModel.Note,
            repliesModel.Photo_url,
            repliesModel.AssignId,
            repliesModel.AssignorId,
            repliesModel.CreatedAt
        );
    }
    public static Replies ToRepliesFormCreateDTO(this RepliesRequestDto repliesDto)
    {
        return new Replies
        {
            TicketId = repliesDto.TicketId,
            Description = repliesDto.Description,
            Note = repliesDto.Note,
            AssignorId = repliesDto.AssignorId,
            AssignId = repliesDto.AssignId
        };
    }
}