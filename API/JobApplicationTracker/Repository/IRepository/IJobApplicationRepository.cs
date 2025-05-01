using JobApplicationTracker.Models;
using System.Linq.Expressions;

namespace JobApplicationTracker.Repository.IRepository
{
    public interface IJobApplicationRepository : IRepository<JobApplication>
    {
        Task<JobApplication> UpdateAsync(JobApplication entity);        
    }
}
