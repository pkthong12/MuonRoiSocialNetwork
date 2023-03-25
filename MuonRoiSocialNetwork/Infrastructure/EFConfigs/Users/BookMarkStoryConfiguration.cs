﻿using MuonRoi.Social_Network.Users;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MuonRoi.Social_Network.Configurations.Users
{
    /// <summary>
    /// Configuration BookMarkStory
    /// </summary>
    public class BookMarkStoryConfiguration : IEntityTypeConfiguration<BookMarkStory>
    {
        /// <summary>
        /// Configuration BookMarkStory
        /// </summary>
        public void Configure(EntityTypeBuilder<BookMarkStory> builder)
        {
            builder.ToTable(nameof(BookMarkStory));
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).UseIdentityColumn();
            builder.HasOne(x => x.UserMember)
                .WithMany(x => x.BookMarkStory)
                .HasForeignKey(x => x.UserGuid);
        }
    }
}
