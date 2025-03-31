using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace IRIS_Web_Rec25_241EC152.Migrations
{
    /// <inheritdoc />
    public partial class EquipmentQuantitySimplified : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AvailableQuantity",
                table: "Equipments");

            migrationBuilder.RenameColumn(
                name: "TotalQuantity",
                table: "Equipments",
                newName: "Quantity");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Quantity",
                table: "Equipments",
                newName: "TotalQuantity");

            migrationBuilder.AddColumn<int>(
                name: "AvailableQuantity",
                table: "Equipments",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.UpdateData(
                table: "Equipments",
                keyColumn: "Id",
                keyValue: 1,
                column: "AvailableQuantity",
                value: 14);

            migrationBuilder.UpdateData(
                table: "Equipments",
                keyColumn: "Id",
                keyValue: 2,
                column: "AvailableQuantity",
                value: 10);

            migrationBuilder.UpdateData(
                table: "Equipments",
                keyColumn: "Id",
                keyValue: 3,
                column: "AvailableQuantity",
                value: 8);

            migrationBuilder.UpdateData(
                table: "Equipments",
                keyColumn: "Id",
                keyValue: 4,
                column: "AvailableQuantity",
                value: 4);

            migrationBuilder.UpdateData(
                table: "Equipments",
                keyColumn: "Id",
                keyValue: 5,
                column: "AvailableQuantity",
                value: 6);

            migrationBuilder.UpdateData(
                table: "Equipments",
                keyColumn: "Id",
                keyValue: 6,
                column: "AvailableQuantity",
                value: 12);

            migrationBuilder.UpdateData(
                table: "Equipments",
                keyColumn: "Id",
                keyValue: 8,
                column: "AvailableQuantity",
                value: 4);

            migrationBuilder.UpdateData(
                table: "Equipments",
                keyColumn: "Id",
                keyValue: 9,
                column: "AvailableQuantity",
                value: 10);

            migrationBuilder.UpdateData(
                table: "Equipments",
                keyColumn: "Id",
                keyValue: 10,
                column: "AvailableQuantity",
                value: 5);
        }
    }
}
