using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "RecognitionModel",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FileName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    ImportTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastTrainedTime = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RecognitionModel", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "User",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Username = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Role = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Tuning",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    StartTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    FinishTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    RecognitionModelId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tuning", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Tuning_RecognitionModel_RecognitionModelId",
                        column: x => x.RecognitionModelId,
                        principalTable: "RecognitionModel",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "FK_Tuning_User_UserId",
                        column: x => x.UserId,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateTable(
                name: "USRS",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    StartTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    FinishTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    RecognitionResultId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    RecognitionModelId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_USRS", x => x.Id);
                    table.ForeignKey(
                        name: "FK_USRS_RecognitionModel_RecognitionModelId",
                        column: x => x.RecognitionModelId,
                        principalTable: "RecognitionModel",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "FK_USRS_User_UserId",
                        column: x => x.UserId,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateTable(
                name: "InFile",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FileName = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    ImportTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    USRSId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    TuningId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InFile", x => x.Id);
                    table.ForeignKey(
                        name: "FK_InFile_Tuning_TuningId",
                        column: x => x.TuningId,
                        principalTable: "Tuning",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "FK_InFile_USRS_USRSId",
                        column: x => x.USRSId,
                        principalTable: "USRS",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateTable(
                name: "RecognitionResult",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    RecognizedText = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RecognitionTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    AverageConfidence = table.Column<float>(type: "real", nullable: false),
                    Accuracy = table.Column<float>(type: "real", nullable: false),
                    CharacterErrorRate = table.Column<float>(type: "real", nullable: false),
                    WordErrorRate = table.Column<float>(type: "real", nullable: false),
                    ProcessingTime = table.Column<float>(type: "real", nullable: false),
                    USRSId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RecognitionResult", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RecognitionResult_USRS_USRSId",
                        column: x => x.USRSId,
                        principalTable: "USRS",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateTable(
                name: "TextBlock",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Text = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TextType = table.Column<int>(type: "int", nullable: false),
                    IsBenchmark = table.Column<bool>(type: "bit", nullable: false),
                    X = table.Column<int>(type: "int", nullable: false),
                    Y = table.Column<int>(type: "int", nullable: false),
                    Width = table.Column<int>(type: "int", nullable: false),
                    Height = table.Column<int>(type: "int", nullable: false),
                    TuningId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    InFileId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TextBlock", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TextBlock_InFile_InFileId",
                        column: x => x.InFileId,
                        principalTable: "InFile",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "FK_TextBlock_Tuning_TuningId",
                        column: x => x.TuningId,
                        principalTable: "Tuning",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateIndex(
                name: "IX_InFile_TuningId",
                table: "InFile",
                column: "TuningId");

            migrationBuilder.CreateIndex(
                name: "IX_InFile_USRSId",
                table: "InFile",
                column: "USRSId");

            migrationBuilder.CreateIndex(
                name: "IX_RecognitionResult_USRSId",
                table: "RecognitionResult",
                column: "USRSId",
                unique: true,
                filter: "[USRSId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_TextBlock_InFileId",
                table: "TextBlock",
                column: "InFileId");

            migrationBuilder.CreateIndex(
                name: "IX_TextBlock_TuningId",
                table: "TextBlock",
                column: "TuningId");

            migrationBuilder.CreateIndex(
                name: "IX_Tuning_RecognitionModelId",
                table: "Tuning",
                column: "RecognitionModelId");

            migrationBuilder.CreateIndex(
                name: "IX_Tuning_UserId",
                table: "Tuning",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_USRS_RecognitionModelId",
                table: "USRS",
                column: "RecognitionModelId");

            migrationBuilder.CreateIndex(
                name: "IX_USRS_UserId",
                table: "USRS",
                column: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "RecognitionResult");

            migrationBuilder.DropTable(
                name: "TextBlock");

            migrationBuilder.DropTable(
                name: "InFile");

            migrationBuilder.DropTable(
                name: "Tuning");

            migrationBuilder.DropTable(
                name: "USRS");

            migrationBuilder.DropTable(
                name: "RecognitionModel");

            migrationBuilder.DropTable(
                name: "User");
        }
    }
}
