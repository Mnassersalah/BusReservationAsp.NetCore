using Microsoft.EntityFrameworkCore.Migrations;

namespace BusSystem.Migrations
{
    public partial class DataSeeding : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] {
                                "Id",
                                "ClientName",
                                "UserName",
                                "NormalizedUserName",
                                "Email",
                                "NormalizedEmail",
                                "EmailConfirmed",
                                "PasswordHash",
                                "SecurityStamp",
                                "ConcurrencyStamp",
                                "PhoneNumber",
                                "PhoneNumberConfirmed",
                                "TwoFactorEnabled",
                                "LockoutEnd",
                                "LockoutEnabled",
                                "AccessFailedCount"
                },
                values: new object[] {
                                "4da77c21-83a3-4d39-ab9f-322c3b7f2cbc",
                                "Admin",
                                "Admin@ByBus.com",
                                "ADMIN@BYBUS.COM",
                                "Admin@ByBus.com",
                                "ADMIN@BYBUS.COM",
                                false,
                                "AQAAAAEAACcQAAAAEMqkMaizZM9FJE4uVT9VN6LkBPfR+Fso7GWpEVHc+gVj8XPCabtMD7ou0R9uHftpeA==",
                                "IWX32XRDPAUWNSW2QNXAR6NZWVNMNNWT",
                                "077d5b40-dbc1-4574-85f1-c294e6d7893c",
                                null,
                                false,
                                false,
                                null,
                                true,
                                0
                });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] {
                    "Id",
                    "Name",
                    "NormalizedName",
                    "ConcurrencyStamp"
                },
                values: new object[] {
                    "c76499ff-fe1f-4513-b46c-3d28294e0cc8",
                    "Admin",
                    "ADMIN",
                    "ee989933-20f5-4d7b-91eb-ecc056bf6129"
                });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] {
                    "Id",
                    "Name",
                    "NormalizedName",
                    "ConcurrencyStamp"
                },
                values: new object[] {
                    "82b91c06-e050-431c-9e40-5b3e15286b2a",
                    "Employee",
                    "EMPLOYEE",
                    "73705231-424e-49a5-bb1f-10d70e73f03f"
                });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] {
                    "UserId",
                    "RoleId"
                },
                values: new object[] {
                    "4da77c21-83a3-4d39-ab9f-322c3b7f2cbc",
                    "c76499ff-fe1f-4513-b46c-3d28294e0cc8"
                });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] {
                    "UserId",
                    "RoleId"
                },
                values: new object[] {
                    "4da77c21-83a3-4d39-ab9f-322c3b7f2cbc",
                    "82b91c06-e050-431c-9e40-5b3e15286b2a"
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
