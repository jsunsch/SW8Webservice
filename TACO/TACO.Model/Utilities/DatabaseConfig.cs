using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using NHibernate.Cfg;
using TACO.Model.Mapping;

namespace TACO.Model.Utilities
{
    public static class DatabaseConfig
    {
        public static FluentConfiguration GetConfig()
        { 
            var cfg = Fluently.Configure()
                        .Database(MsSqlConfiguration.MsSql2008
                            .ConnectionString(
                                "Data Source=0e9e9376-a65b-4c0f-8a53-a000008e4329.sqlserver.sequelizer.com;Persist Security Info=True;User ID=cyakspyqlpqyrglu;Password=2eoFV8iBpHZbBD37CcbbtGrnjudunDm5xFZy6jNfXHiiT5m5RPCMoyCtVavA5WoS")
                        .ShowSql()
                        .Dialect("NHibernate.Spatial.Dialect.MsSql2008GeographyDialect,NHibernate.Spatial.MsSql2008"))
                        .Mappings(x => x.FluentMappings.AddFromAssemblyOf<POIMap>());
            return cfg;
    }
    }
}
