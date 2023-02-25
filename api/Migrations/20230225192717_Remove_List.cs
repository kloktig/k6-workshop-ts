using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace api.Migrations
{
    /// <inheritdoc />
    public partial class Remove_List : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_items_todoList_TodoListTodoId",
                table: "items");

            migrationBuilder.DropTable(
                name: "todoList");

            migrationBuilder.DropPrimaryKey(
                name: "PK_items",
                table: "items");

            migrationBuilder.DropIndex(
                name: "IX_items_TodoListTodoId",
                table: "items");

            migrationBuilder.DropColumn(
                name: "TodoListTodoId",
                table: "items");

            migrationBuilder.RenameTable(
                name: "items",
                newName: "TodoItems");

            migrationBuilder.AddPrimaryKey(
                name: "PK_TodoItems",
                table: "TodoItems",
                column: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_TodoItems",
                table: "TodoItems");

            migrationBuilder.RenameTable(
                name: "TodoItems",
                newName: "items");

            migrationBuilder.AddColumn<int>(
                name: "TodoListTodoId",
                table: "items",
                type: "int",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_items",
                table: "items",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "todoList",
                columns: table => new
                {
                    TodoId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_todoList", x => x.TodoId);
                });

            migrationBuilder.CreateIndex(
                name: "IX_items_TodoListTodoId",
                table: "items",
                column: "TodoListTodoId");

            migrationBuilder.AddForeignKey(
                name: "FK_items_todoList_TodoListTodoId",
                table: "items",
                column: "TodoListTodoId",
                principalTable: "todoList",
                principalColumn: "TodoId");
        }
    }
}
