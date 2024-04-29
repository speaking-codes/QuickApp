using DAL.BuilderModel.Interfaces;
using DAL.BuilderModelTemplate;
using DAL.Helpers;
using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Exstensions
{
    public static class PetExstension
    {
        public static Pet SetPetIdentificationCode(this Pet pet, Random random)
        {
            pet.PetIdentificationCode = Utilities.GeneratePetIdentificationCode(random);
            return pet;
        }
        public static Pet SetPetName(this Pet pet, Random random,IList<string> petNames)
        {
            var i = random.Next(petNames.Count);
            pet.PetName = petNames[i];
            return pet;
        }
        public static Pet SetPetBirthDate(this Pet pet, Random random)
        {
            var age = random.Next(0, 3);
            var days = random.Next(1, 365);
            pet.PetBirthDate = DateTime.Now.AddYears(-age).AddDays(-days);
            return pet;
        }
        public static Pet SetBreedPetDetailType(this Pet pet, Random random, IList<BreedPetDetailType> breedPetDetailTypes)
        {
            var i = random.Next(breedPetDetailTypes.Count);
            pet.BreedPetDetailType = breedPetDetailTypes[i];
            return pet;
        }
        public static string GetPetDescription(this Pet pet)
        {
            var sb = new StringBuilder(string.Empty);
            sb.Append($"Codice identificativo: {pet.PetIdentificationCode}");
            sb.Append($" - Nome: {pet.PetName}");
            sb.Append($" - Data di nascita: {pet.PetBirthDate.ToString("dd/MM/yyyy")}");
            sb.Append($" - Tipo Animale: {pet.BreedPetDetailType.BreedPetType.PetType.PetTypeDescription}");
            sb.Append($" - Razza: {pet.BreedPetDetailType.BreedPetType.BreedPetTypeDescription}");
            sb.Append($" - Dettaglio razza: {pet.BreedPetDetailType.BreedPetDetailTypeDescription}");
            return sb.ToString();
        }
        public static IList<string> GetPetDescriptions(this IEnumerable<Pet> pets)
        {
            var petDescriptions = new List<string>();
            foreach (var item in pets)
                petDescriptions.Add(item.GetPetDescription());
            return petDescriptions;
        }
    }
}
