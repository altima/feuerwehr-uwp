using Microsoft.Practices.ObjectBuilder2;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Windows.Networking.ServiceDiscovery.Dnssd;
using Windows.Networking.Sockets;

namespace Display.Services
{
    /// <summary>
    /// 
    /// </summary>
    public interface IDnssdService
    {
        Task<DnssdRegistrationResult> Announce(string service, int port, Dictionary<string, string> text);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <seealso cref="Display.Services.IDnssdService" />
    public class DnssdService : IDnssdService
    {
        /// <summary>
        /// Announces the specified service.
        /// </summary>
        /// <param name="service">The service.</param>
        /// <param name="port">The port.</param>
        /// <param name="text">The text.</param>
        /// <returns></returns>
        public async Task<DnssdRegistrationResult> Announce(string service, int port, Dictionary<string, string> text)
        {
            var listener = new StreamSocketListener();
            await listener.BindServiceNameAsync("");

            var instance = new DnssdServiceInstance(service, null, Convert.ToUInt16(port));

            if (text != null)
            {
                text.ForEach(p=> instance.TextAttributes.Add(p.Key, p.Value));
            }

            return await instance.RegisterStreamSocketListenerAsync(listener);
        }
    }
}
