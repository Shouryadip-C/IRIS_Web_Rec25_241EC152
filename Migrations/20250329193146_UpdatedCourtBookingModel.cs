using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace IRIS_Web_Rec25_241EC152.Migrations
{
    /// <inheritdoc />
    public partial class UpdatedCourtBookingModel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "HourSlot",
                table: "EquipmentBookings",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "RejectionReason",
                table: "EquipmentBookings",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<bool>(
                name: "ReminderSent",
                table: "EquipmentBookings",
                type: "INTEGER",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "RejectionReason",
                table: "CourtBookings",
                type: "TEXT",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "HourSlot",
                table: "EquipmentBookings");

            migrationBuilder.DropColumn(
                name: "RejectionReason",
                table: "EquipmentBookings");

            migrationBuilder.DropColumn(
                name: "ReminderSent",
                table: "EquipmentBookings");

            migrationBuilder.DropColumn(
                name: "RejectionReason",
                table: "CourtBookings");
        }
    }
}
