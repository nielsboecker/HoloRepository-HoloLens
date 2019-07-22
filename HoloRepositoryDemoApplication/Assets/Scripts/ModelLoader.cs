using Microsoft.MixedReality.Toolkit.Utilities;
using Microsoft.MixedReality.Toolkit.Utilities.Gltf.Serialization;
using Microsoft.MixedReality.Toolkit.UI;
using System;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Microsoft.MixedReality.Toolkit.Examples.Demos.Gltf
{

    public class ModelLoader : MonoBehaviour
    {
        [SerializeField]
        [Tooltip("This can be a local or external resource uri.")]
        private string uri = "https://holoblob.blob.core.windows.net/test/DamagedHelmet-18486331-5441-4271-8169-fcac6b7d8c29.glb";
        [SerializeField, TooltipAttribute("Enter the build index of scene that you want to load the 3D model")]
        private int SceneIndex = 0;

        public async void Start()
        {
            Response response = new Response();

            try
            {
                response = await Rest.GetAsync(uri);
            }
            catch (Exception e)
            {
                Debug.LogError(e.Message);
            }

            if (!response.Successful)
            {
                Debug.LogError($"Failed to get glb model from {uri}");
                return;
            }

            var gltfObject = GltfUtility.GetGltfObjectFromGlb(response.ResponseData);

            try
            {
                GameObject loadedObject = await gltfObject.ConstructAsync();
                Initialize(loadedObject);               
            }
            catch (Exception e)
            {
                Debug.LogError($"{e.Message}\n{e.StackTrace}");
                return;
            }

            if (gltfObject != null)
            {
                Debug.Log("Import successful");
            }
        }

        private void Initialize(GameObject gameobject)
        {
            Mesh mesh = gameobject.GetComponentsInChildren<MeshFilter>()[0].sharedMesh;
            float Max = Math.Max(Math.Max(mesh.bounds.size.x, mesh.bounds.size.y), mesh.bounds.size.z);
            float ScaleSize = 0.5f / Max;
            gameobject.transform.localScale = new Vector3(ScaleSize, ScaleSize, ScaleSize);
            gameobject.transform.position = new Vector3(mesh.bounds.center.x * ScaleSize, -mesh.bounds.center.y * ScaleSize, mesh.bounds.center.z * ScaleSize + 2);
            gameobject.transform.eulerAngles = new Vector3(0, 180, 0);
            gameobject.AddComponent<BoundingBox>();
            gameobject.AddComponent<ManipulationHandler>();
            Scene ModelDisplayScene = SceneManager.GetSceneByBuildIndex(SceneIndex);
            SceneManager.MoveGameObjectToScene(gameobject, ModelDisplayScene);
        }
    }
}