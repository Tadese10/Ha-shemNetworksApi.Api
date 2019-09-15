using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Ha_shemNetworksApi.Api.Migrations
{
    public partial class UpdatedBookColumns : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "BookDate",
                table: "Books",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "Status",
                table: "Books",
                nullable: false,
                defaultValue: false);

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                column: "Password",
                value: "1000000.h+7q5Uy2L4OoJC/xRWrBBw==.BwmBKEGc6VV1w/xKoCQNlLFbdxJ2NBJ7m0VKoPCmTIU=");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BookDate",
                table: "Books");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "Books");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                column: "Password",
                value: "1000000.6QkijDPY6kjCUMI8dhWt3g==.3TwPZXq7fk5I787mjCOJa2nZzwwU/eOOLVlm7PfWWQg=");
        }
    }
}
