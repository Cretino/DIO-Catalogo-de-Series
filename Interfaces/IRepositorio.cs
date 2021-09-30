using System.Collections.Generic;

namespace Catalogo.de.Series.Interfaces
{
    public interface IRepositorio <T>
    {
        List<T> lista();

        T retornaPorId(int id);
        void inserir(T entidade);
        void excluir(int id);
        void atualizar(int id, T entidade);
        int proximoId();
    }
}