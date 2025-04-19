using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TaskManagement.Models
{
    public class Attachment
    {
        [Key]
        public int Id { get; set; }
  
        [Required]
        [StringLength(500, ErrorMessage = "File path cannot exceed 500 characters.")]
        public string FilePath { get; set; }

        [Required]
        [Range(1, 10*1024*1024, ErrorMessage = "File size must be between 1 byte and 10 MB.")]
        public long FileSize { get; set; }

        [Required]
        [EnumDataType(typeof(FileTypes), ErrorMessage = "Invalid file type.")]
        public FileTypes FileType { get; set; }

        [Required]
        [ForeignKey(nameof(Task))]
        public int TaskId { get; set; }

        public virtual TaskEntity Task { get; set; }

        [Required]
        [ForeignKey(nameof(User))]
        public int UserId { get; set; }

        public virtual User User { get; set; }
    }

    public enum FileTypes
    {
        pdf,
        png,
        jpeg,
        docx,
       xlsx,
        txt
    }
}
