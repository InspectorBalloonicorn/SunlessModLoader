

// See https://aka.ms/new-console-template for more information
using Newtonsoft.Json;
using SunlessModLoader.Classes.Classes;
using SunlessModLoader.Classes.Helpers;
using System.Runtime.CompilerServices;
using static System.Net.Mime.MediaTypeNames;

SunlessModHelper _helper = new SunlessModHelper();

////
////
////
////
////
////
////
////READ CONFIG FILE

List<string> addressBook = JsonConvert.DeserializeObject<List<String>>(File.ReadAllText("config.json"));





//Console


////
////
////
////
////////READ MAIN GAME FILES

//TODO: Make this dynamically entered at the start of the program.

string gameDirectory = addressBook[0];
var addonDirectory = Directory.GetFileSystemEntries(addressBook[1]); //addon filepath is [1]
List<Area> masterAreaList = new List<Area>();
List<Event> masterEventList = new List<Event>();
List<Exchange> masterExchangeList = new List<Exchange>();
List<Personae> masterPersonaList = new List<Personae>();
List<Quality>  masterQualityList = new List<Quality>();

var isModifyExisting = false;
List<Area>? addonAreaList = new List<Area>();
List<Event>? addonEventList = new List<Event>();
List<Exchange>? addonExchangeList = new List<Exchange>();
List<Personae>? addonPersonaList = new List<Personae>();
List<Quality>? addonQualityList = new List<Quality>();
int conflicts = 0;

foreach (var folderPath in Directory.GetFileSystemEntries(gameDirectory))
{

        string objectname = (folderPath.Substring(folderPath.LastIndexOf('\\') + 1));
        switch (objectname)
        {
            case ("entities"):
                {
                    _helper.ReadEntities(folderPath, ref masterAreaList, ref masterEventList, ref masterExchangeList, ref masterPersonaList, ref masterQualityList);
                    break;
                }
            default:
                {
                    break;
                }
        }
}


/////
/////
/////
/////
///////////READ ADDONS AND DECIDE HOW TO KEEP
foreach (var addonPath in addonDirectory)
{
    foreach (var folderPath in Directory.GetFileSystemEntries(addonPath))
    {
        string objectname = (folderPath.Substring(folderPath.LastIndexOf('\\')+1));
        int conflictsPreRun = _helper.ConflictCounter;
        switch (objectname)
        {
            case ("entities"):
            {                    
                    Console.WriteLine($"Resolving entity conflicts with addon: {addonPath.Substring(addonPath.LastIndexOf("\\") + 1)}.");                    
                    _helper.ReadEntities(folderPath, ref addonAreaList, ref addonEventList,
                    ref addonExchangeList, ref addonPersonaList, ref addonQualityList);                  

                    
                    //Handle Events
                    foreach(Event addonEvent in addonEventList)
                    {
                       masterEventList = _helper.CompareAndHandleEvents(addonEvent, masterEventList);
                    }

                    //Handle Qualities
                    foreach (Quality addonQuality in addonQualityList)
                    {
                        masterQualityList = _helper.CompareAndHandleQualities(addonQuality, masterQualityList);
                    }
                    

                    //Handle Personas
                    foreach (Personae addonPersona in addonPersonaList)
                    {
                        masterPersonaList = _helper.CompareAndHandlePersonas(addonPersona, masterPersonaList);
                    }

                    //Handle Personas
                    foreach (Area addonArea in addonAreaList)
                    {
                        masterAreaList = _helper.CompareAndHandleAreas(addonArea, masterAreaList);
                    }

                    //Handle Exchanges
                    foreach (Exchange addonExchange in addonExchangeList)
                    {
                        masterExchangeList = _helper.CompareAndHandleExchanges(addonExchange, masterExchangeList);
                    }

                    conflicts = _helper.ConflictCounter - conflictsPreRun;
                    Console.WriteLine($" Conflicts: {conflicts}\n");
                    break;
                }
            case ("encyclopaedia"): 
            {
                    //TODO: Handle encyclopedia objects

                    conflicts = _helper.ConflictCounter - conflictsPreRun;
                    Console.WriteLine($"Resolving encyclopaedia conflicts with addon: {addonPath.Substring(addonPath.LastIndexOf("\\") + 1)}.");
                    Console.WriteLine($" Conflicts: {conflicts}\n");
                    break;
            }
            case ("geography"):
                {
                    //TODO: Handle geography objects

                    conflicts = _helper.ConflictCounter - conflictsPreRun;
                    Console.WriteLine($"Resolving geography conflicts with addon: {addonPath.Substring(addonPath.LastIndexOf("\\") + 1)}.");
                    Console.WriteLine($" Conflicts: {conflicts}\n");
                    break;
                }
            default:
            {                   
                    break;
            }
        }
        
    }
}

////
////
////
////
////
////
////WRITE OUTPUT FILE


var directoryInfo = Directory.CreateDirectory(addressBook[2]);

var outputEntitiesPath = directoryInfo.FullName + "\\entities";
_helper.WriteEntities(outputEntitiesPath, masterAreaList, masterEventList, masterExchangeList, masterPersonaList, masterQualityList);

Console.WriteLine($"RUNTIME COMPLETE\n\n\nTOTAL CONFLICTS RESOLVED: {_helper.ConflictCounter}");
Console.Beep();
Console.ReadLine();





