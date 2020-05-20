using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TheSneakersMob.Migrations
{
    public partial class InitialModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    Name = table.Column<string>(maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUsers",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    UserName = table.Column<string>(maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(maxLength: 256, nullable: true),
                    Email = table.Column<string>(maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(nullable: false),
                    PasswordHash = table.Column<string>(nullable: true),
                    SecurityStamp = table.Column<string>(nullable: true),
                    ConcurrencyStamp = table.Column<string>(nullable: true),
                    PhoneNumber = table.Column<string>(nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(nullable: false),
                    TwoFactorEnabled = table.Column<bool>(nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(nullable: true),
                    LockoutEnabled = table.Column<bool>(nullable: false),
                    AccessFailedCount = table.Column<int>(nullable: false),
                    FirstName = table.Column<string>(nullable: true),
                    LastName = table.Column<string>(nullable: true),
                    Country = table.Column<string>(nullable: true),
                    BannedUntil = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUsers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Brands",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Brands", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Categories",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categories", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Clients",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(nullable: false),
                    UserName = table.Column<string>(nullable: true),
                    Email = table.Column<string>(nullable: true),
                    FirstName = table.Column<string>(nullable: true),
                    LastName = table.Column<string>(nullable: true),
                    Country = table.Column<string>(nullable: true),
                    PhotoUrl = table.Column<string>(nullable: true),
                    RegistrationDate = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Clients", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "DeviceCodes",
                columns: table => new
                {
                    UserCode = table.Column<string>(maxLength: 200, nullable: false),
                    DeviceCode = table.Column<string>(maxLength: 200, nullable: false),
                    SubjectId = table.Column<string>(maxLength: 200, nullable: true),
                    ClientId = table.Column<string>(maxLength: 200, nullable: false),
                    CreationTime = table.Column<DateTime>(nullable: false),
                    Expiration = table.Column<DateTime>(nullable: false),
                    Data = table.Column<string>(maxLength: 50000, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DeviceCodes", x => x.UserCode);
                });

            migrationBuilder.CreateTable(
                name: "PersistedGrants",
                columns: table => new
                {
                    Key = table.Column<string>(maxLength: 200, nullable: false),
                    Type = table.Column<string>(maxLength: 50, nullable: false),
                    SubjectId = table.Column<string>(maxLength: 200, nullable: true),
                    ClientId = table.Column<string>(maxLength: 200, nullable: false),
                    CreationTime = table.Column<DateTime>(nullable: false),
                    Expiration = table.Column<DateTime>(nullable: true),
                    Data = table.Column<string>(maxLength: 50000, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PersistedGrants", x => x.Key);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleId = table.Column<string>(nullable: false),
                    ClaimType = table.Column<string>(nullable: true),
                    ClaimValue = table.Column<string>(nullable: true)
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
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(nullable: false),
                    ClaimType = table.Column<string>(nullable: true),
                    ClaimValue = table.Column<string>(nullable: true)
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
                    LoginProvider = table.Column<string>(nullable: false),
                    ProviderKey = table.Column<string>(nullable: false),
                    ProviderDisplayName = table.Column<string>(nullable: true),
                    UserId = table.Column<string>(nullable: false)
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
                    UserId = table.Column<string>(nullable: false),
                    RoleId = table.Column<string>(nullable: false)
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
                    UserId = table.Column<string>(nullable: false),
                    LoginProvider = table.Column<string>(nullable: false),
                    Name = table.Column<string>(nullable: false),
                    Value = table.Column<string>(nullable: true)
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
                name: "Sizes",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Description = table.Column<string>(nullable: true),
                    CategoryId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sizes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Sizes_Categories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Categories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SubCategories",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: true),
                    CategoryId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SubCategories", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SubCategories_Categories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Categories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ClientFollower",
                columns: table => new
                {
                    ClientId = table.Column<int>(nullable: false),
                    FollowerId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ClientFollower", x => new { x.ClientId, x.FollowerId });
                    table.ForeignKey(
                        name: "FK_ClientFollower_Clients_ClientId",
                        column: x => x.ClientId,
                        principalTable: "Clients",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ClientFollower_Clients_FollowerId",
                        column: x => x.FollowerId,
                        principalTable: "Clients",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(nullable: true),
                    CategoryId = table.Column<int>(nullable: true),
                    SubCategoryId = table.Column<int>(nullable: true),
                    Style = table.Column<int>(nullable: false),
                    BrandId = table.Column<int>(nullable: true),
                    SizeId = table.Column<int>(nullable: true),
                    Color = table.Column<string>(nullable: true),
                    Condition = table.Column<int>(nullable: false),
                    Description = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Products_Brands_BrandId",
                        column: x => x.BrandId,
                        principalTable: "Brands",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Products_Categories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Categories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Products_Sizes_SizeId",
                        column: x => x.SizeId,
                        principalTable: "Sizes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Products_SubCategories_SubCategoryId",
                        column: x => x.SubCategoryId,
                        principalTable: "SubCategories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Auctions",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProductId = table.Column<int>(nullable: true),
                    ExpireDate = table.Column<DateTime>(nullable: false),
                    DirectBuyPrice_Amount = table.Column<decimal>(nullable: true),
                    DirectBuyPrice_Currency = table.Column<int>(nullable: true),
                    InitialPrize_Amount = table.Column<decimal>(nullable: true),
                    InitialPrize_Currency = table.Column<int>(nullable: true),
                    AuctionerId = table.Column<int>(nullable: true),
                    BuyerId = table.Column<int>(nullable: true),
                    Feedback_Stars = table.Column<int>(nullable: true),
                    Feedback_Comment = table.Column<string>(nullable: true),
                    Removed = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Auctions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Auctions_Clients_AuctionerId",
                        column: x => x.AuctionerId,
                        principalTable: "Clients",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Auctions_Clients_BuyerId",
                        column: x => x.BuyerId,
                        principalTable: "Clients",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Auctions_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Designer",
                columns: table => new
                {
                    ProductId = table.Column<int>(nullable: false),
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Designer", x => new { x.ProductId, x.Id });
                    table.ForeignKey(
                        name: "FK_Designer_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Photo",
                columns: table => new
                {
                    ProductId = table.Column<int>(nullable: false),
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(nullable: true),
                    Url = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Photo", x => new { x.ProductId, x.Id });
                    table.ForeignKey(
                        name: "FK_Photo_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Sells",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SellerId = table.Column<int>(nullable: false),
                    BuyerId = table.Column<int>(nullable: true),
                    ProductId = table.Column<int>(nullable: true),
                    Price_Amount = table.Column<decimal>(nullable: true),
                    Price_Currency = table.Column<int>(nullable: true),
                    Feedback_Stars = table.Column<int>(nullable: true),
                    Feedback_Comment = table.Column<string>(nullable: true),
                    Removed = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sells", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Sells_Clients_BuyerId",
                        column: x => x.BuyerId,
                        principalTable: "Clients",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Sells_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Sells_Clients_SellerId",
                        column: x => x.SellerId,
                        principalTable: "Clients",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Auctions_HashTags",
                columns: table => new
                {
                    AuctionId = table.Column<int>(nullable: false),
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Auctions_HashTags", x => new { x.AuctionId, x.Id });
                    table.ForeignKey(
                        name: "FK_Auctions_HashTags_Auctions_AuctionId",
                        column: x => x.AuctionId,
                        principalTable: "Auctions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Auctions_Reports",
                columns: table => new
                {
                    AuctionId = table.Column<int>(nullable: false),
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Reason = table.Column<int>(nullable: false),
                    Severity = table.Column<int>(nullable: false),
                    Date = table.Column<DateTime>(nullable: false),
                    ReporterId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Auctions_Reports", x => new { x.AuctionId, x.Id });
                    table.ForeignKey(
                        name: "FK_Auctions_Reports_Auctions_AuctionId",
                        column: x => x.AuctionId,
                        principalTable: "Auctions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Auctions_Reports_Clients_ReporterId",
                        column: x => x.ReporterId,
                        principalTable: "Clients",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Bid",
                columns: table => new
                {
                    AuctionId = table.Column<int>(nullable: false),
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Amount_Amount = table.Column<decimal>(nullable: true),
                    Amount_Currency = table.Column<int>(nullable: true),
                    BidderId = table.Column<int>(nullable: true),
                    Date = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Bid", x => new { x.AuctionId, x.Id });
                    table.ForeignKey(
                        name: "FK_Bid_Auctions_AuctionId",
                        column: x => x.AuctionId,
                        principalTable: "Auctions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Bid_Clients_BidderId",
                        column: x => x.BidderId,
                        principalTable: "Clients",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Likes",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Date = table.Column<DateTime>(nullable: false),
                    UserId = table.Column<int>(nullable: true),
                    SellId = table.Column<int>(nullable: true),
                    AuctionId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Likes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Likes_Auctions_AuctionId",
                        column: x => x.AuctionId,
                        principalTable: "Auctions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Likes_Sells_SellId",
                        column: x => x.SellId,
                        principalTable: "Sells",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Likes_Clients_UserId",
                        column: x => x.UserId,
                        principalTable: "Clients",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Sells_HashTags",
                columns: table => new
                {
                    SellId = table.Column<int>(nullable: false),
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sells_HashTags", x => new { x.SellId, x.Id });
                    table.ForeignKey(
                        name: "FK_Sells_HashTags_Sells_SellId",
                        column: x => x.SellId,
                        principalTable: "Sells",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Sells_Reports",
                columns: table => new
                {
                    SellId = table.Column<int>(nullable: false),
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Reason = table.Column<int>(nullable: false),
                    Severity = table.Column<int>(nullable: false),
                    Date = table.Column<DateTime>(nullable: false),
                    ReporterId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sells_Reports", x => new { x.SellId, x.Id });
                    table.ForeignKey(
                        name: "FK_Sells_Reports_Clients_ReporterId",
                        column: x => x.ReporterId,
                        principalTable: "Clients",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Sells_Reports_Sells_SellId",
                        column: x => x.SellId,
                        principalTable: "Sells",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Brands",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "Supreme" },
                    { 57, "Kith" },
                    { 56, "Nautica" },
                    { 55, "Harley Davidson" },
                    { 54, "Puma" },
                    { 53, "Lacoste" },
                    { 52, "NBA" },
                    { 51, "Converse" },
                    { 50, "RalphLauren" },
                    { 49, "Kappa" },
                    { 48, "NFL" },
                    { 47, "Kanye West" },
                    { 46, "Stone Island" },
                    { 45, "Dolce & Gabbana" },
                    { 44, "Reebok" },
                    { 43, "Travis Scott" },
                    { 58, "Rick Owens" },
                    { 59, "Carhartt" },
                    { 60, "Calvin Klein" },
                    { 61, "Kaws" },
                    { 78, "Hanes" },
                    { 77, "Playboy" },
                    { 76, "Balmain" },
                    { 75, "MLB" },
                    { 74, "Patagonia" },
                    { 73, "Helmut Lang" },
                    { 72, "Billionaire Boys Club" },
                    { 41, "Balenciaga" },
                    { 71, "Mickey Mouse" },
                    { 68, "Gosha Rubchinskiy" },
                    { 67, "Monsieur" },
                    { 66, "Christian Dior" },
                    { 65, "A.P.C." },
                    { 64, "Visvim" },
                    { 63, "Kenzo" },
                    { 62, "Fendi " },
                    { 69, "Fear of God" },
                    { 40, "Valentino" },
                    { 42, "Versace" },
                    { 38, "Hysteric Glamour" },
                    { 17, "American Vintage" },
                    { 16, "Anti Social Social Club" },
                    { 15, "Palace" },
                    { 14, "The North Face" },
                    { 13, "Uniqlo" },
                    { 12, "Gucci" },
                    { 11, "Undercover" },
                    { 39, "AcneStudios" },
                    { 10, "Comme des Garcons" },
                    { 8, "Champion" },
                    { 7, "Burberry" },
                    { 6, "Bape" },
                    { 5, "Polo Ralph Lauren" },
                    { 4, "Jordan" },
                    { 3, "Adidas" },
                    { 2, "Nike" },
                    { 9, "Tommy Hilfiger" },
                    { 19, "Saint Laurent Paris" },
                    { 18, "Off-White" },
                    { 21, "LaurentMovie" },
                    { 37, "Cartoon Network" },
                    { 36, "Givenchy" },
                    { 35, "Maison Margiela " },
                    { 34, "Fila" },
                    { 33, "(N)ine" },
                    { 20, "Stussy" },
                    { 31, "Dior" },
                    { 30, "Vans" },
                    { 32, "Raf SimonsNumber " },
                    { 28, "Rare" },
                    { 27, "Disney" },
                    { 26, "Louis Vuitton" },
                    { 25, "Yohji Yamamoto" },
                    { 24, "Guess" },
                    { 23, "Issey Miyake" },
                    { 22, "Comme Des Garcons Homme Plus" },
                    { 29, "Prada" }
                });

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 5, "Tailoring" },
                    { 1, "Tops" },
                    { 2, "Bottoms" },
                    { 3, "Outerwear" },
                    { 4, "Footwear" },
                    { 6, "Accesories" }
                });

            migrationBuilder.InsertData(
                table: "Sizes",
                columns: new[] { "Id", "CategoryId", "Description" },
                values: new object[,]
                {
                    { 1, 1, "US XXS/EU 40" },
                    { 414, 5, "42R" },
                    { 413, 5, "42S" },
                    { 412, 5, "40R" },
                    { 411, 5, "40R" },
                    { 409, 5, "40R" },
                    { 408, 5, "40S" },
                    { 407, 5, "38L" },
                    { 406, 5, "38R" },
                    { 405, 5, "38S" },
                    { 404, 5, "36R" },
                    { 403, 5, "36S" },
                    { 402, 5, "34R" },
                    { 415, 5, "44L" },
                    { 401, 5, "34S" },
                    { 318, 4, "US 14/EU 57" },
                    { 317, 4, "US 13/EU 56" },
                    { 316, 4, "US 12.5/EU 55-56" },
                    { 314, 4, "US 11.5/EU 44-55" },
                    { 313, 4, "US 11/EU 44" },
                    { 312, 4, "US 10.5/EU 43-44" },
                    { 311, 4, "US 10/EU 43" },
                    { 310, 4, "US 9.5/EU 42-43" },
                    { 309, 4, "US 9/EU 42" },
                    { 308, 4, "US 8.5/ EU 41-42" },
                    { 307, 4, "US 8/EU 41" },
                    { 306, 4, "US 7.5/EU 40-41" },
                    { 319, 4, "US 15/EU 58" },
                    { 416, 5, "44S" },
                    { 417, 5, "44R" },
                    { 418, 5, "44L" },
                    { 512, 6, "46" },
                    { 511, 6, "44" },
                    { 510, 6, "42" },
                    { 509, 6, "40" },
                    { 508, 6, "38" },
                    { 507, 6, "36" },
                    { 506, 6, "34" },
                    { 505, 6, "32" },
                    { 504, 6, "30" },
                    { 503, 6, "28" },
                    { 502, 6, "26" },
                    { 501, 6, "One Size" },
                    { 433, 5, "54L" },
                    { 432, 5, "54R" },
                    { 431, 5, "54S" },
                    { 430, 5, "52L" },
                    { 429, 5, "52R" },
                    { 428, 5, "52S" },
                    { 427, 5, "50L" },
                    { 426, 5, "50R" },
                    { 425, 5, "50S" },
                    { 424, 5, "48L" },
                    { 423, 5, "48R" },
                    { 422, 5, "48S" },
                    { 421, 5, "46L" },
                    { 420, 5, "46R" },
                    { 419, 5, "46S" },
                    { 305, 4, "US 7/EU 40" },
                    { 304, 4, "US 6.5/EU 39-40" },
                    { 315, 4, "US 12/EU 55" },
                    { 302, 4, "US 5.5/EU 38" },
                    { 115, 2, "US 42/EU 58" },
                    { 114, 2, "US 40/EU 56" },
                    { 113, 2, "US 39" },
                    { 112, 2, "US 38/EU 54" },
                    { 111, 2, "US 37" },
                    { 110, 2, "US 36/ EU 52" },
                    { 109, 2, "US 35" },
                    { 303, 4, "US 6/EU 39" },
                    { 107, 2, "US 33" },
                    { 106, 2, "US 32/EU 48" },
                    { 116, 2, "US 43" },
                    { 105, 2, "US 31" },
                    { 103, 2, "US 29" },
                    { 102, 2, "US28/EU 44" },
                    { 101, 2, "US 27" },
                    { 100, 2, "US 26/EU 42" },
                    { 7, 1, "US S/EU 58/5" },
                    { 6, 1, "US XL/EU 56/4" },
                    { 5, 1, "US L/EU 52-54/3" },
                    { 4, 1, "US M/ EU 48-50/2" },
                    { 3, 1, "US S/ EU 44-46/1" },
                    { 2, 1, "US XS/EU 42/0" },
                    { 104, 2, "US 30/EU 44" },
                    { 117, 2, "US 44/EU 60" },
                    { 108, 2, "US 34/EU 50" },
                    { 301, 4, "US 5/EU 37 " },
                    { 206, 3, "US XL/EU 56/4" },
                    { 207, 3, "US S/EU 58/5" },
                    { 203, 3, "US S/ EU 44-46/1" },
                    { 204, 3, "US M/ EU 48-50/2" },
                    { 201, 3, "US XXS/EU 40" },
                    { 202, 3, "US XS/EU 42/0" },
                    { 205, 3, "US L/EU 52-54/3" }
                });

            migrationBuilder.InsertData(
                table: "SubCategories",
                columns: new[] { "Id", "CategoryId", "Name" },
                values: new object[,]
                {
                    { 1, 1, "Long Sleeve" },
                    { 2, 1, "T-Shirts" },
                    { 3, 1, "Polos" },
                    { 205, 3, "Light Jackets" },
                    { 206, 3, "Parkas" },
                    { 207, 3, "Raincoats" },
                    { 208, 3, "Vests" },
                    { 4, 1, "Shirts(Butoom Ups)" },
                    { 5, 1, "Short Sleeve T-Shirts" },
                    { 204, 3, "Leather Jackets" },
                    { 106, 2, "Sweatpants & Joggers" },
                    { 500, 6, "Bags & Luggage" },
                    { 501, 6, "Belts" },
                    { 502, 6, "Glasses" },
                    { 503, 6, "Gloves & Scarves" },
                    { 504, 6, "Hats" },
                    { 505, 6, "Jewelry & Watches" },
                    { 506, 6, "Wallets" },
                    { 507, 6, "Miscellaneous" },
                    { 508, 6, "Periodicals" },
                    { 509, 6, "Socks & Underwear" },
                    { 510, 6, "Sunglasses" },
                    { 6, 1, "Sweaters & Knitwear" },
                    { 405, 5, "Vests" },
                    { 401, 5, "Formal Shirting" },
                    { 403, 5, "Suits" },
                    { 105, 2, "Shorts" },
                    { 104, 2, "Overalls & Jumpsuits" },
                    { 103, 2, "Leggings" },
                    { 102, 2, "Denim" },
                    { 101, 2, "Cropped Pants" },
                    { 100, 2, "Casual Pants" },
                    { 305, 4, "Slip Ons" },
                    { 304, 4, "Sandals" },
                    { 303, 4, "Low-Top Sneakers" },
                    { 302, 4, "Hi-Top Sneakers" },
                    { 301, 4, "Formal Shoes" },
                    { 300, 4, "Casual Leather Shoes" },
                    { 511, 6, "Supreme" },
                    { 200, 3, "Bombers" },
                    { 201, 3, "Cloaks & Capes" },
                    { 202, 3, "Denim Jackets" },
                    { 203, 3, "Heavy Coats" },
                    { 9, 1, "Jerseys" },
                    { 8, 1, "Tank Tops & Sleeveless" },
                    { 7, 1, "Sweatshirts & Hoodies" },
                    { 400, 5, "Blazers" },
                    { 107, 2, "Swimwear" },
                    { 402, 5, "Formal Trousers" },
                    { 404, 5, "Tuxedos" },
                    { 512, 6, "Ties & Pocketsquares" }
                });

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
                name: "IX_Auctions_AuctionerId",
                table: "Auctions",
                column: "AuctionerId");

            migrationBuilder.CreateIndex(
                name: "IX_Auctions_BuyerId",
                table: "Auctions",
                column: "BuyerId");

            migrationBuilder.CreateIndex(
                name: "IX_Auctions_ProductId",
                table: "Auctions",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_Auctions_Reports_ReporterId",
                table: "Auctions_Reports",
                column: "ReporterId");

            migrationBuilder.CreateIndex(
                name: "IX_Bid_BidderId",
                table: "Bid",
                column: "BidderId");

            migrationBuilder.CreateIndex(
                name: "IX_ClientFollower_FollowerId",
                table: "ClientFollower",
                column: "FollowerId");

            migrationBuilder.CreateIndex(
                name: "IX_DeviceCodes_DeviceCode",
                table: "DeviceCodes",
                column: "DeviceCode",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_DeviceCodes_Expiration",
                table: "DeviceCodes",
                column: "Expiration");

            migrationBuilder.CreateIndex(
                name: "IX_Likes_AuctionId",
                table: "Likes",
                column: "AuctionId");

            migrationBuilder.CreateIndex(
                name: "IX_Likes_SellId",
                table: "Likes",
                column: "SellId");

            migrationBuilder.CreateIndex(
                name: "IX_Likes_UserId",
                table: "Likes",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_PersistedGrants_Expiration",
                table: "PersistedGrants",
                column: "Expiration");

            migrationBuilder.CreateIndex(
                name: "IX_PersistedGrants_SubjectId_ClientId_Type",
                table: "PersistedGrants",
                columns: new[] { "SubjectId", "ClientId", "Type" });

            migrationBuilder.CreateIndex(
                name: "IX_Products_BrandId",
                table: "Products",
                column: "BrandId");

            migrationBuilder.CreateIndex(
                name: "IX_Products_CategoryId",
                table: "Products",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Products_SizeId",
                table: "Products",
                column: "SizeId");

            migrationBuilder.CreateIndex(
                name: "IX_Products_SubCategoryId",
                table: "Products",
                column: "SubCategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Sells_BuyerId",
                table: "Sells",
                column: "BuyerId");

            migrationBuilder.CreateIndex(
                name: "IX_Sells_ProductId",
                table: "Sells",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_Sells_SellerId",
                table: "Sells",
                column: "SellerId");

            migrationBuilder.CreateIndex(
                name: "IX_Sells_Reports_ReporterId",
                table: "Sells_Reports",
                column: "ReporterId");

            migrationBuilder.CreateIndex(
                name: "IX_Sizes_CategoryId",
                table: "Sizes",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_SubCategories_CategoryId",
                table: "SubCategories",
                column: "CategoryId");
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
                name: "Auctions_HashTags");

            migrationBuilder.DropTable(
                name: "Auctions_Reports");

            migrationBuilder.DropTable(
                name: "Bid");

            migrationBuilder.DropTable(
                name: "ClientFollower");

            migrationBuilder.DropTable(
                name: "Designer");

            migrationBuilder.DropTable(
                name: "DeviceCodes");

            migrationBuilder.DropTable(
                name: "Likes");

            migrationBuilder.DropTable(
                name: "PersistedGrants");

            migrationBuilder.DropTable(
                name: "Photo");

            migrationBuilder.DropTable(
                name: "Sells_HashTags");

            migrationBuilder.DropTable(
                name: "Sells_Reports");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "Auctions");

            migrationBuilder.DropTable(
                name: "Sells");

            migrationBuilder.DropTable(
                name: "Clients");

            migrationBuilder.DropTable(
                name: "Products");

            migrationBuilder.DropTable(
                name: "Brands");

            migrationBuilder.DropTable(
                name: "Sizes");

            migrationBuilder.DropTable(
                name: "SubCategories");

            migrationBuilder.DropTable(
                name: "Categories");
        }
    }
}
