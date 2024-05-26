using AlmoxarifadoInfrastructure.Data.Interfaces;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace AlmoxarifadoInfrastructure.Data
{
    public class DbConnectionService : IDbConnectionService
    {
        private readonly IConfiguration _configuration;
        private readonly ILogger<DbConnectionService> _logger;
        private readonly string[] _connectionStrings;
        private string _cachedConnectionString;
        private const int TimeoutInSeconds = 2; 

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
            if (!string.IsNullOrEmpty(_cachedConnectionString))
            {
                _logger.LogInformation($"Tentando conexão em cache: {_cachedConnectionString}");
                if (TryOpenConnection(_cachedConnectionString, out string validConnection))
                {
                    return validConnection;
                }
                else
                {
                    _logger.LogWarning($"Conexão em cache falhou: {_cachedConnectionString}");
                    _cachedConnectionString = null;
                }
            }

            foreach (var connStr in _connectionStrings)
            {
                _logger.LogInformation($"Tentando conectar com: {connStr}");
                if (TryOpenConnection(connStr, out string validConnection))
                {
                    _cachedConnectionString = validConnection;
                    return validConnection;
                }
            }

            _logger.LogError("Nenhuma conexão de banco de dados disponível.");
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
