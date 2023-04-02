using System.Collections.Generic;
using capturerservice.Model;
using capturerservice.Services;
using Microsoft.AspNetCore.Mvc;

namespace capturer_service.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WordsController : ControllerBase
    {
        private WordsService _wordsService;

        public WordsController(WordsService service)
        {
            _wordsService = service;
        }

        [HttpGet("{name}")]
        public WordElement Create(string name)
        {
            return _wordsService.Create(name);
        }

        [HttpGet]
        public List<WordElement> Get()
        {
            return _wordsService.Get();
        }
    }
}
