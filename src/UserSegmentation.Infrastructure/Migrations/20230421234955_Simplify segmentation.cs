using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace UserSegmentation.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Simplifysegmentation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SegmentReference_Assignment",
                table: "User");

            migrationBuilder.RenameColumn(
                name: "SegmentReference_Id",
                table: "User",
                newName: "SegmentId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "SegmentId",
                table: "User",
                newName: "SegmentReference_Id");

            migrationBuilder.AddColumn<int>(
                name: "SegmentReference_Assignment",
                table: "User",
                type: "INTEGER",
                nullable: true);
        }
    }
}
