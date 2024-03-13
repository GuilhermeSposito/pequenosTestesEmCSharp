using ProjetoTeste;
using System.Text.Json;
Console.ForegroundColor = ConsoleColor.White;

while (true)
{
    try
    {
        Console.WriteLine("\n\tMenu: \n" +
            "   1 - LOGIN  " +
            "\n2 - Detalha funcionario" +
            "\n3 - Procurar produto pelo nome" +
            "\n4 - Listar Categorias " +
            "\n5 - Lista Podutos" +
            "\n6 - Logout " +
            "\n99 - Sair Do menu");
        int opMenu = Convert.ToInt32(Console.ReadLine());

        switch (opMenu)
        {
            case 1:
                Console.ForegroundColor = ConsoleColor.DarkBlue;
                Console.Write("\nEmail:");
                string? email = Console.ReadLine();

                Console.Write("Senha:");
                string? senha = Funcionario.GetSenha();
            
                await Funcionario.Login(email, senha);
                Console.ForegroundColor = ConsoleColor.White;

                break;
            case 2:
                //aqui vai listar produtos
                await Funcionario.GetFuncionario();
                break;
            case 3:
                //aqui vai procurar produto pelo nome
                Console.WriteLine("Insira o nome do produto que deseja procurar!");
                await Produtos.ProcuraProdutoPeloNome(Console.ReadLine());
                break;
            case 4:
                await Categorias.GetCategorias();
                break;

            case 5:
                await Produtos.GetProdutos();
                break;
            case 6:
                Token.token = null;
                break;
            case 99:
                return;
            default:
                Console.WriteLine("Escolha de menu invalida tente outra!");
                break;
        }

    }
    catch (Exception ex) when (ex.Message.Contains("format"))
    {
        Console.WriteLine("Formato invalido, Tente outro");
    }
    catch (Exception ex)
    {
        Console.WriteLine(ex.Message);
    }

}


