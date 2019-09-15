using Microsoft.EntityFrameworkCore.Migrations;

namespace Ha_shemNetworksApi.Api.Migrations
{
    public partial class AddedUserIdToBook : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "UserId",
                table: "Books",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                column: "Password",
                value: "1000000.6QkijDPY6kjCUMI8dhWt3g==.3TwPZXq7fk5I787mjCOJa2nZzwwU/eOOLVlm7PfWWQg=");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Books");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                column: "Password",
                value: "1000000.1lwW8jxFzDPMyjQ4h1snQw==.3pN2/j1cqH9lfIoNHZJobSUu1NKGaEw1oRHmzbVPmi0=");
        }
    }
}
