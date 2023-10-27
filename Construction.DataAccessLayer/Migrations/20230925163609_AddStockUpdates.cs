using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Construction.DataAccessLayer.Migrations
{
    /// <inheritdoc />
    public partial class AddStockUpdates : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "StockUpdates",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProjectId = table.Column<int>(type: "int", nullable: false),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    StockType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Old = table.Column<double>(type: "float", nullable: false),
                    New = table.Column<double>(type: "float", nullable: false),
                    Used = table.Column<double>(type: "float", nullable: false),
                    Remaining = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StockUpdates", x => x.Id);
                    table.ForeignKey(
                        name: "FK_StockUpdates_Projects_ProjectId",
                        column: x => x.ProjectId,
                        principalTable: "Projects",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_StockUpdates_ProjectId",
                table: "StockUpdates",
                column: "ProjectId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "StockUpdates");
        }
    }
}
