using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace ShopThoiTrang.Dtos
{
    public class ApiResponse
    {
        public int? status { get; set; } = 200;
        public string message { get; set; } = "";
        public dynamic result { get; set; } = null;


        public virtual ApiResponse Status(HttpStatusCode code)
        {
            status = (int)code;
            return this;
        }

        public virtual ApiResponse Message(string message)
        {
            this.message = message;
            return this;
        }

        public virtual ApiResponse Result(dynamic result)
        {
            this.result = result;
            return this;
        }

        public virtual JsonResult Build()
        {
            return new JsonResult
            {
                Data = result,
                //va
            };
        }

        public virtual JsonResult BadRequest(string message = "bad request!")
        {
            status = 400;
            this.message = message;
            return Build();
        }

        public virtual JsonResult Ok(object result, string message = "ok")
        {
            this.result = result;
            status = 200;
            this.message = message;
            return Build();
        }

        public virtual JsonResult NoContent(string message = "no content.")
        {
            status = 204;
            this.message = "No Content";
            return Build();
        }
    }
}
