using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Add_Default_Language : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("INSERT INTO Languages (Id, Code, IsDefault) VALUES ('1BC7D4F7-436F-4794-B264-BB1663CA6671', 'en-US', 1)");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("REMOVE FROM Languages WHERE Id = '1BC7D4F7-436F-4794-B264-BB1663CA6671'");
        }
    }
}
