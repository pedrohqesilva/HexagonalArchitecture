using Microsoft.EntityFrameworkCore.Migrations;

namespace Core.Infrastructure.Data.Migrations.Migrations
{
    public partial class migration_TGPeticaoEletronica : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"CREATE OR REPLACE TRIGGER TG_AI_PETICAO_ELETRONICA_ARQ
                                    AFTER INSERT ON PETICAO_ELETRONICA_ARQUIVO
                                    FOR EACH ROW
                                    DECLARE
                                    --   PRAGMA AUTONOMOUS_TRANSACTION;
                                                vCOD_DOC NUMBER := 0;
                                                vANO_DOC NUMBER := 0;
                                                vEXISTE NUMBER;

                                                BEGIN
                                                   BEGIN
                                          SELECT PE.COD_DOC, PE.ANO_DOC
                                            INTO vCOD_DOC, vANO_DOC
                                            FROM VW_PETICAO_ELETRONICA PE
                                           WHERE PE.COD_PETICAO = :NEW.COD_PETICAO;
                                                EXCEPTION
                                                     WHEN NO_DATA_FOUND THEN
                                                 RETURN;
                                                END;

                                                BEGIN
                                                   SELECT DISTINCT 1
                                            INTO vEXISTE
                                            FROM DOCUMENTO_ARQUIVO DA
                                           WHERE DA.COD_DOC = vCOD_DOC
                                             AND DA.ANO_DOC = vANO_DOC

                                               AND DA.COD_ARQUIVO = :NEW.COD_ARQUIVO;
                                                EXCEPTION
                                                     WHEN NO_DATA_FOUND THEN
                                          INSERT INTO DOCUMENTO_ARQUIVO(COD_DOC, ANO_DOC, COD_ARQUIVO, DAT_VINCULACAO)
                                               VALUES(vCOD_DOC, vANO_DOC, :NEW.COD_ARQUIVO, SYSDATE);
                                                END;
                                END;"
                            );
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
