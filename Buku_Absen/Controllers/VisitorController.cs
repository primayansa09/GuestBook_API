using Buku_Absen.Base;
using Buku_Absen.Model;
using Buku_Absen.Repository.Data;
using Buku_Absen.Repository.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace Buku_Absen.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VisitorController : BaseController<Visitor, VisitorRepository, string>
    {
        private readonly VisitorRepository repository;
        public VisitorController(VisitorRepository visitorRepository) : base(visitorRepository)
        {
            this.repository = visitorRepository;
        }
    }
}
