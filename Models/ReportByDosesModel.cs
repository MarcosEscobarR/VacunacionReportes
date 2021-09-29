namespace VacunadosReporte.Models
{
    public class ReportByDosesModel: ModelBase
    {
        public ReportByDosesModel(int firstDose, int secondDose, int totalVaccinated): base(totalVaccinated)
        {
            FirstDose = firstDose;
            SecondDose = secondDose;
        }
        public int FirstDose { get;  }
        public int SecondDose { get;  }
    }
}