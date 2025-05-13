using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Hillarys_Hair_Care_Server.Migrations
{
    /// <inheritdoc />
    public partial class Create : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_StylistServices_ServiceId",
                table: "StylistServices",
                column: "ServiceId");

            migrationBuilder.CreateIndex(
                name: "IX_StylistServices_StylistId",
                table: "StylistServices",
                column: "StylistId");

            migrationBuilder.AddForeignKey(
                name: "FK_StylistServices_Services_ServiceId",
                table: "StylistServices",
                column: "ServiceId",
                principalTable: "Services",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_StylistServices_Stylists_StylistId",
                table: "StylistServices",
                column: "StylistId",
                principalTable: "Stylists",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_StylistServices_Services_ServiceId",
                table: "StylistServices");

            migrationBuilder.DropForeignKey(
                name: "FK_StylistServices_Stylists_StylistId",
                table: "StylistServices");

            migrationBuilder.DropIndex(
                name: "IX_StylistServices_ServiceId",
                table: "StylistServices");

            migrationBuilder.DropIndex(
                name: "IX_StylistServices_StylistId",
                table: "StylistServices");
        }
    }
}
