// <auto-generated />
using API_mk1.Context.PitcherContext;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace API_mk1.Migrations
{
    [DbContext(typeof(PitcherContext))]
    [Migration("20211122135058_crunchMk8")]
    partial class crunchMk8
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.7")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("API_mk1.Models.Project.ProjectModel", b =>
                {
                    b.Property<string>("ProjectId")
                        .HasColumnType("nvarchar(250)");

                    b.Property<string>("Body")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<long>("CreatedAtUnix")
                        .HasColumnType("bigint");

                    b.Property<string>("OwnerId")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ProjectId");

                    b.ToTable("Projects_Table");
                });

            modelBuilder.Entity("API_mk1.Models.User.UserModel", b =>
                {
                    b.Property<string>("UserId")
                        .HasMaxLength(250)
                        .HasColumnType("nvarchar(250)");

                    b.Property<bool>("IsContractor")
                        .HasColumnType("bit");

                    b.Property<string>("UserName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("UserId");

                    b.HasIndex("UserId")
                        .IsUnique();

                    b.ToTable("Users_Table");
                });

            modelBuilder.Entity("API_mk1.Models.Project.ProjectModel", b =>
                {
                    b.HasOne("API_mk1.Models.User.UserModel", "UserModel")
                        .WithMany("ProjectModel")
                        .HasForeignKey("ProjectId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("UserModel");
                });

            modelBuilder.Entity("API_mk1.Models.User.UserModel", b =>
                {
                    b.Navigation("ProjectModel");
                });
#pragma warning restore 612, 618
        }
    }
}
