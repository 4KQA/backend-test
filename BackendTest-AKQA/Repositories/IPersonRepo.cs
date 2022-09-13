using BackendTest_AKQA.Models;

namespace BackendTest_AKQA.Repositories
{
    public interface IPersonRepo
    {
        public IEnumerable<Person> GetPeople();
        public IEnumerable<Person> SearchByLastName(string lastName);
        public Person GetPerson(int id);
        public bool CheckIfMoveValid(double oldLat, double newLat);
        public void CreatePerson(Person person);
        public void DeletePerson(int id);
        public string UpdatePerson(int id, Person person);
        public Statistics GetStatistics();
    }
}
