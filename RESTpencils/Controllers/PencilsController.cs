using Microsoft.AspNetCore.Mvc;
using PencilLib;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace RESTpencils.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PencilsController : ControllerBase
    {
        private PencilsRepository repository;

        public PencilsController(PencilsRepository repo)
        {
            repository = repo;
        }

        // GET: api/<PencilsController>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public IEnumerable<Pencil> Get()
        {
            return repository.GetAll();
        }

        // GET api/<PencilsController>/5
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<Pencil> Get(int id)
        {
            Pencil? p = repository.GetById(id);
            if (p == null) return NotFound();
            return Ok(p);
        }

        // POST api/<PencilsController>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<Pencil> Post([FromBody] Pencil value)
        {
            try
            {
                Pencil p = repository.Add(value);
                string uri = Url.RouteUrl(RouteData.Values) + "/" + p.Id;
                return Created(uri, p);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // PUT api/<PencilsController>/5
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<Pencil> Put(int id, [FromBody] Pencil value)
        {
            try
            {
                Pencil? p = repository.Update(id, value);
                return Ok(p);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // DELETE api/<PencilsController>/5
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<Pencil> Delete(int id)
        {
            Pencil? p= repository.Delete(id);
            if (p == null) return NotFound();
            return Ok(p);
        }
    }
}
