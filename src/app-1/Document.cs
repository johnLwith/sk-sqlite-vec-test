using Microsoft.Data.Sqlite;
using Microsoft.SemanticKernel;
using Microsoft.SemanticKernel.Connectors.Sqlite;
using Microsoft.Extensions.VectorData;

namespace DefaultNamespace;
public class Document
{
    [VectorStoreRecordKey]
    public required string Id { get; set; }

    [VectorStoreRecordData]
    public required string Title { get; set; }

    [VectorStoreRecordData]
    public required string Content { get; set; }

    [VectorStoreRecordVector(Dimensions: 768, DistanceFunction.CosineDistance)]
    public ReadOnlyMemory<float> Embedding { get; set; }
}