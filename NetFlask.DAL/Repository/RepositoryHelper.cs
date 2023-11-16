using NetFlask.DAL.Repository.Entities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetFlask.DAL.Repository
{
	public  class RepositoryHelper<T>
		where T : class
	{
		protected readonly IDbConnection connection;

		public RepositoryHelper(string cnstr)
		{
			connection = new SqlDbConnection(cnstr);
		}

		protected T GetOne(string sql , Dictionary<string, object> parameters, Func<IDataRecord,T> MapperToEntity)
		{
			T entity = null;

			if (connection.Connect())
			{
				 
				IEnumerable<T> mesDatas = connection.Execute<T>(sql, parameters, MapperToEntity, false);

				if (mesDatas.Count() > 0) entity = mesDatas.First();
				connection.Disconnect();
			}

			return entity;
		}

		protected List<T> GetAll(string sql, Dictionary<string, object> parameters, Func<IDataRecord, T> MapperToEntity)
		{
			List<T> LEntity = null;

			if (connection.Connect())
			{

				LEntity = connection.Execute<T>(sql, parameters, MapperToEntity, false).ToList();

				 
				connection.Disconnect();
			}

			return LEntity;
		}
	}
}
