using AutoMapper;
using Core.Domain.EventHandlers;
using Core.Domain.Events;
using Core.Domain.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Presentations.Api.Application.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Presentations.Api.Controllers.Base
{
    [Route("api/[controller]")]
    [ProducesResponseType(typeof(ExeceptionResponse), StatusCodes.Status500InternalServerError)]
    public abstract class ApiController : ControllerBase
    {
        protected readonly DomainNotificationHandler _notifications;
        protected readonly IMediatorHandler _mediator;
        protected readonly IMapper _mapper;
        protected readonly IServiceProvider _serviceProvider;

        protected ApiController(INotificationHandler<DomainNotification> notifications,
                                IMediatorHandler mediator,
                                IMapper mapper,
                                IServiceProvider serviceProvider
            )
        {
            _notifications = (DomainNotificationHandler)notifications;
            _mediator = mediator;
            _mapper = mapper;
            _serviceProvider = serviceProvider;
        }

        protected IEnumerable<DomainNotification> Notifications => _notifications.GetNotifications();

        protected new IActionResult Response(object result)
        {
            if (!_notifications.HasNotifications())
            {
                return Ok(result);
            }

            return BadRequest(new ErrorValidationResponse
            {
                Erros = _notifications
                    .GetNotifications()
                    .Select(x => new MessageValidationResponse(x.PropertyName, x.ErrorMessage))
            });
        }
    }
}