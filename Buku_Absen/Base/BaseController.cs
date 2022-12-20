using Buku_Absen.Context;
using Buku_Absen.Model;
using Buku_Absen.Repository.Interface;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace Buku_Absen.Base
{
   [Route("api/[controller]")]
   [ApiController]
    public class BaseController<Entity, Repository, Key>: ControllerBase
        where Entity : class
        where Repository : IRepository<Entity, Key>
    {
        private readonly Repository repository;

        public BaseController(Repository repository)
        {
            this.repository = repository;
        }
        [HttpGet]
        public ActionResult<Entity> Get()
        {
            var get = repository.Get();
            if(get.Count()!= 0)
            {
                return StatusCode(200,
                    new
                    {
                        status = HttpStatusCode.OK,
                        message = get.Count() + "Data Berhasil Ditemuka",
                        Data = get
                    });
            }
            else
            {
                return StatusCode(404,
                    new
                    {
                        status = HttpStatusCode.InternalServerError,
                        message = "Data Tidak Ditemuka",
                        Data = get
                    });
            }
        }
        [HttpGet]
        [Route("{key}")]
        public ActionResult Get(Key key)
        {
            var get = repository.Get(key);
            if(get != null)
            {
                return StatusCode(200,
                    new
                    {
                        status = HttpStatusCode.OK,
                        message = "Data Berhasil Ditemukan",
                        Data = get
                    });
            }
            else
            {
                return StatusCode(404,
                    new
                    {
                        status = HttpStatusCode.InternalServerError,
                        mesasge = "Data Tidak Ditemukan",
                        Data = get
                    });
            }
        }
        [HttpPost]
        public ActionResult Insert(Entity entity)
        {
            var insert = repository.Insert(entity);
            if(insert >= 1)
            {
                return StatusCode(200,
                    new
                    {
                        status = HttpStatusCode.OK,
                        message = "Data Berhasil Dimasukan",
                        Data = insert
                    });
            }
            else
            {
                return StatusCode(404,
                    new
                    {
                        status = HttpStatusCode.InternalServerError,
                        message = "Data Gagal Dimasukan",
                        Data = insert
                    });
            }
        }
        [HttpPut]
        public ActionResult Update(Entity entity, Key key)
        {
            var update = repository.update(entity, key);
            if(update >= 1)
            {
                return StatusCode(200,
                    new
                    {
                        status = HttpStatusCode.OK,
                        message = "Data Berhasil Diupdate",
                        Data = update
                    });
            }
            else
            {
                return StatusCode(404,
                    new
                    {
                        status = HttpStatusCode.InternalServerError,
                        message = "Data Gagal Diupdate",
                        Data = update
                    });
            }
        }
        [HttpDelete]
        [Route("{key}")]
        public ActionResult Delete(Key key)
        {
            var delete = repository.Delete(key);
            if(delete >= 1)
            {
                return StatusCode(200,
                    new
                    {
                        status = HttpStatusCode.OK,
                        message = "Data Berhasil Dihapus",
                        Data = delete
                    });
            }
            else if (delete == 0)
            {
                return StatusCode(404,
                    new
                    {
                        status = HttpStatusCode.OK,
                        message = "Data Gagal Dihapus",
                        Data = delete
                    });
            }
            else
            {
                return StatusCode(404,
                    new
                    {
                        status = HttpStatusCode.InternalServerError,
                        message = "Error",
                        Data = delete
                    });
            }
        }
    }
    
}
