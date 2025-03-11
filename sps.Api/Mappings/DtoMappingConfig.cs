// using Mapster;
// using sps.Domain.Model.Dtos.Diagnosis;
// using sps.Domain.Model.Dtos.EduCategory;
// using sps.Domain.Model.Dtos.Education;
// using sps.Domain.Model.Dtos.EduStatus;
// using sps.Domain.Model.Dtos.Period;
// using sps.Domain.Model.Dtos.Place;
// using sps.Domain.Model.Dtos.SpsaCase;
// using sps.Domain.Model.Dtos.Student;
// using sps.Domain.Model.Dtos.StudentPayment;
// using sps.Domain.Model.Dtos.SupportingTeacher;
// using sps.Domain.Model.Dtos.SupportType;
// using sps.Domain.Model.Dtos.TeacherPayment;
// using sps.Domain.Model.Models;
// using sps.Domain.Model.ValueObjects;
// using System.Collections.Generic;
// using System.Linq;

// namespace sps.API.Mappings
// {
//    /// <summary>
//    /// Configures mappings between domain entities and DTOs
//    /// </summary>
//    public static class DtoMappingConfig
//    {
//        /// <summary>
//        /// Registers all DTO mapping configurations
//        /// </summary>
//        public static void RegisterDtoMappings()
//        {
//            // Student mappings
//            ConfigureStudentMappings();

//            // Education mappings
//            ConfigureEducationMappings();

//            // Period mappings
//            ConfigurePeriodMappings();

//            // Diagnosis mappings
//            ConfigureDiagnosisMappings();

//            // SupportType mappings
//            ConfigureSupportTypeMappings();

//            // EduStatus mappings
//            ConfigureEduStatusMappings();

//            // SupportingTeacher mappings
//            ConfigureSupportingTeacherMappings();

//            // TeacherPayment mappings
//            ConfigureTeacherPaymentMappings();

//            // SpsaCase mappings
//            ConfigureSpsaCaseMappings();

//            // StudentPayment mappings
//            ConfigureStudentPaymentMappings();

//            // EduCategory mappings
//            ConfigureEduCategoryMappings();

//            // Place mappings
//            ConfigurePlaceMappings();

//            // Add other entity mapping configurations here as needed
//        }

//        public static void ConfigureStudentMappings()
//        {
//            // Map from domain model to basic DTO (read operations)
//            TypeAdapterConfig<StudentModel, StudentDto>
//                .NewConfig()
//                .Map(dest => dest.Name, src => src.Name.Value)
//                .Map(dest => dest.Comments, src => src.Comments.Select(c => new StudentCommentDto
//                {
//                    Id = c.Id,
//                    CommentText = c.CommentText.Value,
//                    CreatedAt = c.CreatedAt
//                }).ToList())
//                .Map(dest => dest.EducationName, src => src.Education != null ? src.Education.Name : null)
//                .Map(dest => dest.PeriodName, src => src.StartPeriod != null ? src.StartPeriod.Name : null);

//            // Map from domain model to detail DTO (detailed read operations)
//            TypeAdapterConfig<StudentModel, StudentDetailDto>
//                .NewConfig()
//                .Map(dest => dest.Name, src => src.Name.Value)
//                .Map(dest => dest.Comments, src => src.Comments.Select(c => new StudentCommentDto
//                {
//                    Id = c.Id,
//                    CommentText = c.CommentText.Value,
//                    CreatedAt = c.CreatedAt
//                }).ToList())
//                .Map(dest => dest.StartPeriod, src => src.StartPeriod)
//                .Map(dest => dest.Education, src => src.Education)
//                .Map(dest => dest.SpsaCases, src => src.SpsaCases);

//            // Map from create DTO to domain model
//            TypeAdapterConfig<CreateStudentDto, StudentModel>
//                .NewConfig()
//                .Map(dest => dest.StudentNumber, src => src.StudentNumber)
//                .Map(dest => dest.CPRNumber, src => new CPRNumber(src.CPRNumber))
//                .Map(dest => dest.Name, src => new SensitiveString(src.Name))
//                .Map(dest => dest.Comments, src => src.Comments.Select(c => new StudentCommentModel
//                {
//                    CommentText = new SensitiveString(c),
//                    CreatedAt = DateTime.UtcNow
//                }))
//                .Map(dest => dest.StartPeriodId, src => src.StartPeriodId)
//                .Map(dest => dest.EducationId, src => src.EducationId)
//                .Ignore(dest => dest.Id)
//                .Ignore(dest => dest.StartPeriod)
//                .Ignore(dest => dest.Education)
//                .Ignore(dest => dest.SpsaCases);

//            // Map from update DTO to domain model
//            TypeAdapterConfig<UpdateStudentDto, StudentModel>
//                .NewConfig()
//                .Map(dest => dest.Id, src => src.Id)
//                .Map(dest => dest.StudentNumber, src => src.StudentNumber)
//                .Map(dest => dest.Name, src => new SensitiveString(src.Name))
//                .Map(dest => dest.Comments, src => src.Comments.Select(c => new StudentCommentModel
//                {
//                    CommentText = new SensitiveString(c),
//                    CreatedAt = DateTime.UtcNow
//                }))
//                .Map(dest => dest.StartPeriodId, src => src.StartPeriodId)
//                .Map(dest => dest.EducationId, src => src.EducationId)
//                .Map(dest => dest.FinishedDate, src => src.FinishedDate)
//                .Ignore(dest => dest.CPRNumber) // CPR number cannot be changed
//                .Ignore(dest => dest.StartPeriod)
//                .Ignore(dest => dest.Education)
//                .Ignore(dest => dest.SpsaCases);

