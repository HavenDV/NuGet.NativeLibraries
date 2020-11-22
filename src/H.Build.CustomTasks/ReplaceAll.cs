using System;
using System.IO;
using H.Build.CustomTasks.Extensions;
using Microsoft.Build.Framework;
using Microsoft.Build.Utilities;

namespace H.Build.CustomTasks
{
    public class ReplaceAll : Task
    {
        #region Properties

        public string? Path { get; set; }

        public string? Start { get; set; }

        public string? End { get; set; }

        public string? Value { get; set; }

        #endregion

        #region Methods

        public override bool Execute()
        {
            Path = Path ?? throw new ArgumentNullException(nameof(Path));
            Start = Start ?? throw new ArgumentNullException(nameof(Start));
            End = End ?? throw new ArgumentNullException(nameof(End));

            File.WriteAllText(Path, File.ReadAllText(Path)
                .ReplaceAll(Start, End, Value ?? string.Empty, out var count, StringComparison.OrdinalIgnoreCase));

            Log.LogMessage(MessageImportance.High, $"{nameof(ReplaceAll)}: Removed {count} matches.");

            return true;
        }

        #endregion
    }
}
