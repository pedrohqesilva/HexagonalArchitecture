using AutoMapper;
using ConsoleAppRE;
using Core.Domain.Events;
using Core.Domain.Interfaces;
using Core.Infrastructure.CrossCutting.Transaction;
using Example.Domain.Commands;
using Example.Domain.Interfaces.Queries;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NSwag.Annotations;
using Presentations.Api.Application.ViewModel;
using Presentations.Api.Application.ViewModel.Country;
using Presentations.Api.Controllers.Base;
using Presentations.Api.Documentation;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Presentations.Api.Controllers
{
    [OpenApiTag(Groups.COUNTRY)]
    public class CountriesController : ApiController
    {
        private readonly ICountryQuery _countryQuery;

        public CountriesController(
            INotificationHandler<DomainNotification> notifications,
            IMediatorHandler mediator,
            IMapper mapper,
            IServiceProvider serviceProvider,
            ICountryQuery countryQuery) : base(notifications, mediator, mapper, serviceProvider)
        {
            _countryQuery = countryQuery;
        }

        [HttpGet("{countryId}")]
        [OpenApiOperation("Get a specific Country in the database", "...")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> Get(string countryId, CancellationToken cancellationToken)
        {
            var country = await _countryQuery.Get(countryId, cancellationToken);
            var result = _mapper.Map<Countries, CountryViewModel>(country);
            return Response(result);
        }

        [HttpPost]
        [OpenApiOperation("Create new Country in the database", "...")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ErrorValidationResponse), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Post(AddCountryViewModel viewModel, CancellationToken cancellationToken)
        {
            using (var transaction = new Transactions(_serviceProvider))
            {
                var command = _mapper.Map<AddCountryViewModel, AddCountryCommand>(viewModel);
                var result = await _mediator.SendCommandAsync<AddCountryCommand, Countries>(command, cancellationToken);

                if (result != null)
                {
                    transaction.Commit();
                }

                return Response(result?.CountryId);
            }
        }
    }
}