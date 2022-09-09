using Models;
using Dto;

namespace Repositories{
    public interface BaseInterface{
        public bool Exists(int id);
    }

    public interface ISurvivorRepos : BaseInterface{
        public IEnumerable<Survivor> GetSurvivors();
        public IEnumerable<Survivor> Relitives(Survivor survivor);
        public Survivor GetSurvivor(int SurvivorID);
        public Survivor SearchByLastname(string lastName);
        public bool CheckValidMove(double currentLatitude,double Newlatitude);

        public Stats GetStats();

        public bool DeleteSurvivor(int id);
        public Survivor UpdateSurvivor(int id, Survivor input);
        public Survivor CreateSurvivor(Survivor input);
    }
}