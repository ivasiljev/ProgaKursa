namespace KursaVasiljev.Models
{
    public class Person
    {
        public string Surname { get; set; }
        public string Name { get; set; }
        public int Age { get; set; }

        public override string ToString()
        {
            return $"{Surname} {Name} - {Age} лет";
        }
    }
}
