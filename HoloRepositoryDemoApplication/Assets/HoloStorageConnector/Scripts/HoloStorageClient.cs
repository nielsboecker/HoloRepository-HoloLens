using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Networking;
using Microsoft.MixedReality.Toolkit.Utilities;
using Microsoft.MixedReality.Toolkit.Utilities.Gltf.Serialization;
using System.Collections.Generic;
using System.Reflection;
using SimpleJSON;

namespace HoloStorageConnector
{
    /// <summary>
    /// Class <c>HoloStorageClient</c> provided multiple methods to retrieve data from Storage server.
    /// </summary>
    public class HoloStorageClient : MonoBehaviour
    {
        #region Properties
        private static string StorageAccessorEndpoint = "http://localhost";
        private static string Port = "8080";
        private static string apiVersion = "1.0.0";
        private static string BaseUri = $"{StorageAccessorEndpoint}:{Port}/api/{apiVersion}";        
        private static string WebRequestReturnData = null;
        #endregion Properties

        #region Public Method
        /// <summary>
        /// Set end point Uri
        /// </summary>
        public static void SetEndPoint(string endPoint)
        {
            StorageAccessorEndpoint = endPoint;
        }
        public static void SetPort(string port)
        {
            Port = port;
        }
        public static void SetAPIVersion(string version)
        {
            apiVersion = version;
        }

        /// <summary>
        /// Method <c>GetMultiplePatients</c> is used to retrieve multiple patient meta data from Storage server 
        /// Result will be store in the parameter list 
        /// </summary>
        /// <param name="patientList">Patient object list, used to store information</param>
        /// <param name="IDs">IDs of querying patients</param>
        /// <returns></returns>
        public static IEnumerator GetMultiplePatients(List<Patient> patientList, string IDs)
        {
            string MultiplePatientUri = $"{BaseUri}/patients?pid={IDs}";
            yield return GetRequest(MultiplePatientUri);

            patientList.Clear();
            string[] ids = IDs.Split(',');
            if (WebRequestReturnData != null)
            {
                JSONNode InitialJsonData = JSON.Parse(WebRequestReturnData);
                foreach (string id in ids)
                {
                    JSONNode data = InitialJsonData[id];
                    Patient patient = JsonToPatient(data, id);
                    if (patient.pid != null)
                    {
                        patientList.Add(patient);
                    }
                }
            }            
        }

        /// <summary>
        /// Method <c>GetPatient</c> allows user retrieve single patient meta data from Storage server by patient ID. 
        /// </summary>
        /// <param name="patient">Patient object, used to store information</param>
        /// <param name="patientID">ID of querying patient</param>
        /// <returns></returns>
        public static IEnumerator GetPatient(Patient patient, string patientID)
        {
            string GetPatientUri = $"{BaseUri}/patients/{patientID}";
            yield return GetRequest(GetPatientUri);

            if (WebRequestReturnData != null)
            {
                JSONNode PatientJson = JSON.Parse(WebRequestReturnData);
                Patient Patient = JsonToPatient(PatientJson, patientID);
                CopyProperties(Patient, patient);
            }                
        }

        /// <summary>
        /// Method <c>GetMultipleHolograms</c> is used to retrieve multiple hologram meta data from Storage server
        /// </summary>
        /// <param name="hologramList">Hologram object list, used to store information</param>
        /// <param name="IDs">IDs of querying holograms</param>
        /// <returns></returns>
        public static IEnumerator GetMultipleHolograms(List<Hologram> hologramList, string IDs)
        {
            string MultipleHologramUri = $"{BaseUri}/holograms?hid={IDs}";        
            yield return GetRequest(MultipleHologramUri);

            hologramList.Clear();
            string[] ids = IDs.Split(',');
            if (WebRequestReturnData != null)
            {
                JSONNode InitialJsonData = JSON.Parse(WebRequestReturnData);
                foreach (string id in ids)
                {
                    JSONNode data = InitialJsonData[id];
                    JSONArray JsonArray = data.AsArray;

                    if(JsonArray.Count == 0)
                    {
                        Debug.LogError($"Response from server is empty with this ID: {id}");
                    }

                    foreach (JSONNode HologramJson in JsonArray)
                    {
                        Hologram hologram = JsonToHologram(HologramJson, id);
                        hologramList.Add(hologram);
                    }
                }
            }
        }

