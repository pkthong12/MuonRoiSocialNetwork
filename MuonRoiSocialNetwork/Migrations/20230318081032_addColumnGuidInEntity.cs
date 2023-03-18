using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MuonRoiSocialNetwork.Migrations
{
    public partial class addColumnGuidInEntity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "Guid",
                table: "TagInStory",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"))
                .Annotation("Relational:ColumnOrder", 1);

            migrationBuilder.AddColumn<Guid>(
                name: "Guid",
                table: "Tag",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"))
                .Annotation("Relational:ColumnOrder", 1);

            migrationBuilder.AddColumn<Guid>(
                name: "Guid",
                table: "StoryReview",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"))
                .Annotation("Relational:ColumnOrder", 1);

            migrationBuilder.AddColumn<Guid>(
                name: "Guid",
                table: "StoryPublish",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"))
                .Annotation("Relational:ColumnOrder", 1);

            migrationBuilder.AddColumn<Guid>(
                name: "Guid",
                table: "StoryNotifications",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"))
                .Annotation("Relational:ColumnOrder", 1);

            migrationBuilder.AlterColumn<Guid>(
                name: "Guid",
                table: "Story",
                type: "uniqueidentifier",
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier")
                .Annotation("Relational:ColumnOrder", 1);

            migrationBuilder.AddColumn<Guid>(
                name: "Guid",
                table: "GroupUserMember",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"))
                .Annotation("Relational:ColumnOrder", 1);

            migrationBuilder.AddColumn<Guid>(
                name: "Guid",
                table: "FollowingAuthor",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"))
                .Annotation("Relational:ColumnOrder", 1);

            migrationBuilder.AddColumn<Guid>(
                name: "Guid",
                table: "Chapter",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"))
                .Annotation("Relational:ColumnOrder", 1);

            migrationBuilder.AddColumn<Guid>(
                name: "Guid",
                table: "Category",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"))
                .Annotation("Relational:ColumnOrder", 1);

            migrationBuilder.AddColumn<Guid>(
                name: "Guid",
                table: "BookMarkStory",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"))
                .Annotation("Relational:ColumnOrder", 1);

            migrationBuilder.AlterColumn<DateTime>(
                name: "LastLogin",
                table: "AppUsers",
                type: "datetime2",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.UpdateData(
                table: "AppRole",
                keyColumn: "Id",
                keyValue: new Guid("5ef7d163-8249-445c-8895-4eb97329af7e"),
                column: "ConcurrencyStamp",
                value: "5f54e63e-d347-4969-bfb5-1d13d081e3c2");

            migrationBuilder.UpdateData(
                table: "AppRole",
                keyColumn: "Id",
                keyValue: new Guid("72377426-b057-46ca-98ff-1ca9d33ea0c1"),
                column: "ConcurrencyStamp",
                value: "1eee7116-1cfd-4696-a1e9-35d02132c5e7");

            migrationBuilder.UpdateData(
                table: "AppUsers",
                keyColumn: "Id",
                keyValue: new Guid("05075755-688d-4987-9c1e-f3bef1746d52"),
                columns: new[] { "ConcurrencyStamp", "LastLogin", "PasswordHash" },
                values: new object[] { "68b5dbbd-e346-4c66-a5a1-b0b6a675a09a", null, "AQAAAAEAACcQAAAAEG6UtDOLve1IJN8O82h8VxMUrVaZoK7WYq2DQbfH6d8s/rM5FSDNNPqIlyEIx8OtUQ==" });

            migrationBuilder.UpdateData(
                table: "AppUsers",
                keyColumn: "Id",
                keyValue: new Guid("e0223a03-2945-49db-976e-736433465b7f"),
                columns: new[] { "ConcurrencyStamp", "LastLogin", "PasswordHash" },
                values: new object[] { "dda07fea-6aec-4c2c-b541-30b35efdf6bd", null, "AQAAAAEAACcQAAAAEJMzq19sF0Xy1sDmhwwOvoswG/spiea/X3j/NY4joXABq3FxElUKl8wsTXN2uuGQGA==" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Guid",
                table: "TagInStory");

            migrationBuilder.DropColumn(
                name: "Guid",
                table: "Tag");

            migrationBuilder.DropColumn(
                name: "Guid",
                table: "StoryReview");

            migrationBuilder.DropColumn(
                name: "Guid",
                table: "StoryPublish");

            migrationBuilder.DropColumn(
                name: "Guid",
                table: "StoryNotifications");

            migrationBuilder.DropColumn(
                name: "Guid",
                table: "GroupUserMember");

            migrationBuilder.DropColumn(
                name: "Guid",
                table: "FollowingAuthor");

            migrationBuilder.DropColumn(
                name: "Guid",
                table: "Chapter");

            migrationBuilder.DropColumn(
                name: "Guid",
                table: "Category");

            migrationBuilder.DropColumn(
                name: "Guid",
                table: "BookMarkStory");

            migrationBuilder.AlterColumn<Guid>(
                name: "Guid",
                table: "Story",
                type: "uniqueidentifier",
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier")
                .OldAnnotation("Relational:ColumnOrder", 1);

            migrationBuilder.AlterColumn<DateTime>(
                name: "LastLogin",
                table: "AppUsers",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true);

            migrationBuilder.UpdateData(
                table: "AppRole",
                keyColumn: "Id",
                keyValue: new Guid("5ef7d163-8249-445c-8895-4eb97329af7e"),
                column: "ConcurrencyStamp",
                value: "ccacfde4-c93e-43a4-8d69-10de3722ab71");

            migrationBuilder.UpdateData(
                table: "AppRole",
                keyColumn: "Id",
                keyValue: new Guid("72377426-b057-46ca-98ff-1ca9d33ea0c1"),
                column: "ConcurrencyStamp",
                value: "d35efd8b-63bf-44e4-960a-ed2f06c233f6");

            migrationBuilder.UpdateData(
                table: "AppUsers",
                keyColumn: "Id",
                keyValue: new Guid("05075755-688d-4987-9c1e-f3bef1746d52"),
                columns: new[] { "ConcurrencyStamp", "LastLogin", "PasswordHash" },
                values: new object[] { "c4c555a6-9fdd-420e-b694-80d74880cc6a", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "AQAAAAEAACcQAAAAEMPxO0N/pIH94zJC2xdAL3b7X8yzqWmJDJ/oU8g8o0FTyj/PJ0Vyjr5c4LHUmdMpLQ==" });

            migrationBuilder.UpdateData(
                table: "AppUsers",
                keyColumn: "Id",
                keyValue: new Guid("e0223a03-2945-49db-976e-736433465b7f"),
                columns: new[] { "ConcurrencyStamp", "LastLogin", "PasswordHash" },
                values: new object[] { "aa9ca9d1-5176-46f2-93c0-7a5a5663025f", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "AQAAAAEAACcQAAAAEFULFj3mmIwfpirurzZyN/ZHd0RAcTmk6WaGoZnF5ymwLVUSOiLrtT+5PuzAdRdFhA==" });
        }
    }
}
