using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Assistant_Bdd.Migrations
{
    /// <inheritdoc />
    public partial class initbd : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Assistant",
                columns: table => new
                {
                    IdAssistant = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OpenAiAssisantId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NomAssistant = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    InstructionAssistant = table.Column<string>(type: "ntext", nullable: true),
                    DescriptionAssistant = table.Column<string>(type: "ntext", nullable: true),
                    LogoAssistant = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    AssistantActif = table.Column<bool>(type: "bit", nullable: false),
                    CreationAssistance = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdateAssistance = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IdCreateurAssistant = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Assistant", x => x.IdAssistant);
                });

            migrationBuilder.CreateTable(
                name: "Client",
                columns: table => new
                {
                    IdClient = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NomClient = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    EmailClient = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: true),
                    TelephoneClient = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: true),
                    ClientActif = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Client", x => x.IdClient);
                });

            migrationBuilder.CreateTable(
                name: "Document",
                columns: table => new
                {
                    IdDocument = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OpenAiDocumentId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NomDocument = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    TypeDocument = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    ReferentDocument = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    UpladDocument = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Document", x => x.IdDocument);
                });

            migrationBuilder.CreateTable(
                name: "Discussion",
                columns: table => new
                {
                    IdDiscussion = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OpenAiDiscussionId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    StatutDiscussion = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    CreationDiscussion = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IdAssistant = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Discussion", x => x.IdDiscussion);
                    table.ForeignKey(
                        name: "FK_Discussion_Assistant_IdAssistant",
                        column: x => x.IdAssistant,
                        principalTable: "Assistant",
                        principalColumn: "IdAssistant");
                });

            migrationBuilder.CreateTable(
                name: "Message",
                columns: table => new
                {
                    IdMessage = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OpenAiMessageId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    StatutMessage = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    CreationMessage = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IdDiscussion = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Message", x => x.IdMessage);
                    table.ForeignKey(
                        name: "FK_Message_Discussion_IdDiscussion",
                        column: x => x.IdDiscussion,
                        principalTable: "Discussion",
                        principalColumn: "IdDiscussion");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Discussion_IdAssistant",
                table: "Discussion",
                column: "IdAssistant");

            migrationBuilder.CreateIndex(
                name: "IX_Message_IdDiscussion",
                table: "Message",
                column: "IdDiscussion");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Client");

            migrationBuilder.DropTable(
                name: "Document");

            migrationBuilder.DropTable(
                name: "Message");

            migrationBuilder.DropTable(
                name: "Discussion");

            migrationBuilder.DropTable(
                name: "Assistant");
        }
    }
}
