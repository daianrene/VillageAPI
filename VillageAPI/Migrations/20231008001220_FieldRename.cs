using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace VillageAPI.Migrations
{
    /// <inheritdoc />
    public partial class FieldRename : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "UpdatedTime",
                table: "Villages",
                newName: "UpdatedDate");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "UpdatedDate",
                table: "Villages",
                newName: "UpdatedTime");
        }
    }
}
