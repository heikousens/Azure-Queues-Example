using Microsoft.EntityFrameworkCore.Migrations;

namespace Livraria.Infraestrutura.Data.Migrations
{
    public partial class novaColuna : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "QtdVisualizacao",
                table: "Livro",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "QtdVisualizacao",
                table: "Livro");
        }
    }
}
