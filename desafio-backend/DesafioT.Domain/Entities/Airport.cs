namespace DesafioT.Domain.Entities
{
    public class Airport
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string City { get; set; }

        public bool IsValid()
        {
            bool valid = false;

            // Valida preenchimento
            valid = !string.IsNullOrEmpty(Id) && !string.IsNullOrEmpty(Name) && !string.IsNullOrEmpty(City);

            if (valid)
                // Valida tamanha do campo
                valid = Id.Length == 3;

            return valid;
        }
    }
}
