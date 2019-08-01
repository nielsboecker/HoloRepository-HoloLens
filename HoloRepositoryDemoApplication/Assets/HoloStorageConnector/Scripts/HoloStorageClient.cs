using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Networking;
using Microsoft.MixedReality.Toolkit.Utilities;
using Microsoft.MixedReality.Toolkit.Utilities.Gltf.Serialization;
using System.Collections.Generic;
using System.Reflection;
using SimpleJSON;


/// <summary>
/// Class <c>HoloStorageClient</c> provided multiple methods to retrieve data from Storage server.
/// </summary>
namespace HoloStorageConnector
{
    public class HoloStorageClient : MonoBehaviour
    {
        #region Properties
        private static string BaseUri = "http://localhost:3001";
        private static int apiVersion = 1;
        private static string apiPrefix = $"/api/v{apiVersion}";
        private static string WebRequestReturnData = null;
        #endregion Properties

        #region Public Method
        public static void SetBaseUri(string Uri)
        {
            BaseUri = Uri;
        }
        public static void SetAPIVersion(int version)
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
            //string MultiplePatientUri = $"{BaseUri}{apiPrefix}/patients?={IDs}";
            string MultiplePatientUri = $"{BaseUri}{apiPrefix}/patients";
            yield return GetRequest(MultiplePatientUri);

            patientList.Clear();
            if (WebRequestReturnData != null)
            {
                JSONNode InitialJsonData = JSON.Parse(WebRequestReturnData);
                JSONArray JsonArray = InitialJsonData.AsArray;
                foreach (JSONNode PatientJson in JsonArray)
                {
                    Patient patient = JsonToPatient(PatientJson);
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
            string GetPatientUri = $"{BaseUri}{apiPrefix}/patients/{patientID}";
            yield return GetRequest(GetPatientUri);
            try
            {
                JSONNode PatientJson = JSON.Parse(WebRequestReturnData);
                Patient Patient = JsonToPatient(PatientJson);
                CopyProperties(Patient, patient);
            }
            catch (Exception e)
            {
                Debug.LogError("Failed to get the patient from server! \n[Error message]:" + e.Message);
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
            //string MultipleHologramUri = $"{BaseUri}{apiPrefix}/holograms?={IDs}";        
            string MultipleHologramUri = $"{BaseUri}{apiPrefix}/holograms";
            yield return GetRequest(MultipleHologramUri);

            hologramList.Clear();
            if (WebRequestReturnData != null)
            {
                JSONNode InitialJsonData = JSON.Parse(WebRequestReturnData);
                JSONArray JsonArray = InitialJsonData.AsArray;
                foreach (JSONNode HologramJson in JsonArray)
                {
                    Hologram hologram = JsonToHologram(HologramJson);
                    if (hologram.hid != null)
                    {
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
            string GetHologramUri = $"{BaseUri}{apiPrefix}/holograms/{HolgramID}";
            yield return GetRequest(GetHologramUri);
            try
            {
                JSONNode HologramJson = JSON.Parse(WebRequestReturnData);
                Hologram Hologram = JsonToHologram(HologramJson);
                CopyProperties(Hologram, hologram);
            }
            catch (Exception e)
            {
                Debug.LogError("Failed to get the hologram from server! \n[Error message]: " + e.Message);
            }               
        }

        /// <summary>
        /// Method <c>LoadHologram</c> is used to load hologram from Storage server
        /// It requires thehologram ID as the parameter 
        /// </summary>
        /// <param name="HologramID">ID of Hologram</param>
        public static async void LoadHologram(string HologramID)
        {
            WebRequestReturnData = null;
            //string GetHologramUri = BaseUri + "/holograms/" + HologramID + "/download";
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
                ModelSetting.Initialize(loadedObject);
            }
            catch (Exception e)
            {
                Debug.LogError($"{e.Message}\n{e.StackTrace}");
                return;
            }
        }
        #endregion Public Method

        #region Private Common Method
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
        private static Patient JsonToPatient(JSONNode Json)
        {
            Patient patient = new Patient();

            if (Json["pid"].Value == "")
            {
                Debug.LogError("No response from server with this patient ID!");
                return patient;
            }

            try
            {
                patient.pid = Json["pid"].Value;

                PersonName name = new PersonName();
                name.title = Json["name"]["title"].Value;
                name.full = Json["name"]["full"].Value;
                name.first = Json["name"]["first"].Value;
                name.last = Json["name"]["last"].Value;
                patient.name = name;

                patient.gender = Json["gender"].Value;
                patient.email = Json["email"].Value;
                patient.phone = Json["phone"].Value;
                patient.birthDate = Json["birthDate"].Value;
                patient.pictureUrl = Json["pictureUrl"].Value;

                Address address = new Address();
                address.street = Json["address"]["street"].Value;
                address.city = Json["address"]["city"].Value;
                address.state = Json["address"]["state"].Value;
                address.postcode = Json["address"]["postcode"].AsInt;
                patient.address = address;
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
        private static Hologram JsonToHologram(JSONNode Json)
        {
            Hologram hologram = new Hologram();

            if (Json["hid"].Value == "")
            {
                Debug.LogError("No response from server with this hologram ID!");
                return hologram;
            }

            try
            {
                hologram.hid = Json["hid"].Value;
                hologram.title = Json["title"].Value;

                Subject subject = new Subject();
                subject.pid = Json["subject"]["pid"].Value;
                PersonName name = new PersonName();
                name.title = Json["subject"]["name"]["title"].Value;
                name.full = Json["subject"]["name"]["full"].Value;
                name.first = Json["subject"]["name"]["first"].Value;
                name.last = Json["subject"]["name"]["last"].Value;
                subject.name = name;
                hologram.subject = subject;

                Author author = new Author();
                author.aid = Json["author"]["aid"].Value;
                PersonName AuthorName = new PersonName();
                AuthorName.title = Json["author"]["name"]["title"].Value;
                AuthorName.full = Json["author"]["name"]["full"].Value;
                AuthorName.first = Json["author"]["name"]["first"].Value;
                AuthorName.last = Json["author"]["name"]["last"].Value;
                author.name = AuthorName;
                hologram.author = author;

                hologram.createdDate = Json["createdDate"].Value;
                hologram.fileSizeInkb = Json["fileSizeInkb"].AsInt;
                hologram.imagingStudySeriesId = Json["imagingStudySeriesId"].Value;
            }
            catch (Exception e)
            {
                Debug.LogError("Failed to map hologram from response data! \n[Error message]: " + e);
            }            
            return hologram;
        }
        #endregion Private Commom Method
    }
}