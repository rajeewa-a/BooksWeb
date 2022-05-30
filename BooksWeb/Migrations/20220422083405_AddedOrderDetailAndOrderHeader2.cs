using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BooksWeb.Migrations
{
    public partial class AddedOrderDetailAndOrderHeader2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OrderDetails_orderHeaders_OrderId",
                table: "OrderDetails");

            migrationBuilder.DropForeignKey(
                name: "FK_orderHeaders_AspNetUsers_AppUserId",
                table: "orderHeaders");

            migrationBuilder.DropPrimaryKey(
                name: "PK_orderHeaders",
                table: "orderHeaders");

            migrationBuilder.RenameTable(
                name: "orderHeaders",
                newName: "OrderHeaders");

            migrationBuilder.RenameIndex(
                name: "IX_orderHeaders_AppUserId",
                table: "OrderHeaders",
                newName: "IX_OrderHeaders_AppUserId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_OrderHeaders",
                table: "OrderHeaders",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_OrderDetails_OrderHeaders_OrderId",
                table: "OrderDetails",
                column: "OrderId",
                principalTable: "OrderHeaders",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_OrderHeaders_AspNetUsers_AppUserId",
                table: "OrderHeaders",
                column: "AppUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OrderDetails_OrderHeaders_OrderId",
                table: "OrderDetails");

            migrationBuilder.DropForeignKey(
                name: "FK_OrderHeaders_AspNetUsers_AppUserId",
                table: "OrderHeaders");

            migrationBuilder.DropPrimaryKey(
                name: "PK_OrderHeaders",
                table: "OrderHeaders");

            migrationBuilder.RenameTable(
                name: "OrderHeaders",
                newName: "orderHeaders");

            migrationBuilder.RenameIndex(
                name: "IX_OrderHeaders_AppUserId",
                table: "orderHeaders",
                newName: "IX_orderHeaders_AppUserId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_orderHeaders",
                table: "orderHeaders",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_OrderDetails_orderHeaders_OrderId",
                table: "OrderDetails",
                column: "OrderId",
                principalTable: "orderHeaders",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_orderHeaders_AspNetUsers_AppUserId",
                table: "orderHeaders",
                column: "AppUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
