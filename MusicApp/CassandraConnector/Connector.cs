using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Cassandra;
using Cassandra.Mapping;

namespace MusicApp.CassandraConnector
{
    public static class Connector
    {
        private static ISession session { get; set; }


        /// <summary>
        /// First, create an instance of a cluster, and use that to obtain a session.
        /// A session manages connections to the cluster for you.
        /// In this case the cluster is a localmachine
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        public static RowSet ExecuteQuery(string query)
        {
            Cluster cluster = Cluster.Builder().AddContactPoint("127.0.0.1").Build();
            session = cluster.Connect("musicdemo");
            return session.Execute(query);
           
        }

        /// <summary>
        /// Returns a Mapper for returning a Collection
        /// </summary>
        /// <returns></returns>
        public static Mapper MapperList()
        {
            Cluster cluster = Cluster.Builder().AddContactPoint("127.0.0.1").Build();
            session = cluster.Connect("musicdemo");
            return new Mapper(session);
        }


    }
}