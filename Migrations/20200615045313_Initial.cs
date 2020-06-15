using Microsoft.EntityFrameworkCore.Migrations;

namespace Lambdatech.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Dads",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Dads", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Moms",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Moms", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Students",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: false),
                    MomId = table.Column<int>(nullable: false),
                    DadId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Students", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Students_Dads_DadId",
                        column: x => x.DadId,
                        principalTable: "Dads",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Students_Moms_MomId",
                        column: x => x.MomId,
                        principalTable: "Moms",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Schools",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: false),
                    StudentId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Schools", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Schools_Students_StudentId",
                        column: x => x.StudentId,
                        principalTable: "Students",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Dads",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "Juan Robles" },
                    { 2, "Rodrigo Lopez" }
                });

            migrationBuilder.InsertData(
                table: "Moms",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "Maria Stink" },
                    { 2, "Romina Tumbado" }
                });

            migrationBuilder.InsertData(
                table: "Students",
                columns: new[] { "Id", "DadId", "MomId", "Name" },
                values: new object[] { 1, 1, 1, "Mario Robles Stink" });

            migrationBuilder.InsertData(
                table: "Students",
                columns: new[] { "Id", "DadId", "MomId", "Name" },
                values: new object[] { 2, 2, 2, "Erick Lopez Tumbado" });

            migrationBuilder.InsertData(
                table: "Students",
                columns: new[] { "Id", "DadId", "MomId", "Name" },
                values: new object[] { 3, 2, 2, "Martin Lopez Tumbado" });

            migrationBuilder.InsertData(
                table: "Schools",
                columns: new[] { "Id", "Name", "StudentId" },
                values: new object[,]
                {
                    { 1, "Instituto Kipling", 1 },
                    { 2, "Instituto Benavente", 2 },
                    { 3, "Instituto Capistrano", 2 },
                    { 4, "Instituto Capistrano", 3 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Schools_StudentId",
                table: "Schools",
                column: "StudentId");

            migrationBuilder.CreateIndex(
                name: "IX_Students_DadId",
                table: "Students",
                column: "DadId");

            migrationBuilder.CreateIndex(
                name: "IX_Students_MomId",
                table: "Students",
                column: "MomId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Schools");

            migrationBuilder.DropTable(
                name: "Students");

            migrationBuilder.DropTable(
                name: "Dads");

            migrationBuilder.DropTable(
                name: "Moms");
        }
    }
}
