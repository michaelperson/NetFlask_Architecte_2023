using NetFlask.DAL.Repository.Entities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetFlask.DAL.Repository
{
	public class MovieRepository : RepositoryHelper<MoviesEntity>, IRepository<MoviesEntity, int>
	{
		public MovieRepository(string cnstr) : base(cnstr)
		{
		}

		public void Delete(int id)
		{
			throw new NotImplementedException();
		}

		public MoviesEntity Get(int id)
		{
			string query = "select * from Movies where idMovie= @id";
			Dictionary<string, object> keyValuePairs = new Dictionary<string, object>();
			keyValuePairs.Add("id", id);
			return GetOne(query, keyValuePairs, MapperToEntity);
		}

		public IEnumerable<MoviesEntity> GetAll()
		{
			string query = "select * from Movies";

			return GetAll(query, null, MapperToEntity);
		}

		public void Insert(MoviesEntity entity)
		{
			throw new NotImplementedException();
		}

		public MoviesEntity MapperToEntity(IDataRecord record)
		{
			return new MoviesEntity
			{
				IdMovie = (int)record["IdMovie"],
				Description = record["Description"].ToString(),
				PicturePath = record["PicturePath"].ToString(),
				Rating = (double)record["Rating"],
				ReleaseDate = (DateTime)record["ReleaseDate"],
				Title = record["Title"].ToString()
			};
		}

		public void Update(MoviesEntity entity)
		{
			throw new NotImplementedException();
		}
	}
}
