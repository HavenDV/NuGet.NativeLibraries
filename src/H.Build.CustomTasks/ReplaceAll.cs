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
        public string[]? Paths { get; set; }

        [Required]
        public string? Start { get; set; }

        [Required]
        public string? End { get; set; }

        public string Value { get; set; } = string.Empty;

        #endregion

        #region Methods

        public override bool Execute()
        {
            Path = Path ?? throw new ArgumentNullException(nameof(Path));
            Start = Start ?? throw new ArgumentNullException(nameof(Start));
            End = End ?? throw new ArgumentNullException(nameof(End));

            var sum = 0;
            foreach (var path in Paths ?? new []{ Path })
            {
                var contents = File.ReadAllText(path);
                if (!contents.Contains(Start) || !contents.Contains(End))
                {
                    continue;
                }

                File.WriteAllText(path, contents
                    .ReplaceAll(Start, End, Value ?? string.Empty, out var count, StringComparison.OrdinalIgnoreCase));

                sum += count;
            }

            Log.LogMessage(MessageImportance.High, $"{nameof(ReplaceAll)}: Removed {sum} matches.");

            return true;
        }

        #endregion
    }
}
