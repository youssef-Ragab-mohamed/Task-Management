//using TaskManagement.Application.Dtos;
//using TaskManagement.Application.Interfaces;
//using TaskManagement.Infrastructure.Repositries;
//using TaskManagement.Models;

//namespace TaskManagement.Application.Services
//{
//    public class AttachmentService : IAttachmentService
//    {
//        public AttachmentService(IUnitOfWork unitOfWork)
//        {
//            UnitOfWork = unitOfWork;
//        }

//        public IUnitOfWork UnitOfWork { get; }

//        public async Task AddAttachmentAsync(AttachmentDto attachmentDto)
//        {
//            var fileExtension = Path.GetExtension(attachmentDto.File.FileName).TrimStart('.').ToLower();

          
//            if (!Enum.TryParse<FileTypes>(fileExtension, out var fileType))
//                throw new Exception("Invalid file type. Please upload a supported file.");
//            string newFileName = $"{Guid.NewGuid()}.{fileExtension.ToLower()}";
//            string filePath = Path.Combine("Uploads", newFileName);
//            using (var stream = new FileStream(filePath, FileMode.Create))
//            {
//                await attachmentDto.File.CopyToAsync(stream);
//            }

//            // Create an attachment entity
//            var attachment = new Attachment
//            {
//                FilePath = filePath,
//                FileSize = attachmentDto.File.Length,
//                FileType = fileType, // Use the validated enum value
//                TaskId = attachmentDto.TaskId,
//                UserId = attachmentDto.UserId
//            };



//            await UnitOfWork.Attachments.AddAsync(attachment);
//        }

//        public async Task DeleteAttachmentAsync(int id)
//        {
//             await UnitOfWork.Attachments.DeleteAsync(id);
//        }

//        public async Task<List<Attachment>> GetAllAttachmentsAsync()
//        {
//             return await UnitOfWork.Attachments.GetAllAsync(att=>att.Task,att =>att.User);
//        }

//        public async Task<Attachment> GetAttachmentByIdAsync(int id)
//        {
//           return await UnitOfWork.Attachments.GetByIdAsync(id);
//        }

//        public async Task<List<Attachment>> GetAttachmentsByTaskIdAsync(int taskId)
//        {
//            return await UnitOfWork.Attachments.GetAllAsync(att => att.TaskId == taskId);
//        }

//        public async Task UpdateAttachmentAsync(Attachment attachment)
//        {
//           await UnitOfWork.Attachments.UpdateAsync(attachment);
//        }

//        public Task UpdateAttachmentAsync(AttachmentDto attachmentDto)
//        {
//            throw new NotImplementedException();
//        }
//    }
//}
