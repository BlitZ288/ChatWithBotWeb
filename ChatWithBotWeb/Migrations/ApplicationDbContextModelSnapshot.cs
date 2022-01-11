﻿// <auto-generated />
using System;
using System.Collections.Generic;
using ChatWithBotWeb.Models.Db;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace ChatWithBotWeb.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    partial class ApplicationDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 63)
                .HasAnnotation("ProductVersion", "5.0.13")
                .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            modelBuilder.Entity("ChatUser", b =>
                {
                    b.Property<int>("ChatsChatId")
                        .HasColumnType("integer");

                    b.Property<string>("UsersId")
                        .HasColumnType("text");

                    b.HasKey("ChatsChatId", "UsersId");

                    b.HasIndex("UsersId");

                    b.ToTable("ChatUser");
                });

            modelBuilder.Entity("ChatWithBotWeb.Models.Chat", b =>
                {
                    b.Property<int>("ChatId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<List<string>>("NameBots")
                        .HasColumnType("text[]");

                    b.HasKey("ChatId");

                    b.ToTable("Chats");
                });

            modelBuilder.Entity("ChatWithBotWeb.Models.LogAction", b =>
                {
                    b.Property<int>("LogActionId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<int?>("ChatId")
                        .HasColumnType("integer");

                    b.Property<int>("Content")
                        .HasColumnType("integer");

                    b.Property<DateTime>("FixLog")
                        .HasColumnType("timestamp without time zone");

                    b.Property<bool>("Undread")
                        .HasColumnType("boolean");

                    b.Property<string>("UserId")
                        .HasColumnType("text");

                    b.HasKey("LogActionId");

                    b.HasIndex("ChatId");

                    b.HasIndex("UserId");

                    b.ToTable("LogActions");
                });

            modelBuilder.Entity("ChatWithBotWeb.Models.LogsUser", b =>
                {
                    b.Property<int>("LogsUserId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<int?>("ChatId")
                        .HasColumnType("integer");

                    b.Property<DateTime>("StartChat")
                        .HasColumnType("timestamp without time zone");

                    b.Property<DateTime?>("StopChat")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("UserId")
                        .HasColumnType("text");

                    b.HasKey("LogsUserId");

                    b.HasIndex("ChatId");

                    b.HasIndex("UserId");

                    b.ToTable("LogsUsers");
                });

            modelBuilder.Entity("ChatWithBotWeb.Models.Message", b =>
                {
                    b.Property<int>("MessageId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<int?>("ChatId")
                        .HasColumnType("integer");

                    b.Property<string>("Content")
                        .HasColumnType("text");

                    b.Property<string>("NameBot")
                        .HasColumnType("text");

                    b.Property<bool>("Undread")
                        .HasColumnType("boolean");

                    b.Property<string>("UserId")
                        .HasColumnType("text");

                    b.Property<DateTime>("dateTime")
                        .HasColumnType("timestamp without time zone");

                    b.HasKey("MessageId");

                    b.HasIndex("ChatId");

                    b.HasIndex("UserId");

                    b.ToTable("Messages");
                });

            modelBuilder.Entity("ChatWithBotWeb.Models.User", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("text");

                    b.Property<int>("AccessFailedCount")
                        .HasColumnType("integer");

                    b.Property<string>("ConcurrencyStamp")
                        .HasColumnType("text");

                    b.Property<string>("Email")
                        .HasColumnType("text");

                    b.Property<bool>("EmailConfirmed")
                        .HasColumnType("boolean");

                    b.Property<bool>("LockoutEnabled")
                        .HasColumnType("boolean");

                    b.Property<DateTimeOffset?>("LockoutEnd")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Name")
                        .HasColumnType("text");

                    b.Property<string>("NormalizedEmail")
                        .HasColumnType("text");

                    b.Property<string>("NormalizedUserName")
                        .HasColumnType("text");

                    b.Property<string>("Password")
                        .HasColumnType("text");

                    b.Property<string>("PasswordHash")
                        .HasColumnType("text");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("text");

                    b.Property<bool>("PhoneNumberConfirmed")
                        .HasColumnType("boolean");

                    b.Property<string>("SecurityStamp")
                        .HasColumnType("text");

                    b.Property<bool>("TwoFactorEnabled")
                        .HasColumnType("boolean");

                    b.Property<string>("UserName")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("ChatUser", b =>
                {
                    b.HasOne("ChatWithBotWeb.Models.Chat", null)
                        .WithMany()
                        .HasForeignKey("ChatsChatId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ChatWithBotWeb.Models.User", null)
                        .WithMany()
                        .HasForeignKey("UsersId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("ChatWithBotWeb.Models.LogAction", b =>
                {
                    b.HasOne("ChatWithBotWeb.Models.Chat", "Chat")
                        .WithMany("LogActions")
                        .HasForeignKey("ChatId");

                    b.HasOne("ChatWithBotWeb.Models.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId");

                    b.Navigation("Chat");

                    b.Navigation("User");
                });

            modelBuilder.Entity("ChatWithBotWeb.Models.LogsUser", b =>
                {
                    b.HasOne("ChatWithBotWeb.Models.Chat", "Chat")
                        .WithMany("ChatLogUsers")
                        .HasForeignKey("ChatId");

                    b.HasOne("ChatWithBotWeb.Models.User", "User")
                        .WithMany("LogsUsers")
                        .HasForeignKey("UserId");

                    b.Navigation("Chat");

                    b.Navigation("User");
                });

            modelBuilder.Entity("ChatWithBotWeb.Models.Message", b =>
                {
                    b.HasOne("ChatWithBotWeb.Models.Chat", "Chat")
                        .WithMany("ListMessage")
                        .HasForeignKey("ChatId");

                    b.HasOne("ChatWithBotWeb.Models.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId");

                    b.Navigation("Chat");

                    b.Navigation("User");
                });

            modelBuilder.Entity("ChatWithBotWeb.Models.Chat", b =>
                {
                    b.Navigation("ChatLogUsers");

                    b.Navigation("ListMessage");

                    b.Navigation("LogActions");
                });

            modelBuilder.Entity("ChatWithBotWeb.Models.User", b =>
                {
                    b.Navigation("LogsUsers");
                });
#pragma warning restore 612, 618
        }
    }
}
