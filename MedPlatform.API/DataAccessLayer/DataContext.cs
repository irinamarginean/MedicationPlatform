using BusinessObjectLayer.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace DataAccessLayer
{
    public class DataContext : IdentityDbContext<IdentityUser> 
    {
        public override DbSet<IdentityUser> Users { get; set; }
        public DbSet<MedicationPlan> MedicationPlans { get; set; }
        public DbSet<Medication> Medications { get; set; }
        public DbSet<MedicationMedicationPlan> MedicationMedicationPlans { get; set; }
        public DbSet<ActivityEntity> Activities { get; set; }

        public DataContext() : base() { }

        public DataContext(DbContextOptions<DataContext> options) : base(options) { }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql(
                "User ID=postgres;Password=irina1234;Host=localhost;Port=5432;Database=medication_platform;Pooling=true;TrustServerCertificate=True;");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<DoctorEntity>();
            modelBuilder.Entity<PatientEntity>();
            modelBuilder.Entity<CaregiverEntity>();

            modelBuilder.Entity<CaregiverEntity>()
                .HasMany(c => c.Patients)
                .WithOne(p => p.Caregiver)
                .HasForeignKey(p => p.CaregiverId);

            modelBuilder.Entity<MedicationMedicationPlan>()
                .HasKey(mmp => new {mmp.MedicationId, mmp.MedicationPlanId});
            modelBuilder.Entity<MedicationMedicationPlan>()
                .HasOne(cp => cp.Medication)
                .WithMany(c => c.MedicationMedicationPlans)
                .HasForeignKey(cp => cp.MedicationId)
                .OnDelete(DeleteBehavior.Cascade);
            modelBuilder.Entity<MedicationMedicationPlan>()
                .HasOne(cp => cp.MedicationPlan)
                .WithMany(p => p.MedicationMedicationPlans)
                .HasForeignKey(cp => cp.MedicationPlanId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<PatientEntity>()
                .HasMany(p => p.MedicationPlans)
                .WithOne(mp => mp.Patient)
                .HasForeignKey(mp => mp.PatientId);

              modelBuilder.Entity<ActivityEntity>()
                .HasOne(a => a.Patient)
                .WithMany(p => p.Activities)
                .HasForeignKey(a => a.PatientId);
        }
    }
}
