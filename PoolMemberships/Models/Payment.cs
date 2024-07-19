using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PoolMemberships.Models;

public class Payment
{
    [Key]
    public int PaymentId { get; set; }
    
    [Required]
    [ForeignKey("Membership")]
    public int MembershipId { get; set; }
    
    public Membership Membership { get; set; } = new Membership();
    
    [Required]
    public decimal Amount { get; set; }
    
    [Required]
    public DateOnly PaymentDate { get; set; }
    
    [Column(TypeName = "text")]
    public string? Notes { get; set; }
    
}