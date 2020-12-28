# PDF Service ARM Templates

This project contains the following artifacts:

* ARM templates that can be used to deploy resources within a resource group
* PowerShell deployment script

## Overview

The ARM templates for Development, Test and Production deployments creates:

* Container Registry
* PDF Service (App Service)

In addition to environment specific templates, the project contains configuration for resources that are shared between environments (resource groups). Since Windows and Linux app service plans cannot live in the same resource group, we have to create two separate resource groups.

The ARM template for shared Linux-based resources creates:

* App Service Plan

## Deploying Templates

You can deploy these templates directly through the Azure Portal or by using the scripts supplied in the corresponding folder. Also you can configure deployment using Azure Devops Pipelines.