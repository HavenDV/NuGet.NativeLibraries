using Microsoft.Build.Framework;
using Microsoft.Build.Utilities;

namespace H.Build.CustomTasks
{
    public class RemoveAll : Task
    {
        #region Properties

        public string Path { get; set; }

        public string From { get; set; }

        public string To { get; set; }

        #endregion

        #region Methods

        public override bool Execute()
        {
            var count = 1;
            Log.LogMessage(MessageImportance.High, $"{nameof(RemoveAll)}: Removed {count} matches.");

            return true;
        }

        #endregion
    }
}
