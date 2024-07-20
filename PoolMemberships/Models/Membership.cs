using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PoolMemberships.Models;

public class Membership
{
    [Key]
    public int MembershipId { get; set; }
    
    [Required]
    [ForeignKey("Person")]
    public int PersonId { get; set; }
    
    public Person Person { get; set; } = new Person();
    
    [Required]
    public DateOnly StartDate { get; set; }
    
    [Required]
    public DateOnly EndDate { get; set; }
    
    [MaxLength(50)]
    public string? KeyFobId { get; set; }

    [Required] 
    public bool Active { get; set; } = true;
    
    [Column(TypeName = "text")]
    public string? Notes { get; set; }
    
    public ICollection<Payment> Payments { get; set; } = new List<Payment>();

}