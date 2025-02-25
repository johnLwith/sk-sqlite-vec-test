

using Microsoft.Data.Sqlite;
using Microsoft.SemanticKernel.Connectors.Sqlite;

namespace DefaultNamespace;

public class SqliteVectorTest
{
    public static async Task Run()
    {
        File.Delete("vectors.db");
        
        // Create and configure SQLite connection
        var connection = new SqliteConnection("Data Source=vectors.db");
        connection.Open();
        // Load the sqlite-vec extension
        connection.EnableExtensions();
        connection.LoadExtension("./vec0");
        
        // Create the vector store
        //var vectorStore = new SqliteVectorStore(connection);
        var collection = new SqliteVectorStoreRecordCollection<Document>(connection, "documents");
        await collection.CreateCollectionIfNotExistsAsync();

        // Sample articles with multiple paragraphs
        var articles = new[]
        {
            @"Vector databases are becoming increasingly important in AI applications. 
            They enable efficient similarity search and help in building more intelligent systems.
            These databases store high-dimensional vectors and can quickly find similar items.",

            @"Traditional databases store structured data in tables with rows and columns. 
            They are great for exact matches but struggle with similarity searches.
            SQL databases have been the backbone of data storage for decades.",

            @"Machine learning models often convert text, images, or other data into vectors.
            These vectors capture the semantic meaning of the data.
            Vector similarity helps find related items even if they don't match exactly.",

            @"Natural Language Processing (NLP) has evolved significantly.
            Modern NLP systems use vector embeddings to understand text.
            This enables applications like semantic search and content recommendations.",
                        @"Mike is smiling. He is working. He is a good person.",
        };

        // Store each article's paragraphs
        for (int articleId = 0; articleId < articles.Length; articleId++)
        {
            var paragraphs = articles[articleId].Split("\n", StringSplitOptions.RemoveEmptyEntries);
            for (int paraId = 0; paraId < paragraphs.Length; paraId++)
            {
                var paragraph = paragraphs[paraId].Trim();
                var document = new Document
                {
                    Id = $"article{articleId + 1}_para{paraId + 1}",
                    Title = $"Article {articleId + 1}, Paragraph {paraId + 1}",
                    Content = paragraph,
                    Embedding = (await OllamaTextEmbeddingGeneration.GenerateTextEmbedding(paragraph))
                };
                await collection.UpsertAsync(document);
            }
        }

        // Search for similar content
        var searchValue = "traditinal SQL databases";
        searchValue = "happy person";
        Console.WriteLine($"Searching for content about '{searchValue}'...\n");
        var searchEmbedding = await OllamaTextEmbeddingGeneration.GenerateTextEmbedding(searchValue);
        var searchResults = await collection.VectorizedSearchAsync(searchEmbedding);
        
        // Process results
        Console.WriteLine("Search Results (sorted by relevance):\n");
        await foreach (var result in searchResults.Results)
        {
            Console.WriteLine($"Score: {result.Score:F3}");
            Console.WriteLine($"From: {result.Record.Title}");
            Console.WriteLine($"Content: {result.Record.Content}");
            Console.WriteLine(new string('-', 80));
        }
    }
}