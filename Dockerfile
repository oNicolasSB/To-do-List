FROM mcr.microsoft.com/dotnet/sdk:7.0
LABEL version="1.1" description="To-do-list App ASP.NET Core MVC"


RUN mkdir /app
WORKDIR /app
ENV ASPNETCORE_URLS=http://+:80
COPY to-do-list.csproj .
RUN dotnet restore
COPY . .
RUN dotnet publish -c Release -o dist
EXPOSE 80
CMD [ "dotnet", "dist/to-do-list.dll" ]