using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace ProjetoTeste;

internal class Categorias
{
    public int id { get; set; }
    public string descricao { get; set; }

    public Categorias()
    {

    }

    public Categorias(int Id, string Descricao)
    {
        id = Id;
        descricao = Descricao;
    }

    public static void MostrarCategorias(List<Categorias> categorias)
    {
        foreach (var categoria in categorias)
        {
            Console.WriteLine($"\nID: {categoria.id} \nDescrição: {categoria.descricao}");
        }
    }

    public static async Task GetCategorias()
    {
        using (HttpClient client = new HttpClient())
        {
            try
            {
                HttpResponseMessage response = await client.GetAsync("https://kind-jade-sturgeon-gear.cyclic.app/categorias");

                string? jsonContent = await response.Content.ReadAsStringAsync();
                List<Categorias> categorias = JsonSerializer.Deserialize<List<Categorias>>(jsonContent);
                Console.ForegroundColor = ConsoleColor.Green;
                MostrarCategorias(categorias);
                Console.ForegroundColor = ConsoleColor.White;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        };
    }
}
