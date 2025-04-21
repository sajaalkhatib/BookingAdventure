using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BookingAdventure.Server.Migrations
{
    /// <inheritdoc />
    public partial class pull : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AdventureCategories",
                columns: table => new
                {
                    CategoryId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CategoryName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    Description = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Adventur__19093A0B9B687838", x => x.CategoryId);
                });

            migrationBuilder.CreateTable(
                name: "AdventureTypes",
                columns: table => new
                {
                    TypeId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TypeName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Adventur__516F03B58D70B8EF", x => x.TypeId);
                });

            migrationBuilder.CreateTable(
                name: "ContactMessages",
                columns: table => new
                {
                    MessageId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FullName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    Email = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    Message = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
                    DateSent = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__ContactM__C87C0C9C2C4C74E2", x => x.MessageId);
                });

            migrationBuilder.CreateTable(
                name: "Destinations",
                columns: table => new
                {
                    DestinationId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    ImageUrl = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Destinat__DB5FE4CC217FD7C7", x => x.DestinationId);
                });

            migrationBuilder.CreateTable(
                name: "Instructors",
                columns: table => new
                {
                    InstructorId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FullName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    Bio = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    Phone = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    Email = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Instruct__9D010A9B48B3209A", x => x.InstructorId);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    UserId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FullName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    Email = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    Password = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    Phone = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    Img = table.Column<string>(type: "varchar(500)", unicode: false, maxLength: 500, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Users__1788CC4C15EC4E49", x => x.UserId);
                });

            migrationBuilder.CreateTable(
                name: "DestinationCategories",
                columns: table => new
                {
                    DestinationId = table.Column<int>(type: "int", nullable: false),
                    CategoryId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Destinat__7ACF776CD6959DD7", x => new { x.DestinationId, x.CategoryId });
                    table.ForeignKey(
                        name: "FK__Destinati__Categ__5812160E",
                        column: x => x.CategoryId,
                        principalTable: "AdventureCategories",
                        principalColumn: "CategoryId");
                    table.ForeignKey(
                        name: "FK__Destinati__Desti__571DF1D5",
                        column: x => x.DestinationId,
                        principalTable: "Destinations",
                        principalColumn: "DestinationId");
                });

            migrationBuilder.CreateTable(
                name: "Adventures",
                columns: table => new
                {
                    AdventureId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    Description = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
                    Duration = table.Column<int>(type: "int", nullable: true),
                    Level = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Price = table.Column<decimal>(type: "decimal(10,2)", nullable: true),
                    Location = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    InstructorId = table.Column<int>(type: "int", nullable: true),
                    CategoryId = table.Column<int>(type: "int", nullable: true),
                    MaxParticipants = table.Column<int>(type: "int", nullable: true),
                    IsAvailable = table.Column<bool>(type: "bit", nullable: true, defaultValue: true),
                    DestinationId = table.Column<int>(type: "int", nullable: true),
                    AdventureTypeId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Adventur__D721A00EF328A61C", x => x.AdventureId);
                    table.ForeignKey(
                        name: "FK__Adventure__Adven__5441852A",
                        column: x => x.AdventureTypeId,
                        principalTable: "AdventureTypes",
                        principalColumn: "TypeId");
                    table.ForeignKey(
                        name: "FK__Adventure__Categ__403A8C7D",
                        column: x => x.CategoryId,
                        principalTable: "AdventureCategories",
                        principalColumn: "CategoryId");
                    table.ForeignKey(
                        name: "FK__Adventure__Desti__5165187F",
                        column: x => x.DestinationId,
                        principalTable: "Destinations",
                        principalColumn: "DestinationId");
                    table.ForeignKey(
                        name: "FK__Adventure__Instr__3F466844",
                        column: x => x.InstructorId,
                        principalTable: "Instructors",
                        principalColumn: "InstructorId");
                });

            migrationBuilder.CreateTable(
                name: "AdventureDetail",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AdventureId = table.Column<int>(type: "int", nullable: false),
                    Overview = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    HighlightsJson = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FaqsJson = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AdventureDetail", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AdventureDetail_Adventures_AdventureId",
                        column: x => x.AdventureId,
                        principalTable: "Adventures",
                        principalColumn: "AdventureId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AdventureImages",
                columns: table => new
                {
                    ImageId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AdventureId = table.Column<int>(type: "int", nullable: true),
                    ImageUrl = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Adventur__7516F70CD2664618", x => x.ImageId);
                    table.ForeignKey(
                        name: "FK__Adventure__Adven__4316F928",
                        column: x => x.AdventureId,
                        principalTable: "Adventures",
                        principalColumn: "AdventureId");
                });

            migrationBuilder.CreateTable(
                name: "Bookings",
                columns: table => new
                {
                    BookingId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(type: "int", nullable: true),
                    AdventureId = table.Column<int>(type: "int", nullable: true),
                    BookingDate = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())"),
                    ScheduledDate = table.Column<DateTime>(type: "datetime", nullable: true),
                    NumberOfParticipants = table.Column<int>(type: "int", nullable: true),
                    Status = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    PaymentType = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Bookings__73951AEDFC2D384D", x => x.BookingId);
                    table.ForeignKey(
                        name: "FK__Bookings__Advent__47DBAE45",
                        column: x => x.AdventureId,
                        principalTable: "Adventures",
                        principalColumn: "AdventureId");
                    table.ForeignKey(
                        name: "FK__Bookings__UserId__46E78A0C",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "UserId");
                });

            migrationBuilder.CreateTable(
                name: "Reviews",
                columns: table => new
                {
                    ReviewId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AdventureId = table.Column<int>(type: "int", nullable: true),
                    Rating = table.Column<int>(type: "int", nullable: true),
                    Comment = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
                    ReviewDate = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Reviews__74BC79CE479F14E7", x => x.ReviewId);
                    table.ForeignKey(
                        name: "FK__Reviews__Adventu__4BAC3F29",
                        column: x => x.AdventureId,
                        principalTable: "Adventures",
                        principalColumn: "AdventureId");
                });

            migrationBuilder.CreateIndex(
                name: "IX_AdventureDetail_AdventureId",
                table: "AdventureDetail",
                column: "AdventureId");

            migrationBuilder.CreateIndex(
                name: "IX_AdventureImages_AdventureId",
                table: "AdventureImages",
                column: "AdventureId");

            migrationBuilder.CreateIndex(
                name: "IX_Adventures_AdventureTypeId",
                table: "Adventures",
                column: "AdventureTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Adventures_CategoryId",
                table: "Adventures",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Adventures_DestinationId",
                table: "Adventures",
                column: "DestinationId");

            migrationBuilder.CreateIndex(
                name: "IX_Adventures_InstructorId",
                table: "Adventures",
                column: "InstructorId");

            migrationBuilder.CreateIndex(
                name: "IX_Bookings_AdventureId",
                table: "Bookings",
                column: "AdventureId");

            migrationBuilder.CreateIndex(
                name: "IX_Bookings_UserId",
                table: "Bookings",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_DestinationCategories_CategoryId",
                table: "DestinationCategories",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Reviews_AdventureId",
                table: "Reviews",
                column: "AdventureId");

            migrationBuilder.CreateIndex(
                name: "UQ__Users__A9D10534DCE0DED0",
                table: "Users",
                column: "Email",
                unique: true,
                filter: "[Email] IS NOT NULL");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AdventureDetail");

            migrationBuilder.DropTable(
                name: "AdventureImages");

            migrationBuilder.DropTable(
                name: "Bookings");

            migrationBuilder.DropTable(
                name: "ContactMessages");

            migrationBuilder.DropTable(
                name: "DestinationCategories");

            migrationBuilder.DropTable(
                name: "Reviews");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "Adventures");

            migrationBuilder.DropTable(
                name: "AdventureTypes");

            migrationBuilder.DropTable(
                name: "AdventureCategories");

            migrationBuilder.DropTable(
                name: "Destinations");

            migrationBuilder.DropTable(
                name: "Instructors");
        }
    }
}