//            // Maps for the nested DTOs - specify fully qualified names to resolve ambiguity
//            TypeAdapterConfig<PeriodModel, Domain.Model.Dtos.Student.PeriodDto>.NewConfig();
//            TypeAdapterConfig<EducationModel, Domain.Model.Dtos.Student.EducationDto>
//                .NewConfig()
//                .Map(dest => dest.CategoryName, src => src.EduCategory != null ? src.EduCategory.Name : null);
//        }

//        private static void ConfigureEducationMappings()
//        {
//            // Basic Education DTO mapping - use fully qualified name to resolve ambiguity
//            TypeAdapterConfig<EducationModel, Domain.Model.Dtos.Education.EducationDto>
//                .NewConfig()
//                .Map(dest => dest.CategoryName, src => src.EduCategory != null ? src.EduCategory.Name : null)
//                .Map(dest => dest.StudentCount, src => src.Students != null ? src.Students.Count : 0);

//            // Education Detail DTO mapping
//            TypeAdapterConfig<EducationModel, EducationDetailDto>
//                .NewConfig()
//                .Map(dest => dest.Category, src => src.EduCategory)
//                .Map(dest => dest.PeriodRates, src => src.EducationPeriodRates)
//                .Map(dest => dest.Students, src => src.Students);

//            // Create DTO to Model mapping
//            TypeAdapterConfig<CreateEducationDto, EducationModel>
//                .NewConfig()
//                .Map(dest => dest.Name, src => src.Name)
//                .Map(dest => dest.EduCategoryId, src => src.EduCategoryId)
//                .Ignore(dest => dest.Id)
//                .Ignore(dest => dest.EduCategory)
//                .Ignore(dest => dest.EducationPeriodRates)
//                .Ignore(dest => dest.Students);

//            // Update DTO to Model mapping
//            TypeAdapterConfig<UpdateEducationDto, EducationModel>
//                .NewConfig()
//                .Map(dest => dest.Id, src => src.Id)
//                .Map(dest => dest.Name, src => src.Name)
//                .Map(dest => dest.EduCategoryId, src => src.EduCategoryId)
//                .Ignore(dest => dest.EduCategory)
//                .Ignore(dest => dest.EducationPeriodRates)
//                .Ignore(dest => dest.Students);

//            // Nested DTO mappings
//            TypeAdapterConfig<EduCategoryModel, EducationCategoryDto>.NewConfig();

//            TypeAdapterConfig<EducationPeriodRateModel, EducationPeriodRateDto>
//                .NewConfig()
//                .Map(dest => dest.PeriodName, src => src.Period != null ? src.Period.Name : null);

//            TypeAdapterConfig<StudentModel, StudentSummaryDto>
//                .NewConfig()
//                .Map(dest => dest.FullName, src => src.Name.Value)
//                .Map(dest => dest.StartPeriod, src => src.StartPeriod != null ? src.StartPeriod : null);
//        }

//        private static void ConfigurePeriodMappings()
//        {
//            // Basic Period DTO mapping - use fully qualified name to resolve ambiguity
//            TypeAdapterConfig<PeriodModel, Domain.Model.Dtos.Period.PeriodDto>
//                .NewConfig()
//                .Map(dest => dest.StudentCount, src => src.Students != null ? src.Students.Count : 0)
//                .Map(dest => dest.ActiveCaseCount, src => src.SpsaCases != null ?
//                    src.SpsaCases.Count(c => c.IsActive) : 0);

//            // Period Detail DTO mapping
//            TypeAdapterConfig<PeriodModel, PeriodDetailDto>
//                .NewConfig()
//                .Map(dest => dest.BasicInfo, src => src)
//                .Map(dest => dest.EducationRates, src => src.EducationPeriodRates.Select(rate => new PeriodRateInfo
//                {
//                    Id = rate.Id,
//                    EducationName = rate.Education.Name,
//                    CategoryName = rate.Education.EduCategory?.Name,
//                    Rate = rate.Amount
//                }))
//                .Map(dest => dest.Cases, src => src.SpsaCases.Select(c => new PeriodCaseInfo
//                {
//                    Id = c.Id,
//                    CaseNumber = c.SpsaCaseNumber,
//                    StudentName = c.Student.Name.Value,
//                    EducationName = c.Student.Education?.Name,
//                    IsActive = c.IsActive,
//                    HoursSought = c.HoursSought,
//                    HoursSpent = c.HoursSpent
//                }));

//            // Create DTO to Model mapping
//            TypeAdapterConfig<CreatePeriodDto, PeriodModel>
//                .NewConfig()
//                .Map(dest => dest.Name, src => src.Name)
//                .Ignore(dest => dest.Id)
//                .Ignore(dest => dest.EducationPeriodRates)
//                .Ignore(dest => dest.SpsaCases)
//                .Ignore(dest => dest.Students);

//            // Update DTO to Model mapping
//            TypeAdapterConfig<UpdatePeriodDto, PeriodModel>
//                .NewConfig()
//                .Map(dest => dest.Id, src => src.Id)
//                .Map(dest => dest.Name, src => src.Name)
//                .Ignore(dest => dest.EducationPeriodRates)
//                .Ignore(dest => dest.SpsaCases)
//                .Ignore(dest => dest.Students);
//        }

//        private static void ConfigureDiagnosisMappings()
//        {
//            // Basic Diagnosis DTO mapping
//            TypeAdapterConfig<DiagnosisModel, DiagnosisDto>
//                .NewConfig()
//                .Map(dest => dest.ActiveCaseCount, src => src.SpsaCases != null ?
//                    src.SpsaCases.Count(c => c.IsActive) : 0);

