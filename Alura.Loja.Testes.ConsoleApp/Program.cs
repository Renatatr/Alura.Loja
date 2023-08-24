using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;

namespace Alura.Loja.Testes.ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            using (var contexto = new LojaContext())
            {
                var cliente = contexto.Clientes.Include(c => c.EndereçoDeEntrega).FirstOrDefault();
                Console.WriteLine($"\nEndereço de entrega para o Cliente {cliente.Nome}: {cliente.EndereçoDeEntrega.Logradouro}");

                //  var produto = contexto.Produtos.Include(p => p.Compras).Where(p => p.Id == 1010 ).FirstOrDefault();
                var produto = contexto.Produtos.Where(p => p.Id == 1010).FirstOrDefault();
                contexto.Entry(produto).Collection(p => p.Compras).Query().Where(c => c.Preco > 10).Load();
                Console.WriteLine($"Compras do produto {produto.Nome}:");
                foreach (var item in produto.Compras)
                {
                    Console.WriteLine(item);
                }

            }
        }

        private static void ExibeProdutosPromocao()
        {
            using (var contexto2 = new LojaContext())
            {
                //var promocao = contexto2.Promocoes.Include(p => p.Produtos).ThenInclude(pp => pp.Produto).FirstOrDefault();
                var promocao = contexto2.Promocoes.Include("Produtos.Produto").FirstOrDefault();
                Console.WriteLine("\nProdutos em promoção:");
                foreach (var item in promocao.Produtos)
                {
                    Console.WriteLine(item.Produto);
                }
            }
        }

        private static void IncluiPromocao()
        {
            using (var contexto = new LojaContext())
            {
                var promocao = new Promocao();
                promocao.Descricao = "Outlet3";
                promocao.DataInicio = new DateTime(2023, 3, 1);
                promocao.DataFinal = new DateTime(2023, 3, 25);

                var produtos = contexto.Produtos.Where(p => p.Categoria == "Bebida").ToList();
                foreach (var item in produtos)
                {
                    promocao.IncluiProduto(item);
                }

                contexto.Promocoes.Add(promocao);
                contexto.SaveChanges();
            }
        }

        private static void UmParaUm()
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
            };

            using (var contexto = new LojaContext())
            {
                contexto.Clientes.Add(fulano);
                contexto.SaveChanges();
            }
        }

        public void MuitosParaMuitos()
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
