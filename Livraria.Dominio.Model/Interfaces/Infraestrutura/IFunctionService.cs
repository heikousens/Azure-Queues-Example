using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Livraria.Dominio.Model.Interfaces.Infraestrutura
{
    public interface IFunctionService
    {
        Task InvokeAsync(object objeto);
    }
}
