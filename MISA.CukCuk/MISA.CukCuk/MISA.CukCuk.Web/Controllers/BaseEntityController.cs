using Microsoft.AspNetCore.Mvc;
using MISA.ApplicationCore.Enums;
using MISA.ApplicationCore.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MISA.CukCuk.Web.Controllers
{
    [Route("v1/[controller]")]
    [ApiController]
    public class BaseEntityController<Generic> : ControllerBase
    {
        #region DECLARE
        IBaseService<Generic> _baseService;
        #endregion

        #region Contructor
        public BaseEntityController(IBaseService<Generic> baseService)
        {
            _baseService = baseService;
        }
        #endregion

        #region Method
        // GET: api/<BaseEntityController>
        [HttpGet]
        public IActionResult Get()
        {
            // gọi function lấy dữ liệu
            var data = _baseService.Get();

            //trả về dữ liệu
            return Ok(data);
        }

        // GET api/<BaseEntityController>/5
        [HttpGet("{id}")]
        public IActionResult Get(Guid id)
        {
            // gọi function lấy dữ liệu
            var data = _baseService.GetById(id);

            //trả về dữ liệu
            return Ok(data);
        }

        // POST api/<BaseEntityController>
        [HttpPost]
        public IActionResult Post([FromBody] Generic data)
        {
            // gọi function lấy dữ liệu
            var serviceResult = _baseService.Insert(data);

            //trả về dữ liệu
            if (serviceResult.MISACode == MISAEnum.NotValid)
            {
                return BadRequest(serviceResult);
            }
            if (serviceResult.MISACode == MISAEnum.IsValid)
            {
                return Created("EntityData", data);
            }
            else
            {
                return NoContent();
            }
        }

        // POST api/<BaseEntityController>
        [HttpPost("import")]
        public IActionResult Import(string path)
        {
            // gọi function lấy dữ liệu
            var serviceResult = _baseService.ProcessDataImport(path);

            //trả về dữ liệu
            return Ok(serviceResult);
        }

        // PUT api/<BaseEntityController>/5
        [HttpPut("{id}")]
        public IActionResult Put([FromRoute]Guid id, [FromBody] Generic data)
        {

            // gọi function lấy dữ liệu
            var serviceResult = _baseService.Update(id, data);

            //trả về dữ liệu
            if (serviceResult.MISACode == MISAEnum.NotValid)
            {
                return BadRequest(serviceResult.Data);
            }
            if (serviceResult.MISACode == MISAEnum.IsValid || serviceResult.MISACode == MISAEnum.Success)
            {
                return Created("EntityData", data);
            }
            else
            {
                return NoContent();
            }
        }

        // DELETE api/<BaseEntityController>/5
        [HttpDelete("{id}")]
        public IActionResult Delete(Guid id)
        {
            // gọi function lấy dữ liệu
            var serviceResult = _baseService.DeleteById(id);

            //trả về dữ liệu
            if (serviceResult.MISACode == MISAEnum.NotValid)
            {
                return BadRequest(serviceResult);
            }
            if (serviceResult.MISACode == MISAEnum.IsValid || serviceResult.MISACode == MISAEnum.Success)
            {
                return Ok(serviceResult);
            }
            else
            {
                return NoContent();
            }
        }
        #endregion
    }
}
