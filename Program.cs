using Trabalho_POO.Controllers;
try
{
    Console.WriteLine("Bem vindo ao app de controle de contas");
    Console.WriteLine("Você ja possui uma conta ?");
    Console.WriteLine("[a] sim, possuo uma conta desejo fazer o login.");
    Console.WriteLine("[b] não, desejo criar uma conta.");
    Console.WriteLine("[c] sair.");
    char resp = char.Parse(Console.ReadLine());
    switch (resp)
    {
        case 'a':
            login.Main();
            break;
        case 'b':
            Servicos.CriarCliente();
            break;
        case 'c':
            break;
    }
}
catch (Exception ex)
{
}
