using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using SunlessModLoader.Classes.Classes;
using System.Collections.Generic;
using System.Data;
using System.Net.Http.Headers;
using System.Text;
using System.Text.RegularExpressions;
using static System.Net.Mime.MediaTypeNames;
using System.Xml.Linq;
using System.Reflection.Emit;
using static System.Formats.Asn1.AsnWriter;

namespace SunlessModLoader.Classes.Helpers
{
    public class SunlessModHelper
    {
        public int ConflictCounter{ get; set; }
            
        #region READ DATA
        public void ReadEntities(string folderPath, ref List<Area> areaList, ref List<Event> eventList,
           ref List<Exchange> exchangeList, ref List<Personae> personaList, ref List<Quality> qualityList)
        {
            
            foreach (string filePath in Directory.GetFileSystemEntries(folderPath))
            {
                string objectName = (filePath.Substring(filePath.LastIndexOf('\\') + 1));
                switch (objectName)
                {
                    case "areas.json":
                        {
                            areaList = JsonConvert.DeserializeObject<List<Area>>
                                (SanitizeJson(File.ReadAllText(filePath)));
                            break;
                        }
                    case "events.json":
                        {
                            eventList = JsonConvert.DeserializeObject<List<Event>>
                                (SanitizeJson(File.ReadAllText(filePath)));
                            break;
                        }
                    case "exchanges.json":
                        {
                            exchangeList = JsonConvert.DeserializeObject<List<Exchange>>
                                (SanitizeJson(File.ReadAllText(filePath)));
                            break;
                        }
                    case "personas.json":
                        {
                            personaList = JsonConvert.DeserializeObject<List<Personae>>
                                (SanitizeJson(File.ReadAllText(filePath)));
                            break;
                        }
                    case "qualities.json":
                        {
                            qualityList = JsonConvert.DeserializeObject<List<Quality>>
                                (SanitizeJson(File.ReadAllText(filePath)));
                            break;
                        }
                    default:
                        {
                            //TODO Impliment something here?
                            //Console.WriteLine("default");
                            break;
                        }
                }
            }
        }

        public string SanitizeJson(string inputString)
        {
            string output =  inputString.Replace("\\”", "");
            output = output.Replace("”\\", "");
            output = output.Replace("\\T", "t");
            output = output.Replace("\\.", ".");
            output = output.Replace("\\ ", "");
            return output;
        }
        #endregion

