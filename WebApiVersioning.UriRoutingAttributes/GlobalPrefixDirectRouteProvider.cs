using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http.Controllers;
using System.Web.Http.Routing;

namespace WebApiVersioning.UriRoutingAttributes
{
    public class GlobalPrefixDirectRouteProvider : DefaultDirectRouteProvider
    {
        private string globalPrefix = string.Empty;

        public GlobalPrefixDirectRouteProvider(string globalPrefix)
        {
            this.globalPrefix = globalPrefix;
        }

        protected override string GetRoutePrefix(HttpControllerDescriptor controllerDescriptor)
        {
            var ctlNamespace = controllerDescriptor.ControllerType.Namespace;
            string prefix = globalPrefix;
            if (ctlNamespace.StartsWith("MyNamespace"))
            {
                prefix = prefix + "/" + ctlNamespace.Substring(0, ctlNamespace.LastIndexOf('.'));
            }

            return prefix + "/" + base.GetRoutePrefix(controllerDescriptor);
        }
    }
}