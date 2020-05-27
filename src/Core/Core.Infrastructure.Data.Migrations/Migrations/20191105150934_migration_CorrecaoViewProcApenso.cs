using Microsoft.EntityFrameworkCore.Migrations;

namespace Core.Infrastructure.Data.Migrations.Migrations
{
    public partial class migration_CorrecaoViewProcApenso : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"
                CREATE OR REPLACE FORCE EDITIONABLE VIEW ""TCEMG"".""VW_PETICAO_ELETRONICA"" (""COD_PETICAO"", ""COD_PROC_APENSO"", ""COD_TIPO_PETICAO"", ""SGL_TIPO_PETICAO"",
                ""DSC_TIPO_PETICAO"", ""COD_TIPO_DOC_PETICAO"", ""SGL_TIPO_DOC_PETICAO"", ""DSC_TIPO_DOC_PETICAO"", ""COD_TIPO_FINALIDADE"", ""SGL_TIPO_FINALIDADE"", ""DSC_TIPO_FINALIDADE"",
                ""COD_MODULO_PETICAO"", ""SGL_MODULO_PETICAO"", ""DSC_MODULO_PETICAO"", ""DAT_SOLICITACAO"", ""COD_PESSOA"", ""NOM_PESSOA"", ""NUM_CPF_CNPF"", ""NUM_CPF_CNPF_EDT"",
                ""DSC_EMAIL"", ""NUM_ANO_REF"", ""COD_DOC"", ""ANO_DOC"", ""NUM_PROTOCOLO"", ""NUM_PROTOCOLO_EDT"", ""COD_MUNICIPIO"", ""NOM_MUNICIPIO"", ""UF_MUNICIPIO"",
                ""COD_PROCEDENCIA"", ""NOM_PROCEDENCIA"", ""NUM_CNPJ_PROCEDENCIA"", ""NUM_CNPJ_PROCEDENCIA_EDT"",""DAT_SITUACAO"", ""COD_SITUACAO"", ""SGL_SITUACAO"", ""DSC_SITUACAO"",
                ""DSC_MOTIVO"", ""NUM_MATR_SERV"", ""NOM_SERV"", ""COD_MUNICIPIO_IBGE"", ""COD_UNID_TC"", ""DESC_UNID_TC"", ""DSC_OBSERVACAO"", ""COD_OPCAO_ACAO"", ""SGL_OPCAO_ACAO"",
                ""DSC_OPCAO_ACAO"", ""COD_SEQ_PROC"", ""ANO_REF_PROC"", ""COD_NATUREZA"", ""DSC_NATUREZA"", ""COD_RELATOR"", ""NOM_RELATOR"", ""COD_COMPET"", ""DSC_COMPET"",
                ""COD_UNID_TC_PROC"", ""DESC_UNID_TC_PROC"", ""COD_ULT_HISTORICO"") AS 
                  SELECT PE.COD_PETICAO,
                       AP.COD_SEQ_PROC AS COD_PROC_APENSO,
                       TP_PET.COD_INDICADOR AS COD_TIPO_PETICAO,
                       TP_PET.SGL_INDICADOR AS SGL_TIPO_PETICAO,
                       TP_PET.DSC_INDICADOR AS DSC_TIPO_PETICAO,
                       TP_DOC.COD_INDICADOR AS COD_TIPO_DOC_PETICAO,
                       TP_DOC.SGL_INDICADOR AS SGL_TIPO_DOC_PETICAO,
                       TP_DOC.DSC_INDICADOR AS DSC_TIPO_DOC_PETICAO,
                       TP_FIN.COD_INDICADOR AS COD_TIPO_FINALIDADE,
                       TP_FIN.SGL_INDICADOR AS SGL_TIPO_FINALIDADE,
                       TP_FIN.DSC_INDICADOR AS DSC_TIPO_FINALIDADE,
                       TP_MOD.COD_INDICADOR AS COD_MODULO_PETICAO,
                       TP_MOD.SGL_INDICADOR AS SGL_MODULO_PETICAO,
                       TP_MOD.DSC_INDICADOR AS DSC_MODULO_PETICAO,
                       PE.DAT_SOLICITACAO,
                       PE.COD_PESSOA,
                       PF.NOME_PTE AS NOM_PESSOA,
                       PF.NUM_CPF_CGC_PTE AS NUM_CPF_CNPF,
                       FUN_FORMATA_CPF_CNPJ(PF.NUM_CPF_CGC_PTE, PF.TP_PTE) AS NUM_CPF_CNPF_EDT,
                       PF.DSC_EMAIL,
                       NVL(PE.NUM_ANO_REF, D.NUM_ANO_REF) AS NUM_ANO_REF,
                       PE.COD_DOC, PE.ANO_DOC,
                       PE.COD_DOC || PE.ANO_DOC AS NUM_PROTOCOLO,
                       (CASE WHEN LENGTH(PE.COD_DOC) < 10 THEN
                                SUBSTR(LPAD(PE.COD_DOC, 8, '0'), 1, 4) || '.' ||
                                SUBSTR(LPAD(PE.COD_DOC, 8, '0'), 5, 4) || '.' ||
                                LPAD(PE.ANO_DOC, 4, '0')
                             ELSE
                                SUBSTR(LPAD(PE.COD_DOC, 10, '0'), 1, 2) || '.' ||
                                SUBSTR(LPAD(PE.COD_DOC, 10, '0'), 3, 4) || '.' ||
                                SUBSTR(LPAD(PE.COD_DOC, 10, '0'), 7, 4) || '.' ||
                                LPAD(PE.ANO_DOC, 4, '0')
                        END) AS NUM_PROTOCOLO_EDT,
                       M.COD_MUNIC AS COD_MUNICIPIO,
                       M.DESC_MUNIC AS NOM_MUNICIPIO,
                       M.UF_MUNIC AS UF_MUNICIPIO,
                       PJ.COD_PTE AS COD_PROCEDENCIA,
                       PJ.NOME_PTE AS NOM_PROCEDENCIA,
                       PJ.NUM_CPF_CGC_PTE AS NUM_CNPJ_PROCEDENCIA,
                       FUN_FORMATA_CPF_CNPJ(PJ.NUM_CPF_CGC_PTE, PJ.TP_PTE) AS NUM_CNPJ_PROCEDENCIA_EDT,
                       PEH.DAT_HISTORICO AS DAT_SITUACAO,
                       SIT_PET.COD_INDICADOR AS COD_SITUACAO,
                       SIT_PET.SGL_INDICADOR AS SGL_SITUACAO,
                       SIT_PET.DSC_INDICADOR AS DSC_SITUACAO,
                       PEH.DSC_MOTIVO,
                       PEH.NUM_MATR_SERV,
                       S.NOME_SERV AS NOM_SERV,
                       M.COD_MUNICIPIO_IBGE,
                       D.COD_UNID_TC,
                       U.DESC_UNID_TC,
                       PE.DSC_OBSERVACAO,
                       OP_ACAO.COD_INDICADOR AS COD_OPCAO_ACAO,
                       OP_ACAO.SGL_INDICADOR AS SGL_OPCAO_ACAO,
                       OP_ACAO.DSC_INDICADOR AS DSC_OPCAO_ACAO,
                       PROC.COD_SEQ_PROC, PROC.ANO_REF_PROC,
                       PROC.COD_NATUREZA, PROC.DSC_NATUREZA,
                       PROC.COD_RELATOR, PROC.NOM_RELATOR,
                       PROC.COD_COMPET, PROC.DSC_COMPET,
                       PROC.COD_UNID_TC_PROC, PROC.DESC_UNID_TC_PROC,
                       PE.COD_ULT_HISTORICO

