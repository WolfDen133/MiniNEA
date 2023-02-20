using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Schema;

namespace Game.Level;

public class LevelManager
{
    public Dictionary<string, Level?> Levels = new Dictionary<string, Level?>();

    public LevelManager()
    {
        LoadLevels();
    }

    private void LoadLevels()
    {
        string levelDbRaw = File.ReadAllText(Directory.GetCurrentDirectory() + "/Levels/levels.json");
        JObject levelDb = JObject.Parse(levelDbRaw);

        foreach (var i in levelDb)
        {
            string name = i.Key;
            string file = i.Value.ToString();
            
            RegisterLevel(name, file);
        }
    }

    private void RegisterLevel(string name, string file)
    {
        string rawSchema = File.ReadAllText(Directory.GetCurrentDirectory() + "/Levels/levelSchema.json");
        string levelRaw = File.ReadAllText(Directory.GetCurrentDirectory() + "/Levels/" + file);
        JSchema schema = JSchema.Parse(rawSchema);

        JObject levelObj = JObject.Parse(levelRaw);

        if (!levelObj.IsValid(schema)) return;

        Levels.Add(name, new Level(name, levelRaw));
    }
}