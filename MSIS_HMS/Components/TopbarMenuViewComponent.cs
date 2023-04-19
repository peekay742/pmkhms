using System;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MSIS_HMS.Core.Entities;
using MSIS_HMS.Core.Repositories;
using System.Security.Claims;
using MSIS_HMS.Infrastructure.Enums;
using System.Linq;
using MSIS_HMS.Infrastructure.Data;
using MSIS_HMS.Interfaces;
using System.ComponentModel;
using MSIS_HMS.Models;

namespace MSIS_HMS.Components
{
    [ViewComponent(Name = "TopbarMenu")]
    public class TopbarMenuViewComponent : ViewComponent
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IPaymentRepository _paymentRepository;
        private readonly ILabOrderRepository _labOrderRepository;
        //private readonly ILabResultRepository _labResultRepository;//add new for approve
        private readonly IUserService _userService;
        private readonly IBranchService _branchService;
        public TopbarMenuViewComponent(UserManager<ApplicationUser> userManager,IPaymentRepository paymentRepository,ApplicationDbContext context,ILabOrderRepository labOrderRepository,IUserService userService,IBranchService branchService, ILabResultRepository labResultRepository )
        {           
            _userManager = userManager;
            _paymentRepository = paymentRepository;
            _context=context;
            _labOrderRepository = labOrderRepository;
            //_labResultRepository = labResultRepository;
            _userService = userService;
            _branchService = branchService;
        }

        public IViewComponentResult Invoke()
        {
            //if (_branchService.GetBranchIdByUser() == null)
            //{
            //    AuthenticateRequest authenticateRequest = new AuthenticateRequest();
              
            //    //return View("~/Views/Account/Login.cshtml", authenticateRequest);
            //}
            ViewData["UserName"] = _userManager.GetUserName((ClaimsPrincipal)User);
            var userId = _userManager.GetUserId((ClaimsPrincipal)User);
            var role = _context.UserRoles.Where(x => x.UserId == userId).FirstOrDefault();
           if(role!=null)
            {
                var roleName = _context.Roles.Where(x => x.Id == role.RoleId).FirstOrDefault();
                ViewData["Role"] = roleName.ToString();
                if (RoleEnum.Role.Lab.ToDescription() == roleName.ToString())
                {
                    var labOrders = _labOrderRepository.GetLabOrderFromLabOrderTest(_userService.Get((ClaimsPrincipal)User).BranchId).ToList();
                    ViewData["labOrderCount"] = labOrders.Count;
                    ViewData["labOrders"] = labOrders;
                }
                //add admin role
                //else if(RoleEnum.Role.Admin.ToDescription() == roleName.ToString())
                //{
                //    var labResults = _labResultRepository.GetLabTestNoApproved(_userService.Get((ClaimsPrincipal)User).BranchId).ToList();
                //    ViewData["labResultCount"] = labResults.Count;
                //    ViewData["labResult"] = labResults;
                //}
                else
                {
                    var payments = _paymentRepository.GetIPDPaymentUnderPercent();
                    ViewData["ipdPayemntCount"] = payments.Count;
                    ViewData["ipdPayments"] = payments;
                }
            }
            else
            {
                //var payments = _paymentRepository.GetIPDPaymentUnderPercent();
                ViewData["ipdPayemntCount"] = 0;
                ViewData["ipdPayments"] = "";
            }
           
            
            return View();
        }
    }
}
