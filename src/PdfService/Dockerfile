FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS base
RUN apt-get update

# Install chrome with deps, see https://github.com/jsreport/jsreport/blob/master/docker/full/Dockerfile
RUN apt-get install -y --no-install-recommends libgconf-2-4 gnupg git curl wget ca-certificates libgconf-2-4 && \
    wget -q -O - https://dl-ssl.google.com/linux/linux_signing_key.pub | apt-key add - && \
    sh -c 'echo "deb [arch=amd64] http://dl.google.com/linux/chrome/deb/ stable main" >> /etc/apt/sources.list.d/google.list' && \
    apt-get update && \  
    apt-get install -y lsb-release google-chrome-stable fonts-ipafont-gothic fonts-wqy-zenhei fonts-thai-tlwg fonts-kacst libxtst6 libxss1 --no-install-recommends

ENV chrome_launchOptions_executablePath google-chrome-stable
ENV chrome_launchOptions_args --no-sandbox,--disable-dev-shm-usage,--single-process,--no-zygote

WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /src
COPY "PdfService/PdfService.csproj" "PdfService/"
COPY "PdfService.Models/PdfService.Models.csproj" "PdfService.Models/"
RUN dotnet restore "PdfService/PdfService.csproj"
COPY "PdfService/." "PdfService/"
COPY "PdfService.Models/." "PdfService.Models/"
WORKDIR /src/PdfService

RUN dotnet build "PdfService.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "PdfService.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "PdfService.dll"]