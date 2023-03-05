using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace MuonRoiSocialNetwork.Infrastructure
{
    public class MuonRoiSocialNetworkFactory : IDesignTimeDbContextFactory<MuonRoiSocialNetworkDbContext>
    {
        public MuonRoiSocialNetworkDbContext CreateDbContext(string[] args)
        {
            IConfiguration configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();
            var conn = configuration.GetConnectionString("MuonRoi");
            var optionsBuilder = new DbContextOptionsBuilder<MuonRoiSocialNetworkDbContext>();
            optionsBuilder.UseSqlServer(conn);
            return new MuonRoiSocialNetworkDbContext(optionsBuilder.Options);
        }
    }
}
