using sps.Domain.Model.Entities;
using sps.Domain.Model.Models;
using sps.Domain.Model.ValueObjects;
using Mapster;
using Microsoft.AspNetCore.Identity;

/// <summary>
/// Mapping configuration class.
/// </summary>
public static class MappingConfig
{
    /// <summary>
    /// Register mappings.
    /// </summary>
    public static void RegisterMappings()
    {
        // Identity User mapping
        TypeAdapterConfig<IdentityUser<Guid>, IdentityUserModel>.NewConfig()
            .Map(dest => dest.Id, src => src.Id)
            .Map(dest => dest.UserName, src => src.UserName ?? string.Empty)
            .Map(dest => dest.Email, src => src.Email ?? string.Empty)
            .Map(dest => dest.PhoneNumber, src => src.PhoneNumber ?? string.Empty)
            .Map(dest => dest.EmailConfirmed, src => src.EmailConfirmed);

        // Comment mapping
        TypeAdapterConfig<Comment, CommentModel>.NewConfig()
            .Map(dest => dest.Id, src => src.Id)
            .Map(dest => dest.CommentText, src => src.CommentText)
            .Map(dest => dest.CreatedAt, src => src.CreatedAt)
            .Map(dest => dest.EntityType, src => src.EntityType)
            .Map(dest => dest.CreatedBy, src => src.CreatedBy)
            .Map(dest => dest.EntityId, src => GetEntityId(src));

        TypeAdapterConfig<CommentModel, Comment>.NewConfig()
            .Map(dest => dest.Id, src => src.Id)
            .Map(dest => dest.CommentText, src => src.CommentText)
            .Map(dest => dest.CreatedAt, src => src.CreatedAt)
            .Map(dest => dest.EntityType, src => src.EntityType)
            .Map(dest => dest.CreatedBy, src => src.CreatedBy)
            .AfterMapping((src, dest) =>
            {
                switch (src.EntityType)
                {
                    case "SpsaCase":
                        dest.SpsaCaseId = src.EntityId;
                        break;
                    case "Student":
                        dest.StudentId = src.EntityId;
                        break;
                    case "TeacherPayment":
                        dest.TeacherPaymentId = src.EntityId;
                        break;
                    case "StudentPayment":
                        dest.StudentPaymentId = src.EntityId;
                        break;
                    case "OpkvalSupervision":
                        dest.OpkvalSupervisionId = src.EntityId;
                        break;
                }
            });

        // Student mapping
        TypeAdapterConfig<Student, StudentModel>.NewConfig()
            .Map(dest => dest.Id, src => src.Id)
            .Map(dest => dest.StudentNumber, src => src.StudentNumber)
            .Map(dest => dest.CPRNumber, src => src.CPRNumber)
            .Map(dest => dest.Name, src => src.Name)
            .Map(dest => dest.Comments, src => src.Comments)
            .Map(dest => dest.FinishedDate, src => src.FinishedDate)
            .Map(dest => dest.StartPeriodId, src => src.StartPeriodId)
            .Map(dest => dest.EducationId, src => src.EducationId);

        TypeAdapterConfig<StudentModel, Student>.NewConfig()
            .Map(dest => dest.Id, src => src.Id)
            .Map(dest => dest.StudentNumber, src => src.StudentNumber)
            .Map(dest => dest.CPRNumber, src => src.CPRNumber)
            .Map(dest => dest.Name, src => src.Name)
            .Map(dest => dest.Comments, src => src.Comments)
            .Map(dest => dest.FinishedDate, src => src.FinishedDate)
            .Map(dest => dest.StartPeriodId, src => src.StartPeriodId)
            .Map(dest => dest.EducationId, src => src.EducationId);

        // SpsaCase mapping
        TypeAdapterConfig<SpsaCase, SpsaCaseModel>.NewConfig()
            .Map(dest => dest.Id, src => src.Id)
            .Map(dest => dest.SpsaCaseNumber, src => src.SpsaCaseNumber)
            .Map(dest => dest.HoursSought, src => src.HoursSought)
            .Map(dest => dest.HoursSpent, src => src.HoursSpent)
            .Map(dest => dest.Comments, src => src.Comments)
            .Map(dest => dest.IsActive, src => src.IsActive)
            .Map(dest => dest.ApplicationDate, src => src.ApplicationDate)
            .Map(dest => dest.LatestReapplicationDate, src => src.LatestReapplicationDate)
            .Map(dest => dest.StudentId, src => src.StudentId)
            .Map(dest => dest.SupportingTeacherId, src => src.SupportingTeacherId)
            .Map(dest => dest.AppliedPeriodId, src => src.AppliedPeriodId);

        TypeAdapterConfig<SpsaCaseModel, SpsaCase>.NewConfig()
            .Map(dest => dest.Id, src => src.Id)
            .Map(dest => dest.SpsaCaseNumber, src => src.SpsaCaseNumber)
            .Map(dest => dest.HoursSought, src => src.HoursSought)
            .Map(dest => dest.HoursSpent, src => src.HoursSpent)
            .Map(dest => dest.Comments, src => src.Comments)
            .Map(dest => dest.IsActive, src => src.IsActive)
            .Map(dest => dest.ApplicationDate, src => src.ApplicationDate)
            .Map(dest => dest.LatestReapplicationDate, src => src.LatestReapplicationDate)
            .Map(dest => dest.StudentId, src => src.StudentId)
            .Map(dest => dest.SupportingTeacherId, src => src.SupportingTeacherId)
            .Map(dest => dest.AppliedPeriodId, src => src.AppliedPeriodId);

        // SupportingTeacher mapping
        TypeAdapterConfig<SupportingTeacher, SupportingTeacherModel>.NewConfig()
            .Map(dest => dest.Id, src => src.Id)
            .Map(dest => dest.Name, src => src.Name)
            .Map(dest => dest.Email, src => src.Email)
            .Map(dest => dest.PlacesId, src => src.PlacesId);

        TypeAdapterConfig<SupportingTeacherModel, SupportingTeacher>.NewConfig()
            .Map(dest => dest.Id, src => src.Id)
            .Map(dest => dest.Name, src => src.Name)
            .Map(dest => dest.Email, src => src.Email)
            .Map(dest => dest.PlacesId, src => src.PlacesId);

        // StudentPayment mapping
        TypeAdapterConfig<StudentPayment, StudentPaymentModel>.NewConfig()
            .Map(dest => dest.Id, src => src.Id)
            .Map(dest => dest.Date, src => src.Date)
            .Map(dest => dest.AccountNumber, src => src.AccountNumber)
            .Map(dest => dest.Comments, src => src.Comments)
            .Map(dest => dest.Amount, src => src.Amount)
            .Map(dest => dest.ExternalVoucherNumber, src => src.ExternalVoucherNumber)
            .Map(dest => dest.SupportTypeId, src => src.SupportTypeId);

        TypeAdapterConfig<StudentPaymentModel, StudentPayment>.NewConfig()
            .Map(dest => dest.Id, src => src.Id)
            .Map(dest => dest.Date, src => src.Date)
            .Map(dest => dest.AccountNumber, src => src.AccountNumber)
            .Map(dest => dest.Comments, src => src.Comments)
            .Map(dest => dest.Amount, src => src.Amount)
            .Map(dest => dest.ExternalVoucherNumber, src => src.ExternalVoucherNumber)
            .Map(dest => dest.SupportTypeId, src => src.SupportTypeId);

        // TeacherPayment mapping
        TypeAdapterConfig<TeacherPayment, TeacherPaymentModel>.NewConfig()
            .Map(dest => dest.Id, src => src.Id)
            .Map(dest => dest.Date, src => src.Date)
            .Map(dest => dest.Amount, src => src.Amount)
            .Map(dest => dest.Comments, src => src.Comments)
            .Map(dest => dest.ExternalVoucherNumber, src => src.ExternalVoucherNumber)
            .Map(dest => dest.SupportTypeId, src => src.SupportTypeId);

        TypeAdapterConfig<TeacherPaymentModel, TeacherPayment>.NewConfig()
            .Map(dest => dest.Id, src => src.Id)
            .Map(dest => dest.Date, src => src.Date)
            .Map(dest => dest.Amount, src => src.Amount)
            .Map(dest => dest.Comments, src => src.Comments)
            .Map(dest => dest.ExternalVoucherNumber, src => src.ExternalVoucherNumber)
            .Map(dest => dest.SupportTypeId, src => src.SupportTypeId);

        // Place mapping
        TypeAdapterConfig<Place, PlaceModel>.NewConfig()
            .Map(dest => dest.Id, src => src.Id)
            .Map(dest => dest.Name, src => src.Name)
            .Map(dest => dest.PlaceNumber, src => src.PlaceNumber)
            .Map(dest => dest.Alias, src => src.Alias);

        TypeAdapterConfig<PlaceModel, Place>.NewConfig()
            .Map(dest => dest.Id, src => src.Id)
            .Map(dest => dest.Name, src => src.Name)
            .Map(dest => dest.PlaceNumber, src => src.PlaceNumber)
            .Map(dest => dest.Alias, src => src.Alias);

        // EduCategory mapping
        TypeAdapterConfig<EduCategory, EduCategoryModel>.NewConfig()
            .Map(dest => dest.Id, src => src.Id)
            .Map(dest => dest.Name, src => src.Name);

        TypeAdapterConfig<EduCategoryModel, EduCategory>.NewConfig()
            .Map(dest => dest.Id, src => src.Id)
            .Map(dest => dest.Name, src => src.Name);

        // Education mapping
        TypeAdapterConfig<Education, EducationModel>.NewConfig()
            .Map(dest => dest.Id, src => src.Id)
            .Map(dest => dest.Name, src => src.Name)
            .Map(dest => dest.EduCategoryId, src => src.EduCategoryId);

        TypeAdapterConfig<EducationModel, Education>.NewConfig()
            .Map(dest => dest.Id, src => src.Id)
            .Map(dest => dest.Name, src => src.Name)
            .Map(dest => dest.EduCategoryId, src => src.EduCategoryId);

        // Period mapping
        TypeAdapterConfig<Period, PeriodModel>.NewConfig()
            .Map(dest => dest.Id, src => src.Id)
            .Map(dest => dest.Name, src => src.Name);

        TypeAdapterConfig<PeriodModel, Period>.NewConfig()
            .Map(dest => dest.Id, src => src.Id)
            .Map(dest => dest.Name, src => src.Name);
    }

    private static Guid GetEntityId(Comment src)
    {
        switch (src.EntityType)
        {
            case "SpsaCase":
                return src.SpsaCaseId ?? Guid.Empty;
            case "Student":
                return src.StudentId ?? Guid.Empty;
            case "TeacherPayment":
                return src.TeacherPaymentId ?? Guid.Empty;
            case "StudentPayment":
                return src.StudentPaymentId ?? Guid.Empty;
            case "OpkvalSupervision":
                return src.OpkvalSupervisionId ?? Guid.Empty;
            default:
                return Guid.Empty;
        }
    }
}