        /// <summary>
        /// Method <c>GetHologram</c> allows user retrieve single hologram from Storage server by hologram ID
        /// </summary>
        /// <param name="hologram">Hologram object, used to store information</param>
        /// <param name="HolgramID">ID of querying hologram</param>
        /// <returns></returns>
        public static IEnumerator GetHologram(Hologram hologram, string HolgramID)
        {
            string GetHologramUri = $"{BaseUri}/holograms/{HolgramID}";
            yield return GetRequest(GetHologramUri);

            if (WebRequestReturnData != null)
            {
                JSONNode HologramJson = JSON.Parse(WebRequestReturnData);
                Hologram Hologram = JsonToHologram(HologramJson, HolgramID);
                CopyProperties(Hologram, hologram);
            }             
        }


        /// <summary>
        /// Method <c>GetMultipleAuthors</c> is used to retrieve multiple authors meta data from Storage server
        /// </summary>
        /// <param name="authorList">Author object list, used to store information</param>
        /// <param name="IDs">IDs of querying authors</param>
        /// <returns></returns>
        public static IEnumerator GetMultipleAuthors(List<Author> authorList, string IDs)
        {
            string MultipleAuthorUri = $"{BaseUri}/authors?aid={IDs}";        
            yield return GetRequest(MultipleAuthorUri);

            authorList.Clear();
            string[] ids = IDs.Split(',');
            if (WebRequestReturnData != null)
            {
                JSONNode InitialJsonData = JSON.Parse(WebRequestReturnData);
                foreach (string id in ids)
                {
                    JSONNode data = InitialJsonData[id];
                    Author author = JsonToAuthor(data, id);
                    if (author.aid != null)
                    {
                        authorList.Add(author);
                    }
                }
            }
        }

        /// <summary>
        /// Method <c>GetAuthor</c> allows user retrieve single author from Storage server by hologram ID
        /// </summary>
        /// <param name="author">Author object, used to store information</param>
        /// <param name="AuthorID">ID of querying author</param>
        /// <returns></returns>
        public static IEnumerator GetAuthor(Author author, string AuthorID)
        {
            string GetAuthorUri = $"{BaseUri}/authors/{AuthorID}";
            yield return GetRequest(GetAuthorUri);

            if (WebRequestReturnData != null)
            {
                JSONNode AuthorJson = JSON.Parse(WebRequestReturnData);
                Author Author = JsonToAuthor(AuthorJson, AuthorID);
                CopyProperties(Author, author);
            }
        }

        /// <summary>
        /// Method <c>LoadHologram</c> is used to load hologram from Storage server
        /// It requires thehologram ID as the parameter 
        /// </summary>
        /// <param name="HologramID">ID of Hologram</param>
        public static async void LoadHologram(string HologramID, HologramInstantiationSettings setting = null)
        {
            if (setting == null)
            {
                setting = new HologramInstantiationSettings();
            }

            WebRequestReturnData = null;
            //string GetHologramUri = $"{BaseUri}{apiPrefix}/holograms/{HolgramID}/download";
            string GetHologramUri = "https://holoblob.blob.core.windows.net/test/DamagedHelmet-18486331-5441-4271-8169-fcac6b7d8c29.glb";      

            Response response = new Response();
            try
            {
                response = await Rest.GetAsync(GetHologramUri);
            }
            catch (Exception e)
            {
                Debug.LogError(e.Message);
            }

            if (!response.Successful)
            {
                Debug.LogError($"Failed to get glb model from {GetHologramUri}");
                return;
            }

            var gltfObject = GltfUtility.GetGltfObjectFromGlb(response.ResponseData);

            try
            {
                GameObject loadedObject = await gltfObject.ConstructAsync();
                HologramInstantiationSettings.Initialize(loadedObject, setting);
            }
            catch (Exception e)
            {
                Debug.LogError($"{e.Message}\n{e.StackTrace}");
                return;
            }
        }
        #endregion Public Method

