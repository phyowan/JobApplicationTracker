using JobApplicationTracker.Api.Models;
using JobApplicationTracker.Api.DTOs;

namespace JobApplicationTracker.Tests.TestHelpers;

public static class TestDataFixtures
{
    public static JobApplication CreateSampleJobApplication(int id = 1)
    {
        return new JobApplication
        {
            Id = id,
            Company = "TechCorp Inc.",
            Position = "Software Engineer",
            Status = ApplicationStatus.Applied,
            DateApplied = new DateTime(2025, 9, 15)
        };
    }

    public static List<JobApplication> CreateSampleJobApplications()
    {
        return new List<JobApplication>
        {
            new JobApplication
            {
                Id = 1,
                Company = "TechCorp Inc.",
                Position = "Software Engineer",
                Status = ApplicationStatus.Applied,
                DateApplied = new DateTime(2025, 9, 15),
                CreatedAt = DateTime.UtcNow.AddDays(-5),
                UpdatedAt = DateTime.UtcNow.AddDays(-5)
            },
            new JobApplication
            {
                Id = 2,
                Company = "StartupXYZ",
                Position = "Senior Developer",
                Status = ApplicationStatus.Interview,
                DateApplied = new DateTime(2025, 9, 10),
                CreatedAt = DateTime.UtcNow.AddDays(-10),
                UpdatedAt = DateTime.UtcNow.AddDays(-2)
            },
            new JobApplication
            {
                Id = 3,
                Company = "BigTech Corp",
                Position = "Staff Engineer",
                Status = ApplicationStatus.Rejected,
                DateApplied = new DateTime(2025, 8, 25),
                CreatedAt = DateTime.UtcNow.AddDays(-25),
                UpdatedAt = DateTime.UtcNow.AddDays(-15)
            }
        };
    }

    public static CreateJobApplicationDto CreateSampleCreateDto()
    {
        return new CreateJobApplicationDto
        {
            Company = "NewTech Solutions",
            Position = "Full Stack Developer",
            Status = ApplicationStatus.Applied,
            DateApplied = new DateTime(2025, 9, 18)
        };
    }

    public static UpdateJobApplicationDto CreateSampleUpdateDto()
    {
        return new UpdateJobApplicationDto
        {
            Company = "TechCorp Inc. (Updated)",
            Position = "Senior Software Engineer",
            Status = ApplicationStatus.Interview,
            DateApplied = new DateTime(2025, 9, 15)
           
        };
    }

    public static JobApplicationDto CreateSampleJobApplicationDto(int id = 1)
    {
        var jobApp = CreateSampleJobApplication(id);
        return new JobApplicationDto
        {
            Id = jobApp.Id,
            Company = jobApp.Company,
            Position = jobApp.Position,
            Status = jobApp.Status,
            DateApplied = jobApp.DateApplied,
            CreatedAt = jobApp.CreatedAt,
            UpdatedAt = jobApp.UpdatedAt
        };
    }
}