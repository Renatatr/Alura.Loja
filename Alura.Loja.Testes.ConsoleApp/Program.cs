using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Text;
using System.Threading.Tasks;

namespace Alura.Loja.Testes.ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            var fulano = new Cliente();
            fulano.Nome = "abc";
            fulano.EndereçoDeEntrega = new Endereco()
            {
                Numero = 21,
                Logradouro = "rua abacaxi",
                Complemento = "Loja",
                Bairro = "bairrão",
                Cidade = "Cida"
            }

            using (var contexto = new LojaContext())
            {
                contexto.Clientes.Add(fulano);
                contexto.SaveChanges();
            }
        }

        public static MuitosParaMuitos()
        {
            var p1 = new Produto() { Nome = "Suco Laranja", Categoria = "Bebida", PrecoUnitario = 73, Unidade = "Litros" };
            var p2 = new Produto() { Nome = "Café", Categoria = "Bebida", PrecoUnitario = 2.9, Unidade = "Gramas" };
            var p3 = new Produto() { Nome = "Macarrão", Categoria = "Alimentos", PrecoUnitario = 98.1, Unidade = "Gramas" };

            var promocaoPascoa = new Promocao();
            promocaoPascoa.Descricao = "Páscoa feliz";
            promocaoPascoa.DataInicio = DateTime.Now;
            promocaoPascoa.DataFinal = DateTime.Now.AddMonths(3);

            promocaoPascoa.IncluiProduto(p1);
            promocaoPascoa.IncluiProduto(p2);
            promocaoPascoa.IncluiProduto(p3);



            //var paoFrances = new Produto();
            //paoFrances.Nome = "Pão francês";
            //paoFrances.PrecoUnitario = 0.4;
            //paoFrances.Unidade = "Unidade";
            //paoFrances.Categoria = "Padaria";

            //var compra = new Compra();
            //compra.Quantidade = 6;
            //compra.Produto = paoFrances;
            //compra.Preco = paoFrances.PrecoUnitario*compra.Quantidade;

            using (var contexto = new LojaContext())
            {
                //contexto.Promocoes.Add(promocaoPascoa);
                var promocao = contexto.Promocoes.Find(1);
                contexto.Promocoes.Remove(promocao);
                contexto.SaveChanges();
            }
        }
    }
}
