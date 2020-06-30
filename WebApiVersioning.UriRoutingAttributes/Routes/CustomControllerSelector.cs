using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using System.Web.Http.Controllers;
using System.Web.Http.Dispatcher;
using System.Web.Http.Routing;

namespace WebApiVersioning.UriRoutingAttributes.Routes
{
    public class CustomControllerSelector : IHttpControllerSelector
    {
        private readonly HttpConfiguration _configuration;
        private readonly Lazy<Dictionary<string, HttpControllerDescriptor>> _controllers;

        public CustomControllerSelector(HttpConfiguration configuration)
        {
            _configuration = configuration;
            _controllers = new Lazy<Dictionary<string, HttpControllerDescriptor>>(InitializeControllerDictionary);
        }

        public HttpControllerDescriptor SelectController(HttpRequestMessage request)
        {
            var routeData = request.GetRouteData();
            var subRoute = routeData.GetSubRoutes().FirstOrDefault();
            if (subRoute == null) throw new HttpResponseException(HttpStatusCode.NotFound);

            return ((HttpActionDescriptor[])subRoute.Route.DataTokens["actions"])[0].ControllerDescriptor;
        }

        public IDictionary<string, HttpControllerDescriptor> GetControllerMapping()
        {
            return _controllers.Value;
        }

        private Dictionary<string, HttpControllerDescriptor> InitializeControllerDictionary()
        {
            var dictionary = new Dictionary<string, HttpControllerDescriptor>(StringComparer.OrdinalIgnoreCase);
            var assembliesResolver = _configuration.Services.GetAssembliesResolver();
            var controllersResolver = _configuration.Services.GetHttpControllerTypeResolver();
            var controllerTypes = controllersResolver.GetControllerTypes(assembliesResolver);
            foreach (var controllerType in controllerTypes)
            {
                var segments = controllerType.Namespace.Split(Type.Delimiter);
                var controllerName = controllerType.Name.Remove(controllerType.Name.Length - DefaultHttpControllerSelector.ControllerSuffix.Length);
                var controllerKey = String.Format(CultureInfo.InvariantCulture, "{0}.{1}", segments[segments.Length - 1], controllerName);

                if (!dictionary.Keys.Contains(controllerKey))
                {
                    dictionary[controllerKey] = new HttpControllerDescriptor(_configuration,
                    controllerType.Name,
                    controllerType);
                }
            }
            return dictionary;
        }
    }
}