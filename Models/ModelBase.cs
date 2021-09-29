namespace VacunadosReporte.Models
{
    public abstract class ModelBase
    {
        protected ModelBase(int totalVaccinated)
        {
            TotalVaccinated = totalVaccinated;
        }
        public int TotalVaccinated { get; set; }
    }
}