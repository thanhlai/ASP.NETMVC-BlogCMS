using NHibernate.Mapping.ByCode;
using NHibernate.Mapping.ByCode.Conformist;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SimpleBlog.Models
{
    public class User
    {
        public virtual int Id { get; set; }
        public virtual string Username { get; set; }
        public virtual string Email { get; set; }
        public virtual string PasswordHash { get; set; }
    }
    public class UserMap : ClassMapping<User>
    {
        public UserMap()
        {
            //tell nHibernate what table to look for
            Table("users");
            //identify the primary key
            Id(x => x.Id,x=>x.Generator(Generators.Identity));
            //other fields in the db
            Property(x => x.Username, x => x.NotNullable(true));
            Property(x => x.Email, x => x.NotNullable(true));
            Property(x => x.PasswordHash, x => {
                x.Column("password_hash");
                x.NotNullable(true);
            });
        }
    }
}