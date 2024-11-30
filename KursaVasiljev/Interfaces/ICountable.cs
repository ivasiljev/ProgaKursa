namespace KursaVasiljev.Interfaces
{
    internal interface ICountable
    {
        public int Count();
        public int Count(double avgMark);
        public int Count(double avgMarkMin, double avgMarkMax);
    }
}
