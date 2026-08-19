namespace Zeiss.Products.Infrastructure.Logging;

internal class ElasticsearchSettings
{
    public const string SectionName = "Elasticsearch";
    public required string Uri { get; set; }
    public required string IndexFormat { get; set; }
}