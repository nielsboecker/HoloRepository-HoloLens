using Microsoft.MixedReality.Toolkit.UI;
using UnityEngine.SceneManagement;
using UnityEngine;
using System;

namespace HoloStorageConnector
{
    public class ModelSetting : MonoBehaviour
    {
        #region Properties
        private static string ModelName = "LoadedModel";
        private static Vector3 ModelPosition = new Vector3(0f, 0f, 0f);
        private static Vector3 ModelRotation = new Vector3(0f, 0f, 0f);
        private static float ModelSize = 1f;
        private static bool Manipulable = true;
        private static string SceneName = null;

        #endregion Properties

        #region Set Method
        /// <summary>
        /// Set a name for the loaded model
        /// </summary>
        /// <param name="name"></param>
        public static void SetModelName(string name)
        {
            ModelName = name;
        }
        /// <summary>
        /// Set position for the loaded model
        /// </summary>
        /// <param name="position">Vector3 parameter, position in the scene</param>
        public static void SetPostition(Vector3 position)
        {
            ModelPosition = position;
        }
        /// <summary>
        /// Set rotation for the loaded model, the parameter should be a Vector3 object
        /// </summary>
        /// <param name="rotation">Vector3 parameter, rotation angles</param>
        public static void SetRotation(Vector3 rotation)
        {
            ModelRotation = rotation;
        }
        /// <summary>
        /// Real size in the scene, The longest side of the loaded object will be set to this value
        /// </summary>
        /// <param name="size"></param>
        public static void SetSize(float size)
        {
            ModelSize = size;
        }
        /// <summary>
        /// Determine whether the object could be manipulated
        /// </summary>
        /// <param name="manipulable"></param>
        public static void SetManipulable(bool manipulable)
        {
            Manipulable = manipulable;
        }
        /// <summary>
        /// Determine which scene you want to load the object
        /// </summary>
        /// <param name="sceneName"></param>
        public static void SetSceneName(string sceneName)
        {
            SceneName = sceneName;
        }
        #endregion Set Method

        /// <summary>
        /// Initialize transform settings of the gameobject
        /// </summary>
        /// <param name="gameobject">The loaded GameObject</param>
        public static void Initialize(GameObject gameobject)
        {
            gameobject.name = ModelName;

            Mesh mesh = gameobject.GetComponentsInChildren<MeshFilter>()[0].sharedMesh;
            float Max = Math.Max(Math.Max(mesh.bounds.size.x, mesh.bounds.size.y), mesh.bounds.size.z);
            
            float ScaleSize = ModelSize / Max;
            gameobject.transform.localScale = new Vector3(ScaleSize, ScaleSize, ScaleSize);

            Vector3 InitialPosition = new Vector3(mesh.bounds.center.x , -mesh.bounds.center.y, mesh.bounds.center.z) * ScaleSize;
            gameobject.transform.position = InitialPosition + ModelPosition;

            gameobject.transform.eulerAngles = ModelRotation;

            if (Manipulable)
            {
                gameobject.AddComponent<BoundingBox>();
                gameobject.AddComponent<ManipulationHandler>();
            }

            if (SceneName != null)
            {
                try
                {
                    Scene ModelDisplayScene = SceneManager.GetSceneByName(SceneName);
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