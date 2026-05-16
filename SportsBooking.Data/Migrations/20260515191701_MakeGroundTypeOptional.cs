using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SportsBooking.Data.Migrations
{
    /// <inheritdoc />
    public partial class MakeGroundTypeOptional : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SportsGrounds_GroundTypes_GroundTypeId",
                table: "SportsGrounds");

            migrationBuilder.AddForeignKey(
                name: "FK_SportsGrounds_GroundTypes_GroundTypeId",
                table: "SportsGrounds",
                column: "GroundTypeId",
                principalTable: "GroundTypes",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SportsGrounds_GroundTypes_GroundTypeId",
                table: "SportsGrounds");

            migrationBuilder.AddForeignKey(
                name: "FK_SportsGrounds_GroundTypes_GroundTypeId",
                table: "SportsGrounds",
                column: "GroundTypeId",
                principalTable: "GroundTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
