using Microsoft.EntityFrameworkCore.Migrations;

namespace CatalogMicroservice.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "InsurancePolicies",
                columns: table => new
                {
                    PolicyId = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    BasePrice = table.Column<double>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InsurancePolicies", x => x.PolicyId);
                });

            migrationBuilder.InsertData(
                table: "InsurancePolicies",
                columns: new[] { "PolicyId", "BasePrice", "Description", "Name" },
                values: new object[] { 1L, 106040.0, "This is the most basic policy, for families of size 1-3", "Miniature Policy" });

            migrationBuilder.InsertData(
                table: "InsurancePolicies",
                columns: new[] { "PolicyId", "BasePrice", "Description", "Name" },
                values: new object[] { 2L, 87640.0, "This is a policy for a family of size 4-6", "Small Sized Family Policy" });

            migrationBuilder.InsertData(
                table: "InsurancePolicies",
                columns: new[] { "PolicyId", "BasePrice", "Description", "Name" },
                values: new object[] { 3L, 76810.0, "This policy is for families consiting of 7-9 members", "Medium Sized Family Policy" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "InsurancePolicies");
        }
    }
}
