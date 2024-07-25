provider "azurerm" {
  features {}
}

resource "azurerm_resource_group" "rg" {
  name     = "RG-LISTREPORTS"
  location = "brazilsouth"
}

# Storage Account
resource "azurerm_storage_account" "stg" {
  name                     = "listreportsjuraci"
  resource_group_name      = azurerm_resource_group.rg.name
  location                 = azurerm_resource_group.rg.location
  account_tier             = "Standard"
  account_replication_type = "LRS"

  lifecycle {
    prevent_destroy = true
  }
}

# Blob Containers
resource "azurerm_storage_container" "owaspreports" {
  name                  = "owaspreports"
  storage_account_name  = azurerm_storage_account.stg.name
  container_access_type = "private"
}

resource "azurerm_storage_container" "snykreports" {
  name                  = "snykreports"
  storage_account_name  = azurerm_storage_account.stg.name
  container_access_type = "private"
}

resource "azurerm_storage_container" "sonarreports" {
  name                  = "sonarreports"
  storage_account_name  = azurerm_storage_account.stg.name
  container_access_type = "private"
}

# App Service Plan
resource "azurerm_app_service_plan" "plan" {
  name                = "list-reports-plan"
  location            = azurerm_resource_group.rg.location
  resource_group_name = azurerm_resource_group.rg.name
  kind                = "Windows"
  reserved            = false

  sku {
    tier = "Standard"
    size = "S1"
  }
}

# Web App
resource "azurerm_app_service" "webapp" {
  name                = "list-reports-juraci"
  location            = azurerm_resource_group.rg.location
  resource_group_name = azurerm_resource_group.rg.name
  app_service_plan_id = azurerm_app_service_plan.plan.id

  app_settings = {
    "WEBSITE_RUN_FROM_PACKAGE" = "1"
  }
}
