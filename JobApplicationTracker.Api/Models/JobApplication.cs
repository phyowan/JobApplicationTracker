using System.ComponentModel.DataAnnotations;

namespace JobApplicationTracker.Api.Models;

public class JobApplication
{
    public int Id { get; set; }
    
    [Required]
    [StringLength(200)]
    public string Company { get; set; } = string.Empty;
    
    [Required]
    [StringLength(200)]
    public string Position { get; set; } = string.Empty;
    
    [Required]
    public ApplicationStatus Status { get; set; }
    
    [Required]
    public DateTime DateApplied { get; set; }
    
    // [StringLength(500)]
    // public string? Description { get; set; }
    
    // [StringLength(100)]
    // public string? Location { get; set; }
    
    // [Range(0, double.MaxValue)]
    // public decimal? SalaryExpected { get; set; }
    
    // [StringLength(500)]
    // public string? Notes { get; set; }
    
    public DateTime CreatedAt { get; set; }
    
    public DateTime UpdatedAt { get; set; }
}

public enum ApplicationStatus
{
    Applied = 1,
    UnderReview = 2,
    Interview = 3,
    Offer = 4,
    Rejected = 5,
    Withdrawn = 6
}