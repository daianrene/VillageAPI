using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace VillageAPI.Migrations
{
    /// <inheritdoc />
    public partial class OneSeedVillage : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Villages",
                columns: new[] { "Id", "CreatedDate", "Description", "M2", "Name", "Population", "UpdatedTime" },
                values: new object[] { 1, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Description Test", 4000, "VillaTest", 999, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Villages",
                keyColumn: "Id",
                keyValue: 1);
        }
    }
}
