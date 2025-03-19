using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace sps.DAL.Migrations
{
    /// <inheritdoc />
    public partial class InitialDbSetup : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Diagnoses",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Diagnoses", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "EduCategories",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EduCategories", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "EduStatuses",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EduStatuses", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "OpkvalSupervisions",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreateDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastUpdatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    HoursSought = table.Column<int>(type: "int", nullable: true),
                    QualificationHoursSpent = table.Column<int>(type: "int", nullable: false),
                    SupervisionHoursSpent = table.Column<int>(type: "int", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OpkvalSupervisions", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Periods",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Periods", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Places",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PlaceNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Alias = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Places", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SensitiveString",
                columns: table => new
                {
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                });

            migrationBuilder.CreateTable(
                name: "SupportTypes",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SupportTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Educations",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    EduCategoryId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Educations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Educations_EduCategories_EduCategoryId",
                        column: x => x.EduCategoryId,
                        principalTable: "EduCategories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "SupportingTeachers",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PlacesId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SupportingTeachers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SupportingTeachers_Places_PlacesId",
                        column: x => x.PlacesId,
                        principalTable: "Places",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateTable(
                name: "StudentPayments",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    AccountNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Amount = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    ExternalVoucherNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    VoucherText = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CompleteVoucherText = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SupportTypeId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StudentPayments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_StudentPayments_SupportTypes_SupportTypeId",
                        column: x => x.SupportTypeId,
                        principalTable: "SupportTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateTable(
                name: "TeacherPayments",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Amount = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    ExternalVoucherNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    VoucherText = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CompleteVoucherText = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SupportTypeId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TeacherPayments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TeacherPayments_SupportTypes_SupportTypeId",
                        column: x => x.SupportTypeId,
                        principalTable: "SupportTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateTable(
                name: "EducationPeriodRates",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Amount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    PeriodId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    EducationId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EducationPeriodRates", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EducationPeriodRates_Educations_EducationId",
                        column: x => x.EducationId,
                        principalTable: "Educations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_EducationPeriodRates_Periods_PeriodId",
                        column: x => x.PeriodId,
                        principalTable: "Periods",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Students",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    StudentNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CPRNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FinishedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    StartPeriodId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    EducationId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Students", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Students_Educations_EducationId",
                        column: x => x.EducationId,
                        principalTable: "Educations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "FK_Students_Periods_StartPeriodId",
                        column: x => x.StartPeriodId,
                        principalTable: "Periods",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateTable(
                name: "SpsaCases",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SpsaCaseNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    HoursSought = table.Column<int>(type: "int", nullable: false),
                    HoursSpent = table.Column<int>(type: "int", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    ApplicationDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LatestReapplicationDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CourseDescriptionReceived = table.Column<bool>(type: "bit", nullable: false),
                    TimesheetReceived = table.Column<bool>(type: "bit", nullable: false),
                    StudentRefundReleased = table.Column<bool>(type: "bit", nullable: false),
                    TeacherRefundReleased = table.Column<bool>(type: "bit", nullable: false),
                    SupportRate = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    StudentId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SupportingTeacherId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    AppliedPeriodId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    DiagnosisId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    EduCategoryId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    SupportTypeId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    EduStatusId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    TeacherPaymentId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    OpkvalSupervisionId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    StudentPaymentId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SpsaCases", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SpsaCases_Diagnoses_DiagnosisId",
                        column: x => x.DiagnosisId,
                        principalTable: "Diagnoses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "FK_SpsaCases_EduCategories_EduCategoryId",
                        column: x => x.EduCategoryId,
                        principalTable: "EduCategories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "FK_SpsaCases_EduStatuses_EduStatusId",
                        column: x => x.EduStatusId,
                        principalTable: "EduStatuses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "FK_SpsaCases_OpkvalSupervisions_OpkvalSupervisionId",
                        column: x => x.OpkvalSupervisionId,
                        principalTable: "OpkvalSupervisions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "FK_SpsaCases_Periods_AppliedPeriodId",
                        column: x => x.AppliedPeriodId,
                        principalTable: "Periods",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "FK_SpsaCases_StudentPayments_StudentPaymentId",
                        column: x => x.StudentPaymentId,
                        principalTable: "StudentPayments",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_SpsaCases_Students_StudentId",
                        column: x => x.StudentId,
                        principalTable: "Students",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SpsaCases_SupportTypes_SupportTypeId",
                        column: x => x.SupportTypeId,
                        principalTable: "SupportTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "FK_SpsaCases_SupportingTeachers_SupportingTeacherId",
                        column: x => x.SupportingTeacherId,
                        principalTable: "SupportingTeachers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "FK_SpsaCases_TeacherPayments_TeacherPaymentId",
                        column: x => x.TeacherPaymentId,
                        principalTable: "TeacherPayments",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Comments",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EntityType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SpsaCaseId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    StudentId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    TeacherPaymentId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    StudentPaymentId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    OpkvalSupervisionId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CommentText = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Comments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Comments_OpkvalSupervisions_OpkvalSupervisionId",
                        column: x => x.OpkvalSupervisionId,
                        principalTable: "OpkvalSupervisions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Comments_SpsaCases_SpsaCaseId",
                        column: x => x.SpsaCaseId,
                        principalTable: "SpsaCases",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Comments_StudentPayments_StudentPaymentId",
                        column: x => x.StudentPaymentId,
                        principalTable: "StudentPayments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Comments_Students_StudentId",
                        column: x => x.StudentId,
                        principalTable: "Students",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Comments_TeacherPayments_TeacherPaymentId",
                        column: x => x.TeacherPaymentId,
                        principalTable: "TeacherPayments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Comments_OpkvalSupervisionId",
                table: "Comments",
                column: "OpkvalSupervisionId");

            migrationBuilder.CreateIndex(
                name: "IX_Comments_SpsaCaseId",
                table: "Comments",
                column: "SpsaCaseId");

            migrationBuilder.CreateIndex(
                name: "IX_Comments_StudentId",
                table: "Comments",
                column: "StudentId");

            migrationBuilder.CreateIndex(
                name: "IX_Comments_StudentPaymentId",
                table: "Comments",
                column: "StudentPaymentId");

            migrationBuilder.CreateIndex(
                name: "IX_Comments_TeacherPaymentId",
                table: "Comments",
                column: "TeacherPaymentId");

            migrationBuilder.CreateIndex(
                name: "IX_EducationPeriodRates_EducationId",
                table: "EducationPeriodRates",
                column: "EducationId");

            migrationBuilder.CreateIndex(
                name: "IX_EducationPeriodRates_PeriodId_EducationId",
                table: "EducationPeriodRates",
                columns: new[] { "PeriodId", "EducationId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Educations_EduCategoryId",
                table: "Educations",
                column: "EduCategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_SpsaCases_AppliedPeriodId",
                table: "SpsaCases",
                column: "AppliedPeriodId");

            migrationBuilder.CreateIndex(
                name: "IX_SpsaCases_DiagnosisId",
                table: "SpsaCases",
                column: "DiagnosisId");

            migrationBuilder.CreateIndex(
                name: "IX_SpsaCases_EduCategoryId",
                table: "SpsaCases",
                column: "EduCategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_SpsaCases_EduStatusId",
                table: "SpsaCases",
                column: "EduStatusId");

            migrationBuilder.CreateIndex(
                name: "IX_SpsaCases_OpkvalSupervisionId",
                table: "SpsaCases",
                column: "OpkvalSupervisionId");

            migrationBuilder.CreateIndex(
                name: "IX_SpsaCases_StudentId",
                table: "SpsaCases",
                column: "StudentId");

            migrationBuilder.CreateIndex(
                name: "IX_SpsaCases_StudentPaymentId",
                table: "SpsaCases",
                column: "StudentPaymentId");

            migrationBuilder.CreateIndex(
                name: "IX_SpsaCases_SupportingTeacherId",
                table: "SpsaCases",
                column: "SupportingTeacherId");

            migrationBuilder.CreateIndex(
                name: "IX_SpsaCases_SupportTypeId",
                table: "SpsaCases",
                column: "SupportTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_SpsaCases_TeacherPaymentId",
                table: "SpsaCases",
                column: "TeacherPaymentId");

            migrationBuilder.CreateIndex(
                name: "IX_StudentPayments_SupportTypeId",
                table: "StudentPayments",
                column: "SupportTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Students_EducationId",
                table: "Students",
                column: "EducationId");

            migrationBuilder.CreateIndex(
                name: "IX_Students_StartPeriodId",
                table: "Students",
                column: "StartPeriodId");

            migrationBuilder.CreateIndex(
                name: "IX_SupportingTeachers_PlacesId",
                table: "SupportingTeachers",
                column: "PlacesId");

            migrationBuilder.CreateIndex(
                name: "IX_TeacherPayments_SupportTypeId",
                table: "TeacherPayments",
                column: "SupportTypeId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Comments");

            migrationBuilder.DropTable(
                name: "EducationPeriodRates");

            migrationBuilder.DropTable(
                name: "SensitiveString");

            migrationBuilder.DropTable(
                name: "SpsaCases");

            migrationBuilder.DropTable(
                name: "Diagnoses");

            migrationBuilder.DropTable(
                name: "EduStatuses");

            migrationBuilder.DropTable(
                name: "OpkvalSupervisions");

            migrationBuilder.DropTable(
                name: "StudentPayments");

            migrationBuilder.DropTable(
                name: "Students");

            migrationBuilder.DropTable(
                name: "SupportingTeachers");

            migrationBuilder.DropTable(
                name: "TeacherPayments");

            migrationBuilder.DropTable(
                name: "Educations");

            migrationBuilder.DropTable(
                name: "Periods");

            migrationBuilder.DropTable(
                name: "Places");

            migrationBuilder.DropTable(
                name: "SupportTypes");

            migrationBuilder.DropTable(
                name: "EduCategories");
        }
    }
}
