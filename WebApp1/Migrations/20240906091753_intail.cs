using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebApp1.Migrations
{
	/// <inheritdoc />
	public partial class intail : Migration
	{
		/// <inheritdoc />
		protected override void Up(MigrationBuilder migrationBuilder)
		{
			migrationBuilder.CreateTable(
				name: "Users",
				columns: table => new
				{
					Id = table.Column<int>(type: "int", nullable: false)
						.Annotation("SqlServer:Identity", "1, 1"),
					Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
					Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
					Password = table.Column<string>(type: "nvarchar(max)", nullable: false)
				},
				constraints: table =>
				{
					table.PrimaryKey("PK_Users", x => x.Id);
				});

			migrationBuilder.CreateTable(
				name: "DetailsUser",
				columns: table => new
				{
					Id = table.Column<int>(type: "int", nullable: false)
						.Annotation("SqlServer:Identity", "1, 1"),
					NameTacket = table.Column<string>(type: "nvarchar(max)", nullable: false),
					TimeMatch = table.Column<DateTime>(type: "datetime2", nullable: false),
					Url = table.Column<string>(type: "nvarchar(max)", nullable: false),
					Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
					Palce = table.Column<string>(type: "nvarchar(max)", nullable: false),
					UserId = table.Column<int>(type: "int", nullable: false)
				},
				constraints: table =>
				{
					table.PrimaryKey("PK_DetailsUser", x => x.Id);
					// Adding Foreign Key constraint for UserId
					table.ForeignKey(
						name: "FK_DetailsUser_Users_UserId",
						column: x => x.UserId,
						principalTable: "Users",
						principalColumn: "Id",
						onDelete: ReferentialAction.Cascade);  // Optional: Adjust on delete behavior
				});

			migrationBuilder.CreateTable(
				name: "Tackets",
				columns: table => new
				{
					Id = table.Column<int>(type: "int", nullable: false)
						.Annotation("SqlServer:Identity", "1, 1"),
					NameTacket = table.Column<string>(type: "nvarchar(max)", nullable: false),
					TimeMatch = table.Column<DateTime>(type: "datetime2", nullable: false),
					Url = table.Column<string>(type: "nvarchar(max)", nullable: false),
					Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
					Palce = table.Column<string>(type: "nvarchar(max)", nullable: false)
				},
				constraints: table =>
				{
					table.PrimaryKey("PK_Tackets", x => x.Id);
				});

			// Creating index for faster lookups on foreign key columns
			migrationBuilder.CreateIndex(
				name: "IX_DetailsUser_UserId",
				table: "DetailsUser",
				column: "UserId");
		}

		/// <inheritdoc />
		protected override void Down(MigrationBuilder migrationBuilder)
		{
			migrationBuilder.DropTable(
				name: "DetailsUser");

			migrationBuilder.DropTable(
				name: "Tackets");

			migrationBuilder.DropTable(
				name: "Users");
		}
	}
}
