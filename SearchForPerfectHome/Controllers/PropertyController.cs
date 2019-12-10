using PagedList;
using SearchForPerfectHome.Extensions;
using SearchPerfectHome.Interfaces;
using SearchPerfectHome.Models;
using SearchPerfectHome.ViewModels;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace SearchForPerfectHome.Controllers
{
    [Authorize]
    public class PropertyController : Controller
    {
        private readonly IAddProperty _addProperty;
        private readonly IGetUserProperty _getUserProperty;
        private readonly IGetPropertyDetailsWithUser _getPropertyDetailsWithUser;
        private readonly IGetPropertyImageDetails _getPropertyImageDetails;
        private readonly IDeleteProperty _deleteProperty;
        private readonly IUpdateProperty _updateProperty;

        public PropertyController(IAddProperty addProperty, IGetUserProperty getUserProperty
                                , IGetPropertyDetailsWithUser getPropertyDetailsWithUser
                                , IGetPropertyImageDetails getPropertyImageDetails
                                , IDeleteProperty deleteProperty
                                 , IUpdateProperty updateProperty)
        {
            _addProperty = addProperty;
            _getUserProperty = getUserProperty;
            _getPropertyDetailsWithUser = getPropertyDetailsWithUser;
            _getPropertyImageDetails = getPropertyImageDetails;
            _deleteProperty = deleteProperty;
            _updateProperty = updateProperty;
        }

        // GET: Property
        public ActionResult AddProperty()
        {
            return View();
        }

        // GET: All Property
        public ActionResult GetAllProperty(string Search_Data, int? Page_No, string Filter_Value)
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
        public ActionResult AddProperty(PropertyViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                if (viewModel.PropertyImages.files != null)
                {
                    var imageList = new List<PropertyImageViewModel>();
                    foreach (var image in viewModel.PropertyImages.files)
                    {
                        if (image != null)
                        {
                            var InputFileName = Path.GetFileName(image.FileName);
                            var ServerSavePath = Path.Combine(Server.MapPath("~/UploadedFiles/") + InputFileName);
                            //Save file to server folder  
                            image.SaveAs(ServerSavePath);

                            PropertyImageViewModel img = new PropertyImageViewModel
                            {
                                Name = viewModel.PropertyName,
                                Size = image.ContentLength,
                                filePath = "~/UploadedFiles/" + InputFileName
                            };
                            imageList.Add(img);
                        }
                    }

                    viewModel.UserId = User.Identity.GetSecurityUserId();
                    _addProperty.Add(viewModel, imageList);
                }
            }
            return RedirectToAction("DisplayUserProperty");
        }

        [HttpGet]
        public async Task<ActionResult> DisplayUserProperty()
        {
            List<PropertyImageModel> images = null;
            var userId = User.Identity.GetSecurityUserId();
            if (!string.IsNullOrEmpty(userId))
            {
                images = await _getUserProperty.Get(userId);
            }
            return View("UserProperty", images);
        }

        [HttpPost]
        public async Task<ActionResult> Update(PropertyViewModel viewModel, List<HttpPostedFileBase> upload)
        {
            List<PropertyImageModel> list = new List<PropertyImageModel>();
            foreach (var m in upload)
            {
                var imageModel = new PropertyImageModel
                {
                    filePath = "~/UploadedFiles/" + m.FileName
                };

                var InputFileName = Path.GetFileName(m.FileName);
                var ServerSavePath = Path.Combine(Server.MapPath("~/UploadedFiles/") + InputFileName);
                //Save file to server folder  
                m.SaveAs(ServerSavePath);

                list.Add(imageModel);
            }

            PropertyModel model = new PropertyModel
            {
                Address = viewModel.Address,
                Amenities = viewModel.Amenities,
                City = viewModel.City,
                Details = viewModel.Details,
                PropertyName = viewModel.PropertyName,
                University = viewModel.University,
                State = viewModel.State,
                LastChanged = DateTime.Now,
                Id = viewModel.Id,
                Images = list
            };

            string errorMessage = await _updateProperty.Update(model);

            return RedirectToAction("GetAllProperty");
            //return PartialView("_UpdateUserProperty");
        }

        [HttpGet]
        public ActionResult GetPropertyInfoByImageId(int imageId)
        {
            var propertyModel = _getUserProperty.Get(imageId);
            List<PropertyImageViewModel> list = new List<PropertyImageViewModel>();

            foreach (var m in propertyModel.Images)
            {
                var imageModel = new PropertyImageViewModel
                {
                    filePath = m.filePath,
                    Id = m.Id
                };
                list.Add(imageModel);
            }
            PropertyViewModel model = new PropertyViewModel
            {
                Address = propertyModel.Address,
                Amenities = propertyModel.Amenities,
                Details = propertyModel.Details,
                City = propertyModel.City,
                PropertyName = propertyModel.PropertyName,
                State = propertyModel.State,
                Id = propertyModel.Id,
                University = propertyModel.University,
                Property = list
            };
            return View("_UpdateUserProperty", model);
        }

        public ActionResult GetPropertyInfoByPropertyId(int propertyId)
        {
            var result = _getPropertyDetailsWithUser.GetPropertyDetails(propertyId);
             return PartialView("_GetPropertyDetails", result);
        }

        [HttpPost]
        public async Task<ActionResult> DeleteProperty(int propertyId)
        {
            bool result = await _deleteProperty.Delete(propertyId);
            return Json(result, JsonRequestBehavior.AllowGet);
        }
    }
}