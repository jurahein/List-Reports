package com.devsecops;

import com.azure.storage.blob.BlobContainerClient;
import com.azure.storage.blob.BlobServiceClientBuilder;
import com.azure.storage.blob.models.BlobItem;
import org.springframework.beans.factory.annotation.Value;
import org.springframework.stereotype.Service;

import java.util.ArrayList;
import java.util.List;

@Service
public class AzureBlobService {

    private final BlobContainerClient containerClient;

    public AzureBlobService(@Value("${azure.storage.connection-string}") String connectionString,
                            @Value("${azure.storage.container-name}") String containerName) {
        containerClient = new BlobServiceClientBuilder()
                .connectionString(connectionString)
                .buildClient()
                .getBlobContainerClient(containerName);
    }

    public List<String> listBlobs() {
        List<String> blobs = new ArrayList<>();
        for (BlobItem blobItem : containerClient.listBlobs()) {
            blobs.add(blobItem.getName());
        }
        return blobs;
    }
}
