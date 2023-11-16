using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetFlask.DAL.Repository
{
	public interface IRepository<T, Tkey>
		where T : class, new()
		where Tkey: struct
	{
		T Get(Tkey id);
		IEnumerable<T> GetAll();
		void Insert(T entity);
		void Update(T entity);
		void Delete(Tkey id);
		 
	}
}
