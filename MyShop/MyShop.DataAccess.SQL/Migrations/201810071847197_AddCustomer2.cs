namespace MyShop.DataAccess.SQL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddCustomer2 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Customers", "FirstName", c => c.String(maxLength: 50));
            AlterColumn("dbo.Customers", "LastName", c => c.String(maxLength: 100));
            AlterColumn("dbo.Customers", "Email", c => c.String(maxLength: 100));
            AlterColumn("dbo.Customers", "Street", c => c.String(maxLength: 100));
            AlterColumn("dbo.Customers", "City", c => c.String(maxLength: 100));
            AlterColumn("dbo.Customers", "State", c => c.String(maxLength: 100));
            AlterColumn("dbo.Customers", "ZipCode", c => c.String(maxLength: 6));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Customers", "ZipCode", c => c.String());
            AlterColumn("dbo.Customers", "State", c => c.String());
            AlterColumn("dbo.Customers", "City", c => c.String());
            AlterColumn("dbo.Customers", "Street", c => c.String());
            AlterColumn("dbo.Customers", "Email", c => c.String());
            AlterColumn("dbo.Customers", "LastName", c => c.String());
            AlterColumn("dbo.Customers", "FirstName", c => c.String());
        }
    }
}
