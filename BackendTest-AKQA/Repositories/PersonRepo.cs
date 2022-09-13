using BackendTest_AKQA.Models;

namespace BackendTest_AKQA.Repositories
{
    public class PersonRepo : IPersonRepo
    {
        private readonly PersonContext _context;

        public PersonRepo(PersonContext context)
        {
            _context = context;
        }

        public bool CheckIfMoveValid(double oldLat, double newLat)
        {
            if (oldLat > 0 && newLat < 0 || oldLat < 0 && newLat > 0)
            {
                return false;
            }
            return true;
        }

        public void CreatePerson(Person person)
        {
            Person _person = new Person
            {
                FirstName = person.FirstName,
                LastName = person.LastName,
                Age = person.Age,
                Gender = person.Gender,
                Latitude = person.Latitude,
                Longitude = person.Longitude,
                Alive = person.Alive
            };
            _context.People.Add(_person);
            _context.SaveChanges();
        }

        public void DeletePerson(int id)
        {
            _context.People.Remove(GetPerson(id));
            _context.SaveChanges();
        }

        public IEnumerable<Person> GetPeople()
        {
            return _context.People.ToList();
        }

        public Person GetPerson(int id)
        {
            return _context.People.Find(id);
        }

        public Statistics GetStatistics()
        {
            int people = _context.People.Count();
            int alive = _context.People.Count(p => p.Alive == true);
            int north = _context.People.Count(p => p.Latitude > 0);
            int south = _context.People.Count(p => p.Latitude < 0);

            Statistics stats = new Statistics
            {
                Survivors = alive,
                Deceased = _context.People.Count(p => p.Alive == false),
                PctageOfsurvivors = (100 / people) * alive,
                PctOnNorthHem = (100 / people) * north,
                PctONSouthHem = (100 / people) * south
            };
            return stats;
        }

        public IEnumerable<Person> SearchByLastName(string lastName)
        {
            return _context.People.Where(p => p.LastName.Contains(lastName, StringComparison.OrdinalIgnoreCase));
        }

        public string UpdatePerson(int id, Person person)
        {
            Person old = GetPerson(id);
            double oldLat = old.Latitude;
            if (CheckIfMoveValid(oldLat, person.Latitude))
            {
                //_context.Entry()
                _context.Entry(old).CurrentValues.SetValues(person);
                //_context.Entry(old).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                _context.SaveChanges();
                return "Update succesfull";
            }
            return "Hmmm, unless you can travel in space, you can't move between the two halves of the earth.";
        }
    }
}
