namespace NhnTags.Shared.Configuration;

public class AppSettings
{
    public const string SectionName = "nhn";

    public TokenParameters TokenParameters { get; set; } = new();
    public PrincipalSettings PrincipalSettings { get; set; } = new();
    public MongoDbParameter MongoDbParameters { get; set; } = new();

    public KafkaParameter KafkaParameters { get; set; } = new();

}

public class KafkaParameter
{
    public string ConnectionString { get; set; } = string.Empty;
}

public class TokenParameters
{
    public string ServerRealm { get; set; } = string.Empty;
    public string ClientId { get; set; } = string.Empty;
    public string ClientSecret { get; set; } = string.Empty;
    public string Metadata { get; set; } = string.Empty;
}

public class PrincipalSettings
{
    public string IdClaimType { get; set; } = string.Empty;
    public string ResourceAccessClaimType { get; set; } = string.Empty;
    public string ResourceAccessContext { get; set; } = string.Empty;
    public string ResourceAccessContextRoles { get; set; } = string.Empty;
}

public class MongoDbParameter
{
    public string ConnectionString { get; set; }
    public string DatabaseName { get; set; }
}