        #region COMPARE AND HANDLE OBJECTS
        public List<Event> CompareAndHandleEvents(Event addonEvent, List<Event> masterEventList)
        {
            bool isModifiedExisting = false;
            int idToCheck = 0;
            //process through event list, check if any ids match the passed in id
            for (int x = 0; x < masterEventList.Count(); x++)
            {
                Event masterEvent = masterEventList[x];

                if (addonEvent.Id == masterEvent.Id)
                {
                    //If you match IDs, you are modifying an existing event,
                    //and we need to check what's changed and merge.

                    ConflictCounter++;
                    isModifiedExisting = true;
                    Event mergedEvent = MergeEvents(addonEvent, masterEvent);
                    masterEventList[x] = mergedEvent;
                    break;
                }
            }

            if (!isModifiedExisting)
            {
                masterEventList.Add(addonEvent);
            }

            return masterEventList;
        }
        private List<ChildBranches> CompareAndHandleChildBranches(List<ChildBranches> addonChildBranchList, List<ChildBranches> masterChildBranchList)
        {
            bool isModifiedExisting = false;
            int idToCheck = 0;
            //iterate over addon child branches [ For every child branch in addons...]
            for (int x = 0; x < addonChildBranchList.Count(); x++)
            {
                isModifiedExisting = false;
                ChildBranches addonChildBranch = addonChildBranchList[x];
                //iterate over master branches to check if we find a match
                for (int y = 0; y < masterChildBranchList.Count(); y++)
                {
                    {
                        ChildBranches masterChildBranch = masterChildBranchList[y];


                        //Check if ID match AND if the values are not considered equal.
                        if (addonChildBranch.Id == masterChildBranch.Id && !addonChildBranch.IsEquals(masterChildBranch))
                         {
                            ConflictCounter++;
                            ChildBranches mergedChildBranches = MergeChildBranches(addonChildBranch, masterChildBranch);
                            masterChildBranchList[y] = mergedChildBranches;
                            isModifiedExisting = true;
                            break;
                        }
                        else if (addonChildBranch.Id == masterChildBranch.Id)
                        {
                            isModifiedExisting = true;
                            break;
                        }
                    }
                }

                //If this is a new child branch, add it
                if (!isModifiedExisting)
                {
                    masterChildBranchList.Add(addonChildBranch);
                }         
            }
            return masterChildBranchList;
        }
        private List<QualitiesRequired> CompareAndHandleQualitiesRequired(List<QualitiesRequired> addonQualitiesRequiredList, List<QualitiesRequired> masterQualitiesRequiredList)
        {
            bool isModifiedExisting = false;
            int idToCheck = 0;
            //iterate over addon child branches
            for (int x = 0; x < addonQualitiesRequiredList.Count(); x++)
            {
                isModifiedExisting = false;
                QualitiesRequired addonQualitiesRequired = addonQualitiesRequiredList[x];
                //iterate over master branches to check if we find a match
                for (int y = 0; y < masterQualitiesRequiredList.Count(); y++)
                {
                    {
                        QualitiesRequired masterQualitiesRequired = masterQualitiesRequiredList[y];

                        if (addonQualitiesRequired.Id == masterQualitiesRequired.Id && !addonQualitiesRequired.IsEquals(masterQualitiesRequired))
                        {
                            //If you match IDs here, you are modifying an existing child object in this event,
                            //and we need to check whats changed and merge.
                            ConflictCounter++;
                            QualitiesRequired mergedChildBranches = MergeQualitiesRequired(addonQualitiesRequired, masterQualitiesRequired);
                            masterQualitiesRequiredList[y] = mergedChildBranches;
                            isModifiedExisting = true;
                            break;
                        }
                        else if (addonQualitiesRequired.Id == masterQualitiesRequired.Id)
                        {
                            isModifiedExisting = true;
                            break;
                        }
                    }
                }

                if (!isModifiedExisting)
                {
                    masterQualitiesRequiredList.Add(addonQualitiesRequired);
                }
            }
            return masterQualitiesRequiredList;
        }
        private List<QualitiesAffected> CompareAndHandleQualitiesAffected(List<QualitiesAffected> addonQualitiesAffectedList, List<QualitiesAffected> masterQualitiesAffectedList)
        {
            bool isModifiedExisting = false;
            int idToCheck = 0;
            //iterate over addon child branches
            for (int x = 0; x < addonQualitiesAffectedList.Count(); x++)
            {
                isModifiedExisting = false;
                QualitiesAffected addonQualitiesAffected = addonQualitiesAffectedList[x];
                //iterate over master branches to check if we find a match
                for (int y = 0; y < masterQualitiesAffectedList.Count(); y++)
                {
                    {
                        QualitiesAffected masterQualitiesAffected = masterQualitiesAffectedList[y];

                        if (addonQualitiesAffected.Id == masterQualitiesAffected.Id && !addonQualitiesAffected.IsEquals(masterQualitiesAffected))
                        {
                            //If you match IDs here but are otherwise not equal, you are modifying an existing child object in this event,
                            //and we need to check whats changed and merge.
                            ConflictCounter++;
                            QualitiesAffected mergedQualitiesAffected = MergeQualitiesAffected(addonQualitiesAffected, masterQualitiesAffected);
                            masterQualitiesAffectedList[y] = mergedQualitiesAffected;
                            isModifiedExisting = true;
                            break;
                        }
                        else if (addonQualitiesAffected.Id == masterQualitiesAffected.Id)
                        {
                            isModifiedExisting = true;
                            break;
                        }
                    }
                }

                if (!isModifiedExisting)
                {
                    masterQualitiesAffectedList.Add(addonQualitiesAffected);
                }
            }
            return masterQualitiesAffectedList;
        }
        private List<Enhancement> CompareAndHandleEnhancements(List<Enhancement> addonEnhancementsList, List<Enhancement> masterEnhancementsList)
        {
            bool isModifiedExisting = false;
            int idToCheck = 0;
            //iterate over addon child branches
            for (int x = 0; x < addonEnhancementsList.Count(); x++)
            {
                isModifiedExisting = false;
                Enhancement addonEnhancement = addonEnhancementsList[x];
                //iterate over master branches to check if we find a match
                for (int y = 0; y < masterEnhancementsList.Count(); y++)
                {
                    {
                        Enhancement masterEnhancement = masterEnhancementsList[y];

                        if (addonEnhancement.Id == masterEnhancement.Id && !addonEnhancement.IsEquals(masterEnhancement))
                        {
                            //If you match IDs here but are otherwise not equal, you are modifying an existing child object in this event,
                            //and we need to check whats changed and merge.
                            ConflictCounter++;
                            Enhancement mergedEnhancement = MergeEnhancements(addonEnhancement, masterEnhancement);
                            masterEnhancementsList[y] = mergedEnhancement;
                            isModifiedExisting = true;
                            break;
                        }
                        else if (addonEnhancement.Id == masterEnhancement.Id)
                        {
                            isModifiedExisting = true;
                            break;
                        }
                    }
                }

                if (!isModifiedExisting)
                {
                    masterEnhancementsList.Add(addonEnhancement);
                }
            }
            return masterEnhancementsList;
        }
        private List<Quality> CompareAndHandleQualityLists(List<Quality> addonQualityList, List<Quality> masterQualityList)
        {
            bool isModifiedExisting = false;
            int idToCheck = 0;
            //iterate over addon child branches
            for (int x = 0; x < addonQualityList.Count(); x++)
            {
                isModifiedExisting = false;
                Quality addonQuality = addonQualityList[x];
                //iterate over master branches to check if we find a match
                for (int y = 0; y < masterQualityList.Count(); y++)
                {
                    {
                        Quality masterQuality = masterQualityList[y];

                        if (addonQuality.Id == masterQuality.Id && !addonQuality.IsEquals(masterQuality))
                        {
                            //If you match IDs here but are otherwise not equal, you are modifying an existing child object in this event,
                            //and we need to check whats changed and merge.
                            ConflictCounter++;
                            Quality mergedQuality = MergeQualities(addonQuality, masterQuality);
                            masterQualityList[y] = mergedQuality;
                            isModifiedExisting = true;
                            break;
                        }
                        else if (addonQuality.Id == masterQuality.Id)
                        {
                            isModifiedExisting = true;
                            break;
                        }
                    }
                }

                if (!isModifiedExisting)
                {
                    masterQualityList.Add(addonQuality);
                }
            }
            return masterQualityList;
        }
        public List<Quality> CompareAndHandleQualities(Quality addonQuality, List<Quality> masterQualityList)
        {
            bool isModifiedExisting = false;
            int idToCheck = 0;
            //process through event list, check if any ids match the passed in id
            for (int x = 0; x < masterQualityList.Count(); x++)
            {
                Quality masterQuality = masterQualityList[x];

                if (addonQuality.Id == masterQuality.Id)
                {
                    //If you match IDs, you are modifying an existing event,
                    //and we need to check what's changed and merge.

                    ConflictCounter++;
                    isModifiedExisting = true;
                    Quality mergedQualities = MergeQualities(addonQuality, masterQuality);
                    masterQualityList[x] = mergedQualities;
                    break;
                }
            }

            if (!isModifiedExisting)
            {
                masterQualityList.Add(addonQuality);
            }

            return masterQualityList;
        }
        public List<Personae> CompareAndHandlePersonas(Personae addonPersona, List<Personae> masterPersonaList)
        {
            bool isModifiedExisting = false;
            int idToCheck = 0;
            //process through event list, check if any ids match the passed in id
            for (int x = 0; x < masterPersonaList.Count(); x++)
            {
                Personae masterPersona = masterPersonaList[x];

                if (addonPersona.Id == masterPersona.Id)
                {
                    //If you match IDs, you are modifying an existing event,
                    //and we need to check what's changed and merge.

                    ConflictCounter++;
                    isModifiedExisting = true;
                    Personae mergedPersonas = MergePersonas(addonPersona, masterPersona);
                    masterPersonaList[x] = mergedPersonas;
                    break;
                }
            }

            if (!isModifiedExisting)
            {
                masterPersonaList.Add(addonPersona);
            }

            return masterPersonaList;
        }
        public List<Area> CompareAndHandleAreas(Area addonArea, List<Area> masterAreaList)
        {
            bool isModifiedExisting = false;
            int idToCheck = 0;
            //process through event list, check if any ids match the passed in id
            for (int x = 0; x < masterAreaList.Count(); x++)
            {
                Area masterArea = masterAreaList[x];

                if (addonArea.Id == masterArea.Id)
                {
                    //If you match IDs, you are modifying an existing event,
                    //and we need to check what's changed and merge.

                    ConflictCounter++;
                    isModifiedExisting = true;
                    Area mergedAreas = MergeAreas(addonArea, masterArea);
                    masterAreaList[x] = mergedAreas;
                    break;
                }
            }

            if (!isModifiedExisting)
            {
                masterAreaList.Add(addonArea);
            }

            return masterAreaList;
        }
        public List<Exchange> CompareAndHandleExchanges(Exchange addonExchange, List<Exchange> masterExchangeList)
        {
            bool isModifiedExisting = false;
            int idToCheck = 0;
            //process through event list, check if any ids match the passed in id
            for (int x = 0; x < masterExchangeList.Count(); x++)
            {
                Exchange masterExchange = masterExchangeList[x];

                if (addonExchange.Id == masterExchange.Id)
                {
                    //If you match IDs, you are modifying an existing event,
                    //and we need to check what's changed and merge.

                    ConflictCounter++;
                    isModifiedExisting = true;
                    Exchange mergedExchange = MergeExchanges(addonExchange, masterExchange);
                    masterExchangeList[x] = mergedExchange;
                    break;
                }
            }

            if (!isModifiedExisting)
            {
                masterExchangeList.Add(addonExchange);
            }

            return masterExchangeList;
        }
        private List<Shop> CompareAndHandleShops(List<Shop> addonShops, List<Shop> masterShops)
        {
            bool isModifiedExisting = false;
            int idToCheck = 0;
            //iterate over addon child branches
            for (int x = 0; x < addonShops.Count(); x++)
            {
                isModifiedExisting = false;
                Shop addonShop = addonShops[x];
                //iterate over master branches to check if we find a match
                for (int y = 0; y < masterShops.Count(); y++)
                {
                    {
                        Shop masterShop = masterShops[y];

                        if (addonShop.Id == masterShop.Id && !addonShop.IsEquals(masterShop))
                        {
                            //If you match IDs here but are otherwise not equal, you are modifying an existing child object in this event,
                            //and we need to check whats changed and merge.
                            ConflictCounter++;
                            Shop mergedShop = MergeShops(addonShop, masterShop);
                            masterShops[y] = mergedShop;
                            isModifiedExisting = true;
                            break;
                        }
                        else if (addonShop.Id == masterShop.Id)
                        {
                            isModifiedExisting = true;
                            break;
                        }
                    }
                }

                if (!isModifiedExisting)
                {
                    masterShops.Add(addonShop);
                }
            }
            return masterShops;
        }
        private List<Availability> CompareAndHandleAvailabilities(List<Availability> addonAvailList, List<Availability> masterAvailList)
        {
            bool isModifiedExisting = false;
            int idToCheck = 0;
            //iterate over addon child branches
            for (int x = 0; x < addonAvailList.Count(); x++)
            {
                isModifiedExisting = false;
                Availability addonAvailability = addonAvailList[x];
                //iterate over master branches to check if we find a match
                for (int y = 0; y < masterAvailList.Count(); y++)
                {
                    {
                        Availability masterAvailability = masterAvailList[y];

                        if (addonAvailability.Id == masterAvailability.Id && !addonAvailability.IsEquals(masterAvailability))
                        {
                            //If you match IDs here but are otherwise not equal, you are modifying an existing child object in this event,
                            //and we need to check whats changed and merge.
                            ConflictCounter++;
                            Availability mergedAvail = MergeAvailabilities(addonAvailability, masterAvailability);
                            masterAvailList[y] = mergedAvail;
                            isModifiedExisting = true;
                            break;
                        }
                        else if (addonAvailability.Id == masterAvailability.Id)
                        {
                            isModifiedExisting = true;
                            break;
                        }
                    }
                }

                if (!isModifiedExisting)
                {
                    masterAvailList.Add(addonAvailability);
                }
            }
            return masterAvailList;
        }

