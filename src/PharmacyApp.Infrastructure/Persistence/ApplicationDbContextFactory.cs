using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;

namespace PharmacyApp.Infrastructure.Persistence
{
    public class ApplicationDbContextFactory
        : IDesignTimeDbContextFactory<ApplicationDbContext>
    {
        public ApplicationDbContext CreateDbContext(string[] args)
        {
            var optionsBuilder =
                new DbContextOptionsBuilder<ApplicationDbContext>();

            optionsBuilder.UseSqlServer(
                "Server=AYA\\SQLEXPRESS;" +
                "Database=PharmacyDb;" +
                "Trusted_Connection=True;" +
                "TrustServerCertificate=True;");

            return new ApplicationDbContext(
                optionsBuilder.Options,
                mediator: null!, // 👈 Design-Time فقط
                logger: NullLogger<ApplicationDbContext>.Instance
            );
        }
    }
}
