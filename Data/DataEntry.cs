using System.ComponentModel.DataAnnotations;
namespace BlazorApp.Data{
    public class DataEntry{
    [Required]
    [StringLength(32, ErrorMessage = "Entry is too long.")]
    public string Text { get; set; }  
    }
}