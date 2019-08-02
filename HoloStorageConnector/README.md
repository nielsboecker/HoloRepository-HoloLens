# HoloStorageConnector
HoloStorageConnector is used to provide an importable Unity asset package, to facilitate the development of HoloLens app with HoloStorage. This package make connection with HoloStorage based on the [HoloStorage Accessor API](https://app.swaggerhub.com/apis/boonwj/HoloRepository/1.0.0#/default/get_patients). As the development of HoloStorage is still in progress, you could test the Connector package with the HoloRepositoryUI backend server, details could be found in our [main GitHub repo](https://github.com/nbckr/HoloRepository-Core).

**Important note**: Currently, the HoloStorageConnector script is developed inside the DemoApplication. This folder only contains a symbolic link to the package inside that applicatiton's `Assets/` in order to prevent duplicate code. Ideally, it would be the other way around. However, due to inconsistencies in how Windows supports symlinks, this can break the build (if the symlink is not recognised in a Windows development environment, the Connector namespace is missing in the demo app). For 3rd party developers to obtain the package they can copy the folder or, which is the recommended way, download the AssetPackage from a GitHub release. 

This package provides some scripts allow developers use it to retrieve data and load 3D objects from Storage server. It also provided a Demo scene to guide developer use this package. The detail of methods and usage of each scripts are listed below.

**Remind: Current script is based on HoloRepositoryUI backend server, some method and class definition will be modified later.**

To use the HoloStorageConnector, after you import the asset package, you could import the namespace like this:
```
using HoloStorageConnector;
```

## HoloStorageClient
`HoloStorageClient` script provided multiple methods to retrieve data from Storage server. For now, you could retrieve the meta data of patient and holograms based on ID, and also load 3D object from the server. Please note, currently the `LoadHologram` method only load the object from a hard code uri.

|Method|Description|
| :--- | :--- | 
|`GetMultiplePatients(List<Patient> List, string IDs)`|Retrieve multiple patients meta data from HoloStorage server|
|`GetPatient(Patient patient, string patientID)`|Retrieve a single patient meta data by patient ID|
|`GetMultipleHolograms(List<Hologram> List, string IDs)`|Retrieve multiple Holograms meta data from HoloStorage server|
|`GetHologram(Hologram hologram, string HolgramID)`|Retrieve a single Hologram meta data by hologram ID|
|`LoadHologram(HologramInstantiationSettings setting, string HologramID)`|Load a Hologram object to scene by ID, requires Hologram instantiation settings|

Example usage:
```
StartCoroutine(RetrievePatients());
IEnumerator RetrievePatients()
{        
    List<Patient> patientList = new List<PatientInfo>();
    yield return HoloStorageClient.GetMultiplePatients(patientList, "IDs");
    //Do something here...
    //For example:
    foreach (Patient patient in patientList)
    {
        Debug.Log(patient.name.full);
    }
}
...
HoloStorageClient.LoadHologram(setting, "hid");
```
## HologramInstantiationSettings
`HologramInstantiationSettings` script allow users to set the transform settings before load the 3D object from server, for example, set the position, rotation and scale of the 3D object, you can also determine whether the object could be manipulated or which scene you want to load.

|Properties|Description|
| :--- | :--- | 
|`Name`|Set a name for the loaded model|
|`Postition`|Set position for the loaded model, the value should be a Vector3 object|
|`Rotation`|Set rotation for the loaded model, the parameter should be a Vector3 object|
|`Size`|Real size in the scene, The longest side of the loaded object will be set to this value |
|`Manipulable`|Determine whether the object could be manipulated, default setting is true|
|`SeceneName`|Determine which scene you want to load the object|

Example usage:

You need to create a HologramInstantiationSettings instance before you load the hologram, and pass the settings as a parameter when call the LoadHologram method.
```
void LoadModel()
{
    HologramInstantiationSettings setting = new HologramInstantiationSettings();
    setting.Name = "Loaded Model";
    setting.Rotation = new Vector3(0, 180, 0);
    setting.Position = new Vector3(0f, 0f, 2f);
    setting.Size = 0.5f;
    HoloStorageClient.LoadHologram(setting, "hid");
}
```

## HoloStorageEntities
`HoloStorageEntities` script is used to define the related HoloStorage entities, make it easier to map the information from json data. 

|Classes|Description|
| :--- | :--- | 
|`Patient`|Patient object, contains the basic information of the patient, like name date od birth and address|
|`Hologram`|Hologram object, contains the basic information of the hologram, like title, create date and author|
|`PersonName`|Name for a person, contains title, full name, first name and last name|
|`Address`|Contains street, city state and postcode properties|
|`Subject`|Subject of the Hologram|
|`Author`|Author of the Hologram|

## Prefabs
This package also provided some prefabs to save your development time, which could be found in Prefabs folder.

## Demo scene
<p align="center">
    <img src="../HoloRepositoryDemoApplication/Images/DemoScene.png" height="400">
</p>

This package also provided a Demo scene to guide the developer how to use this package, which you can find [here](https://github.com/nbckr/HoloRepository-HoloLens/tree/LENS/Connector-Scripts/HoloStorageConnector/HoloRpository/Demo).