        private Availability MergeAvailabilities(Availability addonAvailability, Availability masterAvailability)
        {
            if (addonAvailability.Cost != masterAvailability.Cost) { masterAvailability.Cost = addonAvailability.Cost; }
            if (addonAvailability.SellPrice != masterAvailability.SellPrice) { masterAvailability.SellPrice = addonAvailability.SellPrice; }
            if (addonAvailability.BuyMessage != masterAvailability.BuyMessage) { masterAvailability.BuyMessage = addonAvailability.BuyMessage; }
            if (addonAvailability.SellMessage != masterAvailability.SellMessage) { masterAvailability.SellMessage = addonAvailability.SellMessage; }

            //check Quality
            if (addonAvailability.Quality == null && masterAvailability.Quality == null) { /*Do Nothing*/ }
            else if (addonAvailability.Quality == null && masterAvailability.Quality != null) { masterAvailability.Quality = addonAvailability.Quality; }
            else if (addonAvailability.Quality != null && masterAvailability.Quality == null) { masterAvailability.Quality = addonAvailability.Quality; }
            else { if (!addonAvailability.Quality.IsEquals(masterAvailability.Quality)) { masterAvailability.Quality = addonAvailability.Quality; } }

            //check PurchaseQuality
            if (addonAvailability.PurchaseQuality == null && masterAvailability.PurchaseQuality == null) { /*Do Nothing*/ }
            else if (addonAvailability.PurchaseQuality == null && masterAvailability.PurchaseQuality != null) { masterAvailability.PurchaseQuality = addonAvailability.PurchaseQuality; }
            else if (addonAvailability.PurchaseQuality != null && masterAvailability.PurchaseQuality == null) { masterAvailability.PurchaseQuality = addonAvailability.PurchaseQuality; }
            else { if (!addonAvailability.PurchaseQuality.IsEquals(masterAvailability.PurchaseQuality)) { masterAvailability.PurchaseQuality = addonAvailability.PurchaseQuality; } }

            return masterAvailability;
        }

        private List<int> CompareAndHandleSettingIds(List<int> addonSettingIds, List<int> masterSettingIds)
        {
            bool isModifiedExisting = false;
            int idToCheck = 0;
            //iterate over addon child branches
            for (int x = 0; x < addonSettingIds.Count(); x++)
            {
                isModifiedExisting = false;
                int addonSettingId = addonSettingIds[x];
                //iterate over master branches to check if we find a match
                for (int y = 0; y < masterSettingIds.Count(); y++)
                {
                    {
                        int masterQuality = masterSettingIds[y];

                        if (addonSettingId == masterQuality && !addonSettingId.Equals(masterQuality))
                        {
                            //If you match IDs here but are otherwise not equal, you are modifying an existing child object in this event,
                            //and we need to check whats changed and merge.
                            ConflictCounter++;
                            masterSettingIds[y] = addonSettingId;
                            isModifiedExisting = true;
                            break;
                        }
                        else if (addonSettingId == masterQuality)
                        {
                            isModifiedExisting = true;
                            break;
                        }
                    }
                }

                if (!isModifiedExisting)
                {
                    masterSettingIds.Add(addonSettingId);
                }
            }
            return masterSettingIds;
        }

        #endregion

