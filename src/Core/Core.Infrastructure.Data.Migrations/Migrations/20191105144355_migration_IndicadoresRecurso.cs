using Microsoft.EntityFrameworkCore.Migrations;

namespace Core.Infrastructure.Data.Migrations.Migrations
{
    public partial class migration_IndicadoresRecurso : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"DECLARE
   
                               vFound                  NUMBER;
                               vCOD_INDICADOR          INDICADOR.COD_INDICADOR%TYPE;
   
                               CURSOR C_INDICADOR
                               IS
                               SELECT SGL_INDICADOR, DSC_INDICADOR, NUM_ORDEM_INDICADOR, COD_TIPO_INDICADOR, OBS_INDICADOR
                               FROM
                               (
                                  SELECT 'RO' AS SGL_INDICADOR, 'RECURSO ORDINÁRIO' AS DSC_INDICADOR, NULL AS NUM_ORDEM_INDICADOR, COD_TIPO_INDICADOR, NULL AS OBS_INDICADOR 
                                  FROM   TIPO_INDICADOR WHERE NOM_TIPO_INDICADOR = 'TIPO_FINALIDADE_PETICAO' 
      
                                  UNION ALL
      
                                  SELECT 'CRR' AS SGL_INDICADOR, 'CONTRARRAZÃO RECURSO' AS DSC_INDICADOR, NULL AS NUM_ORDEM_INDICADOR, COD_TIPO_INDICADOR, NULL AS OBS_INDICADOR 
                                  FROM   TIPO_INDICADOR WHERE NOM_TIPO_INDICADOR = 'OBJETIVO_DOCUMENTO' 
      
                                  UNION ALL
      
                                  SELECT 'CRR' AS SGL_INDICADOR, 'CONTRARAZ�O RECURSO' AS DSC_INDICADOR, NULL AS NUM_ORDEM_INDICADOR, COD_TIPO_INDICADOR, NULL AS OBS_INDICADOR 
                                  FROM   TIPO_INDICADOR WHERE NOM_TIPO_INDICADOR = 'TIPO_FINALIDADE_PETICAO' 
                               ) A
                               WHERE NOT EXISTS (SELECT 1 FROM INDICADOR I WHERE I.COD_TIPO_INDICADOR = A.COD_TIPO_INDICADOR AND I.SGL_INDICADOR = A.SGL_INDICADOR)
                               ORDER BY COD_TIPO_INDICADOR, SGL_INDICADOR;
   
                               -- SubFunction
                               FUNCTION NOVO_COD_INDICADOR
                               RETURN NUMBER
                               IS
                                  vFound                     NUMBER;
                                  vCodigo                    INDICADOR.COD_INDICADOR%TYPE;
                               BEGIN
                                  vCodigo := 0;
      
                                  LOOP
                                     vCodigo := vCodigo + 1;
         
                                     IF (vCodigo > 999999) THEN
                                        EXIT;
                                     END IF;
         
                                     BEGIN
                                        SELECT 1 
                                        INTO  vFound 
                                        FROM  INDICADOR 
                                        WHERE COD_INDICADOR = vCodigo;
                                     EXCEPTION
                                        WHEN NO_DATA_FOUND THEN
                                           EXIT;
                                     END;
                                  END LOOP;
      
                                  RETURN vCodigo;
      
                               END;
   
   
                            BEGIN
                               DBMS_OUTPUT.ENABLE (buffer_size => NULL);
   
                               FOR REG IN C_INDICADOR 
                               LOOP
                                  vCOD_INDICADOR := NOVO_COD_INDICADOR;
      
                                  SELECT COUNT(1) 
                                  INTO   vFound
                                  FROM   INDICADOR 
                                  WHERE  COD_TIPO_INDICADOR = REG.COD_TIPO_INDICADOR
                                  AND    SGL_INDICADOR = REG.SGL_INDICADOR;
      
                                  IF (vFound = 0) THEN
                                     INSERT INTO INDICADOR
                                       (COD_INDICADOR, SGL_INDICADOR, DSC_INDICADOR, NUM_ORDEM_INDICADOR, COD_TIPO_INDICADOR, OBS_INDICADOR, DAT_INICIO)
                                     VALUES
                                       (vCOD_INDICADOR, REG.SGL_INDICADOR, REG.DSC_INDICADOR, REG.NUM_ORDEM_INDICADOR, REG.COD_TIPO_INDICADOR, REG.OBS_INDICADOR, TRUNC(SYSDATE));
         
                                     DBMS_OUTPUT.PUT_LINE(vCOD_INDICADOR || ' - ' || REG.SGL_INDICADOR || ' - ' || REG.DSC_INDICADOR);
                                  END IF;
      
                               END LOOP;
   
                               COMMIT;
   
                            EXCEPTION
                               WHEN OTHERS THEN
                                  DBMS_OUTPUT.PUT_LINE(sqlerrm);   
                            END;"
                        );
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
