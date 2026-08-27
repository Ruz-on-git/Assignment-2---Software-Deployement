#!/bin/bash

set -e

RESOURCE_GROUP="EastAsia_Area"
APP_NAME="Software-Deployment-Assignment-2"
PLAN_NAME="Assignment-2-Plan"

echo "Stopping web application..."

az webapp stop \
    --resource-group "$RESOURCE_GROUP" \
    --name "$APP_NAME"


az appservice plan update \
    --resource-group "$RESOURCE_GROUP" \
    --name "$PLAN_NAME" \
    --sku F1

echo "Shutdown complete."