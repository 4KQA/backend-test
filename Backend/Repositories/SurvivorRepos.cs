using Context;
using Models;
using Dto;

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

        public Survivor SearchByLastname(string lastname){
            return _Context.Survivors.FirstOrDefault(x => x.lastName.Contains(lastname));
        }

        public IEnumerable<Survivor> Relitives(Survivor survivor){
            return _Context.Survivors.Where(x=> x.lastName == survivor.lastName && x.firstName != survivor.firstName).ToList();
        }

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
        public Survivor UpdateSurvivor(int survivorID, Survivor input){
            Survivor _survivor = _Context.Survivors.FirstOrDefault(x => x.Survivor_ID == survivorID);
            if(_survivor == null){ return null; }

            if (CheckValidMove(_survivor.latitude, input.latitude) == false)
                return null;
            
            _survivor.firstName = input.firstName;
            _survivor.lastName = input.lastName;
            _survivor.age = input.age;
            _survivor.gender = input.gender;
            _survivor.longitude = input.longitude;
            _survivor.latitude = input.latitude;
            _survivor.alive = input.alive;

            _Context.SaveChanges();
            return _survivor;
        }

        public bool DeleteSurvivor(int survivorID){
            var _survivor = _Context.Survivors.Find(survivorID);
            if (_survivor == null) { return false; }

            _Context.Survivors.Remove(_survivor);
            _Context.SaveChanges();
            return true;
        }

        public Stats GetStats(){
            int _Alive_Survivors = _Context.Survivors.Count(x => x.alive == true);
            int _Dead_Survivors = _Context.Survivors.Count(x => x.alive == false);
            Stats _stats = new Stats{
                Ammount_Survivors = _Context.Survivors.Count(),
                Alive_Survivors = _Alive_Survivors,
                Dead_Survivors = _Dead_Survivors,
                Precentage_Alive_Survivors = ( 100 / _Context.Survivors.Count() ) * _Alive_Survivors
            };
            return _stats;
        }

        public bool CheckValidMove(double currentLatitude,double Newlatitude){
            if(currentLatitude > 0 && Newlatitude < 0 ||
               currentLatitude < 0 && Newlatitude > 0)
                return false;
            return true;
        }

        public bool Exists(int survivorID){
            return _Context.Survivors.Any(x => x.Survivor_ID == survivorID);
        }
    }
}