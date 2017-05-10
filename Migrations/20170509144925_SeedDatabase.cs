using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace _2KursVega.Migrations
{
    public partial class SeedDatabase : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("INSERT INTO Makes (Name) VALUES ('Renault')");
            migrationBuilder.Sql("INSERT INTO Makes (Name) VALUES ('Citroen')");
            migrationBuilder.Sql("INSERT INTO Makes (Name) VALUES ('Ford')");

            migrationBuilder.Sql("INSERT INTO Models (Name, MakeId) VALUES ('Clio', (SELECT Id FROM Makes WHERE Name = 'Renault'))");
            migrationBuilder.Sql("INSERT INTO Models (Name, MakeId) VALUES ('Laguna', (SELECT Id FROM Makes WHERE Name = 'Renault'))");
            migrationBuilder.Sql("INSERT INTO Models (Name, MakeId) VALUES ('Talisman', (SELECT Id FROM Makes WHERE Name = 'Renault'))");

            migrationBuilder.Sql("INSERT INTO Models (Name, MakeId) VALUES ('C5', (SELECT Id FROM Makes WHERE Name = 'Citroen'))");
            migrationBuilder.Sql("INSERT INTO Models (Name, MakeId) VALUES ('C4 Picasso', (SELECT Id FROM Makes WHERE Name = 'Citroen'))");
            
            migrationBuilder.Sql("INSERT INTO Models (Name, MakeId) VALUES ('Focus', (SELECT Id FROM Makes WHERE Name = 'Ford'))");
            migrationBuilder.Sql("INSERT INTO Models (Name, MakeId) VALUES ('Mondeo', (SELECT Id FROM Makes WHERE Name = 'Ford'))");
            migrationBuilder.Sql("INSERT INTO Models (Name, MakeId) VALUES ('Escort', (SELECT Id FROM Makes WHERE Name = 'Ford'))");
            
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("DELETE FROM Makes WHERE Name IN ('Renault', 'Citroen', 'Ford')"); // kasuje od razu tez powiazane modele
        }
    }
}
