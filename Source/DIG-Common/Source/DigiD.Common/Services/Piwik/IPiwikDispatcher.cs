using System.Collections.Generic;
using System.Threading.Tasks;
using DigiD.Common.Models.Piwik;

namespace DigiD.Common.Services.Piwik
{
    /// <summary>
    /// Common interface to send the <see cref="PiwikEvent">events</see> to the Piwik site.  
    /// </summary>
    public interface IPiwikDispatcher
    {
        /// <summary>
        /// The base PIWIK url including piwik.php and starting with 
        /// </summary>
        /// <value>The base URL.</value>
        string BaseUrl { get; set; }

        /// <summary>
        /// Override the user agent, we moeten een App UserAgent faken
        /// </summary>
        /// <value>The user agent.</value>
        string UserAgent { get; set; }

        /// <summary>
        /// Gets or sets the size of the screen.
        /// </summary>
        /// <value>The size of the screen.</value>
        string ScreenSize { get; set; }

        /// <summary>
        /// Send a single event.
        /// </summary>
        /// <returns>True if the event is sent.</returns>
        /// <param name="singlePiwikEvent">Single event.</param>
        Task<bool> Send(PiwikEvent singlePiwikEvent);

        /// <summary>
        /// Send the specified list of events.
        /// </summary>
        /// <returns>True if the events are sent.</returns>
        /// <param name="events">Events. <see cref="PiwikEvent"/> </param>
        Task<bool> Send(IEnumerable<PiwikEvent> events);
    }
}
