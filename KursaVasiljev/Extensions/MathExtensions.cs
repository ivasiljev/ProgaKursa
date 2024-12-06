using System.Numerics;

namespace KursaVasiljev.Extensions
{
    internal static class MathExtensions
    {
        public static K OwnSum<T, K>(this T[] array, Func<T, K> selector) where T : class where K : INumber<K>
        {
            K sum = default;
            for (int i = 0; i < array.Length; i++)
            {
                if (i == 0)
                {
                    sum = selector(array[i]);
                }
                else
                {
                    sum = sum + selector(array[i]);
                }
            }
            return sum;
        }

        public static double OwnAvg<T, K>(this T[] array, Func<T, K> selector) where T : class where K : INumber<K>
        {
            try
            {
                var dSum = Convert.ToDouble((object)array.OwnSum(selector));
                return dSum / array.Length;
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Не удалось привести сумму к типу double. Ошибка: {ex.ToString()}");
                return 0;
            }
        }
    }
}
