using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace ProjetoTeste;

internal class Produtos
{
    public int id { get; set; }
    public string? descricao { get; set; }
    public int quantidade_estoque { get; set; }
    public int valor { get; set; }
    public int categoria_id { get; set; }

    public Produtos()
    {

    }

    public static async Task GetProdutos()
    {
        using (HttpClient client = new HttpClient())
        {
            try
            {
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Token.token);
                HttpResponseMessage response = await client.GetAsync("https://kind-jade-sturgeon-gear.cyclic.app/produtos");


                if (response.IsSuccessStatusCode)
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    string jsonContent = await response.Content.ReadAsStringAsync();

                    List<Produtos> produtos = JsonSerializer.Deserialize<List<Produtos>>(jsonContent);
                    MostrarProdutos(produtos);

                    Console.ForegroundColor = ConsoleColor.White;
                }
                else
                {
                    throw new Exception("\nToken Invalido");
                }


            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

        }
    }

    public static async Task ProcuraProdutoPeloNome(string pesquisa)
    {
        try
        {
            using (HttpClient client = new HttpClient())
            {
                 client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Token.token);
                HttpResponseMessage response = await client.GetAsync("https://kind-jade-sturgeon-gear.cyclic.app/produtos");

                string jsonContent = await response.Content.ReadAsStringAsync();

                List<Produtos> produtos = JsonSerializer.Deserialize<List<Produtos>>(jsonContent);
                var produtoPesquisado = produtos.Find(p => p.descricao.Contains(pesquisa));

                if (produtoPesquisado == null)
                {
                    throw new ProdutoNaoEncontradoException("\nProduto não encontrado!");
                }

                List<Produtos> produtoEncontrado = new List<Produtos>();
                produtoEncontrado.Add(produtoPesquisado);

                Console.ForegroundColor = ConsoleColor.Green;
                MostrarProdutos(produtoEncontrado);
                Console.ForegroundColor = ConsoleColor.White;
            }
        }
        catch (Exception ex)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(ex.Message);
            Console.ForegroundColor = ConsoleColor.White;
        }


    }

    public static void MostrarProdutos(List<Produtos> produtos)
    {
        Console.WriteLine("\n\t Produtos:");
        foreach (var produto in produtos)
        {
            double valorProduto = produto.valor / 100;
            Console.WriteLine($"\nId: {produto.id}\nDescrição: {produto.descricao}\nQuantidade Em Estoque: {produto.quantidade_estoque}\nValor: {valorProduto:c} \nId da Categoria: {produto.categoria_id} ");
        }
    }
}

public class ProdutoNaoEncontradoException : Exception
{
    public ProdutoNaoEncontradoException(string? message) : base(message)
    {
    }
}