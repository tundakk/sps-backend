using Mapster;
using Microsoft.Extensions.Logging;
using sps.BLL.Services.Base;
using sps.BLL.Services.Interfaces;
using sps.DAL.Repos.Implementations;
using sps.DAL.Repos.Interfaces;
using sps.Domain.Model.Dtos.EducationalProgram;
using sps.Domain.Model.Entities;
using sps.Domain.Model.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace sps.BLL.Services.Implementations
{
    public class EducationalProgramService : BaseService<EducationalProgramModel, EducationalProgram, IEducationalProgramRepo>, IEducationalProgramService
    {
     public EducationalProgramService(IEducationalProgramRepo programRepository, ILogger<EducationalProgramService> logger)
            : base(programRepository, logger)
        {
        }
      
        
    }
}