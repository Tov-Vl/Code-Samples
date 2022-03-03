using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;

namespace MGTS.WFM.AgentRateImportLib.AgentRateRepository.Ef.Model
{
    public partial class AgentRateEntities : DbContext
    {
        public AgentRateEntities()
            : base("name=AgentRateEntities")
        {
        }

        public virtual DbSet<WfmStvAgent> Agents { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<WfmStvAgent>()
                .HasKey(e => e.RowId)
                .ToTable("WFM_STV_AGENT_RATE")
                .Property(e => e.RowId)
                    .HasColumnName("Row_id");

            modelBuilder.Entity<WfmStvAgent>()
                .Property(e => e.Name)
                    .IsRequired();

            modelBuilder.Entity<WfmStvAgent>()
                .Property(e => e.PeriodDate)
                    .HasColumnType("date")
                    .HasColumnName("Period_date");

            modelBuilder.Entity<WfmStvAgent>()
                .Property(e => e.UploadDate)
                    .HasColumnType("datetime")
                    .HasColumnName("Upload_date");
        }
    }
}
