using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using sps.Domain.Model.Entities;
using sps.Domain.Model.Services;

namespace sps.DAL.Configurations
{
    public class SpsaCaseConfiguration : IEntityTypeConfiguration<SpsaCase>
    {
        private readonly IEncryptionService _encryptionService;

        public SpsaCaseConfiguration(IEncryptionService encryptionService)
        {
            _encryptionService = encryptionService;
        }

        public void Configure(EntityTypeBuilder<SpsaCase> builder)
        {
            builder.HasKey(sc => sc.Id);

            builder.Property(sc => sc.SpsaCaseNumber)
                .IsRequired();

            builder.Property(sc => sc.HoursSought)
                .IsRequired();

            builder.Property(sc => sc.HoursSpent)
                .IsRequired();

            builder.Property(sc => sc.SupportRate)
                .HasPrecision(18, 2)  // Add precision for monetary value
                .IsRequired();

            builder.Property(sc => sc.Comments)
                .IsRequired(false);

            builder.Property(sc => sc.IsActive)
                .IsRequired();

            builder.Property(sc => sc.ApplicationDate)
                .IsRequired(false);

            builder.Property(sc => sc.LatestReapplicationDate)
                .IsRequired(false);

            builder.Property(sc => sc.CourseDescriptionReceived)
                .IsRequired();

            builder.Property(sc => sc.TimesheetReceived)
                .IsRequired();

            builder.Property(sc => sc.StudentRefundReleased)
                .IsRequired();

            builder.Property(sc => sc.TeacherRefundReleased)
                .IsRequired();

            // Configure relationships
            builder.HasOne(sc => sc.Student)
                .WithMany(s => s.SpsaCases)
                .HasForeignKey(sc => sc.StudentId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(sc => sc.SupportingTeacher)
                .WithMany(st => st.SpsaCases)
                .HasForeignKey(sc => sc.SupportingTeacherId)
                .OnDelete(DeleteBehavior.SetNull);

            builder.HasOne(sc => sc.AppliedPeriod)
                .WithMany(p => p.SpsaCases)
                .HasForeignKey(sc => sc.AppliedPeriodId)
                .OnDelete(DeleteBehavior.SetNull);

            builder.HasOne(sc => sc.Diagnosis)
                .WithMany(d => d.SpsaCases)
                .HasForeignKey(sc => sc.DiagnosisId)
                .OnDelete(DeleteBehavior.SetNull);

            builder.HasOne(sc => sc.EduCategory)
                .WithMany(ec => ec.SpsaCases)
                .HasForeignKey(sc => sc.EduCategoryId)
                .OnDelete(DeleteBehavior.SetNull);

            builder.HasOne(sc => sc.SupportType)
                .WithMany(st => st.SpsaCases)
                .HasForeignKey(sc => sc.SupportTypeId)
                .OnDelete(DeleteBehavior.SetNull);

            builder.HasOne(sc => sc.EduStatus)
                .WithMany(es => es.SpsaCases)
                .HasForeignKey(sc => sc.EduStatusId)
                .OnDelete(DeleteBehavior.SetNull);

            // Configure relationship with Comments
            builder.HasMany(sc => sc.Comments)
                .WithOne(c => c.SpsaCase)
                .HasForeignKey(c => c.SpsaCaseId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}