﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using SampleService.Data;

namespace SampleService.Data.SqlServer.Migrations
{
    [DbContext(typeof(DataContext))]
    [Migration("20200829065037_Add_autogenerate_identity")]
    partial class Add_autogenerate_identity
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.6")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("SampleService.Entities.RefreshToken", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("nvarchar(36)")
                        .HasComment("식별자")
                        .HasMaxLength(36);

                    b.Property<DateTimeOffset>("Created")
                        .HasColumnType("datetimeoffset")
                        .HasComment("작성시각");

                    b.Property<string>("CreatedByIp")
                        .IsRequired()
                        .HasColumnType("nvarchar(100)")
                        .HasComment("작성 요청 아이피 주소")
                        .HasMaxLength(100);

                    b.Property<DateTimeOffset>("Expires")
                        .HasColumnType("datetimeoffset")
                        .HasComment("만료시각");

                    b.Property<string>("ReplacedByToken")
                        .HasColumnType("nvarchar(4000)")
                        .HasComment("취소 요청 토큰")
                        .HasMaxLength(4000);

                    b.Property<DateTimeOffset?>("Revoked")
                        .HasColumnType("datetimeoffset")
                        .HasComment("취소시각");

                    b.Property<string>("RevokedByIp")
                        .HasColumnType("nvarchar(100)")
                        .HasComment("취소 요청 아이피 주소")
                        .HasMaxLength(100);

                    b.Property<string>("Token")
                        .IsRequired()
                        .HasColumnType("nvarchar(4000)")
                        .HasComment("리프레시 토큰")
                        .HasMaxLength(4000);

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(36)")
                        .HasComment("사용자 식별자")
                        .HasMaxLength(36);

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("RefreshToken");
                });

            modelBuilder.Entity("SampleService.Entities.User", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("nvarchar(36)")
                        .HasComment("식별자")
                        .HasMaxLength(36);

                    b.Property<int>("FailCount")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasComment("인증 실패수")
                        .HasDefaultValue(0);

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("nvarchar(100)")
                        .HasComment("성")
                        .HasMaxLength(100);

                    b.Property<bool>("IsEnabled")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bit")
                        .HasComment("사용여부")
                        .HasDefaultValue(true);

                    b.Property<bool>("IsLocked")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bit")
                        .HasComment("계정 잠금 여부")
                        .HasDefaultValue(false);

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("nvarchar(100)")
                        .HasComment("이름")
                        .HasMaxLength(100);

                    b.Property<string>("Password")
                        .HasColumnType("nvarchar(4000)")
                        .HasComment("비밀번호")
                        .HasMaxLength(4000);

                    b.Property<string>("UserName")
                        .IsRequired()
                        .HasColumnType("nvarchar(100)")
                        .HasComment("계정이름")
                        .HasMaxLength(100);

                    b.HasKey("Id");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("SampleService.Entities.RefreshToken", b =>
                {
                    b.HasOne("SampleService.Entities.User", "User")
                        .WithMany("RefreshTokens")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}