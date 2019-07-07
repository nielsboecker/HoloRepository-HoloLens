using Microsoft.MixedReality.Toolkit.Utilities;
using Microsoft.MixedReality.Toolkit.Utilities.Gltf.Serialization;
using System;
using Microsoft.MixedReality.Toolkit.UI;
using UnityEngine;

namespace Microsoft.MixedReality.Toolkit.Examples.Demos.Gltf
{

    public class ModelLoader : MonoBehaviour
    {
        [SerializeField]
        [Tooltip("This can be a local or external resource uri.")]
        private string uri = "https://holoblob.blob.core.windows.net/test/DamagedHelmet-18486331-5441-4271-8169-fcac6b7d8c29.glb";

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
               loadedObject.transform.position = new Vector3(0.0f, 0.0f, 2.0f);
               loadedObject.transform.localScale = new Vector3(0.25F, 0.25F, 0.25F);
               loadedObject.transform.eulerAngles = new Vector3(0, 180, 0);
               loadedObject.AddComponent<BoundingBox>();
               loadedObject.AddComponent<ManipulationHandler>();
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
    }
}