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

        [Required]
        public string[]? Paths { get; set; }

        [Required]
        public string? Start { get; set; }

        [Required]
        public string? End { get; set; }

        public string Value { get; set; } = string.Empty;
        public bool IncludeStartEnd { get; set; } = true;

        #endregion

        #region Methods

        public override bool Execute()
        {
            Start = Start ?? throw new ArgumentNullException(nameof(Start));
            End = End ?? throw new ArgumentNullException(nameof(End));
            Paths = Paths ?? throw new ArgumentNullException(nameof(Paths));

            foreach (var path in Paths)
            {
                var contents = File.ReadAllText(path);
                if (!contents.Contains(Start) || !contents.Contains(End))
                {
                    continue;
                }

                File.WriteAllText(path, contents
                    .ReplaceAll(Start, End, Value ?? string.Empty, out var count, IncludeStartEnd, StringComparison.OrdinalIgnoreCase));

                Log.LogMessage(MessageImportance.High, $"{nameof(ReplaceAll)}: Removed {count} matches from \"{path}\".");
            }

            return true;
        }

        #endregion
    }
}
