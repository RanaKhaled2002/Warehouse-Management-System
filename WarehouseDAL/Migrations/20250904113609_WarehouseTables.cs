using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WarehouseDAL.Migrations
{
    /// <inheritdoc />
    public partial class WarehouseTables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Items",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    BarCode = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Items", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Lots",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    LotCode = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    ShipmentStatus = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LotStatus = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Lots", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Warehouses",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Location = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Warehouses", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Pickings",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Description = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    PickingDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    WarehouseId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pickings", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Pickings_Warehouses_WarehouseId",
                        column: x => x.WarehouseId,
                        principalTable: "Warehouses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Transfers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PickingId = table.Column<int>(type: "int", nullable: false),
                    SourceWarehouseId = table.Column<int>(type: "int", nullable: false),
                    DestinationWarehouseId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Transfers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Transfers_Pickings_PickingId",
                        column: x => x.PickingId,
                        principalTable: "Pickings",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Transfers_Warehouses_DestinationWarehouseId",
                        column: x => x.DestinationWarehouseId,
                        principalTable: "Warehouses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Transfers_Warehouses_SourceWarehouseId",
                        column: x => x.SourceWarehouseId,
                        principalTable: "Warehouses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Boxes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Label = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    TransferId = table.Column<int>(type: "int", nullable: false),
                    LotId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Boxes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Boxes_Lots_LotId",
                        column: x => x.LotId,
                        principalTable: "Lots",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "FK_Boxes_Transfers_TransferId",
                        column: x => x.TransferId,
                        principalTable: "Transfers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TransferItems",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RequestedQty = table.Column<int>(type: "int", nullable: false),
                    ScannedQty = table.Column<int>(type: "int", nullable: false),
                    TransferId = table.Column<int>(type: "int", nullable: false),
                    ItemId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TransferItems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TransferItems_Items_ItemId",
                        column: x => x.ItemId,
                        principalTable: "Items",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TransferItems_Transfers_TransferId",
                        column: x => x.TransferId,
                        principalTable: "Transfers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "BoxItems",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    BoxId = table.Column<int>(type: "int", nullable: false),
                    ItemId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BoxItems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BoxItems_Boxes_BoxId",
                        column: x => x.BoxId,
                        principalTable: "Boxes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BoxItems_Items_ItemId",
                        column: x => x.ItemId,
                        principalTable: "Items",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Boxes_Label",
                table: "Boxes",
                column: "Label",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Boxes_LotId",
                table: "Boxes",
                column: "LotId");

            migrationBuilder.CreateIndex(
                name: "IX_Boxes_TransferId",
                table: "Boxes",
                column: "TransferId");

            migrationBuilder.CreateIndex(
                name: "IX_BoxItems_BoxId",
                table: "BoxItems",
                column: "BoxId");

            migrationBuilder.CreateIndex(
                name: "IX_BoxItems_ItemId",
                table: "BoxItems",
                column: "ItemId");

            migrationBuilder.CreateIndex(
                name: "IX_Items_BarCode",
                table: "Items",
                column: "BarCode",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Lots_LotCode",
                table: "Lots",
                column: "LotCode",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Pickings_WarehouseId",
                table: "Pickings",
                column: "WarehouseId");

            migrationBuilder.CreateIndex(
                name: "IX_TransferItems_ItemId",
                table: "TransferItems",
                column: "ItemId");

            migrationBuilder.CreateIndex(
                name: "IX_TransferItems_TransferId",
                table: "TransferItems",
                column: "TransferId");

            migrationBuilder.CreateIndex(
                name: "IX_Transfers_DestinationWarehouseId",
                table: "Transfers",
                column: "DestinationWarehouseId");

            migrationBuilder.CreateIndex(
                name: "IX_Transfers_PickingId",
                table: "Transfers",
                column: "PickingId");

            migrationBuilder.CreateIndex(
                name: "IX_Transfers_SourceWarehouseId",
                table: "Transfers",
                column: "SourceWarehouseId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BoxItems");

            migrationBuilder.DropTable(
                name: "TransferItems");

            migrationBuilder.DropTable(
                name: "Boxes");

            migrationBuilder.DropTable(
                name: "Items");

            migrationBuilder.DropTable(
                name: "Lots");

            migrationBuilder.DropTable(
                name: "Transfers");

            migrationBuilder.DropTable(
                name: "Pickings");

            migrationBuilder.DropTable(
                name: "Warehouses");
        }
    }
}
