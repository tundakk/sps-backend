using Mapster;
using sps.Domain.Model.Dtos.EducationalProgram;
using sps.Domain.Model.Models;
using sps.Domain.Model.ValueObjects;
using System.Collections.Generic;
using System.Linq;

namespace sps.API.Mappings
{
   /// <summary>
   /// Configures mappings between domain entities and DTOs
   /// </summary>
   public static class DtoMappingConfig
   {       /// <summary>
       /// Registers all DTO mapping configurations
       /// </summary>
       public static void RegisterDtoMappings()
       {
           // Educational Program mappings (currently implemented)
           ConfigureEducationalProgramMappings();
           
           // TODO: Add other entity mapping configurations as they are implemented:
           // - Student mappings
           // - Period mappings
           // - Diagnosis mappings
           // - SupportType mappings
           // - EduStatus mappings
           // - SupportingTeacher mappings
           // - TeacherPayment mappings
           // - SpsaCase mappings
           // - StudentPayment mappings
           // - EduCategory mappings
       }

    //    public static void ConfigureStudentMappings()
           /// <summary>
       /// Configures mappings for Educational Program objects
       /// </summary>
       public static void ConfigureEducationalProgramMappings()
       {
           // Create DTO to Model mapping
           TypeAdapterConfig<CreateEducationalProgramDto, EducationalProgramModel>
               .NewConfig()
               .Map(dest => dest.Name, src => src.Name)
               .Map(dest => dest.ProgramCode, src => src.ProgramCode)
               .Map(dest => dest.Alias, src => src.Alias)
               .Map(dest => dest.EduCategoryId, src => src.EduCategoryId)
               .Ignore(dest => dest.Id)
               .Ignore(dest => dest.EduCategory!)
               .Ignore(dest => dest.EducationPeriodRates)
               .Ignore(dest => dest.Students)
               .Ignore(dest => dest.SupportingTeachers);

           // Update DTO to Model mapping
           TypeAdapterConfig<UpdateEducationalProgramDto, EducationalProgramModel>
               .NewConfig()
               .Map(dest => dest.Id, src => src.Id)
               .Map(dest => dest.Name, src => src.Name)
               .Map(dest => dest.ProgramCode, src => src.ProgramCode)
               .Map(dest => dest.Alias, src => src.Alias)
               .Map(dest => dest.EduCategoryId, src => src.EduCategoryId)
               .Ignore(dest => dest.EduCategory!)
               .Ignore(dest => dest.EducationPeriodRates)
               .Ignore(dest => dest.Students)
               .Ignore(dest => dest.SupportingTeachers);
       }
   }
}