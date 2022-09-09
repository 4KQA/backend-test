using Context;
using Models;

namespace Repositories{
    public class SurvivorsRepos : ISurvivorRepos{
        private readonly ApiContext _Context;
        public SurvivorsRepos(ApiContext Context){
            _Context = Context ?? throw new ArgumentNullException(nameof(Context));
        }

        public IEnumerable<Survivor> GetSurvivors(){
            return _Context.Survivors.ToList();
        }
        public Survivor GetSurvivor(int survivorID){
            return _Context.Survivors.FirstOrDefault(x => x.Survivor_ID == survivorID);
        }

        // extra
        public IEnumerable<Survivor> GetSurvivorsByHemisphere(string hemisphere){
            throw new NotImplementedException();
        }
        public Survivor GetSurvivorByHemisphere(string hemisphere, int survivorID){
            throw new NotImplementedException();
        }
        // extra end


        // cud
        // mangler validation af data input
        public Survivor CreateSurvivor(Survivor input){
            Survivor _survivor = new Survivor{
                firstName = input.firstName,
                lastName = input.lastName,
                age = input.age,
                gender = input.gender,
                longitude = input.longitude,
                latitude = input.latitude,
                alive = input.alive,
            };

            _Context.Survivors.Add(_survivor);
            _Context.SaveChanges();
            return _survivor;
        }
        public bool UpdateSurvivor(int survivorID, Survivor input){
            Survivor _survivor = _Context.Survivors.FirstOrDefault(x => x.Survivor_ID == survivorID);
            if(_survivor == null){ return false; }
            
            _survivor.firstName = input.firstName;
            _survivor.lastName = input.lastName;
            _survivor.age = input.age;
            _survivor.gender = input.gender;
            _survivor.longitude = input.longitude;
            _survivor.latitude = input.latitude;
            _survivor.alive = input.alive;

            _Context.SaveChanges();
            return true;
        }

        public bool DeleteSurvivor(int survivorID){
            var _survivor = _Context.Survivors.Find(survivorID);
            if (_survivor == null) { return false; }

            _Context.Survivors.Remove(_survivor);
            _Context.SaveChanges();
            return true;
        }



        public bool Exists(int survivorID){
            return _Context.Survivors.Any(x => x.Survivor_ID == survivorID);
        }
    }
}