# SQLite Vector Embedding Demo

This repository demonstrates how to use [sqlite-vec](https://github.com/asg017/sqlite-vec) for vector embeddings in SQLite databases.

Using Sematic Kernel & Ollama & sqlite-vec to implement vector search.

### Available Models

| Model | Dimensions | Description |
|-------|------------|-------------|
| nomic-embed-text | 768 | General purpose text embedding model |
| mxbai-embed-large | 1024 | Higher dimensional embedding model |


## Resources

- [sqlite-vec GitHub Repository](https://github.com/asg017/sqlite-vec)
- [sqlite-vec Linux x86_64 Download (v0.1.7-alpha.2)](https://github.com/asg017/sqlite-vec/releases/download/v0.1.7-alpha.2/sqlite-vec-0.1.7-alpha.2-loadable-linux-x86_64.tar.gz)
- [Microsoft Semantic Kernel Embedding Documentation](https://learn.microsoft.com/en-us/semantic-kernel/concepts/ai-services/embedding-generation/?tabs=csharp-Ollama&pivots=programming-language-csharp)

## Embedding Models

This project uses [Ollama](https://ollama.ai/) to run embedding models locally:
