using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InnoShop.UserService.Domain.Migrations
{
    /// <inheritdoc />
    public partial class AddedPasswordRecoveryCodeField : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "PasswordResetCodeToken",
                table: "Users",
                type: "nvarchar(255)",
                maxLength: 255,
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PasswordResetCodeToken",
                table: "Users");
        }
    }
}