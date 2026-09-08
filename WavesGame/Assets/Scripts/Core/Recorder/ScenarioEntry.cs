using UUtils;

namespace Core.Recorder
{
    public class ScenarioEntry : WavesEntry
    {
        public ScenarioEntry(WavesRecordEntryType eventType) : base(eventType)
        {
        }

        public override void PerformEntry()
        {
            DebugUtils.DebugLogMsg($"TODO", DebugUtils.DebugType.Temporary);
        }
    }
}