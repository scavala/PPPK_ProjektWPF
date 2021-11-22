namespace Zadatak.Models
{
    public class Klub
    {
        public int IDKlub { get; set; }
        public string Name { get; set; }

        public override string ToString() => Name;
       
    }
}
