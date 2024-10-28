using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AutoFieldTranslationExperiment.Infrastructure.Data.Migrations
{
    /// <inheritdoc />
    public partial class Add_IsDefault_To_Migrations : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsDefault",
                table: "Languages",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsDefault",
                table: "Languages");
        }
    }
}
