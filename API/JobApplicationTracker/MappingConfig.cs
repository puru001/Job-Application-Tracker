using AutoMapper;
using JobApplicationTracker.DTO;
using JobApplicationTracker.Models;

namespace JobApplicationTracker
{
    public class MappingConfig :Profile
    {
        public MappingConfig()
        {
            CreateMap<JobApplication, JobApplicationDTO>();
            CreateMap<JobApplicationDTO, JobApplication>();

            CreateMap<JobApplication, JobApplicationCreateDTO>().ReverseMap();
            CreateMap<JobApplication, JobApplicationUpdateDTO>().ReverseMap();
        }
    }
}
