var builder = WebApplication
   .CreateBuilder(args);

builder
   .Services
   .AddControllers();

builder
   .Services
   .AddEndpointsApiExplorer();

builder
   .Services
   .AddSwaggerGen(opt =>
                  {
                      opt.EnableAnnotations();
                      opt.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, $"{Assembly.GetExecutingAssembly().GetName().Name}.xml"));

                      Assembly
                         .GetExecutingAssembly()
                         .GetReferencedAssemblies()
                         .Where(a => a.Name!.StartsWith("Tests.User.", StringComparison.InvariantCultureIgnoreCase))
                         .Select(a => Path.Combine(AppContext.BaseDirectory, $"{a.Name}.xml"))
                         .Where(File.Exists)
                         .ToList()
                         .ForEach(path => opt.IncludeXmlComments(path));
                  });

builder
   .Services
   .AddInfrastructure();

builder
   .Services
   .AddApplication();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
