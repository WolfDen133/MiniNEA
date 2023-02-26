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

    // Read all level data files
    private void LoadLevels()
    {
        string levelDbRaw = File.ReadAllText(Directory.GetCurrentDirectory() + "/Levels/levels.json");
        JObject levelDb = JObject.Parse(levelDbRaw);

        // Iterate through every entry in the level database file
        foreach (var i in levelDb)
        {
            string name = i.Key;
            string file = i.Value.ToString();
            
            RegisterLevel(name, file);
        }
    }

    // Validates and registers to database
    private void RegisterLevel(string name, string file)
    {
        string rawSchema = File.ReadAllText(Directory.GetCurrentDirectory() + "/Levels/levelSchema.json");
        string levelRaw = File.ReadAllText(Directory.GetCurrentDirectory() + "/Levels/" + file);
        JSchema schema = JSchema.Parse(rawSchema);

        JObject levelObj = JObject.Parse(levelRaw);

        if (!levelObj.IsValid(schema)) return;

        Levels.Add(name, new Level(name, levelRaw));
    }
    
    public void SelectLevel(string id)
    {
        Loader.Game.LevelManager.Levels.TryGetValue(id, out Level? level);

        Loader.Game.Level = level;
        
        Loader.Game.LoadLevel();
        Loader.Game.IsRunning = true;
    }
}