                FROM   PETICAO_ELETRONICA PE, PETICAO_ELETRONICA_HIST PEH,
                       INDICADOR SIT_PET, INDICADOR TP_PET, INDICADOR TP_DOC, INDICADOR TP_FIN, INDICADOR TP_MOD, INDICADOR OP_ACAO,
                       PARTES PF, SERVIDORES S,
                       DOCUMENTO D, MUNICIPIOS M, PARTES PJ, UNI_TCS U,
                       (SELECT
                          PROC.COD_SEQ_PROC
                          ,PROC.ANO_DOC_PROC
                          ,PROC.COD_DOC_PROC
                        FROM PROCESSOS PROC
                        INNER JOIN PETICAO_ELETRONICA PE
                        ON PE.ANO_DOC = PROC.ANO_DOC_PROC
                        AND PE.COD_DOC = PROC.COD_DOC_PROC) AP,
                       (SELECT DP.COD_DOC, DP.ANO_DOC,
                               P.COD_SEQ_PROC, P.ANO_REF_PROC,
                               N.COD_NAT AS COD_NATUREZA, N.DESC_NAT AS DSC_NATUREZA,
                               RA.COD_REAU AS COD_RELATOR, RA.NOME_REAU AS NOM_RELATOR,
                               CP.COD_UNID_TC AS COD_COMPET, CP.DESC_UNID_TC AS DSC_COMPET,
                               U.COD_UNID_TC AS COD_UNID_TC_PROC, U.DESC_UNID_TC AS DESC_UNID_TC_PROC
                        FROM   DOCUMENTO_PROCESSO DP, PROCESSOS P, NATUREZAS N, RELAT_AUDI RA, UNI_TCS CP, UNI_TCS U
                        WHERE  P.COD_SEQ_PROC = DP.COD_SEQ_PROC
                        AND P.NADM_COD_NAT = N.COD_NAT
                        AND P.RECO_COD_REAU_RCOMP = RA.COD_REAU
                        AND P.RECO_COD_UNID_TC_RCOMP = CP.COD_UNID_TC
                        AND P.COD_UNI_TC_PROC = U.COD_UNID_TC

                        UNION

                        SELECT J.COD_DOC_JUNT AS COD_DOC,
                               J.ANO_DOC_JUNT AS ANO_DOC,
                               P.COD_SEQ_PROC, P.ANO_REF_PROC,
                               N.COD_NAT AS COD_NATUREZA, N.DESC_NAT AS DSC_NATUREZA,
                               RA.COD_REAU AS COD_RELATOR, RA.NOME_REAU AS NOM_RELATOR,
                               CP.COD_UNID_TC AS COD_COMPET, CP.DESC_UNID_TC AS DSC_COMPET,
                               U.COD_UNID_TC AS COD_UNID_TC_PROC, U.DESC_UNID_TC AS DESC_UNID_TC_PROC
                        FROM   JUNTADAS J, PROCESSOS P, NATUREZAS N, RELAT_AUDI RA, UNI_TCS CP, UNI_TCS U
                        WHERE  J.COD_SEQ_PROC_JUNT = P.COD_SEQ_PROC
                        AND P.NADM_COD_NAT = N.COD_NAT
                        AND P.RECO_COD_REAU_RCOMP = RA.COD_REAU
                        AND P.RECO_COD_UNID_TC_RCOMP = CP.COD_UNID_TC
                        AND P.COD_UNI_TC_PROC = U.COD_UNID_TC) PROC


