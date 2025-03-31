using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace IRIS_Web_Rec25_241EC152.Migrations
{
    /// <inheritdoc />
    public partial class UpdateModelsAndData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Category",
                table: "Equipment",
                newName: "Type");

            migrationBuilder.AlterColumn<int>(
                name: "Status",
                table: "Equipment",
                type: "INTEGER",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "TEXT");

            migrationBuilder.UpdateData(
                table: "Courts",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Location", "OpeningTime" },
                values: new object[] { "Old Sports Complex", new TimeSpan(0, 7, 0, 0, 0) });

            migrationBuilder.UpdateData(
                table: "Courts",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "Availability", "Location", "OpeningTime" },
                values: new object[] { 0, "Old Sports Complex", new TimeSpan(0, 7, 0, 0, 0) });

            migrationBuilder.UpdateData(
                table: "Courts",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "Availability", "Capacity", "ClosingTime", "Location", "Name", "Type" },
                values: new object[] { 1, 4, new TimeSpan(0, 21, 0, 0, 0), "Old Sports Complex", "Badminton Court 3", "Badminton" });

            migrationBuilder.UpdateData(
                table: "Courts",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "Capacity", "ClosingTime", "Location", "Name", "OpeningTime", "Type" },
                values: new object[] { 2, new TimeSpan(0, 20, 0, 0, 0), "Opposite Mechaical Department", "Tennis Court 1", new TimeSpan(0, 7, 0, 0, 0), "Tennis" });

            migrationBuilder.UpdateData(
                table: "Courts",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "Availability", "Capacity", "ClosingTime", "Location", "Name", "OpeningTime", "Type" },
                values: new object[] { 0, 10, new TimeSpan(0, 21, 0, 0, 0), "Opposite Mechaical Department", "Basketball Court", new TimeSpan(0, 6, 0, 0, 0), "Basketball" });

            migrationBuilder.UpdateData(
                table: "Courts",
                keyColumn: "Id",
                keyValue: 6,
                columns: new[] { "Availability", "ClosingTime", "Location", "Name", "OpeningTime", "Type" },
                values: new object[] { 2, new TimeSpan(0, 22, 0, 0, 0), "New Sports Complex", "Squash Court 1", new TimeSpan(0, 10, 0, 0, 0), "Squash" });

            migrationBuilder.UpdateData(
                table: "Courts",
                keyColumn: "Id",
                keyValue: 7,
                columns: new[] { "Availability", "ClosingTime", "Location", "Name", "Type" },
                values: new object[] { 0, new TimeSpan(0, 20, 0, 0, 0), "New Sports Complex", "Table Tennis Area", "Table Tennis" });

            migrationBuilder.InsertData(
                table: "Courts",
                columns: new[] { "Id", "Availability", "Capacity", "ClosingTime", "Location", "Name", "OpeningTime", "Type" },
                values: new object[] { 8, 3, 2, new TimeSpan(0, 18, 0, 0, 0), "New Sports Complex", "Kabaddi Room", new TimeSpan(0, 8, 0, 0, 0), "Kabaddi" });

            migrationBuilder.UpdateData(
                table: "Equipment",
                keyColumn: "Id",
                keyValue: 1,
                column: "Status",
                value: 1);

            migrationBuilder.UpdateData(
                table: "Equipment",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "AvailableQuantity", "Status" },
                values: new object[] { 10, 1 });

            migrationBuilder.UpdateData(
                table: "Equipment",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "AvailableQuantity", "Status" },
                values: new object[] { 8, 1 });

            migrationBuilder.UpdateData(
                table: "Equipment",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "AvailableQuantity", "Status" },
                values: new object[] { 4, 1 });

            migrationBuilder.UpdateData(
                table: "Equipment",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "AvailableQuantity", "Status" },
                values: new object[] { 6, 1 });

            migrationBuilder.UpdateData(
                table: "Equipment",
                keyColumn: "Id",
                keyValue: 6,
                columns: new[] { "AvailableQuantity", "Status" },
                values: new object[] { 12, 1 });

            migrationBuilder.UpdateData(
                table: "Equipment",
                keyColumn: "Id",
                keyValue: 8,
                columns: new[] { "AvailableQuantity", "Status" },
                values: new object[] { 4, 1 });

            migrationBuilder.UpdateData(
                table: "Equipment",
                keyColumn: "Id",
                keyValue: 9,
                columns: new[] { "AvailableQuantity", "Status" },
                values: new object[] { 10, 1 });

            migrationBuilder.UpdateData(
                table: "Equipment",
                keyColumn: "Id",
                keyValue: 10,
                columns: new[] { "AvailableQuantity", "Status" },
                values: new object[] { 5, 1 });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Courts",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.RenameColumn(
                name: "Type",
                table: "Equipment",
                newName: "Category");

            migrationBuilder.AlterColumn<string>(
                name: "Status",
                table: "Equipment",
                type: "TEXT",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "INTEGER");

            migrationBuilder.UpdateData(
                table: "Courts",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Location", "OpeningTime" },
                values: new object[] { "Sports Complex - North Wing", new TimeSpan(0, 8, 0, 0, 0) });

            migrationBuilder.UpdateData(
                table: "Courts",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "Availability", "Location", "OpeningTime" },
                values: new object[] { 1, "Sports Complex - North Wing", new TimeSpan(0, 9, 0, 0, 0) });

            migrationBuilder.UpdateData(
                table: "Courts",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "Availability", "Capacity", "ClosingTime", "Location", "Name", "Type" },
                values: new object[] { 0, 2, new TimeSpan(0, 20, 0, 0, 0), "Outdoor Facility - East Side", "Tennis Court 1", "Tennis" });

            migrationBuilder.UpdateData(
                table: "Courts",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "Capacity", "ClosingTime", "Location", "Name", "OpeningTime", "Type" },
                values: new object[] { 10, new TimeSpan(0, 23, 0, 0, 0), "Main Arena", "Basketball Court", new TimeSpan(0, 6, 0, 0, 0), "Basketball" });

            migrationBuilder.UpdateData(
                table: "Courts",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "Availability", "Capacity", "ClosingTime", "Location", "Name", "OpeningTime", "Type" },
                values: new object[] { 2, 2, new TimeSpan(0, 22, 0, 0, 0), "Indoor Facility - Level 2", "Squash Court 1", new TimeSpan(0, 10, 0, 0, 0), "Squash" });

            migrationBuilder.UpdateData(
                table: "Courts",
                keyColumn: "Id",
                keyValue: 6,
                columns: new[] { "Availability", "ClosingTime", "Location", "Name", "OpeningTime", "Type" },
                values: new object[] { 0, new TimeSpan(0, 20, 0, 0, 0), "Recreation Zone", "Table Tennis Area", new TimeSpan(0, 8, 0, 0, 0), "Table Tennis" });

            migrationBuilder.UpdateData(
                table: "Courts",
                keyColumn: "Id",
                keyValue: 7,
                columns: new[] { "Availability", "ClosingTime", "Location", "Name", "Type" },
                values: new object[] { 3, new TimeSpan(0, 18, 0, 0, 0), "South Field", "Outdoor Tennis Court", "Tennis" });

            migrationBuilder.UpdateData(
                table: "Equipment",
                keyColumn: "Id",
                keyValue: 1,
                column: "Status",
                value: "Available");

            migrationBuilder.UpdateData(
                table: "Equipment",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "AvailableQuantity", "Status" },
                values: new object[] { 14, "Available" });

            migrationBuilder.UpdateData(
                table: "Equipment",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "AvailableQuantity", "Status" },
                values: new object[] { 14, "Available" });

            migrationBuilder.UpdateData(
                table: "Equipment",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "AvailableQuantity", "Status" },
                values: new object[] { 14, "Available" });

            migrationBuilder.UpdateData(
                table: "Equipment",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "AvailableQuantity", "Status" },
                values: new object[] { 14, "Available" });

            migrationBuilder.UpdateData(
                table: "Equipment",
                keyColumn: "Id",
                keyValue: 6,
                columns: new[] { "AvailableQuantity", "Status" },
                values: new object[] { 14, "Available" });

            migrationBuilder.UpdateData(
                table: "Equipment",
                keyColumn: "Id",
                keyValue: 8,
                columns: new[] { "AvailableQuantity", "Status" },
                values: new object[] { 14, "Available" });

            migrationBuilder.UpdateData(
                table: "Equipment",
                keyColumn: "Id",
                keyValue: 9,
                columns: new[] { "AvailableQuantity", "Status" },
                values: new object[] { 14, "Available" });

            migrationBuilder.UpdateData(
                table: "Equipment",
                keyColumn: "Id",
                keyValue: 10,
                columns: new[] { "AvailableQuantity", "Status" },
                values: new object[] { 14, "Available" });
        }
    }
}
