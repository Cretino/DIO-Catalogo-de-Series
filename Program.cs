using System;
using System.Collections.Generic;

namespace Catalogo.de.Series
{
    class Program
    {
        static SerieRepositorio repositorio = new SerieRepositorio();

        // Método para verificar se a lista tem dados, se não tiver, retornará 'true'.
        static bool verificarListaVazia()
        {
            return (repositorio.lista().Count == 0);
        }

        // Método para selecionar a série por id ou nome do título fornecido pelo usuário.
        static Serie selecionarSerie(string tipo)
        {
            int id = 0;

            if (int.TryParse(tipo, out id) == false)
            {
                foreach(Serie s in repositorio.lista())
                {
                    if (s.retornaTitulo().Equals(tipo))
                    {
                        id = s.id;
                        break;
                    }
                }
            }

            return repositorio.retornaPorId(id);
        }
    
        // Método para preencher os dados da série.
        static Serie preencherDados(int id)
        {
            string titulo;
            string descricao;
            int ano;
            int genero;
            Serie serie;

            Console.WriteLine("Digite o título da série:");
            titulo = Console.ReadLine();
            Console.WriteLine("Digite a descrição do título \"{0}\":", titulo);
            descricao = Console.ReadLine();
            Console.WriteLine("Digite o ano de lançamento do título \"{0}\":", titulo);
            ano = int.Parse(Console.ReadLine());
            Console.WriteLine("Digite o valor do gênero do título \"{0}\":", titulo);
            Console.WriteLine("Gêneros disponíveis:");

            foreach(int i in Enum.GetValues(typeof(Genero)))
            {
                Console.WriteLine("{0} - {1}", i, Enum.GetName(typeof(Genero), i));
            }

            genero = int.Parse(Console.ReadLine());
            serie = new Serie(id, (Genero)genero, titulo, descricao, ano);

            return serie;
        }

