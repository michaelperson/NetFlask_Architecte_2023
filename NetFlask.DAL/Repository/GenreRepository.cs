using NetFlask.DAL.Repository.Entities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetFlask.DAL.Repository
{
	public class GenreRepository : RepositoryHelper<GenreEntity>, IRepository<GenreEntity, int>
	{
		public GenreRepository(string cnstr) : base(cnstr)
		{
		}

		public void Delete(int id)
		{
			throw new NotImplementedException();
		}

		public GenreEntity Get(int id)
		{
			string query = "select * from Genre where idGenre = @id";
			Dictionary<string, object> keyValuePairs = new Dictionary<string, object>();
			keyValuePairs.Add("id", id);
			return GetOne(query, keyValuePairs, MapperToEntity);
		}

		public IEnumerable<GenreEntity> GetAll()
		{
			string query = "select * from Genre";
			 
			return GetAll(query, null, MapperToEntity);
		}

		public void Insert(GenreEntity entity)
		{
			throw new NotImplementedException();
		}

		public void Update(GenreEntity entity)
		{
			throw new NotImplementedException();
		}



		public IEnumerable<GenreEntity> GetByMovie(int id)
		{
			string query = "select * from Genre inner join GenreMovie on Genre.idGenre=GenreMovie.IdGenre where IdMovie = @id";
			Dictionary<string, object> keyValuePairs = new Dictionary<string, object>();
			keyValuePairs.Add("id", id);
			return GetAll(query, keyValuePairs, MapperToEntity);
		}

		private GenreEntity MapperToEntity(System.Data.IDataRecord record)
		{
			return new GenreEntity()
			{
				Id = (int)record["IdGenre"],
				Libelle = record["Libelle"] == DBNull.Value ? null : record["Libelle"].ToString()
			};
		}
		 
	}
}
