using JobApplicationTracker.Api.Models;

namespace JobApplicationTracker.Api.Repositories;

public interface IJobApplicationRepository
{
    Task<IEnumerable<JobApplication>> GetAllAsync();
    Task<JobApplication?> GetByIdAsync(int id);
    Task<JobApplication> CreateAsync(JobApplication jobApplication);
    Task<JobApplication?> UpdateAsync(int id, JobApplication jobApplication);
    Task<bool> DeleteAsync(int id);
    Task<bool> ExistsAsync(int id);
    Task<IEnumerable<JobApplication>> GetByStatusAsync(ApplicationStatus status);
    Task<IEnumerable<JobApplication>> GetByCompanyAsync(string company);
}