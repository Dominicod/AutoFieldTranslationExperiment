using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AutoFieldTranslationExperiment.Data.Migrations
{
    /// <inheritdoc />
    public partial class Add_Language_To_DB : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "LanguageCode",
                table: "Translations");

            migrationBuilder.AddColumn<Guid>(
                name: "LanguageId",
                table: "Translations",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateTable(
                name: "Languages",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Code = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Languages", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Translations_LanguageId",
                table: "Translations",
                column: "LanguageId");

            migrationBuilder.AddForeignKey(
                name: "FK_Translations_Languages_LanguageId",
                table: "Translations",
                column: "LanguageId",
                principalTable: "Languages",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Translations_Languages_LanguageId",
                table: "Translations");

            migrationBuilder.DropTable(
                name: "Languages");

            migrationBuilder.DropIndex(
                name: "IX_Translations_LanguageId",
                table: "Translations");

            migrationBuilder.DropColumn(
                name: "LanguageId",
                table: "Translations");

            migrationBuilder.AddColumn<string>(
                name: "LanguageCode",
                table: "Translations",
                type: "nvarchar(10)",
                maxLength: 10,
                nullable: false,
                defaultValue: "");
        }
    }
}
