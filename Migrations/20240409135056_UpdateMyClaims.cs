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
                    VALUES ('1', 'CanUpdateDetails'), 
                           ('2', 'CanDeleteContacts'),
                           ('3', 'CanGetExistingContactsBySearchTerm'),
                           ('4', 'CanGetSingleContactById'),
                           ('5', 'CanGetPaginatedExistingContacts'),
                           ('6', 'CanAssignClaim'),
                           ('7', 'CanAssignRole')
            ");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
