using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace NZWalksAPI.Migrations
{
    /// <inheritdoc />
    public partial class SeedDataforDifficultiesandregions : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Difficulties",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { new Guid("25a40027-f82d-4f0a-af0e-7981826379cd"), "Medium" },
                    { new Guid("a65165d3-293e-4756-be70-10bd10304099"), "Easy" }
                });

            migrationBuilder.InsertData(
                table: "Regions",
                columns: new[] { "Id", "Code", "Name", "RegionImageUrl" },
                values: new object[,]
                {
                    { new Guid("21f6b252-7f56-44b7-90ae-a8a92e2e3107"), "AKL", "Auckland", "region-image-url" },
                    { new Guid("72d9505e-9442-4255-be1a-36f6d012bce2"), "DND", "Dunedin", "region-image-url" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Difficulties",
                keyColumn: "Id",
                keyValue: new Guid("25a40027-f82d-4f0a-af0e-7981826379cd"));

            migrationBuilder.DeleteData(
                table: "Difficulties",
                keyColumn: "Id",
                keyValue: new Guid("a65165d3-293e-4756-be70-10bd10304099"));

            migrationBuilder.DeleteData(
                table: "Regions",
                keyColumn: "Id",
                keyValue: new Guid("21f6b252-7f56-44b7-90ae-a8a92e2e3107"));

            migrationBuilder.DeleteData(
                table: "Regions",
                keyColumn: "Id",
                keyValue: new Guid("72d9505e-9442-4255-be1a-36f6d012bce2"));
        }
    }
}
