using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using CodingCraft1.Context;
using CodingCraft1.Email;
using CodingCraft1.Models;
using FluentDateTime;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace CodingCraft1.Controllers
{
    [Authorize(Roles = "admin")]
    [RoutePrefix("Admin")]
    public class AdminController : ApiController
    {
    }
}
