using AutoMapper;
using Example.Domain.Commands;
using Presentations.Api.Application.ViewModel.Country;

namespace Presentations.Api.Application.Mappings.ViewModelToDomain.Countries
{
    public class AddCountryMap : Profile
    {
        public AddCountryMap()
        {
            CreateMap<AddCountryViewModel, AddCountryCommand>();
        }
    }
}