// See https://aka.ms/new-console-template for more information
using Microsoft.Data.SqlClient;
using System.Data.Common;

Console.WriteLine("POC ADO");

//1- Nuget Microsoft.Data.SqlClient

//2- Connectionstring
string cnstr = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=NetFlask;Integrated Security=True;Connect Timeout=60;Encrypt=False;Trust Server Certificate=False;";

//3- Mon objet de connection
DbConnection oConn = new SqlConnection(cnstr);

try
{
	//4- Je dois me connecter
	oConn.Open();

	//5- Premier test --> inserer une donnée
	// se fait via une COMMAND

	//5.1- Créer ma commande
	DbCommand oCmd = oConn.CreateCommand();
	//5.2- Requête
	string Genre = Console.ReadLine(); // ');Delete From Genre;--
	string query = $"Insert into Genre OUTPUT inserted.IdGenre Values(@MonGenre)";
	//A NE JAMAIS FAIRE!!!!!!!!!!!
	//String query = $"Insert into Genre Values('{Genre}')";
	//"insert into Genre values ('');Delete from Genre;--');"
	//5.3 Je créé le(s) paramètres
	DbParameter paramGenre = oCmd.CreateParameter();
	paramGenre.Value = Genre;
	paramGenre.ParameterName = "MonGenre";
	paramGenre.DbType = System.Data.DbType.String;
	paramGenre.Size = 150;

	//5.4 Ajoute la query dans la commande
	oCmd.CommandText = query;
	oCmd.CommandType = System.Data.CommandType.Text;
	//5.4 Ajouter les paramètres à la commande
	oCmd.Parameters.Add(paramGenre);

	//5.5 J'execute
	//Comme c'est un insert --> ExecuteNonQuery ou ExecuteScalar
	int lenouvelId =(int) oCmd.ExecuteScalar();
    Console.WriteLine($"Id du genre : {lenouvelId}");
    //5.6 Je ferme la connexion
    oConn.Close();

}
catch (Exception ex )
{
	Console.ForegroundColor = ConsoleColor.Red;
    Console.WriteLine(ex.Message);
	Console.ResetColor();
}