using System;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;

namespace DigiD.Common.Http.Helpers
{
    public static class HttpRequestMessageHelper
    {
        public static async Task<HttpRequestMessage> Clone(this HttpRequestMessage req)
        {
            if (req == null)
                throw new ArgumentNullException(nameof(req));

            var clone = new HttpRequestMessage(req.Method, req.RequestUri)
            {
                Content = await req.Content.CloneAsync().ConfigureAwait(false), 
                Version = req.Version
            };

            foreach (var prop in req.Properties)
                clone.Properties.Add(prop);

            foreach (var header in req.Headers)
                clone.Headers.TryAddWithoutValidation(header.Key, header.Value);

            return clone;
        }

        /// <summary>Get a copy of the request content.</summary>
        /// <param name="content">The content to copy.</param>
        /// <remarks>Note that cloning content isn't possible after it's dispatched, because the stream is automatically disposed after the request.</remarks>
        internal static async Task<HttpContent> CloneAsync(this HttpContent content)
        {
            if (content == null)
                return null;
 
            Stream stream = new MemoryStream();
            await content.CopyToAsync(stream).ConfigureAwait(false);
            stream.Position = 0;
 
            StreamContent clone = new StreamContent(stream);
            foreach (var header in content.Headers)
                clone.Headers.Add(header.Key, header.Value);
 
            return clone;
        }
    }
}
