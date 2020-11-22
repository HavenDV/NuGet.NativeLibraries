using System;

#nullable enable

namespace H.Build.CustomTasks.Extensions
{
    /// <summary>
    /// Extensions that work with <see langword="string"/>
    /// </summary>
    public static class StringExtensions
    {
        /// <summary>
        /// Replaces the strings between the starting fragment and the ending.
        /// All available fragments will be replaced.
        /// <para/>Default <paramref name="comparison"/> is <see cref="StringComparison.Ordinal"/>.
        /// <![CDATA[Version: 1.0.0.0]]>
        /// </summary>
        /// <param name="text"></param>
        /// <param name="start"></param>
        /// <param name="end"></param>
        /// <param name="value"></param>
        /// <param name="count"></param>
        /// <param name="comparison"></param>
        /// <returns></returns>
        public static string ReplaceAll(this string text, string start, string end, string value, out int count, StringComparison? comparison = null)
        {
            text = text ?? throw new ArgumentNullException(nameof(text));
            start = start ?? throw new ArgumentNullException(nameof(start));
            end = end ?? throw new ArgumentNullException(nameof(end));

            count = 0;

            var index2 = -end.Length;
            while (true)
            {
                var index1 = text.IndexOf(start, index2 + end.Length, comparison ?? StringComparison.Ordinal);
                if (index1 < 0)
                {
                    return text;
                }

                index1 += start.Length;
                index2 = text.IndexOf(end, index1, comparison ?? StringComparison.Ordinal);
                if (index2 < 0)
                {
                    return text;
                }

                count++;

                index1 -= start.Length;
                index2 += end.Length;

                text = text.Remove(index1, index2 - index1);
                text = text.Insert(index1, value);

                index2 = index1 + value.Length;
            }
        }
    }
}
