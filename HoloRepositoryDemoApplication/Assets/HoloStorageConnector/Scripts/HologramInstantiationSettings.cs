using Microsoft.MixedReality.Toolkit.UI;
using UnityEngine.SceneManagement;
using UnityEngine;
using System;

namespace HoloStorageConnector
{
    public class HologramInstantiationSettings
    {
        #region Properties
        public string Name { get; set; } = "LoadedModel";
        public Vector3 Position { get; set; } = new Vector3(0f, 0f, 0f);
        public Vector3 Rotation { get; set; } = new Vector3(0f, 0f, 0f);
        public float Size { get; set; } = 1f;
        public bool Manipulable { get; set; } = true;
        public string SceneName { get; set; } = null;
        #endregion Properties

        /// <summary>
        /// Initialize transform settings of the gameobject
        /// </summary>
        /// <param name="gameobject">The loaded GameObject</param>
        public static void Initialize(GameObject gameobject, HologramInstantiationSettings setting)
        {
            gameobject.name = setting.Name;

            Mesh mesh = gameobject.GetComponentsInChildren<MeshFilter>()[0].sharedMesh;
            float Max = Math.Max(Math.Max(mesh.bounds.size.x, mesh.bounds.size.y), mesh.bounds.size.z);
            
            float ScaleSize = setting.Size / Max;
            gameobject.transform.localScale = new Vector3(ScaleSize, ScaleSize, ScaleSize);

            Vector3 InitialPosition = new Vector3(mesh.bounds.center.x , -mesh.bounds.center.y, mesh.bounds.center.z) * ScaleSize;
            gameobject.transform.position = InitialPosition + setting.Position;

            gameobject.transform.eulerAngles = setting.Rotation;

            if (setting.Manipulable)
            {
                gameobject.AddComponent<BoundingBox>();
                gameobject.AddComponent<ManipulationHandler>();
            }

            if (setting.SceneName != null)
            {
                try
                {
                    Scene ModelDisplayScene = SceneManager.GetSceneByName(setting.SceneName);
                    SceneManager.MoveGameObjectToScene(gameobject, ModelDisplayScene);
                }
                catch (Exception e)
                {
                    Debug.LogError("Failed to move the object to specific scene! \n[Error message]: " + e.Message);
                }              
            }
        }
    }
}