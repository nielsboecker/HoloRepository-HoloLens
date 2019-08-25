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
    public enum QueryType {hids, pids}

    /// <summary>
    /// Class <c>HoloStorageClient</c> provided multiple methods to retrieve data from Storage server.
    /// </summary>
    public class HoloStorageClient
    {
        #region Properties
        private static string StorageAccessorEndpoint = "http://localhost";
        private static string Port = "3200";
        private static string ApiVersion = "v1";
        private static readonly string BaseUri = $"{StorageAccessorEndpoint}:{Port}/api/{ApiVersion}";        
        private static string WebRequestReturnData = null;
        #endregion Properties

        #region Public Method
        /// <summary>
        /// Set base end point Uri
        /// </summary>
        public static void SetEndpoint(string endpoint)
        {
            StorageAccessorEndpoint = endpoint;
        }
        public static void SetPort(string portValue)
        {
            Port = portValue;
        }
        public static void SetApiVersion(string version)
        {
            ApiVersion = version;
        }

        /// <summary>
        /// Method <c>GetMultiplePatients</c> is used to retrieve multiple patients meta data from Storage server 
        /// Result will be store in the parameter list 
        /// </summary>
        /// <param name="patientList">Patient object list, used to store information</param>
        /// <param name="IDs">IDs of querying patients</param>
        public static IEnumerator GetMultiplePatients(List<Patient> patientList, string IDs)
        {
            string multiplePatientUri = $"{BaseUri}/patients?pids={IDs}";
            yield return GetRequest(multiplePatientUri);

            patientList.Clear();
            string[] ids = IDs.Split(',');
            if (WebRequestReturnData != null)
            {
                JSONNode initialJsonData = JSON.Parse(WebRequestReturnData);
                foreach (string id in ids)
                {
                    JSONNode data = initialJsonData[id];
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
        /// <param name="resultPatient">Patient object, used to store information</param>
        /// <param name="patientID">ID of querying patient</param>
        public static IEnumerator GetPatient(Patient resultPatient, string patientID)
        {
            string getPatientUri = $"{BaseUri}/patients/{patientID}";
            yield return GetRequest(getPatientUri);

            if (WebRequestReturnData != null)
            {
                JSONNode patientJson = JSON.Parse(WebRequestReturnData);
                Patient receivedPatient = JsonToPatient(patientJson, patientID);
                CopyProperties(receivedPatient, resultPatient);
            }                
        }

        /// <summary>
        /// Method <c>GetMultipleHolograms</c> is used to retrieve multiple hologram meta data from Storage server
        /// </summary>
        /// <param name="hologramList">Hologram object list, used to store information</param>
        /// <param name="IDs">IDs of querying holograms</param>
        public static IEnumerator GetMultipleHolograms(List<Hologram> hologramList, string IDs, QueryType queryType = QueryType.hids)
        {
            string multipleHologramUri = $"{BaseUri}/holograms?{queryType.ToString()}={IDs}";
            yield return GetRequest(multipleHologramUri);

            hologramList.Clear();
            string[] ids = IDs.Split(',');
            if (WebRequestReturnData != null)
            {
                JSONNode initialJsonData = JSON.Parse(WebRequestReturnData);
                foreach (string id in ids)
                {
                    JSONNode data = initialJsonData[id];
                    JSONArray JsonArray = data.AsArray;

                    if(JsonArray.Count == 0)
                    {
                        Debug.LogWarning($"Response from server is empty with this patient ID: {id}");
                    }

                    foreach (JSONNode hologramJson in JsonArray)
                    {
                        Hologram hologram = JsonToHologram(hologramJson, id);
                        hologramList.Add(hologram);
                    }
                }
            }
        }

        /// <summary>
        /// Method <c>GetHologram</c> allows user retrieve single hologram from Storage server by hologram ID
        /// </summary>
        /// <param name="resultHologram">Hologram object, used to store information</param>
        /// <param name="holgramID">ID of querying hologram</param>
        public static IEnumerator GetHologram(Hologram resultHologram, string holgramID)
        {
            string getHologramUri = $"{BaseUri}/holograms/{holgramID}";
            yield return GetRequest(getHologramUri);

            if (WebRequestReturnData != null)
            {
                JSONNode hologramJson = JSON.Parse(WebRequestReturnData);
                Hologram receivedHologram = JsonToHologram(hologramJson, holgramID);
                CopyProperties(receivedHologram, resultHologram);
            }             
        }

        /// <summary>
        /// Method <c>GetMultipleAuthors</c> is used to retrieve multiple authors meta data from Storage server
        /// </summary>
        /// <param name="authorList">Author object list, used to store information</param>
        /// <param name="IDs">IDs of querying authors</param>
        public static IEnumerator GetMultipleAuthors(List<Author> authorList, string IDs)
        {
            string multipleAuthorUri = $"{BaseUri}/authors?aids={IDs}";        
            yield return GetRequest(multipleAuthorUri);

            authorList.Clear();
            string[] ids = IDs.Split(',');
            if (WebRequestReturnData != null)
            {
                JSONNode initialJsonData = JSON.Parse(WebRequestReturnData);
                foreach (string id in ids)
                {
                    JSONNode data = initialJsonData[id];
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
        /// <param name="resultAuthor">Author object, used to store information</param>
        /// <param name="authorID">ID of querying author</param>
        public static IEnumerator GetAuthor(Author resultAuthor, string authorID)
        {
            string getAuthorUri = $"{BaseUri}/authors/{authorID}";
            yield return GetRequest(getAuthorUri);

            if (WebRequestReturnData != null)
            {
                JSONNode authorJson = JSON.Parse(WebRequestReturnData);
                Author receivedAuthor = JsonToAuthor(authorJson, authorID);
                CopyProperties(receivedAuthor, resultAuthor);
            }
        }

        /// <summary>
        /// Method <c>LoadHologram</c> is used to load hologram from Storage server
        /// It requires thehologram ID as the parameter 
        /// </summary>
        /// <param name="hologramID">ID of Hologram</param>
        public static async void LoadHologram(string hologramID, HologramInstantiationSettings setting = null)
        {
            if (setting == null)
            {
                setting = new HologramInstantiationSettings();
            }

            string getHologramUri = $"{BaseUri}/holograms/{hologramID}/download";

            Response response = new Response();
            try
            {
                response = await Rest.GetAsync(getHologramUri);
            }
            catch (Exception e)
            {
                Debug.LogError(e.Message);
            }

            if (!response.Successful)
            {
                Debug.LogError($"Failed to get glb model from {getHologramUri}");
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
        /// <param name="json">Initial json data</param>
        /// <returns>Patient object with retrieved information</returns>
        public static Patient JsonToPatient(JSONNode json, string id)
        {
            Patient patient = new Patient();

            if (json == null)
            {
                Debug.LogWarning($"Response error with this patient ID: {id}");
                return patient;
            }

            if (json["pid"].Value == "")
            {
                Debug.LogWarning($"Response from server is empty with this patient ID: {id}");
                return patient;
            }

            try
            {
                patient.pid = json["pid"].Value;
                patient.gender = json["gender"].Value;
                patient.birthDate = json["birthDate"].Value;

                PersonName name = new PersonName
                {
                    title = json["name"]["title"].Value,
                    full = json["name"]["full"].Value,
                    given = json["name"]["given"].Value,
                    family = json["name"]["family"].Value,
                };
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
        /// <param name="json">Initial json data</param>
        /// <returns>Hologram object with retrieved information</returns>
        public static Hologram JsonToHologram(JSONNode json, string id)
        {
            Hologram hologram = new Hologram();

            if (json == null)
            {
                Debug.LogWarning($"Response error with this hologram ID: {id}");
                return hologram;
            }

            if (json["hid"].Value == "")
            {
                Debug.LogWarning($"Response from server is empty with this hologram ID: {id}");
                return hologram;
            }

            try
            {
                hologram.hid = json["hid"].Value;
                hologram.title = json["title"].Value;
                hologram.description = json["description"].Value;
                hologram.contentType = json["contentType"].Value;
                hologram.fileSizeInkb = json["fileSizeInkb"].AsInt;
                hologram.bodySite = json["bodySite"].Value;
                hologram.dateOfImaging = json["dateOfImaging"].Value;
                hologram.creationDate = json["creationDate"].Value;
                hologram.creationMode = json["creationMode"].Value;
                hologram.creationDescription = json["creationDescription"].Value;
                hologram.aid = json["aid"].Value;
                hologram.pid = json["pid"].Value;
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
        /// <param name="json">Initial json data</param>
        /// <returns>Author object with retrieved information</returns>
        public static Author JsonToAuthor(JSONNode json, string id)
        {
            Author author = new Author();

            if (json == null)
            {
                Debug.LogWarning($"Response error with this author ID: {id}");
                return author;
            }

            if (json["aid"].Value == "")
            {
                Debug.LogWarning($"Response from server is empty with this author ID: {id}");
                return author;
            }

            try
            {
                author.aid = json["aid"].Value;

                PersonName name = new PersonName
                {
                    title = json["name"]["title"].Value,
                    full = json["name"]["full"].Value,
                    given = json["name"]["given"].Value,
                    family = json["name"]["family"].Value,
                };
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