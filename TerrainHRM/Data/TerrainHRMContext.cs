using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using TerrainHRM.Models;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace TerrainHRM.Data
{
    public partial class TerrainHRMContext : DbContext
    {
        public TerrainHRMContext()
        {
        }

        public TerrainHRMContext(DbContextOptions<TerrainHRMContext> options)
            : base(options)
        {
        }

        public virtual DbSet<AspNetRoleClaims> AspNetRoleClaims { get; set; }
        public virtual DbSet<AspNetRoles> AspNetRoles { get; set; }
        public virtual DbSet<AspNetUserClaims> AspNetUserClaims { get; set; }
        public virtual DbSet<AspNetUserLogins> AspNetUserLogins { get; set; }
        public virtual DbSet<AspNetUserRoles> AspNetUserRoles { get; set; }
        public virtual DbSet<AspNetUserTokens> AspNetUserTokens { get; set; }
        public virtual DbSet<AspNetUsers> AspNetUsers { get; set; }
        public virtual DbSet<CompanyInfoDtl> CompanyInfoDtl { get; set; }
        public virtual DbSet<CompanyInfoMst> CompanyInfoMst { get; set; }
        public virtual DbSet<DemoTempTable> DemoTempTable { get; set; }
        public virtual DbSet<FileOnFileSystem> FileOnFileSystem { get; set; }
        public virtual DbSet<HrApplicant> HrApplicant { get; set; }
        public virtual DbSet<HrExperience> HrExperience { get; set; }
        public virtual DbSet<LdMenu> LdMenu { get; set; }
        public virtual DbSet<LdUser> LdUser { get; set; }
        public virtual DbSet<Makers> Makers { get; set; }
        public virtual DbSet<ModelTypes> ModelTypes { get; set; }
        public virtual DbSet<CompanyOfficeAddress> CompanyOfficeAddress { get; set; }

        //        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //        {
        //            if (!optionsBuilder.IsConfigured)
        //            {
        //#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
        //                optionsBuilder.UseOracle("Data Source=(DESCRIPTION = (ADDRESS = (PROTOCOL = TCP)(HOST = Baitul)(PORT = 1521)) (CONNECT_DATA = (SERVER = DEDICATED) (SERVICE_NAME = XE) ));User ID=DU;Password=du");
        //            }
        //        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:DefaultSchema", "DU");

            modelBuilder.Entity<AspNetRoleClaims>(entity =>
            {
                entity.HasIndex(e => e.RoleId);

                entity.Property(e => e.RoleId).IsRequired();

                entity.HasOne(d => d.Role)
                    .WithMany(p => p.AspNetRoleClaims)
                    .HasForeignKey(d => d.RoleId)
                    .HasConstraintName("FK_AspNetRoleClaims_RoleId");
            });

            modelBuilder.Entity<AspNetRoles>(entity =>
            {
                entity.HasIndex(e => e.NormalizedName)
                    .HasName("RoleNameIndex")
                    .IsUnique();

                entity.Property(e => e.Name).HasMaxLength(256);

                entity.Property(e => e.NormalizedName).HasMaxLength(256);
            });

            modelBuilder.Entity<AspNetUserClaims>(entity =>
            {
                entity.HasIndex(e => e.UserId);

                entity.Property(e => e.UserId).IsRequired();

                entity.HasOne(d => d.User)
                    .WithMany(p => p.AspNetUserClaims)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("FK_AspNetUserClaims_UserId");
            });

            modelBuilder.Entity<AspNetUserLogins>(entity =>
            {
                entity.HasKey(e => new { e.LoginProvider, e.ProviderKey });

                entity.HasIndex(e => e.UserId);

                entity.Property(e => e.UserId).IsRequired();

                entity.HasOne(d => d.User)
                    .WithMany(p => p.AspNetUserLogins)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("FK_AspNetUserLogins_UserId");
            });

            modelBuilder.Entity<AspNetUserRoles>(entity =>
            {
                entity.HasKey(e => new { e.UserId, e.RoleId });

                entity.HasIndex(e => e.RoleId);

                entity.HasOne(d => d.Role)
                    .WithMany(p => p.AspNetUserRoles)
                    .HasForeignKey(d => d.RoleId)
                    .HasConstraintName("FK_AspNetUserRoles_RoleId");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.AspNetUserRoles)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("FK_AspNetUserRoles_UserId");
            });

            modelBuilder.Entity<AspNetUserTokens>(entity =>
            {
                entity.HasKey(e => new { e.UserId, e.LoginProvider, e.Name });

                entity.HasOne(d => d.User)
                    .WithMany(p => p.AspNetUserTokens)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("FK_AspNetUserTokens_UserId");
            });

            modelBuilder.Entity<AspNetUsers>(entity =>
            {
                entity.HasIndex(e => e.NormalizedEmail)
                    .HasName("EmailIndex");

                entity.HasIndex(e => e.NormalizedUserName)
                    .HasName("UserNameIndex")
                    .IsUnique();

                entity.Property(e => e.Email).HasMaxLength(256);

                entity.Property(e => e.NormalizedEmail).HasMaxLength(256);

                entity.Property(e => e.NormalizedUserName).HasMaxLength(256);

                entity.Property(e => e.UserName).HasMaxLength(256);
            });

            modelBuilder.Entity<CompanyInfoDtl>(entity =>
            {
                entity.HasKey(e => e.CidId)
                    .HasName("SYS_C0014334");

                entity.ToTable("COMPANY_INFO_DTL");

                entity.HasIndex(e => e.CidName)
                    .HasName("COMPANY_INFO_DTL_UK1")
                    .IsUnique();

                entity.Property(e => e.CidId).HasColumnName("CID_ID");

                entity.Property(e => e.CidCimId).HasColumnName("CID_CIM_ID");

                entity.Property(e => e.CidDetails)
                    .HasColumnName("CID_DETAILS")
                    .HasMaxLength(1800)
                    .IsUnicode(false);

                entity.Property(e => e.CidLogo)
                    .HasColumnName("CID_LOGO")
                    .HasColumnType("LONG RAW");

                entity.Property(e => e.CidMoto)
                    .HasColumnName("CID_MOTO")
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.Property(e => e.CidName)
                    .HasColumnName("CID_NAME")
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.Property(e => e.CidShortName)
                    .HasColumnName("CID_SHORT_NAME")
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.HasOne(d => d.CidCim)
                    .WithMany(p => p.CompanyInfoDtl)
                    .HasForeignKey(d => d.CidCimId)
                    .HasConstraintName("COMPANY_FK1");
            });

            modelBuilder.Entity<CompanyInfoMst>(entity =>
            {
                entity.HasKey(e => e.CimId)
                    .HasName("COMPANY_INFO_PK");

                entity.ToTable("COMPANY_INFO_MST");

                entity.Property(e => e.CimId)
                    .HasColumnName("CIM_ID")
                    .ValueGeneratedNever();

                entity.Property(e => e.CimDetails)
                    .HasColumnName("CIM_DETAILS")
                    .HasMaxLength(1800)
                    .IsUnicode(false);

                entity.Property(e => e.CimFileCharacter)
                    .HasColumnName("CIM_FILE_CHARACTER")
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.Property(e => e.CimFileMimetype)
                    .HasColumnName("CIM_FILE_MIMETYPE")
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.Property(e => e.CimFileName)
                    .HasColumnName("CIM_FILE_NAME")
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.Property(e => e.CimLogo)
                    .HasColumnName("CIM_LOGO")
                    .HasColumnType("LONG RAW");

                entity.Property(e => e.CimLogoApps)
                    .HasColumnName("CIM_LOGO_APPS")
                    .HasColumnType("BLOB");

                entity.Property(e => e.CimMoto)
                    .HasColumnName("CIM_MOTO")
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.Property(e => e.CimMultiAddressFlag)
                    .HasColumnName("CIM_MULTI_ADDRESS_FLAG")
                    .HasDefaultValueSql("1");

                entity.Property(e => e.CimMultiCompanyFlag)
                    .HasColumnName("CIM_MULTI_COMPANY_FLAG")
                    .HasColumnType("NUMBER(2)")
                    .HasDefaultValueSql("0");

                entity.Property(e => e.CimName)
                    .HasColumnName("CIM_NAME")
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.Property(e => e.CimShortName)
                    .HasColumnName("CIM_SHORT_NAME")
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.CimUpdateDate)
                    .HasColumnName("CIM_UPDATE_DATE")
                    .HasColumnType("DATE");
            });



            modelBuilder.Entity<CompanyOfficeAddress>(entity =>
            {
                entity.HasKey(e => e.CoaId)
                    .HasName("COMPANY_OFFICE_ADDRESS_PK");

                entity.ToTable("COMPANY_OFFICE_ADDRESS");

                //entity.HasIndex(e => e.CidName)
                //    .HasName("COMPANY_INFO_DTL_UK1")
                //    .IsUnique();

                entity.Property(e => e.CoaId).HasColumnName("COA_ID");

                entity.Property(e => e.CoaName)
                    .HasColumnName("COA_NAME")
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.CoaAddress)
                    .HasColumnName("COA_ADDRESS")
                    .HasMaxLength(1000)
                    .IsUnicode(false);

                entity.Property(e => e.CoaShortName)
                    .HasColumnName("COA_SHORT_NAME")
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.CoaShortCode)
                    .HasColumnName("COA_SHORT_CODE")
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.CoaUseTypeFlag)
                    .HasColumnName("COA_USE_TYPE_FLAG")
                    .HasMaxLength(2)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<DemoTempTable>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("DEMO_TEMP_TABLE");

                entity.Property(e => e.EmpId)
                    .HasColumnName("EMP_ID")
                    .HasColumnType("NUMBER");

                entity.Property(e => e.EmpName)
                    .HasColumnName("EMP_NAME")
                    .HasMaxLength(20)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<HrApplicant>(entity =>
            {
                entity.HasKey(e => e.ApplicantId)
                    .HasName("APPLICANT_PK");

                entity.ToTable("HR_APPLICANT");

                entity.Property(e => e.ApplicantId)
                    .HasColumnName("APPLICANT_ID")
                    .HasColumnType("NUMBER");

                entity.Property(e => e.Age)
                    .HasColumnName("AGE")
                    .HasColumnType("NUMBER");

                entity.Property(e => e.ApplicantName)
                    .HasColumnName("APPLICANT_NAME")
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.Property(e => e.Gender)
                    .HasColumnName("GENDER")
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.TotalExperiences)
                    .HasColumnName("TOTAL_EXPERIENCES")
                    .HasColumnType("NUMBER");
            });

            modelBuilder.Entity<HrExperience>(entity =>
            {
                entity.HasKey(e => e.ExperienceId)
                    .HasName("EXPERIENCE_PK");

                entity.ToTable("HR_EXPERIENCE");

                entity.Property(e => e.ExperienceId)
                    .HasColumnName("EXPERIENCE_ID")
                    .HasColumnType("NUMBER");

                entity.Property(e => e.ApplicantId)
                    .HasColumnName("APPLICANT_ID")
                    .HasColumnType("NUMBER");

                entity.Property(e => e.CompanyName)
                    .HasColumnName("COMPANY_NAME")
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.Property(e => e.Designation)
                    .HasColumnName("DESIGNATION")
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.Property(e => e.YearsWorked)
                    .HasColumnName("YEARS_WORKED")
                    .HasColumnType("NUMBER");

                entity.HasOne(d => d.Applicant)
                    .WithMany(p => p.HrExperience)
                    .HasForeignKey(d => d.ApplicantId)
                    .HasConstraintName("EXPERIENCE_FK01");
            });

            modelBuilder.Entity<LdMenu>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("LD_MENU");

                entity.Property(e => e.LdDeleted)
                    .HasColumnName("LD_DELETED")
                    .HasColumnType("NUMBER(38)");

                entity.Property(e => e.LdDescription)
                    .HasColumnName("LD_DESCRIPTION")
                    .IsUnicode(false);

                entity.Property(e => e.LdIcon)
                    .HasColumnName("LD_ICON")
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.LdId).HasColumnName("LD_ID");

                entity.Property(e => e.LdLastmodified)
                    .HasColumnName("LD_LASTMODIFIED")
                    .HasColumnType("TIMESTAMP(6)");

                entity.Property(e => e.LdParentid).HasColumnName("LD_PARENTID");

                entity.Property(e => e.LdPosition)
                    .HasColumnName("LD_POSITION")
                    .HasColumnType("NUMBER(38)");

                entity.Property(e => e.LdRecordversion).HasColumnName("LD_RECORDVERSION");

                entity.Property(e => e.LdSecurityref).HasColumnName("LD_SECURITYREF");

                entity.Property(e => e.LdTenantid).HasColumnName("LD_TENANTID");

                entity.Property(e => e.LdText)
                    .HasColumnName("LD_TEXT")
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.LdType)
                    .HasColumnName("LD_TYPE")
                    .HasColumnType("NUMBER(38)");
            });

            modelBuilder.Entity<LdUser>(entity =>
            {
                entity.HasKey(e => e.LdId)
                    .HasName("SYS_C0014016");

                entity.ToTable("LD_USER");

                entity.Property(e => e.LdId)
                    .HasColumnName("LD_ID")
                    .ValueGeneratedNever();

                entity.Property(e => e.LdCert)
                    .HasColumnName("LD_CERT")
                    .IsUnicode(false);

                entity.Property(e => e.LdCertdigest)
                    .HasColumnName("LD_CERTDIGEST")
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.LdCertsubject)
                    .HasColumnName("LD_CERTSUBJECT")
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.LdCity)
                    .HasColumnName("LD_CITY")
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.LdCountry)
                    .HasColumnName("LD_COUNTRY")
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.LdDeleted)
                    .HasColumnName("LD_DELETED")
                    .HasColumnType("NUMBER(38)");

                entity.Property(e => e.LdEmail)
                    .HasColumnName("LD_EMAIL")
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.LdEnabled)
                    .HasColumnName("LD_ENABLED")
                    .HasColumnType("NUMBER(38)");

                entity.Property(e => e.LdFirstname)
                    .HasColumnName("LD_FIRSTNAME")
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.LdIpblacklist)
                    .HasColumnName("LD_IPBLACKLIST")
                    .IsUnicode(false);

                entity.Property(e => e.LdIpwhitelist)
                    .HasColumnName("LD_IPWHITELIST")
                    .IsUnicode(false);

                entity.Property(e => e.LdKey)
                    .HasColumnName("LD_KEY")
                    .IsUnicode(false);

                entity.Property(e => e.LdKeydigest)
                    .HasColumnName("LD_KEYDIGEST")
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.LdLanguage)
                    .HasColumnName("LD_LANGUAGE")
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.LdLastmodified)
                    .HasColumnName("LD_LASTMODIFIED")
                    .HasColumnType("TIMESTAMP(6)");

                entity.Property(e => e.LdName)
                    .HasColumnName("LD_NAME")
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.LdPassword)
                    .HasColumnName("LD_PASSWORD")
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.LdPasswordchanged)
                    .HasColumnName("LD_PASSWORDCHANGED")
                    .HasColumnType("TIMESTAMP(6)");

                entity.Property(e => e.LdPasswordexpired)
                    .HasColumnName("LD_PASSWORDEXPIRED")
                    .HasColumnType("NUMBER(38)");

                entity.Property(e => e.LdPasswordexpires)
                    .HasColumnName("LD_PASSWORDEXPIRES")
                    .HasColumnType("NUMBER(38)");

                entity.Property(e => e.LdPasswordmd4)
                    .HasColumnName("LD_PASSWORDMD4")
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.LdPostalcode)
                    .HasColumnName("LD_POSTALCODE")
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.LdQuota).HasColumnName("LD_QUOTA");

                entity.Property(e => e.LdRecordversion).HasColumnName("LD_RECORDVERSION");

                entity.Property(e => e.LdSource)
                    .HasColumnName("LD_SOURCE")
                    .HasColumnType("NUMBER(38)");

                entity.Property(e => e.LdState)
                    .HasColumnName("LD_STATE")
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.LdStreet)
                    .HasColumnName("LD_STREET")
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.LdTelephone)
                    .HasColumnName("LD_TELEPHONE")
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.LdTelephone2)
                    .HasColumnName("LD_TELEPHONE2")
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.LdTenantid).HasColumnName("LD_TENANTID");

                entity.Property(e => e.LdType)
                    .HasColumnName("LD_TYPE")
                    .HasColumnType("NUMBER(38)");

                entity.Property(e => e.LdUsername)
                    .IsRequired()
                    .HasColumnName("LD_USERNAME")
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.LdWelcomescreen)
                    .HasColumnName("LD_WELCOMESCREEN")
                    .HasColumnType("NUMBER(38)");
            });

            modelBuilder.Entity<Makers>(entity =>
            {
                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(200)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<ModelTypes>(entity =>
            {
                entity.HasIndex(e => e.MakerId);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.HasOne(d => d.Maker)
                    .WithMany(p => p.ModelTypes)
                    .HasForeignKey(d => d.MakerId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ModelTypes_MakerId");
            });


            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
