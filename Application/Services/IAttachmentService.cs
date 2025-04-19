using TaskManagement.Application.Dtos;
using TaskManagement.Models;

namespace TaskManagement.Application.Services
{
    public interface IAttachmentService
    {
        Task<List<Attachment>> GetAllAttachmentsAsync();
        Task<Attachment> GetAttachmentByIdAsync(int id);
        Task<List<Attachment>> GetAttachmentsByTaskIdAsync(int taskId);
        Task UpdateAttachmentAsync(AttachmentDto attachmentDto);
        Task AddAttachmentAsync(AttachmentDto attachmentDto);
        Task DeleteAttachmentAsync(int id);
    }
}
