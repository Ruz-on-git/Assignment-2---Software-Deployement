#!/bin/bash

set -e

RESOURCE_GROUP="EastAsia_Area"
APP_NAME="Software-Deployment-Assignment-2"
PLAN_NAME="Assignment-2-Plan"
PLAN_TIER="F1"

echo "Starting App Service Plan..."

az appservice plan update \
    --resource-group "$RESOURCE_GROUP" \
    --name "$PLAN_NAME" \
    --sku "$PLAN_TIER"

echo "Starting web application..."

az webapp start \
    --resource-group "$RESOURCE_GROUP" \
    --name "$APP_NAME"

echo "Startup complete."
echo "https://$APP_NAME.azurewebsites.net"