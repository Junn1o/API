using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace API.Migrations
{
    /// <inheritdoc />
    public partial class UpdateDatabasev1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "image",
                table: "User");

            migrationBuilder.DropColumn(
                name: "image",
                table: "Photo");

            migrationBuilder.DropColumn(
                name: "image",
                table: "Author");

            migrationBuilder.RenameColumn(
                name: "Area",
                table: "Room",
                newName: "area");

            migrationBuilder.AddColumn<string>(
                name: "imagepath",
                table: "User",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "imagepath",
                table: "Photo",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "imagepath",
                table: "Author",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "imagepath",
                table: "User");

            migrationBuilder.DropColumn(
                name: "imagepath",
                table: "Photo");

            migrationBuilder.DropColumn(
                name: "imagepath",
                table: "Author");

            migrationBuilder.RenameColumn(
                name: "area",
                table: "Room",
                newName: "Area");

            migrationBuilder.AddColumn<byte[]>(
                name: "image",
                table: "User",
                type: "varbinary(max)",
                nullable: true);

            migrationBuilder.AddColumn<byte[]>(
                name: "image",
                table: "Photo",
                type: "varbinary(max)",
                nullable: false,
                defaultValue: new byte[0]);

            migrationBuilder.AddColumn<byte[]>(
                name: "image",
                table: "Author",
                type: "varbinary(max)",
                nullable: true);
        }
    }
}
