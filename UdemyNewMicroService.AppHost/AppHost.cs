var builder = DistributedApplication.CreateBuilder(args);

builder.AddProject<Projects.UdemyNewMicroService_Catalog_Api>("udemynewmicroservice-catalog-api");

builder.Build().Run();
