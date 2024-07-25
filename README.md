# List Reports

Este projeto é uma aplicação ASP.NET Core chamada "List Reports", que lista e permite a visualização de arquivos armazenados no Azure Blob Storage. A aplicação é configurada para ser executada em um Azure Web App e suporta vários tipos de arquivos, incluindo `.html`, `.md`, `.jpeg`, `.txt`, `.png`, e `.pdf`.

## Funcionalidades

- Lista de arquivos armazenados em containers específicos do Azure Blob Storage.
- Visualização direta de arquivos HTML no navegador.
- Interface simples e intuitiva para navegação e visualização de relatórios.

## Requisitos

- [.NET 6 SDK](https://dotnet.microsoft.com/download/dotnet/6.0)
- [Azure Storage Blob SDK](https://www.nuget.org/packages/Azure.Storage.Blobs/)
- Conta no Azure com permissões para criar recursos (Resource Group, Storage Account, Azure Web App, AKS)

## Configuração

- Contém o diretório /IaC com o arquivo main.tf (Execução TERRAFORM) Para criar os recursos no Azure (Resource Group, Storage Account, ServicePlan, Azure Web App)

