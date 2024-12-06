namespace KursaVasiljev.Common
{
    internal class CustomRandom : Random
    {
        protected override double Sample()
        {
            return Math.Pow(base.Sample(), 2);
        }

        public override int Next(int minValue, int maxValue)
        {
            return Next(minValue, maxValue, false);
        }

        public int Next(int minValue, int maxValue, bool inverse)
        {
            var k = inverse ? Sample() : (1 - Sample());
            var difValue = maxValue - minValue;
            return (int)Math.Round(k * difValue) + minValue;
        }
    }
}
