using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using JobApplicationTracker.Api.Controllers;
using JobApplicationTracker.Api.Repositories;
using JobApplicationTracker.Api.Models;
using JobApplicationTracker.Api.DTOs;
using JobApplicationTracker.Tests.TestHelpers;

namespace JobApplicationTracker.Tests.Controllers;

public class JobApplicationsControllerTests
{
    private readonly Mock<IJobApplicationRepository> _mockRepository;
    private readonly Mock<ILogger<JobApplicationsController>> _mockLogger;
    private readonly JobApplicationsController _controller;

    public JobApplicationsControllerTests()
    {
        _mockRepository = new Mock<IJobApplicationRepository>();
        _mockLogger = new Mock<ILogger<JobApplicationsController>>();
        _controller = new JobApplicationsController(_mockRepository.Object, _mockLogger.Object);
    }

    #region GET Tests

    [Fact]
    public async Task GetJobApplications_ReturnsOkResult_WithListOfJobApplications()
    {
        // Arrange
        var jobApplications = TestDataFixtures.CreateSampleJobApplications();
        _mockRepository.Setup(repo => repo.GetAllAsync()).ReturnsAsync(jobApplications);

        // Act
        var result = await _controller.GetJobApplications();

        // Assert 
        Assert.NotNull(result);
        Assert.NotNull(result.Result);
        Assert.IsType<OkObjectResult>(result.Result);
        
        var okResult = (OkObjectResult)result.Result;
        var returnedData = okResult.Value as IEnumerable<JobApplicationDto>;
        Assert.NotNull(returnedData);
        Assert.Equal(3, returnedData.Count());
    }

    [Fact]
    public async Task GetJobApplication_WithValidId_ReturnsJobApplication()
    {
        // Arrange
        var jobApplication = TestDataFixtures.CreateSampleJobApplication(1);
        _mockRepository.Setup(repo => repo.GetByIdAsync(1)).ReturnsAsync(jobApplication);

        // Act
        var result = await _controller.GetJobApplication(1);

        // Assert 
        Assert.NotNull(result);
        Assert.NotNull(result.Result);
        Assert.IsType<OkObjectResult>(result.Result);
        
        var okResult = (OkObjectResult)result.Result;
        var returnedJobApplication = okResult.Value as JobApplicationDto;
        Assert.NotNull(returnedJobApplication);
        Assert.Equal(1, returnedJobApplication.Id);
    }

    [Fact]
    public async Task GetJobApplication_WithInvalidId_ReturnsNotFound()
    {
        // Arrange
        _mockRepository.Setup(repo => repo.GetByIdAsync(999)).ReturnsAsync((JobApplication?)null);

        // Act
        var result = await _controller.GetJobApplication(999);

        // Assert 
        Assert.NotNull(result);
        Assert.NotNull(result.Result);
        Assert.IsType<NotFoundObjectResult>(result.Result);
    }

    #endregion

    #region POST Tests

    [Fact]
    public async Task CreateJobApplication_WithValidData_ReturnsCreatedResult()
    {
        // Arrange
        var createDto = TestDataFixtures.CreateSampleCreateDto();
        var createdJobApplication = new JobApplication
        {
            Id = 1,
            Company = createDto.Company,
            Position = createDto.Position,
            Status = createDto.Status,
            DateApplied = createDto.DateApplied,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow
        };

        _mockRepository.Setup(repo => repo.CreateAsync(It.IsAny<JobApplication>())).ReturnsAsync(createdJobApplication);

        // Act
        var result = await _controller.CreateJobApplication(createDto);

        // Assert 
        Assert.NotNull(result);
        Assert.NotNull(result.Result);
        Assert.IsType<CreatedAtActionResult>(result.Result);
        
        var createdResult = (CreatedAtActionResult)result.Result;
        Assert.Equal(201, createdResult.StatusCode);
    }

    [Fact]
    public async Task CreateJobApplication_WithInvalidModelState_ReturnsBadRequest()
    {
        // Arrange
        var createDto = TestDataFixtures.CreateSampleCreateDto();
        _controller.ModelState.AddModelError("Company", "Company is required");

        // Act
        var result = await _controller.CreateJobApplication(createDto);

        // Assert 
        Assert.NotNull(result);
        Assert.NotNull(result.Result);
        Assert.IsType<BadRequestObjectResult>(result.Result);
    }

    #endregion

    #region PUT Tests

    [Fact]
    public async Task UpdateJobApplication_WithValidData_ReturnsOkResult()
    {
        // Arrange
        var updateDto = TestDataFixtures.CreateSampleUpdateDto();
        var updatedJobApplication = new JobApplication
        {
            Id = 1,
            Company = updateDto.Company,
            Position = updateDto.Position,
            Status = updateDto.Status,
            UpdatedAt = DateTime.UtcNow
        };

        _mockRepository.Setup(repo => repo.UpdateAsync(1, It.IsAny<JobApplication>())).ReturnsAsync(updatedJobApplication);

        // Act
        var result = await _controller.UpdateJobApplication(1, updateDto);

        // Assert 
        Assert.NotNull(result);
        Assert.NotNull(result.Result);
        Assert.IsType<OkObjectResult>(result.Result);
    }

    [Fact]
    public async Task UpdateJobApplication_WithInvalidId_ReturnsNotFound()
    {
        // Arrange
        var updateDto = TestDataFixtures.CreateSampleUpdateDto();
        _mockRepository.Setup(repo => repo.UpdateAsync(999, It.IsAny<JobApplication>())).ReturnsAsync((JobApplication?)null);

        // Act
        var result = await _controller.UpdateJobApplication(999, updateDto);

        // Assert 
        Assert.NotNull(result);
        Assert.NotNull(result.Result);
        Assert.IsType<NotFoundObjectResult>(result.Result);
    }

    #endregion

    #region DELETE Tests

    [Fact]
    public async Task DeleteJobApplication_WithValidId_ReturnsNoContent()
    {
        // Arrange
        _mockRepository.Setup(repo => repo.DeleteAsync(1)).ReturnsAsync(true);

        // Act
        var result = await _controller.DeleteJobApplication(1);

        // Assert 
        Assert.NotNull(result);
        Assert.IsType<NoContentResult>(result);
        
        var noContentResult = (NoContentResult)result;
        Assert.Equal(204, noContentResult.StatusCode);
    }

    [Fact]
    public async Task DeleteJobApplication_WithInvalidId_ReturnsNotFound()
    {
        // Arrange
        _mockRepository.Setup(repo => repo.DeleteAsync(999)).ReturnsAsync(false);

        // Act
        var result = await _controller.DeleteJobApplication(999);

        // Assert 
        Assert.NotNull(result);
        Assert.IsType<NotFoundObjectResult>(result);
    }

    #endregion
}