        #region Common Method
        /// <summary>
        /// Common method <c>GetRequest</c> is used to handle web request 
        /// </summary>
        /// <param name="uri">Endpoint for the web request</param>
        /// <returns></returns>
        private static IEnumerator GetRequest(string uri)
        {
            using (UnityWebRequest webRequest = UnityWebRequest.Get(uri))
            {
                yield return webRequest.SendWebRequest();
                WebRequestReturnData = null;
                if (webRequest.isNetworkError)
                {
                    Debug.LogError("Web request Error! [Error message]: " + webRequest.error);
                }
                else
                {
                    WebRequestReturnData = webRequest.downloadHandler.text;
                }
            }
        }

        /// <summary>
        /// Common method <c>CopyProperties</c> is used to map the preoperties between two objects 
        /// </summary>
        /// <param name="source">Source object</param>
        /// <param name="destination">Target object</param>
        private static void CopyProperties(object source, object destination)
        {
            PropertyInfo[] destinationProperties = destination.GetType().GetProperties();
            foreach (PropertyInfo destinationPi in destinationProperties)
            {
                PropertyInfo sourcePi = source.GetType().GetProperty(destinationPi.Name);
                destinationPi.SetValue(destination, sourcePi.GetValue(source, null), null);
            }
        }

        /// <summary>
        /// Method <c>JsonToPatient</c> map the json data into Patient object 
        /// </summary>
        /// <param name="Json">Initial json data</param>
        /// <returns>Patient object with retrieved information</returns>
        public static Patient JsonToPatient(JSONNode Json, string id)
        {
            Patient patient = new Patient();

            if (Json["pid"].Value == "")
            {
                Debug.LogError($"Response from server is empty with this patient ID: {id}");
                return patient;
            }

            try
            {
                patient.pid = Json["pid"].Value;
                patient.gender = Json["gender"].Value;
                patient.birthDate = Json["birthDate"].Value;

                PersonName name = new PersonName();
                name.title = Json["name"]["title"].Value;
                name.full = Json["name"]["full"].Value;
                name.given = Json["name"]["given"].Value;
                name.family = Json["name"]["family"].Value;
                patient.name = name;
            }
            catch (Exception e)
            {
                Debug.LogError("Failed to map patient from response data! \n[Error message]: " + e);
            }
                    
            return patient;
        }

        /// <summary>
        /// Method <c>JsonToHologram</c> map the json data into Hologram object
        /// </summary>
        /// <param name="Json">Initial json data</param>
        /// <returns>Hologram object with retrieved information</returns>
        public static Hologram JsonToHologram(JSONNode Json, string id)
        {
            Hologram hologram = new Hologram();

            if (Json["hid"].Value == "")
            {
                Debug.LogError($"Response from server is empty with this ID: {id}");
                return hologram;
            }

            try
            {
                hologram.hid = Json["hid"].Value;
                hologram.title = Json["title"].Value;
                hologram.description = Json["description"].Value;
                hologram.contentType = Json["contentType"].Value;
                hologram.fileSizeInkb = Json["fileSizeInkb"].AsInt;
                hologram.bodySite = Json["bodySite"].Value;
                hologram.dateOfImaging = Json["dateOfImaging"].Value;
                hologram.creationDate = Json["creationDate"].Value;
                hologram.creationMode = Json["creationMode"].Value;
                hologram.creationDescription = Json["creationDescription"].Value;
                hologram.aid = Json["aid"].Value;
                hologram.pid = Json["pid"].Value;
            }
            catch (Exception e)
            {
                Debug.LogError("Failed to map hologram from response data! \n[Error message]: " + e);
            }            
            return hologram;
        }

        /// <summary>
        /// Method <c>JsonToAuthor</c> map the json data into Author object
        /// </summary>
        /// <param name="Json">Initial json data</param>
        /// <returns>Author object with retrieved information</returns>
        public static Author JsonToAuthor(JSONNode Json, string id)
        {
            Author author = new Author();

            if (Json["aid"].Value == "")
            {
                Debug.LogError($"Response from server is empty with this author ID: {id}");
                return author;
            }

            try
            {
                author.aid = Json["aid"].Value;

                PersonName name = new PersonName();
                name.full = Json["name"]["full"].Value;
                name.title = Json["name"]["title"].Value;               
                name.given = Json["name"]["given"].Value;
                name.family = Json["name"]["family"].Value;
                author.name = name;
            }
            catch (Exception e)
            {
                Debug.LogError("Failed to map author from response data! \n[Error message]: " + e);
            }
            return author;
        }
        #endregion Commom Method
    }
}