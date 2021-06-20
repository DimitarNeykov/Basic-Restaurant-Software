using Microsoft.EntityFrameworkCore.Migrations;

namespace Restaurant.Data.Migrations
{
    public partial class AddRolesAndAdminProfile : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "e60e5ab5-8ed4-44de-8c9c-a3134f484844", "dff37999-a801-46d8-ae27-def0bfc007e3", "Administrator", "ADMINISTRATOR" },
                    { "64cdbe7e-f60d-469f-b4c3-7b1b5bb44383", "d9da9a8f-ce48-45ba-9ab2-9bd660a5db0b", "Manager", "MANAGER" },
                    { "ca17aba3-5499-476e-8794-839d84c9aa87", "10be2f84-5294-4d15-9bc8-21cc796b0ed1", "Waiter", "WAITER" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "RestaurantInfoId", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "0631dfbf-82d5-4a00-9368-edcfd7aec44d", 0, "9085d83d-c1a5-4e7f-9f05-0a9498721fdc", "admin@restaurant.bg", true, false, null, "ADMIN@RESTAURANT.BG", "ADMIN", "AQAAAAEAACcQAAAAEIL5VDC8HCV6JwPHuwma4o7+duK7gATZJEAxSx63sM6cmfYpfYMk7yNFKzUacINKbw==", null, false, null, "20b05f1d-d209-46ac-9dd8-08456378e0fc", false, "Admin" });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "UserId", "RoleId" },
                values: new object[] { "0631dfbf-82d5-4a00-9368-edcfd7aec44d", "e60e5ab5-8ed4-44de-8c9c-a3134f484844" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "64cdbe7e-f60d-469f-b4c3-7b1b5bb44383");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "ca17aba3-5499-476e-8794-839d84c9aa87");

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "UserId", "RoleId" },
                keyValues: new object[] { "0631dfbf-82d5-4a00-9368-edcfd7aec44d", "e60e5ab5-8ed4-44de-8c9c-a3134f484844" });

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "e60e5ab5-8ed4-44de-8c9c-a3134f484844");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "0631dfbf-82d5-4a00-9368-edcfd7aec44d");
        }
    }
}
