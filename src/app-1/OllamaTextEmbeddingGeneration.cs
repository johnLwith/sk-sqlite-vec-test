using Microsoft.Extensions.DependencyInjection;
using Microsoft.SemanticKernel.Embeddings;

namespace DefaultNamespace;
using Microsoft.SemanticKernel;

#pragma warning disable SKEXP0070
#pragma warning disable SKEXP0001
public class OllamaTextEmbeddingGeneration
{
    public static async Task<ReadOnlyMemory<float>> GenerateTextEmbedding(string searchValue)
    {
        IKernelBuilder kernelBuilder = Kernel.CreateBuilder();
        kernelBuilder.AddOllamaTextEmbeddingGeneration(
            modelId: "nomic-embed-text",           // E.g. "mxbai-embed-large" if mxbai-embed-large was downloaded as described above.
            endpoint: new Uri("http://localhost:11434") // E.g. "http://localhost:11434" if Ollama has been started in docker as described above.
            //serviceId: "SERVICE_ID"             // Optional; for targeting specific services within Semantic Kernel
        );
        Kernel kernel = kernelBuilder.Build();
        var service = kernel.Services.GetRequiredService<ITextEmbeddingGenerationService>();
        return await service.GenerateEmbeddingAsync(searchValue);
    }
}