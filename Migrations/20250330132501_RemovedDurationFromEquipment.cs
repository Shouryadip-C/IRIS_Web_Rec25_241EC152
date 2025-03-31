using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace IRIS_Web_Rec25_241EC152.Migrations
{
    /// <inheritdoc />
    public partial class RemovedDurationFromEquipment : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DurationHours",
                table: "EquipmentBookings");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "DurationHours",
                table: "EquipmentBookings",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);
        }
    }
}
