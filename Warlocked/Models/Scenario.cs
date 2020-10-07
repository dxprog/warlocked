using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using System.Text.Json.Serialization;

using WarnerEngine.Services;

namespace Warlocked.Models
{
    public class Scenario
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public List<Objective> Objectives { get; set; }
        public Map Map { get; set; }

        public static void RegisterAssetLoader()
        {
            GameService.GetService<IContentService>().RegisterAssetLoader<Scenario>(LoadFromFile, "Scenario");
        }

        private static (string, object)[] LoadFromFile(string _, string scenarioName)
        {
            string scenarioJson = File.ReadAllText("Content\\" + scenarioName + ".json");
            Scenario scenarioObj = JsonSerializer.Deserialize<Scenario>(scenarioJson);
            return new (string, object)[] { (scenarioName, scenarioObj) };
        }
    }
}
