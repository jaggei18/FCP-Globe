using Microsoft.EntityFrameworkCore;
using FirstCommand.API.Services.Models;
using FirstCommand.API.Database.Entities;
using static FirstCommand.API.Database.Entities.GovAllotmentResponse;

namespace FirstCommand.API.Database
{
    public class FirstCommandDBContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (optionsBuilder.IsConfigured)
            {
                return;
            }

            //Uncomment below commneted secret manager line after configuring with secratemanager
            //var secretsManagerService = new SecretsManagerService();
            //var dbUser = secretsManagerService.GetSecretAsync();

            //MySQl Connectionstring
            //var connectionString = $"server=database-2-instance-1.chghx7yyux8l.us-east-1.rds.amazonaws.com;user=admin;database=dbo;port=3306;password=DBAAdmin1234;SslMode=None";

            //Azure SQL Server Connectionstring
            //comment below lines after getting those value from secret manager
            DbUserModel dbUserModel = new DbUserModel();
            dbUserModel.Username = "maaf-sqladmin"; //fcadmin
            dbUserModel.Password = "DBA$1234";
            dbUserModel.Database = "firstcommand";//"FirstCommand"

            //var connectionString = $"server=globelifefc.database.windows.net;database='{dbUserModel.Database}';user='{dbUserModel.Username}';password='{dbUserModel.Password}';";
            var connectionString = $"server=maaf-sqlserver.database.windows.net;database='{dbUserModel.Database}';user='{dbUserModel.Username}';password='{dbUserModel.Password}';";

            optionsBuilder.UseSqlServer(connectionString);
            base.OnConfiguring(optionsBuilder);
        }
        public virtual DbSet<AuthAgentResponse> AuthAgentResponse { get; set; }        
        public virtual DbSet<BankDraftStype1Response> BankDraftStype1Response { get; set; }
        public virtual DbSet<BankDraftStype2Response> BankDraftStype2Response { get; set; }
        public virtual DbSet<BankDraftStype3Response> BankDraftStype3Response { get; set; }
        public virtual DbSet<CheckAgentResponse> CheckAgentResponse { get; set; }
        public virtual DbSet<InqVerifyResponse> InqVerifyResponse { get; set; }
        public virtual DbSet<PendVerifyResponse> PendVerifyResponse { get; set; }
        public virtual DbSet<AgentNumberXREFResponse> AgentNumberXREFResponse { get; set; }
        public virtual DbSet<PolicyInformationBillingResponse> PolicyInformationBillingResponse { get; set; }
        public virtual DbSet<PolicyInformationBilling_BeneficiaryResponse> PolicyInformationBilling_BeneficiaryResponse { get; set; }
        public virtual DbSet<AgentInfoResponse> AgentInfoResponse { get; set; }
        public virtual DbSet<PendPolicyInfoResponse> PendPolicyInfoResponse { get; set; }
        public virtual DbSet<CoverageResponse> CoverageResponse { get; set; }
        public virtual DbSet<PendingNotesResponse> PendingNotesResponse { get; set; }
        public virtual DbSet<GovAllotmentStype3Response> GovAllotmentStype3Response { get; set; }
        public virtual DbSet<GovAllotmentStype4Response> GovAllotmentStype4Response { get; set; }
        public virtual DbSet<GovAllotmentStype5Response> GovAllotmentStype5Response { get; set; }
        public virtual DbSet<GovAllotmentStype6Response> GovAllotmentStype6Response { get; set; }
    }
}
