using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MuonRoiSocialNetwork.Migrations
{
    public partial class updateBaseEntityClassIdToGuid : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreatedUserId",
                table: "TagInStory");

            migrationBuilder.DropColumn(
                name: "DeletedUserId",
                table: "TagInStory");

            migrationBuilder.DropColumn(
                name: "UpdatedUserId",
                table: "TagInStory");

            migrationBuilder.DropColumn(
                name: "CreatedUserId",
                table: "Tag");

            migrationBuilder.DropColumn(
                name: "DeletedUserId",
                table: "Tag");

            migrationBuilder.DropColumn(
                name: "UpdatedUserId",
                table: "Tag");

            migrationBuilder.DropColumn(
                name: "CreatedUserId",
                table: "StoryReview");

            migrationBuilder.DropColumn(
                name: "DeletedUserId",
                table: "StoryReview");

            migrationBuilder.DropColumn(
                name: "UpdatedUserId",
                table: "StoryReview");

            migrationBuilder.DropColumn(
                name: "CreatedUserId",
                table: "StoryPublish");

            migrationBuilder.DropColumn(
                name: "DeletedUserId",
                table: "StoryPublish");

            migrationBuilder.DropColumn(
                name: "UpdatedUserId",
                table: "StoryPublish");

            migrationBuilder.DropColumn(
                name: "CreatedUserId",
                table: "StoryNotifications");

            migrationBuilder.DropColumn(
                name: "DeletedUserId",
                table: "StoryNotifications");

            migrationBuilder.DropColumn(
                name: "UpdatedUserId",
                table: "StoryNotifications");

            migrationBuilder.DropColumn(
                name: "CreatedUserId",
                table: "Story");

            migrationBuilder.DropColumn(
                name: "DeletedUserId",
                table: "Story");

            migrationBuilder.DropColumn(
                name: "UpdatedUserId",
                table: "Story");

            migrationBuilder.DropColumn(
                name: "CreatedUserId",
                table: "GroupUserMember");

            migrationBuilder.DropColumn(
                name: "DeletedUserId",
                table: "GroupUserMember");

            migrationBuilder.DropColumn(
                name: "UpdatedUserId",
                table: "GroupUserMember");

            migrationBuilder.DropColumn(
                name: "CreatedUserId",
                table: "FollowingAuthor");

            migrationBuilder.DropColumn(
                name: "DeletedUserId",
                table: "FollowingAuthor");

            migrationBuilder.DropColumn(
                name: "UpdatedUserId",
                table: "FollowingAuthor");

            migrationBuilder.DropColumn(
                name: "CreatedUserId",
                table: "Chapter");

            migrationBuilder.DropColumn(
                name: "DeletedUserId",
                table: "Chapter");

            migrationBuilder.DropColumn(
                name: "UpdatedUserId",
                table: "Chapter");

            migrationBuilder.DropColumn(
                name: "CreatedUserId",
                table: "Category");

            migrationBuilder.DropColumn(
                name: "DeletedUserId",
                table: "Category");

            migrationBuilder.DropColumn(
                name: "UpdatedUserId",
                table: "Category");

            migrationBuilder.DropColumn(
                name: "CreatedUserId",
                table: "BookMarkStory");

            migrationBuilder.DropColumn(
                name: "DeletedUserId",
                table: "BookMarkStory");

            migrationBuilder.DropColumn(
                name: "UpdatedUserId",
                table: "BookMarkStory");

            migrationBuilder.AlterColumn<string>(
                name: "CreatedUserName",
                table: "TagInStory",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100);

            migrationBuilder.AddColumn<Guid>(
                name: "CreatedUserGuid",
                table: "TagInStory",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"))
                .Annotation("Relational:ColumnOrder", 101);

            migrationBuilder.AddColumn<Guid>(
                name: "DeletedUserGuid",
                table: "TagInStory",
                type: "uniqueidentifier",
                nullable: true)
                .Annotation("Relational:ColumnOrder", 103);

            migrationBuilder.AddColumn<Guid>(
                name: "UpdatedUserGuid",
                table: "TagInStory",
                type: "uniqueidentifier",
                nullable: true)
                .Annotation("Relational:ColumnOrder", 102);

            migrationBuilder.AlterColumn<string>(
                name: "CreatedUserName",
                table: "Tag",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100);

            migrationBuilder.AddColumn<Guid>(
                name: "CreatedUserGuid",
                table: "Tag",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"))
                .Annotation("Relational:ColumnOrder", 101);

            migrationBuilder.AddColumn<Guid>(
                name: "DeletedUserGuid",
                table: "Tag",
                type: "uniqueidentifier",
                nullable: true)
                .Annotation("Relational:ColumnOrder", 103);

            migrationBuilder.AddColumn<Guid>(
                name: "UpdatedUserGuid",
                table: "Tag",
                type: "uniqueidentifier",
                nullable: true)
                .Annotation("Relational:ColumnOrder", 102);

            migrationBuilder.AlterColumn<string>(
                name: "CreatedUserName",
                table: "StoryReview",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100);

            migrationBuilder.AddColumn<Guid>(
                name: "CreatedUserGuid",
                table: "StoryReview",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"))
                .Annotation("Relational:ColumnOrder", 101);

            migrationBuilder.AddColumn<Guid>(
                name: "DeletedUserGuid",
                table: "StoryReview",
                type: "uniqueidentifier",
                nullable: true)
                .Annotation("Relational:ColumnOrder", 103);

            migrationBuilder.AddColumn<Guid>(
                name: "UpdatedUserGuid",
                table: "StoryReview",
                type: "uniqueidentifier",
                nullable: true)
                .Annotation("Relational:ColumnOrder", 102);

            migrationBuilder.AlterColumn<string>(
                name: "CreatedUserName",
                table: "StoryPublish",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100);

            migrationBuilder.AddColumn<Guid>(
                name: "CreatedUserGuid",
                table: "StoryPublish",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"))
                .Annotation("Relational:ColumnOrder", 101);

            migrationBuilder.AddColumn<Guid>(
                name: "DeletedUserGuid",
                table: "StoryPublish",
                type: "uniqueidentifier",
                nullable: true)
                .Annotation("Relational:ColumnOrder", 103);

            migrationBuilder.AddColumn<Guid>(
                name: "UpdatedUserGuid",
                table: "StoryPublish",
                type: "uniqueidentifier",
                nullable: true)
                .Annotation("Relational:ColumnOrder", 102);

            migrationBuilder.AlterColumn<string>(
                name: "CreatedUserName",
                table: "StoryNotifications",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100);

            migrationBuilder.AddColumn<Guid>(
                name: "CreatedUserGuid",
                table: "StoryNotifications",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"))
                .Annotation("Relational:ColumnOrder", 101);

            migrationBuilder.AddColumn<Guid>(
                name: "DeletedUserGuid",
                table: "StoryNotifications",
                type: "uniqueidentifier",
                nullable: true)
                .Annotation("Relational:ColumnOrder", 103);

            migrationBuilder.AddColumn<Guid>(
                name: "UpdatedUserGuid",
                table: "StoryNotifications",
                type: "uniqueidentifier",
                nullable: true)
                .Annotation("Relational:ColumnOrder", 102);

            migrationBuilder.AlterColumn<string>(
                name: "CreatedUserName",
                table: "Story",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100);

            migrationBuilder.AddColumn<Guid>(
                name: "CreatedUserGuid",
                table: "Story",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"))
                .Annotation("Relational:ColumnOrder", 101);

            migrationBuilder.AddColumn<Guid>(
                name: "DeletedUserGuid",
                table: "Story",
                type: "uniqueidentifier",
                nullable: true)
                .Annotation("Relational:ColumnOrder", 103);

            migrationBuilder.AddColumn<Guid>(
                name: "UpdatedUserGuid",
                table: "Story",
                type: "uniqueidentifier",
                nullable: true)
                .Annotation("Relational:ColumnOrder", 102);

            migrationBuilder.AlterColumn<string>(
                name: "CreatedUserName",
                table: "GroupUserMember",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100);

            migrationBuilder.AddColumn<Guid>(
                name: "CreatedUserGuid",
                table: "GroupUserMember",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"))
                .Annotation("Relational:ColumnOrder", 101);

            migrationBuilder.AddColumn<Guid>(
                name: "DeletedUserGuid",
                table: "GroupUserMember",
                type: "uniqueidentifier",
                nullable: true)
                .Annotation("Relational:ColumnOrder", 103);

            migrationBuilder.AddColumn<Guid>(
                name: "UpdatedUserGuid",
                table: "GroupUserMember",
                type: "uniqueidentifier",
                nullable: true)
                .Annotation("Relational:ColumnOrder", 102);

            migrationBuilder.AlterColumn<string>(
                name: "CreatedUserName",
                table: "FollowingAuthor",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100);

            migrationBuilder.AddColumn<Guid>(
                name: "CreatedUserGuid",
                table: "FollowingAuthor",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"))
                .Annotation("Relational:ColumnOrder", 101);

            migrationBuilder.AddColumn<Guid>(
                name: "DeletedUserGuid",
                table: "FollowingAuthor",
                type: "uniqueidentifier",
                nullable: true)
                .Annotation("Relational:ColumnOrder", 103);

            migrationBuilder.AddColumn<Guid>(
                name: "UpdatedUserGuid",
                table: "FollowingAuthor",
                type: "uniqueidentifier",
                nullable: true)
                .Annotation("Relational:ColumnOrder", 102);

            migrationBuilder.AlterColumn<string>(
                name: "CreatedUserName",
                table: "Chapter",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100);

            migrationBuilder.AddColumn<Guid>(
                name: "CreatedUserGuid",
                table: "Chapter",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"))
                .Annotation("Relational:ColumnOrder", 101);

            migrationBuilder.AddColumn<Guid>(
                name: "DeletedUserGuid",
                table: "Chapter",
                type: "uniqueidentifier",
                nullable: true)
                .Annotation("Relational:ColumnOrder", 103);

            migrationBuilder.AddColumn<Guid>(
                name: "UpdatedUserGuid",
                table: "Chapter",
                type: "uniqueidentifier",
                nullable: true)
                .Annotation("Relational:ColumnOrder", 102);

            migrationBuilder.AlterColumn<string>(
                name: "CreatedUserName",
                table: "Category",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100);

            migrationBuilder.AddColumn<Guid>(
                name: "CreatedUserGuid",
                table: "Category",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"))
                .Annotation("Relational:ColumnOrder", 101);

            migrationBuilder.AddColumn<Guid>(
                name: "DeletedUserGuid",
                table: "Category",
                type: "uniqueidentifier",
                nullable: true)
                .Annotation("Relational:ColumnOrder", 103);

            migrationBuilder.AddColumn<Guid>(
                name: "UpdatedUserGuid",
                table: "Category",
                type: "uniqueidentifier",
                nullable: true)
                .Annotation("Relational:ColumnOrder", 102);

            migrationBuilder.AlterColumn<string>(
                name: "CreatedUserName",
                table: "BookMarkStory",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100);

            migrationBuilder.AddColumn<Guid>(
                name: "CreatedUserGuid",
                table: "BookMarkStory",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"))
                .Annotation("Relational:ColumnOrder", 101);

            migrationBuilder.AddColumn<Guid>(
                name: "DeletedUserGuid",
                table: "BookMarkStory",
                type: "uniqueidentifier",
                nullable: true)
                .Annotation("Relational:ColumnOrder", 103);

            migrationBuilder.AddColumn<Guid>(
                name: "UpdatedUserGuid",
                table: "BookMarkStory",
                type: "uniqueidentifier",
                nullable: true)
                .Annotation("Relational:ColumnOrder", 102);

            migrationBuilder.UpdateData(
                table: "AppRole",
                keyColumn: "Id",
                keyValue: new Guid("5ef7d163-8249-445c-8895-4eb97329af7e"),
                column: "ConcurrencyStamp",
                value: "bf0dcae5-2f8e-43ac-a0aa-dfdba245f63e");

            migrationBuilder.UpdateData(
                table: "AppRole",
                keyColumn: "Id",
                keyValue: new Guid("72377426-b057-46ca-98ff-1ca9d33ea0c1"),
                column: "ConcurrencyStamp",
                value: "1b58680f-4914-4c6c-9a85-b13e4a2113a8");

            migrationBuilder.UpdateData(
                table: "AppUsers",
                keyColumn: "Id",
                keyValue: new Guid("05075755-688d-4987-9c1e-f3bef1746d52"),
                column: "ConcurrencyStamp",
                value: "ddd97ebf-2f48-4be4-bb3e-d537eb08bb18");

            migrationBuilder.UpdateData(
                table: "AppUsers",
                keyColumn: "Id",
                keyValue: new Guid("e0223a03-2945-49db-976e-736433465b7f"),
                column: "ConcurrencyStamp",
                value: "a60995fc-5818-4225-9cf4-5ae066624ef5");

            migrationBuilder.UpdateData(
                table: "Category",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedUserGuid",
                value: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.UpdateData(
                table: "Category",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedUserGuid",
                value: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.UpdateData(
                table: "Chapter",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedUserGuid",
                value: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.UpdateData(
                table: "Chapter",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedUserGuid",
                value: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.UpdateData(
                table: "GroupUserMember",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedUserGuid",
                value: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.UpdateData(
                table: "GroupUserMember",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedUserGuid",
                value: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.UpdateData(
                table: "Story",
                keyColumn: "Guid",
                keyValue: new Guid("32048316-149b-4838-bd27-1b5da11bd4fd"),
                column: "CreatedUserGuid",
                value: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.UpdateData(
                table: "Story",
                keyColumn: "Guid",
                keyValue: new Guid("c5c9ce29-28b5-4121-a1ce-7d03d8c22839"),
                column: "CreatedUserGuid",
                value: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.UpdateData(
                table: "Tag",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedUserGuid",
                value: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.UpdateData(
                table: "Tag",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedUserGuid",
                value: new Guid("00000000-0000-0000-0000-000000000000"));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreatedUserGuid",
                table: "TagInStory");

            migrationBuilder.DropColumn(
                name: "DeletedUserGuid",
                table: "TagInStory");

            migrationBuilder.DropColumn(
                name: "UpdatedUserGuid",
                table: "TagInStory");

            migrationBuilder.DropColumn(
                name: "CreatedUserGuid",
                table: "Tag");

            migrationBuilder.DropColumn(
                name: "DeletedUserGuid",
                table: "Tag");

            migrationBuilder.DropColumn(
                name: "UpdatedUserGuid",
                table: "Tag");

            migrationBuilder.DropColumn(
                name: "CreatedUserGuid",
                table: "StoryReview");

            migrationBuilder.DropColumn(
                name: "DeletedUserGuid",
                table: "StoryReview");

            migrationBuilder.DropColumn(
                name: "UpdatedUserGuid",
                table: "StoryReview");

            migrationBuilder.DropColumn(
                name: "CreatedUserGuid",
                table: "StoryPublish");

            migrationBuilder.DropColumn(
                name: "DeletedUserGuid",
                table: "StoryPublish");

            migrationBuilder.DropColumn(
                name: "UpdatedUserGuid",
                table: "StoryPublish");

            migrationBuilder.DropColumn(
                name: "CreatedUserGuid",
                table: "StoryNotifications");

            migrationBuilder.DropColumn(
                name: "DeletedUserGuid",
                table: "StoryNotifications");

            migrationBuilder.DropColumn(
                name: "UpdatedUserGuid",
                table: "StoryNotifications");

            migrationBuilder.DropColumn(
                name: "CreatedUserGuid",
                table: "Story");

            migrationBuilder.DropColumn(
                name: "DeletedUserGuid",
                table: "Story");

            migrationBuilder.DropColumn(
                name: "UpdatedUserGuid",
                table: "Story");

            migrationBuilder.DropColumn(
                name: "CreatedUserGuid",
                table: "GroupUserMember");

            migrationBuilder.DropColumn(
                name: "DeletedUserGuid",
                table: "GroupUserMember");

            migrationBuilder.DropColumn(
                name: "UpdatedUserGuid",
                table: "GroupUserMember");

            migrationBuilder.DropColumn(
                name: "CreatedUserGuid",
                table: "FollowingAuthor");

            migrationBuilder.DropColumn(
                name: "DeletedUserGuid",
                table: "FollowingAuthor");

            migrationBuilder.DropColumn(
                name: "UpdatedUserGuid",
                table: "FollowingAuthor");

            migrationBuilder.DropColumn(
                name: "CreatedUserGuid",
                table: "Chapter");

            migrationBuilder.DropColumn(
                name: "DeletedUserGuid",
                table: "Chapter");

            migrationBuilder.DropColumn(
                name: "UpdatedUserGuid",
                table: "Chapter");

            migrationBuilder.DropColumn(
                name: "CreatedUserGuid",
                table: "Category");

            migrationBuilder.DropColumn(
                name: "DeletedUserGuid",
                table: "Category");

            migrationBuilder.DropColumn(
                name: "UpdatedUserGuid",
                table: "Category");

            migrationBuilder.DropColumn(
                name: "CreatedUserGuid",
                table: "BookMarkStory");

            migrationBuilder.DropColumn(
                name: "DeletedUserGuid",
                table: "BookMarkStory");

            migrationBuilder.DropColumn(
                name: "UpdatedUserGuid",
                table: "BookMarkStory");

            migrationBuilder.AlterColumn<string>(
                name: "CreatedUserName",
                table: "TagInStory",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100,
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "CreatedUserId",
                table: "TagInStory",
                type: "int",
                nullable: false,
                defaultValue: 0)
                .Annotation("Relational:ColumnOrder", 101);

            migrationBuilder.AddColumn<int>(
                name: "DeletedUserId",
                table: "TagInStory",
                type: "int",
                nullable: true)
                .Annotation("Relational:ColumnOrder", 103);

            migrationBuilder.AddColumn<int>(
                name: "UpdatedUserId",
                table: "TagInStory",
                type: "int",
                nullable: true)
                .Annotation("Relational:ColumnOrder", 102);

            migrationBuilder.AlterColumn<string>(
                name: "CreatedUserName",
                table: "Tag",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100,
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "CreatedUserId",
                table: "Tag",
                type: "int",
                nullable: false,
                defaultValue: 0)
                .Annotation("Relational:ColumnOrder", 101);

            migrationBuilder.AddColumn<int>(
                name: "DeletedUserId",
                table: "Tag",
                type: "int",
                nullable: true)
                .Annotation("Relational:ColumnOrder", 103);

            migrationBuilder.AddColumn<int>(
                name: "UpdatedUserId",
                table: "Tag",
                type: "int",
                nullable: true)
                .Annotation("Relational:ColumnOrder", 102);

            migrationBuilder.AlterColumn<string>(
                name: "CreatedUserName",
                table: "StoryReview",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100,
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "CreatedUserId",
                table: "StoryReview",
                type: "int",
                nullable: false,
                defaultValue: 0)
                .Annotation("Relational:ColumnOrder", 101);

            migrationBuilder.AddColumn<int>(
                name: "DeletedUserId",
                table: "StoryReview",
                type: "int",
                nullable: true)
                .Annotation("Relational:ColumnOrder", 103);

            migrationBuilder.AddColumn<int>(
                name: "UpdatedUserId",
                table: "StoryReview",
                type: "int",
                nullable: true)
                .Annotation("Relational:ColumnOrder", 102);

            migrationBuilder.AlterColumn<string>(
                name: "CreatedUserName",
                table: "StoryPublish",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100,
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "CreatedUserId",
                table: "StoryPublish",
                type: "int",
                nullable: false,
                defaultValue: 0)
                .Annotation("Relational:ColumnOrder", 101);

            migrationBuilder.AddColumn<int>(
                name: "DeletedUserId",
                table: "StoryPublish",
                type: "int",
                nullable: true)
                .Annotation("Relational:ColumnOrder", 103);

            migrationBuilder.AddColumn<int>(
                name: "UpdatedUserId",
                table: "StoryPublish",
                type: "int",
                nullable: true)
                .Annotation("Relational:ColumnOrder", 102);

            migrationBuilder.AlterColumn<string>(
                name: "CreatedUserName",
                table: "StoryNotifications",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100,
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "CreatedUserId",
                table: "StoryNotifications",
                type: "int",
                nullable: false,
                defaultValue: 0)
                .Annotation("Relational:ColumnOrder", 101);

            migrationBuilder.AddColumn<int>(
                name: "DeletedUserId",
                table: "StoryNotifications",
                type: "int",
                nullable: true)
                .Annotation("Relational:ColumnOrder", 103);

            migrationBuilder.AddColumn<int>(
                name: "UpdatedUserId",
                table: "StoryNotifications",
                type: "int",
                nullable: true)
                .Annotation("Relational:ColumnOrder", 102);

            migrationBuilder.AlterColumn<string>(
                name: "CreatedUserName",
                table: "Story",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100,
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "CreatedUserId",
                table: "Story",
                type: "int",
                nullable: false,
                defaultValue: 0)
                .Annotation("Relational:ColumnOrder", 101);

            migrationBuilder.AddColumn<int>(
                name: "DeletedUserId",
                table: "Story",
                type: "int",
                nullable: true)
                .Annotation("Relational:ColumnOrder", 103);

            migrationBuilder.AddColumn<int>(
                name: "UpdatedUserId",
                table: "Story",
                type: "int",
                nullable: true)
                .Annotation("Relational:ColumnOrder", 102);

            migrationBuilder.AlterColumn<string>(
                name: "CreatedUserName",
                table: "GroupUserMember",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100,
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "CreatedUserId",
                table: "GroupUserMember",
                type: "int",
                nullable: false,
                defaultValue: 0)
                .Annotation("Relational:ColumnOrder", 101);

            migrationBuilder.AddColumn<int>(
                name: "DeletedUserId",
                table: "GroupUserMember",
                type: "int",
                nullable: true)
                .Annotation("Relational:ColumnOrder", 103);

            migrationBuilder.AddColumn<int>(
                name: "UpdatedUserId",
                table: "GroupUserMember",
                type: "int",
                nullable: true)
                .Annotation("Relational:ColumnOrder", 102);

            migrationBuilder.AlterColumn<string>(
                name: "CreatedUserName",
                table: "FollowingAuthor",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100,
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "CreatedUserId",
                table: "FollowingAuthor",
                type: "int",
                nullable: false,
                defaultValue: 0)
                .Annotation("Relational:ColumnOrder", 101);

            migrationBuilder.AddColumn<int>(
                name: "DeletedUserId",
                table: "FollowingAuthor",
                type: "int",
                nullable: true)
                .Annotation("Relational:ColumnOrder", 103);

            migrationBuilder.AddColumn<int>(
                name: "UpdatedUserId",
                table: "FollowingAuthor",
                type: "int",
                nullable: true)
                .Annotation("Relational:ColumnOrder", 102);

            migrationBuilder.AlterColumn<string>(
                name: "CreatedUserName",
                table: "Chapter",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100,
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "CreatedUserId",
                table: "Chapter",
                type: "int",
                nullable: false,
                defaultValue: 0)
                .Annotation("Relational:ColumnOrder", 101);

            migrationBuilder.AddColumn<int>(
                name: "DeletedUserId",
                table: "Chapter",
                type: "int",
                nullable: true)
                .Annotation("Relational:ColumnOrder", 103);

            migrationBuilder.AddColumn<int>(
                name: "UpdatedUserId",
                table: "Chapter",
                type: "int",
                nullable: true)
                .Annotation("Relational:ColumnOrder", 102);

            migrationBuilder.AlterColumn<string>(
                name: "CreatedUserName",
                table: "Category",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100,
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "CreatedUserId",
                table: "Category",
                type: "int",
                nullable: false,
                defaultValue: 0)
                .Annotation("Relational:ColumnOrder", 101);

            migrationBuilder.AddColumn<int>(
                name: "DeletedUserId",
                table: "Category",
                type: "int",
                nullable: true)
                .Annotation("Relational:ColumnOrder", 103);

            migrationBuilder.AddColumn<int>(
                name: "UpdatedUserId",
                table: "Category",
                type: "int",
                nullable: true)
                .Annotation("Relational:ColumnOrder", 102);

            migrationBuilder.AlterColumn<string>(
                name: "CreatedUserName",
                table: "BookMarkStory",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100,
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "CreatedUserId",
                table: "BookMarkStory",
                type: "int",
                nullable: false,
                defaultValue: 0)
                .Annotation("Relational:ColumnOrder", 101);

            migrationBuilder.AddColumn<int>(
                name: "DeletedUserId",
                table: "BookMarkStory",
                type: "int",
                nullable: true)
                .Annotation("Relational:ColumnOrder", 103);

            migrationBuilder.AddColumn<int>(
                name: "UpdatedUserId",
                table: "BookMarkStory",
                type: "int",
                nullable: true)
                .Annotation("Relational:ColumnOrder", 102);

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
                column: "ConcurrencyStamp",
                value: "ab51b3b2-9738-439e-b6c0-48f86423912b");

            migrationBuilder.UpdateData(
                table: "AppUsers",
                keyColumn: "Id",
                keyValue: new Guid("e0223a03-2945-49db-976e-736433465b7f"),
                column: "ConcurrencyStamp",
                value: "b59f9499-c066-4066-a5c7-2a28dbe79ef9");

            migrationBuilder.UpdateData(
                table: "Category",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedUserId",
                value: 1);

            migrationBuilder.UpdateData(
                table: "Category",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedUserId",
                value: 1);

            migrationBuilder.UpdateData(
                table: "Chapter",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedUserId",
                value: 1);

            migrationBuilder.UpdateData(
                table: "Chapter",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedUserId",
                value: 1);

            migrationBuilder.UpdateData(
                table: "GroupUserMember",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedUserId",
                value: 1);

            migrationBuilder.UpdateData(
                table: "GroupUserMember",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedUserId",
                value: 1);

            migrationBuilder.UpdateData(
                table: "Story",
                keyColumn: "Guid",
                keyValue: new Guid("32048316-149b-4838-bd27-1b5da11bd4fd"),
                column: "CreatedUserId",
                value: 1);

            migrationBuilder.UpdateData(
                table: "Story",
                keyColumn: "Guid",
                keyValue: new Guid("c5c9ce29-28b5-4121-a1ce-7d03d8c22839"),
                column: "CreatedUserId",
                value: 1);

            migrationBuilder.UpdateData(
                table: "Tag",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedUserId",
                value: 1);

            migrationBuilder.UpdateData(
                table: "Tag",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedUserId",
                value: 1);
        }
    }
}
