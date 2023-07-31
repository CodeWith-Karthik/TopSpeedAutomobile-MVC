using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.WebUtilities;
using System.Diagnostics;
using TopSpeed.Application.Contracts.Presistence;
using TopSpeed.Application.ExtensionsMethods;
using TopSpeed.Domain.Models;
using TopSpeed.Domain.ViewModel;

namespace TopSpeed.Web.Areas.Customer.Controllers
{
    [Area("Customer")]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IUnitOfWork _unitOfWork;

        public HomeController(ILogger<HomeController> logger, IUnitOfWork unitOfWork)
        {
            _logger = logger;
            _unitOfWork = unitOfWork;
        }

        [HttpGet]
        public async Task<IActionResult> Index(int? page, bool resetFilter = false)
        {
            IEnumerable<SelectListItem> brandList = _unitOfWork.Brand.Query().Select(x => new SelectListItem
            {
                Text = x.Name.ToUpper(),
                Value = x.Id.ToString()
            });

            IEnumerable<SelectListItem> vehicleTypeList = _unitOfWork.VehicleType.Query().Select(x => new SelectListItem
            {
                Text = x.Name.ToUpper(),
                Value = x.Id.ToString()
            });

            List<Post> posts;

            if (resetFilter)
            {
                TempData.Remove("FilteredPosts");
                TempData.Remove("SelectedBrandId");
                TempData.Remove("SelectedVehicleTypeId");
            }

            if (TempData.ContainsKey("FilteredPosts"))
            {
                posts = TempData.Get<List<Post>>("FilteredPosts");
                TempData.Keep("FilteredPosts");
            }
            else
            {
                posts = await _unitOfWork.Post.GetAllPost();
            }

            int pageSize = 3;

            int pageNumber = page ?? 1;

            int totalItems = posts.Count;

            int totalPages = (int)Math.Ceiling((double)totalItems / pageSize);

            ViewBag.TotalPages = totalPages;
            ViewBag.CurrentPage = pageNumber;

            var pagedPosts = posts.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToList();

            HttpContext.Session.SetString("PreviousUrl", HttpContext.Request.Path);

            HomePostVM homePostVM = new HomePostVM
            {
                Posts = pagedPosts,
                BrandList = brandList,
                VehicleTypeList = vehicleTypeList,
                BrandId = (Guid?)TempData["SelectedBrandId"],
                VehicleTypeId = (Guid?)TempData["SelectedVehicleTypeId"]
            };

            return View(homePostVM);
        }


        [HttpPost]
        public async Task<IActionResult> Index(HomePostVM homePostVM)
        {
            var posts = await _unitOfWork.Post.GetAllPost(homePostVM.searchBox, homePostVM.BrandId, homePostVM.VehicleTypeId);

            TempData.Put("FilteredPosts", posts);
            TempData["SelectedBrandId"] = homePostVM.BrandId;
            TempData["SelectedVehicleTypeId"] = homePostVM.VehicleTypeId;

            return RedirectToAction("Index", new { page = 1, resetFilter = false });
        }

        public async Task<IActionResult> Details(Guid id, int? page)
        {
            Post post = await _unitOfWork.Post.GetPostById(id);

            List<Post> posts = new List<Post>();

            if (post != null)
            {
                posts = await _unitOfWork.Post.GetAllPost(post.Id, post.BrandId);
            }

            ViewBag.CurrentPage = page;

            CustomerDetailsVM customerDetailsVM = new CustomerDetailsVM
            {
                Post = post,
                Posts = posts
            };

            return View(customerDetailsVM);
        }



        public IActionResult GoBack(int? page)
        {
            string? previousUrl = HttpContext.Session.GetString("PreviousUrl");

            if (!string.IsNullOrEmpty(previousUrl))
            {
                // Append the page number to the previous URL if it exists
                if (page.HasValue)
                {
                    previousUrl = QueryHelpers.AddQueryString(previousUrl, "page", page.Value.ToString());
                }

                HttpContext.Session.Remove("PreviousUrl"); // Remove the session variable

                return Redirect(previousUrl);
            }
            else
            {
                // Handle the case when there is no previous URL stored in the session
                // You can redirect to a default page or take some other action
                return RedirectToAction("Index");
            }
        }



        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}