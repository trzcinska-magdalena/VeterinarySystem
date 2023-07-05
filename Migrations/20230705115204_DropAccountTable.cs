using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace VeterinarySystem.Migrations
{
    /// <inheritdoc />
    public partial class DropAccountTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {

            migrationBuilder.DropTable(
                name: "Account");

        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "AccountId",
                table: "Vet",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Account",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Admin = table.Column<bool>(type: "bit", nullable: false),
                    Login = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    Password = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Account", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Vet_AccountId",
                table: "Vet",
                column: "AccountId");

            migrationBuilder.AddForeignKey(
                name: "FK_Vet_Account_AccountId",
                table: "Vet",
                column: "AccountId",
                principalTable: "Account",
                principalColumn: "Id");
        }
    }
}
