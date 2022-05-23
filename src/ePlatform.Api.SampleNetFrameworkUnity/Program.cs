using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ePlatform.Api;
using Flurl.Http;
using Flurl.Http.Configuration;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Unity;

namespace ePlatform.Api.SampleNetFrameworkUnity
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            var container = new UnityContainer();

            container.AddePlatformClients(new ClientOptions()
            {
                InvoiceServiceUrl = "https://efaturaservicetest.isim360.com",
                TicketServiceUrl = "https://ebiletservicetest.isim360.com",
                ApiKey = ""
            });

            var service = container.Resolve<IInboxInvoiceClient>();
            var result = await service.GetStatus(Guid.Empty);
        }
    }

    internal static class UnityContainerExtension
    {
        public static IUnityContainer AddePlatformClients(this IUnityContainer services,
            ClientOptions clientOptions)
        {
            return services
                .RegisterInstance(Options.Create(clientOptions))
                .AddFlurl()
                .AddClients();
        }

        private static IUnityContainer AddClients(this IUnityContainer container)
        {
            return container.RegisterType<IOutboxInvoiceClient, OutboxInvoiceClient>()
                .RegisterType<IInboxInvoiceClient, InboxInvoiceClient>()
                .RegisterType<ICommonClient, CommonClient>()
                .RegisterType<IEArchiveInvoiceClient, EArchiveInvoiceClient>()
                .RegisterType<ICommonTicketClient, CommonTicketClient>()
                .RegisterType<IEventTicketClient, EventTicketClient>()
                .RegisterType<IPassengerTicketClient, PassengerTicketClient>();
        }

        public static IUnityContainer AddFlurl(this IUnityContainer services)
        {
            var clientOptions = services.Resolve<IOptions<ClientOptions>>();
            FlurlHttp.Configure(settings =>
            {
                settings.BeforeCall = (call) =>
                {
                    call.Request.Headers.Add("X-Api-Key", clientOptions.Value.ApiKey);
                    call.Request.Headers.Add("Client-Info", clientOptions.Value.ApiKey.ToMaskedString());
                };

                settings.OnErrorAsync = async httpCall =>
                {
                    if (httpCall.Response == null)
                        throw new ePlatformException("Check internet connection");

                    httpCall.ExceptionHandled = true;
                    string correlationId = string.Empty;
                    if (httpCall.Response.Headers.Contains("X-Correlation-Id"))
                    {
                        correlationId = httpCall.Response.GetHeaderValue("X-Correlation-Id");
                    }

                    if ((int)httpCall.HttpStatus == 422)
                    {
                        var result = JsonConvert.DeserializeObject<Dictionary<string, IEnumerable<string>>>(await httpCall.Response.Content.ReadAsStringAsync());
                        if (result.Any())
                        {
                            throw new EntityValidationException(result, result.FirstOrDefault().Value.FirstOrDefault(), correlationId, httpCall.Exception);
                        }
                    }
                    else if (httpCall.HttpStatus == System.Net.HttpStatusCode.NotFound)
                    {
                        throw new EntityNotFoundException(await httpCall.Response.Content.ReadAsStringAsync(), correlationId, httpCall.Exception);
                    }
                    else if (httpCall.HttpStatus == System.Net.HttpStatusCode.Forbidden)
                    {
                        throw new ForbiddenExcepitons(await httpCall.Response.Content.ReadAsStringAsync(), httpCall.Exception);
                    }
                    else if (httpCall.HttpStatus == System.Net.HttpStatusCode.Unauthorized)
                    {
                        throw new UnauthorizedExcepiton(await httpCall.Response.Content.ReadAsStringAsync(), httpCall.Exception);
                    }
                    else
                    {
                        throw new ePlatformException(await httpCall.Response.Content.ReadAsStringAsync(), correlationId, httpCall.Exception);
                    }
                };
            });

            return services.RegisterInstance<IFlurlClientFactory>(new PerBaseUrlFlurlClientFactory());
        }
    }
}
