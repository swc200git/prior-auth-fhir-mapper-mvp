using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Api.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "PriorAuthRecords",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    ReceivedAtUtc = table.Column<DateTime>(type: "TEXT", nullable: false),
                    PatientId = table.Column<string>(type: "TEXT", nullable: false),
                    ProviderNpi = table.Column<string>(type: "TEXT", nullable: false),
                    ServiceCode = table.Column<string>(type: "TEXT", nullable: false),
                    DiagnosisCode = table.Column<string>(type: "TEXT", nullable: true),
                    Status = table.Column<string>(type: "TEXT", nullable: false),
                    RawRequestJson = table.Column<string>(type: "TEXT", nullable: false),
                    FhirClaimJson = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PriorAuthRecords", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PriorAuthRecords_ReceivedAtUtc",
                table: "PriorAuthRecords",
                column: "ReceivedAtUtc");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PriorAuthRecords");
        }
    }
}
