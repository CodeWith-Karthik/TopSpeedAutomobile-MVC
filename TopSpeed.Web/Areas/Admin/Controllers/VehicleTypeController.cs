using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Data;
using TopSpeed.Application.ApplicationConstants;
using TopSpeed.Application.Contracts.Presistence;
using TopSpeed.Domain.Models;
using TopSpeed.Infrastructure.Common;

namespace TopSpeed.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = CustomRole.MasterAdmin + "," + CustomRole.Admin)]
    public class VehicleTypeController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public VehicleTypeController(IUnitOfWork unitOfWork, IWebHostEnvironment webHostEnvironment)
        {
            _unitOfWork = unitOfWork;
            _webHostEnvironment = webHostEnvironment;

        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            List<VehicleType> vehicleTypes = await _unitOfWork.VehicleType.GetAllAsync();

            return View(vehicleTypes);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(VehicleType vehicleType)
        {

            if (ModelState.IsValid)
            {
                await _unitOfWork.VehicleType.Create(vehicleType);
                await _unitOfWork.SaveAsync();

                TempData["success"] = CommonMessage.RecordCreated;

                return RedirectToAction(nameof(Index));
            }

            return View();
        }

        [HttpGet]
        public async Task<IActionResult> Details(Guid id)
        {
            VehicleType vehicleType = await _unitOfWork.VehicleType.GetByIdAsync(id);

            return View(vehicleType);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(Guid id)
        {
            VehicleType vehicleType = await _unitOfWork.VehicleType.GetByIdAsync(id);

            return View(vehicleType);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(VehicleType vehicleType)
        {
           
            if (ModelState.IsValid)
            {
                await _unitOfWork.VehicleType.Update(vehicleType);
                await _unitOfWork.SaveAsync();

                TempData["warning"] = CommonMessage.RecordUpdated;

                return RedirectToAction(nameof(Index));
            }

            return View();
        }

        [HttpGet]
        public async Task<IActionResult> Delete(Guid id)
        {
            VehicleType vehicleType = await _unitOfWork.VehicleType.GetByIdAsync(id);

            return View(vehicleType);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(VehicleType vehicleType)
        {
            
            await _unitOfWork.VehicleType.Delete(vehicleType);
            await _unitOfWork.SaveAsync();

            TempData["error"] = CommonMessage.RecordDeleted;

            return RedirectToAction(nameof(Index));
        }
    }
}
