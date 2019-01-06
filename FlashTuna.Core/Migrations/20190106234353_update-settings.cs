using Microsoft.EntityFrameworkCore.Migrations;

namespace FlashTuna.Core.Migrations
{
    public partial class updatesettings : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TrackedMethod_Setting_ConfigurationId",
                table: "TrackedMethod");

            migrationBuilder.DropPrimaryKey(
                name: "PK_TrackedMethod",
                table: "TrackedMethod");

            migrationBuilder.DropIndex(
                name: "IX_TrackedMethod_ConfigurationId",
                table: "TrackedMethod");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Setting",
                table: "Setting");

            migrationBuilder.DropColumn(
                name: "ConfigurationId",
                table: "TrackedMethod");

            migrationBuilder.RenameTable(
                name: "TrackedMethod",
                newName: "TrackedMethods");

            migrationBuilder.RenameTable(
                name: "Setting",
                newName: "Settings");

            migrationBuilder.AddColumn<string>(
                name: "ClassName",
                table: "TrackedMethods",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "SettingId",
                table: "TrackedMethods",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_TrackedMethods",
                table: "TrackedMethods",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Settings",
                table: "Settings",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_TrackedMethods_SettingId",
                table: "TrackedMethods",
                column: "SettingId");

            migrationBuilder.AddForeignKey(
                name: "FK_TrackedMethods_Settings_SettingId",
                table: "TrackedMethods",
                column: "SettingId",
                principalTable: "Settings",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TrackedMethods_Settings_SettingId",
                table: "TrackedMethods");

            migrationBuilder.DropPrimaryKey(
                name: "PK_TrackedMethods",
                table: "TrackedMethods");

            migrationBuilder.DropIndex(
                name: "IX_TrackedMethods_SettingId",
                table: "TrackedMethods");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Settings",
                table: "Settings");

            migrationBuilder.DropColumn(
                name: "ClassName",
                table: "TrackedMethods");

            migrationBuilder.DropColumn(
                name: "SettingId",
                table: "TrackedMethods");

            migrationBuilder.RenameTable(
                name: "TrackedMethods",
                newName: "TrackedMethod");

            migrationBuilder.RenameTable(
                name: "Settings",
                newName: "Setting");

            migrationBuilder.AddColumn<long>(
                name: "ConfigurationId",
                table: "TrackedMethod",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddPrimaryKey(
                name: "PK_TrackedMethod",
                table: "TrackedMethod",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Setting",
                table: "Setting",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_TrackedMethod_ConfigurationId",
                table: "TrackedMethod",
                column: "ConfigurationId");

            migrationBuilder.AddForeignKey(
                name: "FK_TrackedMethod_Setting_ConfigurationId",
                table: "TrackedMethod",
                column: "ConfigurationId",
                principalTable: "Setting",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
