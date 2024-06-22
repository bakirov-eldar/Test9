using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace WalletWebApp.Migrations
{
    /// <inheritdoc />
    public partial class AddWalletServiceProviderAndClientAccount : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "WalletServiceClientAccountId",
                table: "Transactions",
                type: "integer",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "WalletServiceProvider",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WalletServiceProvider", x => x.Id);
                    table.ForeignKey(
                        name: "FK_WalletServiceProvider_AspNetUsers_Id",
                        column: x => x.Id,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "WalletServiceClientAccount",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    AccountNumber = table.Column<int>(type: "integer", nullable: false),
                    WalletServiceProviderId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WalletServiceClientAccount", x => x.Id);
                    table.ForeignKey(
                        name: "FK_WalletServiceClientAccount_WalletServiceProvider_WalletServ~",
                        column: x => x.WalletServiceProviderId,
                        principalTable: "WalletServiceProvider",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Transactions_WalletServiceClientAccountId",
                table: "Transactions",
                column: "WalletServiceClientAccountId");

            migrationBuilder.CreateIndex(
                name: "IX_WalletServiceClientAccount_WalletServiceProviderId",
                table: "WalletServiceClientAccount",
                column: "WalletServiceProviderId");

            migrationBuilder.AddForeignKey(
                name: "FK_Transactions_WalletServiceClientAccount_WalletServiceClient~",
                table: "Transactions",
                column: "WalletServiceClientAccountId",
                principalTable: "WalletServiceClientAccount",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Transactions_WalletServiceClientAccount_WalletServiceClient~",
                table: "Transactions");

            migrationBuilder.DropTable(
                name: "WalletServiceClientAccount");

            migrationBuilder.DropTable(
                name: "WalletServiceProvider");

            migrationBuilder.DropIndex(
                name: "IX_Transactions_WalletServiceClientAccountId",
                table: "Transactions");

            migrationBuilder.DropColumn(
                name: "WalletServiceClientAccountId",
                table: "Transactions");
        }
    }
}
