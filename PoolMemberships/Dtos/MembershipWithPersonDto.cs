using System;

namespace PoolMemberships.Dtos;

public class MembershipWithPersonDto
{
    public int MembershipId { get; set; }
    public DateOnly StartDate { get; set; }
    public DateOnly EndDate { get; set; }
    public bool Active { get; set; }

    public string? KeyFobId { get; set; }
    public string? Notes { get; set; }
    public string PersonFirstName { get; set; }
    public string PersonLastName { get; set; }
    public string? PersonEmail { get; set; }
    public string? PersonPhoneNumber { get; set; }
    public string? PersonAddress { get; set; }
    public string? PersonCity { get; set; }
    public string? PersonState { get; set; }
    public string? PersonZip { get; set; }
}