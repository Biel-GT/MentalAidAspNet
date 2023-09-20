using Microsoft.Ajax.Utilities;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Web;
using TesteAdoNET.Data.Entities;

namespace TesteAdoNET.Data.Config
{
    public class UsuarioMap : EntityTypeConfiguration<Usuario>
    {
        public UsuarioMap()
        {
            //Nome da tabela
            this.ToTable("Usuarios");

            //Chave primária
            this.HasKey(x => x.Id);

            //Colunas
            this.Property(x => x.Nome);
            this.Property(x => x.Email);
            this.Property(x => x.Senha);
        }
    }
}