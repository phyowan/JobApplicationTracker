using System.ComponentModel.DataAnnotations;
using JobApplicationTracker.Api.Models;

namespace JobApplicationTracker.Api.DTOs;

public class CreateJobApplicationDto
{
    [Required(ErrorMessage = "Company name is required")]
    [StringLength(200, ErrorMessage = "Company name cannot exceed 200 characters")]
    public string Company { get; set; } = string.Empty;
    
    [Required(ErrorMessage = "Position is required")]
    [StringLength(200, ErrorMessage = "Position cannot exceed 200 characters")]
    public string Position { get; set; } = string.Empty;
    
    [Required(ErrorMessage = "Status is required")]
    public ApplicationStatus Status { get; set; }
    
    [Required(ErrorMessage = "Date applied is required")]
    public DateTime DateApplied { get; set; }
    
    [StringLength(500, ErrorMessage = "Description cannot exceed 500 characters")]
    public string? Description { get; set; }
    
    [StringLength(100, ErrorMessage = "Location cannot exceed 100 characters")]
    public string? Location { get; set; }
    
    [Range(0, double.MaxValue, ErrorMessage = "Salary expected must be a positive value")]
    public decimal? SalaryExpected { get; set; }
    
    [StringLength(500, ErrorMessage = "Notes cannot exceed 500 characters")]
    public string? Notes { get; set; }
}

public class UpdateJobApplicationDto
{
    [Required(ErrorMessage = "Company name is required")]
    [StringLength(200, ErrorMessage = "Company name cannot exceed 200 characters")]
    public string Company { get; set; } = string.Empty;
    
    [Required(ErrorMessage = "Position is required")]
    [StringLength(200, ErrorMessage = "Position cannot exceed 200 characters")]
    public string Position { get; set; } = string.Empty;
    
    [Required(ErrorMessage = "Status is required")]
    public ApplicationStatus Status { get; set; }
    
    [Required(ErrorMessage = "Date applied is required")]
    public DateTime DateApplied { get; set; }
    
    [StringLength(500, ErrorMessage = "Description cannot exceed 500 characters")]
    public string? Description { get; set; }
    
    [StringLength(100, ErrorMessage = "Location cannot exceed 100 characters")]
    public string? Location { get; set; }
    
    [Range(0, double.MaxValue, ErrorMessage = "Salary expected must be a positive value")]
    public decimal? SalaryExpected { get; set; }
    
    [StringLength(500, ErrorMessage = "Notes cannot exceed 500 characters")]
    public string? Notes { get; set; }
}

public class JobApplicationDto
{
    public int Id { get; set; }
    public string Company { get; set; } = string.Empty;
    public string Position { get; set; } = string.Empty;
    public ApplicationStatus Status { get; set; }
    public DateTime DateApplied { get; set; }
    public string? Description { get; set; }
    public string? Location { get; set; }
    public decimal? SalaryExpected { get; set; }
    public string? Notes { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
}