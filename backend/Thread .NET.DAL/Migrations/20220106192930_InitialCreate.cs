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
                    { 1, new DateTime(2022, 1, 6, 22, 29, 29, 261, DateTimeKind.Local).AddTicks(2802), "https://cloudflare-ipfs.com/ipfs/Qmd3W5DuhgHirLHGVixi6V76LhCkZUz6pnFt5AJBiyvHye/avatar/1190.jpg", new DateTime(2022, 1, 6, 22, 29, 29, 261, DateTimeKind.Local).AddTicks(6878) },
                    { 23, new DateTime(2022, 1, 6, 22, 29, 29, 266, DateTimeKind.Local).AddTicks(5687), "https://picsum.photos/640/480/?image=873", new DateTime(2022, 1, 6, 22, 29, 29, 266, DateTimeKind.Local).AddTicks(5689) },
                    { 24, new DateTime(2022, 1, 6, 22, 29, 29, 266, DateTimeKind.Local).AddTicks(5704), "https://picsum.photos/640/480/?image=602", new DateTime(2022, 1, 6, 22, 29, 29, 266, DateTimeKind.Local).AddTicks(5706) },
                    { 25, new DateTime(2022, 1, 6, 22, 29, 29, 266, DateTimeKind.Local).AddTicks(5720), "https://picsum.photos/640/480/?image=365", new DateTime(2022, 1, 6, 22, 29, 29, 266, DateTimeKind.Local).AddTicks(5722) },
                    { 26, new DateTime(2022, 1, 6, 22, 29, 29, 266, DateTimeKind.Local).AddTicks(5736), "https://picsum.photos/640/480/?image=221", new DateTime(2022, 1, 6, 22, 29, 29, 266, DateTimeKind.Local).AddTicks(5739) },
                    { 27, new DateTime(2022, 1, 6, 22, 29, 29, 266, DateTimeKind.Local).AddTicks(5753), "https://picsum.photos/640/480/?image=366", new DateTime(2022, 1, 6, 22, 29, 29, 266, DateTimeKind.Local).AddTicks(5755) },
                    { 28, new DateTime(2022, 1, 6, 22, 29, 29, 266, DateTimeKind.Local).AddTicks(5768), "https://picsum.photos/640/480/?image=455", new DateTime(2022, 1, 6, 22, 29, 29, 266, DateTimeKind.Local).AddTicks(5770) },
                    { 29, new DateTime(2022, 1, 6, 22, 29, 29, 266, DateTimeKind.Local).AddTicks(5784), "https://picsum.photos/640/480/?image=504", new DateTime(2022, 1, 6, 22, 29, 29, 266, DateTimeKind.Local).AddTicks(5786) },
                    { 30, new DateTime(2022, 1, 6, 22, 29, 29, 266, DateTimeKind.Local).AddTicks(5799), "https://picsum.photos/640/480/?image=822", new DateTime(2022, 1, 6, 22, 29, 29, 266, DateTimeKind.Local).AddTicks(5801) },
                    { 31, new DateTime(2022, 1, 6, 22, 29, 29, 266, DateTimeKind.Local).AddTicks(5816), "https://picsum.photos/640/480/?image=1075", new DateTime(2022, 1, 6, 22, 29, 29, 266, DateTimeKind.Local).AddTicks(5818) },
                    { 32, new DateTime(2022, 1, 6, 22, 29, 29, 266, DateTimeKind.Local).AddTicks(5832), "https://picsum.photos/640/480/?image=380", new DateTime(2022, 1, 6, 22, 29, 29, 266, DateTimeKind.Local).AddTicks(5834) },
                    { 33, new DateTime(2022, 1, 6, 22, 29, 29, 266, DateTimeKind.Local).AddTicks(5907), "https://picsum.photos/640/480/?image=231", new DateTime(2022, 1, 6, 22, 29, 29, 266, DateTimeKind.Local).AddTicks(5910) },
                    { 34, new DateTime(2022, 1, 6, 22, 29, 29, 266, DateTimeKind.Local).AddTicks(5926), "https://picsum.photos/640/480/?image=1066", new DateTime(2022, 1, 6, 22, 29, 29, 266, DateTimeKind.Local).AddTicks(5928) },
                    { 35, new DateTime(2022, 1, 6, 22, 29, 29, 266, DateTimeKind.Local).AddTicks(5942), "https://picsum.photos/640/480/?image=614", new DateTime(2022, 1, 6, 22, 29, 29, 266, DateTimeKind.Local).AddTicks(5944) },
                    { 36, new DateTime(2022, 1, 6, 22, 29, 29, 266, DateTimeKind.Local).AddTicks(5958), "https://picsum.photos/640/480/?image=82", new DateTime(2022, 1, 6, 22, 29, 29, 266, DateTimeKind.Local).AddTicks(5961) },
                    { 37, new DateTime(2022, 1, 6, 22, 29, 29, 266, DateTimeKind.Local).AddTicks(5974), "https://picsum.photos/640/480/?image=1078", new DateTime(2022, 1, 6, 22, 29, 29, 266, DateTimeKind.Local).AddTicks(5977) },
                    { 38, new DateTime(2022, 1, 6, 22, 29, 29, 266, DateTimeKind.Local).AddTicks(5990), "https://picsum.photos/640/480/?image=336", new DateTime(2022, 1, 6, 22, 29, 29, 266, DateTimeKind.Local).AddTicks(5992) },
                    { 39, new DateTime(2022, 1, 6, 22, 29, 29, 266, DateTimeKind.Local).AddTicks(6006), "https://picsum.photos/640/480/?image=38", new DateTime(2022, 1, 6, 22, 29, 29, 266, DateTimeKind.Local).AddTicks(6008) },
                    { 22, new DateTime(2022, 1, 6, 22, 29, 29, 266, DateTimeKind.Local).AddTicks(5663), "https://picsum.photos/640/480/?image=555", new DateTime(2022, 1, 6, 22, 29, 29, 266, DateTimeKind.Local).AddTicks(5668) },
                    { 40, new DateTime(2022, 1, 6, 22, 29, 29, 266, DateTimeKind.Local).AddTicks(6022), "https://picsum.photos/640/480/?image=669", new DateTime(2022, 1, 6, 22, 29, 29, 266, DateTimeKind.Local).AddTicks(6024) },
                    { 21, new DateTime(2022, 1, 6, 22, 29, 29, 266, DateTimeKind.Local).AddTicks(4618), "https://picsum.photos/640/480/?image=107", new DateTime(2022, 1, 6, 22, 29, 29, 266, DateTimeKind.Local).AddTicks(5459) },
                    { 19, new DateTime(2022, 1, 6, 22, 29, 29, 261, DateTimeKind.Local).AddTicks(7743), "https://cloudflare-ipfs.com/ipfs/Qmd3W5DuhgHirLHGVixi6V76LhCkZUz6pnFt5AJBiyvHye/avatar/202.jpg", new DateTime(2022, 1, 6, 22, 29, 29, 261, DateTimeKind.Local).AddTicks(7746) },
                    { 2, new DateTime(2022, 1, 6, 22, 29, 29, 261, DateTimeKind.Local).AddTicks(7390), "https://cloudflare-ipfs.com/ipfs/Qmd3W5DuhgHirLHGVixi6V76LhCkZUz6pnFt5AJBiyvHye/avatar/25.jpg", new DateTime(2022, 1, 6, 22, 29, 29, 261, DateTimeKind.Local).AddTicks(7395) },
                    { 3, new DateTime(2022, 1, 6, 22, 29, 29, 261, DateTimeKind.Local).AddTicks(7418), "https://cloudflare-ipfs.com/ipfs/Qmd3W5DuhgHirLHGVixi6V76LhCkZUz6pnFt5AJBiyvHye/avatar/41.jpg", new DateTime(2022, 1, 6, 22, 29, 29, 261, DateTimeKind.Local).AddTicks(7420) },
                    { 4, new DateTime(2022, 1, 6, 22, 29, 29, 261, DateTimeKind.Local).AddTicks(7435), "https://cloudflare-ipfs.com/ipfs/Qmd3W5DuhgHirLHGVixi6V76LhCkZUz6pnFt5AJBiyvHye/avatar/551.jpg", new DateTime(2022, 1, 6, 22, 29, 29, 261, DateTimeKind.Local).AddTicks(7437) },
                    { 5, new DateTime(2022, 1, 6, 22, 29, 29, 261, DateTimeKind.Local).AddTicks(7451), "https://cloudflare-ipfs.com/ipfs/Qmd3W5DuhgHirLHGVixi6V76LhCkZUz6pnFt5AJBiyvHye/avatar/454.jpg", new DateTime(2022, 1, 6, 22, 29, 29, 261, DateTimeKind.Local).AddTicks(7453) },
                    { 6, new DateTime(2022, 1, 6, 22, 29, 29, 261, DateTimeKind.Local).AddTicks(7467), "https://cloudflare-ipfs.com/ipfs/Qmd3W5DuhgHirLHGVixi6V76LhCkZUz6pnFt5AJBiyvHye/avatar/384.jpg", new DateTime(2022, 1, 6, 22, 29, 29, 261, DateTimeKind.Local).AddTicks(7469) },
                    { 7, new DateTime(2022, 1, 6, 22, 29, 29, 261, DateTimeKind.Local).AddTicks(7482), "https://cloudflare-ipfs.com/ipfs/Qmd3W5DuhgHirLHGVixi6V76LhCkZUz6pnFt5AJBiyvHye/avatar/746.jpg", new DateTime(2022, 1, 6, 22, 29, 29, 261, DateTimeKind.Local).AddTicks(7485) },
                    { 8, new DateTime(2022, 1, 6, 22, 29, 29, 261, DateTimeKind.Local).AddTicks(7568), "https://cloudflare-ipfs.com/ipfs/Qmd3W5DuhgHirLHGVixi6V76LhCkZUz6pnFt5AJBiyvHye/avatar/164.jpg", new DateTime(2022, 1, 6, 22, 29, 29, 261, DateTimeKind.Local).AddTicks(7571) },
                    { 9, new DateTime(2022, 1, 6, 22, 29, 29, 261, DateTimeKind.Local).AddTicks(7587), "https://cloudflare-ipfs.com/ipfs/Qmd3W5DuhgHirLHGVixi6V76LhCkZUz6pnFt5AJBiyvHye/avatar/487.jpg", new DateTime(2022, 1, 6, 22, 29, 29, 261, DateTimeKind.Local).AddTicks(7590) },
                    { 10, new DateTime(2022, 1, 6, 22, 29, 29, 261, DateTimeKind.Local).AddTicks(7603), "https://cloudflare-ipfs.com/ipfs/Qmd3W5DuhgHirLHGVixi6V76LhCkZUz6pnFt5AJBiyvHye/avatar/921.jpg", new DateTime(2022, 1, 6, 22, 29, 29, 261, DateTimeKind.Local).AddTicks(7605) },
                    { 11, new DateTime(2022, 1, 6, 22, 29, 29, 261, DateTimeKind.Local).AddTicks(7619), "https://cloudflare-ipfs.com/ipfs/Qmd3W5DuhgHirLHGVixi6V76LhCkZUz6pnFt5AJBiyvHye/avatar/575.jpg", new DateTime(2022, 1, 6, 22, 29, 29, 261, DateTimeKind.Local).AddTicks(7621) },
                    { 12, new DateTime(2022, 1, 6, 22, 29, 29, 261, DateTimeKind.Local).AddTicks(7634), "https://cloudflare-ipfs.com/ipfs/Qmd3W5DuhgHirLHGVixi6V76LhCkZUz6pnFt5AJBiyvHye/avatar/824.jpg", new DateTime(2022, 1, 6, 22, 29, 29, 261, DateTimeKind.Local).AddTicks(7636) },
                    { 13, new DateTime(2022, 1, 6, 22, 29, 29, 261, DateTimeKind.Local).AddTicks(7649), "https://cloudflare-ipfs.com/ipfs/Qmd3W5DuhgHirLHGVixi6V76LhCkZUz6pnFt5AJBiyvHye/avatar/953.jpg", new DateTime(2022, 1, 6, 22, 29, 29, 261, DateTimeKind.Local).AddTicks(7651) },
                    { 14, new DateTime(2022, 1, 6, 22, 29, 29, 261, DateTimeKind.Local).AddTicks(7665), "https://cloudflare-ipfs.com/ipfs/Qmd3W5DuhgHirLHGVixi6V76LhCkZUz6pnFt5AJBiyvHye/avatar/958.jpg", new DateTime(2022, 1, 6, 22, 29, 29, 261, DateTimeKind.Local).AddTicks(7668) },
                    { 15, new DateTime(2022, 1, 6, 22, 29, 29, 261, DateTimeKind.Local).AddTicks(7681), "https://cloudflare-ipfs.com/ipfs/Qmd3W5DuhgHirLHGVixi6V76LhCkZUz6pnFt5AJBiyvHye/avatar/531.jpg", new DateTime(2022, 1, 6, 22, 29, 29, 261, DateTimeKind.Local).AddTicks(7683) },
                    { 16, new DateTime(2022, 1, 6, 22, 29, 29, 261, DateTimeKind.Local).AddTicks(7697), "https://cloudflare-ipfs.com/ipfs/Qmd3W5DuhgHirLHGVixi6V76LhCkZUz6pnFt5AJBiyvHye/avatar/1212.jpg", new DateTime(2022, 1, 6, 22, 29, 29, 261, DateTimeKind.Local).AddTicks(7700) },
                    { 17, new DateTime(2022, 1, 6, 22, 29, 29, 261, DateTimeKind.Local).AddTicks(7713), "https://cloudflare-ipfs.com/ipfs/Qmd3W5DuhgHirLHGVixi6V76LhCkZUz6pnFt5AJBiyvHye/avatar/563.jpg", new DateTime(2022, 1, 6, 22, 29, 29, 261, DateTimeKind.Local).AddTicks(7715) },
                    { 18, new DateTime(2022, 1, 6, 22, 29, 29, 261, DateTimeKind.Local).AddTicks(7728), "https://cloudflare-ipfs.com/ipfs/Qmd3W5DuhgHirLHGVixi6V76LhCkZUz6pnFt5AJBiyvHye/avatar/142.jpg", new DateTime(2022, 1, 6, 22, 29, 29, 261, DateTimeKind.Local).AddTicks(7730) },
                    { 20, new DateTime(2022, 1, 6, 22, 29, 29, 261, DateTimeKind.Local).AddTicks(7759), "https://cloudflare-ipfs.com/ipfs/Qmd3W5DuhgHirLHGVixi6V76LhCkZUz6pnFt5AJBiyvHye/avatar/7.jpg", new DateTime(2022, 1, 6, 22, 29, 29, 261, DateTimeKind.Local).AddTicks(7762) }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "AvatarId", "CreatedAt", "Email", "Password", "Salt", "UpdatedAt", "UserName" },
                values: new object[] { 21, null, new DateTime(2022, 1, 6, 22, 29, 29, 488, DateTimeKind.Local).AddTicks(3280), "test@gmail.com", "euKF1e5k2L0+r5FqMBKqdnatGeIMPac1SoFtz3IMgLY=", "F7R4eUMvgwNlLbc8LUs0YQuUREr4Meo9vGH++zpTqyA=", new DateTime(2022, 1, 6, 22, 29, 29, 488, DateTimeKind.Local).AddTicks(3280), "testUser" });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "AvatarId", "CreatedAt", "Email", "Password", "Salt", "UpdatedAt", "UserName" },
                values: new object[,]
                {
                    { 16, 3, new DateTime(2022, 1, 6, 22, 29, 29, 443, DateTimeKind.Local).AddTicks(1386), "Allan.Frami@hotmail.com", "kiWXKZxqtQWBkg2AI5yscRlrHjA52JH3T4a7UDXclUw=", "oSk9aBDUsqj/67iNVdA7iB4AkrmlYV1qdZanXbvE9Qw=", new DateTime(2022, 1, 6, 22, 29, 29, 443, DateTimeKind.Local).AddTicks(1401), "Sim_Pouros53" },
                    { 7, 18, new DateTime(2022, 1, 6, 22, 29, 29, 362, DateTimeKind.Local).AddTicks(1632), "Marlin.Kovacek@gmail.com", "ry11JxiHQnZyvY5oLsDJ3znaPoUE/JWQTRCssupCAz8=", "QvUXE+ga1LWp7U7MMjIT99Yjp7cS/R65Mr16Uyf/hF8=", new DateTime(2022, 1, 6, 22, 29, 29, 362, DateTimeKind.Local).AddTicks(1640), "Martin.Kassulke" },
                    { 2, 18, new DateTime(2022, 1, 6, 22, 29, 29, 317, DateTimeKind.Local).AddTicks(806), "Brandy35@gmail.com", "22DkLs2JpI6PDmqE30QPWF9KdyrmqtF8TjVieXWPoUM=", "qaJ/UsWmsq24k13SIfz7qg1lVz8LphH1OiNTNc2RH2I=", new DateTime(2022, 1, 6, 22, 29, 29, 317, DateTimeKind.Local).AddTicks(818), "Stanley.Predovic94" },
                    { 12, 17, new DateTime(2022, 1, 6, 22, 29, 29, 407, DateTimeKind.Local).AddTicks(2769), "Karson46@hotmail.com", "nHYdUsM97xG2fMaY5mQvsz2ToxXaX/FijWSPyQbZSHI=", "lFYC+S5yDj9JZHBfY2OB6OYidwfLk1Q7fcRAkZw+z6Q=", new DateTime(2022, 1, 6, 22, 29, 29, 407, DateTimeKind.Local).AddTicks(2785), "Gina91" },
                    { 5, 17, new DateTime(2022, 1, 6, 22, 29, 29, 343, DateTimeKind.Local).AddTicks(9516), "Delta7@yahoo.com", "EAFhDSGx62ANmqRIgFFkmTVy5px1e1/j+IivtlpXxkA=", "Dhcg7J1qfFkZx6qO5AixFuanhKaNSGwuvYdxIAUV0S8=", new DateTime(2022, 1, 6, 22, 29, 29, 343, DateTimeKind.Local).AddTicks(9522), "Tracey.Sipes69" },
                    { 19, 16, new DateTime(2022, 1, 6, 22, 29, 29, 470, DateTimeKind.Local).AddTicks(2374), "Pearlie_Emmerich@yahoo.com", "Q2AEusvZJxF58Y1Nmu4x4HMgSzA23kFCGfizSkl98c8=", "tFCXmw0nzEOZEVktkUK8nddRWdQnKu/8upqkti0HwJE=", new DateTime(2022, 1, 6, 22, 29, 29, 470, DateTimeKind.Local).AddTicks(2384), "Yvonne_Boehm79" },
                    { 17, 14, new DateTime(2022, 1, 6, 22, 29, 29, 452, DateTimeKind.Local).AddTicks(2127), "Miracle33@yahoo.com", "MQwReTBj4Ek/NYO/TrHVYPFq3OqARDCGbWYUWJeIcCw=", "EfjOY5oGnAX/qsSAKUzmIrYeC7LZaa4o16w59TtixpY=", new DateTime(2022, 1, 6, 22, 29, 29, 452, DateTimeKind.Local).AddTicks(2158), "Schuyler_MacGyver" },
                    { 9, 13, new DateTime(2022, 1, 6, 22, 29, 29, 380, DateTimeKind.Local).AddTicks(270), "Loraine_Kling@yahoo.com", "sSBKx6QQDNoIPHFXFb9VV56zaKvUPSsnBu8kR5+sz2U=", "0Lfl/ezGThkKYggWLv26D2eU8C85GVopAZ1F+qSbZic=", new DateTime(2022, 1, 6, 22, 29, 29, 380, DateTimeKind.Local).AddTicks(291), "Jovany_Weber45" },
                    { 11, 12, new DateTime(2022, 1, 6, 22, 29, 29, 398, DateTimeKind.Local).AddTicks(2860), "Kelley56@yahoo.com", "esiputRvWweTL+Cj5VmRdn+PJupfHD5QvTAX009xXwc=", "2q6Yo44pEEalZCaLB3I1zAgHljPbrdf9cJGUTAStUOY=", new DateTime(2022, 1, 6, 22, 29, 29, 398, DateTimeKind.Local).AddTicks(2880), "Jeremie83" },
                    { 1, 12, new DateTime(2022, 1, 6, 22, 29, 29, 307, DateTimeKind.Local).AddTicks(9170), "Colton.Ebert39@yahoo.com", "cDsOmgj4ds1N44BwNaLM2RjdWodDupmJSXU43YuxICw=", "o5gl1CvSx7RNu/jWLjyHIWwEI/MKPlR025qGA3KLdcY=", new DateTime(2022, 1, 6, 22, 29, 29, 307, DateTimeKind.Local).AddTicks(9774), "Hailee.Dooley26" },
                    { 6, 11, new DateTime(2022, 1, 6, 22, 29, 29, 353, DateTimeKind.Local).AddTicks(1939), "Kody.Sanford@yahoo.com", "0zmr0YL4Mky1N1rXC/aXjmIIbwxPmGoWasAqfyYSIo4=", "/KmZFQ0b5s2P7avW8QAEkyrNwv2s7J3GfVqiH6hbWd0=", new DateTime(2022, 1, 6, 22, 29, 29, 353, DateTimeKind.Local).AddTicks(1969), "Randal18" },
                    { 15, 10, new DateTime(2022, 1, 6, 22, 29, 29, 434, DateTimeKind.Local).AddTicks(1220), "Sylvester53@gmail.com", "jQyQisUk2/hw62xEbHWivhi85R6MCGIpOcMtDurR3S4=", "gUBwOpESL4Yp/TbyNvI1jEQJR4ADkumGdU7vh3tnRr8=", new DateTime(2022, 1, 6, 22, 29, 29, 434, DateTimeKind.Local).AddTicks(1236), "Elissa45" },
                    { 4, 10, new DateTime(2022, 1, 6, 22, 29, 29, 335, DateTimeKind.Local).AddTicks(185), "Billie.Keeling@yahoo.com", "uguy29JmfmdjAQWEBDO7nClSGFaexeVE66SCusl1UKg=", "cWdY35pLbvmX5msJYgbya8tQzhgR4cJwIuumSEGp5A0=", new DateTime(2022, 1, 6, 22, 29, 29, 335, DateTimeKind.Local).AddTicks(199), "Jordi99" },
                    { 3, 8, new DateTime(2022, 1, 6, 22, 29, 29, 326, DateTimeKind.Local).AddTicks(239), "Clare84@yahoo.com", "sAzhv0moj8XK+77IeGTe2arAw0829QPlmMGyjl7h3bQ=", "hVIaPbeTfw64UoDnXWfUSFd5Ln0lJj1iu+iBPKnDK28=", new DateTime(2022, 1, 6, 22, 29, 29, 326, DateTimeKind.Local).AddTicks(253), "Justyn74" },
                    { 20, 7, new DateTime(2022, 1, 6, 22, 29, 29, 479, DateTimeKind.Local).AddTicks(2076), "Destinee95@gmail.com", "J0luUdJ9gSsD57Cj37v7MP/C12tbTPgJYfLxj6z11ys=", "RoOXhZVvbQVHNlxmYAqzgf/D/0T2T/ezaFWEWGySlWM=", new DateTime(2022, 1, 6, 22, 29, 29, 479, DateTimeKind.Local).AddTicks(2090), "Colt_Schaefer" },
                    { 18, 6, new DateTime(2022, 1, 6, 22, 29, 29, 461, DateTimeKind.Local).AddTicks(2722), "Ora90@yahoo.com", "Kjdfzrz9zJTqhr27sMsvYdeOtyOOjgc0GRgt7vgq8O8=", "fRfKmMd6XKPHgyFCzFQv5BFj6ovN7MlbVEBlugbivRo=", new DateTime(2022, 1, 6, 22, 29, 29, 461, DateTimeKind.Local).AddTicks(2736), "Bulah.Von87" },
                    { 13, 5, new DateTime(2022, 1, 6, 22, 29, 29, 416, DateTimeKind.Local).AddTicks(2067), "Genesis2@hotmail.com", "ReWcPJkCvzUjh3HlEJgqORY/NZZrwwfNDrb8TcuxrKo=", "zAN+2Bbpe849RAD8Ux1JE8m0NCerXb4ChIUTMyqxT14=", new DateTime(2022, 1, 6, 22, 29, 29, 416, DateTimeKind.Local).AddTicks(2076), "Karelle_Dare46" },
                    { 10, 5, new DateTime(2022, 1, 6, 22, 29, 29, 389, DateTimeKind.Local).AddTicks(134), "Joey_Kohler35@gmail.com", "pOd0I8BGjcWJxJlnpbjpchEl6HEFLwrVK/i5cuQUryA=", "MROupjN3X8ZNeo7VkBHCTmvnVEgT76MfnQhNrGGnvUY=", new DateTime(2022, 1, 6, 22, 29, 29, 389, DateTimeKind.Local).AddTicks(141), "Darius_Williamson36" },
                    { 8, 18, new DateTime(2022, 1, 6, 22, 29, 29, 371, DateTimeKind.Local).AddTicks(724), "Deven2@yahoo.com", "V6o+BFP4mGJQJYFlmV/QqlfL6z16YfqWpHYla6Bj74Y=", "xJTOzFubJszlu6/LF2jm6Tdzl3xvFYnUA7hKUBZ5fJc=", new DateTime(2022, 1, 6, 22, 29, 29, 371, DateTimeKind.Local).AddTicks(730), "Miguel57" },
                    { 14, 18, new DateTime(2022, 1, 6, 22, 29, 29, 425, DateTimeKind.Local).AddTicks(1861), "Ayla_Willms85@hotmail.com", "X96paDWt4gG3OHqoZFtCnIzgK+4zAHiHQNPTM0XyA9Y=", "g0iyBet+Je7qkQWaMwh37fgd1/x0Z1uja/qq8rf8Fng=", new DateTime(2022, 1, 6, 22, 29, 29, 425, DateTimeKind.Local).AddTicks(1869), "Modesto48" }
                });

            migrationBuilder.InsertData(
                table: "Posts",
                columns: new[] { "Id", "AuthorId", "Body", "CreatedAt", "IsDeleted", "PreviewId", "UpdatedAt" },
                values: new object[,]
                {
                    { 8, 16, "Mollitia dolorem libero et veniam et molestias nihil. Incidunt iste nesciunt velit repudiandae est sint harum aperiam consequuntur. Quia similique porro laudantium quo quibusdam.", new DateTime(2022, 1, 6, 22, 29, 29, 489, DateTimeKind.Local).AddTicks(4488), false, 23, new DateTime(2022, 1, 6, 22, 29, 29, 489, DateTimeKind.Local).AddTicks(4488) },
                    { 4, 14, "Aliquam consectetur et sunt. Veniam esse quia sit corporis dolore nam quasi quam. Necessitatibus et placeat soluta. Laudantium ducimus quasi qui.", new DateTime(2022, 1, 6, 22, 29, 29, 489, DateTimeKind.Local).AddTicks(4488), false, 21, new DateTime(2022, 1, 6, 22, 29, 29, 489, DateTimeKind.Local).AddTicks(4488) },
                    { 13, 8, "Perspiciatis ut laborum quaerat.\nQuos omnis illo eos aut qui nisi provident eos minus.\nConsequatur dolorem voluptatem magni doloribus autem possimus.\nSed ea et dolores.\nMinus itaque quidem perspiciatis omnis.", new DateTime(2022, 1, 6, 22, 29, 29, 489, DateTimeKind.Local).AddTicks(4488), false, 29, new DateTime(2022, 1, 6, 22, 29, 29, 489, DateTimeKind.Local).AddTicks(4488) },
                    { 16, 7, "Minima hic cupiditate laboriosam excepturi quo.\nDistinctio in natus reprehenderit eius voluptatibus omnis ut quam.\nQuo aut eos consequatur et.", new DateTime(2022, 1, 6, 22, 29, 29, 489, DateTimeKind.Local).AddTicks(4488), false, 27, new DateTime(2022, 1, 6, 22, 29, 29, 489, DateTimeKind.Local).AddTicks(4488) },
                    { 15, 7, "Officia cumque quia aut at velit voluptatem occaecati. Quia odit et id alias. Repellat voluptas vel aut amet recusandae a.", new DateTime(2022, 1, 6, 22, 29, 29, 489, DateTimeKind.Local).AddTicks(4488), false, 29, new DateTime(2022, 1, 6, 22, 29, 29, 489, DateTimeKind.Local).AddTicks(4488) },
                    { 2, 2, "Enim et eaque qui consequatur harum quod.\nSit aut quae aut vitae adipisci amet.\nVoluptates sit consequatur.\nMolestiae et eum voluptatibus autem aut et quidem.\nAut aspernatur assumenda enim.", new DateTime(2022, 1, 6, 22, 29, 29, 489, DateTimeKind.Local).AddTicks(4488), false, 39, new DateTime(2022, 1, 6, 22, 29, 29, 489, DateTimeKind.Local).AddTicks(4488) },
                    { 14, 19, "Magnam necessitatibus nobis.\nRerum adipisci maiores est et quibusdam dolore quisquam.\nOdio occaecati aliquam.\nNon laudantium illo deleniti.\nMinima nobis ea quasi sit.\nPossimus optio qui rerum accusantium quod aut labore nemo.", new DateTime(2022, 1, 6, 22, 29, 29, 489, DateTimeKind.Local).AddTicks(4488), false, 33, new DateTime(2022, 1, 6, 22, 29, 29, 489, DateTimeKind.Local).AddTicks(4488) },
                    { 3, 19, "Autem exercitationem at perferendis molestias ipsum est. Id voluptatem officiis sint totam est placeat. Nobis ea eos illo ut et reprehenderit odit suscipit ullam. Minima sed voluptatem consequuntur magnam fuga voluptas modi.", new DateTime(2022, 1, 6, 22, 29, 29, 489, DateTimeKind.Local).AddTicks(4488), false, 31, new DateTime(2022, 1, 6, 22, 29, 29, 489, DateTimeKind.Local).AddTicks(4488) },
                    { 19, 17, "veniam", new DateTime(2022, 1, 6, 22, 29, 29, 489, DateTimeKind.Local).AddTicks(4488), false, 32, new DateTime(2022, 1, 6, 22, 29, 29, 489, DateTimeKind.Local).AddTicks(4488) },
                    { 17, 17, "earum", new DateTime(2022, 1, 6, 22, 29, 29, 489, DateTimeKind.Local).AddTicks(4488), false, 21, new DateTime(2022, 1, 6, 22, 29, 29, 489, DateTimeKind.Local).AddTicks(4488) },
                    { 1, 17, "sit", new DateTime(2022, 1, 6, 22, 29, 29, 489, DateTimeKind.Local).AddTicks(4488), false, 28, new DateTime(2022, 1, 6, 22, 29, 29, 489, DateTimeKind.Local).AddTicks(4488) },
                    { 9, 9, "Consequatur et quis.\nQuasi labore autem possimus vero quisquam dolores nesciunt autem inventore.\nQuo cupiditate sed dolore tenetur.\nQuisquam et quas doloremque aut sed et atque iure temporibus.", new DateTime(2022, 1, 6, 22, 29, 29, 489, DateTimeKind.Local).AddTicks(4488), false, 21, new DateTime(2022, 1, 6, 22, 29, 29, 489, DateTimeKind.Local).AddTicks(4488) },
                    { 11, 11, "Quaerat minima enim expedita quo. Laboriosam voluptate in a officia animi. Perspiciatis ipsa est doloribus dolor molestiae. Aperiam totam enim totam non totam facere quia accusamus est. Natus laudantium ut optio nihil qui ut autem dolorum est.", new DateTime(2022, 1, 6, 22, 29, 29, 489, DateTimeKind.Local).AddTicks(4488), false, 36, new DateTime(2022, 1, 6, 22, 29, 29, 489, DateTimeKind.Local).AddTicks(4488) },
                    { 7, 1, "Quae quasi qui.\nAd est repellendus adipisci totam dolorum quam dolorum architecto atque.\nEa et quia beatae consectetur quo unde.\nOdio dicta quae eius fuga asperiores omnis.\nEt doloribus ipsa ea tempora velit veritatis est laborum voluptatum.\nIusto voluptas omnis.", new DateTime(2022, 1, 6, 22, 29, 29, 489, DateTimeKind.Local).AddTicks(4488), false, 32, new DateTime(2022, 1, 6, 22, 29, 29, 489, DateTimeKind.Local).AddTicks(4488) },
                    { 6, 4, "Ullam nisi illo rerum eum quaerat. Et laudantium sit quia. Suscipit laborum fugit.", new DateTime(2022, 1, 6, 22, 29, 29, 489, DateTimeKind.Local).AddTicks(4488), false, 25, new DateTime(2022, 1, 6, 22, 29, 29, 489, DateTimeKind.Local).AddTicks(4488) },
                    { 12, 20, "Mollitia vero nesciunt ut.\nMagni aperiam cumque et temporibus consequatur et.\nDolore et neque sed fuga aperiam accusantium dolores.\nPariatur laboriosam adipisci sit doloribus corporis.\nLabore sapiente maxime libero.\nAccusamus eveniet perferendis sit modi autem non voluptas.", new DateTime(2022, 1, 6, 22, 29, 29, 489, DateTimeKind.Local).AddTicks(4488), false, 35, new DateTime(2022, 1, 6, 22, 29, 29, 489, DateTimeKind.Local).AddTicks(4488) },
                    { 10, 20, "amet", new DateTime(2022, 1, 6, 22, 29, 29, 489, DateTimeKind.Local).AddTicks(4488), false, 31, new DateTime(2022, 1, 6, 22, 29, 29, 489, DateTimeKind.Local).AddTicks(4488) },
                    { 20, 16, "Quod beatae impedit non aperiam. Nam corrupti labore sit ullam libero deserunt accusamus. Praesentium repudiandae dolorum dicta sunt ipsum.", new DateTime(2022, 1, 6, 22, 29, 29, 489, DateTimeKind.Local).AddTicks(4488), false, 28, new DateTime(2022, 1, 6, 22, 29, 29, 489, DateTimeKind.Local).AddTicks(4488) },
                    { 5, 14, "Omnis ut odit omnis.", new DateTime(2022, 1, 6, 22, 29, 29, 489, DateTimeKind.Local).AddTicks(4488), false, 21, new DateTime(2022, 1, 6, 22, 29, 29, 489, DateTimeKind.Local).AddTicks(4488) },
                    { 18, 14, "Ipsam molestias est maxime sunt facere repellendus mollitia vitae.\nLaboriosam eaque odio recusandae mollitia accusantium voluptate optio ut.\nLaudantium deserunt sed quam.", new DateTime(2022, 1, 6, 22, 29, 29, 489, DateTimeKind.Local).AddTicks(4488), false, 34, new DateTime(2022, 1, 6, 22, 29, 29, 489, DateTimeKind.Local).AddTicks(4488) }
                });

            migrationBuilder.InsertData(
                table: "Comments",
                columns: new[] { "Id", "AuthorId", "Body", "CreatedAt", "PostId", "UpdatedAt" },
                values: new object[,]
                {
                    { 7, 21, "Est cum quis dicta.", new DateTime(2022, 1, 6, 22, 29, 29, 504, DateTimeKind.Local).AddTicks(2106), 8, new DateTime(2022, 1, 6, 22, 29, 29, 504, DateTimeKind.Local).AddTicks(2106) },
                    { 2, 14, "Enim omnis optio vero rerum illum autem aperiam officiis.", new DateTime(2022, 1, 6, 22, 29, 29, 504, DateTimeKind.Local).AddTicks(2106), 5, new DateTime(2022, 1, 6, 22, 29, 29, 504, DateTimeKind.Local).AddTicks(2106) },
                    { 14, 6, "Ex laboriosam quia ipsum reiciendis esse illo maxime.", new DateTime(2022, 1, 6, 22, 29, 29, 504, DateTimeKind.Local).AddTicks(2106), 4, new DateTime(2022, 1, 6, 22, 29, 29, 504, DateTimeKind.Local).AddTicks(2106) },
                    { 20, 14, "Fuga molestiae soluta reprehenderit blanditiis ratione.", new DateTime(2022, 1, 6, 22, 29, 29, 504, DateTimeKind.Local).AddTicks(2106), 13, new DateTime(2022, 1, 6, 22, 29, 29, 504, DateTimeKind.Local).AddTicks(2106) },
                    { 15, 2, "Magni dicta magnam est qui aut.", new DateTime(2022, 1, 6, 22, 29, 29, 504, DateTimeKind.Local).AddTicks(2106), 2, new DateTime(2022, 1, 6, 22, 29, 29, 504, DateTimeKind.Local).AddTicks(2106) },
                    { 5, 6, "Eum ipsum non in aspernatur.", new DateTime(2022, 1, 6, 22, 29, 29, 504, DateTimeKind.Local).AddTicks(2106), 2, new DateTime(2022, 1, 6, 22, 29, 29, 504, DateTimeKind.Local).AddTicks(2106) },
                    { 12, 3, "Fugit quia rerum velit aut unde et molestiae vel ratione.", new DateTime(2022, 1, 6, 22, 29, 29, 504, DateTimeKind.Local).AddTicks(2106), 14, new DateTime(2022, 1, 6, 22, 29, 29, 504, DateTimeKind.Local).AddTicks(2106) },
                    { 10, 7, "Sunt aliquam quia repellat voluptatem et qui qui.", new DateTime(2022, 1, 6, 22, 29, 29, 504, DateTimeKind.Local).AddTicks(2106), 17, new DateTime(2022, 1, 6, 22, 29, 29, 504, DateTimeKind.Local).AddTicks(2106) },
                    { 9, 20, "In tempora id exercitationem dolore natus.", new DateTime(2022, 1, 6, 22, 29, 29, 504, DateTimeKind.Local).AddTicks(2106), 17, new DateTime(2022, 1, 6, 22, 29, 29, 504, DateTimeKind.Local).AddTicks(2106) },
                    { 3, 4, "Ut facere inventore omnis qui qui expedita.", new DateTime(2022, 1, 6, 22, 29, 29, 504, DateTimeKind.Local).AddTicks(2106), 18, new DateTime(2022, 1, 6, 22, 29, 29, 504, DateTimeKind.Local).AddTicks(2106) },
                    { 6, 13, "Provident magni voluptatem ea.", new DateTime(2022, 1, 6, 22, 29, 29, 504, DateTimeKind.Local).AddTicks(2106), 1, new DateTime(2022, 1, 6, 22, 29, 29, 504, DateTimeKind.Local).AddTicks(2106) },
                    { 19, 20, "Facere et dolorem ad qui sit doloremque ea in.", new DateTime(2022, 1, 6, 22, 29, 29, 504, DateTimeKind.Local).AddTicks(2106), 9, new DateTime(2022, 1, 6, 22, 29, 29, 504, DateTimeKind.Local).AddTicks(2106) },
                    { 4, 16, "Aut iure ex ea eos earum id sed doloribus non.", new DateTime(2022, 1, 6, 22, 29, 29, 504, DateTimeKind.Local).AddTicks(2106), 9, new DateTime(2022, 1, 6, 22, 29, 29, 504, DateTimeKind.Local).AddTicks(2106) },
                    { 16, 1, "Aperiam delectus aut.", new DateTime(2022, 1, 6, 22, 29, 29, 504, DateTimeKind.Local).AddTicks(2106), 11, new DateTime(2022, 1, 6, 22, 29, 29, 504, DateTimeKind.Local).AddTicks(2106) },
                    { 8, 1, "Enim et atque tenetur iusto ullam facere est.", new DateTime(2022, 1, 6, 22, 29, 29, 504, DateTimeKind.Local).AddTicks(2106), 17, new DateTime(2022, 1, 6, 22, 29, 29, 504, DateTimeKind.Local).AddTicks(2106) },
                    { 18, 4, "Aut perspiciatis earum.", new DateTime(2022, 1, 6, 22, 29, 29, 504, DateTimeKind.Local).AddTicks(2106), 20, new DateTime(2022, 1, 6, 22, 29, 29, 504, DateTimeKind.Local).AddTicks(2106) },
                    { 17, 10, "Voluptas dicta aut id est.", new DateTime(2022, 1, 6, 22, 29, 29, 504, DateTimeKind.Local).AddTicks(2106), 6, new DateTime(2022, 1, 6, 22, 29, 29, 504, DateTimeKind.Local).AddTicks(2106) },
                    { 1, 1, "Aliquid explicabo non perspiciatis et.", new DateTime(2022, 1, 6, 22, 29, 29, 504, DateTimeKind.Local).AddTicks(2106), 6, new DateTime(2022, 1, 6, 22, 29, 29, 504, DateTimeKind.Local).AddTicks(2106) },
                    { 11, 16, "Atque voluptatibus quia vero doloremque sunt qui consequatur.", new DateTime(2022, 1, 6, 22, 29, 29, 504, DateTimeKind.Local).AddTicks(2106), 10, new DateTime(2022, 1, 6, 22, 29, 29, 504, DateTimeKind.Local).AddTicks(2106) },
                    { 13, 15, "Illo exercitationem atque.", new DateTime(2022, 1, 6, 22, 29, 29, 504, DateTimeKind.Local).AddTicks(2106), 11, new DateTime(2022, 1, 6, 22, 29, 29, 504, DateTimeKind.Local).AddTicks(2106) }
                });

            migrationBuilder.InsertData(
                table: "PostReactions",
                columns: new[] { "Id", "CreatedAt", "IsLike", "PostId", "UpdatedAt", "UserId" },
                values: new object[,]
                {
                    { 16, new DateTime(2022, 1, 6, 22, 29, 29, 523, DateTimeKind.Local).AddTicks(1564), false, 7, new DateTime(2022, 1, 6, 22, 29, 29, 523, DateTimeKind.Local).AddTicks(1566), 8 },
                    { 20, new DateTime(2022, 1, 6, 22, 29, 29, 523, DateTimeKind.Local).AddTicks(1641), true, 8, new DateTime(2022, 1, 6, 22, 29, 29, 523, DateTimeKind.Local).AddTicks(1643), 5 },
                    { 18, new DateTime(2022, 1, 6, 22, 29, 29, 523, DateTimeKind.Local).AddTicks(1603), false, 4, new DateTime(2022, 1, 6, 22, 29, 29, 523, DateTimeKind.Local).AddTicks(1605), 11 },
                    { 6, new DateTime(2022, 1, 6, 22, 29, 29, 523, DateTimeKind.Local).AddTicks(1368), true, 13, new DateTime(2022, 1, 6, 22, 29, 29, 523, DateTimeKind.Local).AddTicks(1370), 17 },
                    { 2, new DateTime(2022, 1, 6, 22, 29, 29, 523, DateTimeKind.Local).AddTicks(1274), false, 20, new DateTime(2022, 1, 6, 22, 29, 29, 523, DateTimeKind.Local).AddTicks(1279), 12 },
                    { 12, new DateTime(2022, 1, 6, 22, 29, 29, 523, DateTimeKind.Local).AddTicks(1489), true, 16, new DateTime(2022, 1, 6, 22, 29, 29, 523, DateTimeKind.Local).AddTicks(1492), 15 },
                    { 15, new DateTime(2022, 1, 6, 22, 29, 29, 523, DateTimeKind.Local).AddTicks(1545), true, 15, new DateTime(2022, 1, 6, 22, 29, 29, 523, DateTimeKind.Local).AddTicks(1548), 6 },
                    { 13, new DateTime(2022, 1, 6, 22, 29, 29, 523, DateTimeKind.Local).AddTicks(1508), false, 15, new DateTime(2022, 1, 6, 22, 29, 29, 523, DateTimeKind.Local).AddTicks(1510), 14 },
                    { 10, new DateTime(2022, 1, 6, 22, 29, 29, 523, DateTimeKind.Local).AddTicks(1450), false, 15, new DateTime(2022, 1, 6, 22, 29, 29, 523, DateTimeKind.Local).AddTicks(1452), 8 },
                    { 14, new DateTime(2022, 1, 6, 22, 29, 29, 523, DateTimeKind.Local).AddTicks(1526), true, 20, new DateTime(2022, 1, 6, 22, 29, 29, 523, DateTimeKind.Local).AddTicks(1528), 4 },
                    { 11, new DateTime(2022, 1, 6, 22, 29, 29, 523, DateTimeKind.Local).AddTicks(1469), true, 10, new DateTime(2022, 1, 6, 22, 29, 29, 523, DateTimeKind.Local).AddTicks(1471), 17 },
                    { 3, new DateTime(2022, 1, 6, 22, 29, 29, 523, DateTimeKind.Local).AddTicks(1306), false, 3, new DateTime(2022, 1, 6, 22, 29, 29, 523, DateTimeKind.Local).AddTicks(1309), 2 },
                    { 1, new DateTime(2022, 1, 6, 22, 29, 29, 523, DateTimeKind.Local).AddTicks(228), true, 19, new DateTime(2022, 1, 6, 22, 29, 29, 523, DateTimeKind.Local).AddTicks(797), 21 },
                    { 9, new DateTime(2022, 1, 6, 22, 29, 29, 523, DateTimeKind.Local).AddTicks(1431), true, 17, new DateTime(2022, 1, 6, 22, 29, 29, 523, DateTimeKind.Local).AddTicks(1433), 4 },
                    { 17, new DateTime(2022, 1, 6, 22, 29, 29, 523, DateTimeKind.Local).AddTicks(1584), true, 12, new DateTime(2022, 1, 6, 22, 29, 29, 523, DateTimeKind.Local).AddTicks(1586), 10 },
                    { 19, new DateTime(2022, 1, 6, 22, 29, 29, 523, DateTimeKind.Local).AddTicks(1622), true, 6, new DateTime(2022, 1, 6, 22, 29, 29, 523, DateTimeKind.Local).AddTicks(1624), 4 },
                    { 5, new DateTime(2022, 1, 6, 22, 29, 29, 523, DateTimeKind.Local).AddTicks(1348), true, 7, new DateTime(2022, 1, 6, 22, 29, 29, 523, DateTimeKind.Local).AddTicks(1350), 6 },
                    { 7, new DateTime(2022, 1, 6, 22, 29, 29, 523, DateTimeKind.Local).AddTicks(1388), false, 7, new DateTime(2022, 1, 6, 22, 29, 29, 523, DateTimeKind.Local).AddTicks(1390), 11 },
                    { 4, new DateTime(2022, 1, 6, 22, 29, 29, 523, DateTimeKind.Local).AddTicks(1328), true, 15, new DateTime(2022, 1, 6, 22, 29, 29, 523, DateTimeKind.Local).AddTicks(1330), 5 },
                    { 8, new DateTime(2022, 1, 6, 22, 29, 29, 523, DateTimeKind.Local).AddTicks(1411), false, 18, new DateTime(2022, 1, 6, 22, 29, 29, 523, DateTimeKind.Local).AddTicks(1413), 15 }
                });

            migrationBuilder.InsertData(
                table: "CommentReactions",
                columns: new[] { "Id", "CommentId", "CreatedAt", "IsLike", "UpdatedAt", "UserId" },
                values: new object[,]
                {
                    { 5, 7, new DateTime(2022, 1, 6, 22, 29, 29, 527, DateTimeKind.Local).AddTicks(4294), false, new DateTime(2022, 1, 6, 22, 29, 29, 527, DateTimeKind.Local).AddTicks(4296), 5 },
                    { 19, 5, new DateTime(2022, 1, 6, 22, 29, 29, 527, DateTimeKind.Local).AddTicks(4566), false, new DateTime(2022, 1, 6, 22, 29, 29, 527, DateTimeKind.Local).AddTicks(4569), 14 },
                    { 18, 12, new DateTime(2022, 1, 6, 22, 29, 29, 527, DateTimeKind.Local).AddTicks(4540), false, new DateTime(2022, 1, 6, 22, 29, 29, 527, DateTimeKind.Local).AddTicks(4542), 19 },
                    { 3, 12, new DateTime(2022, 1, 6, 22, 29, 29, 527, DateTimeKind.Local).AddTicks(4252), true, new DateTime(2022, 1, 6, 22, 29, 29, 527, DateTimeKind.Local).AddTicks(4254), 16 },
                    { 11, 9, new DateTime(2022, 1, 6, 22, 29, 29, 527, DateTimeKind.Local).AddTicks(4408), false, new DateTime(2022, 1, 6, 22, 29, 29, 527, DateTimeKind.Local).AddTicks(4410), 17 },
                    { 6, 9, new DateTime(2022, 1, 6, 22, 29, 29, 527, DateTimeKind.Local).AddTicks(4312), true, new DateTime(2022, 1, 6, 22, 29, 29, 527, DateTimeKind.Local).AddTicks(4315), 14 },
                    { 2, 9, new DateTime(2022, 1, 6, 22, 29, 29, 527, DateTimeKind.Local).AddTicks(4218), true, new DateTime(2022, 1, 6, 22, 29, 29, 527, DateTimeKind.Local).AddTicks(4224), 2 },
                    { 9, 6, new DateTime(2022, 1, 6, 22, 29, 29, 527, DateTimeKind.Local).AddTicks(4371), true, new DateTime(2022, 1, 6, 22, 29, 29, 527, DateTimeKind.Local).AddTicks(4373), 6 },
                    { 8, 19, new DateTime(2022, 1, 6, 22, 29, 29, 527, DateTimeKind.Local).AddTicks(4351), true, new DateTime(2022, 1, 6, 22, 29, 29, 527, DateTimeKind.Local).AddTicks(4354), 17 },
                    { 13, 4, new DateTime(2022, 1, 6, 22, 29, 29, 527, DateTimeKind.Local).AddTicks(4445), true, new DateTime(2022, 1, 6, 22, 29, 29, 527, DateTimeKind.Local).AddTicks(4447), 18 },
                    { 12, 4, new DateTime(2022, 1, 6, 22, 29, 29, 527, DateTimeKind.Local).AddTicks(4426), true, new DateTime(2022, 1, 6, 22, 29, 29, 527, DateTimeKind.Local).AddTicks(4429), 7 },
                    { 4, 4, new DateTime(2022, 1, 6, 22, 29, 29, 527, DateTimeKind.Local).AddTicks(4273), false, new DateTime(2022, 1, 6, 22, 29, 29, 527, DateTimeKind.Local).AddTicks(4276), 8 },
                    { 14, 13, new DateTime(2022, 1, 6, 22, 29, 29, 527, DateTimeKind.Local).AddTicks(4464), true, new DateTime(2022, 1, 6, 22, 29, 29, 527, DateTimeKind.Local).AddTicks(4466), 10 },
                    { 20, 17, new DateTime(2022, 1, 6, 22, 29, 29, 527, DateTimeKind.Local).AddTicks(4585), false, new DateTime(2022, 1, 6, 22, 29, 29, 527, DateTimeKind.Local).AddTicks(4588), 7 },
                    { 7, 17, new DateTime(2022, 1, 6, 22, 29, 29, 527, DateTimeKind.Local).AddTicks(4332), true, new DateTime(2022, 1, 6, 22, 29, 29, 527, DateTimeKind.Local).AddTicks(4334), 14 },
                    { 17, 11, new DateTime(2022, 1, 6, 22, 29, 29, 527, DateTimeKind.Local).AddTicks(4521), false, new DateTime(2022, 1, 6, 22, 29, 29, 527, DateTimeKind.Local).AddTicks(4523), 20 },
                    { 10, 11, new DateTime(2022, 1, 6, 22, 29, 29, 527, DateTimeKind.Local).AddTicks(4390), true, new DateTime(2022, 1, 6, 22, 29, 29, 527, DateTimeKind.Local).AddTicks(4392), 2 },
                    { 16, 7, new DateTime(2022, 1, 6, 22, 29, 29, 527, DateTimeKind.Local).AddTicks(4502), true, new DateTime(2022, 1, 6, 22, 29, 29, 527, DateTimeKind.Local).AddTicks(4504), 21 },
                    { 1, 14, new DateTime(2022, 1, 6, 22, 29, 29, 527, DateTimeKind.Local).AddTicks(3235), true, new DateTime(2022, 1, 6, 22, 29, 29, 527, DateTimeKind.Local).AddTicks(3790), 21 },
                    { 15, 2, new DateTime(2022, 1, 6, 22, 29, 29, 527, DateTimeKind.Local).AddTicks(4483), false, new DateTime(2022, 1, 6, 22, 29, 29, 527, DateTimeKind.Local).AddTicks(4486), 11 }
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
