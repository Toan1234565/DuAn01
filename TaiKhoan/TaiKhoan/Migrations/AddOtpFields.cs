using Microsoft.EntityFrameworkCore.Migrations;

public partial class AddOtpFields : Migration
{
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.AddColumn<string>("OtpCode", "TaiKHoans", nullable: true);
        migrationBuilder.AddColumn<DateTime>("OtpExpires", "TaiKHoans", nullable: true);
        migrationBuilder.AddColumn<bool>("IsVerified", "TaiKHoans", defaultValue: false);
    }

    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropColumn("OtpCode", "TaiKHoans");
        migrationBuilder.DropColumn("OtpExpires", "TaiKHoans");
        migrationBuilder.DropColumn("IsVerified", "TaiKHoans");
    }
}
