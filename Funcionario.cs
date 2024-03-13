using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Text.Json;
using System.Text.Json.Nodes;
using System.Threading.Tasks;
using System.Net.Http.Headers;

namespace ProjetoTeste;

internal class Funcionario
{
    public int id { get; set; }
    public string nome { get; set; }
    public string email { get; set; }
    public string senha { get; set; }

    public Funcionario() { }

    public Funcionario(string Email, string Senha) //Este Constructor vai ser usado para o login 
    {
        email = Email;
        senha = Senha;
    }

    public static async Task Login(string? email, string? senha)
    {
        try
        {
            using (HttpClient client = new HttpClient())
            { 
                string jsonContent = JsonSerializer.Serialize(new Funcionario(email, senha));
                StringContent content = new StringContent(jsonContent, Encoding.UTF8, "application/json");

                HttpResponseMessage response = await client.PostAsync("https://kind-jade-sturgeon-gear.cyclic.app/login", content);

                if (response.IsSuccessStatusCode)
                {
                    string respost = await response.Content.ReadAsStringAsync();

                    Token.token = respost.Replace($"\"", "");
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("\n\nLogado com successo");
                    Console.ForegroundColor = ConsoleColor.White;
                }
                else
                {
                    throw new Exception("\nUsuario ou senha Invalidos!");
                }
            };

        }
        catch (Exception ex)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(ex.Message);
            Console.ForegroundColor = ConsoleColor.White;
        }
    }
    public static string GetSenha()
    {
        string senha = "";
        ConsoleKeyInfo key;

        do
        {
            key = Console.ReadKey(true);

            // Se a tecla não for Enter (13), adiciona à senha
            if (key.Key != ConsoleKey.Enter)
            {
                senha += key.KeyChar;
                Console.Write("*");
            }
        } while (key.Key != ConsoleKey.Enter);

        return senha;
    }

    public static async Task GetFuncionario()
    {
        try
        {
            using (HttpClient client = new HttpClient())
            {
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Token.token);
                HttpResponseMessage response = await client.GetAsync("https://kind-jade-sturgeon-gear.cyclic.app/funcionario");

                string jsonContent = await response.Content.ReadAsStringAsync();



                if (response.IsSuccessStatusCode)
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Funcionario funcionario = JsonSerializer.Deserialize<Funcionario>(jsonContent);
                    Console.WriteLine($"\nID: {funcionario.id}\nNome: {funcionario.nome}\nEmail: {funcionario.email} ");
                    Console.ForegroundColor = ConsoleColor.White;
                }
                else
                {
                    throw new Exception("\nFuncionario não está logado");
                }

            }

        }
        catch (Exception ex)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(ex.Message);
            Console.ForegroundColor = ConsoleColor.White;
        }
    }

}
