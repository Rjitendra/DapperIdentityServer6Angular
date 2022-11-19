



namespace TechBox.Model.Contexts
{
    using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore;
    using TechBox.Model.Entity;

    public class ApiContext : IdentityDbContext<ApplicationUser, ApplicationUserIdentityRole, int>
    {
        public ApiContext(string connectionString) : this(new DbContextOptionsBuilder<ApiContext>().UseSqlServer(connectionString).Options)
        {
            // This constructor is for support to use this context in LinqPad
        }

        public ApiContext(DbContextOptions<ApiContext> options) : base(options)
        {
        }
        public virtual DbSet<Student> Students { get; set; }
        public virtual DbSet<Grade> Grades { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Student>()
                .HasOne<Grade>(s => s.CurrentGrade)
                .WithMany(g => g.Students)
                .HasForeignKey(s => s.CurrentGradeId);
        }
    }
}
