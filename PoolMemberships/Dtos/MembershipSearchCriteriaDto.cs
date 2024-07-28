namespace PoolMemberships.Dtos;

public class MembershipSearchCriteriaDto
{
    public string? KeyFobId { get; set; }
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public bool? Active { get; set; }
}