        static void Main(string[] args)
        {
            bool finalizarMenu = false;
            Serie serieSelecionada;
            string opcao;

            while(finalizarMenu == false)
            {
                Console.WriteLine("--- Catálogos de Séries ---");
                Console.WriteLine("Informe a opção desejada:");
                Console.WriteLine("1 - Listar séries");
                Console.WriteLine("2 - Adicionar nova série");
                Console.WriteLine("3 - Atualizar série");
                Console.WriteLine("4 - Excluir série");
                Console.WriteLine("5 - Vizualizar uma série");
                Console.WriteLine("C - Limpar tela");
                Console.WriteLine("X - Sair");
                Console.WriteLine();

                switch(Console.ReadLine().ToLower())
                {
                    case "1":
                        if (verificarListaVazia())
                        {
                            Console.WriteLine("A lista de séries está vazia.");
                            break;
                        }

                        Console.WriteLine("--- Lista de Séries ---");

                        foreach(Serie s in repositorio.lista())
                        {
                            Console.WriteLine("Id {0}: - {1}", s.id, s.retornaTitulo());
                        }

                        Console.WriteLine();
                    break;

                    case "2":
                        Console.WriteLine("--- Cadastro de Série ---");
                        repositorio.inserir(preencherDados(repositorio.proximoId()));
                        Console.WriteLine();
                    break;

                    case "3":
                        if (verificarListaVazia())
                        {
                            Console.WriteLine("Você não pode autalizar uma série, por que a lista está vazia!");
                            break;
                        }

                        Console.WriteLine("--- Atualizar série ---");
                        Console.WriteLine("Você pode atualizar uma série fornecendo o título ou id:");
                        Console.WriteLine("Entre com a informação:");
                        opcao = Console.ReadLine();
                        serieSelecionada = selecionarSerie(opcao);

                        if (serieSelecionada == null)
                        {
                            Console.WriteLine("Voltando ao menu principal.");
                            break;
                        }

                        Console.WriteLine("Você deseja mesmo atualizar a série \"{0}\"?", opcao);
                        Console.WriteLine("s = \"Sim\", n = \"Não\"");
                        Console.WriteLine("--- Detalhes da série ---");
                        Console.WriteLine(serieSelecionada.ToString());
                        opcao = Console.ReadLine().ToLower();

                        switch(opcao)
                        {
                            case "s":
                            case "sim":
                            case "y":
                            case "yes":
                                repositorio.atualizar(serieSelecionada.id, preencherDados(serieSelecionada.id));
                                Console.WriteLine("Série: \"{0}\" - \"{1}\", atualizada com sucesso!", serieSelecionada.id, serieSelecionada.retornaTitulo());
                                Console.WriteLine("Voltando ao menu principal.");
                                Console.WriteLine();
                            break;

                            case "n":
                            case "não":
                            case "nao":
                            case "no":
                                Console.WriteLine("OK, voltando ao menu principal.");
                                Console.WriteLine();
                            break;

                            default:
                                Console.WriteLine("Opção inválida, cancelando operação de exclusão da série.");
                                Console.WriteLine();
                            break;
                        }
                    break;

                    case "4":
                        if (verificarListaVazia())
                        {
                            Console.WriteLine("Você não pode excluir uma série, por que a lista está vazia!");
                            break;
                        }

                        Console.WriteLine("--- Excluir Série ---");
                        Console.WriteLine("Você pode excluir uma série fornecendo o título ou id ou digitando \"t\" para apagar todas as séries:");
                        Console.WriteLine("Entre com a informação:");
                        opcao = Console.ReadLine();

                        if (opcao.ToLower().Equals("t"))
                        {
                            Console.WriteLine("Você deseja mesmo excluir todas as séries?");
                            Console.WriteLine("s = \"Sim\", n = \"Não\"");
                            opcao = Console.ReadLine().ToLower();

                            switch(opcao)
                            {
                                case "s":
                                case "sim":
                                case "y":
                                case "yes":
                                    repositorio.excluirTudo();
                                    Console.WriteLine("Todas as séries foram excluídas com sucesso!");
                                    Console.WriteLine("Voltando ao menu principal.");
                                    Console.WriteLine();
                                break;

                                case "n":
                                case "não":
                                case "nao":
                                case "no":
                                    Console.WriteLine("OK, voltando ao menu principal.");
                                    Console.WriteLine();
                                break;

                                default:
                                    Console.WriteLine("Opção inválida, cancelando operação de exclusão da série.");
                                    Console.WriteLine();
                                break;
                            }

                            break;
                        }

                        serieSelecionada = selecionarSerie(opcao);

                        if (serieSelecionada == null)
                        {
                            Console.WriteLine("Voltando ao menu principal.");
                            break;
                        }

                        Console.WriteLine("Você deseja mesmo excluir a série \"{0}\"?", opcao);
                        Console.WriteLine("s = \"Sim\", n = \"Não\"");
                        Console.WriteLine("--- Detalhes da série ---");
                        Console.WriteLine(serieSelecionada.ToString());
                        opcao = Console.ReadLine().ToLower();

                        switch(opcao)
                        {
                            case "s":
                            case "sim":
                            case "y":
                            case "yes":
                                repositorio.excluir(serieSelecionada.id);
                                Console.WriteLine("Série: \"{0}\" - \"{1}\", excluída com sucesso!", serieSelecionada.id, serieSelecionada.retornaTitulo());
                                Console.WriteLine("Voltando ao menu principal.");
                                Console.WriteLine();
                            break;

                            case "n":
                            case "não":
                            case "nao":
                            case "no":
                                Console.WriteLine("OK, voltando ao menu principal.");
                                Console.WriteLine();
                            break;

                            default:
                                Console.WriteLine("Opção inválida, cancelando operação de exclusão da série.");
                                Console.WriteLine();
                            break;
                        }
                    break;

                    case "5":
                        if (verificarListaVazia())
                        {
                            Console.WriteLine("Você não pode vizualizar uma série, por que a lista está vazia!");
                            break;
                        }

                        Console.WriteLine("--- Vizualizar Série ---");
                        Console.WriteLine("Você pode vizualizar todos detalhes de uma série fornecendo título ou id:");
                        Console.WriteLine("Entre com a informação:");
                        opcao = Console.ReadLine();
                        serieSelecionada = selecionarSerie(opcao);

                        if (serieSelecionada == null)
                        {
                            Console.WriteLine("Voltando ao menu principal.");
                            Console.WriteLine();
                            break;
                        }

                        Console.WriteLine("--- Detalhes da série ---");
                        Console.WriteLine(serieSelecionada.ToString());
                        Console.WriteLine("Voltando ao menu principal.");
                        Console.WriteLine();
                    break;

                    case "c":
                        Console.Clear();
                    break;

                    case "x":
                        finalizarMenu = true;
                        Console.WriteLine("Obrigado por utilizar o programa, volte sempre!");
                    break;

                    default:
                        Console.WriteLine("Opção inválida, tente digitar novamente.");
                        Console.WriteLine();
                    break;
                }
            }
        }
    }
}
