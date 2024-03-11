using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MaccabiSearch.Domain.Services.Implementations.EntityFramework.Migrations
{
    /// <inheritdoc />
    public partial class CreateTableSearchResults : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "search_results",
                columns: table => new
                {
                    id = table.Column<string>(type: "text", nullable: false, defaultValueSql: "uuid_generate_v4()"),
                    title = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    entered_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP"),
                    search_engine = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_search_results", x => x.id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_search_results_title",
                table: "search_results",
                column: "title");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "search_results");
        }
    }
}
