using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WalletWebApp.Migrations
{
    /// <inheritdoc />
    public partial class FixAddWalletServiceProviderAndClientAccount : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Transactions_WalletServiceClientAccount_WalletServiceClient~",
                table: "Transactions");

            migrationBuilder.DropForeignKey(
                name: "FK_WalletServiceClientAccount_WalletServiceProvider_WalletServ~",
                table: "WalletServiceClientAccount");

            migrationBuilder.DropPrimaryKey(
                name: "PK_WalletServiceClientAccount",
                table: "WalletServiceClientAccount");

            migrationBuilder.RenameTable(
                name: "WalletServiceClientAccount",
                newName: "WalletServiceClientAccounts");

            migrationBuilder.RenameIndex(
                name: "IX_WalletServiceClientAccount_WalletServiceProviderId",
                table: "WalletServiceClientAccounts",
                newName: "IX_WalletServiceClientAccounts_WalletServiceProviderId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_WalletServiceClientAccounts",
                table: "WalletServiceClientAccounts",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Transactions_WalletServiceClientAccounts_WalletServiceClien~",
                table: "Transactions",
                column: "WalletServiceClientAccountId",
                principalTable: "WalletServiceClientAccounts",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_WalletServiceClientAccounts_WalletServiceProvider_WalletSer~",
                table: "WalletServiceClientAccounts",
                column: "WalletServiceProviderId",
                principalTable: "WalletServiceProvider",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Transactions_WalletServiceClientAccounts_WalletServiceClien~",
                table: "Transactions");

            migrationBuilder.DropForeignKey(
                name: "FK_WalletServiceClientAccounts_WalletServiceProvider_WalletSer~",
                table: "WalletServiceClientAccounts");

            migrationBuilder.DropPrimaryKey(
                name: "PK_WalletServiceClientAccounts",
                table: "WalletServiceClientAccounts");

            migrationBuilder.RenameTable(
                name: "WalletServiceClientAccounts",
                newName: "WalletServiceClientAccount");

            migrationBuilder.RenameIndex(
                name: "IX_WalletServiceClientAccounts_WalletServiceProviderId",
                table: "WalletServiceClientAccount",
                newName: "IX_WalletServiceClientAccount_WalletServiceProviderId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_WalletServiceClientAccount",
                table: "WalletServiceClientAccount",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Transactions_WalletServiceClientAccount_WalletServiceClient~",
                table: "Transactions",
                column: "WalletServiceClientAccountId",
                principalTable: "WalletServiceClientAccount",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_WalletServiceClientAccount_WalletServiceProvider_WalletServ~",
                table: "WalletServiceClientAccount",
                column: "WalletServiceProviderId",
                principalTable: "WalletServiceProvider",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
