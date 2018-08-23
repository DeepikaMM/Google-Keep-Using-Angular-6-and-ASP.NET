﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using TodoNotes.Models;

namespace TodoNotes.Migrations
{
    [DbContext(typeof(TodoNotesContext))]
    [Migration("20180822045835_intial")]
    partial class intial
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.1.1-rtm-30846")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("TodoNotes.Models.CheckList", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("CheckListName");

                    b.Property<bool>("CheckListStatus");

                    b.Property<int?>("NotesId");

                    b.HasKey("Id");

                    b.HasIndex("NotesId");

                    b.ToTable("CheckList");
                });

            modelBuilder.Entity("TodoNotes.Models.Label", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("LabelName");

                    b.Property<int?>("NotesId");

                    b.HasKey("Id");

                    b.HasIndex("NotesId");

                    b.ToTable("Label");
                });

            modelBuilder.Entity("TodoNotes.Models.Notes", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<bool>("PinStatus");

                    b.Property<string>("PlainText");

                    b.Property<string>("Title");

                    b.HasKey("Id");

                    b.ToTable("Notes");
                });

            modelBuilder.Entity("TodoNotes.Models.CheckList", b =>
                {
                    b.HasOne("TodoNotes.Models.Notes")
                        .WithMany("checkList")
                        .HasForeignKey("NotesId");
                });

            modelBuilder.Entity("TodoNotes.Models.Label", b =>
                {
                    b.HasOne("TodoNotes.Models.Notes")
                        .WithMany("label")
                        .HasForeignKey("NotesId");
                });
#pragma warning restore 612, 618
        }
    }
}
