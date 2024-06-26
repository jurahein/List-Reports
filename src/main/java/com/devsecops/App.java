package com.devsecops;
import com.azure.storage.blob.BlobContainerClient;
import com.azure.storage.blob.BlobContainerClientBuilder;
import org.springframework.beans.factory.annotation.Value;
import org.springframework.boot.SpringApplication;
import org.springframework.boot.autoconfigure.SpringBootApplication;
import org.springframework.context.annotation.Bean;

@SpringBootApplication
public class App {

    @Value("${azure.storage.connection-string}")
    private String azureStorageConnectionString;

    @Value("${azure.storage.container-name}")
    private String azureStorageContainerName;

    public static void main(String[] args) {
        SpringApplication.run(App.class, args);
    }

    @Bean
    public void listBlobs() {
        // Criar um cliente para o container Blob
        BlobContainerClient blobContainerClient = new BlobContainerClientBuilder()
                .connectionString(azureStorageConnectionString)
                .containerName(azureStorageContainerName)
                .buildClient();

        // Listar blobs dentro do container
        System.out.println("Blobs no container '" + azureStorageContainerName + "':");
        blobContainerClient.listBlobs().forEach(blobItem -> {
            System.out.println(blobItem.getName());
        });
    }
}
