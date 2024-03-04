using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.BuilderModelTemplate
{
    public class DeliveryModelTemplate
    {
        public readonly IList<string> ProviderMails;

        public DeliveryModelTemplate()
        {
            ProviderMails = new List<string>
            {
                "@QuantumMail.cosm",
                "@EternaInbox.inf",
                "@NebulaPost.space",
                "@CyberVault.tech",
                "@ZephyrMail.io",
                "@GalacticComms.gal",
                "@DreamWaveMail.dream",
                "@SynthMail.gen",
                "@PinnaclePost.sky",
                "@EpicEnclave.email",
                "@QuantumSphere.email",
                "@AstralInbox.universe",
                "@TechTesseract.mail",
                "@CosmicCourier.space",
                "@InfiniteLink.post",
                "@NebulaMailwave.star",
                "@SynthScribe.tech",
                "@GalaxyGrove.email",
                "@CelestialHub.mail",
                "@ZenithPost.comet"
            };
        }
    }
}
