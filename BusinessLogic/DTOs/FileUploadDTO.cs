using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace BusinessLogic.DTOs
{
    public class InFileUploadDTO
    {
        public string Name { get; set; }

        [Required]
        public IFormFile File { get; set; }
    }
}
