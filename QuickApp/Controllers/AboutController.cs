using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Models.ViewModels;
using System.Collections.Generic;

namespace QuickApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AboutController : ControllerBase
    {
        [HttpGet]
        public IActionResult Get()
        {
            return Ok(new AboutViewModel
            {
                Name = "DAT - Assicurazioni",
                Logo = "",
                LogoDescription = "DAT - Assicurazioni",
                Address = "Via Roma, 123",
                City = "Milano - (MI)",
                Country = "Italia - IT",
                PostalCode = "20100",
                Email = "info@datassicurazioni.it",
                PhoneNumber = "+39 02 1234567",
                FaxNumber = "+39 02 7654321",
                Description1 = "DAT - Assicurazioni è un'azienda italiana leader nel settore assicurativo, con una sede principale situata in una moderna struttura all'indirizzo Via Roma, 123, nella città di Milano, provincia di Milano, CAP 20100.",
                Description2 = "La nostra compagnia si distingue per l'eccellenza dei servizi offerti e l'approccio innovativo nel fornire soluzioni assicurative su misura per soddisfare le esigenze di individui, famiglie e imprese. Con un'esperienza pluriennale nel settore, ci impegniamo a garantire la massima protezione e tranquillità ai nostri clienti.",
                Description3 = "Il nostro portafoglio comprende una vasta gamma di prodotti assicurativi, tra cui polizze vita, assicurazioni auto e moto, coperture sanitarie, assicurazioni casa, responsabilità civile, e soluzioni per aziende e professionisti. Mettiamo a disposizione dei nostri assicurati piani personalizzati, progettati con cura per adattarsi alle loro esigenze specifiche, garantendo una copertura completa e affidabile.",
                Description4 = "Presso DAT - Assicurazioni, la soddisfazione del cliente è al centro di tutto ciò che facciamo. Il nostro team altamente qualificato e dedicato è pronto a offrire consulenza esperta e supporto costante, guidando i nostri clienti nella scelta delle soluzioni più adatte e assistendoli in ogni fase, dalla sottoscrizione del contratto alla gestione delle richieste di risarcimento.",
                Description5 = "Il nostro impegno verso la qualità, la trasparenza e l'etica aziendale ci guida ogni giorno nel perseguire l'eccellenza nel settore assicurativo. Con un capitale sociale di €10.000.000,00, investiamo costantemente in tecnologie all'avanguardia e formazione continua per garantire un servizio all'altezza delle aspettative dei nostri clienti.",
                Description6 = "In sintesi, DAT - Assicurazioni si pone come un punto di riferimento affidabile e competente nel mondo assicurativo, offrendo protezione, sicurezza e tranquillità a chiunque cerchi soluzioni assicurative personalizzate e di qualità superiore.",
                SocialCapital = "€ 10.000.000,00"
            });
        }
    }
}