//            // Diagnosis Detail DTO mapping
//            TypeAdapterConfig<DiagnosisModel, DiagnosisDetailDto>
//                .NewConfig()
//                .Map(dest => dest.BasicInfo, src => src)
//                .Map(dest => dest.Cases, src => src.SpsaCases);

//            // Create DTO to Model mapping
//            TypeAdapterConfig<CreateDiagnosisDto, DiagnosisModel>
//                .NewConfig()
//                .Map(dest => dest.Name, src => src.Name)
//                .Ignore(dest => dest.Id)
//                .Ignore(dest => dest.SpsaCases);

//            // Update DTO to Model mapping
//            TypeAdapterConfig<UpdateDiagnosisDto, DiagnosisModel>
//                .NewConfig()
//                .Map(dest => dest.Id, src => src.Id)
//                .Map(dest => dest.Name, src => src.Name)
//                .Ignore(dest => dest.SpsaCases);
//        }

//        private static void ConfigureSupportTypeMappings()
//        {
//            // Basic SupportType DTO mapping
//            TypeAdapterConfig<SupportTypeModel, SupportTypeDto>
//                .NewConfig()
//                .Map(dest => dest.ActiveCaseCount, src => src.SpsaCases != null ?
//                    src.SpsaCases.Count(c => c.IsActive) : 0)
//                .Map(dest => dest.TotalTeacherPayments, src => src.TeacherPayments != null ?
//                    src.TeacherPayments.Sum(p => p.Amount) : 0)
//                .Map(dest => dest.TotalStudentPayments, src => src.StudentPayments != null ?
//                    src.StudentPayments.Sum(p => p.Amount) : 0);

//            //// SupportType Detail DTO mapping
//            //TypeAdapterConfig<SupportTypeModel, SupportTypeDetailDto>
//            //    .NewConfig()
//            //    .Map(dest => dest.BasicInfo, src => src)
//            //    .Map(dest => dest.Cases, src => src.SpsaCases)
//            //    .Map(dest => dest.PaymentsByPeriod, src =>
//            //        src.TeacherPayments
//            //            .GroupBy(p => p. Period)
//            //            .Select(g => new PeriodPaymentStats
//            //            {
//            //                PeriodName = g.Key.Name,
//            //                StartDate = g.Key.StartDate,
//            //                EndDate = g.Key.EndDate,
//            //                TeacherPayments = g.Sum(p => p.Amount),
//            //                StudentPayments = src.StudentPayments
//            //                    .Where(sp => sp.Period == g.Key)
//            //                    .Sum(sp => sp.Amount),
//            //                CaseCount = src.SpsaCases
//            //                    .Count(c => c.AppliedPeriod == g.Key)
//            //            }));

//            // Create DTO to Model mapping
//            TypeAdapterConfig<CreateSupportTypeDto, SupportTypeModel>
//                .NewConfig()
//                .Map(dest => dest.Name, src => src.Name)
//                .Ignore(dest => dest.Id)
//                .Ignore(dest => dest.SpsaCases)
//                .Ignore(dest => dest.TeacherPayments)
//                .Ignore(dest => dest.StudentPayments);

//            // Update DTO to Model mapping
//            TypeAdapterConfig<UpdateSupportTypeDto, SupportTypeModel>
//                .NewConfig()
//                .Map(dest => dest.Id, src => src.Id)
//                .Map(dest => dest.Name, src => src.Name)
//                .Ignore(dest => dest.SpsaCases)
//                .Ignore(dest => dest.TeacherPayments)
//                .Ignore(dest => dest.StudentPayments);
//        }

//        private static void ConfigureEduStatusMappings()
//        {
//            // Basic EduStatus DTO mapping
//            TypeAdapterConfig<EduStatusModel, EduStatusDto>
//                .NewConfig()
//                .Map(dest => dest.ActiveCaseCount, src => src.SpsaCases != null ?
//                    src.SpsaCases.Count(c => c.IsActive) : 0);

//            // EduStatus Detail DTO mapping
//            TypeAdapterConfig<EduStatusModel, EduStatusDetailDto>
//                .NewConfig()
//                .Map(dest => dest.BasicInfo, src => src)
//                .Map(dest => dest.Cases, src => src.SpsaCases)
//                .Map(dest => dest.StatsByCategory, src =>
//                    src.SpsaCases
//                        .GroupBy(c => c.EduCategory)
//                        .Select(g => new CategoryStats
//                        {
//                            CategoryName = g.Key?.Name ?? "Uncategorized",
//                            ActiveCaseCount = g.Count(c => c.IsActive),
//                            TotalCaseCount = g.Count(),
//                            AverageHoursSought = g.Average(c => c.HoursSought)
//                        }));

//            // Create DTO to Model mapping
//            TypeAdapterConfig<CreateEduStatusDto, EduStatusModel>
//                .NewConfig()
//                .Map(dest => dest.Name, src => src.Name)
//                .Ignore(dest => dest.Id)
//                .Ignore(dest => dest.SpsaCases);

//            // Update DTO to Model mapping
//            TypeAdapterConfig<UpdateEduStatusDto, EduStatusModel>
//                .NewConfig()
//                .Map(dest => dest.Id, src => src.Id)
//                .Map(dest => dest.Name, src => src.Name)
//                .Ignore(dest => dest.SpsaCases);
//        }

