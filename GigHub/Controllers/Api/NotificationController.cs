using AutoMapper;
using GigHub.Core;
using GigHub.Core.Dtos;
using GigHub.Core.Models;
using GigHub.Persistance;
using Microsoft.AspNet.Identity;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web.Http;
using WebGrease.Css.Extensions;

namespace GigHub.Controllers.Api
{
    [Authorize]
    public class NotificationController : ApiController
    {
        private readonly IUnitOfWork _unitOfWork;

        public NotificationController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IEnumerable<NotificationDto> GetNewNotifications()
        {   
            string userId = User.Identity.GetUserId();
            var notifications = _unitOfWork.UserNotifications.GetNewNotificationsByUser(userId);

            return notifications.Select(Mapper.Map<Notification, NotificationDto>);
        }

        [HttpPost]
        public IHttpActionResult MarkAsRead()
        {
            string userId = User.Identity.GetUserId();
            var notifications = _unitOfWork.UserNotifications.GetNewUserNotificationsByUser(userId);

            notifications.ForEach(x => x.Read());

            _unitOfWork.Commit();

            return Ok();
        }
    }
}
