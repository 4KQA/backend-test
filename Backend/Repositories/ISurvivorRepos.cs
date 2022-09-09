using Models;

namespace Repositories{
    public interface BaseInterface{
        public bool Exists(int id);
    }

    public interface ISurvivorRepos : BaseInterface{
        public IEnumerable<Survivor> GetSurvivors();
        public IEnumerable<Survivor> GetSurvivorsByHemisphere(string hemisphere);
        public Survivor GetSurvivorByHemisphere(string hemisphere, int survivorID);
        public Survivor GetSurvivor(int SurvivorID);


        public bool DeleteSurvivor(int id);
        public bool UpdateSurvivor(int id, Survivor input);
        public Survivor CreateSurvivor(Survivor input);
    }
}