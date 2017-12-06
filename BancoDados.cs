using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace ExemploCRUD
{
    public class BancoDados
    {
        SqlConnection cn;
        SqlCommand comandos;
        SqlDataReader rd;

        public bool Adicionar(Categoria cat){
            bool rs = false;
            try{
                
                //Estabelece conexão com o banco de dados
                cn = new SqlConnection();
                cn.ConnectionString = @"Data Source=.\sqlexpress;Initial Catalog=Papelaria;user id=sa; password=senai@123";
                
                //Abre o banco
                cn.Open();

                //Prepara a Query
                comandos = new SqlCommand();
                comandos.Connection = cn;
                comandos.CommandType = CommandType.Text;
                comandos.CommandText = "insert into categorias(titulo)values(@titulo)";
                comandos.Parameters.AddWithValue("@titulo", cat.Titulo);

                //Executa a Query
                int r = comandos.ExecuteNonQuery();
                
                //Verifica se a query foi executada
                if(r > 0)
                    rs = true;
                
                //Limpa os parâmetros
                comandos.Parameters.Clear();
            } catch (SqlException se){
                throw new Exception("Erro ao tentar cadastrar. " + se.Message);
            } catch (Exception ex){
                throw new Exception("Erro inesperado. " + ex.Message);
            } finally {
                cn.Close();
            }
            return rs;
        }

        public bool Atualizar(Categoria cat){
            bool rs = false;
            try{
                
                //Estabelece conexão com o banco de dados
                cn = new SqlConnection();
                cn.ConnectionString = @"Data Source=.\sqlexpress;Initial Catalog=Papelaria;user id=sa; password=senai@123";
                
                //Abre o banco
                cn.Open();

                //Prepara a Query
                comandos = new SqlCommand();
                comandos.Connection = cn;
                comandos.CommandType = CommandType.Text;
                comandos.CommandText = "update categorias set titulo = @titulo where idcategoria = @idcategoria";
                comandos.Parameters.AddWithValue("@titulo", cat.Titulo);
                comandos.Parameters.AddWithValue("@idcategoria", cat.idCategoria);

                //Executa a Query
                int r = comandos.ExecuteNonQuery();
                
                //Verifica se a query foi executada
                if(r > 0)
                    rs = true;
                
                //Limpa os parâmetros
                comandos.Parameters.Clear();
            } catch (SqlException se){
                throw new Exception("Erro ao tentar Atualizar. " + se.Message);
            } catch (Exception ex){
                throw new Exception("Erro inesperado. " + ex.Message);
            } finally {
                cn.Close();
            }
            return rs;
        }

        public bool Apagar(Categoria cat){
            bool rs = false;
            try{
                
                //Estabelece conexão com o banco de dados
                cn = new SqlConnection();
                cn.ConnectionString = @"Data Source=.\sqlexpress;Initial Catalog=Papelaria;user id=sa; password=senai@123";
                
                //Abre o banco
                cn.Open();

                //Prepara a Query
                comandos = new SqlCommand();
                comandos.Connection = cn;
                comandos.CommandType = CommandType.Text;
                comandos.CommandText = "delete from categorias where idcategoria = @idcategoria";
                comandos.Parameters.AddWithValue("@idcategoria", cat.idCategoria);

                //Executa a Query
                int r = comandos.ExecuteNonQuery();
                
                //Verifica se a query foi executada
                if(r > 0)
                    rs = true;
                
                //Limpa os parâmetros
                comandos.Parameters.Clear();
            } catch (SqlException se){
                throw new Exception("Erro ao tentar Apagar. " + se.Message);
            } catch (Exception ex){
                throw new Exception("Erro inesperado. " + ex.Message);
            } finally {
                cn.Close();
            }
            return rs;
        }

        public List<Categoria> ListarCategorias(int id){
            List<Categoria> lista = new List<Categoria>();
            try{
                //Estabelece conexão com o banco de dados
                cn = new SqlConnection();
               cn.ConnectionString = @"Data Source=.\sqlexpress;Initial Catalog=Papelaria;user id=sa; password=senai@123";

                //Abre o banco
                cn.Open();

                //Prepara a Query
                comandos = new SqlCommand();
                comandos.Connection = cn;
                comandos.CommandType = CommandType.Text;
                comandos.CommandText = "Select * from categorias where idcategoria=@idcategoria";
                comandos.Parameters.AddWithValue("@idcategoria", id);

                //Executa a Query e retorna o resultado da busca
                rd = comandos.ExecuteReader();

                while(rd.Read()){

                    lista.Add(new Categoria{
                                    idCategoria = rd.GetInt32(0),
                                    Titulo = rd.GetString(1)
                            }
                            );
                }
                comandos.Parameters.Clear();
            } catch (SqlException se){
                throw new Exception("Erro ao tentar listar. " + se.Message);
            } catch (Exception ex){
                throw new Exception("Erro inesperado " + ex.Message);
            } finally {
                cn.Close();
            }
            return lista;
        }

        public List<Categoria> ListarCategorias(string titulo){
            List<Categoria> lista = new List<Categoria>();
            try{
                //Estabelece conexão com o banco de dados
                cn = new SqlConnection();
                cn.ConnectionString = @"Data Source=.\sqlexpress;Initial Catalog=Papelaria;user id=sa; password=senai@123";
                //Abre o banco
                cn.Open();

                //Prepara a Query
                comandos = new SqlCommand();
                comandos.Connection = cn;
                comandos.CommandType = CommandType.Text;
                comandos.CommandText = "Select * from categorias where titulo like @titulo";
                comandos.Parameters.AddWithValue("@titulo", titulo);

                //Executa a Query e retorna o resultado da busca
                rd = comandos.ExecuteReader();

                while(rd.Read()){

                    lista.Add(new Categoria{
                                    idCategoria = rd.GetInt32(0),
                                    Titulo = rd.GetString(1)
                            }
                            );
                }
                comandos.Parameters.Clear();
            } catch (SqlException se){
                throw new Exception("Erro ao tentar listar. " + se.Message);
            } catch (Exception ex){
                throw new Exception("Erro inesperado " + ex.Message);
            } finally {
                cn.Close();
            }
            return lista;
        }

        public bool AdicionarCliente(Cliente cliente){
            bool rs = false;
            try{
                cn = new SqlConnection();
                cn.ConnectionString = @"Data Source=.\sqlexpress;Initial Catalog=Papelaria;user id=sa; password=senai@123";
                cn.Open();
                comandos = new SqlCommand();
                comandos.Connection = cn;
                comandos.CommandType = CommandType.StoredProcedure;
                comandos.CommandText = "sp_CadCliente";
                SqlParameter pnome = new SqlParameter("@nome", SqlDbType.VarChar,50);
                pnome.Value = cliente.Nome;
                comandos.Parameters.Add(pnome);

                SqlParameter pemail = new SqlParameter("@email", SqlDbType.VarChar,100);
                pemail.Value = cliente.Email;
                comandos.Parameters.Add(pemail);

                SqlParameter pcpf = new SqlParameter("@cpf", SqlDbType.VarChar,50);
                pcpf.Value = cliente.CPF;
                comandos.Parameters.Add(pcpf);

                int r = comandos.ExecuteNonQuery();

                if (r > 0)
                    rs = true;
                
                comandos.Parameters.Clear();
                
            } catch (SqlException se){
                throw new Exception("Erro ao tentar inserir os dados. " + se.Message);
            }catch (Exception ex){
                throw new Exception("Erro inesperado " + ex.Message);
            } finally{
                cn.Close();
            }
            return rs;
        }    
    }
}