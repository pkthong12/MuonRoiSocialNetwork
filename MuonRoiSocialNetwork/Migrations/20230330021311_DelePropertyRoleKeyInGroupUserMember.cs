using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MuonRoiSocialNetwork.Migrations
{
    public partial class DelePropertyRoleKeyInGroupUserMember : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AppRoleKey",
                table: "GroupUserMember");

            migrationBuilder.AlterColumn<string>(
                name: "GroupName",
                table: "GroupUserMember",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.UpdateData(
                table: "AppRole",
                keyColumn: "Id",
                keyValue: new Guid("5ef7d163-8249-445c-8895-4eb97329af7e"),
                column: "ConcurrencyStamp",
                value: "e9a8d6a0-c4fc-4c50-887f-7a5481212442");

            migrationBuilder.UpdateData(
                table: "AppRole",
                keyColumn: "Id",
                keyValue: new Guid("72377426-b057-46ca-98ff-1ca9d33ea0c1"),
                column: "ConcurrencyStamp",
                value: "ac8eb0ab-0b83-4f52-81c0-66544cf8899b");

            migrationBuilder.UpdateData(
                table: "AppUsers",
                keyColumn: "Id",
                keyValue: new Guid("05075755-688d-4987-9c1e-f3bef1746d52"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "ab51b3b2-9738-439e-b6c0-48f86423912b", "123456Az*" });

            migrationBuilder.UpdateData(
                table: "AppUsers",
                keyColumn: "Id",
                keyValue: new Guid("e0223a03-2945-49db-976e-736433465b7f"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "b59f9499-c066-4066-a5c7-2a28dbe79ef9", "0967442142Az*" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "GroupName",
                table: "GroupUserMember",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<Guid>(
                name: "AppRoleKey",
                table: "GroupUserMember",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.UpdateData(
                table: "AppRole",
                keyColumn: "Id",
                keyValue: new Guid("5ef7d163-8249-445c-8895-4eb97329af7e"),
                column: "ConcurrencyStamp",
                value: "8c52c918-3751-4bb3-9d5e-4181e04b72e7");

            migrationBuilder.UpdateData(
                table: "AppRole",
                keyColumn: "Id",
                keyValue: new Guid("72377426-b057-46ca-98ff-1ca9d33ea0c1"),
                column: "ConcurrencyStamp",
                value: "b82f68cc-3e01-4e36-b6f5-de76de487b67");

            migrationBuilder.UpdateData(
                table: "AppUsers",
                keyColumn: "Id",
                keyValue: new Guid("05075755-688d-4987-9c1e-f3bef1746d52"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "a6d822cd-3118-4955-a362-75a70fe403b1", "AQAAAAEAACcQAAAAEEWBp9ddUBjOYr2RoiHGdN+f8qYrXx6QK9mHxXWwZ2JcY62NVvlqs80KqhwftFvDXQ==" });

            migrationBuilder.UpdateData(
                table: "AppUsers",
                keyColumn: "Id",
                keyValue: new Guid("e0223a03-2945-49db-976e-736433465b7f"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "86e05609-f8e0-4222-acb6-a0380e2d8111", "AQAAAAEAACcQAAAAEKcdDFUGDB+9TAeNBzAoURL8TdBe9EYKzoUzSa1+Wt2RkU0B3UATcj5guItHf2D78w==" });

            migrationBuilder.UpdateData(
                table: "GroupUserMember",
                keyColumn: "Id",
                keyValue: 1,
                column: "AppRoleKey",
                value: new Guid("72377426-b057-46ca-98ff-1ca9d33ea0c1"));

            migrationBuilder.UpdateData(
                table: "GroupUserMember",
                keyColumn: "Id",
                keyValue: 2,
                column: "AppRoleKey",
                value: new Guid("5ef7d163-8249-445c-8895-4eb97329af7e"));
        }
    }
}
