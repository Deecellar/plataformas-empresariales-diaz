using System;

namespace Infrastructure.Persistence
{
    public static class MigrationExtensions
    {
        public static FluentMigrator.Builders.Create.Table.ICreateTableColumnOptionOrWithColumnSyntax WithAuditable(this FluentMigrator.Builders.Create.Table.ICreateTableColumnOptionOrWithColumnSyntax root ) => 
        root.WithColumn("CreatedBy").AsString().NotNullable().Indexed()
                .WithColumn("Created").AsDateTime().NotNullable().Indexed()
                .WithColumn("LastModifiedBy").AsString().Nullable().Indexed()
                .WithColumn("LastModified").AsDateTime().Nullable().Indexed();
        
    }
}
