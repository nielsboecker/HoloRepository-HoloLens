using UnityEngine;
using HoloStorageConnector;

public class HologramLoader : MonoBehaviour
{
    public string HologramID = string.Empty;
    public string SceneName;
    public string ModelName = "LoadedModel";
    public Vector3 ModelPosition = new Vector3(0f, 0f, 0f);
    public Vector3 ModelRotation = new Vector3(0f, 0f, 0f);
    public float ModelSize = 0.5f;
    public bool Manipulable = true;

    void Start()
    {
        HologramInstantiationSettings setting = new HologramInstantiationSettings
        {
            Name = ModelName,
            Position = ModelPosition,
            Rotation = ModelRotation,
            Manipulable = true,
            Size = ModelSize
        };

        if (SceneName != string.Empty)
        {
            setting.SceneName = SceneName;
        }
        
        HoloStorageClient.LoadHologram(HologramID, setting);
    }
}