//        private static void ConfigureSupportingTeacherMappings()
//        {
//            // Basic SupportingTeacher DTO mapping
//            TypeAdapterConfig<SupportingTeacherModel, SupportingTeacherDto>
//                .NewConfig()
//                .Map(dest => dest.Email, src => src.Email.Value)
//                .Map(dest => dest.PlaceName, src => src.Place != null ? src.Place.Name : null)
//                .Map(dest => dest.ActiveCaseCount, src => src.SpsaCases != null ?
//                    src.SpsaCases.Count(c => c.IsActive) : 0);

//            // SupportingTeacher Detail DTO mapping
//            TypeAdapterConfig<SupportingTeacherModel, SupportingTeacherDetailDto>
//                .NewConfig()
//                .Map(dest => dest.BasicInfo, src => src)
//                .Map(dest => dest.Cases, src => src.SpsaCases)
//                .Map(dest => dest.WorkloadByPeriod, src =>
//                    src.SpsaCases
//                        .GroupBy(c => c.AppliedPeriod)
//                        .Select(g => new PeriodWorkload
//                        {
//                            PeriodName = g.Key.Name,
//                            StartDate = g.Key.StartDate,
//                            EndDate = g.Key.EndDate,
//                            ActiveCaseCount = g.Count(c => c.IsActive),
//                            TotalHoursSought = g.Sum(c => c.HoursSought),
//                            TotalHoursSpent = g.Sum(c => c.HoursSpent),
//                            TotalPayment = g.Sum(c =>
//                                c.TeacherPayment != null ? c.TeacherPayment.Amount : 0)
//                        }));

//            // Create DTO to Model mapping
//            TypeAdapterConfig<CreateSupportingTeacherDto, SupportingTeacherModel>
//                .NewConfig()
//                .Map(dest => dest.Name, src => src.Name)
//                .Map(dest => dest.PlacesId, src => src.PlacesId)
//                .Ignore(dest => dest.Id)
//                .Ignore(dest => dest.Email) // Handled in controller
//                .Ignore(dest => dest.Place)
//                .Ignore(dest => dest.SpsaCases);

//            // Update DTO to Model mapping
//            TypeAdapterConfig<UpdateSupportingTeacherDto, SupportingTeacherModel>
//                .NewConfig()
//                .Map(dest => dest.Id, src => src.Id)
//                .Map(dest => dest.Name, src => src.Name)
//                .Map(dest => dest.PlacesId, src => src.PlacesId)
//                .Ignore(dest => dest.Email) // Handled in controller
//                .Ignore(dest => dest.Place)
//                .Ignore(dest => dest.SpsaCases);
//        }

//        private static void ConfigureTeacherPaymentMappings()
//        {
//            // Basic TeacherPayment DTO mapping
//            TypeAdapterConfig<TeacherPaymentModel, TeacherPaymentDto>
//                .NewConfig()
//                .Map(dest => dest.SupportTypeName, src => src.SupportType != null ? src.SupportType.Name : null)
//                .Map(dest => dest.CaseCount, src => src.SpsaCases != null ? src.SpsaCases.Count : 0)
//                .Map(dest => dest.TotalHours, src => src.SpsaCases != null ?
//                    src.SpsaCases.Sum(c => c.HoursSpent) : 0);

//            // TeacherPayment Detail DTO mapping
//            TypeAdapterConfig<TeacherPaymentModel, TeacherPaymentDetailDto>
//                .NewConfig()
//                .Map(dest => dest.BasicInfo, src => src)
//                .Map(dest => dest.Comment, src => src.Comment != null ? src.Comment.Value : null)
//                .Map(dest => dest.Cases, src => src.SpsaCases)
//                .Map(dest => dest.HoursByType, src =>
//                    src.SpsaCases
//                        .GroupBy(c => c.SupportType)
//                        .Select(g => new Domain.Model.Dtos.TeacherPayment.SupportTypeHours
//                        {
//                            SupportTypeName = g.Key?.Name ?? "Uncategorized",
//                            HoursSought = g.Sum(c => c.HoursSought),
//                            HoursSpent = g.Sum(c => c.HoursSpent),
//                            Amount = g.Sum(c => c.TeacherPayment != null ?
//                                c.TeacherPayment.Amount : 0)
//                        }));

//            // Create DTO to Model mapping
//            TypeAdapterConfig<CreateTeacherPaymentDto, TeacherPaymentModel>
//                .NewConfig()
//                .Map(dest => dest.Date, src => src.Date)
//                .Map(dest => dest.Amount, src => src.Amount)
//                .Map(dest => dest.SupportTypeId, src => src.SupportTypeId)
//                .Map(dest => dest.ExternalVoucherNumber, src => src.ExternalVoucherNumber)
//                .Ignore(dest => dest.Id)
//                .Ignore(dest => dest.Comment) // Handled in controller
//                .Ignore(dest => dest.SpsaCases)
//                .Ignore(dest => dest.SupportType);

//            // Update DTO to Model mapping
//            TypeAdapterConfig<UpdateTeacherPaymentDto, TeacherPaymentModel>
//                .NewConfig()
//                .Map(dest => dest.Id, src => src.Id)
//                .Map(dest => dest.Date, src => src.Date)
//                .Map(dest => dest.Amount, src => src.Amount)
//                .Map(dest => dest.SupportTypeId, src => src.SupportTypeId)
//                .Map(dest => dest.ExternalVoucherNumber, src => src.ExternalVoucherNumber)
//                .Ignore(dest => dest.Comment) // Handled in controller
//                .Ignore(dest => dest.SpsaCases)
//                .Ignore(dest => dest.SupportType);
//        }

