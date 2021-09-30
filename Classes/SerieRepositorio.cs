using System;
using System.Collections.Generic;
using Catalogo.de.Series.Interfaces;

namespace Catalogo.de.Series
{
    public class SerieRepositorio : IRepositorio<Serie>
    {
        private List<Serie> listaSerie = new List<Serie>();

        public void atualizar(int id, Serie entidade)
        {
            id--;

            if (entidade == null || id < 0 || id > listaSerie.Count - 1)
            {
                Console.WriteLine("Série inexistente.");
                return;
            }

            listaSerie[id] = entidade;
        }

        public void excluir(int id)
        {
            id--;

            if (id < 0 || id > listaSerie.Count - 1)
            {
                Console.WriteLine("Série inexistente.");
                return;
            }

            if (listaSerie[id] == null)
            {
                Console.WriteLine("Série inexistente.");
                return;
            }

            listaSerie.RemoveAt(id);
        }

        public void excluirTudo()
        {
            listaSerie.Clear();
        }

        public void inserir(Serie entidade)
        {
            listaSerie.Add(entidade);
            Console.WriteLine("Série adicionada com sucesso!");
        }

        public List<Serie> lista()
        {
            return listaSerie;
        }

        public int proximoId()
        {
            return listaSerie.Count + 1;
        }

        public Serie retornaPorId(int id)
        {
            id--;

            if (id < 0 || id > listaSerie.Count - 1)
            {
                Console.WriteLine("Série inexistente.");
                return null;
            }

            if (listaSerie[id] == null)
            {
                Console.WriteLine("Série inexistente.");
                return null;
            }

            return listaSerie[id];
        }
    }
}