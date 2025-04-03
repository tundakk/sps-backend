using Microsoft.EntityFrameworkCore;
using sps.DAL.DataModel;
using sps.DAL.Repos.Base;
using sps.DAL.Repos.Interfaces;
using sps.Domain.Model.Dtos.EducationalProgram;
using sps.Domain.Model.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace sps.DAL.Repos.Implementations
{
    public class EducationalProgramRepo : BaseRepo<EducationalProgram>, IEducationalProgramRepo
    {
        public EducationalProgramRepo(SpsDbContext dataContext) : base(dataContext)
        {
        }

        public async Task<IEnumerable<EducationalProgramDto>> GetAllEducationalProgramsAsync()
        {
            return await _context.EducationalPrograms
                .Include(p => p.EduCategory)
                .Include(p => p.Students)
                .Include(p => p.SupportingTeachers)
                .Select(p => new EducationalProgramDto
                {
                    Id = p.Id,
                    Name = p.Name,
                    ProgramCode = p.ProgramCode,
                    Alias = p.Alias,
                    CategoryName = p.EduCategory.Name,
                    StudentCount = p.Students.Count,
                    TeacherCount = p.SupportingTeachers.Count
                })
                .OrderBy(p => p.Name)
                .ToListAsync();
        }

        public async Task<EducationalProgramDto> GetEducationalProgramByIdAsync(Guid id)
        {
            return await _context.EducationalPrograms
                .Include(p => p.EduCategory)
                .Include(p => p.Students)
                .Include(p => p.SupportingTeachers)
                .Where(p => p.Id == id)
                .Select(p => new EducationalProgramDto
                {
                    Id = p.Id,
                    Name = p.Name,
                    ProgramCode = p.ProgramCode,
                    Alias = p.Alias,
                    CategoryName = p.EduCategory.Name,
                    StudentCount = p.Students.Count,
                    TeacherCount = p.SupportingTeachers.Count
                })
                .FirstOrDefaultAsync();
        }

        public async Task<EducationalProgram> CreateEducationalProgramAsync(EducationalProgram program)
        {
            await _context.EducationalPrograms.AddAsync(program);
            await _context.SaveChangesAsync();
            return program;
        }

        public async Task<EducationalProgram> UpdateEducationalProgramAsync(EducationalProgram program)
        {
            _context.EducationalPrograms.Update(program);
            await _context.SaveChangesAsync();
            return program;
        }

        public async Task<bool> DeleteEducationalProgramAsync(Guid id)
        {
            var program = await _context.EducationalPrograms.FindAsync(id);
            if (program == null)
                return false;

            _context.EducationalPrograms.Remove(program);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> EducationalProgramExistsAsync(Guid id)
        {
            return await _context.EducationalPrograms.AnyAsync(p => p.Id == id);
        }

        public async Task<bool> EducationalProgramNameExistsAsync(string name, Guid? excludeId = null)
        {
            return await _context.EducationalPrograms
                .Where(p => p.Name == name)
                .Where(p => excludeId == null || p.Id != excludeId)
                .AnyAsync();
        }
    }
}