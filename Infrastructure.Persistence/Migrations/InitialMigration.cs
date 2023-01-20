using FluentMigrator;

namespace Infrastructure.Persistence.Migrations
{
    [Migration(0)]
    public class InitialMigration : Migration
    {
        public override void Up()
        {
            Create.Table("Products")
                .WithColumn("Id").AsInt32().PrimaryKey().Identity().NotNullable()
                .WithColumn("Name").AsString().Indexed().NotNullable()
                .WithColumn("Barcode").AsString().Indexed().NotNullable()
                .WithColumn("Description").AsString().NotNullable()
                .WithColumn("Rate").AsDecimal().NotNullable()
                .WithAuditable();

            Create.Table("FileServices")
                .WithColumn("Id").AsInt32().PrimaryKey().Identity().NotNullable()
                .WithColumn("FileServiceName").AsString().NotNullable();
            Create.Table("Files")
                .WithColumn("Id").AsGuid().PrimaryKey().NotNullable()
                .WithColumn("FileName").AsString().Indexed().NotNullable()
                .WithColumn("FileUrl").AsString().Indexed().NotNullable()
                .WithColumn("FileLenght").AsInt64().NotNullable()
                .WithColumn("FileServiceId").AsInt32().NotNullable().ForeignKey("FileServices","Id")
                .WithAuditable();
            
        }

        public override void Down()
        {
            Delete.Table("Products");
        }
    }
}