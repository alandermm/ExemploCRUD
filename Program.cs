using System;
using System.Collections.Generic;

namespace ExemploCRUD
{
    class Program
    {
        static void Main(string[] args)
        {
            MenuCategoria();
        }
        static int SubMenuPesquisarCategoria(){
                int opcaoAlteraCategoria;
                do{
                    Console.WriteLine("Submenu - Pesquisar Categoria\n");
                    Console.Write("Escolha uma opção abaixo:\n" +
                                "1 - Id\n" +
                                "2 - Título\n" +
                                "3 - Voltar\n"
                    );
                    Console.Write("Opção: ");
                    opcaoAlteraCategoria = Int16.Parse(Console.ReadLine());
                } while (opcaoAlteraCategoria < 0 || opcaoAlteraCategoria > 3);
                return opcaoAlteraCategoria;
            }

        static void MenuCategoria(){
            int opcao = 9;
            BancoDados bd;
            Categoria categoria;
            do{
                Console.WriteLine("Menu Categoria\n");
                Console.WriteLine("Escolha uma opção: ");
                Console.Write("1 - Cadastrar Categoria\n" +
                            "2 - Alterar Categoria\n" +
                            "3 - Excluir Categoria\n" +
                            "4 - Pesquisar Categoria\n" +
                            "5 - Sair\n"
                );
                Console.Write("Opção: ");
                opcao = Int16.Parse(Console.ReadLine());
                
                switch(opcao){
                    case 1:{
                        bd = new BancoDados();
                        categoria = new Categoria();
                        Console.Write("Digite o nome da categoria: ");
                        categoria.Titulo = Console.ReadLine();
                        bd.Adicionar(categoria);
                        break;
                    }
                    case 2:{
                        bd = new BancoDados();
                        categoria = new Categoria();
                        
                        Console.Write("Digite o ID da categoria a ser alterada: ");
                        int idCategoria = Int32.Parse(Console.ReadLine());
                        List<Categoria> categoriaSelecionada = bd.ListarCategorias(idCategoria);
                        foreach(var item in categoriaSelecionada){
                            categoria.idCategoria = item.idCategoria;
                            categoria.Titulo = item.Titulo;
                        }
                        Console.Write("Digite o novo título para a categoria " + categoria.Titulo + ": ");
                        categoria.Titulo = Console.ReadLine();
                        Console.WriteLine(categoria.Titulo);
                        Console.WriteLine(bd.Atualizar(categoria));
                        break;
                    }
                    case 3:{
                        bd = new BancoDados();
                        categoria = new Categoria();
                        Console.Write("Digite o ID da categoria a ser apagada: ");
                        categoria.idCategoria = Int16.Parse(Console.ReadLine());
                        bd.Apagar(categoria);
                        break;
                    }
                    case 4:{
                        bd = new BancoDados();
                        categoria = new Categoria();
                        List<Categoria> categoriaSelecionada = null;
                        int opcaoAlteraCategoria = SubMenuPesquisarCategoria();
                        if(opcaoAlteraCategoria.Equals(2)){
                            Console.Write("Digite o título da categoria a ser selecionada: ");
                            string titulo = Console.ReadLine();
                            categoriaSelecionada = bd.ListarCategorias(titulo);
                        } else if(opcaoAlteraCategoria.Equals(1)) {
                            Console.Write("Digite o ID da categoria a ser selecionada: ");
                            int id = Int16.Parse(Console.ReadLine());
                            categoriaSelecionada = bd.ListarCategorias(id);
                        }
                        foreach(var item in categoriaSelecionada){
                            categoria.idCategoria = item.idCategoria;
                            categoria.Titulo = item.Titulo;
                        }
                        Console.WriteLine("ID: " + categoria.idCategoria + ", Título: " + categoria.Titulo);
                        break;
                    }
                    case 9:
                        Environment.Exit(0);
                        break;
                }
            } while (!opcao.Equals(9));
        }
    }
}
