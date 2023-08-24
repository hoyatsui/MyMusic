using Microsoft.EntityFrameworkCore.Migrations;

namespace MyMusic.Data.Migrations
{
    public partial class SeedMusicsAndArtistsTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder
                .Sql("INSERT INTO Artists (Name) VALUES ('Pink Floyd')");
            migrationBuilder
                .Sql("INSERT INTO Artists (Name) VALUES ('Eminem')");
            migrationBuilder
                .Sql("INSERT INTO Artists (Name) VALUES ('Lana Del Rey')");
            migrationBuilder
                .Sql("INSERT INTO Artists (Name) VALUES ('Oasis')");
            migrationBuilder
                .Sql("INSERT INTO Musics (Name, ArtistId) VALUES ('Wish You Were Here', (SELECT ID FROM Artists WHERE Name = 'Pink Floyd'))");
            migrationBuilder
                .Sql("INSERT INTO Musics(Name, ArtistId) VALUES ('Time', (SELECT ID FROM Artists WHERE Name = 'Pink Floyd'))");
            migrationBuilder
                .Sql("INSERT INTO Musics (Name, ArtistId) VALUES ('Lucky You', (SELECT ID FROM Artists WHERE Name = 'Eminem'))");
            migrationBuilder
                .Sql("INSERT INTO Musics (Name, ArtistId) VALUES ('Lose Yourself', (SELECT ID FROM Artists WHERE Name = 'Eminem'))");
            migrationBuilder
                .Sql("INSERT INTO Musics (Name, ArtistId) VALUES ('Born To Die', (SELECT ID FROM Artists WHERE Name = 'Lana Del Rey'))");
            migrationBuilder
                .Sql("INSERT INTO Musics (Name, ArtistId) VALUES ('Summertime Sadness', (SELECT ID FROM Artists WHERE Name = 'Lana Del Rey'))");
            migrationBuilder
                .Sql("INSERT INTO Musics (Name, ArtistId) VALUES ('Don''t Look Back In Anger', (SELECT ID FROM Artists WHERE Name = 'Oasis'))");
            migrationBuilder
                .Sql("INSERT INTO Musics (Name, ArtistId) VALUES ('Champagne Supernova', (SELECT ID FROM Artists WHERE Name = 'Oasis'))");
            migrationBuilder
                .Sql("INSERT INTO Musics (Name, ArtistId) VALUES ('Wonderwall', (SELECT ID FROM Artists WHERE Name = 'Oasis'))");
            migrationBuilder
                .Sql("INSERT INTO Musics (Name, ArtistId) VALUES ('Stop Crying Your Heart Out', (SELECT ID FROM Artists WHERE Name = 'Oasis'))");

        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder
                .Sql("DELETE FROM Artists");
            migrationBuilder
                .Sql("DELETE FROM Musics");
        }
    }
}
