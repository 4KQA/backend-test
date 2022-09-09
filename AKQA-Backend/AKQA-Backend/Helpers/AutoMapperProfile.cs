using AKQA_Backend.Entities;
using AKQA_Backend.Models;
using AutoMapper;

namespace AKQA_Backend.Helpers
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            //CreatePeople -> People
            CreateMap<CreatePeople, People>();

            //UpdatePeople -> People
            CreateMap<UpdatePeople, People>().ForAllMembers(x => x.Condition(
                    (src, dest, prop) =>
                    {
                        // ignore null & empty string properties
                        if (prop == null) return false;
                        if (prop.GetType() == typeof(string) && string.IsNullOrEmpty((string)prop)) return false;

                        return true;
                    }
                ));
        }
    }
}
