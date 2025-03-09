using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using sps.Domain.Model.Entities;

namespace sps.DAL.Configurations
{
    public class SpsaCaseConfiguration : IEntityTypeConfiguration<SpsaCase>
    {
        public void Configure(EntityTypeBuilder<SpsaCase> builder)
        {
            builder.HasKey(sc => sc.Id);

            // Configure properties
            builder.Property(sc => sc.SpsaCaseNumber).IsRequired();
            builder.Property(sc => sc.HoursSought).IsRequired();
            builder.Property(sc => sc.HoursSpent).IsRequired();
            builder.Property(sc => sc.Comment).IsRequired(false);
            builder.Property(sc => sc.IsActive).IsRequired();
            builder.Property(sc => sc.ApplicationDate).IsRequired(false);
            builder.Property(sc => sc.LatestReapplicationDate).IsRequired(false);
            
            // Configure additional status fields
            builder.Property(sc => sc.CourseDescriptionReceived).IsRequired();
            builder.Property(sc => sc.TimesheetReceived).IsRequired();
            builder.Property(sc => sc.StudentRefundReleased).IsRequired();
            builder.Property(sc => sc.TeacherRefundReleased).IsRequired();
            builder.Property(sc => sc.SupportRate).HasColumnType("decimal(18,2)").IsRequired();

            // Configure comments relationship
            builder.HasMany(sc => sc.Comments)
                .WithOne(c => c.SpsaCase)
                .HasForeignKey(c => c.SpsaCaseId)
                .OnDelete(DeleteBehavior.Cascade);

            // Configure relationships
            builder.HasOne(sc => sc.Student)
                .WithMany(s => s.SpsaCases)
                .HasForeignKey(sc => sc.StudentId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(sc => sc.SupportingTeacher)
                .WithMany()
                .HasForeignKey(sc => sc.SupportingTeacherId)
                .IsRequired(false)
                .OnDelete(DeleteBehavior.SetNull);

            builder.HasOne(sc => sc.AppliedPeriod)
                .WithMany()
                .HasForeignKey(sc => sc.AppliedPeriodId)
                .IsRequired(false)
                .OnDelete(DeleteBehavior.SetNull);

            builder.HasOne(sc => sc.Diagnosis)
                .WithMany(d => d.SpsaCases)
                .HasForeignKey(sc => sc.DiagnosisId)
                .IsRequired(false)
                .OnDelete(DeleteBehavior.SetNull);

            builder.HasOne(sc => sc.EduCategory)
                .WithMany()
                .HasForeignKey(sc => sc.EduCategoryId)
                .IsRequired(false)
                .OnDelete(DeleteBehavior.SetNull);

            builder.HasOne(sc => sc.SupportType)
                .WithMany()
                .HasForeignKey(sc => sc.SupportTypeId)
                .IsRequired(false)
                .OnDelete(DeleteBehavior.SetNull);

            builder.HasOne(sc => sc.EduStatus)
                .WithMany()
                .HasForeignKey(sc => sc.EduStatusId)
                .IsRequired(false)
                .OnDelete(DeleteBehavior.SetNull);

            builder.HasOne(sc => sc.TeacherPayment)
                .WithMany()
                .HasForeignKey(sc => sc.TeacherPaymentId)
                .IsRequired(false)
                .OnDelete(DeleteBehavior.SetNull);

            builder.HasOne(sc => sc.OpkvalSupervision)
                .WithMany()
                .HasForeignKey(sc => sc.OpkvalSupervisionId)
                .IsRequired(false)
                .OnDelete(DeleteBehavior.SetNull);

            builder.HasOne(sc => sc.StudentPayment)
                .WithMany()
                .HasForeignKey(sc => sc.StudentPaymentId)
                .IsRequired(false)
                .OnDelete(DeleteBehavior.SetNull);
        }
    }
}