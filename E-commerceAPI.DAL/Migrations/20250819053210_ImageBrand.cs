using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace E_commerceAPI.DAL.Migrations
{
    /// <inheritdoc />
    public partial class ImageBrand : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ImageMain",
                table: "Brands",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ImageMain",
                table: "Brands");
        }
    }
}
