## Turkcell e-Şirket .NET Client Örnekleri

e-Belge ve e-Bilet uygulamaları için .Net Client kullanım örnekleri.

Kütüphaneler NET Standard 2.0 ile geliştirilmiştir. Uyumlu versiyonlar için [Net Standard Versiyon uyumluluk tablosuna](https://docs.microsoft.com/tr-tr/dotnet/standard/net-standard?tabs=net-standard-2-0#select-net-standard-version) gözatabilirsiniz.

## Başlarken
Servisleri nuget paketleri ile kullanmak için [nuget adresimizi](https://nuget.org/eplatform) takip edebilir, teknik dokümantasyon için  [Turkcell e-Şirket Teknik Dokümanlar](https://developer.turkcellesirket.com/) adresimizi ziyaret edebilirsiniz. Entegrasyon süreçlerinizde [entegrasyon@eplatform.com.tr](entegrasyon@eplatform.com.tr) üzerinden iletişime geçebilirsiniz.

## Kullanım

### Console Uygulaması/Minimal API Örneği

Console uygulamanızda `main` metodu içine aşağıdaki satırları ekledikten sonra, interfaceler üzerinden servis metodlarına erişebilirsiniz.

```
ServiceProvider serviceProvider = new ServiceCollection()
	.AddePlatformClients(clientOptions =>
	{
		clientOptions.InvoiceServiceUrl = "https://efaturaservicetest.isim360.com";
		clientOptions.TicketServiceUrl = "https://ebiletservicetest.isim360.com";
		clientOptions.ApiKey = "";
	})
	.BuildServiceProvider();

	var outboxInvoiceClient = serviceProvider.GetService<IOutboxInvoiceClient>();
	var inboxInvoiceClient = serviceProvider.GetService<IInboxInvoiceClient>();
	var commonClient = serviceProvider.GetService<ICommonClient>();
	var earchiveClient = serviceProvider.GetService<IEArchiveInvoiceClient>();
	var eventTicketClient = serviceProvider.GetService<IEventTicketClient>();
	var passengerTicketClient = serviceProvider.GetService<IPassengerTicketClient>();
}
```

### .NET Core Örneği

.NET core uygulamanızda `Startup` dosyanızı aşağıdaki gibi yapılandırın.

```
public Startup(IConfiguration configuration)
{
    Configuration = configuration;
}

public IConfiguration Configuration { get; }

public void ConfigureServices(IServiceCollection services)
{
    services.AddePlatformClients(Configuration);
    services.AddControllers();
}
```

Controller injection
```
private readonly IOutboxInvoiceClient _outboxInvoiceClient;

public OutboxInvoiceController(IOutboxInvoiceClient outboxInvoiceClient)
{
    _outboxInvoiceClient = outboxInvoiceClient;
}

[HttpGet("pdf/{id}")]
public async Task<ActionResult> GetPdf(Guid id)
{
    var data = await _outboxInvoiceClient.GetPdf(id);
    return File(data, "application/pdf", $"{id}.zip");
}
```

### .NET Framework Örneği
```
protected void Application_Start()
{
    var services = new ServiceCollection();

    //Use this method to add invoice clients
    services.AddePlatformClients(clientOptions =>
    {
        clientOptions.InvoiceServiceUrl = "https://efaturaservicetest.isim360.com";
        clientOptions.ApiKey = "";
    });

    var providerFactory = new AutofacServiceProviderFactory();
    //to populate your container
    ContainerBuilder builder = providerFactory.CreateBuilder(services);

    // MVC - Register your MVC controllers.
    builder.RegisterControllers(typeof(WebApiApplication).Assembly);

    // MVC - OPTIONAL: Register model binders that require DI.
    builder.RegisterModelBinders(typeof(WebApiApplication).Assembly);
    builder.RegisterModelBinderProvider();

    // MVC - OPTIONAL: Register web abstractions like HttpContextBase.
    builder.RegisterModule<AutofacWebTypesModule>();

    // MVC - OPTIONAL: Enable property injection in view pages.
    builder.RegisterSource(new ViewRegistrationSource());

    // MVC - OPTIONAL: Enable property injection into action filters.
    builder.RegisterFilterProvider();

    // MVC - Set Autofac as dependency resolver.
    var container = builder.Build();
    DependencyResolver.SetResolver(new AutofacDependencyResolver(container));

    // Standard MVC setup:

    AreaRegistration.RegisterAllAreas();
    GlobalConfiguration.Configure(WebApiConfig.Register);
    FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
    RouteConfig.RegisterRoutes(RouteTable.Routes);
    BundleConfig.RegisterBundles(BundleTable.Bundles);
}

```

Controller injection
```
private readonly IOutboxInvoiceClient _outboxInvoiceClient;

public OutboxInvoiceController(IOutboxInvoiceClient outboxInvoiceClient)
{
    _outboxInvoiceClient = outboxInvoiceClient;
}

[HttpGet("pdf/{id}")]
public async Task<ActionResult> GetPdf(Guid id)
{
    var data = await _outboxInvoiceClient.GetPdf(id);
    return File(data, "application/pdf", $"{id}.zip");
}
```