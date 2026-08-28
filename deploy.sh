#!/bin/bash

set -e

RESOURCE_GROUP="EastAsia_Area"
APP_NAME="Software-Deployment-Assignment-2"
PLAN_NAME="Assignment-2-Plan"
LOCATION="eastasia"
PLAN_TIER="F1"


if ! az group show \
    --name "$RESOURCE_GROUP" \
    --output none 2>/dev/null; then

    echo "Resource group '$RESOURCE_GROUP' does not exist."
    echo "Creating resource group in '$LOCATION'..."

    az group create \
        --name "$RESOURCE_GROUP" \
        --location "$LOCATION"

    echo "Resource group created."
else
    echo "Resource group '$RESOURCE_GROUP' already exists."
fi

if ! az webapp show \
    --resource-group "$RESOURCE_GROUP" \
    --name "$APP_NAME" \
    --output none 2>/dev/null; then

    echo "Creating App Service Plan..."

    az appservice plan create \
        --resource-group "$RESOURCE_GROUP" \
        --name "$PLAN_NAME" \
        --location "$LOCATION" \
        --sku "$PLAN_TIER" \
        --is-linux

    echo "Creating Web App..."

    az webapp create \
        --resource-group "$RESOURCE_GROUP" \
        --plan "$PLAN_NAME" \
        --name "$APP_NAME" \
        --runtime "DOTNETCORE:8.0"
fi

echo "Configuring App Service settings..."
    az webapp config appsettings set \
        --resource-group "$RESOURCE_GROUP" \
        --name "$APP_NAME" \
        --settings \
            "ASPNETCORE_ENVIRONMENT=Production" \
            "ConnectionStrings__DefaultConnection=DataSource=app.db;Cache=Shared"

echo "Enabling diagnostics..."
    az webapp log config \
        --resource-group "$RESOURCE_GROUP" \
        --name "$APP_NAME" \
        --level information

echo "Publishing application..."
    rm -rf ./publish

    dotnet clean
    dotnet restore
    dotnet publish -c Release -o ./publish

    if [[ "$OSTYPE" == "msys" || "$OSTYPE" == "cygwin" || -n "$WINDIR" ]]; then
        pwsh -Command "Compress-Archive -Path '.\publish\*' -DestinationPath '.\app.zip' -Force"
    else
        cd publish
        zip -r ../app.zip .
        cd ..
    fi

echo "Starting web application..."
    az webapp start \
        --resource-group "$RESOURCE_GROUP" \
        --name "$APP_NAME"

echo "Deploying application..."
    az webapp deploy \
        --resource-group "$RESOURCE_GROUP" \
        --name "$APP_NAME" \
        --src-path ./app.zip \
        --type zip \
        --clean true

echo "Deployment complete."
echo "https://$APP_NAME.azurewebsites.net"
echo "Health check: https://$APP_NAME.azurewebsites.net/health"