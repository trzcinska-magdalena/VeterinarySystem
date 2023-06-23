﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using VeterinarySystem.Data;

#nullable disable

namespace VeterinarySystem.Migrations
{
    [DbContext(typeof(VeterinarySystemContext))]
    [Migration("20230623113255_changeNamesOfTables")]
    partial class changeNamesOfTables
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.7")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("VeterinarySystem.Models.Db.Account", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("Admin")
                        .HasColumnType("int");

                    b.Property<string>("Login")
                        .IsRequired()
                        .HasMaxLength(30)
                        .HasColumnType("nvarchar(30)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)");

                    b.HasKey("Id");

                    b.ToTable("Account", (string)null);
                });

            modelBuilder.Entity("VeterinarySystem.Models.Db.Animal", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("BirthDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("BreedId")
                        .HasColumnType("int");

                    b.Property<int>("ClientId")
                        .HasColumnType("int");

                    b.Property<string>("Gender")
                        .IsRequired()
                        .HasMaxLength(1)
                        .HasColumnType("nvarchar(1)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("Id");

                    b.HasIndex("BreedId");

                    b.HasIndex("ClientId");

                    b.ToTable("Animal", (string)null);
                });

            modelBuilder.Entity("VeterinarySystem.Models.Db.Appointment", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("AnimalId")
                        .HasColumnType("int");

                    b.Property<DateTime>("Date")
                        .HasColumnType("datetime2");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.HasKey("Id");

                    b.HasIndex("AnimalId");

                    b.ToTable("Appointment", (string)null);
                });

            modelBuilder.Entity("VeterinarySystem.Models.Db.AppointmentMedicine", b =>
                {
                    b.Property<int>("AppointmentId")
                        .HasColumnType("int");

                    b.Property<int>("MedicineId")
                        .HasColumnType("int");

                    b.Property<int>("Amount")
                        .HasColumnType("int");

                    b.HasKey("AppointmentId", "MedicineId");

                    b.HasIndex("MedicineId");

                    b.ToTable("AppointmentMedicine", (string)null);
                });

            modelBuilder.Entity("VeterinarySystem.Models.Db.AppointmentSurgery", b =>
                {
                    b.Property<int>("AppointmentId")
                        .HasColumnType("int");

                    b.Property<int>("SurgeryId")
                        .HasColumnType("int");

                    b.HasKey("AppointmentId", "SurgeryId");

                    b.HasIndex("SurgeryId");

                    b.ToTable("AppointmentSurgery", (string)null);
                });

            modelBuilder.Entity("VeterinarySystem.Models.Db.AppointmentVet", b =>
                {
                    b.Property<int>("AppointmentId")
                        .HasColumnType("int");

                    b.Property<int>("VetId")
                        .HasColumnType("int");

                    b.HasKey("AppointmentId", "VetId");

                    b.HasIndex("VetId");

                    b.ToTable("AppointmentVet", (string)null);
                });

            modelBuilder.Entity("VeterinarySystem.Models.Db.Breed", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(30)
                        .HasColumnType("nvarchar(30)");

                    b.HasKey("Id");

                    b.ToTable("Breed", (string)null);
                });

            modelBuilder.Entity("VeterinarySystem.Models.Db.Client", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("City")
                        .IsRequired()
                        .HasMaxLength(150)
                        .HasColumnType("nvarchar(150)");

                    b.Property<string>("Email")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasMaxLength(30)
                        .HasColumnType("nvarchar(30)");

                    b.Property<string>("LastMame")
                        .IsRequired()
                        .HasMaxLength(70)
                        .HasColumnType("nvarchar(70)");

                    b.Property<string>("PhoneNumber")
                        .IsRequired()
                        .HasMaxLength(12)
                        .HasColumnType("nvarchar(12)");

                    b.HasKey("Id");

                    b.ToTable("Client", (string)null);
                });

            modelBuilder.Entity("VeterinarySystem.Models.Db.Medicine", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("Id");

                    b.ToTable("Medicine", (string)null);
                });

            modelBuilder.Entity("VeterinarySystem.Models.Db.Specialisation", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(30)
                        .HasColumnType("nvarchar(30)");

                    b.HasKey("Id");

                    b.ToTable("Specialisation", (string)null);
                });

            modelBuilder.Entity("VeterinarySystem.Models.Db.Surgery", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(300)
                        .HasColumnType("nvarchar(300)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(30)
                        .HasColumnType("nvarchar(30)");

                    b.HasKey("Id");

                    b.ToTable("Surgery", (string)null);
                });

            modelBuilder.Entity("VeterinarySystem.Models.Db.TypeOfVaccine", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("Id");

                    b.ToTable("TypeOfVaccine", (string)null);
                });

            modelBuilder.Entity("VeterinarySystem.Models.Db.Vaccination", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("AnimalId")
                        .HasColumnType("int");

                    b.Property<DateTime>("Date")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("ExpiryDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("TypeOfVaccineId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("AnimalId");

                    b.HasIndex("TypeOfVaccineId");

                    b.ToTable("Vaccination", (string)null);
                });

            modelBuilder.Entity("VeterinarySystem.Models.Db.Vet", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("AccountId")
                        .HasColumnType("int");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasMaxLength(30)
                        .HasColumnType("nvarchar(30)");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)");

                    b.HasKey("Id");

                    b.HasIndex("AccountId");

                    b.ToTable("Vet", (string)null);
                });

            modelBuilder.Entity("VeterinarySystem.Models.Db.VetSpecialisation", b =>
                {
                    b.Property<int>("SpecialisationId")
                        .HasColumnType("int");

                    b.Property<int>("VetId")
                        .HasColumnType("int");

                    b.Property<DateTime>("DateFrom")
                        .HasColumnType("datetime2");

                    b.HasKey("SpecialisationId", "VetId");

                    b.HasIndex("VetId");

                    b.ToTable("VetSpecialisation", (string)null);
                });

            modelBuilder.Entity("VeterinarySystem.Models.Db.Weight", b =>
                {
                    b.Property<int>("AnimalId")
                        .HasColumnType("int");

                    b.Property<DateTime>("Date")
                        .HasColumnType("datetime2");

                    b.Property<double>("Value")
                        .HasColumnType("float");

                    b.HasKey("AnimalId", "Date");

                    b.ToTable("Weight", (string)null);
                });

            modelBuilder.Entity("VeterinarySystem.Models.Db.Animal", b =>
                {
                    b.HasOne("VeterinarySystem.Models.Db.Breed", "Breed")
                        .WithMany("Animals")
                        .HasForeignKey("BreedId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("VeterinarySystem.Models.Db.Client", "Client")
                        .WithMany("Animals")
                        .HasForeignKey("ClientId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Breed");

                    b.Navigation("Client");
                });

            modelBuilder.Entity("VeterinarySystem.Models.Db.Appointment", b =>
                {
                    b.HasOne("VeterinarySystem.Models.Db.Animal", "Animal")
                        .WithMany("Appointments")
                        .HasForeignKey("AnimalId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Animal");
                });

            modelBuilder.Entity("VeterinarySystem.Models.Db.AppointmentMedicine", b =>
                {
                    b.HasOne("VeterinarySystem.Models.Db.Appointment", "Appointment")
                        .WithMany("AppointmentMedicines")
                        .HasForeignKey("AppointmentId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("VeterinarySystem.Models.Db.Medicine", "Medicine")
                        .WithMany("AppointmentMedicines")
                        .HasForeignKey("MedicineId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Appointment");

                    b.Navigation("Medicine");
                });

            modelBuilder.Entity("VeterinarySystem.Models.Db.AppointmentSurgery", b =>
                {
                    b.HasOne("VeterinarySystem.Models.Db.Appointment", "Appointment")
                        .WithMany("AppointmentSurgeries")
                        .HasForeignKey("AppointmentId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("VeterinarySystem.Models.Db.Surgery", "Surgery")
                        .WithMany("AppointmentSurgeries")
                        .HasForeignKey("SurgeryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Appointment");

                    b.Navigation("Surgery");
                });

            modelBuilder.Entity("VeterinarySystem.Models.Db.AppointmentVet", b =>
                {
                    b.HasOne("VeterinarySystem.Models.Db.Appointment", "Appointment")
                        .WithMany("AppointmentVets")
                        .HasForeignKey("AppointmentId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("VeterinarySystem.Models.Db.Vet", "Vet")
                        .WithMany("AppointmentVets")
                        .HasForeignKey("VetId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Appointment");

                    b.Navigation("Vet");
                });

            modelBuilder.Entity("VeterinarySystem.Models.Db.Vaccination", b =>
                {
                    b.HasOne("VeterinarySystem.Models.Db.Animal", "Animal")
                        .WithMany("Vaccinations")
                        .HasForeignKey("AnimalId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("VeterinarySystem.Models.Db.TypeOfVaccine", "TypeOfVaccine")
                        .WithMany("Vaccinations")
                        .HasForeignKey("TypeOfVaccineId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Animal");

                    b.Navigation("TypeOfVaccine");
                });

            modelBuilder.Entity("VeterinarySystem.Models.Db.Vet", b =>
                {
                    b.HasOne("VeterinarySystem.Models.Db.Account", "Account")
                        .WithMany("Vets")
                        .HasForeignKey("AccountId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Account");
                });

            modelBuilder.Entity("VeterinarySystem.Models.Db.VetSpecialisation", b =>
                {
                    b.HasOne("VeterinarySystem.Models.Db.Specialisation", "Specialisation")
                        .WithMany("VetSpecialisations")
                        .HasForeignKey("SpecialisationId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("VeterinarySystem.Models.Db.Vet", "Vet")
                        .WithMany("VetSpecialisations")
                        .HasForeignKey("VetId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Specialisation");

                    b.Navigation("Vet");
                });

            modelBuilder.Entity("VeterinarySystem.Models.Db.Weight", b =>
                {
                    b.HasOne("VeterinarySystem.Models.Db.Animal", "Animal")
                        .WithMany("Weights")
                        .HasForeignKey("AnimalId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Animal");
                });

            modelBuilder.Entity("VeterinarySystem.Models.Db.Account", b =>
                {
                    b.Navigation("Vets");
                });

            modelBuilder.Entity("VeterinarySystem.Models.Db.Animal", b =>
                {
                    b.Navigation("Appointments");

                    b.Navigation("Vaccinations");

                    b.Navigation("Weights");
                });

            modelBuilder.Entity("VeterinarySystem.Models.Db.Appointment", b =>
                {
                    b.Navigation("AppointmentMedicines");

                    b.Navigation("AppointmentSurgeries");

                    b.Navigation("AppointmentVets");
                });

            modelBuilder.Entity("VeterinarySystem.Models.Db.Breed", b =>
                {
                    b.Navigation("Animals");
                });

            modelBuilder.Entity("VeterinarySystem.Models.Db.Client", b =>
                {
                    b.Navigation("Animals");
                });

            modelBuilder.Entity("VeterinarySystem.Models.Db.Medicine", b =>
                {
                    b.Navigation("AppointmentMedicines");
                });

            modelBuilder.Entity("VeterinarySystem.Models.Db.Specialisation", b =>
                {
                    b.Navigation("VetSpecialisations");
                });

            modelBuilder.Entity("VeterinarySystem.Models.Db.Surgery", b =>
                {
                    b.Navigation("AppointmentSurgeries");
                });

            modelBuilder.Entity("VeterinarySystem.Models.Db.TypeOfVaccine", b =>
                {
                    b.Navigation("Vaccinations");
                });

            modelBuilder.Entity("VeterinarySystem.Models.Db.Vet", b =>
                {
                    b.Navigation("AppointmentVets");

                    b.Navigation("VetSpecialisations");
                });
#pragma warning restore 612, 618
        }
    }
}
