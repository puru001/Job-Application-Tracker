using JobApplicationTracker.Data;
using JobApplicationTracker.Models;
using JobApplicationTracker.Repository.IRepository;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace JobApplicationTracker.Repository
{
    public class JobApplicationRepository : Repository<JobApplication>, IJobApplicationRepository
    {
        private readonly ApplicationDbContext _db;
        public JobApplicationRepository(ApplicationDbContext db): base(db)
        {
            _db = db;                
        }
        public async Task<JobApplication> UpdateAsync(JobApplication entity)
        {
            entity.UpdatedDate = DateTime.Now;
            _db.JobApplications.Update(entity);
            await _db.SaveChangesAsync();
            return entity;
        }
    }
}