//        private static void ConfigureSpsaCaseDetailDtoMappings()
//        {
//            // Map SpsaCase model to detail DTO
//            TypeAdapterConfig<SpsaCaseModel, SpsaCaseDetailDto>
//                .NewConfig()
//                .Map(dest => dest.BasicInfo, src => src.Adapt<SpsaCaseDto>())
//                .Map(dest => dest.Comments, src => src.Comments.Select(c => new SpsaCaseCommentDto
//                {
//                    Id = c.Id,
//                    CommentText = c.CommentText.Value,
//                    CreatedAt = c.CreatedAt
//                }).ToList())
//                .Map(dest => dest.Comment, src => src.Comment != null ? src.Comment.Value : null)
//                .Map(dest => dest.CourseDescriptionReceived, src => src.CourseDescriptionReceived)
//                .Map(dest => dest.TimesheetReceived, src => src.TimesheetReceived)
//                .Map(dest => dest.StudentRefundReleased, src => src.StudentRefundReleased)
//                .Map(dest => dest.TeacherRefundReleased, src => src.TeacherRefundReleased);

//            // Map Student model to StudentInfo
//            TypeAdapterConfig<StudentModel, StudentInfo>
//                .NewConfig()
//                .Map(dest => dest.Id, src => src.Id)
//                .Map(dest => dest.StudentNumber, src => src.StudentNumber)
//                .Map(dest => dest.Name, src => src.Name.Value)
//                .Map(dest => dest.Education, src => src.Education != null ? src.Education.Name : null)
//                .Map(dest => dest.StartPeriodName, src => src.StartPeriod != null ? src.StartPeriod.Name : null)
//                .Map(dest => dest.FinishedDate, src => src.FinishedDate)
//                .Map(dest => dest.CPRNumber, src => src.CPRNumber.Value);

//            // Map SupportingTeacher model to TeacherInfo
//            TypeAdapterConfig<SupportingTeacherModel, TeacherInfo>
//                .NewConfig()
//                .Map(dest => dest.Id, src => src.Id)
//                .Map(dest => dest.Name, src => src.Name)
//                .Map(dest => dest.Department, src => src.Department)
//                .Map(dest => dest.Email, src => src.Email.Value)
//                .Map(dest => dest.PlaceNumber, src => src.Place != null ? src.Place.PlaceNumber : null)
//                .Map(dest => dest.PlaceAlias, src => src.Place != null ? src.Place.Alias : null);

//            // Map Period model to PeriodInfo
//            TypeAdapterConfig<PeriodModel, PeriodInfo>
//                .NewConfig()
//                .Map(dest => dest.Id, src => src.Id)
//                .Map(dest => dest.Name, src => src.Name)
//                .Map(dest => dest.StartDate, src => src.StartDate)
//                .Map(dest => dest.EndDate, src => src.EndDate)
//                .Map(dest => dest.IsActive, src => src.IsActive);

//            // Map TeacherPayment model to PaymentInfo
//            TypeAdapterConfig<TeacherPaymentModel, PaymentInfo>
//                .NewConfig()
//                .Map(dest => dest.Id, src => src.Id)
//                .Map(dest => dest.Amount, src => src.Amount)
//                .Map(dest => dest.PaymentDate, src => src.Date)
//                .Map(dest => dest.ExternalVoucherNumber, src => src.ExternalVoucherNumber)
//                .Map(dest => dest.VoucherText, src => src.VoucherText)
//                .Map(dest => dest.CompleteVoucherText, src => src.CompleteVoucherText)
//                .Map(dest => dest.Comments, src => src.Comments.Select(c => new CommentInfo
//                {
//                    Id = c.Id,
//                    CommentText = c.CommentText.Value,
//                    CreatedAt = c.CreatedAt
//                }).ToList());

//            // Map StudentPayment model to PaymentInfo
//            TypeAdapterConfig<StudentPaymentModel, PaymentInfo>
//                .NewConfig()
//                .Map(dest => dest.Id, src => src.Id)
//                .Map(dest => dest.Amount, src => src.Amount)
//                .Map(dest => dest.PaymentDate, src => src.Date)
//                .Map(dest => dest.AccountNumber, src => src.AccountNumber.Value)
//                .Map(dest => dest.ExternalVoucherNumber, src => src.ExternalVoucherNumber)
//                .Map(dest => dest.VoucherText, src => src.VoucherText)
//                .Map(dest => dest.CompleteVoucherText, src => src.CompleteVoucherText)
//                .Map(dest => dest.Comments, src => src.Comments.Select(c => new CommentInfo
//                {
//                    Id = c.Id,
//                    CommentText = c.CommentText.Value,
//                    CreatedAt = c.CreatedAt
//                }).ToList());

//            // Map OpkvalSupervision model to SupervisionInfo
//            TypeAdapterConfig<OpkvalSupervisionModel, SupervisionInfo>
//                .NewConfig()
//                .Map(dest => dest.Id, src => src.Id)
//                .Map(dest => dest.Date, src => src.Date)
//                .Map(dest => dest.Supervisor, src => src.SupervisorName)
//                .Map(dest => dest.Notes, src => src.Notes)
//                .Map(dest => dest.Outcome, src => src.Outcome)
//                .Map(dest => dest.HoursSought, src => src.HoursSought)
//                .Map(dest => dest.QualificationHoursSpent, src => src.QualificationHoursSpent)
//                .Map(dest => dest.SupervisionHoursSpent, src => src.SupervisionHoursSpent)
//                .Map(dest => dest.Status, src => src.Status)
//                .Map(dest => dest.Comments, src => src.Comments.Select(c => new CommentInfo
//                {
//                    Id = c.Id,
//                    CommentText = c.CommentText.Value,
//                    CreatedAt = c.CreatedAt
//                }).ToList());
//        }

