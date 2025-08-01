using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TPMTwinAPI.Migrations
{
    /// <inheritdoc />
    public partial class updatingclassforitemtype : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ParentItemId",
                table: "SprintCandidates",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Type",
                table: "SprintCandidates",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ParentItemId",
                table: "SprintCandidates");

            migrationBuilder.DropColumn(
                name: "Type",
                table: "SprintCandidates");
        }
    }
}
