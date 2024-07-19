using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PoolMemberships.Models;

public class Person
{
    [Key]
    public int PersonId { get; set; }
    
    [Required]
    [MaxLength(50)]
    public string? FirstName { get; set; }
    
    [Required]
    [MaxLength(50)]
    public string? LastName { get; set; }
    
    [EmailAddress]
    [MaxLength(255)]
    public string? Email { get; set; }
    
    [Phone]
    [MaxLength(10)]
    public string? Phone { get; set; }
    
    [MaxLength(255)]
    public string? Address { get; set; }
    
    [MaxLength(100)]
    public string? City { get; set; }
    
    [MaxLength(100)]
    public string? State { get; set; }
    
    [MaxLength(9)]
    public string? Zip { get; set; }
    
    [Column(TypeName = "text")]
    public string? Notes { get; set; }
}