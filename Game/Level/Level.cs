using System.Numerics;
using Newtonsoft.Json;

namespace Game.Level;

public class FloorData
{
    public Vector2 Position { get; set; }
    public Vector2 Dimensions { get; set; }
}

public class Level
{
    public string Name { get; }
    private readonly string _data;

    private List<FloorData> _floors = new();
    private Vector2 _spawn;

    public Level(string name, string data)
    {
        Name = name;
        _data = data;
        
        LoadData();
    }
    
    // Load json _data into data structure 
    private void LoadData()
    {
        dynamic ?levelData = JsonConvert.DeserializeObject(_data);

        if (levelData == null) return;

        int sx = levelData.spawn[0];
        int sy = levelData.spawn[1];
        
        var floorList = new List<FloorData>();

        foreach (var floorData in levelData.floors)
        {
            int x = floorData.position[0];
            int y = floorData.position[1];
            int dx = floorData.dimensions[0];
            int dy = floorData.dimensions[1];

            FloorData floor = new FloorData();
            floor.Position = new Vector2(x, y);
            floor.Dimensions = new Vector2(dx, dy);


            floorList.Add(floor);
        }
        
        // TODO: Items
        _spawn = new Vector2(sx, sy);
        _floors = floorList;
    }

    public List<FloorData> GetFloors() { return _floors;  }
    public Vector2 GetSpawn() { return _spawn; }
}