//        private static void ConfigureSpsaCaseMappings()
//        {
//            TypeAdapterConfig<SpsaCaseModel, SpsaCaseSummaryDto>
//                .NewConfig()
//                .Map(dest => dest.Status, src => src.EduStatus != null ? src.EduStatus.Name : "Unknown");

//            // Basic SpsaCase DTO mapping
//            TypeAdapterConfig<SpsaCaseModel, SpsaCaseDto>
//                .NewConfig()
//                .Map(dest => dest.StudentName, src => src.Student != null ? src.Student.Name.Value : null)
//                .Map(dest => dest.StudentNumber, src => src.Student != null ? src.Student.StudentNumber : null)
//                .Map(dest => dest.EducationName, src => src.Student?.Education?.Name)
//                .Map(dest => dest.EducationStartPeriod, src => src.Student?.StartPeriod?.Name)
//                .Map(dest => dest.EducationFinishDate, src => src.Student?.FinishedDate)
//                .Map(dest => dest.SupportingTeacherName, src => src.SupportingTeacher != null ? src.SupportingTeacher.Name : null)
//                .Map(dest => dest.SupportingTeacherEmail, src => src.SupportingTeacher?.Email.Value)
//                .Map(dest => dest.DiagnosisName, src => src.Diagnosis != null ? src.Diagnosis.Name : null)
//                .Map(dest => dest.EduCategoryName, src => src.EduCategory != null ? src.EduCategory.Name : null)
//                .Map(dest => dest.SupportTypeName, src => src.SupportType != null ? src.SupportType.Name : null)
//                .Map(dest => dest.EduStatusName, src => src.EduStatus != null ? src.EduStatus.Name : null)
//                .Map(dest => dest.PlaceName, src => src.SupportingTeacher?.Place?.Name)
//                .Map(dest => dest.PlaceNumber, src => src.SupportingTeacher?.Place?.PlaceNumber)
//                .Map(dest => dest.PlaceAlias, src => src.SupportingTeacher?.Place?.Alias)
//                .Map(dest => dest.SupportRate, src => src.SupportRate)
//                .Map(dest => dest.StudentSupportSum, src => src.StudentPayment != null ? src.StudentPayment.Amount : 0)
//                .Map(dest => dest.TeacherSupportSum, src => src.TeacherPayment != null ? src.TeacherPayment.Amount : 0)
//                .Map(dest => dest.TotalSum, src =>
//                    (src.StudentPayment != null ? src.StudentPayment.Amount : 0) +
//                    (src.TeacherPayment != null ? src.TeacherPayment.Amount : 0))
//                .Map(dest => dest.CourseDescriptionReceived, src => src.CourseDescriptionReceived)
//                .Map(dest => dest.TimesheetReceived, src => src.TimesheetReceived)
//                .Map(dest => dest.StudentRefundReleased, src => src.StudentRefundReleased)
//                .Map(dest => dest.TeacherRefundReleased, src => src.TeacherRefundReleased)
//                .Map(dest => dest.Comment, src => src.Comment != null ? src.Comment.Value : null)
//                .Map(dest => dest.Comments, src => src.Comments
//                    .OrderByDescending(c => c.CreatedAt)
//                    .Select(c => new SpsaCaseCommentDto
//                    {
//                        Id = c.Id,
//                        CommentText = c.CommentText.Value,
//                        CreatedAt = c.CreatedAt
//                    })
//                    .ToList());

//            // Detailed SpsaCase DTO mapping
//            ConfigureSpsaCaseDetailDtoMappings();

//            // Create DTO to Model mapping
//            TypeAdapterConfig<CreateSpsaCaseDto, SpsaCaseModel>
//                .NewConfig()
//                .Map(dest => dest.SpsaCaseNumber, src => src.SpsaCaseNumber)
//                .Map(dest => dest.HoursSought, src => src.HoursSought)
//                .Map(dest => dest.Comment, src => !string.IsNullOrEmpty(src.Comment) ? new SensitiveString(src.Comment) : null)
//                .Map(dest => dest.Comments, src => src.Comments.Select(c => new SpsaCaseCommentModel
//                {
//                    CommentText = new SensitiveString(c),
//                    CreatedAt = DateTime.UtcNow
//                }))
//                .Map(dest => dest.IsActive, src => true) // New cases are active by default
//                .Map(dest => dest.StudentId, src => src.StudentId)
//                .Map(dest => dest.SupportingTeacherId, src => src.SupportingTeacherId)
//                .Map(dest => dest.AppliedPeriodId, src => src.AppliedPeriodId)
//                .Map(dest => dest.DiagnosisId, src => src.DiagnosisId)
//                .Map(dest => dest.EduCategoryId, src => src.EduCategoryId)
//                .Map(dest => dest.SupportTypeId, src => src.SupportTypeId)
//                .Map(dest => dest.EduStatusId, src => src.EduStatusId)
//                .Map(dest => dest.ApplicationDate, src => src.ApplicationDate ?? DateTime.UtcNow)
//                .Map(dest => dest.CourseDescriptionReceived, src => src.CourseDescriptionReceived)
//                .Map(dest => dest.TimesheetReceived, src => src.TimesheetReceived)
//                .Ignore(dest => dest.Id)
//                .Ignore(dest => dest.HoursSpent)
//                .Ignore(dest => dest.Student)
//                .Ignore(dest => dest.SupportingTeacher)
//                .Ignore(dest => dest.AppliedPeriod)
//                .Ignore(dest => dest.TeacherPayment)
//                .Ignore(dest => dest.OpkvalSupervision)
//                .Ignore(dest => dest.StudentPayment)
//                .Ignore(dest => dest.StudentRefundReleased)
//                .Ignore(dest => dest.TeacherRefundReleased);

