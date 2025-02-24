﻿using Conduit.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Conduit.Infrastructure
{
    public class TagsConfiguration
    {
        public void Configure(EntityTypeBuilder<Tag> builder)
        {
            builder.HasMany(t => t.Articles).WithMany(a => a.Tags);
        }
    }
}
