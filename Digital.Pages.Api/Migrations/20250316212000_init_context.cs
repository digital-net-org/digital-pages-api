using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Digital.Pages.Api.Migrations
{
    /// <inheritdoc />
    public partial class init_context : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "digital_pages");

            migrationBuilder.CreateTable(
                name: "Frame",
                schema: "digital_pages",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "character varying(1024)", maxLength: 1024, nullable: false),
                    Data = table.Column<string>(type: "text", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Frame", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "View",
                schema: "digital_pages",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Title = table.Column<string>(type: "character varying(1024)", maxLength: 1024, nullable: false),
                    Path = table.Column<string>(type: "character varying(128)", maxLength: 128, nullable: false),
                    IsPublished = table.Column<bool>(type: "boolean", nullable: false),
                    FrameId = table.Column<Guid>(type: "uuid", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_View", x => x.Id);
                    table.ForeignKey(
                        name: "FK_View_Frame_FrameId",
                        column: x => x.FrameId,
                        principalSchema: "digital_pages",
                        principalTable: "Frame",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Frame_Name",
                schema: "digital_pages",
                table: "Frame",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_View_FrameId",
                schema: "digital_pages",
                table: "View",
                column: "FrameId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_View_Path",
                schema: "digital_pages",
                table: "View",
                column: "Path",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_View_Title",
                schema: "digital_pages",
                table: "View",
                column: "Title",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "View",
                schema: "digital_pages");

            migrationBuilder.DropTable(
                name: "Frame",
                schema: "digital_pages");
        }
    }
}