//            // Update DTO to Model mapping
//            TypeAdapterConfig<UpdateSpsaCaseDto, SpsaCaseModel>
//                .NewConfig()
//                .Map(dest => dest.Id, src => src.Id)
//                .Map(dest => dest.SpsaCaseNumber, src => src.SpsaCaseNumber)
//                .Map(dest => dest.HoursSought, src => src.HoursSought)
//                .Map(dest => dest.HoursSpent, src => src.HoursSpent)
//                .Map(dest => dest.Comment, src => !string.IsNullOrEmpty(src.Comment) ? new SensitiveString(src.Comment) : null)
//                .Map(dest => dest.Comments, src => src.Comments.Select(c => new SpsaCaseCommentModel
//                {
//                    CommentText = new SensitiveString(c),
//                    CreatedAt = DateTime.UtcNow
//                }))
//                .Map(dest => dest.IsActive, src => src.IsActive)
//                .Map(dest => dest.SupportingTeacherId, src => src.SupportingTeacherId)
//                .Map(dest => dest.AppliedPeriodId, src => src.AppliedPeriodId)
//                .Map(dest => dest.DiagnosisId, src => src.DiagnosisId)
//                .Map(dest => dest.EduCategoryId, src => src.EduCategoryId)
//                .Map(dest => dest.SupportTypeId, src => src.SupportTypeId)
//                .Map(dest => dest.EduStatusId, src => src.EduStatusId)
//                .Map(dest => dest.LatestReapplicationDate, src => src.LatestReapplicationDate)
//                .Map(dest => dest.CourseDescriptionReceived, src => src.CourseDescriptionReceived)
//                .Map(dest => dest.TimesheetReceived, src => src.TimesheetReceived)
//                .Map(dest => dest.StudentRefundReleased, src => src.StudentRefundReleased)
//                .Map(dest => dest.TeacherRefundReleased, src => src.TeacherRefundReleased)
//                .Ignore(dest => dest.StudentId) // Cannot change the student
//                .Ignore(dest => dest.Student)
//                .Ignore(dest => dest.SupportingTeacher)
//                .Ignore(dest => dest.AppliedPeriod)
//                .Ignore(dest => dest.Diagnosis)
//                .Ignore(dest => dest.EduCategory)
//                .Ignore(dest => dest.SupportType)
//                .Ignore(dest => dest.EduStatus)
//                .Ignore(dest => dest.TeacherPayment)
//                .Ignore(dest => dest.OpkvalSupervision)
//                .Ignore(dest => dest.StudentPayment);
//        }

//        private static void ConfigureStudentPaymentMappings()
//        {
//            // Map from domain model to DTO
//            TypeAdapterConfig<StudentPaymentModel, StudentPaymentDto>
//                .NewConfig()
//                .Map(dest => dest.AccountNumber, src => src.AccountNumber.Value)
//                .Map(dest => dest.Comments, src => src.Comments.Select(c => new StudentPaymentCommentDto
//                {
//                    Id = c.Id,
//                    CommentText = c.CommentText.Value,
//                    CreatedAt = c.CreatedAt
//                }).ToList())
//                .Map(dest => dest.CaseCount, src => src.SpsaCases != null ? src.SpsaCases.Count : 0)
//                .Map(dest => dest.SupportTypeName, src => src.SupportType != null ? src.SupportType.Name : null);

//            // StudentPayment Detail DTO mapping
//            TypeAdapterConfig<StudentPaymentModel, StudentPaymentDetailDto>
//                .NewConfig()
//                .Map(dest => dest.BasicInfo, src => src)
//                .Map(dest => dest.Comment, src => src.Comment != null ? src.Comment.Value : null)
//                .Map(dest => dest.Cases, src => src.SpsaCases)
//                .Map(dest => dest.HoursByType, src =>
//                    src.SpsaCases
//                        .GroupBy(c => c.SupportType)
//                        .Select(g => new SupportTypeHours
//                        {
//                            SupportTypeName = g.Key?.Name ?? "Uncategorized",
//                            HoursSought = g.Sum(c => c.HoursSought),
//                            HoursSpent = g.Sum(c => c.HoursSpent),
//                            Amount = g.Sum(c => c.StudentPayment != null ?
//                                c.StudentPayment.Amount : 0)
//                        }));

//            // Create DTO to Model mapping
//            TypeAdapterConfig<CreateStudentPaymentDto, StudentPaymentModel>
//                .NewConfig()
//                .Map(dest => dest.Date, src => src.Date)
//                .Map(dest => dest.Amount, src => src.Amount)
//                .Map(dest => dest.AccountNumber, src => new SensitiveString(src.AccountNumber))
//                .Map(dest => dest.Comments, src => src.Comments.Select(c => new StudentPaymentCommentModel
//                {
//                    CommentText = new SensitiveString(c),
//                    CreatedAt = DateTime.UtcNow
//                }))
//                .Map(dest => dest.ExternalVoucherNumber, src => src.ExternalVoucherNumber)
//                .Map(dest => dest.SupportTypeId, src => src.SupportTypeId)
//                .Ignore(dest => dest.Id)
//                .Ignore(dest => dest.SupportType)
//                .Ignore(dest => dest.SpsaCases);

