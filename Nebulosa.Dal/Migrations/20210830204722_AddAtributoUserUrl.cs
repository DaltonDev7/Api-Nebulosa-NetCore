using Microsoft.EntityFrameworkCore.Migrations;

namespace Nebulosa.Dal.Migrations
{
    public partial class AddAtributoUserUrl : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "UserUrl",
                table: "Usuarios",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UserUrl",
                table: "Usuarios");
        }
    }
}
