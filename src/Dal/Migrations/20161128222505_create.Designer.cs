using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Dal;

namespace Dal.Migrations
{
    [DbContext(typeof(BusinessProContext))]
    [Migration("20161128222505_create")]
    partial class create
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "1.1.0-rtm-22752");

            modelBuilder.Entity("Dal.Models.Department", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Name");

                    b.Property<int>("RoomId");

                    b.HasKey("Id");

                    b.HasIndex("RoomId");

                    b.ToTable("Departments");
                });

            modelBuilder.Entity("Dal.Models.Person", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Name");

                    b.Property<int?>("RoleId");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("People");
                });

            modelBuilder.Entity("Dal.Models.PersonDepartment", b =>
                {
                    b.Property<int>("PersonId");

                    b.Property<int>("DepartmentId");

                    b.HasKey("PersonId", "DepartmentId");

                    b.HasIndex("DepartmentId");

                    b.ToTable("PersonDepartment");
                });

            modelBuilder.Entity("Dal.Models.Role", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Name");

                    b.HasKey("Id");

                    b.ToTable("Roles");
                });

            modelBuilder.Entity("Dal.Models.Room", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Name");

                    b.HasKey("Id");

                    b.ToTable("Rooms");
                });

            modelBuilder.Entity("Dal.Models.Department", b =>
                {
                    b.HasOne("Dal.Models.Room", "Room")
                        .WithMany("Departments")
                        .HasForeignKey("RoomId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Dal.Models.Person", b =>
                {
                    b.HasOne("Dal.Models.Role", "Role")
                        .WithMany("People")
                        .HasForeignKey("RoleId");
                });

            modelBuilder.Entity("Dal.Models.PersonDepartment", b =>
                {
                    b.HasOne("Dal.Models.Department", "Department")
                        .WithMany("PersonDepartments")
                        .HasForeignKey("DepartmentId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Dal.Models.Person", "Person")
                        .WithMany("PersonDepartments")
                        .HasForeignKey("PersonId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
        }
    }
}
