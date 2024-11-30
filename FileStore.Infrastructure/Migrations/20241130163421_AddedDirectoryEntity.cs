using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FileStore.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddedDirectoryEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "File");

            migrationBuilder.DropColumn(
                name: "UpdatedAt",
                table: "File");

            migrationBuilder.RenameColumn(
                name: "UpdatedBy",
                table: "File",
                newName: "OwnerId");

            migrationBuilder.CreateTable(
                name: "Directory",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    DirectoryName = table.Column<string>(type: "text", nullable: false),
                    ParentDirectoryId = table.Column<Guid>(type: "uuid", nullable: true),
                    OwnerId = table.Column<Guid>(type: "uuid", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Directory", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_File_DirectoryId",
                table: "File",
                column: "DirectoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_File_Directory_DirectoryId",
                table: "File",
                column: "DirectoryId",
                principalTable: "Directory",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_File_Directory_DirectoryId",
                table: "File");

            migrationBuilder.DropTable(
                name: "Directory");

            migrationBuilder.DropIndex(
                name: "IX_File_DirectoryId",
                table: "File");

            migrationBuilder.RenameColumn(
                name: "OwnerId",
                table: "File",
                newName: "UpdatedBy");

            migrationBuilder.AddColumn<Guid>(
                name: "CreatedBy",
                table: "File",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedAt",
                table: "File",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }
    }
}
