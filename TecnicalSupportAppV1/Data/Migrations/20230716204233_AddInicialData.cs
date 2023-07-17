using Microsoft.EntityFrameworkCore.Migrations;
using TecnicalSupportAppV1.Api.Models.Enums;
using TecnicalSupportAppV1.Api.Models;
using TecnicalSupportAppV1.Bussiness.Authentification;

#nullable disable

namespace TecnicalSupportAppV1.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddInicialData : Migration
    {
        private const string username = "superuser";
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            //Add Roles
            migrationBuilder.InsertData("Roles",
                columns: new[] { nameof(Roles.Rol), nameof(Roles.CreationDate), nameof(Roles.IsActive) },
                values: new object[,] {
                   { (int)RolesEnum.SuperAdmin, DateTime.UtcNow, true},
                   { (int)RolesEnum.Admin, DateTime.UtcNow, true},
                   { (int)RolesEnum.Client, DateTime.UtcNow, true},
                   { (int)RolesEnum.Technician, DateTime.UtcNow, true},
                    }
                );

            //MainUser
            PasswordHasher passwordHasher = new PasswordHasher();
            var passwordhash = passwordHasher.Hash("12345");
            migrationBuilder.InsertData("Users",
                columns: new[] { nameof(User.Username), nameof(User.Password), nameof(User.CreationDate), nameof(User.IsActive) },
                values: new object[,] {
                   { username, passwordhash, DateTime.UtcNow, true} }
                );

            //Assign Role to Super User
            migrationBuilder.InsertData("RolesUser",
                columns: new[] { "RolesId", "UsersId" },
                values: new object[,] {
                   { 1, 1 } }
                );
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData("Roles", nameof(Roles.Rol), (int)RolesEnum.SuperAdmin);
            migrationBuilder.DeleteData("Roles", nameof(Roles.Rol), (int)RolesEnum.Admin);
            migrationBuilder.DeleteData("Roles", nameof(Roles.Rol), (int)RolesEnum.Client);
            migrationBuilder.DeleteData("Roles", nameof(Roles.Rol), (int)RolesEnum.Technician);
            migrationBuilder.DeleteData("RolesUser", "RolesId", 1);
            migrationBuilder.DeleteData("Users", nameof(User.Username), username);
        }
    }
}