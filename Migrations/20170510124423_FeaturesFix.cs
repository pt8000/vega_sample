using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace _2KursVega.Migrations
{
    public partial class FeaturesFix : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Feature_Models_ModelId",
                table: "Feature");

            migrationBuilder.DropIndex(
                name: "IX_Feature_ModelId",
                table: "Feature");

            migrationBuilder.DropColumn(
                name: "ModelId",
                table: "Feature");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ModelId",
                table: "Feature",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Feature_ModelId",
                table: "Feature",
                column: "ModelId");

            migrationBuilder.AddForeignKey(
                name: "FK_Feature_Models_ModelId",
                table: "Feature",
                column: "ModelId",
                principalTable: "Models",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
