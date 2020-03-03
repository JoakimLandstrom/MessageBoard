using System;
using System.ComponentModel.DataAnnotations;
using App.Models;
using App.Services;
using Microsoft.AspNetCore.Mvc;

namespace App.Controllers
{
    public class MessageController : ApiBase
    {
        private readonly IMessageService _service;

        public MessageController(IMessageService service)
        {
            _service = service;
        }

        [HttpGet]
        public ActionResult<string> GetAll()
        {
            return Ok(_service.GetAll());
        }

        [HttpPut]
        public ActionResult<Message> Update([FromBody][Required] Message message)
        {
            return Ok(_service.Update(message, User.Identity.Name));
        }

        [HttpPost]
        public ActionResult<Message> Create([FromBody][Required] Message message)
        {
            return Ok(_service.Create(message, User.Identity.Name));
        }

        [HttpDelete("{id}")]
        public ActionResult<Message> Delete(Guid id)
        {
            _service.Delete(id, User.Identity.Name);
            return Ok();
        }
    }
}