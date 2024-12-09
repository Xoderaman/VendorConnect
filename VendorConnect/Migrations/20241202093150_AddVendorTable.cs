using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace VendorConnect.Migrations
{
    /// <inheritdoc />
    public partial class AddVendorTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Vendor_AccountTable_UserId",
                table: "Vendor");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Vendor",
                table: "Vendor");

            migrationBuilder.RenameTable(
                name: "Vendor",
                newName: "VendorTable");

            migrationBuilder.RenameIndex(
                name: "IX_Vendor_UserId",
                table: "VendorTable",
                newName: "IX_VendorTable_UserId");

            migrationBuilder.AlterColumn<string>(
                name: "VendorState",
                table: "AccountTable",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "VendorRegion",
                table: "AccountTable",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "VendorGst",
                table: "AccountTable",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_VendorTable",
                table: "VendorTable",
                column: "VendorId");

            migrationBuilder.AddForeignKey(
                name: "FK_VendorTable_AccountTable_UserId",
                table: "VendorTable",
                column: "UserId",
                principalTable: "AccountTable",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_VendorTable_AccountTable_UserId",
                table: "VendorTable");

            migrationBuilder.DropPrimaryKey(
                name: "PK_VendorTable",
                table: "VendorTable");

            migrationBuilder.RenameTable(
                name: "VendorTable",
                newName: "Vendor");

            migrationBuilder.RenameIndex(
                name: "IX_VendorTable_UserId",
                table: "Vendor",
                newName: "IX_Vendor_UserId");

            migrationBuilder.AlterColumn<string>(
                name: "VendorState",
                table: "AccountTable",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "VendorRegion",
                table: "AccountTable",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "VendorGst",
                table: "AccountTable",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Vendor",
                table: "Vendor",
                column: "VendorId");

            migrationBuilder.AddForeignKey(
                name: "FK_Vendor_AccountTable_UserId",
                table: "Vendor",
                column: "UserId",
                principalTable: "AccountTable",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
