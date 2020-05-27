using Microsoft.EntityFrameworkCore.Migrations;

namespace Core.Infrastructure.Data.Migrations.Migrations
{
    public partial class seed_TipoDocumentoRecursoOrdinario : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"insert into tipo_documento (COD_SEQ_TIPO_DOCUMENTO, COD_ORIGEM_DOC, COD_TIPO_DOC, COD_OBJETIVO_DOC, IND_ARQUIVO_ASSINADO, IND_PROCESSO_OBRIGATORIO, IND_EXIBIR_PROC_ADVOGADO, IND_OFICIO_OBRIGATORIO, DAT_INICIO, DAT_FIM, IND_ORGAO_OBRIGATORIO, IND_PERMITE_DUPLICIDADE, COD_TIPO_DOC_SUBSTITUTO, COD_OBJETIVO_DOC_SUBSTITUTO, COD_UNID_TC_INICIAL, COD_MODULO_DOC, IND_EXIBE_PROCESSO, IND_EXIBE_OFICIO, IND_RESPOSTA_PETICAO)
                                   values (1123, 2187, 545, 585, 'S', 'S', 'N', 'N', to_date('30-09-2019', 'dd-mm-yyyy'), null, 'N', 'S', null, null, 21, null, 'S', 'N', 'N');");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