//            // Update DTO to Model mapping
//            TypeAdapterConfig<UpdateStudentPaymentDto, StudentPaymentModel>
//                .NewConfig()
//                .Map(dest => dest.Id, src => src.Id)
//                .Map(dest => dest.Date, src => src.Date)
//                .Map(dest => dest.Amount, src => src.Amount)
//                .Map(dest => dest.AccountNumber, src => new SensitiveString(src.AccountNumber))
//                .Map(dest => dest.Comments, src => src.Comments.Select(c => new StudentPaymentCommentModel
//                {
//                    CommentText = new SensitiveString(c),
//                    CreatedAt = DateTime.UtcNow
//                }))
//                .Map(dest => dest.ExternalVoucherNumber, src => src.ExternalVoucherNumber)
//                .Map(dest => dest.SupportTypeId, src => src.SupportTypeId)
//                .Ignore(dest => dest.SupportType)
//                .Ignore(dest => dest.SpsaCases);
//        }

//        private static void ConfigureEduCategoryMappings()
//        {
//            // Basic EduCategory DTO mapping
//            TypeAdapterConfig<EduCategoryModel, EduCategoryDto>
//                .NewConfig()
//                .Map(dest => dest.EducationCount, src => src.Educations != null ?
//                    src.Educations.Count : 0)
//                .Map(dest => dest.ActiveCaseCount, src => src.SpsaCases != null ?
//                    src.SpsaCases.Count(c => c.IsActive) : 0);

//            // EduCategory Detail DTO mapping
//            TypeAdapterConfig<EduCategoryModel, EduCategoryDetailDto>
//                .NewConfig()
//                .Map(dest => dest.BasicInfo, src => src)
//                .Map(dest => dest.Educations, src => src.Educations)
//                .Map(dest => dest.Cases, src => src.SpsaCases)
//                .Map(dest => dest.SupportByPeriod, src =>
//                    src.SpsaCases
//                        .GroupBy(c => c.AppliedPeriod)
//                        .Select(g => new PeriodSupportStats
//                        {
//                            PeriodName = g.Key.Name,
//                            ActiveCaseCount = g.Count(c => c.IsActive),
//                            TotalHoursSought = g.Sum(c => c.HoursSought),
//                            TotalHoursSpent = g.Sum(c => c.HoursSpent),
//                            TeacherPayments = g.Sum(c =>
//                                c.TeacherPayment != null ? c.TeacherPayment.Amount : 0),
//                            StudentPayments = g.Sum(c =>
//                                c.StudentPayment != null ? c.StudentPayment.Amount : 0)
//                        }));

//            // Create DTO to Model mapping
//            TypeAdapterConfig<CreateEduCategoryDto, EduCategoryModel>
//                .NewConfig()
//                .Map(dest => dest.Name, src => src.Name)
//                .Ignore(dest => dest.Id)
//                .Ignore(dest => dest.Educations)
//                .Ignore(dest => dest.SpsaCases);

//            // Update DTO to Model mapping
//            TypeAdapterConfig<UpdateEduCategoryDto, EduCategoryModel>
//                .NewConfig()
//                .Map(dest => dest.Id, src => src.Id)
//                .Map(dest => dest.Name, src => src.Name)
//                .Ignore(dest => dest.Educations)
//                .Ignore(dest => dest.SpsaCases);
//        }

//        private static void ConfigurePlaceMappings()
//        {
//            // Basic Place DTO mapping
//            TypeAdapterConfig<PlaceModel, PlaceDto>
//                .NewConfig()
//                .Map(dest => dest.TeacherCount, src => src.SupportingTeachers != null ?
//                    src.SupportingTeachers.Count : 0);

//            // Place Detail DTO mapping
//            TypeAdapterConfig<PlaceModel, PlaceDetailDto>
//                .NewConfig()
//                .Map(dest => dest.BasicInfo, src => src)
//                .Map(dest => dest.Teachers, src => src.SupportingTeachers)
//                .Map(dest => dest.TeacherWorkloads, src =>
//                    src.SupportingTeachers.Select(t => new TeacherWorkload
//                    {
//                        TeacherName = t.Name,
//                        ActiveCaseCount = t.SpsaCases.Count(c => c.IsActive),
//                        TotalHoursSought = t.SpsaCases
//                            .Where(c => c.IsActive)
//                            .Sum(c => c.HoursSought),
//                        TotalHoursSpent = t.SpsaCases
//                            .Where(c => c.IsActive)
//                            .Sum(c => c.HoursSpent),
//                        CompletionRate = t.SpsaCases
//                            .Where(c => c.IsActive && c.HoursSought > 0)
//                            .Average(c => (double)c.HoursSpent / c.HoursSought)
//                    }));

//            // Create DTO to Model mapping
//            TypeAdapterConfig<CreatePlaceDto, PlaceModel>
//                .NewConfig()
//                .Map(dest => dest.Name, src => src.Name)
//                .Map(dest => dest.PlaceNumber, src => src.PlaceNumber)
//                .Map(dest => dest.Alias, src => src.Alias)
//                .Ignore(dest => dest.Id)
//                .Ignore(dest => dest.SupportingTeachers);

//            // Update DTO to Model mapping
//            TypeAdapterConfig<UpdatePlaceDto, PlaceModel>
//                .NewConfig()
//                .Map(dest => dest.Id, src => src.Id)
//                .Map(dest => dest.Name, src => src.Name)
//                .Map(dest => dest.PlaceNumber, src => src.PlaceNumber)
//                .Map(dest => dest.Alias, src => src.Alias)
//                .Ignore(dest => dest.SupportingTeachers);
//        }
//    }
// }