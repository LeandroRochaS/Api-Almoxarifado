using AlmoxarifadoInfrastructure.Data.Interfaces;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlmoxarifadoInfrastructure.Data
{
    public class DbConnectionService : IDbConnectionService
    {

        private readonly IConfiguration _configuration;

        public DbConnectionService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public string GetConnectionString()
        {
            try
            {
                using (var connection = new SqlConnection(_configuration.GetConnectionString("ConexaoHome")))
                {
                    connection.Open();
                    return _configuration.GetConnectionString("ConexaoHome");
                }
            }
            catch
            {
                return _configuration.GetConnectionString("ConexaoHomeReplica");
            }
        }
    }
}
