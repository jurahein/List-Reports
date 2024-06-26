package com.devsecops;

import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.stereotype.Controller;
import org.springframework.ui.Model;
import org.springframework.web.bind.annotation.GetMapping;

@Controller
public class BlobController {

    @Autowired
    private AzureBlobService blobService;

    @GetMapping("/")
    public String listBlobs(Model model) {
        model.addAttribute("blobs", blobService.listBlobs());
        return "index";
    }
}