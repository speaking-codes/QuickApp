using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ConsoleAppCaricamentoDati.Models;

namespace ConsoleAppCaricamentoDati.Builder
{
    public class CustomerBaseTemplateBuilder
    {
        private CustomerBaseTemplate _customerBaseTemplate;

        public CustomerBaseTemplateBuilder()
        {
            _customerBaseTemplate = new CustomerBaseTemplate();
        }

        public CustomerBaseTemplateBuilder SetLastName(string pathFile)
        {
            using (var reader = new StreamReader(pathFile))
                _customerBaseTemplate.LastNames = reader.ReadToEnd().Split('\n').ToList();

            for (var i = 0; i < _customerBaseTemplate.LastNames.Count; i++)
                _customerBaseTemplate.LastNames[i] = _customerBaseTemplate.LastNames[i].Replace('\r', ' ').Trim();

            return this;
        }

        public CustomerBaseTemplateBuilder SetFirstName(string pathFileMale, string pathFileFemale)
        {
            using (var reader = new StreamReader(pathFileMale))
                _customerBaseTemplate.FirstNameTemplates = reader.ReadToEnd()
                                                                 .Split('\n').Select(x =>
                                                                    new FirstNameTemplate
                                                                    {
                                                                        FirstName = x.Replace('\r', ' ').Trim(),
                                                                        IsMan = true
                                                                    }
                                                                  ).ToList();

            using (var reader = new StreamReader(pathFileFemale))
                ((List<FirstNameTemplate>)_customerBaseTemplate.FirstNameTemplates).AddRange(
                            reader.ReadToEnd()
                                    .Split('\n').Select(x =>
                                    new FirstNameTemplate
                                    {
                                        FirstName = x.Replace('\r', ' ').Trim(),
                                        IsMan = false
                                    }
                                    ).ToList());


            return this;
        }

        public CustomerBaseTemplateBuilder SetAddress(string pathFile)
        {
            using (var reader = new StreamReader(pathFile))
                _customerBaseTemplate.AddressTemplates = reader.ReadToEnd().Split('\n').ToList();

            for (var i = 0; i < _customerBaseTemplate.AddressTemplates.Count; i++)
                _customerBaseTemplate.AddressTemplates[i] = _customerBaseTemplate.AddressTemplates[i].Replace('\r', ' ').Trim();

            return this;
        }

        public CustomerBaseTemplateBuilder SetProviderMail()
        {
            _customerBaseTemplate.ProviderMailTemplates = new List<string>();
            _customerBaseTemplate.ProviderMailTemplates.Add("@QuantumMail.cosm");
            _customerBaseTemplate.ProviderMailTemplates.Add("@EternaInbox.inf");
            _customerBaseTemplate.ProviderMailTemplates.Add("@NebulaPost.space");
            _customerBaseTemplate.ProviderMailTemplates.Add("@CyberVault.tech");
            _customerBaseTemplate.ProviderMailTemplates.Add("@ZephyrMail.io");
            _customerBaseTemplate.ProviderMailTemplates.Add("@GalacticComms.gal");
            _customerBaseTemplate.ProviderMailTemplates.Add("@DreamWaveMail.dream");
            _customerBaseTemplate.ProviderMailTemplates.Add("@SynthMail.gen");
            _customerBaseTemplate.ProviderMailTemplates.Add("@PinnaclePost.sky");
            _customerBaseTemplate.ProviderMailTemplates.Add("@EpicEnclave.email");
            _customerBaseTemplate.ProviderMailTemplates.Add("@QuantumSphere.email");
            _customerBaseTemplate.ProviderMailTemplates.Add("@AstralInbox.universe");
            _customerBaseTemplate.ProviderMailTemplates.Add("@TechTesseract.mail");
            _customerBaseTemplate.ProviderMailTemplates.Add("@CosmicCourier.space");
            _customerBaseTemplate.ProviderMailTemplates.Add("@InfiniteLink.post");
            _customerBaseTemplate.ProviderMailTemplates.Add("@NebulaMailwave.star");
            _customerBaseTemplate.ProviderMailTemplates.Add("@SynthScribe.tech");
            _customerBaseTemplate.ProviderMailTemplates.Add("@GalaxyGrove.email");
            _customerBaseTemplate.ProviderMailTemplates.Add("@CelestialHub.mail");
            _customerBaseTemplate.ProviderMailTemplates.Add("@ZenithPost.comet");
            return this;
        }

        public CustomerBaseTemplateBuilder SetIncomesBase()
        {
            var random = new Random();
            _customerBaseTemplate.Incomes = new List<double>();
            double incomeBase = 15000;

            while (incomeBase < 180000)
            {
                _customerBaseTemplate.Incomes.Add(incomeBase);
                incomeBase = incomeBase + 2000 + 1000 * random.NextDouble();
            }
            return this;
        }
        public CustomerBaseTemplate Build() => _customerBaseTemplate;
    }
}
