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
   {
       /// <summary>
       /// Registers all DTO mapping configurations
       /// </summary>
       public static void RegisterDtoMappings()
       {
           // Student mappings
        //    ConfigureStudentMappings();

           // Educational Program mappings
           ConfigureEducationalProgramMappings();
           
        //    // Period mappings
        //    ConfigurePeriodMappings();

        //    // Diagnosis mappings
        //    ConfigureDiagnosisMappings();

        //    // SupportType mappings
        //    ConfigureSupportTypeMappings();

        //    // EduStatus mappings
        //    ConfigureEduStatusMappings();

        //    // SupportingTeacher mappings
        //    ConfigureSupportingTeacherMappings();

        //    // TeacherPayment mappings
        //    ConfigureTeacherPaymentMappings();

        //    // SpsaCase mappings
        //    ConfigureSpsaCaseMappings();

        //    // StudentPayment mappings
        //    ConfigureStudentPaymentMappings();

        //    // EduCategory mappings
        //    ConfigureEduCategoryMappings();


           // Add other entity mapping configurations here as needed
       }

    //    public static void ConfigureStudentMappings()
    //    {
    //        // Map from domain model to basic DTO (read operations)
    //        TypeAdapterConfig<StudentModel, StudentDto>
    //            .NewConfig()
    //            .Map(dest => dest.Name, src => src.Name.Value)
    //            .Map(dest => dest.Comments, src => src.Comments.Select(c => new StudentCommentDto
    //            {
    //                Id = c.Id,
    //                CommentText = c.CommentText.Value,
    //                CreatedAt = c.CreatedAt
    //            }).ToList())
    //            .Map(dest => dest.EducationName, src => src.Education != null ? src.Education.Name : null)
    //            .Map(dest => dest.PeriodName, src => src.StartPeriod != null ? src.StartPeriod.Name : null);

    //        // Map from domain model to detail DTO (detailed read operations)
    //        TypeAdapterConfig<StudentModel, StudentDetailDto>
    //            .NewConfig()
    //            .Map(dest => dest.Name, src => src.Name.Value)
    //            .Map(dest => dest.Comments, src => src.Comments.Select(c => new StudentCommentDto
    //            {
    //                Id = c.Id,
    //                CommentText = c.CommentText.Value,
    //                CreatedAt = c.CreatedAt
    //            }).ToList())
    //            .Map(dest => dest.StartPeriod, src => src.StartPeriod)
    //            .Map(dest => dest.Education, src => src.Education)
    //            .Map(dest => dest.SpsaCases, src => src.SpsaCases);

    //        // Map from create DTO to domain model
    //        TypeAdapterConfig<CreateStudentDto, StudentModel>
    //            .NewConfig()
    //            .Map(dest => dest.StudentNumber, src => src.StudentNumber)
    //            .Map(dest => dest.CPRNumber, src => new CPRNumber(src.CPRNumber))
    //            .Map(dest => dest.Name, src => new SensitiveString(src.Name))
    //            .Map(dest => dest.Comments, src => src.Comments.Select(c => new StudentCommentModel
    //            {
    //                CommentText = new SensitiveString(c),
    //                CreatedAt = DateTime.UtcNow
    //            }))
    //            .Map(dest => dest.StartPeriodId, src => src.StartPeriodId)
    //            .Map(dest => dest.EducationId, src => src.EducationId)
    //            .Ignore(dest => dest.Id)
    //            .Ignore(dest => dest.StartPeriod)
    //            .Ignore(dest => dest.Education)
    //            .Ignore(dest => dest.SpsaCases);

    //        // Map from update DTO to domain model
    //        TypeAdapterConfig<UpdateStudentDto, StudentModel>
    //            .NewConfig()
    //            .Map(dest => dest.Id, src => src.Id)
    //            .Map(dest => dest.StudentNumber, src => src.StudentNumber)
    //            .Map(dest => dest.Name, src => new SensitiveString(src.Name))
    //            .Map(dest => dest.Comments, src => src.Comments.Select(c => new StudentCommentModel
    //            {
    //                CommentText = new SensitiveString(c),
    //                CreatedAt = DateTime.UtcNow
    //            }))
    //            .Map(dest => dest.StartPeriodId, src => src.StartPeriodId)
    //            .Map(dest => dest.EducationId, src => src.EducationId)
    //            .Map(dest => dest.FinishedDate, src => src.FinishedDate)
    //            .Ignore(dest => dest.CPRNumber) // CPR number cannot be changed
    //            .Ignore(dest => dest.StartPeriod)
    //            .Ignore(dest => dest.Education)
    //            .Ignore(dest => dest.SpsaCases);

    //        // Maps for the nested DTOs - specify fully qualified names to resolve ambiguity
    //        TypeAdapterConfig<PeriodModel, Domain.Model.Dtos.Student.PeriodDto>.NewConfig();
    //        TypeAdapterConfig<EducationModel, Domain.Model.Dtos.Student.EducationDto>
    //            .NewConfig()
    //            .Map(dest => dest.CategoryName, src => src.EduCategory != null ? src.EduCategory.Name : null);
    //    }

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