using Microsoft.EntityFrameworkCore.Migrations;

namespace MovieWEBApp.Migrations
{
    public partial class InitDB : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Movies",
                columns: table => new
                {
                    MovieID = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    title = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    language = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    averageRating = table.Column<float>(type: "real", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Movies", x => x.MovieID);
                });

            migrationBuilder.CreateTable(
                name: "Staffs",
                columns: table => new
                {
                    StaffID = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    fullName = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Staffs", x => x.StaffID);
                });

            migrationBuilder.CreateTable(
                name: "Tags",
                columns: table => new
                {
                    TagID = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    name = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tags", x => x.TagID);
                });

            migrationBuilder.CreateTable(
                name: "MovieStaff",
                columns: table => new
                {
                    actorsStaffID = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    isActorMovieID = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MovieStaff", x => new { x.actorsStaffID, x.isActorMovieID });
                    table.ForeignKey(
                        name: "FK_MovieStaff_Movies_isActorMovieID",
                        column: x => x.isActorMovieID,
                        principalTable: "Movies",
                        principalColumn: "MovieID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MovieStaff_Staffs_actorsStaffID",
                        column: x => x.actorsStaffID,
                        principalTable: "Staffs",
                        principalColumn: "StaffID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MovieStaff1",
                columns: table => new
                {
                    directorsStaffID = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    isDirectorMovieID = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MovieStaff1", x => new { x.directorsStaffID, x.isDirectorMovieID });
                    table.ForeignKey(
                        name: "FK_MovieStaff1_Movies_isDirectorMovieID",
                        column: x => x.isDirectorMovieID,
                        principalTable: "Movies",
                        principalColumn: "MovieID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MovieStaff1_Staffs_directorsStaffID",
                        column: x => x.directorsStaffID,
                        principalTable: "Staffs",
                        principalColumn: "StaffID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MovieTag",
                columns: table => new
                {
                    moviesMovieID = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    tagsTagID = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MovieTag", x => new { x.moviesMovieID, x.tagsTagID });
                    table.ForeignKey(
                        name: "FK_MovieTag_Movies_moviesMovieID",
                        column: x => x.moviesMovieID,
                        principalTable: "Movies",
                        principalColumn: "MovieID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MovieTag_Tags_tagsTagID",
                        column: x => x.tagsTagID,
                        principalTable: "Tags",
                        principalColumn: "TagID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_MovieStaff_isActorMovieID",
                table: "MovieStaff",
                column: "isActorMovieID");

            migrationBuilder.CreateIndex(
                name: "IX_MovieStaff1_isDirectorMovieID",
                table: "MovieStaff1",
                column: "isDirectorMovieID");

            migrationBuilder.CreateIndex(
                name: "IX_MovieTag_tagsTagID",
                table: "MovieTag",
                column: "tagsTagID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MovieStaff");

            migrationBuilder.DropTable(
                name: "MovieStaff1");

            migrationBuilder.DropTable(
                name: "MovieTag");

            migrationBuilder.DropTable(
                name: "Staffs");

            migrationBuilder.DropTable(
                name: "Movies");

            migrationBuilder.DropTable(
                name: "Tags");
        }
    }
}
