using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace Car_wash.Models;

public class Reviews{
    [Key]
    public int ReviewID{get;set;}

    [ForeignKey("Orders")]
    public int OrderID{get;set;}

    [Required]
    [Range(0,10, ErrorMessage = "Rating can't be greater than 10 or less than 0")]
    public float Rating{get;set;}

    [Required]
    [StringLength(500, ErrorMessage = "Review can't be longer than 500 characters.")]
    public string Review_comment{get;set;}

    public virtual Orders? Orders{get;set;}
}