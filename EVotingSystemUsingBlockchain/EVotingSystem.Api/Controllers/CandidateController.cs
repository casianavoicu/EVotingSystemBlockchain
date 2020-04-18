using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EVotingSystem.Application.Model;
using Microsoft.AspNetCore.Mvc;

namespace EVotingSystem.Api.Controllers
{
    [Route("[controller]")]
    public class CandidateController : Controller
    {
        [HttpGet]
        public IActionResult Index()
        {
            return Ok();
        }

        [HttpPost]
        [Route("AddCandidate")]
        public async Task<TransactionVoteModel> AddCandidate([FromBody]CreateCandidateModel candidateModel)
        {

            return new TransactionVoteModel()
            {
                MyProperty = 1
            };
        }
    }
}