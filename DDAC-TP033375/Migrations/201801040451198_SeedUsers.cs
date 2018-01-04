namespace DDAC_TP033375.Migrations
{
	using System;
	using System.Data.Entity.Migrations;
	
	public partial class SeedUsers : DbMigration
	{
		public override void Up()
		{
			Sql(@"
INSERT INTO [dbo].[AspNetUsers] ([Id], [Email], [EmailConfirmed], [PasswordHash], [SecurityStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEndDateUtc], [LockoutEnabled], [AccessFailedCount], [UserName], [Name], [CompanyName]) VALUES (N'aebc3cb0-2df0-4ca2-84e8-a147943150f9', N'test@agent.com', 0, N'AP2Q7UazA6Y2Qbd175ojnK8uS4Dfg0VK4XYpogBUmxBnepoDSbc1jfaZDDKxTfu9lg==', N'4134de83-48a7-4fd4-85d5-770e59a84c3b', N'+60123456789', 0, 0, NULL, 1, 0, N'test@agent.com', N'Testing Agent 1', N'Testing Agency')
INSERT INTO [dbo].[AspNetUsers] ([Id], [Email], [EmailConfirmed], [PasswordHash], [SecurityStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEndDateUtc], [LockoutEnabled], [AccessFailedCount], [UserName], [Name], [CompanyName]) VALUES (N'f2761ae1-fb18-40b7-b5e6-35ccbbef2c9d', N'admin@maersk.com', 0, N'ACjB1OieGJTX+tvGmwkS4D9phKwFWhk6o5Umjjd5hkIrI0ofTxiRFC5EDpBFUwaoeg==', N'3843def9-18c3-4f45-a1aa-ea93e77168ac', N'+60165679363', 0, 0, NULL, 1, 0, N'admin@maersk.com', N'Maersk Line Admin', N'Maersk Line')

INSERT INTO [dbo].[AspNetRoles] ([Id], [Name]) VALUES (N'3db4f5b4-d2d9-44db-9bc7-c2f1385dc775', N'Admin')

INSERT INTO [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (N'f2761ae1-fb18-40b7-b5e6-35ccbbef2c9d', N'3db4f5b4-d2d9-44db-9bc7-c2f1385dc775')
");
		}
		
		public override void Down()
		{
		}
	}
}