                WHERE  PE.COD_ULT_HISTORICO = PEH.COD_HISTORICO
                AND PEH.COD_SITUACAO = SIT_PET.COD_INDICADOR
                AND PE.COD_TIPO_PETICAO = TP_PET.COD_INDICADOR
                AND PE.COD_TIPO_DOC_PETICAO = TP_DOC.COD_INDICADOR
                AND PE.COD_TIPO_FINALIDADE = TP_FIN.COD_INDICADOR
                AND PE.COD_MODULO_PETICAO = TP_MOD.COD_INDICADOR(+)
                AND PE.COD_PESSOA = PF.COD_PTE
                AND PEH.NUM_MATR_SERV = S.NUM_MATR_SERV(+)
                AND PE.COD_DOC = D.COD_DOC
                AND PE.ANO_DOC = D.ANO_DOC
                AND D.COD_MUNICIPIO = M.COD_MUNIC
                AND D.COD_PESSOA_PROCEDENCIA = PJ.COD_PTE(+)
                AND D.COD_UNID_TC = U.COD_UNID_TC
                AND PE.COD_OPCAO_ACAO = OP_ACAO.COD_INDICADOR(+)
                AND PE.COD_DOC = PROC.COD_DOC(+)
                AND PE.ANO_DOC = PROC.ANO_DOC(+)
                AND PE.COD_DOC = AP.COD_DOC_PROC(+)
                AND PE.ANO_DOC = AP.ANO_DOC_PROC(+);

            ");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
