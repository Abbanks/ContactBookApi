using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ContactPageApi.Migrations
{
    /// <inheritdoc />
    public partial class SeedRolesMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"
                    INSERT INTO AspNetRoles (Id, Name, NormalizedName, ConcurrencyStamp) 
                    VALUES ('1', 'regular', 'REGULAR', '40EDA8B1-7D6A-4396-B855-78480CE120AD'),  
                           ('2', 'admin', 'ADMIN', 'C0D5CFC5-6D37-4E08-BCCF-3C459FCAADD6')
            ");

        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
