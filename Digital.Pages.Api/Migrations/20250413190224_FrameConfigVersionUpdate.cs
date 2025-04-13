using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Digital.Pages.Api.Migrations
{
    /// <inheritdoc />
    public partial class FrameConfigVersionUpdate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsPublished",
                schema: "digital_pages",
                table: "FrameConfig");

            migrationBuilder.AddColumn<int>(
                name: "ConfigId",
                schema: "digital_pages",
                table: "Frame",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Frame_FrameConfig_ConfigId",
                schema: "digital_pages",
                table: "Frame");

            migrationBuilder.DropForeignKey(
                name: "FK_FrameConfig_Document_DocumentId",
                schema: "digital_pages",
                table: "FrameConfig");
        }
    }
}
