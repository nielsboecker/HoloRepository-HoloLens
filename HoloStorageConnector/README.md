# HoloRepository
The COMP0111 project MSGOSHHOLO, where we develop a software engineering solution for Microsoft and GOSH.
# HoloRepository Connector package
Connector assets package is used to provide , to facilitate the development of HoloLens app with HoloStorage. This package make connection with HoloStorage based on the [HoloStorage Accessor API](https://app.swaggerhub.com/apis/boonwj/HoloRepository/1.0.0#/default/get_patients). As the development of HoloStorage is still in progress, you could test the Connector package with the HoloRepositoryUI backend server, details could be found in our [main GitHub repo](https://github.com/nbckr/HoloRepository-Core).

Currently, the HoloRepositoryConnector script is developed inside the DemoApplication, it provides some scripts allow developers use it to retrieve data and load 3D objects from Storage server. It also provided a Demo scene to guide developer use this package. The detail of methods and usage of each scripts are listed below.

**Remind: Current script is based on HoloRepositoryUI backend server, some method and class defination will be modified later.**

To use the HoloConnector, after you import the assest package, you could import the namespcae like this:
```
using HoloRepository;
```

## StorageConnectionServer
`StorageConnectionServer` script provided multiple methods to retrieve data from Storage server. For now, you could retieve the meta data of patient and holograms based on ID, and also load 3D object from the server. Please note, currently the `LoadHologram` method only load the object from a hard-code uri.
|Method|Description|
| :--- | :--- | 
|`GetMultiplePatient(List<Patient> List, string IDs)`|Retrieve multiple patients meta data from HoloStorage server, parameter "IDs" can be |
|`GetPatient(Patient patient, string patientID)`|Retrieve a single patient meta data by patient ID|
|`GetMultipleHologram(List<Hologram> List, string IDs)`|Retrieve multiple Holograms meta data from HoloStorage server|
|`GetHologram(Hologram hologram, string HolgramID)`|Retrieve a single Hologram meta data by hologram ID|
|`LoadHologram(string HologramID)`|Load a Hologram object to scene by ID|
Example usage:
```
StartCoroutine(Query());
IEnumerator Query()
{        
    List<Patient> patientList = new List<PatientInfo>();
    yield return StorageConnectionServer.GetMultiplePatient(patientList, "IDs");
    //Do something here...
    foreach (PatientInfo patient in patientList)
    {
        Debug.Log(patient.name.full);
    }
}
...
StorageConnectionServer.LoadHologram("hololensid");
```
## ModelSetting
`ModelSetting` script allow users to set the transform settings before load the 3D object from server, for example, set the position, rotation and scale of the 3D object, you can also determine whether the object could be manipulated or which scene you want to load.
|Method|Description|
| :--- | :--- | 
|`SetModelName(string name)`|Set a name for the loaded model|
|`SetPostition(Vector3 position)`|Set load position for the loaded model, the parameter should be a Vector3 object|
|`SetRotation(Vector3 rotation)`|Set load rotation for the loaded model, the parameter should be a Vector3 object|
|`SetSize(float size)`|Set size for the loaded model, |
|`SetManipulable(bool manipulable)`|Determine whether the object could be manipulated, default setting is true|
|`SetSeceneName(string scenename)`|Determine which scene you want to load the object|
Example usage:
```
void LoadModel()
{
    ModelSetting.SetName("LoadedModel");
    ModelSetting.SetManipulable(true);
    ModelSetting.SetRotation(new Vector3(0, 180, 0));
    ModelSetting.SetPostition(new Vector3(0, 0, 2f));
    ModelSetting.SetSize(1f);
    StorageConnectionServer.LoadHologram("hololensid");
}
```

## ClassesDefination
`ClassesDefination` script is used to define the related class, make it easier to map the information from json data. 
|Class|Description|
| :--- | :--- | 
|`Patient`|Patient object, contains the basic information of the patient, like name date od birth and adress|
|`Hologram`|Hologram object, contains the basic information of the hologram, like title, create date and author|
|`PersonName`|Name for a person, contains title, full name, first name and last name|
|`Address`|Contains street, city state and postcode properties|
|`Subject`|Subject of the Hologram|
|`Author`|Author of the Hologram|

## Prefabs
This package also provided some prefabs to save your development time, Which could be foud in Prefabs folder, include a Canvas for HoloLens app and a numer pad for D3D type HoloLens app.

## Demo scene
<p align="center">
    <img src="../HoloRepositoryDemoApplication/Images/DemoScene.png" height="200">
</p>

This pckage also provided a Demo scene to guide the developer how to use this package, which you can find [here](https://github.com/nbckr/HoloRepository-HoloLens/tree/LENS/Connector-Scripts/HoloStorageConnector/HoloRpository/Demo).