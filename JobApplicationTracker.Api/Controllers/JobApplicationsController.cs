using Microsoft.AspNetCore.Mvc;
using JobApplicationTracker.Api.DTOs;
using JobApplicationTracker.Api.Models;
using JobApplicationTracker.Api.Repositories;

namespace JobApplicationTracker.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
[Produces("application/json")]
public class JobApplicationsController : ControllerBase
{
    private readonly IJobApplicationRepository _repository;
    private readonly ILogger<JobApplicationsController> _logger;

    public JobApplicationsController(IJobApplicationRepository repository, ILogger<JobApplicationsController> logger)
    {
        _repository = repository;
        _logger = logger;
    }

    /// <summary>
    /// Gets all job applications
    /// </summary>
    /// <returns>A list of job applications</returns>
    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<JobApplicationDto>), StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<JobApplicationDto>>> GetJobApplications()
    {
        try
        {
            var jobApplications = await _repository.GetAllAsync();
            var jobApplicationDtos = jobApplications.Select(MapToDto);
            return Ok(jobApplicationDtos);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error occurred while retrieving job applications");
            return StatusCode(500, "An error occurred while retrieving job applications");
        }
    }

    /// <summary>
    /// Gets a specific job application by ID
    /// </summary>
    /// <param name="id">The job application ID</param>
    /// <returns>The job application</returns>
    [HttpGet("{id}")]
    [ProducesResponseType(typeof(JobApplicationDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<JobApplicationDto>> GetJobApplication(int id)
    {
        try
        {
            var jobApplication = await _repository.GetByIdAsync(id);

            if (jobApplication == null)
            {
                return NotFound($"Job application with ID {id} not found");
            }

            return Ok(MapToDto(jobApplication));
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error occurred while retrieving job application with ID {Id}", id);
            return StatusCode(500, "An error occurred while retrieving the job application");
        }
    }

    /// <summary>
    /// Creates a new job application
    /// </summary>
    /// <param name="createDto">The job application data</param>
    /// <returns>The created job application</returns>
    [HttpPost]
    [ProducesResponseType(typeof(JobApplicationDto), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<JobApplicationDto>> CreateJobApplication([FromBody] CreateJobApplicationDto createDto)
    {
        try
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var jobApplication = new JobApplication
            {
                Company = createDto.Company,
                Position = createDto.Position,
                Status = createDto.Status,
                DateApplied = createDto.DateApplied
                
            };

            var createdJobApplication = await _repository.CreateAsync(jobApplication);
            var jobApplicationDto = MapToDto(createdJobApplication);

            return CreatedAtAction(
                nameof(GetJobApplication),
                new { id = createdJobApplication.Id },
                jobApplicationDto);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error occurred while creating job application");
            return StatusCode(500, "An error occurred while creating the job application");
        }
    }

    /// <summary>
    /// Updates an existing job application
    /// </summary>
    /// <param name="id">The job application ID</param>
    /// <param name="updateDto">The updated job application data</param>
    /// <returns>The updated job application</returns>
    [HttpPut("{id}")]
    [ProducesResponseType(typeof(JobApplicationDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<JobApplicationDto>> UpdateJobApplication(int id, [FromBody] UpdateJobApplicationDto updateDto)
    {
        try
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var jobApplication = new JobApplication
            {
                Company = updateDto.Company,
                Position = updateDto.Position,
                Status = updateDto.Status,
                DateApplied = updateDto.DateApplied
            
            };

            var updatedJobApplication = await _repository.UpdateAsync(id, jobApplication);

            if (updatedJobApplication == null)
            {
                return NotFound($"Job application with ID {id} not found");
            }

            return Ok(MapToDto(updatedJobApplication));
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error occurred while updating job application with ID {Id}", id);
            return StatusCode(500, "An error occurred while updating the job application");
        }
    }

    /// <summary>
    /// Deletes a job application
    /// </summary>
    /// <param name="id">The job application ID</param>
    /// <returns>No content if successful</returns>
    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteJobApplication(int id)
    {
        try
        {
            var deleted = await _repository.DeleteAsync(id);

            if (!deleted)
            {
                return NotFound($"Job application with ID {id} not found");
            }

            return NoContent();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error occurred while deleting job application with ID {Id}", id);
            return StatusCode(500, "An error occurred while deleting the job application");
        }
    }

    private static JobApplicationDto MapToDto(JobApplication jobApplication)
    {
        return new JobApplicationDto
        {
            Id = jobApplication.Id,
            Company = jobApplication.Company,
            Position = jobApplication.Position,
            Status = jobApplication.Status,
            DateApplied = jobApplication.DateApplied,
            CreatedAt = jobApplication.CreatedAt,
            UpdatedAt = jobApplication.UpdatedAt
        };
    }
}