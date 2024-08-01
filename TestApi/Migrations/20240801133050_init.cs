using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BaseApi.Migrations
{
    /// <inheritdoc />
    public partial class init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Customers",
                columns: table => new
                {
                    CustomerId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Oib = table.Column<string>(type: "nvarchar(12)", maxLength: 12, nullable: false),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Address = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    Email = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    Phone = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    Overdue = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Customers", x => x.CustomerId);
                });

            migrationBuilder.CreateTable(
                name: "Items",
                columns: table => new
                {
                    ItemId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Description = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Items", x => x.ItemId);
                });

            migrationBuilder.CreateTable(
                name: "StockJournals",
                columns: table => new
                {
                    StockJournalId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Category = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TransactionTypeId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StockJournals", x => x.StockJournalId);
                });

            migrationBuilder.CreateTable(
                name: "Transactions",
                columns: table => new
                {
                    TransactionId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TransactionDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DateDue = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Transactions", x => x.TransactionId);
                });

            migrationBuilder.CreateTable(
                name: "BaseInventory",
                columns: table => new
                {
                    BaseInventoryId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ItemId = table.Column<int>(type: "int", nullable: true),
                    QuantityStored = table.Column<int>(type: "int", nullable: false),
                    QuantityRented = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BaseInventory", x => x.BaseInventoryId);
                    table.ForeignKey(
                        name: "FK_BaseInventory_Items_ItemId",
                        column: x => x.ItemId,
                        principalTable: "Items",
                        principalColumn: "ItemId");
                });

            migrationBuilder.CreateTable(
                name: "Inventories",
                columns: table => new
                {
                    CustomerId = table.Column<int>(type: "int", nullable: false),
                    ItemId = table.Column<int>(type: "int", nullable: false),
                    InventoryId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Quantity = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Inventories", x => new { x.CustomerId, x.ItemId });
                    table.ForeignKey(
                        name: "FK_Inventories_Customers_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "Customers",
                        principalColumn: "CustomerId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Inventories_Items_ItemId",
                        column: x => x.ItemId,
                        principalTable: "Items",
                        principalColumn: "ItemId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Deliveries",
                columns: table => new
                {
                    StockDeliveryId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    InventoryCustomerId = table.Column<int>(type: "int", nullable: true),
                    InventoryItemId = table.Column<int>(type: "int", nullable: true),
                    QuantityDelivered = table.Column<int>(type: "int", nullable: false),
                    QuantityToReturn = table.Column<int>(type: "int", nullable: false),
                    TransactionInfoTransactionId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Deliveries", x => x.StockDeliveryId);
                    table.ForeignKey(
                        name: "FK_Deliveries_Inventories_InventoryCustomerId_InventoryItemId",
                        columns: x => new { x.InventoryCustomerId, x.InventoryItemId },
                        principalTable: "Inventories",
                        principalColumns: new[] { "CustomerId", "ItemId" });
                    table.ForeignKey(
                        name: "FK_Deliveries_Transactions_TransactionInfoTransactionId",
                        column: x => x.TransactionInfoTransactionId,
                        principalTable: "Transactions",
                        principalColumn: "TransactionId");
                });

            migrationBuilder.CreateTable(
                name: "Returns",
                columns: table => new
                {
                    StockReturnId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DeliveryStockDeliveryId = table.Column<int>(type: "int", nullable: true),
                    QuantityReturned = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Returns", x => x.StockReturnId);
                    table.ForeignKey(
                        name: "FK_Returns_Deliveries_DeliveryStockDeliveryId",
                        column: x => x.DeliveryStockDeliveryId,
                        principalTable: "Deliveries",
                        principalColumn: "StockDeliveryId");
                });

            migrationBuilder.CreateIndex(
                name: "IX_BaseInventory_ItemId",
                table: "BaseInventory",
                column: "ItemId");

            migrationBuilder.CreateIndex(
                name: "IX_Customers_Oib",
                table: "Customers",
                column: "Oib",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Deliveries_InventoryCustomerId_InventoryItemId",
                table: "Deliveries",
                columns: new[] { "InventoryCustomerId", "InventoryItemId" });

            migrationBuilder.CreateIndex(
                name: "IX_Deliveries_TransactionInfoTransactionId",
                table: "Deliveries",
                column: "TransactionInfoTransactionId");

            migrationBuilder.CreateIndex(
                name: "IX_Inventories_ItemId",
                table: "Inventories",
                column: "ItemId");

            migrationBuilder.CreateIndex(
                name: "IX_Returns_DeliveryStockDeliveryId",
                table: "Returns",
                column: "DeliveryStockDeliveryId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BaseInventory");

            migrationBuilder.DropTable(
                name: "Returns");

            migrationBuilder.DropTable(
                name: "StockJournals");

            migrationBuilder.DropTable(
                name: "Deliveries");

            migrationBuilder.DropTable(
                name: "Inventories");

            migrationBuilder.DropTable(
                name: "Transactions");

            migrationBuilder.DropTable(
                name: "Customers");

            migrationBuilder.DropTable(
                name: "Items");
        }
    }
}
