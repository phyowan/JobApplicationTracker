using Microsoft.EntityFrameworkCore;
using JobApplicationTracker.Api.Data;
using JobApplicationTracker.Api.Models;

namespace JobApplicationTracker.Api.Repositories;

public class JobApplicationRepository : IJobApplicationRepository
{
    private readonly ApplicationDbContext _context;

    public JobApplicationRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<JobApplication>> GetAllAsync()
    {
        return await _context.JobApplications
            .OrderByDescending(ja => ja.DateApplied)
            .ToListAsync();
    }

    public async Task<JobApplication?> GetByIdAsync(int id)
    {
        return await _context.JobApplications
            .FirstOrDefaultAsync(ja => ja.Id == id);
    }

    public async Task<JobApplication> CreateAsync(JobApplication jobApplication)
    {
        _context.JobApplications.Add(jobApplication);
        await _context.SaveChangesAsync();
        return jobApplication;
    }

    public async Task<JobApplication?> UpdateAsync(int id, JobApplication jobApplication)
    {
        var existingApplication = await _context.JobApplications
            .FirstOrDefaultAsync(ja => ja.Id == id);

        if (existingApplication == null)
        {
            return null;
        }

        existingApplication.Company = jobApplication.Company;
        existingApplication.Position = jobApplication.Position;
        existingApplication.Status = jobApplication.Status;
        existingApplication.DateApplied = jobApplication.DateApplied;
        
        await _context.SaveChangesAsync();
        return existingApplication;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var jobApplication = await _context.JobApplications
            .FirstOrDefaultAsync(ja => ja.Id == id);

        if (jobApplication == null)
        {
            return false;
        }

        _context.JobApplications.Remove(jobApplication);
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<bool> ExistsAsync(int id)
    {
        return await _context.JobApplications
            .AnyAsync(ja => ja.Id == id);
    }

    public async Task<IEnumerable<JobApplication>> GetByStatusAsync(ApplicationStatus status)
    {
        return await _context.JobApplications
            .Where(ja => ja.Status == status)
            .OrderByDescending(ja => ja.DateApplied)
            .ToListAsync();
    }

    public async Task<IEnumerable<JobApplication>> GetByCompanyAsync(string company)
    {
        return await _context.JobApplications
            .Where(ja => ja.Company.ToLower().Contains(company.ToLower()))
            .OrderByDescending(ja => ja.DateApplied)
            .ToListAsync();
    }
}