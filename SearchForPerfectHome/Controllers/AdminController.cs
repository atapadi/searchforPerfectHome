using Microsoft.AspNet.Identity.Owin;
using PagedList;
using SearchPerfectHome.DAL;
using SearchPerfectHome.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace SearchForPerfectHome.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminController : Controller
    {
        private readonly IGetUser _getUser;
        private readonly IDeleteUser _deleteUser;
        private readonly IGetPropertyDetailsWithUser _getPropertyDetailsWithUser;

        public AdminController(IGetUser getUser, IDeleteUser deleteUser, IGetPropertyDetailsWithUser getPropertyDetailsWithUser)
        {
            _getUser = getUser;
            _deleteUser = deleteUser;
            _getPropertyDetailsWithUser = getPropertyDetailsWithUser;
        }

        // GET: Admin
        public ActionResult ManageUsers(string Search_Data, int? Page_No, string Filter_Value)
        {
            var user = _getUser.GetAll();

            if (Search_Data != null)
            {
                Page_No = 1;
            }
            else
            {
                Search_Data = Filter_Value;
            }

            ViewBag.FilterValue = Search_Data;

            if (!string.IsNullOrEmpty(Search_Data))
            {
                user = user.Where(x => x.University.ToLower().Contains(Search_Data.ToLower()));
            }

            int Size_Of_Page = 2;
            int No_Of_Page = (Page_No ?? 1);
            return View(user.ToList().ToPagedList(No_Of_Page, Size_Of_Page));
        }

        public ActionResult ManageAllProperty(string Search_Data, int? Page_No, string Filter_Value)
        {
            var allProperty = _getPropertyDetailsWithUser.GetAllProperty();

            if (Search_Data != null)
            {
                Page_No = 1;
            }
            else
            {
                Search_Data = Filter_Value;
            }

            ViewBag.FilterValue = Search_Data;

            if (!string.IsNullOrEmpty(Search_Data))
            {
                allProperty = allProperty.Where(x => x.University.ToLower().Contains(Search_Data.ToLower()));
            }

            int Size_Of_Page = 2;
            int No_Of_Page = (Page_No ?? 1);
            return View(allProperty.ToPagedList(No_Of_Page, Size_Of_Page));
        }

        [HttpPost]
        public async Task<ActionResult> DeleteUser(string userId)
        {
            bool result = false;
            var _userManager = HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            var user = await _userManager.FindByIdAsync(userId);
            var rolesForUser = await _userManager.GetRolesAsync(userId);
            if (user != null)
            {
                result = await _deleteUser.Delete(userId);
                if(result)
                {
                    if (rolesForUser != null && rolesForUser.Count() > 0)
                    {
                        foreach (var item in rolesForUser.ToList())
                        {
                            // item should be the name of the role
                            await _userManager.RemoveFromRoleAsync(user.Id, item);
                        }
                    }
                    await _userManager.DeleteAsync(user);
                }
            }
            return Json(result, JsonRequestBehavior.AllowGet);
        }
    }
}