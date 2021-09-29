namespace VacunadosReporte.Models
{
    public class ReportByVaccineModel: ModelBase
    {
        public ReportByVaccineModel(int totalVaccinated, int pfizer, int astrazeneca, int sputnik): base(totalVaccinated)
        {
            Pfizer = pfizer;
            Astrazeneca = astrazeneca;
            Sputnik = sputnik;
        }
        public int Pfizer { get; }
        public int Astrazeneca { get; }
        public int Sputnik { get; }
    }
}