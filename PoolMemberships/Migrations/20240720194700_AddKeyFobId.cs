using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PoolMemberships.Migrations
{
    /// <inheritdoc />
    public partial class AddKeyFobId : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "KeyFobId",
                table: "Memberships",
                type: "TEXT",
                maxLength: 50,
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "KeyFobId",
                table: "Memberships");
        }
    }
}