        #region MERGE OBJECTS
        private Event MergeEvents(Event addonEvent, Event masterEvent)
        {
            bool matchFound;
            //master event is what gets returned

            //check Primatives
            if (addonEvent.Autofire != masterEvent.Autofire){masterEvent.Autofire = addonEvent.Autofire;}
            if (addonEvent.CanGoBack != masterEvent.CanGoBack){masterEvent.CanGoBack = addonEvent.CanGoBack;}
            if (addonEvent.Category != masterEvent.Category){masterEvent.Category = addonEvent.Category;}
            if (addonEvent.ChallengeLevel != masterEvent.ChallengeLevel){masterEvent.ChallengeLevel = addonEvent.ChallengeLevel;}
            if (addonEvent.Description != masterEvent.Description){masterEvent.Description = addonEvent.Description;}
            if (addonEvent.Distribution != masterEvent.Distribution){masterEvent.Distribution = addonEvent.Distribution;}
            if (addonEvent.ExoticEffects != masterEvent.ExoticEffects){masterEvent.ExoticEffects = addonEvent.ExoticEffects;}
            if (addonEvent.Image != masterEvent.Image){masterEvent.Image = addonEvent.Image;}
            if (addonEvent.Name != masterEvent.Name){masterEvent.Name = addonEvent.Name;}
            if (addonEvent.Ordering != masterEvent.Ordering){masterEvent.Ordering = addonEvent.Ordering;}
            if (addonEvent.ShowAsMessage != masterEvent.ShowAsMessage){masterEvent.ShowAsMessage = addonEvent.ShowAsMessage;}
            if (addonEvent.Stickiness != masterEvent.Stickiness){masterEvent.Stickiness = addonEvent.Stickiness;}
            if (addonEvent.Transient != masterEvent.Transient){masterEvent.Transient = addonEvent.Transient;}
            if (addonEvent.Urgency != masterEvent.Urgency){masterEvent.Urgency = addonEvent.Urgency;}

            //check LimitedToArea
            if (addonEvent.LimitedToArea == null && masterEvent.LimitedToArea == null) { /*Do Nothing*/ }
            else if (addonEvent.LimitedToArea == null && masterEvent.LimitedToArea != null) { masterEvent.LimitedToArea = addonEvent.LimitedToArea; }
            else if (addonEvent.LimitedToArea != null && masterEvent.LimitedToArea == null) { masterEvent.LimitedToArea = addonEvent.LimitedToArea; }
            else { if (!addonEvent.LimitedToArea.IsEquals(masterEvent.LimitedToArea)) { masterEvent.LimitedToArea = addonEvent.LimitedToArea; } }

            //Check Setting
            if (addonEvent.Setting == null && masterEvent.Setting == null) { /*Do Nothing*/ }
            else if (addonEvent.Setting == null && masterEvent.Setting != null) { masterEvent.Setting = addonEvent.Setting; }
            else if (addonEvent.Setting != null && masterEvent.Setting == null) { masterEvent.Setting = addonEvent.Setting; }
            else { if (!addonEvent.Setting.IsEquals(masterEvent.Setting)) { masterEvent.Setting = addonEvent.Setting; } }

            //check Deck
            if (addonEvent.Deck == null && masterEvent.Deck == null) { /*Do Nothing*/ }
            else if (addonEvent.Deck == null && masterEvent.Deck != null) { masterEvent.Deck = addonEvent.Deck; }
            else if (addonEvent.Deck != null && masterEvent.Deck == null) { masterEvent.Deck = addonEvent.Deck; }
            else { if (!addonEvent.Deck.IsEquals(masterEvent.Deck)) { masterEvent.Deck = addonEvent.Deck; } }

            //Check Child Branches
            if (addonEvent.ChildBranches == null && masterEvent.ChildBranches == null) { /*Do Nothing*/ }
            else if (addonEvent.ChildBranches == null && masterEvent.ChildBranches != null) { masterEvent.ChildBranches = addonEvent.ChildBranches; }
            else if (addonEvent.ChildBranches != null && masterEvent.ChildBranches == null) { masterEvent.ChildBranches = addonEvent.ChildBranches; }
            else
            {
                foreach (ChildBranches cb in addonEvent.ChildBranches)
                {
                    //check against the master list of child branches and confirm the childbranch matches in the list.
                    //If a child object is found that doesn't match exactly, the cbs are not equal and need to be handled.
                    matchFound = false;
                    foreach (ChildBranches cb2 in masterEvent.ChildBranches)
                    {
                        if (cb.IsEquals(cb2))
                        {
                            matchFound = true;
                        };
                    }
                    if (matchFound == false)
                    {
                        masterEvent.ChildBranches = CompareAndHandleChildBranches(addonEvent.ChildBranches, masterEvent.ChildBranches);
                    }
                }
            }

            //Check QualitiesAffected
            if (addonEvent.QualitiesAffected == null && masterEvent.QualitiesAffected == null) { /*Do Nothing*/ }
            else if (addonEvent.QualitiesAffected == null && masterEvent.QualitiesAffected != null) { masterEvent.QualitiesAffected = addonEvent.QualitiesAffected; }
            else if (addonEvent.QualitiesAffected != null && masterEvent.QualitiesAffected == null) { masterEvent.QualitiesAffected = addonEvent.QualitiesAffected; }
            else
            {
                foreach (QualitiesAffected qa in addonEvent.QualitiesAffected)
                {
                    //check against the master list of child branches and confirm the childbranch matches in the list.
                    //If a child object is found that doesn't match exactly, the events are not equal.
                    matchFound = false;
                    foreach (QualitiesAffected qa2 in masterEvent.QualitiesAffected)
                    {
                        if (qa.IsEquals(qa2))
                        {
                            matchFound = true;
                        };
                    }
                    if (matchFound == false)
                    {
                        masterEvent.QualitiesAffected = CompareAndHandleQualitiesAffected(addonEvent.QualitiesAffected, masterEvent.QualitiesAffected);
                    }
                }
            }

            //Check QualitiesRequired
            if (addonEvent.QualitiesAffected == null && masterEvent.QualitiesAffected == null) { /*Do Nothing*/ }
            else if (addonEvent.QualitiesAffected == null && masterEvent.QualitiesAffected != null) { masterEvent.QualitiesAffected = addonEvent.QualitiesAffected; }
            else if (addonEvent.QualitiesAffected != null && masterEvent.QualitiesAffected == null) { masterEvent.QualitiesAffected = addonEvent.QualitiesAffected; }
            else
            {
                foreach (QualitiesRequired qr in addonEvent.QualitiesRequired)
                {
                    //check against the master list of child branches and confirm the childbranch matches in the list.
                    //If a child object is found that doesn't match exactly, the events are not equal.
                    matchFound = false;
                    foreach (QualitiesRequired qr2 in masterEvent.QualitiesRequired)
                    {
                        if (qr.IsEquals(qr2))
                        {
                            matchFound = true;
                        };
                    }
                    if (matchFound == false)
                    {
                        masterEvent.QualitiesRequired = CompareAndHandleQualitiesRequired(addonEvent.QualitiesRequired, masterEvent.QualitiesRequired);
                    }
                }
            }

            return masterEvent;
        }
        private Quality MergeQualities(Quality addonQuality, Quality masterQuality)
        {
            bool matchFound;
            //Merge Primitives
            if (addonQuality.RelationshipCapable != masterQuality.RelationshipCapable) masterQuality.RelationshipCapable = addonQuality.RelationshipCapable;
            if (addonQuality.OwnerName != masterQuality.OwnerName) masterQuality.OwnerName = addonQuality.OwnerName;
            if (addonQuality.Description != masterQuality.Description) masterQuality.Description = addonQuality.Description;
            if (addonQuality.Image != masterQuality.Image) masterQuality.Image = addonQuality.Image;
            if (addonQuality.Notes != masterQuality.Notes) masterQuality.Notes = addonQuality.Notes;
            if (addonQuality.Tag != masterQuality.Tag) masterQuality.Tag = addonQuality.Tag;
            if (addonQuality.Cap != masterQuality.Cap) masterQuality.Cap = addonQuality.Cap;
            if (addonQuality.HimbleLevel != masterQuality.HimbleLevel) masterQuality.HimbleLevel = addonQuality.HimbleLevel;
            if (addonQuality.UsePyramidNumbers != masterQuality.UsePyramidNumbers) masterQuality.UsePyramidNumbers = addonQuality.UsePyramidNumbers;
            if (addonQuality.PyramidNumberIncreaseLimit != masterQuality.PyramidNumberIncreaseLimit)
                masterQuality.PyramidNumberIncreaseLimit = addonQuality.PyramidNumberIncreaseLimit;
            if (addonQuality.AvailableAt != masterQuality.AvailableAt) masterQuality.AvailableAt = addonQuality.AvailableAt;
            if (addonQuality.PreventNaming != masterQuality.PreventNaming) masterQuality.PreventNaming = addonQuality.PreventNaming;
            if (addonQuality.CssClasses != masterQuality.CssClasses) masterQuality.CssClasses = addonQuality.CssClasses;
            if (addonQuality.World != masterQuality.World) masterQuality.World = addonQuality.World;
            if (addonQuality.Ordering != masterQuality.Ordering) masterQuality.Ordering = addonQuality.Ordering;
            if (addonQuality.IsSlot != masterQuality.IsSlot) masterQuality.IsSlot = addonQuality.IsSlot;
            if (addonQuality.Persistent != masterQuality.Persistent) masterQuality.Persistent = addonQuality.Persistent;
            if (addonQuality.Visible != masterQuality.Visible) masterQuality.Visible = addonQuality.Visible;
            if (addonQuality.EnhancementsDescription != masterQuality.EnhancementsDescription)
                masterQuality.EnhancementsDescription = addonQuality.EnhancementsDescription;
            if (addonQuality.AllowsSecondChancesOnChallengesForQuality != masterQuality.AllowsSecondChancesOnChallengesForQuality)
                masterQuality.AllowsSecondChancesOnChallengesForQuality = addonQuality.AllowsSecondChancesOnChallengesForQuality;
            if (addonQuality.GivesTrophy != masterQuality.GivesTrophy) masterQuality.GivesTrophy = addonQuality.GivesTrophy;
            if (addonQuality.DifficultyTestType != masterQuality.DifficultyTestType) masterQuality.DifficultyTestType = addonQuality.DifficultyTestType;
            if (addonQuality.DifficultyScaler != masterQuality.DifficultyScaler) masterQuality.DifficultyScaler = addonQuality.DifficultyScaler;
            if (addonQuality.AllowedOn != masterQuality.AllowedOn) masterQuality.AllowedOn = addonQuality.AllowedOn;
            if (addonQuality.Category != masterQuality.Category) masterQuality.Category = addonQuality.Category;
            if (addonQuality.LevelDescriptionText != masterQuality.LevelDescriptionText) masterQuality.LevelDescriptionText = addonQuality.LevelDescriptionText;
            if (addonQuality.ChangeDescriptionText != masterQuality.ChangeDescriptionText) masterQuality.ChangeDescriptionText = addonQuality.ChangeDescriptionText;
            if (addonQuality.LevelImageText != masterQuality.LevelImageText) masterQuality.LevelImageText = addonQuality.LevelImageText;
            if (addonQuality.Name != masterQuality.Name) masterQuality.Name = addonQuality.Name;

            //Merge LimitedToArea
            if (addonQuality.LimitedToArea == null && masterQuality.LimitedToArea == null) { /*Do Nothing*/ }
            else if (addonQuality.LimitedToArea == null && masterQuality.LimitedToArea != null) { masterQuality.LimitedToArea = addonQuality.LimitedToArea; }
            else if (addonQuality.LimitedToArea != null && masterQuality.LimitedToArea == null) { masterQuality.LimitedToArea = addonQuality.LimitedToArea; }
            else { if (!addonQuality.LimitedToArea.IsEquals(masterQuality.LimitedToArea)) { masterQuality.LimitedToArea = addonQuality.LimitedToArea; } }

            //Merge AssignToSlot
            if (addonQuality.AssignToSlot == null && masterQuality.AssignToSlot == null) { /*Do Nothing*/ }
            else if (addonQuality.AssignToSlot == null && masterQuality.AssignToSlot != null) { masterQuality.AssignToSlot = addonQuality.AssignToSlot; }
            else if (addonQuality.AssignToSlot != null && masterQuality.AssignToSlot == null) { masterQuality.AssignToSlot = addonQuality.AssignToSlot; }
            else { if (!addonQuality.AssignToSlot.IsEquals(masterQuality.AssignToSlot)) { masterQuality.AssignToSlot = addonQuality.AssignToSlot; } }

            //Merge Use Event
            if (addonQuality.UseEvent == null && masterQuality.UseEvent == null) { /*Do Nothing*/ }
            else if (addonQuality.UseEvent == null && masterQuality.UseEvent != null) { masterQuality.UseEvent = addonQuality.UseEvent; }
            else if (addonQuality.UseEvent != null && masterQuality.UseEvent == null) { masterQuality.UseEvent = addonQuality.UseEvent; }
            else { if (!addonQuality.UseEvent.IsEquals(masterQuality.UseEvent)) { masterQuality.UseEvent = addonQuality.UseEvent; } }

            //Merge QualitiesWhichAllowSecondChanceOnThis
            if (addonQuality.QualitiesWhichAllowSecondChanceOnThis == null && masterQuality.QualitiesWhichAllowSecondChanceOnThis == null) { /*do nothing*/ }
            else if (addonQuality.QualitiesWhichAllowSecondChanceOnThis == null && masterQuality.QualitiesWhichAllowSecondChanceOnThis != null)
            { masterQuality.QualitiesWhichAllowSecondChanceOnThis = addonQuality.QualitiesWhichAllowSecondChanceOnThis; }
            else if (addonQuality.QualitiesWhichAllowSecondChanceOnThis != null && masterQuality.QualitiesWhichAllowSecondChanceOnThis == null)
            { masterQuality.QualitiesWhichAllowSecondChanceOnThis = addonQuality.QualitiesWhichAllowSecondChanceOnThis; }
            else
            {
                foreach (Quality quality in addonQuality.QualitiesWhichAllowSecondChanceOnThis)
                {
                    //check against the master list of qualities and confirm the quality matches in the list.
                    //If a quality is found that doesn't match exactly, the AssignToSlots are not equal.
                    matchFound = false;
                    foreach (Quality quality2 in masterQuality.QualitiesWhichAllowSecondChanceOnThis)
                    {
                        if (quality.IsEquals(quality2))
                        {
                            matchFound = true;
                        };
                    }
                    if (matchFound == false)
                    {
                        masterQuality.QualitiesWhichAllowSecondChanceOnThis =
                            CompareAndHandleQualityLists(addonQuality.QualitiesWhichAllowSecondChanceOnThis, masterQuality.QualitiesWhichAllowSecondChanceOnThis);
                    }
                }
            }

            //Merge Enhancements
            if (addonQuality.Enhancements == null && masterQuality.Enhancements == null) { /*do nothing*/ }
            else if (addonQuality.Enhancements == null && masterQuality.Enhancements != null) { masterQuality.Enhancements = addonQuality.Enhancements; }
            else if (addonQuality.Enhancements != null && masterQuality.Enhancements == null) { masterQuality.Enhancements = addonQuality.Enhancements; }
            else
            {
                foreach (Enhancement enchn in addonQuality.Enhancements)
                {
                    //Check against the master list of Enhancements and confirm the childbranch Enhancements are in the list.
                    //If an Enhancement is found that doesn't match exactly, the events are not equal.
                    matchFound = false;
                    foreach (Enhancement enchan2 in masterQuality.Enhancements)
                    {
                        if (enchn.IsEquals(enchan2))
                        {
                            matchFound = true;
                        };
                    }
                    
                    if (matchFound == false) masterQuality.Enhancements = CompareAndHandleEnhancements(addonQuality.Enhancements, masterQuality.Enhancements);
                }
            }            

            return masterQuality;
        }
        private Enhancement MergeEnhancements(Enhancement addonEnhancement, Enhancement masterEnhancement)
        {
            if (addonEnhancement.Level != masterEnhancement.Level) masterEnhancement.Level = addonEnhancement.Level;
            if (addonEnhancement.AssociatedQualityId != masterEnhancement.AssociatedQualityId) masterEnhancement.AssociatedQualityId = addonEnhancement.AssociatedQualityId;
            if (addonEnhancement.QualityName != masterEnhancement.QualityName) masterEnhancement.QualityName = addonEnhancement.QualityName;
            if (addonEnhancement.QualityDescription != masterEnhancement.QualityDescription) masterEnhancement.QualityDescription = addonEnhancement.QualityDescription;
            if (addonEnhancement.QualityImage != masterEnhancement.QualityImage) masterEnhancement.QualityImage = addonEnhancement.QualityImage;
            if (addonEnhancement.QualityCategory != masterEnhancement.QualityCategory) masterEnhancement.QualityCategory = addonEnhancement.QualityCategory;
            if (addonEnhancement.QualityAllowedOn != masterEnhancement.QualityAllowedOn) masterEnhancement.QualityAllowedOn = addonEnhancement.QualityAllowedOn;

            //Check AssociatedQuality
            if (addonEnhancement.AssociatedQuality == null && masterEnhancement.AssociatedQuality == null) { /*Do Nothing*/ }
            else if (addonEnhancement.AssociatedQuality == null && masterEnhancement.AssociatedQuality != null) { masterEnhancement.AssociatedQuality = addonEnhancement.AssociatedQuality; }
            else if (addonEnhancement.AssociatedQuality != null && masterEnhancement.AssociatedQuality == null) { masterEnhancement.AssociatedQuality = addonEnhancement.AssociatedQuality; }
            else { if (!addonEnhancement.AssociatedQuality.IsEquals(masterEnhancement.AssociatedQuality)) { masterEnhancement.AssociatedQuality = addonEnhancement.AssociatedQuality; } }

            return masterEnhancement;
        }
        private ChildBranches MergeChildBranches(ChildBranches addonChildBranch, ChildBranches masterChildBranch)
        {
            bool matchFound;
            if (addonChildBranch.ActionCost != masterChildBranch.ActionCost)
            {
                masterChildBranch.ActionCost = addonChildBranch.ActionCost;
            }

            if (addonChildBranch.ButtonText != masterChildBranch.ButtonText)
            {
                masterChildBranch.ButtonText = addonChildBranch.ButtonText;
            }

            //Check DefaultEvent
            if (addonChildBranch.DefaultEvent == null && masterChildBranch.DefaultEvent == null) { /*Do Nothing*/ }
            else if (addonChildBranch.DefaultEvent == null && masterChildBranch.DefaultEvent != null) { masterChildBranch.DefaultEvent = addonChildBranch.DefaultEvent; }
            else if (addonChildBranch.DefaultEvent != null && masterChildBranch.DefaultEvent == null) { masterChildBranch.DefaultEvent = addonChildBranch.DefaultEvent; }
            else { if (!addonChildBranch.DefaultEvent.IsEquals(masterChildBranch.DefaultEvent)) { masterChildBranch.DefaultEvent = addonChildBranch.DefaultEvent; } }

            if (addonChildBranch.Description != masterChildBranch.Description)
            {
                masterChildBranch.Description = addonChildBranch.Description;
            }

            if(addonChildBranch.Image != masterChildBranch.Image)
            {
                masterChildBranch.Image = addonChildBranch.Image;
            }

            if(addonChildBranch.Name != masterChildBranch.Name)
            {
                masterChildBranch.Name = addonChildBranch.Name;
            }

            if(addonChildBranch.Ordering != masterChildBranch.Ordering)
            {
                masterChildBranch.Ordering = addonChildBranch.Ordering;
            }

            //Check ParentEvent
            if (addonChildBranch.ParentEvent == null && masterChildBranch.ParentEvent == null) { /*Do Nothing*/ }
            else if (addonChildBranch.ParentEvent == null && masterChildBranch.ParentEvent != null) { masterChildBranch.ParentEvent = addonChildBranch.ParentEvent; }
            else if (addonChildBranch.ParentEvent != null && masterChildBranch.ParentEvent == null) { masterChildBranch.ParentEvent = addonChildBranch.ParentEvent; }
            else { if (!addonChildBranch.ParentEvent.IsEquals(masterChildBranch.ParentEvent)) { masterChildBranch.ParentEvent = addonChildBranch.ParentEvent; } }


            //Check QualitiesRequired
            if (addonChildBranch.QualitiesRequired == null && masterChildBranch.QualitiesRequired == null) { /*Do Nothing*/ }
            else if (addonChildBranch.QualitiesRequired == null && masterChildBranch.QualitiesRequired != null) { masterChildBranch.QualitiesRequired = addonChildBranch.QualitiesRequired; }
            else if (addonChildBranch.QualitiesRequired != null && masterChildBranch.QualitiesRequired == null) { masterChildBranch.QualitiesRequired = addonChildBranch.QualitiesRequired; }
            else
            {
                foreach (QualitiesRequired qr in addonChildBranch.QualitiesRequired)
                {
                    //check against the master list of child branches and confirm the childbranch matches in the list.
                    //If a child object is found that doesn't match exactly, the events are not equal.
                    matchFound = false;
                    foreach (QualitiesRequired qr2 in masterChildBranch.QualitiesRequired)
                    {
                        if (qr.IsEquals(qr2))
                        {
                            matchFound = true;
                        };
                    }
                    if (matchFound == false)
                    {
                        masterChildBranch.QualitiesRequired = CompareAndHandleQualitiesRequired(addonChildBranch.QualitiesRequired, masterChildBranch.QualitiesRequired);
                    }
                }
            }

            //Check RareDefaultEvent
            if (addonChildBranch.RareDefaultEvent == null && masterChildBranch.RareDefaultEvent == null) { /*Do Nothing*/ }
            else if (addonChildBranch.RareDefaultEvent == null && masterChildBranch.RareDefaultEvent != null) { masterChildBranch.RareDefaultEvent = addonChildBranch.RareDefaultEvent; }
            else if (addonChildBranch.RareDefaultEvent != null && masterChildBranch.RareDefaultEvent == null) { masterChildBranch.RareDefaultEvent = addonChildBranch.RareDefaultEvent; }
            else { if (!addonChildBranch.RareDefaultEvent.IsEquals(masterChildBranch.RareDefaultEvent)) { masterChildBranch.RareDefaultEvent = addonChildBranch.RareDefaultEvent; } }

            if (addonChildBranch.RareDefaultEventChance != masterChildBranch.RareDefaultEventChance){masterChildBranch.RareDefaultEventChance = addonChildBranch.RareDefaultEventChance;}

            //Check RareSuccessEvent
            if (addonChildBranch.RareSuccessEvent == null && masterChildBranch.RareSuccessEvent == null) { /*Do Nothing*/ }
            else if (addonChildBranch.RareSuccessEvent == null && masterChildBranch.RareSuccessEvent != null) { masterChildBranch.RareSuccessEvent = addonChildBranch.RareSuccessEvent; }
            else if (addonChildBranch.RareSuccessEvent != null && masterChildBranch.RareSuccessEvent == null) { masterChildBranch.RareSuccessEvent = addonChildBranch.RareSuccessEvent; }
            else { if (!addonChildBranch.RareSuccessEvent.IsEquals(masterChildBranch.RareSuccessEvent)) { masterChildBranch.RareSuccessEvent = addonChildBranch.RareSuccessEvent; } }

            if (addonChildBranch.RareSuccessEventChance != masterChildBranch.RareSuccessEventChance){masterChildBranch.RareSuccessEventChance = addonChildBranch.RareSuccessEventChance;}

            //Check SuccessEvent
            if (addonChildBranch.SuccessEvent == null && masterChildBranch.SuccessEvent == null) { /*Do Nothing*/ }
            else if (addonChildBranch.SuccessEvent == null && masterChildBranch.SuccessEvent != null) { masterChildBranch.SuccessEvent = addonChildBranch.SuccessEvent; }
            else if (addonChildBranch.SuccessEvent != null && masterChildBranch.SuccessEvent == null) { masterChildBranch.SuccessEvent = addonChildBranch.SuccessEvent; }
            else { if (!addonChildBranch.SuccessEvent.IsEquals(masterChildBranch.SuccessEvent)) { masterChildBranch.SuccessEvent = addonChildBranch.SuccessEvent; } }

            return masterChildBranch;
        }
        private QualitiesRequired MergeQualitiesRequired(QualitiesRequired addonQualitiesRequired, QualitiesRequired masterQualitiesRequired)
        {
            if (addonQualitiesRequired.MinLevel != masterQualitiesRequired.MinLevel) { masterQualitiesRequired.MinLevel = addonQualitiesRequired.MinLevel; }
            if (addonQualitiesRequired.MaxLevel != masterQualitiesRequired.MaxLevel) { masterQualitiesRequired.MaxLevel = addonQualitiesRequired.MaxLevel; }

            //Check AssociatedQuality
            if (addonQualitiesRequired.AssociatedQuality == null && masterQualitiesRequired.AssociatedQuality == null) { /*Do Nothing*/ }
            else if (addonQualitiesRequired.AssociatedQuality == null && masterQualitiesRequired.AssociatedQuality != null) { masterQualitiesRequired.AssociatedQuality = addonQualitiesRequired.AssociatedQuality; }
            else if (addonQualitiesRequired.AssociatedQuality != null && masterQualitiesRequired.AssociatedQuality == null) { masterQualitiesRequired.AssociatedQuality = addonQualitiesRequired.AssociatedQuality; }
            else { if (!addonQualitiesRequired.AssociatedQuality.IsEquals(masterQualitiesRequired.AssociatedQuality)) { masterQualitiesRequired.AssociatedQuality = addonQualitiesRequired.AssociatedQuality; } }

            if (addonQualitiesRequired.DifficultyLevel != masterQualitiesRequired.DifficultyLevel) { masterQualitiesRequired.DifficultyLevel = addonQualitiesRequired.DifficultyLevel; }
            if (addonQualitiesRequired.VisibleWhenRequirementFailed != masterQualitiesRequired.VisibleWhenRequirementFailed) { masterQualitiesRequired.VisibleWhenRequirementFailed = addonQualitiesRequired.VisibleWhenRequirementFailed; }
            if (addonQualitiesRequired.BranchVisibleWhenRequirementFailed != masterQualitiesRequired.BranchVisibleWhenRequirementFailed) { masterQualitiesRequired.BranchVisibleWhenRequirementFailed = addonQualitiesRequired.BranchVisibleWhenRequirementFailed; }
            if (addonQualitiesRequired.DifficultyAdvanced != masterQualitiesRequired.DifficultyAdvanced) { masterQualitiesRequired.DifficultyAdvanced = addonQualitiesRequired.DifficultyAdvanced; }
            if (addonQualitiesRequired.MaxAdvanced != masterQualitiesRequired.MaxAdvanced) { masterQualitiesRequired.MaxAdvanced = addonQualitiesRequired.MaxAdvanced; }
            if (addonQualitiesRequired.MinAdvanced != masterQualitiesRequired.MinAdvanced) { masterQualitiesRequired.MinAdvanced = addonQualitiesRequired.MinAdvanced; }


            return masterQualitiesRequired;
        }
        private QualitiesAffected MergeQualitiesAffected(QualitiesAffected addonQualitiesAffected, QualitiesAffected masterQualitiesAffected)
        {
            if (addonQualitiesAffected.Level != masterQualitiesAffected.Level) masterQualitiesAffected.Level = addonQualitiesAffected.Level;
            if (addonQualitiesAffected.AssociatedQuality != masterQualitiesAffected.AssociatedQuality) masterQualitiesAffected.AssociatedQuality = addonQualitiesAffected.AssociatedQuality;
            if (addonQualitiesAffected.Priority != masterQualitiesAffected.Priority) masterQualitiesAffected.Priority = addonQualitiesAffected.Priority;
            if (addonQualitiesAffected.SetToExactly != masterQualitiesAffected.SetToExactly) masterQualitiesAffected.SetToExactly = addonQualitiesAffected.SetToExactly;
            if (addonQualitiesAffected.ForceEquip != masterQualitiesAffected.ForceEquip) masterQualitiesAffected.ForceEquip = addonQualitiesAffected.ForceEquip;
            if (addonQualitiesAffected.OnlyIfNoMoreThan != masterQualitiesAffected.OnlyIfNoMoreThan) masterQualitiesAffected.OnlyIfNoMoreThan = addonQualitiesAffected.OnlyIfNoMoreThan;
            if (addonQualitiesAffected.ChangeByAdvanced != masterQualitiesAffected.ChangeByAdvanced) masterQualitiesAffected.ChangeByAdvanced = addonQualitiesAffected.ChangeByAdvanced;
            if (addonQualitiesAffected.OnlyIfAtLeast != masterQualitiesAffected.OnlyIfAtLeast) masterQualitiesAffected.OnlyIfAtLeast = addonQualitiesAffected.OnlyIfAtLeast;

            return masterQualitiesAffected;
        }
        private Personae MergePersonas(Personae addonPersona, Personae masterPersona)
        {
            bool matchFound;
            if (addonPersona.Description != masterPersona.Description) { masterPersona.Description = addonPersona.Description; }
            if (addonPersona.OwnerName != masterPersona.OwnerName) { masterPersona.Description = addonPersona.Description; }
            if (addonPersona.Name != masterPersona.Name) { masterPersona.Description = addonPersona.Description; }

            //Check Setting
            if (addonPersona.Setting == null && masterPersona.Setting == null) { /*Do Nothing*/ }
            else if (addonPersona.Setting == null && masterPersona.Setting != null) { masterPersona.Description = addonPersona.Description; }
            else if (addonPersona.Setting != null && masterPersona.Setting == null) { masterPersona.Description = addonPersona.Description; }
            else { if (!addonPersona.Setting.IsEquals(masterPersona.Setting)) { masterPersona.Description = addonPersona.Description; } }

            //Check DateTimeCreated
            if (addonPersona.DateTimeCreated == null && masterPersona.DateTimeCreated == null) { /*Do Nothing*/ }
            else if (addonPersona.DateTimeCreated == null && masterPersona.DateTimeCreated != null) { masterPersona.Description = addonPersona.Description; }
            else if (addonPersona.DateTimeCreated != null && masterPersona.DateTimeCreated == null) { masterPersona.Description = addonPersona.Description; }
            else { if (!addonPersona.DateTimeCreated.Equals(masterPersona.DateTimeCreated)) { masterPersona.Description = addonPersona.Description; } }

            //Check QualitiesAffected
            if (addonPersona.QualitiesAffected == null && masterPersona.QualitiesAffected == null) { /*do nothing*/ }
            else if (addonPersona.QualitiesAffected == null && masterPersona.QualitiesAffected != null) { masterPersona.Description = addonPersona.Description; }
            else if (addonPersona.QualitiesAffected != null && masterPersona.QualitiesAffected == null) { masterPersona.Description = addonPersona.Description; }
            else
            {
                foreach (QualitiesAffected qa in addonPersona.QualitiesAffected)
                {
                    //check against the master list of child branches and confirm the childbranch matches in the list.
                    //If a child object is found that doesn't match exactly, the events are not equal.
                    matchFound = false;
                    foreach (QualitiesAffected qa2 in masterPersona.QualitiesAffected)
                    {
                        if (qa.IsEquals(qa2))
                        {
                            matchFound = true;
                            break;
                        };
                    }
                    if (matchFound == false) masterPersona.QualitiesAffected = CompareAndHandleQualitiesAffected(addonPersona.QualitiesAffected, masterPersona.QualitiesAffected);
                }
            }

            //Check QualitiesRequired
            if (addonPersona.QualitiesRequired == null && masterPersona.QualitiesRequired == null) { /*do nothing*/ }
            else if (addonPersona.QualitiesRequired == null && masterPersona.QualitiesRequired != null) { masterPersona.QualitiesRequired = addonPersona.QualitiesRequired; }
            else if (addonPersona.QualitiesRequired != null && masterPersona.QualitiesRequired == null) { masterPersona.QualitiesRequired = addonPersona.QualitiesRequired; }
            else
            {
                foreach (QualitiesRequired qr in addonPersona.QualitiesRequired)
                {
                    //check against the master list of child branches and confirm the childbranch matches in the list.
                    //If a child object is found that doesn't match exactly, the events are not equal.
                    matchFound = false;
                    foreach (QualitiesRequired qr2 in masterPersona.QualitiesRequired)
                    {
                        if (qr.IsEquals(qr2))
                        {
                            matchFound = true;
                        };
                    }
                    if (matchFound == false) masterPersona.QualitiesRequired = CompareAndHandleQualitiesRequired(addonPersona.QualitiesRequired, masterPersona.QualitiesRequired);
                }
            }

            return masterPersona;
        }
        private Area MergeAreas(Area addonArea, Area masterArea)
        {
            if (addonArea.Name != masterArea.Name) masterArea.Name = addonArea.Name;
            if (addonArea.Description != masterArea.Description) masterArea.Description = addonArea.Description;
            if (addonArea.ImageName != masterArea.ImageName) masterArea.ImageName = addonArea.ImageName;
            if (addonArea.MarketAccessPermitted != masterArea.MarketAccessPermitted) masterArea.MarketAccessPermitted = addonArea.MarketAccessPermitted;
            if (addonArea.MoveMessage != masterArea.MoveMessage) masterArea.MoveMessage = addonArea.MoveMessage;
            if (addonArea.HideName != masterArea.HideName) masterArea.HideName = addonArea.HideName;
            if (addonArea.RandomPostcard != masterArea.RandomPostcard) masterArea.RandomPostcard = addonArea.RandomPostcard;
            if (addonArea.MapX != masterArea.MapX) masterArea.MapX = addonArea.MapX;
            if (addonArea.MapY != masterArea.MapY) masterArea.MapY = addonArea.MapY;
            if (addonArea.ShowOps != masterArea.ShowOps) masterArea.ShowOps = addonArea.ShowOps;
            if (addonArea.PremiumSubRequired != masterArea.PremiumSubRequired) masterArea.PremiumSubRequired = addonArea.PremiumSubRequired;
            if (addonArea.Id != masterArea.Id) masterArea.Id = addonArea.Id;

            //check UnlocksWithQuality
            if (addonArea.UnlocksWithQuality == null && masterArea.UnlocksWithQuality == null) { /*Do Nothing*/ }
            else if (addonArea.UnlocksWithQuality == null && masterArea.UnlocksWithQuality != null) { masterArea.UnlocksWithQuality = addonArea.UnlocksWithQuality; }
            else if (addonArea.UnlocksWithQuality != null && masterArea.UnlocksWithQuality == null) { masterArea.UnlocksWithQuality = addonArea.UnlocksWithQuality; }
            else { if (!addonArea.UnlocksWithQuality.IsEquals(masterArea.UnlocksWithQuality)) { masterArea.UnlocksWithQuality = addonArea.UnlocksWithQuality; } }

            return masterArea;
        }
        private Exchange MergeExchanges(Exchange addonExchange, Exchange masterExchange)
        {
            bool matchFound;
            if (addonExchange.Name != masterExchange.Name) { masterExchange.Name = addonExchange.Name; }
            if (addonExchange.Title != masterExchange.Title) { masterExchange.Title = addonExchange.Title; }
            if (addonExchange.Image != masterExchange.Image) { masterExchange.Image = addonExchange.Image; }
            if (addonExchange.Description != masterExchange.Description) { masterExchange.Description = addonExchange.Description; }

            //Check SettingIds
            if (addonExchange.SettingIds == null && masterExchange.SettingIds == null) { /*Do nothing*/ }
            else if (addonExchange.SettingIds == null && masterExchange.SettingIds != null) { masterExchange.SettingIds = addonExchange.SettingIds; }
            else if (addonExchange.SettingIds != null && masterExchange.SettingIds == null) { masterExchange.SettingIds = addonExchange.SettingIds; }
            else
            {
                //For each child branch required from this addon event
                foreach (int settingId in addonExchange.SettingIds)
                {
                    //check against the master list of child branches and confirm the childbranch matches in the list.
                    //If a child object is found that doesn't match exactly, the events are not equal.
                    matchFound = false;
                    foreach (int settingId2 in masterExchange.SettingIds)
                    {
                        if (settingId.Equals(settingId2))
                        {
                            matchFound = true;
                            break;
                        };
                    }
                    if (matchFound == false) masterExchange.SettingIds = CompareAndHandleSettingIds(addonExchange.SettingIds, masterExchange.SettingIds);
                }
            }

            //Check Shops
            if (addonExchange.Shops == null && masterExchange.Shops == null) { /*Do nothing*/ }
            else if (addonExchange.Shops == null && masterExchange.Shops != null) { masterExchange.Shops = addonExchange.Shops; }
            else if (addonExchange.Shops != null && masterExchange.Shops == null) { masterExchange.Shops = addonExchange.Shops; }
            else
            {
                //For each child branch required from this addon event
                foreach (Shop shop in addonExchange.Shops)
                {
                    //check against the master list of child branches and confirm the childbranch matches in the list.
                    //If a child object is found that doesn't match exactly, the events are not equal.
                    matchFound = false;
                    foreach (Shop shop2 in masterExchange.Shops)
                    {
                        if (shop.IsEquals(shop2))
                        {
                            matchFound = true;
                            break;
                        };
                    }//TODO: CompareAndHandleShops
                    if (matchFound == false) masterExchange.Shops = CompareAndHandleShops(addonExchange.Shops, masterExchange.Shops);
                }
            }

            return masterExchange;
        }
        private Shop MergeShops(Shop addonShop, Shop masterShop)
        {
            bool matchFound;
            if (addonShop.Name != masterShop.Name) { masterShop.Name = addonShop.Name; }
            if (addonShop.Image != masterShop.Image) { masterShop.Image = addonShop.Image; }
            if (addonShop.Description != masterShop.Description) { masterShop.Description = addonShop.Description; }
            if (addonShop.Ordering != masterShop.Ordering) { masterShop.Ordering = addonShop.Ordering; }

            //Check QualitiesRequired
            if (addonShop.QualitiesRequired == null && masterShop.QualitiesRequired == null) {  }
            else if (addonShop.QualitiesRequired == null && masterShop.QualitiesRequired != null) { masterShop.QualitiesRequired = addonShop.QualitiesRequired; }
            else if (addonShop.QualitiesRequired != null && masterShop.QualitiesRequired == null) { masterShop.QualitiesRequired = addonShop.QualitiesRequired; }
            else
            {
                foreach (QualitiesRequired qr in addonShop.QualitiesRequired)
                {
                    //check against the master list of child branches and confirm the childbranch matches in the list.
                    //If a child object is found that doesn't match exactly, the events are not equal.
                    matchFound = false;
                    foreach (QualitiesRequired qr2 in masterShop.QualitiesRequired)
                    {
                        if (qr.IsEquals(qr2))
                        {
                            matchFound = true;
                            break;
                        };
                    }
                    if (matchFound == false) masterShop.QualitiesRequired = CompareAndHandleQualitiesRequired(addonShop.QualitiesRequired, masterShop.QualitiesRequired);
                }
            }

            //Check Availabilities
            if (addonShop.Availabilities == null && masterShop.Availabilities == null) {  }
            else if (addonShop.Availabilities == null && masterShop.Availabilities != null) { masterShop.Name = addonShop.Name; }
            else if (addonShop.Availabilities != null && masterShop.Availabilities == null) { masterShop.Name = addonShop.Name; }
            else
            {
                foreach (Availability av in addonShop.Availabilities)
                {
                    //check against the master list of child branches and confirm the childbranch matches in the list.
                    //If a child object is found that doesn't match exactly, the events are not equal.
                    matchFound = false;
                    foreach (Availability av2 in masterShop.Availabilities)
                    {
                        if (av.IsEquals(av2))
                        {
                            matchFound = true;
                            break;
                        };
                    }
                    if (matchFound == false) masterShop.Availabilities = CompareAndHandleAvailabilities(addonShop.Availabilities, masterShop.Availabilities);
                }
            }

            return masterShop;
        }

