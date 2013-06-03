namespace NietzscheBiography.Domain.Models
{
    public static class NietzscheConstants
    {
        public const long NietzscheId = 603;

        public const long WritingEventTypeId = 92;
        public const long ComposingEventTypeId = 22;
        public const long PublishingEventTypeId = 72;
        public const long PhotographEventTypeId = 93;

        public const long AgentThematicRoleId = 1;
        public const long BeneficiaryThematicRoleId = 2;

        public const string EventDetailUrl = "event-{id}";
        public const string ConnectionDetailUrl = "info/{participantId}";
        public const string MediaItemDetailUrl = "mediaitem-{id}";
        public const string LocationDetailUrl = "place-{id}";
        
    }
}