using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CounterPartyDomain.Migrations
{
    public partial class NewMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Company",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ExternalId = table.Column<string>(maxLength: 50, nullable: false),
                    TradingName = table.Column<string>(nullable: false),
                    LegalName = table.Column<string>(nullable: false),
                    CompanyType = table.Column<int>(nullable: false),
                    Unused = table.Column<bool>(nullable: false),
                    IsForwarder = table.Column<bool>(nullable: false),
                    Phone = table.Column<string>(maxLength: 50, nullable: false),
                    Fax = table.Column<string>(maxLength: 50, nullable: false),
                    AddressId = table.Column<int>(nullable: true),
                    MailAddressId = table.Column<int>(nullable: true),
                    IsCustomClerance = table.Column<bool>(nullable: false),
                    IsActive = table.Column<bool>(nullable: false),
                    IsCarrier = table.Column<bool>(nullable: false),
                    IsWareHouse = table.Column<bool>(nullable: false),
                    ChamberOfCommerce = table.Column<string>(nullable: true),
                    ChamberOfCommerceCi = table.Column<string>(nullable: true),
                    CountryVAT = table.Column<string>(nullable: true),
                    IsExchangeBroker = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Company", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Company");
        }
    }
}
