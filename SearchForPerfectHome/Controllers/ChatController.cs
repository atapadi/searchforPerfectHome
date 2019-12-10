using SearchForPerfectHome.Extensions;
using SearchPerfectHome.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SearchForPerfectHome.Controllers
{
    [Authorize]
    public class ChatController : Controller
    {
        private readonly IGetUser _getUser;

        public ChatController(IGetUser getUser)
        {
            _getUser = getUser;
        }

        // GET: Chat
        public ActionResult Chat()
        {
            var userId = User.Identity.GetSecurityUserId();
            var model = _getUser.Get(userId);
            return View(model);
        }
    }
}