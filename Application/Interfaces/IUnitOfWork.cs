using TaskManagement.Models;

namespace TaskManagement.Application.Interfaces
{
    public interface IUnitOfWork
    {
        IGenericRepositry<User> Users { get; }
        IGenericRepositry<TaskEntity> Tasks { get; }
        IGenericRepositry<Attachment> Attachments { get; }
        IGenericRepositry<TaskHistory> TaskHistories { get; }
        Task CompleteAsync();

    }
}
