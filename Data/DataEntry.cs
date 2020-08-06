using System.ComponentModel.DataAnnotations;

namespace BlazorApp.Data
{
    public class DataEntry
    {
        [Required]
        [StringLength(256, ErrorMessage = "Entry is too long.")]
        public string Text { get; set; }
    }
}