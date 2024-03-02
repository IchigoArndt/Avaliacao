using System.Collections.Generic;

namespace Avaliacao.Repositorio.Repositorios
{
    public interface IRepositorio<T>
    {
        public int Adicionar(T item);
        public IEnumerable<T> BuscarTodos();
        public T BuscarPorId(int id);
        public int Editar(T item);
        public int Deletar(int id);
    }
}
