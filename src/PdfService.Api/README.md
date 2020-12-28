# PDF Service

This is a Docker based service that accepts web requests posting domain specific data models or pure HTML to be converted into PDF.

## Overview

The service consists of several different components:

* ASP.NET Core application with view support
* API client
* Service related models

The current implementation uses [jsreport](https://jsreport.net) as a PDF generation engine which in turn utilizes Headless Chrome to print documents.

## jsreport

The package provides middleware filter capable of transforming view output into any format `jsreport` supports. You can for example easily transform MVC view into PDF or Excel. The idea is to use views as HTML generator and `jsreport` server as transformer to the desired output.

Note that we do not have to use Razor and MVC views to render the report HTML content. The rendering can be invoked just from raw HTML as well.

Although it can be very convenient to run `jsreport` right from the .NET, we should consider pros and cons compared to running `jsreport` separately (e.g., using Docker Compose). This follows the better architectural concept and according to documentation it is usually better to run `jsreport` as a standalone service.

## Service Implementation Details

This solution is based on ASP.NET Core and supposed to be hosted on Azure within App Service.