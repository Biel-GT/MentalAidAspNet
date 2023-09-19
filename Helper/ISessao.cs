using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TesteAdoNET.Data;

namespace TesteAdoNET.Helper
{
    public interface ISessao
    {
        void CriarSessao(Usuarios usuario);

        Usuarios BuscarSessao();

        void RemoverSessao();
    }
}
