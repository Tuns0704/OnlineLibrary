using Microsoft.EntityFrameworkCore.Migrations;

namespace OnlineLibrary.DataAccess.Migrations
{
    public partial class AddProcForAuthor : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"CREATE PROC usp_GetAuthors 
                                    AS 
                                    BEGIN 
                                     SELECT * FROM   dbo.Authors 
                                    END");

            migrationBuilder.Sql(@"CREATE PROC usp_GetAuthor 
                                    @Id int 
                                    AS 
                                    BEGIN 
                                     SELECT * FROM   dbo.Authors  WHERE  (Id = @Id) 
                                    END ");

            migrationBuilder.Sql(@"CREATE PROC usp_UpdateAuthor
	                                @Id int,
	                                @Name varchar(100)
                                    AS 
                                    BEGIN 
                                     UPDATE dbo.Authors
                                     SET  Name = @Name
                                     WHERE  Id = @Id
                                    END");

            migrationBuilder.Sql(@"CREATE PROC usp_DeleteAuthor
	                                @Id int
                                    AS 
                                    BEGIN 
                                     DELETE FROM dbo.Authors
                                     WHERE  Id = @Id
                                    END");

            migrationBuilder.Sql(@"CREATE PROC usp_CreateAuthor
                                   @Name varchar(100)
                                   AS 
                                   BEGIN 
                                    INSERT INTO dbo.Authors(Name)
                                    VALUES (@Name)
                                   END");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"DROP PROCEDURE usp_GetAuthors");
            migrationBuilder.Sql(@"DROP PROCEDURE usp_GetAuthor");
            migrationBuilder.Sql(@"DROP PROCEDURE usp_UpdateAuthor");
            migrationBuilder.Sql(@"DROP PROCEDURE usp_DeleteAuthor");
            migrationBuilder.Sql(@"DROP PROCEDURE usp_CreateAuthor");
        }
    }
}
