using Microsoft.EntityFrameworkCore.Migrations;

namespace Core.Infrastructure.Data.Migrations.Migrations
{
    public partial class migration_eventLog : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"create table LOG_EVENTO_DOMINIO
                                    (
                                      cod_evento_log VARCHAR2(32) not null,
                                      nome_tipo_evento VARCHAR2(150) not null,
                                      estado_evento INTEGER not null,
                                      dat_evento DATE not null,
                                      conteudo_evento VARCHAR2(4000) not null
                                    )
                                    tablespace OUTRAS
                                      pctfree 10
                                      initrans 1
                                      maxtrans 255
                                      storage
                                      (
                                        initial 64K
                                        next 1M
                                        minextents 1
                                        maxextents unlimited
                                      );
                                    -- Create/Recreate primary, unique and foreign key constraints
                                    alter table LOG_EVENTO_DOMINIO
                                      add primary key(COD_EVENTO_LOG)
                                      using index 
                                      tablespace OUTRAS
                                      pctfree 10
                                      initrans 2
                                      maxtrans 255
                                      storage
                                      (
                                        initial 64K
                                        next 1M
                                        minextents 1
                                        maxextents unlimited
                                      );"
                                );
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}

