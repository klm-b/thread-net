using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Thread_.NET.DAL.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Images",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    URL = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Images", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AvatarId = table.Column<int>(type: "int", nullable: true),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Salt = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Users_Images_AvatarId",
                        column: x => x.AvatarId,
                        principalTable: "Images",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Posts",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AuthorId = table.Column<int>(type: "int", nullable: false),
                    PreviewId = table.Column<int>(type: "int", nullable: true),
                    Body = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Posts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Posts_Images_PreviewId",
                        column: x => x.PreviewId,
                        principalTable: "Images",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Posts_Users_AuthorId",
                        column: x => x.AuthorId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "RefreshTokens",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Token = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Expires = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RefreshTokens", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RefreshTokens_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Comments",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AuthorId = table.Column<int>(type: "int", nullable: false),
                    PostId = table.Column<int>(type: "int", nullable: false),
                    Body = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Comments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Comments_Posts_PostId",
                        column: x => x.PostId,
                        principalTable: "Posts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Comments_Users_AuthorId",
                        column: x => x.AuthorId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PostReactions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PostId = table.Column<int>(type: "int", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    IsLike = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PostReactions", x => x.Id);
                    table.UniqueConstraint("AK_PostReactions_PostId_UserId", x => new { x.PostId, x.UserId });
                    table.ForeignKey(
                        name: "FK_PostReactions_Posts_PostId",
                        column: x => x.PostId,
                        principalTable: "Posts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PostReactions_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CommentReactions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CommentId = table.Column<int>(type: "int", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    IsLike = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CommentReactions", x => x.Id);
                    table.UniqueConstraint("AK_CommentReactions_CommentId_UserId", x => new { x.CommentId, x.UserId });
                    table.ForeignKey(
                        name: "FK_CommentReactions_Comments_CommentId",
                        column: x => x.CommentId,
                        principalTable: "Comments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CommentReactions_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Images",
                columns: new[] { "Id", "CreatedAt", "URL", "UpdatedAt" },
                values: new object[,]
                {
                    { 1, new DateTime(2022, 1, 12, 22, 10, 37, 541, DateTimeKind.Local).AddTicks(7174), "https://cloudflare-ipfs.com/ipfs/Qmd3W5DuhgHirLHGVixi6V76LhCkZUz6pnFt5AJBiyvHye/avatar/838.jpg", new DateTime(2022, 1, 12, 22, 10, 37, 542, DateTimeKind.Local).AddTicks(5473) },
                    { 23, new DateTime(2022, 1, 12, 22, 10, 37, 552, DateTimeKind.Local).AddTicks(9611), "https://picsum.photos/640/480/?image=814", new DateTime(2022, 1, 12, 22, 10, 37, 552, DateTimeKind.Local).AddTicks(9616) },
                    { 24, new DateTime(2022, 1, 12, 22, 10, 37, 552, DateTimeKind.Local).AddTicks(9639), "https://picsum.photos/640/480/?image=267", new DateTime(2022, 1, 12, 22, 10, 37, 552, DateTimeKind.Local).AddTicks(9643) },
                    { 25, new DateTime(2022, 1, 12, 22, 10, 37, 552, DateTimeKind.Local).AddTicks(9665), "https://picsum.photos/640/480/?image=128", new DateTime(2022, 1, 12, 22, 10, 37, 552, DateTimeKind.Local).AddTicks(9668) },
                    { 26, new DateTime(2022, 1, 12, 22, 10, 37, 552, DateTimeKind.Local).AddTicks(9690), "https://picsum.photos/640/480/?image=990", new DateTime(2022, 1, 12, 22, 10, 37, 552, DateTimeKind.Local).AddTicks(9694) },
                    { 27, new DateTime(2022, 1, 12, 22, 10, 37, 552, DateTimeKind.Local).AddTicks(9717), "https://picsum.photos/640/480/?image=463", new DateTime(2022, 1, 12, 22, 10, 37, 552, DateTimeKind.Local).AddTicks(9720) },
                    { 28, new DateTime(2022, 1, 12, 22, 10, 37, 552, DateTimeKind.Local).AddTicks(9743), "https://picsum.photos/640/480/?image=719", new DateTime(2022, 1, 12, 22, 10, 37, 552, DateTimeKind.Local).AddTicks(9746) },
                    { 29, new DateTime(2022, 1, 12, 22, 10, 37, 552, DateTimeKind.Local).AddTicks(9855), "https://picsum.photos/640/480/?image=977", new DateTime(2022, 1, 12, 22, 10, 37, 552, DateTimeKind.Local).AddTicks(9860) },
                    { 30, new DateTime(2022, 1, 12, 22, 10, 37, 552, DateTimeKind.Local).AddTicks(9889), "https://picsum.photos/640/480/?image=550", new DateTime(2022, 1, 12, 22, 10, 37, 552, DateTimeKind.Local).AddTicks(9893) },
                    { 31, new DateTime(2022, 1, 12, 22, 10, 37, 552, DateTimeKind.Local).AddTicks(9916), "https://picsum.photos/640/480/?image=646", new DateTime(2022, 1, 12, 22, 10, 37, 552, DateTimeKind.Local).AddTicks(9920) },
                    { 32, new DateTime(2022, 1, 12, 22, 10, 37, 552, DateTimeKind.Local).AddTicks(9942), "https://picsum.photos/640/480/?image=867", new DateTime(2022, 1, 12, 22, 10, 37, 552, DateTimeKind.Local).AddTicks(9946) },
                    { 33, new DateTime(2022, 1, 12, 22, 10, 37, 552, DateTimeKind.Local).AddTicks(9968), "https://picsum.photos/640/480/?image=811", new DateTime(2022, 1, 12, 22, 10, 37, 552, DateTimeKind.Local).AddTicks(9972) },
                    { 34, new DateTime(2022, 1, 12, 22, 10, 37, 552, DateTimeKind.Local).AddTicks(9995), "https://picsum.photos/640/480/?image=848", new DateTime(2022, 1, 12, 22, 10, 37, 552, DateTimeKind.Local).AddTicks(9998) },
                    { 35, new DateTime(2022, 1, 12, 22, 10, 37, 553, DateTimeKind.Local).AddTicks(21), "https://picsum.photos/640/480/?image=472", new DateTime(2022, 1, 12, 22, 10, 37, 553, DateTimeKind.Local).AddTicks(25) },
                    { 36, new DateTime(2022, 1, 12, 22, 10, 37, 553, DateTimeKind.Local).AddTicks(50), "https://picsum.photos/640/480/?image=1077", new DateTime(2022, 1, 12, 22, 10, 37, 553, DateTimeKind.Local).AddTicks(53) },
                    { 37, new DateTime(2022, 1, 12, 22, 10, 37, 553, DateTimeKind.Local).AddTicks(77), "https://picsum.photos/640/480/?image=1055", new DateTime(2022, 1, 12, 22, 10, 37, 553, DateTimeKind.Local).AddTicks(81) },
                    { 38, new DateTime(2022, 1, 12, 22, 10, 37, 553, DateTimeKind.Local).AddTicks(103), "https://picsum.photos/640/480/?image=987", new DateTime(2022, 1, 12, 22, 10, 37, 553, DateTimeKind.Local).AddTicks(107) },
                    { 39, new DateTime(2022, 1, 12, 22, 10, 37, 553, DateTimeKind.Local).AddTicks(128), "https://picsum.photos/640/480/?image=752", new DateTime(2022, 1, 12, 22, 10, 37, 553, DateTimeKind.Local).AddTicks(132) },
                    { 22, new DateTime(2022, 1, 12, 22, 10, 37, 552, DateTimeKind.Local).AddTicks(9571), "https://picsum.photos/640/480/?image=168", new DateTime(2022, 1, 12, 22, 10, 37, 552, DateTimeKind.Local).AddTicks(9578) },
                    { 40, new DateTime(2022, 1, 12, 22, 10, 37, 553, DateTimeKind.Local).AddTicks(155), "https://picsum.photos/640/480/?image=864", new DateTime(2022, 1, 12, 22, 10, 37, 553, DateTimeKind.Local).AddTicks(159) },
                    { 21, new DateTime(2022, 1, 12, 22, 10, 37, 552, DateTimeKind.Local).AddTicks(8311), "https://picsum.photos/640/480/?image=483", new DateTime(2022, 1, 12, 22, 10, 37, 552, DateTimeKind.Local).AddTicks(9196) },
                    { 19, new DateTime(2022, 1, 12, 22, 10, 37, 542, DateTimeKind.Local).AddTicks(7608), "https://cloudflare-ipfs.com/ipfs/Qmd3W5DuhgHirLHGVixi6V76LhCkZUz6pnFt5AJBiyvHye/avatar/678.jpg", new DateTime(2022, 1, 12, 22, 10, 37, 542, DateTimeKind.Local).AddTicks(7617) },
                    { 2, new DateTime(2022, 1, 12, 22, 10, 37, 542, DateTimeKind.Local).AddTicks(6567), "https://cloudflare-ipfs.com/ipfs/Qmd3W5DuhgHirLHGVixi6V76LhCkZUz6pnFt5AJBiyvHye/avatar/609.jpg", new DateTime(2022, 1, 12, 22, 10, 37, 542, DateTimeKind.Local).AddTicks(6578) },
                    { 3, new DateTime(2022, 1, 12, 22, 10, 37, 542, DateTimeKind.Local).AddTicks(6640), "https://cloudflare-ipfs.com/ipfs/Qmd3W5DuhgHirLHGVixi6V76LhCkZUz6pnFt5AJBiyvHye/avatar/963.jpg", new DateTime(2022, 1, 12, 22, 10, 37, 542, DateTimeKind.Local).AddTicks(6647) },
                    { 4, new DateTime(2022, 1, 12, 22, 10, 37, 542, DateTimeKind.Local).AddTicks(6692), "https://cloudflare-ipfs.com/ipfs/Qmd3W5DuhgHirLHGVixi6V76LhCkZUz6pnFt5AJBiyvHye/avatar/178.jpg", new DateTime(2022, 1, 12, 22, 10, 37, 542, DateTimeKind.Local).AddTicks(6698) },
                    { 5, new DateTime(2022, 1, 12, 22, 10, 37, 542, DateTimeKind.Local).AddTicks(6884), "https://cloudflare-ipfs.com/ipfs/Qmd3W5DuhgHirLHGVixi6V76LhCkZUz6pnFt5AJBiyvHye/avatar/685.jpg", new DateTime(2022, 1, 12, 22, 10, 37, 542, DateTimeKind.Local).AddTicks(6890) },
                    { 6, new DateTime(2022, 1, 12, 22, 10, 37, 542, DateTimeKind.Local).AddTicks(6937), "https://cloudflare-ipfs.com/ipfs/Qmd3W5DuhgHirLHGVixi6V76LhCkZUz6pnFt5AJBiyvHye/avatar/682.jpg", new DateTime(2022, 1, 12, 22, 10, 37, 542, DateTimeKind.Local).AddTicks(6947) },
                    { 7, new DateTime(2022, 1, 12, 22, 10, 37, 542, DateTimeKind.Local).AddTicks(6993), "https://cloudflare-ipfs.com/ipfs/Qmd3W5DuhgHirLHGVixi6V76LhCkZUz6pnFt5AJBiyvHye/avatar/888.jpg", new DateTime(2022, 1, 12, 22, 10, 37, 542, DateTimeKind.Local).AddTicks(7002) },
                    { 8, new DateTime(2022, 1, 12, 22, 10, 37, 542, DateTimeKind.Local).AddTicks(7048), "https://cloudflare-ipfs.com/ipfs/Qmd3W5DuhgHirLHGVixi6V76LhCkZUz6pnFt5AJBiyvHye/avatar/1025.jpg", new DateTime(2022, 1, 12, 22, 10, 37, 542, DateTimeKind.Local).AddTicks(7057) },
                    { 9, new DateTime(2022, 1, 12, 22, 10, 37, 542, DateTimeKind.Local).AddTicks(7102), "https://cloudflare-ipfs.com/ipfs/Qmd3W5DuhgHirLHGVixi6V76LhCkZUz6pnFt5AJBiyvHye/avatar/809.jpg", new DateTime(2022, 1, 12, 22, 10, 37, 542, DateTimeKind.Local).AddTicks(7108) },
                    { 10, new DateTime(2022, 1, 12, 22, 10, 37, 542, DateTimeKind.Local).AddTicks(7153), "https://cloudflare-ipfs.com/ipfs/Qmd3W5DuhgHirLHGVixi6V76LhCkZUz6pnFt5AJBiyvHye/avatar/35.jpg", new DateTime(2022, 1, 12, 22, 10, 37, 542, DateTimeKind.Local).AddTicks(7160) },
                    { 11, new DateTime(2022, 1, 12, 22, 10, 37, 542, DateTimeKind.Local).AddTicks(7205), "https://cloudflare-ipfs.com/ipfs/Qmd3W5DuhgHirLHGVixi6V76LhCkZUz6pnFt5AJBiyvHye/avatar/394.jpg", new DateTime(2022, 1, 12, 22, 10, 37, 542, DateTimeKind.Local).AddTicks(7215) },
                    { 12, new DateTime(2022, 1, 12, 22, 10, 37, 542, DateTimeKind.Local).AddTicks(7259), "https://cloudflare-ipfs.com/ipfs/Qmd3W5DuhgHirLHGVixi6V76LhCkZUz6pnFt5AJBiyvHye/avatar/1179.jpg", new DateTime(2022, 1, 12, 22, 10, 37, 542, DateTimeKind.Local).AddTicks(7264) },
                    { 13, new DateTime(2022, 1, 12, 22, 10, 37, 542, DateTimeKind.Local).AddTicks(7310), "https://cloudflare-ipfs.com/ipfs/Qmd3W5DuhgHirLHGVixi6V76LhCkZUz6pnFt5AJBiyvHye/avatar/1088.jpg", new DateTime(2022, 1, 12, 22, 10, 37, 542, DateTimeKind.Local).AddTicks(7317) },
                    { 14, new DateTime(2022, 1, 12, 22, 10, 37, 542, DateTimeKind.Local).AddTicks(7363), "https://cloudflare-ipfs.com/ipfs/Qmd3W5DuhgHirLHGVixi6V76LhCkZUz6pnFt5AJBiyvHye/avatar/281.jpg", new DateTime(2022, 1, 12, 22, 10, 37, 542, DateTimeKind.Local).AddTicks(7370) },
                    { 15, new DateTime(2022, 1, 12, 22, 10, 37, 542, DateTimeKind.Local).AddTicks(7414), "https://cloudflare-ipfs.com/ipfs/Qmd3W5DuhgHirLHGVixi6V76LhCkZUz6pnFt5AJBiyvHye/avatar/1172.jpg", new DateTime(2022, 1, 12, 22, 10, 37, 542, DateTimeKind.Local).AddTicks(7424) },
                    { 16, new DateTime(2022, 1, 12, 22, 10, 37, 542, DateTimeKind.Local).AddTicks(7469), "https://cloudflare-ipfs.com/ipfs/Qmd3W5DuhgHirLHGVixi6V76LhCkZUz6pnFt5AJBiyvHye/avatar/334.jpg", new DateTime(2022, 1, 12, 22, 10, 37, 542, DateTimeKind.Local).AddTicks(7475) },
                    { 17, new DateTime(2022, 1, 12, 22, 10, 37, 542, DateTimeKind.Local).AddTicks(7508), "https://cloudflare-ipfs.com/ipfs/Qmd3W5DuhgHirLHGVixi6V76LhCkZUz6pnFt5AJBiyvHye/avatar/698.jpg", new DateTime(2022, 1, 12, 22, 10, 37, 542, DateTimeKind.Local).AddTicks(7512) },
                    { 18, new DateTime(2022, 1, 12, 22, 10, 37, 542, DateTimeKind.Local).AddTicks(7547), "https://cloudflare-ipfs.com/ipfs/Qmd3W5DuhgHirLHGVixi6V76LhCkZUz6pnFt5AJBiyvHye/avatar/30.jpg", new DateTime(2022, 1, 12, 22, 10, 37, 542, DateTimeKind.Local).AddTicks(7555) },
                    { 20, new DateTime(2022, 1, 12, 22, 10, 37, 542, DateTimeKind.Local).AddTicks(7664), "https://cloudflare-ipfs.com/ipfs/Qmd3W5DuhgHirLHGVixi6V76LhCkZUz6pnFt5AJBiyvHye/avatar/308.jpg", new DateTime(2022, 1, 12, 22, 10, 37, 542, DateTimeKind.Local).AddTicks(7668) }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "AvatarId", "CreatedAt", "Email", "Password", "Salt", "UpdatedAt", "UserName" },
                values: new object[] { 21, null, new DateTime(2022, 1, 12, 22, 10, 37, 782, DateTimeKind.Local).AddTicks(3024), "test@gmail.com", "I8iDEHzTrasfFX9gUP8xApIMrWhA3yfJCZB44X9CAr0=", "FaX3ExCQpnSFCE32l+kGQ8r8KZ3GMDOwwT/wm9rhpmU=", new DateTime(2022, 1, 12, 22, 10, 37, 782, DateTimeKind.Local).AddTicks(3024), "testUser" });

            migrationBuilder.InsertData(
                table: "Posts",
                columns: new[] { "Id", "AuthorId", "Body", "CreatedAt", "IsDeleted", "PreviewId", "UpdatedAt" },
                values: new object[,]
                {
                    { 9, 21, "Et vitae earum. Illo non possimus magnam tempora culpa omnis temporibus. Natus blanditiis ratione. Et beatae ipsa odio nostrum est. Quod sit exercitationem molestiae a adipisci ducimus rerum. Iusto libero error nihil porro numquam.", new DateTime(2022, 1, 12, 22, 10, 37, 783, DateTimeKind.Local).AddTicks(3365), false, 22, new DateTime(2022, 1, 12, 22, 10, 37, 783, DateTimeKind.Local).AddTicks(3365) },
                    { 3, 21, "Veritatis minus itaque perspiciatis nihil fuga.\nExplicabo reprehenderit et amet laborum voluptatem qui minus sapiente voluptatem.\nSuscipit nihil harum quos dolore.\nEnim atque aperiam repudiandae impedit.", new DateTime(2022, 1, 12, 22, 10, 37, 783, DateTimeKind.Local).AddTicks(3365), false, 22, new DateTime(2022, 1, 12, 22, 10, 37, 783, DateTimeKind.Local).AddTicks(3365) }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "AvatarId", "CreatedAt", "Email", "Password", "Salt", "UpdatedAt", "UserName" },
                values: new object[,]
                {
                    { 12, 20, new DateTime(2022, 1, 12, 22, 10, 37, 700, DateTimeKind.Local).AddTicks(3515), "Daryl57@gmail.com", "9uHtUJbwDuD3keMCJfPN+MMG+4oZSB21y4kkz37+JGs=", "U9c0TnbrZQWfjTMJu72657sskL8Ct08YAtmO/WcoZbE=", new DateTime(2022, 1, 12, 22, 10, 37, 700, DateTimeKind.Local).AddTicks(3544), "Karianne_Dickinson" },
                    { 6, 20, new DateTime(2022, 1, 12, 22, 10, 37, 644, DateTimeKind.Local).AddTicks(9867), "Nya.Conn@yahoo.com", "9SnOF4ljiCYbcbgFIAc1YsJFXqIXB/xnvXNm0UrAOko=", "Nlb5MSAnHM/+5zTXEGbovHKS9AiWInDaye8xmx8mpcU=", new DateTime(2022, 1, 12, 22, 10, 37, 644, DateTimeKind.Local).AddTicks(9895), "Israel_Rempel9" },
                    { 19, 19, new DateTime(2022, 1, 12, 22, 10, 37, 764, DateTimeKind.Local).AddTicks(3751), "Irving_Thompson@yahoo.com", "seC8ZGWbFSfC5WIOeisoGZcaKk1W98SBt8N0+T9C7qI=", "lo+VahcZ1X1RYzeJU3qEnDTSBilRSrWpGw9SNYGcC3I=", new DateTime(2022, 1, 12, 22, 10, 37, 764, DateTimeKind.Local).AddTicks(3761), "Ruthe.Fisher6" },
                    { 13, 19, new DateTime(2022, 1, 12, 22, 10, 37, 709, DateTimeKind.Local).AddTicks(3194), "Osborne.Leuschke@hotmail.com", "VzmYh2xxlrJXzcglT5ojA5yu8OerI3bv7Y8DlpBOS8M=", "/O6VS7qw5rcFxTWua2JxK3Rb2iGQdPfUul7n3uhdsug=", new DateTime(2022, 1, 12, 22, 10, 37, 709, DateTimeKind.Local).AddTicks(3211), "Ashlynn_Schuppe" },
                    { 11, 19, new DateTime(2022, 1, 12, 22, 10, 37, 690, DateTimeKind.Local).AddTicks(9916), "Marcella_Hettinger97@gmail.com", "4QBN7LRuRKSunL+nf3Wl/MMI/aqLJu5zr2Hp4Le7SNk=", "3nQY6ClDQ7uU36Gh4s7JyJO2KjWmY875aJNLpNtoFis=", new DateTime(2022, 1, 12, 22, 10, 37, 690, DateTimeKind.Local).AddTicks(9934), "Kaleigh_Feil" },
                    { 1, 19, new DateTime(2022, 1, 12, 22, 10, 37, 598, DateTimeKind.Local).AddTicks(9674), "Chet40@gmail.com", "fGy1sAXEjUCozuTLXdmAlYt05KTHExvPRpTDCsjZMnU=", "VR29j9M7tRZHu1hIXHqTlpjFw+fPjJKi919RN5sCCTM=", new DateTime(2022, 1, 12, 22, 10, 37, 599, DateTimeKind.Local).AddTicks(305), "Pansy31" },
                    { 16, 18, new DateTime(2022, 1, 12, 22, 10, 37, 737, DateTimeKind.Local).AddTicks(3609), "Keely58@hotmail.com", "DA1X2D9PKIaOOgEm4wDBbV/2ljC1YqwIWR69PB7ZG8k=", "sw4fQnATidxlKwZ3yJ7zfYGgvin3jJp8lI4MQ9v6YDU=", new DateTime(2022, 1, 12, 22, 10, 37, 737, DateTimeKind.Local).AddTicks(3622), "Davion_Moore38" },
                    { 20, 16, new DateTime(2022, 1, 12, 22, 10, 37, 773, DateTimeKind.Local).AddTicks(3996), "Otha76@hotmail.com", "edCVkQfRxxNwsOBJQBiVCtr9nsqEOvqOft/HWEcYs4M=", "2Xpiv55lN06Z8xKyjg12KP0jo3MC2p9TSNgSuQptEnc=", new DateTime(2022, 1, 12, 22, 10, 37, 773, DateTimeKind.Local).AddTicks(4016), "Mattie89" },
                    { 3, 15, new DateTime(2022, 1, 12, 22, 10, 37, 617, DateTimeKind.Local).AddTicks(9057), "Mack53@hotmail.com", "QtEpJwPYSS8b8A/+RvuKVlACD5RZdWONaKMrSn6rfJc=", "JTpvJMsj6lydyxrGeaGiTDDlwoSY8Yytcl2ClXCgOZY=", new DateTime(2022, 1, 12, 22, 10, 37, 617, DateTimeKind.Local).AddTicks(9090), "Mariano.Robel" },
                    { 17, 14, new DateTime(2022, 1, 12, 22, 10, 37, 746, DateTimeKind.Local).AddTicks(3570), "Leanne_Bailey@hotmail.com", "/TqtlXfZtVM8FWl7axYl8PWNTsqgIB6BgnJub/rjb1Q=", "yvDbPdiTKVgH/CYekjWDvy0BjAUtNR1hWYjnhHFPPjo=", new DateTime(2022, 1, 12, 22, 10, 37, 746, DateTimeKind.Local).AddTicks(3586), "Adelle.Larson" },
                    { 10, 12, new DateTime(2022, 1, 12, 22, 10, 37, 681, DateTimeKind.Local).AddTicks(8707), "Adela.Klocko68@gmail.com", "yAWFLk0N4bd7GkMahcR9mcaQK4w03G1W50inydIYGd0=", "y8NsqjKydkiTl1hiZp/KrARjaa3rm82s0vkRkxlH3BM=", new DateTime(2022, 1, 12, 22, 10, 37, 681, DateTimeKind.Local).AddTicks(8738), "Jacques57" },
                    { 9, 12, new DateTime(2022, 1, 12, 22, 10, 37, 672, DateTimeKind.Local).AddTicks(4127), "Alexie.Hamill56@gmail.com", "PdiJPqgm7mCVL0YvHI/SrpR9jyPFFa3SJF4+8bc09QA=", "n0EH7BecrWRBmE7A6aHnm20Tpmi2u8M5LbCfADF3sfQ=", new DateTime(2022, 1, 12, 22, 10, 37, 672, DateTimeKind.Local).AddTicks(4144), "Marvin.Leuschke" },
                    { 5, 12, new DateTime(2022, 1, 12, 22, 10, 37, 635, DateTimeKind.Local).AddTicks(9078), "Joey_Kreiger@gmail.com", "Jt/ZM1dnDl30PCFLyn+XZNEkxMd5i05dpFQUR7dnG3I=", "UOy1TrF9caeMzSJWric77hSCZWhV5SvCAoUgdj8CihM=", new DateTime(2022, 1, 12, 22, 10, 37, 635, DateTimeKind.Local).AddTicks(9100), "Mandy52" },
                    { 14, 8, new DateTime(2022, 1, 12, 22, 10, 37, 718, DateTimeKind.Local).AddTicks(3458), "Brain.Metz@yahoo.com", "fnd533Ih9BnzpWYXm/e8PK3s++tP+eAbKR0LFwK3yfM=", "V+xUwbM9MkI2eMP0PrHMvXQb1XzgeoLp5pEVdHfa+gA=", new DateTime(2022, 1, 12, 22, 10, 37, 718, DateTimeKind.Local).AddTicks(3476), "Sigrid.Cruickshank42" },
                    { 7, 6, new DateTime(2022, 1, 12, 22, 10, 37, 654, DateTimeKind.Local).AddTicks(2606), "Rosetta48@gmail.com", "mddtLUV3Tzs1xvmHEUVxXXIqJV4Ipdzuu8v8XRgc4V8=", "GS6DYS7SCqx3UkoYI0+X0GaT6bQaaZchpAko7Gfz4aw=", new DateTime(2022, 1, 12, 22, 10, 37, 654, DateTimeKind.Local).AddTicks(2634), "Tierra.Lang" },
                    { 4, 4, new DateTime(2022, 1, 12, 22, 10, 37, 626, DateTimeKind.Local).AddTicks(9077), "Rosamond6@hotmail.com", "1N+/Cj8WFYJ0ab3yLeVL3LrgR56OY+SpXUnsPLTUH/M=", "AlkUMt6OWqVSaGzL+jb1/VWJjRKYYe3ono3E8rqML6k=", new DateTime(2022, 1, 12, 22, 10, 37, 626, DateTimeKind.Local).AddTicks(9089), "Buford_Lang" },
                    { 18, 3, new DateTime(2022, 1, 12, 22, 10, 37, 755, DateTimeKind.Local).AddTicks(4310), "Darren.Runolfsdottir@hotmail.com", "Cl4G3D+9N25qfeFpbGr1ol0lJPHcx/ti2z2dvaltunk=", "8SMq5F5Mc6jZaonOAApIaZPmL0MZVAvvQWWEXbkn8Cs=", new DateTime(2022, 1, 12, 22, 10, 37, 755, DateTimeKind.Local).AddTicks(4331), "Alessandro_Dare36" },
                    { 8, 3, new DateTime(2022, 1, 12, 22, 10, 37, 663, DateTimeKind.Local).AddTicks(3643), "Ryley_Johnson@hotmail.com", "8m/vFN4YVe5ZU+U9SoZRZB5kW+jkFC6LTvgxcwaI68s=", "mvcabPOwlLlssiM/KI3l/DQmAdlcDJ4rRzRa9ojcqPU=", new DateTime(2022, 1, 12, 22, 10, 37, 663, DateTimeKind.Local).AddTicks(3671), "Brittany.Daniel97" },
                    { 15, 14, new DateTime(2022, 1, 12, 22, 10, 37, 728, DateTimeKind.Local).AddTicks(3804), "Martina13@gmail.com", "BiMyAhczmQkABcaxb1nCqOvI5xzwq9rpH4NOzaZB0DY=", "eLlZMtterngB1g407esoGFWaw8aC6XK2qLUD7KN65ec=", new DateTime(2022, 1, 12, 22, 10, 37, 728, DateTimeKind.Local).AddTicks(3823), "Aletha_Towne" },
                    { 2, 2, new DateTime(2022, 1, 12, 22, 10, 37, 608, DateTimeKind.Local).AddTicks(5877), "Steve.Ankunding@yahoo.com", "uc06B2JcCg3bGR3AYxgkaLXluzuHtlGhYpz7TZ1GRqQ=", "2JgW5bbuNqKeRsI0LT+0p7ubXVI6o5n7S4eIo7XolWc=", new DateTime(2022, 1, 12, 22, 10, 37, 608, DateTimeKind.Local).AddTicks(5901), "Eleanore74" }
                });

            migrationBuilder.InsertData(
                table: "Comments",
                columns: new[] { "Id", "AuthorId", "Body", "CreatedAt", "IsDeleted", "PostId", "UpdatedAt" },
                values: new object[,]
                {
                    { 16, 8, "Itaque aspernatur molestias aut voluptas.", new DateTime(2022, 1, 12, 22, 10, 37, 800, DateTimeKind.Local).AddTicks(9122), false, 9, new DateTime(2022, 1, 12, 22, 10, 37, 800, DateTimeKind.Local).AddTicks(9122) },
                    { 19, 4, "Asperiores ad voluptatem quod et consequuntur ea illum perspiciatis.", new DateTime(2022, 1, 12, 22, 10, 37, 800, DateTimeKind.Local).AddTicks(9122), false, 9, new DateTime(2022, 1, 12, 22, 10, 37, 800, DateTimeKind.Local).AddTicks(9122) }
                });

            migrationBuilder.InsertData(
                table: "PostReactions",
                columns: new[] { "Id", "CreatedAt", "IsLike", "PostId", "UpdatedAt", "UserId" },
                values: new object[,]
                {
                    { 3, new DateTime(2022, 1, 12, 22, 10, 37, 813, DateTimeKind.Local).AddTicks(6538), false, 9, new DateTime(2022, 1, 12, 22, 10, 37, 813, DateTimeKind.Local).AddTicks(6541), 20 },
                    { 18, new DateTime(2022, 1, 12, 22, 10, 37, 813, DateTimeKind.Local).AddTicks(6868), true, 3, new DateTime(2022, 1, 12, 22, 10, 37, 813, DateTimeKind.Local).AddTicks(6870), 5 },
                    { 12, new DateTime(2022, 1, 12, 22, 10, 37, 813, DateTimeKind.Local).AddTicks(6741), true, 3, new DateTime(2022, 1, 12, 22, 10, 37, 813, DateTimeKind.Local).AddTicks(6744), 13 },
                    { 4, new DateTime(2022, 1, 12, 22, 10, 37, 813, DateTimeKind.Local).AddTicks(6562), true, 3, new DateTime(2022, 1, 12, 22, 10, 37, 813, DateTimeKind.Local).AddTicks(6565), 16 }
                });

            migrationBuilder.InsertData(
                table: "Posts",
                columns: new[] { "Id", "AuthorId", "Body", "CreatedAt", "IsDeleted", "PreviewId", "UpdatedAt" },
                values: new object[,]
                {
                    { 17, 12, "qui", new DateTime(2022, 1, 12, 22, 10, 37, 783, DateTimeKind.Local).AddTicks(3365), false, 22, new DateTime(2022, 1, 12, 22, 10, 37, 783, DateTimeKind.Local).AddTicks(3365) },
                    { 14, 12, "Vero harum porro eligendi distinctio quo itaque sit.\nAliquam deleniti id voluptate iure minus.\nOmnis est quia porro alias earum ut.\nRepudiandae temporibus ut ut.\nPorro fugit aut velit quaerat.\nVoluptas occaecati ab ratione aut ut aliquam quis molestiae.", new DateTime(2022, 1, 12, 22, 10, 37, 783, DateTimeKind.Local).AddTicks(3365), false, 28, new DateTime(2022, 1, 12, 22, 10, 37, 783, DateTimeKind.Local).AddTicks(3365) },
                    { 20, 6, "Molestias non nostrum harum voluptas aut aut illo.\nEarum et doloremque nihil quae praesentium delectus.\nAut ipsa repellendus et hic voluptatem aliquid eius voluptatum.\nNon reiciendis voluptatibus hic nulla dicta sint natus officiis dolorem.", new DateTime(2022, 1, 12, 22, 10, 37, 783, DateTimeKind.Local).AddTicks(3365), false, 40, new DateTime(2022, 1, 12, 22, 10, 37, 783, DateTimeKind.Local).AddTicks(3365) },
                    { 2, 19, "Deserunt adipisci rerum provident deleniti optio quia.\nDeserunt hic cupiditate.\nAut accusamus itaque fuga ex facere tenetur vel.\nEnim eos cupiditate eveniet doloribus.\nVoluptas omnis odit sint est doloribus.", new DateTime(2022, 1, 12, 22, 10, 37, 783, DateTimeKind.Local).AddTicks(3365), false, 21, new DateTime(2022, 1, 12, 22, 10, 37, 783, DateTimeKind.Local).AddTicks(3365) },
                    { 5, 13, "et", new DateTime(2022, 1, 12, 22, 10, 37, 783, DateTimeKind.Local).AddTicks(3365), false, 37, new DateTime(2022, 1, 12, 22, 10, 37, 783, DateTimeKind.Local).AddTicks(3365) },
                    { 7, 1, "optio", new DateTime(2022, 1, 12, 22, 10, 37, 783, DateTimeKind.Local).AddTicks(3365), false, 31, new DateTime(2022, 1, 12, 22, 10, 37, 783, DateTimeKind.Local).AddTicks(3365) },
                    { 16, 16, "Ex dolorum sequi.\nExplicabo vel voluptatem sit est officiis.\nSunt dolorem velit at expedita.\nNihil quaerat repudiandae placeat officiis sint qui.\nEos incidunt aliquid minima voluptas ea vero.", new DateTime(2022, 1, 12, 22, 10, 37, 783, DateTimeKind.Local).AddTicks(3365), false, 31, new DateTime(2022, 1, 12, 22, 10, 37, 783, DateTimeKind.Local).AddTicks(3365) },
                    { 11, 15, "unde", new DateTime(2022, 1, 12, 22, 10, 37, 783, DateTimeKind.Local).AddTicks(3365), false, 21, new DateTime(2022, 1, 12, 22, 10, 37, 783, DateTimeKind.Local).AddTicks(3365) },
                    { 10, 15, "Labore officia sequi et.", new DateTime(2022, 1, 12, 22, 10, 37, 783, DateTimeKind.Local).AddTicks(3365), false, 31, new DateTime(2022, 1, 12, 22, 10, 37, 783, DateTimeKind.Local).AddTicks(3365) },
                    { 8, 15, "Dicta quia voluptatem quia id exercitationem.\nVoluptates facilis est quam ut et id.\nVoluptate qui commodi earum quaerat aut nesciunt eius.", new DateTime(2022, 1, 12, 22, 10, 37, 783, DateTimeKind.Local).AddTicks(3365), false, 40, new DateTime(2022, 1, 12, 22, 10, 37, 783, DateTimeKind.Local).AddTicks(3365) },
                    { 18, 10, "deserunt", new DateTime(2022, 1, 12, 22, 10, 37, 783, DateTimeKind.Local).AddTicks(3365), false, 40, new DateTime(2022, 1, 12, 22, 10, 37, 783, DateTimeKind.Local).AddTicks(3365) },
                    { 15, 9, "Dolorem ipsam necessitatibus voluptatum explicabo. Sint in qui omnis officiis accusamus deserunt. Distinctio eos autem consectetur voluptatem animi delectus architecto et. Nostrum atque placeat similique nemo possimus consequatur sit. Aut sequi ea tempora. Voluptatem harum officiis consequatur itaque enim ad perferendis.", new DateTime(2022, 1, 12, 22, 10, 37, 783, DateTimeKind.Local).AddTicks(3365), false, 33, new DateTime(2022, 1, 12, 22, 10, 37, 783, DateTimeKind.Local).AddTicks(3365) },
                    { 13, 14, "omnis", new DateTime(2022, 1, 12, 22, 10, 37, 783, DateTimeKind.Local).AddTicks(3365), false, 38, new DateTime(2022, 1, 12, 22, 10, 37, 783, DateTimeKind.Local).AddTicks(3365) },
                    { 12, 4, "Reprehenderit ut sapiente aperiam ut eum exercitationem aperiam dolores eum. Culpa saepe saepe odio eligendi. Doloribus et ut fugit qui vitae. Omnis facere modi rerum facilis in labore. Consequatur porro quia iusto facere sit vero voluptatem.", new DateTime(2022, 1, 12, 22, 10, 37, 783, DateTimeKind.Local).AddTicks(3365), false, 24, new DateTime(2022, 1, 12, 22, 10, 37, 783, DateTimeKind.Local).AddTicks(3365) },
                    { 1, 4, "ut", new DateTime(2022, 1, 12, 22, 10, 37, 783, DateTimeKind.Local).AddTicks(3365), false, 34, new DateTime(2022, 1, 12, 22, 10, 37, 783, DateTimeKind.Local).AddTicks(3365) },
                    { 19, 18, "Ad aut vero quam ea voluptatem est.\nOfficiis magnam atque odit nihil et.\nCulpa voluptates ut assumenda sint quos similique et in blanditiis.\nAb facilis libero hic corporis neque possimus.\nVoluptas accusantium quis non.", new DateTime(2022, 1, 12, 22, 10, 37, 783, DateTimeKind.Local).AddTicks(3365), false, 39, new DateTime(2022, 1, 12, 22, 10, 37, 783, DateTimeKind.Local).AddTicks(3365) },
                    { 6, 17, "Repellendus cupiditate debitis accusantium.\nIn quaerat perferendis repellendus similique corrupti culpa ut itaque.\nVoluptatibus eum sit non voluptas nihil vel.\nExcepturi dicta cupiditate rem aspernatur quibusdam eum saepe.", new DateTime(2022, 1, 12, 22, 10, 37, 783, DateTimeKind.Local).AddTicks(3365), false, 34, new DateTime(2022, 1, 12, 22, 10, 37, 783, DateTimeKind.Local).AddTicks(3365) },
                    { 4, 18, "Itaque eligendi modi debitis.", new DateTime(2022, 1, 12, 22, 10, 37, 783, DateTimeKind.Local).AddTicks(3365), false, 35, new DateTime(2022, 1, 12, 22, 10, 37, 783, DateTimeKind.Local).AddTicks(3365) }
                });

            migrationBuilder.InsertData(
                table: "CommentReactions",
                columns: new[] { "Id", "CommentId", "CreatedAt", "IsLike", "UpdatedAt", "UserId" },
                values: new object[] { 16, 19, new DateTime(2022, 1, 12, 22, 10, 37, 819, DateTimeKind.Local).AddTicks(9522), false, new DateTime(2022, 1, 12, 22, 10, 37, 819, DateTimeKind.Local).AddTicks(9524), 1 });

            migrationBuilder.InsertData(
                table: "Comments",
                columns: new[] { "Id", "AuthorId", "Body", "CreatedAt", "IsDeleted", "PostId", "UpdatedAt" },
                values: new object[,]
                {
                    { 20, 2, "Labore qui dolorem eaque sequi aperiam enim ipsa corporis.", new DateTime(2022, 1, 12, 22, 10, 37, 800, DateTimeKind.Local).AddTicks(9122), false, 17, new DateTime(2022, 1, 12, 22, 10, 37, 800, DateTimeKind.Local).AddTicks(9122) },
                    { 2, 4, "Neque nulla et dolorum qui iste quia earum esse nostrum.", new DateTime(2022, 1, 12, 22, 10, 37, 800, DateTimeKind.Local).AddTicks(9122), false, 17, new DateTime(2022, 1, 12, 22, 10, 37, 800, DateTimeKind.Local).AddTicks(9122) },
                    { 11, 5, "Et similique magnam optio quia enim.", new DateTime(2022, 1, 12, 22, 10, 37, 800, DateTimeKind.Local).AddTicks(9122), false, 20, new DateTime(2022, 1, 12, 22, 10, 37, 800, DateTimeKind.Local).AddTicks(9122) },
                    { 18, 11, "Temporibus et assumenda qui error et nisi.", new DateTime(2022, 1, 12, 22, 10, 37, 800, DateTimeKind.Local).AddTicks(9122), false, 2, new DateTime(2022, 1, 12, 22, 10, 37, 800, DateTimeKind.Local).AddTicks(9122) },
                    { 15, 12, "Eum iste nihil possimus.", new DateTime(2022, 1, 12, 22, 10, 37, 800, DateTimeKind.Local).AddTicks(9122), false, 5, new DateTime(2022, 1, 12, 22, 10, 37, 800, DateTimeKind.Local).AddTicks(9122) },
                    { 3, 20, "Error doloribus in.", new DateTime(2022, 1, 12, 22, 10, 37, 800, DateTimeKind.Local).AddTicks(9122), false, 16, new DateTime(2022, 1, 12, 22, 10, 37, 800, DateTimeKind.Local).AddTicks(9122) },
                    { 5, 21, "Quo ipsa at.", new DateTime(2022, 1, 12, 22, 10, 37, 800, DateTimeKind.Local).AddTicks(9122), false, 6, new DateTime(2022, 1, 12, 22, 10, 37, 800, DateTimeKind.Local).AddTicks(9122) },
                    { 17, 10, "Ratione delectus sint.", new DateTime(2022, 1, 12, 22, 10, 37, 800, DateTimeKind.Local).AddTicks(9122), false, 11, new DateTime(2022, 1, 12, 22, 10, 37, 800, DateTimeKind.Local).AddTicks(9122) },
                    { 12, 21, "Quisquam illum perferendis praesentium ut nulla blanditiis hic vitae.", new DateTime(2022, 1, 12, 22, 10, 37, 800, DateTimeKind.Local).AddTicks(9122), false, 10, new DateTime(2022, 1, 12, 22, 10, 37, 800, DateTimeKind.Local).AddTicks(9122) },
                    { 14, 11, "Dolore aut ea animi aut blanditiis consequatur eius.", new DateTime(2022, 1, 12, 22, 10, 37, 800, DateTimeKind.Local).AddTicks(9122), false, 8, new DateTime(2022, 1, 12, 22, 10, 37, 800, DateTimeKind.Local).AddTicks(9122) },
                    { 9, 3, "Quos cumque commodi odit.", new DateTime(2022, 1, 12, 22, 10, 37, 800, DateTimeKind.Local).AddTicks(9122), false, 8, new DateTime(2022, 1, 12, 22, 10, 37, 800, DateTimeKind.Local).AddTicks(9122) },
                    { 4, 1, "Officia quibusdam quibusdam est et consequuntur.", new DateTime(2022, 1, 12, 22, 10, 37, 800, DateTimeKind.Local).AddTicks(9122), false, 4, new DateTime(2022, 1, 12, 22, 10, 37, 800, DateTimeKind.Local).AddTicks(9122) },
                    { 6, 10, "Illum odit numquam.", new DateTime(2022, 1, 12, 22, 10, 37, 800, DateTimeKind.Local).AddTicks(9122), false, 4, new DateTime(2022, 1, 12, 22, 10, 37, 800, DateTimeKind.Local).AddTicks(9122) },
                    { 8, 8, "Qui adipisci incidunt fugit tempora quo quos voluptas ipsum.", new DateTime(2022, 1, 12, 22, 10, 37, 800, DateTimeKind.Local).AddTicks(9122), false, 4, new DateTime(2022, 1, 12, 22, 10, 37, 800, DateTimeKind.Local).AddTicks(9122) },
                    { 10, 11, "Numquam quas veritatis maxime rerum sit.", new DateTime(2022, 1, 12, 22, 10, 37, 800, DateTimeKind.Local).AddTicks(9122), false, 12, new DateTime(2022, 1, 12, 22, 10, 37, 800, DateTimeKind.Local).AddTicks(9122) },
                    { 1, 21, "Expedita saepe possimus modi excepturi quia fuga.", new DateTime(2022, 1, 12, 22, 10, 37, 800, DateTimeKind.Local).AddTicks(9122), false, 19, new DateTime(2022, 1, 12, 22, 10, 37, 800, DateTimeKind.Local).AddTicks(9122) },
                    { 7, 21, "Nam tempora unde.", new DateTime(2022, 1, 12, 22, 10, 37, 800, DateTimeKind.Local).AddTicks(9122), false, 8, new DateTime(2022, 1, 12, 22, 10, 37, 800, DateTimeKind.Local).AddTicks(9122) },
                    { 13, 8, "Iste quia quia omnis nesciunt aliquid omnis qui.", new DateTime(2022, 1, 12, 22, 10, 37, 800, DateTimeKind.Local).AddTicks(9122), false, 19, new DateTime(2022, 1, 12, 22, 10, 37, 800, DateTimeKind.Local).AddTicks(9122) }
                });

            migrationBuilder.InsertData(
                table: "PostReactions",
                columns: new[] { "Id", "CreatedAt", "IsLike", "PostId", "UpdatedAt", "UserId" },
                values: new object[,]
                {
                    { 5, new DateTime(2022, 1, 12, 22, 10, 37, 813, DateTimeKind.Local).AddTicks(6588), false, 15, new DateTime(2022, 1, 12, 22, 10, 37, 813, DateTimeKind.Local).AddTicks(6590), 1 },
                    { 16, new DateTime(2022, 1, 12, 22, 10, 37, 813, DateTimeKind.Local).AddTicks(6826), true, 20, new DateTime(2022, 1, 12, 22, 10, 37, 813, DateTimeKind.Local).AddTicks(6828), 13 },
                    { 2, new DateTime(2022, 1, 12, 22, 10, 37, 813, DateTimeKind.Local).AddTicks(6501), false, 20, new DateTime(2022, 1, 12, 22, 10, 37, 813, DateTimeKind.Local).AddTicks(6507), 7 },
                    { 10, new DateTime(2022, 1, 12, 22, 10, 37, 813, DateTimeKind.Local).AddTicks(6698), true, 4, new DateTime(2022, 1, 12, 22, 10, 37, 813, DateTimeKind.Local).AddTicks(6700), 7 },
                    { 13, new DateTime(2022, 1, 12, 22, 10, 37, 813, DateTimeKind.Local).AddTicks(6762), true, 2, new DateTime(2022, 1, 12, 22, 10, 37, 813, DateTimeKind.Local).AddTicks(6765), 10 },
                    { 14, new DateTime(2022, 1, 12, 22, 10, 37, 813, DateTimeKind.Local).AddTicks(6784), false, 5, new DateTime(2022, 1, 12, 22, 10, 37, 813, DateTimeKind.Local).AddTicks(6787), 12 },
                    { 11, new DateTime(2022, 1, 12, 22, 10, 37, 813, DateTimeKind.Local).AddTicks(6721), true, 19, new DateTime(2022, 1, 12, 22, 10, 37, 813, DateTimeKind.Local).AddTicks(6723), 10 },
                    { 20, new DateTime(2022, 1, 12, 22, 10, 37, 813, DateTimeKind.Local).AddTicks(6930), false, 1, new DateTime(2022, 1, 12, 22, 10, 37, 813, DateTimeKind.Local).AddTicks(6933), 1 },
                    { 9, new DateTime(2022, 1, 12, 22, 10, 37, 813, DateTimeKind.Local).AddTicks(6676), false, 11, new DateTime(2022, 1, 12, 22, 10, 37, 813, DateTimeKind.Local).AddTicks(6678), 6 },
                    { 15, new DateTime(2022, 1, 12, 22, 10, 37, 813, DateTimeKind.Local).AddTicks(6806), false, 10, new DateTime(2022, 1, 12, 22, 10, 37, 813, DateTimeKind.Local).AddTicks(6808), 17 },
                    { 8, new DateTime(2022, 1, 12, 22, 10, 37, 813, DateTimeKind.Local).AddTicks(6654), true, 17, new DateTime(2022, 1, 12, 22, 10, 37, 813, DateTimeKind.Local).AddTicks(6656), 21 },
                    { 6, new DateTime(2022, 1, 12, 22, 10, 37, 813, DateTimeKind.Local).AddTicks(6610), true, 12, new DateTime(2022, 1, 12, 22, 10, 37, 813, DateTimeKind.Local).AddTicks(6612), 1 },
                    { 7, new DateTime(2022, 1, 12, 22, 10, 37, 813, DateTimeKind.Local).AddTicks(6632), true, 8, new DateTime(2022, 1, 12, 22, 10, 37, 813, DateTimeKind.Local).AddTicks(6634), 19 },
                    { 19, new DateTime(2022, 1, 12, 22, 10, 37, 813, DateTimeKind.Local).AddTicks(6889), true, 13, new DateTime(2022, 1, 12, 22, 10, 37, 813, DateTimeKind.Local).AddTicks(6892), 13 },
                    { 17, new DateTime(2022, 1, 12, 22, 10, 37, 813, DateTimeKind.Local).AddTicks(6847), true, 6, new DateTime(2022, 1, 12, 22, 10, 37, 813, DateTimeKind.Local).AddTicks(6849), 13 },
                    { 1, new DateTime(2022, 1, 12, 22, 10, 37, 813, DateTimeKind.Local).AddTicks(5284), false, 10, new DateTime(2022, 1, 12, 22, 10, 37, 813, DateTimeKind.Local).AddTicks(5968), 13 }
                });

            migrationBuilder.InsertData(
                table: "CommentReactions",
                columns: new[] { "Id", "CommentId", "CreatedAt", "IsLike", "UpdatedAt", "UserId" },
                values: new object[,]
                {
                    { 9, 6, new DateTime(2022, 1, 12, 22, 10, 37, 819, DateTimeKind.Local).AddTicks(9346), false, new DateTime(2022, 1, 12, 22, 10, 37, 819, DateTimeKind.Local).AddTicks(9349), 12 },
                    { 6, 3, new DateTime(2022, 1, 12, 22, 10, 37, 819, DateTimeKind.Local).AddTicks(9281), true, new DateTime(2022, 1, 12, 22, 10, 37, 819, DateTimeKind.Local).AddTicks(9283), 4 },
                    { 19, 5, new DateTime(2022, 1, 12, 22, 10, 37, 819, DateTimeKind.Local).AddTicks(9583), true, new DateTime(2022, 1, 12, 22, 10, 37, 819, DateTimeKind.Local).AddTicks(9586), 20 },
                    { 15, 5, new DateTime(2022, 1, 12, 22, 10, 37, 819, DateTimeKind.Local).AddTicks(9499), false, new DateTime(2022, 1, 12, 22, 10, 37, 819, DateTimeKind.Local).AddTicks(9501), 19 },
                    { 13, 17, new DateTime(2022, 1, 12, 22, 10, 37, 819, DateTimeKind.Local).AddTicks(9455), false, new DateTime(2022, 1, 12, 22, 10, 37, 819, DateTimeKind.Local).AddTicks(9458), 15 },
                    { 12, 14, new DateTime(2022, 1, 12, 22, 10, 37, 819, DateTimeKind.Local).AddTicks(9434), true, new DateTime(2022, 1, 12, 22, 10, 37, 819, DateTimeKind.Local).AddTicks(9436), 9 },
                    { 7, 14, new DateTime(2022, 1, 12, 22, 10, 37, 819, DateTimeKind.Local).AddTicks(9303), true, new DateTime(2022, 1, 12, 22, 10, 37, 819, DateTimeKind.Local).AddTicks(9305), 13 },
                    { 20, 9, new DateTime(2022, 1, 12, 22, 10, 37, 819, DateTimeKind.Local).AddTicks(9604), false, new DateTime(2022, 1, 12, 22, 10, 37, 819, DateTimeKind.Local).AddTicks(9607), 9 },
                    { 8, 15, new DateTime(2022, 1, 12, 22, 10, 37, 819, DateTimeKind.Local).AddTicks(9325), false, new DateTime(2022, 1, 12, 22, 10, 37, 819, DateTimeKind.Local).AddTicks(9327), 14 },
                    { 18, 9, new DateTime(2022, 1, 12, 22, 10, 37, 819, DateTimeKind.Local).AddTicks(9562), true, new DateTime(2022, 1, 12, 22, 10, 37, 819, DateTimeKind.Local).AddTicks(9565), 7 },
                    { 11, 9, new DateTime(2022, 1, 12, 22, 10, 37, 819, DateTimeKind.Local).AddTicks(9413), false, new DateTime(2022, 1, 12, 22, 10, 37, 819, DateTimeKind.Local).AddTicks(9415), 10 },
                    { 3, 9, new DateTime(2022, 1, 12, 22, 10, 37, 819, DateTimeKind.Local).AddTicks(9211), true, new DateTime(2022, 1, 12, 22, 10, 37, 819, DateTimeKind.Local).AddTicks(9213), 3 },
                    { 5, 7, new DateTime(2022, 1, 12, 22, 10, 37, 819, DateTimeKind.Local).AddTicks(9258), true, new DateTime(2022, 1, 12, 22, 10, 37, 819, DateTimeKind.Local).AddTicks(9261), 6 },
                    { 2, 7, new DateTime(2022, 1, 12, 22, 10, 37, 819, DateTimeKind.Local).AddTicks(9171), true, new DateTime(2022, 1, 12, 22, 10, 37, 819, DateTimeKind.Local).AddTicks(9178), 21 },
                    { 14, 10, new DateTime(2022, 1, 12, 22, 10, 37, 819, DateTimeKind.Local).AddTicks(9477), true, new DateTime(2022, 1, 12, 22, 10, 37, 819, DateTimeKind.Local).AddTicks(9480), 19 },
                    { 1, 13, new DateTime(2022, 1, 12, 22, 10, 37, 819, DateTimeKind.Local).AddTicks(7989), true, new DateTime(2022, 1, 12, 22, 10, 37, 819, DateTimeKind.Local).AddTicks(8661), 19 },
                    { 10, 1, new DateTime(2022, 1, 12, 22, 10, 37, 819, DateTimeKind.Local).AddTicks(9389), false, new DateTime(2022, 1, 12, 22, 10, 37, 819, DateTimeKind.Local).AddTicks(9392), 1 },
                    { 17, 9, new DateTime(2022, 1, 12, 22, 10, 37, 819, DateTimeKind.Local).AddTicks(9542), false, new DateTime(2022, 1, 12, 22, 10, 37, 819, DateTimeKind.Local).AddTicks(9544), 6 },
                    { 4, 11, new DateTime(2022, 1, 12, 22, 10, 37, 819, DateTimeKind.Local).AddTicks(9235), false, new DateTime(2022, 1, 12, 22, 10, 37, 819, DateTimeKind.Local).AddTicks(9238), 7 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_CommentReactions_UserId",
                table: "CommentReactions",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Comments_AuthorId",
                table: "Comments",
                column: "AuthorId");

            migrationBuilder.CreateIndex(
                name: "IX_Comments_PostId",
                table: "Comments",
                column: "PostId");

            migrationBuilder.CreateIndex(
                name: "IX_PostReactions_UserId",
                table: "PostReactions",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Posts_AuthorId",
                table: "Posts",
                column: "AuthorId");

            migrationBuilder.CreateIndex(
                name: "IX_Posts_PreviewId",
                table: "Posts",
                column: "PreviewId");

            migrationBuilder.CreateIndex(
                name: "IX_RefreshTokens_UserId",
                table: "RefreshTokens",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Users_AvatarId",
                table: "Users",
                column: "AvatarId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CommentReactions");

            migrationBuilder.DropTable(
                name: "PostReactions");

            migrationBuilder.DropTable(
                name: "RefreshTokens");

            migrationBuilder.DropTable(
                name: "Comments");

            migrationBuilder.DropTable(
                name: "Posts");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "Images");
        }
    }
}
