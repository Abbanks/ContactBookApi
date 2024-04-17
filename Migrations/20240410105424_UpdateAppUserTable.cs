using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ContactPageApi.Migrations
{
    /// <inheritdoc />
    public partial class UpdateAppUserTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ProfilePhoto",
                table: "AspNetUsers",
                newName: "PublicId");

            migrationBuilder.AddColumn<string>(
                name: "PhotoUrl",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PhotoUrl",
                table: "AspNetUsers");

            migrationBuilder.RenameColumn(
                name: "PublicId",
                table: "AspNetUsers",
                newName: "ProfilePhoto");
        }
    }
}
