using Microsoft.EntityFrameworkCore.Migrations;

namespace prs_server.Migrations
{
    public partial class changedfirstnamespelling : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Product_Vendors_VendorId",
                table: "Product");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Vendors",
                table: "Vendors");

            migrationBuilder.RenameTable(
                name: "Vendors",
                newName: "Vendor");

            migrationBuilder.RenameColumn(
                name: "Firtsname",
                table: "User",
                newName: "Firstname");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Vendor",
                table: "Vendor",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_User_Username",
                table: "User",
                column: "Username",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Product_PartNbr",
                table: "Product",
                column: "PartNbr",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Vendor_Code",
                table: "Vendor",
                column: "Code",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Product_Vendor_VendorId",
                table: "Product",
                column: "VendorId",
                principalTable: "Vendor",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Product_Vendor_VendorId",
                table: "Product");

            migrationBuilder.DropIndex(
                name: "IX_User_Username",
                table: "User");

            migrationBuilder.DropIndex(
                name: "IX_Product_PartNbr",
                table: "Product");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Vendor",
                table: "Vendor");

            migrationBuilder.DropIndex(
                name: "IX_Vendor_Code",
                table: "Vendor");

            migrationBuilder.RenameTable(
                name: "Vendor",
                newName: "Vendors");

            migrationBuilder.RenameColumn(
                name: "Firstname",
                table: "User",
                newName: "Firtsname");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Vendors",
                table: "Vendors",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Product_Vendors_VendorId",
                table: "Product",
                column: "VendorId",
                principalTable: "Vendors",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
