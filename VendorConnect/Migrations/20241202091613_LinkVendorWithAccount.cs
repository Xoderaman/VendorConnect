using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace VendorConnect.Migrations
{
    /// <inheritdoc />
    public partial class LinkVendorWithAccount : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Vendor",
                columns: table => new
                {
                    VendorId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    VendorRegion = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    VendorGst = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    VendorState = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    VendorCreateAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    VendorEmail = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Vendor", x => x.VendorId);
                    table.ForeignKey(
                        name: "FK_Vendor_AccountTable_UserId",
                        column: x => x.UserId,
                        principalTable: "AccountTable",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Vendor_UserId",
                table: "Vendor",
                column: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Vendor");
        }
    }
}
