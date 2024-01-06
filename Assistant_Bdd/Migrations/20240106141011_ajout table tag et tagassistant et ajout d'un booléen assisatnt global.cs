using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Assistant_Bdd.Migrations
{
    /// <inheritdoc />
    public partial class ajouttabletagettagassistantetajoutdunbooléenassisatntglobal : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsAssistantGlobal",
                table: "Assistant",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.CreateTable(
                name: "Tag",
                columns: table => new
                {
                    IdTag = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    LibelleTag = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    CreationTag = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tag", x => x.IdTag);
                });

            migrationBuilder.CreateTable(
                name: "TagAssistant",
                columns: table => new
                {
                    IdTag = table.Column<int>(type: "int", nullable: false),
                    IdAssistant = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TagAssistant", x => new { x.IdTag, x.IdAssistant });
                    table.ForeignKey(
                        name: "FK_TagAssistant_Assistant_IdAssistant",
                        column: x => x.IdAssistant,
                        principalTable: "Assistant",
                        principalColumn: "IdAssistant",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TagAssistant_Tag_IdTag",
                        column: x => x.IdTag,
                        principalTable: "Tag",
                        principalColumn: "IdTag",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TagAssistant_IdAssistant",
                table: "TagAssistant",
                column: "IdAssistant");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TagAssistant");

            migrationBuilder.DropTable(
                name: "Tag");

            migrationBuilder.DropColumn(
                name: "IsAssistantGlobal",
                table: "Assistant");
        }
    }
}
