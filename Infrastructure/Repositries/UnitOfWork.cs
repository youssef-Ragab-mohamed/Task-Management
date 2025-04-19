using Microsoft.EntityFrameworkCore;
using TaskManagement.Application.Interfaces;
using TaskManagement.Models;

namespace TaskManagement.Infrastructure.Repositries
{
    public class UnitOfWork : IUnitOfWork
    {

        public AppDbContext _Context { get; }

        public IGenericRepositry<User> Users { get; }

        public IGenericRepositry<TaskEntity> Tasks { get; }

        public IGenericRepositry<Attachment> Attachments { get; }

        public IGenericRepositry<TaskHistory> TaskHistories { get; }
        public UnitOfWork(AppDbContext Context)
        {
            _Context = Context;
            Users = new GenericRepositry<User>(_Context);
            Tasks = new GenericRepositry<TaskEntity>(_Context);
            Attachments = new GenericRepositry<Attachment>(_Context);
            TaskHistories = new GenericRepositry<TaskHistory>(_Context);
        }

        public async Task  CompleteAsync()
        {
            await _Context.SaveChangesAsync();
        }
    }
}
