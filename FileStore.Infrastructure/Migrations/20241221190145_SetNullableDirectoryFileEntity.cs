using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FileStore.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class SetNullableDirectoryFileEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_File_Directory_DirectoryId",
                table: "File");

            migrationBuilder.AlterColumn<Guid>(
                name: "DirectoryId",
                table: "File",
                type: "uuid",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uuid");

            migrationBuilder.AddForeignKey(
                name: "FK_File_Directory_DirectoryId",
                table: "File",
                column: "DirectoryId",
                principalTable: "Directory",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_File_Directory_DirectoryId",
                table: "File");

            migrationBuilder.AlterColumn<Guid>(
                name: "DirectoryId",
                table: "File",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uuid",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_File_Directory_DirectoryId",
                table: "File",
                column: "DirectoryId",
                principalTable: "Directory",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
