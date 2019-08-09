# HoloStorageConnector
HoloStorageConnector is used to provide an importable Unity asset package, to facilitate the development of HoloLens app with HoloStorage. This package build the connection with HoloStorage based on the [HoloStorage Accessor API](https://app.swaggerhub.com/apis/boonwj/HoloRepository/1.0.0#/). Current connector package is developed based on HoloStorage Accessor API version 1.0.0, you can access our main [GitHub repository](https://github.com/nbckr/HoloRepository-Core) to find more information about the HoloStorage Accessor server.

**Important note**: Currently, the HoloStorageConnector script is developed inside the DemoApplication. This folder only contains a symbolic link to the package inside that applicatiton's `Assets/` in order to prevent duplicate code. Ideally, it would be the other way around. However, due to inconsistencies in how Windows supports symlinks, this can break the build (if the symlink is not recognised in a Windows development environment, the Connector namespace is missing in the demo app). For 3rd party developers to obtain the package they can copy the folder or, which is the recommended way, download the AssetPackage from a GitHub release. 

This package provides some scripts allow developers use it to retrieve data and load 3D objects from Storage server. It also provided a Demo scene to guide developer use this package. The detail of methods and usage of each scripts are listed below.

To use the HoloStorageConnector, after you import the asset package, you could import the namespace like this:
```
using HoloStorageConnector;
```

## HoloStorageClient
`HoloStorageClient` script provided multiple methods to retrieve data from Storage server. For now, you could retrieve the meta data of patients, holograms and authors based on ID, and also load 3D object from the server. Please note, currently the `LoadHologram` method only load the object from a hard code uri. You have to create a coroutine to run the retrieve methods, the details could be found in example usage.

|Method|Description|
| :--- | :--- | 
|`GetMultiplePatients(List<Patient> patientList, string IDs)`|Retrieve multiple patients meta data from HoloStorage server|
|`GetPatient(Patient patient, string patientID)`|Retrieve a single patient meta data by patient ID|
|`GetMultipleHolograms(List<Hologram> hologramList, string IDs, QueryType queryType)`|Retrieve multiple holograms meta data from HoloStorage server. QueryType is optional, determines whether query holograms by hid or pid, could be either `QueryType.hid` or `QueryType.pid`, the default value is `QueryType.hid`|
|`GetHologram(Hologram hologram, string holgramID)`|Retrieve a single hologram meta data by hologram ID|
|`GetMultipleAuthors(List<Author> authorList, string IDs)`|Retrieve multiple authors meta data from HoloStorage server|
|`GetAuthor(Author author, string authorID)`|Retrieve a single author meta data by author ID|
|`LoadHologram(string hologramID, HologramInstantiationSettings setting)`|Load a Hologram object to scene by ID, Hologram instantiation settings is optional|

Example usage:
```
void Start()
{
StartCoroutine(RetrievePatients());
...
HoloStorageClient.LoadHologram("hid");
}

IEnumerator RetrievePatients()
{        
    List<Patient> patientList = new List<Patient>();
    yield return HoloStorageClient.GetMultiplePatients(patientList, "pids");
    //Do something here...
    //For example:
    foreach (Patient patient in patientList)
    {
        Debug.Log(patient.name.full);
    }
}
```
## HologramInstantiationSettings
`HologramInstantiationSettings` script allow users to set the transform settings before load the 3D object from server, for example, set the position, rotation and scale of the 3D object, you can also determine whether the object could be manipulated or which scene you want to load.

|Properties|Description|
| :--- | :--- | 
|`Name`|Set a name for the loaded model. Default value is "LoadedModel"|
|`Position`|Set position for the loaded model, the value should be a Vector3 object. Default value is (0, 0, 0)|
|`Rotation`|Set rotation for the loaded model, the parameter should be a Vector3 object. Default value is (0, 0, 0)|
|`Size`|Real size in the scene, The longest side of the loaded object will be set to this value. Default value is 0.5f|
|`Manipulable`|Determine whether the object could be manipulated. Default setting is true|
|`SeceneName`|Determine which scene you want to load the object. Default value is null, which means the object will be loaded to the current active scene|

Example usage:

You could create a HologramInstantiationSettings instance before you load the hologram, and pass the settings as a parameter when call the LoadHologram method. The settings are optional, if you ignore the settings, the value of each properties will be set to default.
```
void LoadModel()
{
    HologramInstantiationSettings setting = new HologramInstantiationSettings
    {
        Manipulable = true,
        Rotation = new Vector3(0, 180, 0),
        Position = new Vector3(0f, 0f, 2.0f),
        Size = 0.5f
    };
    HoloStorageClient.LoadHologram("hid", setting);
}
```

## HoloStorageEntities
`HoloStorageEntities` script is used to define the related HoloStorage entities, make it easier to map the information from json data. 

|Classes|Description|
| :--- | :--- | 
|`Patient`|Patient object, contains the basic information of the patient, like id, name, date of birth and gender|
|`Hologram`|Hologram object, contains the basic information of the hologram, like title, create date and description|
|`Author`|Author of the Hologram, contains the ID and name of the author|
|`PersonName`|Name for a person, contains title, full name, given name and family name|

## Prefabs
This package also provided some prefabs to save your development time, which could be found in [Prefabs folder](../HoloRepositoryDemoApplication/Assets/HoloStorageConnector/Prefabs).

### InformationList
You could add this list view to you scene, choose the information type that you want to get, whether patient or hologram, you could also select what information you want to display in the list, all settings could be done in inspector.

<p align="center">
    <img src="../HoloRepositoryDemoApplication/Images/InformationList.png">
</p>

### HologramLoader
Drag this prefab into you scene, and set the related options in the inspector. In the play mode, it will load the hologram to you scene. 
<p align="center">
    <img src="../HoloRepositoryDemoApplication/Images/HologramLoader.png">
</p>

## Demo scene
<p align="center">
    <img src="../HoloRepositoryDemoApplication/Images/DemoScene.png" height="400">
</p>

This package also provided some demo scene to guide the developer how to use this package, which you can find [here](../HoloRepositoryDemoApplication/Assets/HoloStorageConnector/Demo). You could follow the StorageConnectorDemo scene and demo script to learn the basic usage of the HoloStorageConnector package. There is also a prefab scene to demostrate how to use the prefabs, feel free to explore.

Note: You have to import the TMP Essentials when you open the scenes at first time, just follow the guide in the window.

<p align="center">
    <img src="../HoloRepositoryDemoApplication/Images/TMP.png">
</p>

## Technologies
The following technologies are used in this component:
* [SimpleJSON](https://wiki.unity3d.com/index.php/SimpleJSON) is used to parse the JSON data.
* [Unity 2018.4.2f1](https://unity3d.com/unity/whats-new/2018.4.2) is the main platform to develop the HoloLens app and connector package.   
* [MRTK v2.0.0 RC2.1](https://github.com/microsoft/MixedRealityToolkit-Unity) is an open source development kit used to develop mixed reality applications.
