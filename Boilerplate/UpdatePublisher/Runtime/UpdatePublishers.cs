using System.Collections.Generic;

namespace UpdatePublisher.Runtime
{
    public static class UpdatePublishers
    {
        //Update publishers
        public static UpdatePublisher updatePublisher = new();
        public static FixedUpdatePublisher fixedUpdatePublisher = new();
        public static LateUpdatePublisher lateUpdatePublisher = new();
        public static Dictionary<ScaledUpdateGroupEnum, ScaledUpdatePublisher> scaledUpdatePublishers = new();
    }
}
