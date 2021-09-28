using System;

namespace VacunadosReporte.Entities
{
    public class People
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Lastname { get; set; }
        public string Place { get; set; }
        public DateTime VaccinationDate { get; set; }
        public string DocumentId { get; set; }
        public int Dose { get; set; }
        public string VaccineDescription { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}