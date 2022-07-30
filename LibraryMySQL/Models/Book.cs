namespace LibraryMySQL.Models;

public class Book
{
    public int Id { get; set; }
    [Required(ErrorMessage = "Book title is required.")]
    public string Title { get; set; } = String.Empty;

    [Required(ErrorMessage = "Book authors are required.")]
    public string Authors { get; set; } = String.Empty;
    [Required(ErrorMessage = "Please select realese date.")]
    [DataType(DataType.Date)]
    [Display(Name = "Release date")]
    [DisplayFormat(DataFormatString = "{0:dd.MM.yyyy}", ApplyFormatInEditMode = true)]
    public DateTime ReleaseDate { get; set; }
    [Required]
    [StringLength(13, MinimumLength = 13)]
    public string ISBN { get; set; } = String.Empty;
    [Required]
    [StringLength(4)]
    public string Format { get; set; } = String.Empty;
    [Required(ErrorMessage = "Enter a number between 1 and 15000.")]
    [Range(1, 15000)]
    public int Pages { get; set; }
    public string Description { get; set; } = String.Empty;
}
