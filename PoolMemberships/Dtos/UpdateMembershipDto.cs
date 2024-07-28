using System;

namespace PoolMemberships.Dtos;

public class UpdateMembershipDto
{
    public DateOnly StartDate { get; set; }
    public DateOnly EndDate { get; set; }
    public string? KeyFobId { get; set; }
    public bool Active { get; set; } = true;
    public string? Notes { get; set; }
}