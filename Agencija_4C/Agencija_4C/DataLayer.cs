using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using NHibernate;
using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using Agencija_4C.Mapiranja;
using MySql.Data.MySqlClient;

namespace Agencija_4C
{
    public class DataLayer
    {
        private static ISessionFactory _factory = null;
        private static object objLock = new object();
        
        public static ISession GetSession()
        {
            if (_factory == null)
            {
                lock (objLock)
                {
                    if (_factory == null)
                        _factory = CreateSessionFactory();
                }
            }

            return _factory.OpenSession();
        }
        
        private static ISessionFactory CreateSessionFactory()
        {
            try
            {
                
                return Fluently.Configure()
                    .Database(MySQLConfiguration.Standard
                    .ConnectionString(c => c.Server("localhost").Database("softversko").Username("root").Password("root")))
                    //.ConnectionString(c => c.Server("sql7.freemysqlhosting.net").Database("sql7294448").Username("sql7294448").Password("GierMdXiLs")))
                    //c.FromConnectionStringWithKey("Server=localhost;Database=softversko;Uid=root;Pwd=root;"))) //?!
                    .Mappings(m => m.FluentMappings.AddFromAssemblyOf<DestinacijaMapiranja>())
                    .BuildSessionFactory();

               
            }
            catch (Exception ec)
            {
                //System.Windows.Forms.MessageBox.Show(ec.Message);
                
                return null;
            }
        }
        }
}
