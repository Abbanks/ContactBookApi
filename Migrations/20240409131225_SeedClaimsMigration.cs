using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ContactPageApi.Migrations
{
    /// <inheritdoc />
    public partial class SeedClaimsMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"
                    INSERT INTO MyClaims (Id, Name) 
                    VALUES ('1', 'CanAdd'), 
                           ('2', 'CanUpdate'), 
                           ('3', 'CanDelete'),
                           ('4', 'CanGetExistingRecords')

            ");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
