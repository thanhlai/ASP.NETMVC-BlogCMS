using NHibernate.Mapping.ByCode;
using NHibernate.Mapping.ByCode.Conformist;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SimpleBlog.Models
{
    public class Post
    {
        public virtual int Id { get; set; }
        public virtual User User { get; set; }

        public virtual string Title { get; set; }
        public virtual string Slug { get; set; }
        public virtual string Content { get; set; }

        public virtual DateTime CreateAt { get; set; }
        public virtual DateTime? UpdateAt { get; set; } //Nullable
        public virtual DateTime? DeleteAt { get; set; }

        public virtual IList<Tag> Tags { get; set; }

        public virtual bool IsDeleted { get { return DeleteAt != null; } }
    }

    public class PostMap : ClassMapping<Post>
    {
        public PostMap()
        {
            Table("posts");

            Id(x => x.Id, x => x.Generator(Generators.Identity));

            ManyToOne(x => x.User, x =>
            {
                x.Column("user_id");
                x.NotNullable(true);
            });

            Property(x => x.Title, x => x.NotNullable(true));
            Property(x => x.Slug, x => x.NotNullable(true));
            Property(x => x.Content, x => x.NotNullable(true));

            Property(x => x.CreateAt, x =>
            {
                x.Column("created_at");
                x.NotNullable(true);
            });

            Property(x => x.UpdateAt, x => x.Column("updated_at"));
            Property(x => x.DeleteAt, x => x.Column("deleted_at"));


            Bag(x => x.Tags, x =>
            {
                x.Key(y => y.Column("post_id"));
                x.Table("post_tags");
            }, x => x.ManyToMany(y => y.Column("tag_id")));
        }
    }
}