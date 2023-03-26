using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MuonRoiSocialNetwork.Migrations
{
    public partial class RenewTableUserLogin : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_GroupUserMember_AppRole_AppRoleKey",
                table: "GroupUserMember");

            migrationBuilder.DropForeignKey(
                name: "FK_GroupUserMember_AppUsers_AppUserKey",
                table: "GroupUserMember");

            migrationBuilder.DropTable(
                name: "RoleClaim");

            migrationBuilder.DropTable(
                name: "UserClaim");

            migrationBuilder.DropTable(
                name: "UserRole");

            migrationBuilder.DropTable(
                name: "UserToken");

            migrationBuilder.DropPrimaryKey(
                name: "PK_UserLogin",
                table: "UserLogin");

            migrationBuilder.DropPrimaryKey(
                name: "PK_GroupUserMember",
                table: "GroupUserMember");

            migrationBuilder.DropIndex(
                name: "IX_GroupUserMember_AppRoleKey",
                table: "GroupUserMember");

            migrationBuilder.DropColumn(
                name: "LoginProvider",
                table: "UserLogin");

            migrationBuilder.DropColumn(
                name: "ProviderDisplayName",
                table: "UserLogin");

            migrationBuilder.DropColumn(
                name: "AppUserKey",
                table: "GroupUserMember");

            migrationBuilder.RenameColumn(
                name: "ProviderKey",
                table: "UserLogin",
                newName: "KeySalt");

            migrationBuilder.AddColumn<string>(
                name: "RefreshToken",
                table: "UserLogin",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<double>(
                name: "CreateDateTS",
                table: "UserLogin",
                type: "float",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "RefreshTokenExpiryTimeTS",
                table: "UserLogin",
                type: "float",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "GroupName",
                table: "GroupUserMember",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<int>(
                name: "GroupId",
                table: "AppRole",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "RoleName",
                table: "AppRole",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_UserLogin",
                table: "UserLogin",
                columns: new[] { "UserId", "RefreshToken" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_GroupUserMember",
                table: "GroupUserMember",
                column: "Id");

            migrationBuilder.UpdateData(
                table: "AppRole",
                keyColumn: "Id",
                keyValue: new Guid("5ef7d163-8249-445c-8895-4eb97329af7e"),
                columns: new[] { "ConcurrencyStamp", "GroupId" },
                values: new object[] { "7119f229-64af-4ed9-a20a-d4bdabe30558", 2 });

            migrationBuilder.UpdateData(
                table: "AppRole",
                keyColumn: "Id",
                keyValue: new Guid("72377426-b057-46ca-98ff-1ca9d33ea0c1"),
                columns: new[] { "ConcurrencyStamp", "GroupId" },
                values: new object[] { "15287b99-220b-433d-99de-7d0e9dc53c21", 1 });

            migrationBuilder.CreateIndex(
                name: "IX_UserLogin_UserId",
                table: "UserLogin",
                column: "UserId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_AppUsers_GroupId",
                table: "AppUsers",
                column: "GroupId");

            migrationBuilder.CreateIndex(
                name: "IX_AppRole_GroupId",
                table: "AppRole",
                column: "GroupId");

            migrationBuilder.AddForeignKey(
                name: "FK_AppRole_GroupUserMember_GroupId",
                table: "AppRole",
                column: "GroupId",
                principalTable: "GroupUserMember",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AppUsers_GroupUserMember_GroupId",
                table: "AppUsers",
                column: "GroupId",
                principalTable: "GroupUserMember",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_UserLogin_AppUsers_UserId",
                table: "UserLogin",
                column: "UserId",
                principalTable: "AppUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AppRole_GroupUserMember_GroupId",
                table: "AppRole");

            migrationBuilder.DropForeignKey(
                name: "FK_AppUsers_GroupUserMember_GroupId",
                table: "AppUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_UserLogin_AppUsers_UserId",
                table: "UserLogin");

            migrationBuilder.DropPrimaryKey(
                name: "PK_UserLogin",
                table: "UserLogin");

            migrationBuilder.DropIndex(
                name: "IX_UserLogin_UserId",
                table: "UserLogin");

            migrationBuilder.DropPrimaryKey(
                name: "PK_GroupUserMember",
                table: "GroupUserMember");

            migrationBuilder.DropIndex(
                name: "IX_AppUsers_GroupId",
                table: "AppUsers");

            migrationBuilder.DropIndex(
                name: "IX_AppRole_GroupId",
                table: "AppRole");

            migrationBuilder.DeleteData(
                table: "GroupUserMember",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "GroupUserMember",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DropColumn(
                name: "RefreshToken",
                table: "UserLogin");

            migrationBuilder.DropColumn(
                name: "CreateDateTS",
                table: "UserLogin");

            migrationBuilder.DropColumn(
                name: "RefreshTokenExpiryTimeTS",
                table: "UserLogin");

            migrationBuilder.DropColumn(
                name: "GroupId",
                table: "AppRole");

            migrationBuilder.DropColumn(
                name: "RoleName",
                table: "AppRole");

            migrationBuilder.RenameColumn(
                name: "KeySalt",
                table: "UserLogin",
                newName: "ProviderKey");

            migrationBuilder.AddColumn<string>(
                name: "LoginProvider",
                table: "UserLogin",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ProviderDisplayName",
                table: "UserLogin",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "GroupName",
                table: "GroupUserMember",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "AppUserKey",
                table: "GroupUserMember",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddPrimaryKey(
                name: "PK_UserLogin",
                table: "UserLogin",
                column: "UserId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_GroupUserMember",
                table: "GroupUserMember",
                columns: new[] { "AppUserKey", "AppRoleKey" });

            migrationBuilder.CreateTable(
                name: "RoleClaim",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RoleId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RoleClaim", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "UserClaim",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserClaim", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "UserRole",
                columns: table => new
                {
                    RoleId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserRole", x => new { x.RoleId, x.UserId });
                });

            migrationBuilder.CreateTable(
                name: "UserToken",
                columns: table => new
                {
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    LoginProvider = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserToken", x => x.UserId);
                });

            migrationBuilder.UpdateData(
                table: "AppRole",
                keyColumn: "Id",
                keyValue: new Guid("5ef7d163-8249-445c-8895-4eb97329af7e"),
                column: "ConcurrencyStamp",
                value: "1cd690c9-5bb6-46c8-9832-c61c43d40acc");

            migrationBuilder.UpdateData(
                table: "AppRole",
                keyColumn: "Id",
                keyValue: new Guid("72377426-b057-46ca-98ff-1ca9d33ea0c1"),
                column: "ConcurrencyStamp",
                value: "b35c3eeb-998c-439f-9f47-d804fe32534b");

            migrationBuilder.InsertData(
                table: "GroupUserMember",
                columns: new[] { "AppRoleKey", "AppUserKey", "CreatedDateTS", "CreatedUserId", "CreatedUserName", "DeletedDateTS", "DeletedUserId", "DeletedUserName", "GroupName", "Guid", "Id", "IsDeleted", "UpdatedDateTS", "UpdatedUserId", "UpdatedUserName" },
                values: new object[,]
                {
                    { new Guid("5ef7d163-8249-445c-8895-4eb97329af7e"), new Guid("05075755-688d-4987-9c1e-f3bef1746d52"), 1672531200.0, 1, "muonroi", null, null, null, "Default User", new Guid("00000000-0000-0000-0000-000000000000"), 2, false, null, null, null },
                    { new Guid("72377426-b057-46ca-98ff-1ca9d33ea0c1"), new Guid("e0223a03-2945-49db-976e-736433465b7f"), 1672531200.0, 1, "muonroi", null, null, null, "Administratior", new Guid("00000000-0000-0000-0000-000000000000"), 1, false, null, null, null }
                });

            migrationBuilder.CreateIndex(
                name: "IX_GroupUserMember_AppRoleKey",
                table: "GroupUserMember",
                column: "AppRoleKey");

            migrationBuilder.AddForeignKey(
                name: "FK_GroupUserMember_AppRole_AppRoleKey",
                table: "GroupUserMember",
                column: "AppRoleKey",
                principalTable: "AppRole",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_GroupUserMember_AppUsers_AppUserKey",
                table: "GroupUserMember",
                column: "AppUserKey",
                principalTable: "AppUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
