namespace GigHub.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class PopulateGenresToDatabase : DbMigration
    {
        public override void Up()
        {
            Sql("INSERT INTO Genres (Id, Name) VALUES (1,'Jazz')");
            Sql("INSERT INTO Genres (Id, Name) VALUES (2,'Blues')");
            Sql("INSERT INTO Genres (Id, Name) VALUES (3,'Rock')");
            Sql("INSERT INTO Genres (Id, Name) VALUES (4,'Country')");
            Sql("INSERT INTO Genres (Id, Name) VALUES (5,'Rap')");
            Sql("INSERT INTO Genres (Id, Name) VALUES (6,'Pop')");
        }
        
        public override void Down()
        {
            Sql("DELETE FROM Genres WHRE Id IN (1,2,3,4,5,6)");
        }
    }
}
