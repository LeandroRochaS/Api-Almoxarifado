using AlmoxarifadoInfrastructure.Data.Interfaces;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;

namespace AlmoxarifadoInfrastructure.Data
{
    public class DbConnectionService : IDbConnectionService
    {
        private readonly IConfiguration _configuration;
        private readonly ILogger<DbConnectionService> _logger;
        private readonly string[] _connectionStrings;
        private string _cachedConnectionString;
        private const int TimeoutInSeconds = 2; // Timeout reduzido

        public DbConnectionService(IConfiguration configuration, ILogger<DbConnectionService> logger)
        {
            _configuration = configuration;
            _logger = logger;
            _connectionStrings = new[]
            {
                _configuration.GetConnectionString("ConexaoHome"),
                _configuration.GetConnectionString("ConexaoHomeReplica"),
            };
        }

        public string GetConnectionString()
        {
            // Primeiro tenta usar a conexão em cache
            if (!string.IsNullOrEmpty(_cachedConnectionString))
            {
                if (TryOpenConnection(_cachedConnectionString, out string validConnection))
                {
                    return validConnection;
                }
                else
                {
                    _cachedConnectionString = null;
                }
            }

            foreach (var connStr in _connectionStrings)
            {
                if (TryOpenConnection(connStr, out string validConnection))
                {
                    _cachedConnectionString = validConnection;
                    return validConnection;
                }
            }
            throw new Exception("Nenhuma conexão de banco de dados disponível.");
        }

        private bool TryOpenConnection(string connectionString, out string validConnection)
        {
            try
            {
                using (var connection = new SqlConnection(connectionString))
                {
                    connection.ConnectionString += $";Connection Timeout={TimeoutInSeconds};";
                    connection.Open();
                    _logger.LogInformation($"Conexão bem-sucedida com: {connectionString}");
                    validConnection = connectionString;
                    return true;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Falha ao conectar com: {connectionString}, erro: {ex.Message}");
                validConnection = null;
                return false;
            }
        }
    }
}
