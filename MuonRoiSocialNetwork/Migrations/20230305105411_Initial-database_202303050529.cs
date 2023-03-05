using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MuonRoiSocialNetwork.Migrations
{
    public partial class Initialdatabase_202303050529 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUsers",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Surname = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Address = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
                    BirthDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Gender = table.Column<int>(type: "int", nullable: true),
                    LastLogin = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Avatar = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
                    Status = table.Column<int>(type: "int", nullable: false),
                    Note = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LockReason = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    GroupId = table.Column<int>(type: "int", nullable: true),
                    UserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    Email = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SecurityStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    TwoFactorEnabled = table.Column<bool>(type: "bit", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    LockoutEnabled = table.Column<bool>(type: "bit", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUsers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Category",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreatedUserId = table.Column<int>(type: "int", nullable: false),
                    UpdatedUserId = table.Column<int>(type: "int", nullable: true),
                    DeletedUserId = table.Column<int>(type: "int", nullable: true),
                    CreatedUserName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    UpdatedUserName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    DeletedUserName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    CreatedDateTS = table.Column<double>(type: "float", nullable: false),
                    UpdatedDateTS = table.Column<double>(type: "float", nullable: true),
                    DeletedDateTS = table.Column<double>(type: "float", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    NameCategory = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    Guid = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SiteId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Category", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Tag",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreatedUserId = table.Column<int>(type: "int", nullable: false),
                    UpdatedUserId = table.Column<int>(type: "int", nullable: true),
                    DeletedUserId = table.Column<int>(type: "int", nullable: true),
                    CreatedUserName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    UpdatedUserName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    DeletedUserName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    CreatedDateTS = table.Column<double>(type: "float", nullable: false),
                    UpdatedDateTS = table.Column<double>(type: "float", nullable: true),
                    DeletedDateTS = table.Column<double>(type: "float", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    TagName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Details = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    Guid = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SiteId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tag", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserLogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderKey = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderDisplayName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserLogins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserRoles",
                columns: table => new
                {
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    RoleId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserTokens",
                columns: table => new
                {
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "BookMarkStory",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreatedUserId = table.Column<int>(type: "int", nullable: false),
                    UpdatedUserId = table.Column<int>(type: "int", nullable: true),
                    DeletedUserId = table.Column<int>(type: "int", nullable: true),
                    CreatedUserName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    UpdatedUserName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    DeletedUserName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    CreatedDateTS = table.Column<double>(type: "float", nullable: false),
                    UpdatedDateTS = table.Column<double>(type: "float", nullable: true),
                    DeletedDateTS = table.Column<double>(type: "float", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    StoryGuid = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserGuid = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    BookmarkDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Guid = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SiteId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BookMarkStory", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BookMarkStory_AspNetUsers_UserGuid",
                        column: x => x.UserGuid,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "FollowingAuthor",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreatedUserId = table.Column<int>(type: "int", nullable: false),
                    UpdatedUserId = table.Column<int>(type: "int", nullable: true),
                    DeletedUserId = table.Column<int>(type: "int", nullable: true),
                    CreatedUserName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    UpdatedUserName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    DeletedUserName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    CreatedDateTS = table.Column<double>(type: "float", nullable: false),
                    UpdatedDateTS = table.Column<double>(type: "float", nullable: true),
                    DeletedDateTS = table.Column<double>(type: "float", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    UserGuid = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    StoryGuid = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Guid = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SiteId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FollowingAuthor", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FollowingAuthor_AspNetUsers_UserGuid",
                        column: x => x.UserGuid,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "GroupUserMember",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreatedUserId = table.Column<int>(type: "int", nullable: false),
                    UpdatedUserId = table.Column<int>(type: "int", nullable: true),
                    DeletedUserId = table.Column<int>(type: "int", nullable: true),
                    CreatedUserName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    UpdatedUserName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    DeletedUserName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    CreatedDateTS = table.Column<double>(type: "float", nullable: false),
                    UpdatedDateTS = table.Column<double>(type: "float", nullable: true),
                    DeletedDateTS = table.Column<double>(type: "float", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    AppUserKey = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    AppRoleKey = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Manage = table.Column<int>(type: "int", nullable: false),
                    Staff = table.Column<int>(type: "int", nullable: false),
                    Viewer = table.Column<int>(type: "int", nullable: false),
                    Guest = table.Column<int>(type: "int", nullable: false),
                    Guid = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SiteId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GroupUserMember", x => new { x.AppUserKey, x.AppRoleKey });
                    table.ForeignKey(
                        name: "FK_GroupUserMember_AspNetRoles_AppRoleKey",
                        column: x => x.AppRoleKey,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_GroupUserMember_AspNetUsers_AppUserKey",
                        column: x => x.AppUserKey,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "StoryPublish",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreatedUserId = table.Column<int>(type: "int", nullable: false),
                    UpdatedUserId = table.Column<int>(type: "int", nullable: true),
                    DeletedUserId = table.Column<int>(type: "int", nullable: true),
                    CreatedUserName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    UpdatedUserName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    DeletedUserName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    CreatedDateTS = table.Column<double>(type: "float", nullable: false),
                    UpdatedDateTS = table.Column<double>(type: "float", nullable: true),
                    DeletedDateTS = table.Column<double>(type: "float", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    StoryGuid = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserGuid = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Guid = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SiteId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StoryPublish", x => x.Id);
                    table.ForeignKey(
                        name: "FK_StoryPublish_AspNetUsers_UserGuid",
                        column: x => x.UserGuid,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "StoryReview",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreatedUserId = table.Column<int>(type: "int", nullable: false),
                    UpdatedUserId = table.Column<int>(type: "int", nullable: true),
                    DeletedUserId = table.Column<int>(type: "int", nullable: true),
                    CreatedUserName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    UpdatedUserName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    DeletedUserName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    CreatedDateTS = table.Column<double>(type: "float", nullable: false),
                    UpdatedDateTS = table.Column<double>(type: "float", nullable: true),
                    DeletedDateTS = table.Column<double>(type: "float", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    StoryGuid = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserGuid = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Rating = table.Column<double>(type: "float", nullable: false),
                    Content = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Guid = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SiteId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StoryReview", x => x.Id);
                    table.ForeignKey(
                        name: "FK_StoryReview_AspNetUsers_UserGuid",
                        column: x => x.UserGuid,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Story",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreatedUserId = table.Column<int>(type: "int", nullable: false),
                    UpdatedUserId = table.Column<int>(type: "int", nullable: true),
                    DeletedUserId = table.Column<int>(type: "int", nullable: true),
                    CreatedUserName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    UpdatedUserName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    DeletedUserName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    CreatedDateTS = table.Column<double>(type: "float", nullable: false),
                    UpdatedDateTS = table.Column<double>(type: "float", nullable: true),
                    DeletedDateTS = table.Column<double>(type: "float", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    Guid = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Story_Title = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Story_Synopsis = table.Column<string>(type: "nvarchar(max)", maxLength: 5000, nullable: false),
                    Img_Url = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: false),
                    IsShow = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    TotalView = table.Column<int>(type: "int", nullable: false),
                    TotalFavorite = table.Column<int>(type: "int", nullable: false),
                    Rating = table.Column<double>(type: "float", nullable: false),
                    Slug = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    CategoryId = table.Column<int>(type: "int", nullable: false),
                    SiteId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Story", x => x.Guid);
                    table.ForeignKey(
                        name: "FK_Story_Category_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Category",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Chapter",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreatedUserId = table.Column<int>(type: "int", nullable: false),
                    UpdatedUserId = table.Column<int>(type: "int", nullable: true),
                    DeletedUserId = table.Column<int>(type: "int", nullable: true),
                    CreatedUserName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    UpdatedUserName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    DeletedUserName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    CreatedDateTS = table.Column<double>(type: "float", nullable: false),
                    UpdatedDateTS = table.Column<double>(type: "float", nullable: true),
                    DeletedDateTS = table.Column<double>(type: "float", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    ChapterTitle = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Body = table.Column<string>(type: "nvarchar(max)", maxLength: 100000, nullable: false),
                    NumberOfChapter = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    NumberCharacter = table.Column<int>(type: "int", nullable: false),
                    StoryGuid = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Slug = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Guid = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SiteId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Chapter", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Chapter_Story_StoryGuid",
                        column: x => x.StoryGuid,
                        principalTable: "Story",
                        principalColumn: "Guid",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "StoryNotifications",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreatedUserId = table.Column<int>(type: "int", nullable: false),
                    UpdatedUserId = table.Column<int>(type: "int", nullable: true),
                    DeletedUserId = table.Column<int>(type: "int", nullable: true),
                    CreatedUserName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    UpdatedUserName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    DeletedUserName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    CreatedDateTS = table.Column<double>(type: "float", nullable: false),
                    UpdatedDateTS = table.Column<double>(type: "float", nullable: true),
                    DeletedDateTS = table.Column<double>(type: "float", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    UserGuid = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    StoryGuid = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    NotifiUrl = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: false),
                    Title = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Message = table.Column<string>(type: "nvarchar(350)", maxLength: 350, nullable: false),
                    NotificationSate = table.Column<int>(type: "int", nullable: false),
                    ReadNotificationDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Img_Url = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: false),
                    Guid = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SiteId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StoryNotifications", x => x.Id);
                    table.ForeignKey(
                        name: "FK_StoryNotifications_AspNetUsers_UserGuid",
                        column: x => x.UserGuid,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_StoryNotifications_Story_StoryGuid",
                        column: x => x.StoryGuid,
                        principalTable: "Story",
                        principalColumn: "Guid",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TagInStory",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreatedUserId = table.Column<int>(type: "int", nullable: false),
                    UpdatedUserId = table.Column<int>(type: "int", nullable: true),
                    DeletedUserId = table.Column<int>(type: "int", nullable: true),
                    CreatedUserName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    UpdatedUserName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    DeletedUserName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    CreatedDateTS = table.Column<double>(type: "float", nullable: false),
                    UpdatedDateTS = table.Column<double>(type: "float", nullable: true),
                    DeletedDateTS = table.Column<double>(type: "float", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    StoryGuid = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TagId = table.Column<int>(type: "int", nullable: false),
                    Guid = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SiteId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TagInStory", x => new { x.StoryGuid, x.TagId });
                    table.ForeignKey(
                        name: "FK_TagInStory_Story_StoryGuid",
                        column: x => x.StoryGuid,
                        principalTable: "Story",
                        principalColumn: "Guid",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TagInStory_Tag_TagId",
                        column: x => x.TagId,
                        principalTable: "Tag",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Description", "Name", "NormalizedName" },
                values: new object[] { new Guid("72377426-b057-46ca-98ff-1ca9d33ea0c1"), "63311886-7f55-45f6-aa4b-75f8bbee5503", "Người quản lý cao nhất", "Administratior", null });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "Address", "Avatar", "BirthDate", "ConcurrencyStamp", "Email", "EmailConfirmed", "Gender", "GroupId", "LastLogin", "LockReason", "LockoutEnabled", "LockoutEnd", "Name", "NormalizedEmail", "NormalizedUserName", "Note", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "Status", "Surname", "TwoFactorEnabled", "UserName" },
                values: new object[] { new Guid("e0223a03-2945-49db-976e-736433465b7f"), 0, null, null, null, "7900a40e-ff12-41bd-95b6-6c3fc294fd0b", "leanhphi1706@gmail.com", false, null, null, null, null, false, null, "Phi Le", null, null, null, "AQAAAAEAACcQAAAAEHwagF11oCjTp3pLAWt//8vtinUTnYvfTdFfR30F2lapmfOZOvW2H10hJFsPkR6yYg==", "093.310.5367", false, null, 1, "Anh", false, "muonroi" });

            migrationBuilder.InsertData(
                table: "Category",
                columns: new[] { "Id", "CreatedDateTS", "CreatedUserId", "CreatedUserName", "DeletedDateTS", "DeletedUserId", "DeletedUserName", "Guid", "IsActive", "IsDeleted", "NameCategory", "SiteId", "UpdatedDateTS", "UpdatedUserId", "UpdatedUserName" },
                values: new object[,]
                {
                    { 1, 1672531200.0, 1, "muonroi", null, null, null, new Guid("00000000-0000-0000-0000-000000000000"), true, false, "Tiên hiệp", 0, null, null, null },
                    { 2, 1672531200.0, 1, "muonroi", null, null, null, new Guid("00000000-0000-0000-0000-000000000000"), true, false, "Huyền huyễn", 0, null, null, null }
                });

            migrationBuilder.InsertData(
                table: "Tag",
                columns: new[] { "Id", "CreatedDateTS", "CreatedUserId", "CreatedUserName", "DeletedDateTS", "DeletedUserId", "DeletedUserName", "Details", "Guid", "IsDeleted", "SiteId", "TagName", "UpdatedDateTS", "UpdatedUserId", "UpdatedUserName" },
                values: new object[,]
                {
                    { 1, 1672531200.0, 1, "muonroi", null, null, null, "Truyện đã hoàn thành xong", new Guid("00000000-0000-0000-0000-000000000000"), false, 0, "Đã hoàn thành", null, null, null },
                    { 2, 1672531200.0, 1, "muonroi", null, null, null, "Truyện chưa hoàn thành xong", new Guid("00000000-0000-0000-0000-000000000000"), false, 0, "Chưa hoàn thành", null, null, null }
                });

            migrationBuilder.InsertData(
                table: "GroupUserMember",
                columns: new[] { "AppRoleKey", "AppUserKey", "CreatedDateTS", "CreatedUserId", "CreatedUserName", "DeletedDateTS", "DeletedUserId", "DeletedUserName", "Guest", "Guid", "Id", "IsDeleted", "Manage", "SiteId", "Staff", "UpdatedDateTS", "UpdatedUserId", "UpdatedUserName", "Viewer" },
                values: new object[] { new Guid("72377426-b057-46ca-98ff-1ca9d33ea0c1"), new Guid("e0223a03-2945-49db-976e-736433465b7f"), 1672531200.0, 1, "muonroi", null, null, null, 0, new Guid("00000000-0000-0000-0000-000000000000"), 1, false, 0, 0, 0, null, null, null, 0 });

            migrationBuilder.InsertData(
                table: "Story",
                columns: new[] { "Guid", "CategoryId", "CreatedDateTS", "CreatedUserId", "CreatedUserName", "DeletedDateTS", "DeletedUserId", "DeletedUserName", "Img_Url", "IsDeleted", "IsShow", "Rating", "SiteId", "Slug", "Story_Synopsis", "Story_Title", "TotalFavorite", "TotalView", "UpdatedDateTS", "UpdatedUserId", "UpdatedUserName" },
                values: new object[] { new Guid("1bcfb006-2ec0-4795-9610-6c711b6955df"), 1, 1672531200.0, 1, "muonroi", null, null, null, "079dec71-43fd-4701-8450-a1ad1e6c39ff.image/jpeg*Stories/ConnectVn_nhan-sinh-tuy-tien-bat-dau-tu-tuoi-ba-muoi-ban-dic-7a54bfe686.jpg", false, true, 0.0, 0, "nhan-sinh-tuy-tien-bat-dau-tu-tuoi-ba-muoi", "Người khác xuyên việt trẻ thêm vài tuổi, Trần Tự xuyên việt thành ông chú 30.  Tưởng đâu đã có mái ấm êm đềm, ai ngờ xuyên đến lại đúng dịp ly hôn  Nếu như đã không có gì để mà lo lắng, vậy thì sống cho thật thoải mái đi.", "Nhân Sinh Tùy Tiện Bắt Đầu Từ Tuổi Ba Mươi (Bản Dịch)", 0, 0, null, null, null });

            migrationBuilder.InsertData(
                table: "Story",
                columns: new[] { "Guid", "CategoryId", "CreatedDateTS", "CreatedUserId", "CreatedUserName", "DeletedDateTS", "DeletedUserId", "DeletedUserName", "Img_Url", "IsDeleted", "IsShow", "Rating", "SiteId", "Slug", "Story_Synopsis", "Story_Title", "TotalFavorite", "TotalView", "UpdatedDateTS", "UpdatedUserId", "UpdatedUserName" },
                values: new object[] { new Guid("4636aa8f-683a-4cb4-8498-152a63c9d881"), 1, 1672531200.0, 1, "muonroi", null, null, null, "aacd50da-4e0d-47e4-939c-a4ace0f707ea.image/jpeg*Stories/ConnectVn_ta-co-mot-toa-khi-van-te-dan-cbde5bc2e8.jpg", false, true, 0.0, 0, "ta-co-mot-toa-khi-van-te-dan", "Bình An huyện nha dịch Trần Uyên xuyên qua mà đến, trong đầu cất giấu một tòa khí vận tế đàn .  Chỉ cần hiến tế khí vận, liền có thể thu được thiên cơ chỉ dẫn, thần thông, công pháp, tà thuật, thiên tài địa bảo ...  Mọi loại đều là hạ phẩm, chỉ có tập võ cao!  Đại Tấn những năm cuối, Tây vực Phật môn truyền đạo Trung Nguyên, Nam Cương yêu tộc nhìn chằm chằm .  Bắc man thiết kỵ 300 ngàn, uy áp biên cảnh .  Đạo môn chân nhân, Kiếm Tông kiếm tiên, ma đạo cự phách, giang hồ danh túc ... Thiên hạ đem loạn!  Đây là xấu nhất thời đại, cũng là tốt nhất thời đại ...  Ta gọi Trần Uyên, đến từ vực sâu!  Sát phạt quả đoán .  Chúc bạn có những giây phút vui vẻ khi đọc truyện Ta Có Một Tòa Khí Vận Tế Đàn!", "Ta Có Một Tòa Khí Vận Tế Đàn", 0, 0, null, null, null });

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoleClaims_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles",
                column: "NormalizedName",
                unique: true,
                filter: "[NormalizedName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserClaims_UserId",
                table: "AspNetUserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserLogins_UserId",
                table: "AspNetUserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_RoleId",
                table: "AspNetUserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "AspNetUsers",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "AspNetUsers",
                column: "NormalizedUserName",
                unique: true,
                filter: "[NormalizedUserName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_BookMarkStory_UserGuid",
                table: "BookMarkStory",
                column: "UserGuid");

            migrationBuilder.CreateIndex(
                name: "IX_Chapter_StoryGuid",
                table: "Chapter",
                column: "StoryGuid");

            migrationBuilder.CreateIndex(
                name: "IX_FollowingAuthor_UserGuid",
                table: "FollowingAuthor",
                column: "UserGuid");

            migrationBuilder.CreateIndex(
                name: "IX_GroupUserMember_AppRoleKey",
                table: "GroupUserMember",
                column: "AppRoleKey");

            migrationBuilder.CreateIndex(
                name: "IX_Story_CategoryId",
                table: "Story",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_StoryNotifications_StoryGuid",
                table: "StoryNotifications",
                column: "StoryGuid");

            migrationBuilder.CreateIndex(
                name: "IX_StoryNotifications_UserGuid",
                table: "StoryNotifications",
                column: "UserGuid");

            migrationBuilder.CreateIndex(
                name: "IX_StoryPublish_UserGuid",
                table: "StoryPublish",
                column: "UserGuid");

            migrationBuilder.CreateIndex(
                name: "IX_StoryReview_UserGuid",
                table: "StoryReview",
                column: "UserGuid");

            migrationBuilder.CreateIndex(
                name: "IX_TagInStory_TagId",
                table: "TagInStory",
                column: "TagId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AspNetRoleClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserLogins");

            migrationBuilder.DropTable(
                name: "AspNetUserRoles");

            migrationBuilder.DropTable(
                name: "AspNetUserTokens");

            migrationBuilder.DropTable(
                name: "BookMarkStory");

            migrationBuilder.DropTable(
                name: "Chapter");

            migrationBuilder.DropTable(
                name: "FollowingAuthor");

            migrationBuilder.DropTable(
                name: "GroupUserMember");

            migrationBuilder.DropTable(
                name: "StoryNotifications");

            migrationBuilder.DropTable(
                name: "StoryPublish");

            migrationBuilder.DropTable(
                name: "StoryReview");

            migrationBuilder.DropTable(
                name: "TagInStory");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "Story");

            migrationBuilder.DropTable(
                name: "Tag");

            migrationBuilder.DropTable(
                name: "Category");
        }
    }
}
