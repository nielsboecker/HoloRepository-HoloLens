using Microsoft.MixedReality.Toolkit.UI;
using UnityEngine.SceneManagement;
using UnityEngine;
using System;

namespace HoloStorageConnector
{
    /// <summary>
    /// Class <c>HologramInstantiationSettings</c> is used set the transform settings before load the 3D object from server
    /// For example, the position, rotation and scale of the 3D object
    /// </summary>
    public class HologramInstantiationSettings
    {
        #region Properties
        /// <summary>
        /// Set a name for the loaded model
        /// </summary>
        public string name { get; set; } = "LoadedModel";
        /// <summary>
        /// Set position for the loaded model, the parameter should be a Vector3 value
        /// </summary>
        public Vector3 position { get; set; } = new Vector3(0f, 0f, 0f);
        /// <summary>
        /// Set rotation for the loaded model, the parameter should be a Vector3 value
        /// </summary>
        public Vector3 rotation { get; set; } = new Vector3(0f, 0f, 0f);
        /// <summary>
        /// Real size in the scene, The longest side of the loaded object will be set to this value
        /// </summary>
        public float size { get; set; } = 0.5f;
        /// <summary>
        /// Determine whether the object could be manipulated
        /// </summary>
        public bool manipulable { get; set; } = true;
        /// <summary>
        /// Determine which scene you want to load the object
        /// </summary>
        public string sceneName { get; set; } = null;
        #endregion Properties

        /// <summary>
        /// Initialize transform settings of the gameobject
        /// </summary>
        /// <param name="gameobject">The loaded GameObject</param>
        public static void Initialize(GameObject gameobject, HologramInstantiationSettings setting)
        {
            gameobject.name = setting.name;

            Mesh mesh = gameobject.GetComponentsInChildren<MeshFilter>()[0].sharedMesh;
            float max = Math.Max(Math.Max(mesh.bounds.size.x, mesh.bounds.size.y), mesh.bounds.size.z);
            
            float scaleSize = setting.size / max;
            gameobject.transform.localScale = new Vector3(scaleSize, scaleSize, scaleSize);

            Vector3 initialPosition = new Vector3(mesh.bounds.center.x , -mesh.bounds.center.y, mesh.bounds.center.z) * scaleSize;
            gameobject.transform.position = initialPosition + setting.position;

            gameobject.transform.eulerAngles = setting.rotation;

            if (setting.manipulable)
            {
                gameobject.AddComponent<BoundingBox>();
                gameobject.AddComponent<ManipulationHandler>();
            }

            if (setting.sceneName != null)
            {
                try
                {
                    Scene HologramDisplayScene = SceneManager.GetSceneByName(setting.sceneName);
                    SceneManager.MoveGameObjectToScene(gameobject, HologramDisplayScene);
                }
                catch (Exception e)
                {
                    Debug.LogError("Failed to move the object to specific scene! \n[Error message]: " + e.Message);
                }              
            }
        }
    }
}