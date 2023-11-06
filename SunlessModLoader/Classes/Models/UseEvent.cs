using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SunlessModLoader.Classes.Classes
{
    public class UseEvent
    {
        public List<ChildBranches>? ChildBranches { get; set; }
        public string? ParentBranch { get; set; }
        public List<QualitiesAffected>? QualitiesAffected { get; set; }
        public List<QualitiesRequired>? QualitiesRequired { get; set; }
        public string? Image { get; set; }
        public string? Description { get; set; }
        public string? Tag { get; set; }
        public string? ExoticEffects { get; set; }
        public string? Note { get; set; }
        public int ChallengeLevel { get; set; }
        public string? UnclearedEditAt { get; set; }
        public string? LastEditedBy { get; set; }
        public double? Ordering { get; set; }
        public bool? ShowAsMessage { get; set; }
        public string? LivingStory { get; set; }
        public LinkToEvent? LinkToEvent { get; set; }
        public Deck? Deck { get; set; }
        public string? Category { get; set; }
        public LimitedToArea? LimitedToArea { get; set; }
        public string? World { get; set; }
        public string? Transient { get; set; }
        public int? Stickiness { get; set; }
        public int? MoveToAreaId { get; set; }
        public MoveToArea? MoveToArea { get; set; }
        public string? MoveToDomicile { get; set; }
        public SwitchToSetting? SwitchToSetting { get; set; }
        public int? FatePointsChange { get; set; }
        public int? BootyValue { get; set; }
        public string? LogInJournalAgainstQuality { get; set; }
        public Setting? Setting { get; set; }
        public string? Urgency { get; set; }
        public string? Teaser { get; set; }
        public string? OwnerName { get; set; }
        public DateTime? DateTimeCreated { get; set; }
        public int? Distribution { get; set; }
        public bool? Autofire { get; set; }
        public bool? CanGoBack { get; set; }
        public string? Name { get; set; }
        public int Id { get; set; }

        public bool IsEquals(UseEvent useEvent)
        {
            if (ReferenceEquals(useEvent, null) && ReferenceEquals(this, null)) { return true; }
            //if one is null, and the other is not, return false immediately
            if (ReferenceEquals(useEvent, null) && !ReferenceEquals(this, null)) { return false; }
            if (!ReferenceEquals(useEvent, null) && ReferenceEquals(this, null)) { return false; }

            return true;
        }
    }
}