        #endregion

        #region WRITE DATA

        public void WriteEntities(string entitiesOutputPath, List<Area> areaList, List<Event> eventList,
           List<Exchange> exchangeList, List<Personae> personaList, List<Quality> qualityList)
        {
            var areaOutputFilePath = entitiesOutputPath + "\\areas.json";
            var eventOutputFilePath = entitiesOutputPath + "\\events.json";
            var exchangeOutputFilePath = entitiesOutputPath + "\\exchanges.json";
            var personaOutputFilePath = entitiesOutputPath + "\\personas.json";
            var qualityOutputFilePath = entitiesOutputPath + "\\qualities.json";

            //Write Areas
            string areaOutput = JsonConvert.SerializeObject(areaList, Formatting.Indented);
            File.Create(areaOutputFilePath).Close();
            File.WriteAllText(areaOutputFilePath, areaOutput);

            //Write Events
            string eventOutput = JsonConvert.SerializeObject(eventList, Formatting.Indented);
            File.Create(eventOutputFilePath).Close();
            File.WriteAllText(eventOutputFilePath, eventOutput);

            //Write Exchanges
            string exchangeOutput = JsonConvert.SerializeObject(exchangeList, Formatting.Indented);
            File.Create(exchangeOutputFilePath).Close();
            File.WriteAllText(exchangeOutputFilePath, exchangeOutput);

            //Write Personae
            string personaeOutput = JsonConvert.SerializeObject(personaList, Formatting.Indented);
            File.Create(personaOutputFilePath).Close();
            File.WriteAllText(personaOutputFilePath, personaeOutput);

            //Write Quality
            string qualityOutput = JsonConvert.SerializeObject(qualityList, Formatting.Indented);
            File.Create(qualityOutputFilePath).Close();
            File.WriteAllText(qualityOutputFilePath, qualityOutput);
        }









        #endregion
    }
}



