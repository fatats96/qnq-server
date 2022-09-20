using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace QandQ.Data.Migrations
{
    public partial class AddedMovieId : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "MovieId",
                table: "Movies",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MovieId",
                table: "Movies");
        }
    }
}
