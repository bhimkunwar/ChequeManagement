﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentNHibernate;
using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using NHibernate;
using NHibernate.Tool.hbm2ddl;
namespace BM.Dao
{
    public class NhibernateHelper
    {
        public static ISessionFactory _sessionFactory;
        public static ISessionFactory SessionFactory
        {
            get
            {
                if (_sessionFactory == null)
                    InitializeSessionFactory();
                return _sessionFactory;
            }

            set { NhibernateHelper._sessionFactory = value; }
        }
        private static void InitializeSessionFactory()
        {
            _sessionFactory = Fluently.Configure()
                .Database(MsSqlConfiguration.MsSql2008
                .ConnectionString(@"server = kunwar-pc; database = srt; trusted_connection= True;")
                .ShowSql()
                )
                .Mappings(m => m.FluentMappings
                    .AddFromAssemblyOf<NhibernateHelper>())
                    .ExposeConfiguration(cfg => new SchemaExport(cfg)
                    .Create(false, false))
                    .BuildSessionFactory();
        }
        public static ISession OpenSession()
        {
            return SessionFactory.OpenSession();
        }       
    }
}
