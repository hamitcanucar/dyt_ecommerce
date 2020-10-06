﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using dytsenayasar.Context;

namespace dytsenayasar.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20201005115835_init_13")]
    partial class init_13
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Npgsql:PostgresExtension:uuid-ossp", ",,")
                .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn)
                .HasAnnotation("ProductVersion", "3.1.8")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            modelBuilder.Entity("dytsenayasar.DataAccess.Entities.User", b =>
                {
                    b.Property<Guid>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("id")
                        .HasColumnType("uuid")
                        .HasDefaultValueSql("uuid_generate_v4()");

                    b.Property<bool>("Active")
                        .HasColumnName("active")
                        .HasColumnType("boolean");

                    b.Property<DateTime>("BirthDay")
                        .HasColumnName("birth_day")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("City")
                        .HasColumnType("text");

                    b.Property<DateTime>("CreateTime")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("create_time")
                        .HasColumnType("timestamp without time zone")
                        .HasDefaultValueSql("now()");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnName("email")
                        .HasColumnType("varchar(64)");

                    b.Property<string>("FirstName")
                        .HasColumnName("first_name")
                        .HasColumnType("varchar(64)");

                    b.Property<string>("Gender")
                        .IsRequired()
                        .ValueGeneratedOnAdd()
                        .HasColumnName("gender")
                        .HasColumnType("varchar(16)")
                        .HasDefaultValue("Male");

                    b.Property<Guid?>("Image")
                        .HasColumnName("image")
                        .HasColumnType("uuid");

                    b.Property<string>("LastName")
                        .HasColumnName("last_name")
                        .HasColumnType("varchar(64)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnName("password")
                        .HasColumnType("varchar(128)");

                    b.Property<string>("PersonalId")
                        .IsRequired()
                        .HasColumnName("personal_id")
                        .HasColumnType("varchar(64)");

                    b.Property<string>("Phone")
                        .HasColumnName("phone")
                        .HasColumnType("varchar(32)");

                    b.Property<string>("UserType")
                        .IsRequired()
                        .ValueGeneratedOnAdd()
                        .HasColumnName("user_type")
                        .HasColumnType("varchar(16)")
                        .HasDefaultValue("User");

                    b.HasKey("ID");

                    b.HasIndex("Email")
                        .IsUnique();

                    b.HasIndex("Gender")
                        .HasAnnotation("Npgsql:IndexMethod", "hash");

                    b.HasIndex("PersonalId")
                        .IsUnique();

                    b.HasIndex("UserType")
                        .HasAnnotation("Npgsql:IndexMethod", "hash");

                    b.ToTable("user");
                });

            modelBuilder.Entity("dytsenayasar.DataAccess.Entities.UserClient", b =>
                {
                    b.Property<Guid>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("id")
                        .HasColumnType("uuid")
                        .HasDefaultValueSql("uuid_generate_v4()");

                    b.Property<string>("ClientId")
                        .HasColumnName("clientid")
                        .HasColumnType("text");

                    b.Property<byte[]>("ClientIdHash")
                        .HasColumnName("clientid_hash")
                        .HasColumnType("bytea");

                    b.Property<DateTime>("CreateTime")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("create_time")
                        .HasColumnType("timestamp without time zone")
                        .HasDefaultValueSql("now()");

                    b.Property<Guid>("UserId")
                        .HasColumnName("user_id")
                        .HasColumnType("uuid");

                    b.HasKey("ID");

                    b.HasIndex("ClientIdHash")
                        .HasAnnotation("Npgsql:IndexMethod", "hash");

                    b.HasIndex("UserId")
                        .IsUnique();

                    b.ToTable("user_client");
                });

            modelBuilder.Entity("dytsenayasar.DataAccess.Entities.UserFile", b =>
                {
                    b.Property<Guid>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("id")
                        .HasColumnType("uuid")
                        .HasDefaultValueSql("uuid_generate_v4()");

                    b.Property<DateTime>("CreateTime")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("create_time")
                        .HasColumnType("timestamp without time zone")
                        .HasDefaultValueSql("now()");

                    b.Property<DateTime?>("CreatedOn")
                        .HasColumnName("created_on")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("FileName")
                        .IsRequired()
                        .HasColumnName("file_name")
                        .HasColumnType("varchar(128)");

                    b.Property<string>("FileType")
                        .HasColumnName("file_type")
                        .HasColumnType("varchar(255)");

                    b.Property<Guid>("UserId")
                        .HasColumnName("user_id")
                        .HasColumnType("uuid");

                    b.HasKey("ID");

                    b.HasIndex("UserId");

                    b.ToTable("user_file");
                });

            modelBuilder.Entity("dytsenayasar.DataAccess.Entities.UserForm", b =>
                {
                    b.Property<Guid>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("id")
                        .HasColumnType("uuid")
                        .HasDefaultValueSql("uuid_generate_v4()");

                    b.Property<string>("AllergyFoods")
                        .HasColumnType("text");

                    b.Property<string>("Anemia")
                        .HasColumnType("text");

                    b.Property<string>("BadHabits")
                        .HasColumnType("text");

                    b.Property<string>("BestFoods")
                        .HasColumnType("text");

                    b.Property<string>("BreakfastFoods")
                        .HasColumnType("text");

                    b.Property<string>("BreakfastTime")
                        .HasColumnType("text");

                    b.Property<string>("Breastfeed")
                        .HasColumnType("text");

                    b.Property<string>("CallTimes")
                        .HasColumnType("text");

                    b.Property<string>("CardiovascularDiseases")
                        .HasColumnType("text");

                    b.Property<int>("Chest")
                        .HasColumnType("integer");

                    b.Property<string>("ContinuousDrugs")
                        .HasColumnType("text");

                    b.Property<DateTime>("CreateTime")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("create_time")
                        .HasColumnType("timestamp without time zone")
                        .HasDefaultValueSql("now()");

                    b.Property<string>("Diabetes")
                        .HasColumnType("text");

                    b.Property<int>("DietType")
                        .HasColumnType("integer");

                    b.Property<string>("DinnerFoods")
                        .HasColumnType("text");

                    b.Property<string>("DinnerTime")
                        .HasColumnType("text");

                    b.Property<string>("Drugs")
                        .HasColumnType("text");

                    b.Property<string>("Family")
                        .HasColumnType("text");

                    b.Property<string>("FavoriteBreakfastFoods")
                        .HasColumnType("text");

                    b.Property<string>("FavoriteFruits")
                        .HasColumnType("text");

                    b.Property<string>("FavoriteMeatFoods")
                        .HasColumnType("text");

                    b.Property<string>("FavoriteVegetablesFoods")
                        .HasColumnType("text");

                    b.Property<string>("FoodsUntilSleep")
                        .HasColumnType("text");

                    b.Property<string>("Hospital")
                        .HasColumnType("text");

                    b.Property<bool?>("IsOrderRegl")
                        .HasColumnType("boolean");

                    b.Property<bool?>("IsRegl")
                        .HasColumnType("boolean");

                    b.Property<string>("Job")
                        .HasColumnType("text");

                    b.Property<int>("Length")
                        .HasColumnType("integer");

                    b.Property<string>("LunchFoods")
                        .HasColumnType("text");

                    b.Property<string>("LunchTime")
                        .HasColumnType("text");

                    b.Property<string>("LungInfection")
                        .HasColumnType("text");

                    b.Property<float>("MaxWeight")
                        .HasColumnType("real");

                    b.Property<string>("Methods")
                        .HasColumnType("text");

                    b.Property<float>("MinWeight")
                        .HasColumnType("real");

                    b.Property<string>("NoteToDietitian")
                        .HasColumnType("text");

                    b.Property<string>("Operation")
                        .HasColumnType("text");

                    b.Property<string>("OralDiseases")
                        .HasColumnType("text");

                    b.Property<string>("Parturition")
                        .HasColumnType("text");

                    b.Property<string>("Phone2")
                        .HasColumnType("text");

                    b.Property<string>("References")
                        .HasColumnType("text");

                    b.Property<string>("SleepPatterns")
                        .HasColumnType("text");

                    b.Property<string>("SleepTime")
                        .HasColumnType("text");

                    b.Property<string>("Sports")
                        .HasColumnType("text");

                    b.Property<string>("StomachAndIntestineDiseases")
                        .HasColumnType("text");

                    b.Property<string>("ThyroidDiseases")
                        .HasColumnType("text");

                    b.Property<string>("ToiletFrequency")
                        .HasColumnType("text");

                    b.Property<string>("UrinaryInfection")
                        .HasColumnType("text");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uuid");

                    b.Property<int>("Waist")
                        .HasColumnType("integer");

                    b.Property<string>("WakeUpTime")
                        .HasColumnType("text");

                    b.Property<float>("Weight")
                        .HasColumnType("real");

                    b.HasKey("ID");

                    b.HasIndex("UserId")
                        .IsUnique();

                    b.ToTable("user_form");
                });

            modelBuilder.Entity("dytsenayasar.DataAccess.Entities.UserRequest", b =>
                {
                    b.Property<Guid>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("id")
                        .HasColumnType("uuid")
                        .HasDefaultValueSql("uuid_generate_v4()");

                    b.Property<DateTime>("CreateTime")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("create_time")
                        .HasColumnType("timestamp without time zone")
                        .HasDefaultValueSql("now()");

                    b.Property<string>("RequestType")
                        .IsRequired()
                        .ValueGeneratedOnAdd()
                        .HasColumnName("request_type")
                        .HasColumnType("varchar(16)")
                        .HasDefaultValue("PasswordReset");

                    b.Property<string>("Token")
                        .HasColumnName("token")
                        .HasColumnType("varchar(64)");

                    b.Property<Guid>("UserId")
                        .HasColumnName("user_id")
                        .HasColumnType("uuid");

                    b.Property<DateTime>("ValidityDate")
                        .HasColumnName("validity_date")
                        .HasColumnType("timestamp without time zone");

                    b.HasKey("ID");

                    b.HasIndex("RequestType")
                        .HasAnnotation("Npgsql:IndexMethod", "hash");

                    b.HasIndex("UserId");

                    b.ToTable("user_request");
                });

            modelBuilder.Entity("dytsenayasar.DataAccess.Entities.UserScale", b =>
                {
                    b.Property<Guid>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("id")
                        .HasColumnType("uuid")
                        .HasDefaultValueSql("uuid_generate_v4()");

                    b.Property<int>("Cheest")
                        .HasColumnType("integer");

                    b.Property<DateTime>("CreateTime")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("create_time")
                        .HasColumnType("timestamp without time zone")
                        .HasDefaultValueSql("now()");

                    b.Property<int>("Hip")
                        .HasColumnName("hip")
                        .HasColumnType("integer");

                    b.Property<int>("Leg")
                        .HasColumnName("leg")
                        .HasColumnType("integer");

                    b.Property<int>("UpperArm")
                        .HasColumnName("upper_arm")
                        .HasColumnType("integer");

                    b.Property<Guid>("UserId")
                        .HasColumnName("user_id")
                        .HasColumnType("uuid");

                    b.Property<int>("Waist")
                        .HasColumnName("waist")
                        .HasColumnType("integer");

                    b.Property<float>("Weigt")
                        .HasColumnName("weight")
                        .HasColumnType("real");

                    b.HasKey("ID");

                    b.HasIndex("UserId");

                    b.ToTable("user_scale");
                });

            modelBuilder.Entity("dytsenayasar.DataAccess.Entities.UserClient", b =>
                {
                    b.HasOne("dytsenayasar.DataAccess.Entities.User", "User")
                        .WithOne("Client")
                        .HasForeignKey("dytsenayasar.DataAccess.Entities.UserClient", "UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("dytsenayasar.DataAccess.Entities.UserFile", b =>
                {
                    b.HasOne("dytsenayasar.DataAccess.Entities.User", "User")
                        .WithMany("UserFiles")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("dytsenayasar.DataAccess.Entities.UserForm", b =>
                {
                    b.HasOne("dytsenayasar.DataAccess.Entities.User", "User")
                        .WithOne("Form")
                        .HasForeignKey("dytsenayasar.DataAccess.Entities.UserForm", "UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("dytsenayasar.DataAccess.Entities.UserRequest", b =>
                {
                    b.HasOne("dytsenayasar.DataAccess.Entities.User", "User")
                        .WithMany("Requests")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("dytsenayasar.DataAccess.Entities.UserScale", b =>
                {
                    b.HasOne("dytsenayasar.DataAccess.Entities.User", "User")
                        .WithMany("UserScales")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
