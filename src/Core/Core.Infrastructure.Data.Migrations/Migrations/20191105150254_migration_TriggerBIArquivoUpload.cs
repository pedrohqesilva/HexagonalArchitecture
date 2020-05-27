using Microsoft.EntityFrameworkCore.Migrations;

namespace Core.Infrastructure.Data.Migrations.Migrations
{
    public partial class migration_TriggerBIArquivoUpload : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"CREATE OR REPLACE TRIGGER TG_BI_ARQUIVO_UPLOAD
                                    BEFORE INSERT ON ARQUIVO_UPLOAD
                                    FOR EACH ROW
                                    BEGIN
                                      IF :NEW.COD_ARQUIVO_UPLOAD IS NULL THEN
                                         SELECT SQ_ARQUIVO_UPLOAD.NEXTVAL
                                           INTO :NEW.COD_ARQUIVO_UPLOAD
                                           FROM DUAL;
                                      END IF;
                                    END;");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
