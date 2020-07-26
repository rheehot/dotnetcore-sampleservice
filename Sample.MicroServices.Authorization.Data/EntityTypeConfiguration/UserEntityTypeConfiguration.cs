using System;
using System.Collections.Generic;
using System.Text;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using Sample.MicroServices.Entities;

namespace Sample.MicroServices.Authorization.Data.EntityTypeConfiguration
{
    internal class UserEntityTypeConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasKey(x => x.Id);

            builder
                .Property(x => x.Id)
                .HasMaxLength(StringLengths.Identifier)
                .IsRequired()
                .HasComment("식별자")
                ;
            builder
                .Property(x => x.FirstName)
                .HasMaxLength(StringLengths.Name)
                .IsRequired()
                .HasComment("성")
                ;
            builder
                .Property(x => x.LastName)
                .HasMaxLength(StringLengths.Name)
                .IsRequired()
                .HasComment("이름")
                ;
            builder
                .Property(x => x.UserName)
                .HasMaxLength(StringLengths.Name)
                .IsRequired()
                .HasComment("계정이름")
                ;
            builder
                .Property(x => x.Password)
                .HasMaxLength(StringLengths.Long)
                .HasComment("비밀번호")
                ;                
        }
    }
}
