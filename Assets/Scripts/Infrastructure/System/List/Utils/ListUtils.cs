using System;
using System.Collections.Generic;
using JetBrains.Annotations;
using ArgumentNullException = Infrastructure.System.Exceptions.ArgumentNullException;

namespace Infrastructure.System.List.Utils
{
    // TODO: Test
    public static class ListUtils
    {
        public static void Shuffle<T>([NotNull] this IList<T> list, [NotNull] Random random)
        {
            ArgumentNullException.ThrowIfNull(list);
            ArgumentNullException.ThrowIfNull(random);

            for (int i = list.Count - 1; i > 0; --i)
            {
                int j = random.Next(i + 1);

                (list[j], list[i]) = (list[i], list[j]);
            }
        }
    }
}