namespace KursaVasiljev.Common
{
    internal static class OwnMath
    {
        public static void BinaryInsertionSort<T, K>(T[] array, Func<T, K> selector, SortDirection sortDirection) where T : class where K : IComparable
        {
            int count = 0;

            bool comparator(K a, K b) => sortDirection == SortDirection.Descending ? a.CompareTo(b) < 0 : a.CompareTo(b) > 0;

            for (int i = 0; i < array.Length; i++)
            {
                var elem = array[i];

                // Бин-поиск
                K tmp = selector(elem); int left = 0; int right = i - 1;
                while (left <= right)
                {
                    int m = (left + right) / 2;
                    if (comparator(tmp, selector(array[m])))
                        right = m - 1;
                    else left = m + 1;
                    count++;
                }

                for (int j = i - 1; j >= left; j--)
                {
                    array[j + 1] = array[j]; // Сдвиг
                }

                array[left] = elem; // вставка элемента на нужное место
            }
        }
    }
}
