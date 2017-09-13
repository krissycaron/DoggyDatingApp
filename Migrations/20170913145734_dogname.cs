using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace serverSideCapstone.Migrations
{
    public partial class dogname : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "DogName",
                table: "AspNetUsers",
                type: "TEXT",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DogName",
                table: "AspNetUsers");
        }
    }
}
