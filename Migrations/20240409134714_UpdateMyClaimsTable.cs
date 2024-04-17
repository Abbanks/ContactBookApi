using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ContactPageApi.Migrations
{
    /// <inheritdoc />
    public partial class UpdateMyClaimsTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {

            migrationBuilder.Sql(@"
                 DELETE FROM MyClaims
            ");

        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
