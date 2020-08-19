using System.ComponentModel.DataAnnotations;
///<summary> Just Takes Data</summary> 
namespace BlazorApp.Services
{
    public class DataEntry
    {
        [Required]
        [StringLength(256, ErrorMessage = "Entry is too long.")]
        public string Text { get; set; }
        public string Time { get; set; }
    }
}