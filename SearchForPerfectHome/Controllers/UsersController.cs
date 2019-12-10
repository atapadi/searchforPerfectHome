using Newtonsoft.Json;
using PagedList;
using SearchForPerfectHome.Extensions;
using SearchPerfectHome.Interfaces;
using SearchPerfectHome.Models;
using SearchPerfectHome.ViewModels;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace SearchForPerfectHome.Controllers
{
    [Authorize]
    public class UsersController : Controller
    {
        private readonly IUpdateUserProfile _updateUserProfile;
        private readonly IGetUser _getUser;

        public UsersController(IUpdateUserProfile updateUserProfile, IGetUser getUser)
        {
            _updateUserProfile = updateUserProfile;
            _getUser = getUser;
        }

        // GET: Users
        public ActionResult Index()
        {
            var userId = User.Identity.GetSecurityUserId();
            var model = _getUser.Get(userId);
            var vm = new UsersViewModel
            {
                UserId = model.UserId,
                Degree = model.Degree,
                UserName = model.UserName,
                University = model.University,
                Interests = model.Interests,
                Status = model.Status,
                ImagePath = model.ImagePath
            };
            return View(vm);
        }

        public ActionResult GetAllUsers(string Search_Data, int? Page_No, string Filter_Value)
        {
            var user = _getUser.GetAll();
            var userId = User.Identity.GetSecurityUserId();

            ViewBag.UserId = userId;

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

        public ActionResult UpdateUserAfterSignUp()
        {
            return View(new UsersViewModel());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Update(UsersViewModel viewModel)
        {
            string message = "";
            if (ModelState.IsValid)
            {
                string InputFileName = "";
                if (viewModel.UserImage != null)
                {
                    InputFileName = Path.GetFileName(viewModel.UserImage.First().FileName);
                    var ServerSavePath = Path.Combine(Server.MapPath("~/UserImage/") + InputFileName);
                    //Save file to server folder  
                    viewModel.UserImage.First().SaveAs(ServerSavePath);
                }

                var userId = User.Identity.GetSecurityUserId();
                var email = User.Identity.GetSecurityEmail();
                var user = _getUser.Get(userId);
                var model = new UsersModel
                {
                    Id=  user.Id,
                    UserName = viewModel.UserName,
                    University = viewModel.University,
                    Degree = viewModel.Degree,
                    Interests = viewModel.Interests,
                    ImagePath = string.IsNullOrEmpty(InputFileName) ? null : "~/UserImage/" + InputFileName,
                    Status = viewModel.Status,
                    UserId = userId,
                    Email = email
                };
                message = await _updateUserProfile.Update(model);
            }

            return RedirectToAction("Index", "Users");
        }

        [HttpPost]
        public JsonResult AutoCompleteUniversity(string keyword)
        {
            List<string> result = null;
            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("text/json"));
                using (var responseMessage = client.GetAsync("http://universities.hipolabs.com/search?country=united+states"))
                {
                    var str = responseMessage.Result.Content.ReadAsStringAsync();
                    string s = str.Result;
                    var list = JsonConvert.DeserializeObject<List<ParseModel>>(s);
                    result = list.Where(x => x.name.ToLower().StartsWith(keyword.ToLower()))
                                        .Select(y => y.name).Distinct().OrderBy(y => y).ToList();
                }
            }
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetUserDetailsByUserId(string userid)
        {
            var user = _getUser.Get(userid);
            return PartialView("_GetUserDetails", user);
        }

        public class ParseModel
        {
            [JsonProperty(PropertyName = "name")]
            public string name { get; set; }
        }
    }
}