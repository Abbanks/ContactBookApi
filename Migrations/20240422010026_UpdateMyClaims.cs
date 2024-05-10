using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ContactPageApi.Migrations
{
    /// <inheritdoc />
    public partial class UpdateMyClaims : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"
                    INSERT INTO MyClaims (Id, Name) 
                    

            ");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
