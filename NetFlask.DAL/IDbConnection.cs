using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetFlask.DAL
{
	public interface IDbConnection
	{
		bool Connect();
		bool Disconnect();

		bool Execute(string sql, Dictionary<string, object> parameters, bool isStoredProc = false);

		IEnumerable<T> Execute<T>(string sql, Dictionary<string, object> parameters, Func<IDataRecord,T> mapper, bool isStoredProc = false);
	}
}
