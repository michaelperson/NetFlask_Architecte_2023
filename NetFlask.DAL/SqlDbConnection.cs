using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetFlask.DAL
{
	public class SqlDbConnection : IDbConnection, IDisposable
	{
		private readonly DbConnection _connection; 
        public SqlDbConnection(string cnstr)
        {
				_connection = new SqlConnection(cnstr);
        }

        public bool Connect()
		{
			try
			{
				if (_connection.State != System.Data.ConnectionState.Open)
				{					
					_connection.Open();
				}

				return true;
			}
			catch (Exception)
			{
				return false;
				
			}
		}

		public bool Disconnect()
		{
			try
			{
				if (_connection.State != System.Data.ConnectionState.Closed)
				{
					_connection.Close();
				}

				return true;
			}
			catch (Exception)
			{
				return false;

			}
		}

		public void Dispose()
		{
			if(_connection.State!= System.Data.ConnectionState.Closed)
			{
				Disconnect();
			}
			_connection.Dispose();
		}

		/// <summary>
		/// Permet d'exécuter une requête de modification de type insert, update, delete
		/// </summary>
		/// <param name="sql">La requête sql</param>
		/// <param name="parameters">Les paramètres éventuels</param>
		/// <param name="isStoredProc">True si c'est un appel à une procédure stockée</param>
		/// <returns>True si la requête est passée</returns>
		/// <exception cref="InvalidOperationException">Retournée si la requ^te est vide</exception>
		public bool Execute(string sql, Dictionary<string, object> parameters, bool isStoredProc = false)
		{
			bool isSuccess = false;

			//1- Vérification si la connexion est ouverte
			if (_connection.State == System.Data.ConnectionState.Closed) Connect();
			//2- si le paramètre sql est vide
			if(string.IsNullOrEmpty(sql)) { throw new InvalidOperationException("La requête sql doit être transmise"); }
			
			//3- Création de l'objet commande
			using(DbCommand oCmd = _connection.CreateCommand())
			{
				//4- ajout de la requête dans la commande
				oCmd.CommandText = sql;
				//5- Si il y a des paramètres, je les ajoutes dans la commande
				foreach(KeyValuePair<string, object> kvp in parameters)
				{
					DbParameter parametre = oCmd.CreateParameter();
					parametre.ParameterName = kvp.Key;
					parametre.Value = kvp.Value;
				}
				//6- Si c'est une procédure stockée, je change le command type
				oCmd.CommandType = isStoredProc ? System.Data.CommandType.StoredProcedure : System.Data.CommandType.Text;
				//7- Exécution de la requête
				try
				{
					oCmd.ExecuteNonQuery();
					isSuccess= true;

				}
				catch (Exception)
				{
					isSuccess=false;
					
				}
			}
			return isSuccess;
		}

		/// <summary>
		/// Permet d'éxécuter une requête de récupération
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="sql">La requête sql</param>
		/// <param name="parameters">Les paramètres éventuels</param>
		/// <param name="mapper">un delegate permettant de mapper les donénes du datareader et l'obet T</param>
		/// <param name="isStoredProc">True si c'est un appel à une procédure stockée</param>
		/// <returns>Un IEnumerable de de T avec les données de la db</returns>
		/// <exception cref="InvalidOperationException">Retournée si la requête est vide</exception>
		public IEnumerable<T> Execute<T>(string sql, Dictionary<string, object> parameters, Func<System.Data.IDataRecord, T> mapper, bool isStoredProc = false)
		{

			List<T> retour = new List<T>();
			//1- Vérification si la connexion est ouverte
			if (_connection.State == System.Data.ConnectionState.Closed) Connect();
			//2- si le paramètre sql est vide
			if (string.IsNullOrEmpty(sql)) { throw new InvalidOperationException("La requête sql doit être transmise"); }

			//3- Création de l'objet commande
			using (DbCommand oCmd = _connection.CreateCommand())
			{
				//4- ajout de la requête dans la commande
				oCmd.CommandText = sql;
				//5- Si il y a des paramètres, je les ajoutes dans la commande
				foreach (KeyValuePair<string, object> kvp in parameters)
				{
					DbParameter parametre = oCmd.CreateParameter();
					parametre.ParameterName = kvp.Key;
					parametre.Value = kvp.Value;
					oCmd.Parameters.Add(parametre);
				}
				//6- Si c'est une procédure stockée, je change le command type
				oCmd.CommandType = isStoredProc ? System.Data.CommandType.StoredProcedure : System.Data.CommandType.Text;
				//7- Exécution de la requête
				try
				{
					DbDataReader oDr = oCmd.ExecuteReader();
					if(oDr.HasRows)
					{
						while (oDr.Read())
						{
							retour.Add(mapper.Invoke(oDr));
						}
					} 
					 
				}
				catch (Exception)
				{
					throw;
				}
			}
			return retour;
		}
	}
}
