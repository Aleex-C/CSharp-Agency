﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Npgsql;

namespace AgentieTurismC.Repository
{
	public static class DBUtils
	{


		private static IDbConnection instance = null;


		public static IDbConnection getConnection(IDictionary<string, string> props)
		{
			if (instance == null || instance.State == System.Data.ConnectionState.Closed)
			{
				instance = getNewConnection(props);
				instance.Open();
			}
			return instance;
		}

		private static IDbConnection getNewConnection(IDictionary<string, string> props)
		{

			String connectionString = props["ConnectionString"];
			Console.WriteLine("PostgreSQL -- se deschide o conexiune la ... {0}", connectionString);
			return new NpgsqlConnection(connectionString);

		}
	}
}
