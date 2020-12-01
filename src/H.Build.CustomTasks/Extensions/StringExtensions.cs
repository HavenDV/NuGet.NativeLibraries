using System;
using System.Collections.Generic;

#nullable enable

namespace H.Build.CustomTasks.Extensions
{
    /// <summary>
    /// Extensions that work with <see langword="string"/>
    /// </summary>
    public static class StringExtensions
    {
        /// <summary>
        /// Retrieves the strings between the starting fragment and the ending.
        /// All available fragments are retrieved.
        /// <para/>Returns empty <see cref="List{T}"/> if nothing is found.
        /// <para/>Default <paramref name="comparison"/> is <see cref="StringComparison.Ordinal"/>.
        /// <![CDATA[Version: 1.0.0.1]]>
        /// </summary>
        /// <param name="text"></param>
        /// <param name="start"></param>
        /// <param name="end"></param>
        /// <param name="comparison"></param>
        /// <returns></returns>
        public static IEnumerable<(int Start, int Length)> ExtractAllFragments(this string text, string start, string end, StringComparison? comparison = null)
        {
            text = text ?? throw new ArgumentNullException(nameof(text));
            start = start ?? throw new ArgumentNullException(nameof(start));
            end = end ?? throw new ArgumentNullException(nameof(end));

            var index2 = -end.Length;
            while (true)
            {
                var index1 = text.IndexOf(start, index2 + end.Length, comparison ?? StringComparison.Ordinal);
                if (index1 < 0)
                {
                    yield break;
                }

                index1 += start.Length;
                index2 = text.IndexOf(end, index1, comparison ?? StringComparison.Ordinal);
                if (index2 < 0)
                {
                    yield break;
                }

                yield return (index1, index2 - index1);
            }
        }

        /// <summary>
        /// Replaces the strings between the starting fragment and the ending.
        /// All available fragments will be replaced.
        /// <para/>Default <paramref name="comparison"/> is <see cref="StringComparison.Ordinal"/>.
        /// <![CDATA[Version: 1.0.0.2]]>
        /// <![CDATA[Dependency: ExtractAllFragments(this string text, string start, string end, StringComparison? comparison = null)]]> <br/>
        /// </summary>
        /// <param name="text"></param>
        /// <param name="start"></param>
        /// <param name="end"></param>
        /// <param name="value"></param>
        /// <param name="count"></param>
        /// <param name="includeStartEnd"></param>
        /// <param name="comparison"></param>
        /// <returns></returns>
        public static string ReplaceAll(this string text, string start, string end, string value, out int count, bool includeStartEnd = true, StringComparison? comparison = null)
        {
            text = text ?? throw new ArgumentNullException(nameof(text));
            start = start ?? throw new ArgumentNullException(nameof(start));
            end = end ?? throw new ArgumentNullException(nameof(end));

            var fix = 0;
            count = 0;
            foreach (var fragment in text.ExtractAllFragments(start, end, comparison))
            {
                var startIndex = fragment.Start + fix;
                var length = fragment.Length;
                if (includeStartEnd)
                {
                    startIndex -= start.Length;
                    length += start.Length + end.Length;
                }

                text = text.Remove(startIndex, length);
                text = text.Insert(startIndex, value);

                fix += value.Length - length;
                count++;
            }

            return text;
        }
    }
}
