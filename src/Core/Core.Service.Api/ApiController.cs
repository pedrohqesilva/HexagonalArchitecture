using Core.Domain.EventHandlers;
using Core.Domain.Events;
using Core.Domain.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace Core.Service.Api
{
    [Route("api/[controller]")]
    [ApiController]
    public class ApiController : ControllerBase
    {
        protected readonly DomainNotificationHandler _notifications;
        protected readonly IMediatorHandler _mediator;

        protected ApiController(INotificationHandler<DomainNotification> notifications,
                                IMediatorHandler mediator)
        {
            _notifications = (DomainNotificationHandler)notifications;
            _mediator = mediator;
        }

        protected IEnumerable<DomainNotification> Notifications => _notifications.GetNotifications();

        protected new IActionResult Response(bool result)
        {
            if (!_notifications.HasNotifications())
            {
                return Ok(result);
            }

            return BadRequest(new
            {
                errors = _notifications.GetNotifications()
            });
        }
    }
}
