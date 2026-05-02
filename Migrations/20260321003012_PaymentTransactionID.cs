using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DnTech_ECommerce.Migrations
{
    /// <inheritdoc />
    public partial class PaymentTransactionID : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "PaymentTransactionId",
                table: "Orders",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PaymentTransactionId",
                table: "Orders");
        }
    }
}
