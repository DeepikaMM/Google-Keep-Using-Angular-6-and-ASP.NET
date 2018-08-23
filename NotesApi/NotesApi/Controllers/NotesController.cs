using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NotesApi.Models;

namespace NotesApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NotesController : ControllerBase
    {
        private readonly NotesApiContext _context;

        public NotesController(NotesApiContext context)
        {
            _context = context;
        }

        // GET: api/Notes
        [HttpGet]
        public async Task<IEnumerable<Note>> GetNote()
        {
            var x = await(_context.Note.Include(p => p.checkList).Include
                (p => p.label).ToListAsync());
            return x;
        }

        // GET: api/Notes/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetNote([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var note =  await _context.Note.Include(p => p.checkList).Include
              (p => p.label).SingleOrDefaultAsync(p => p.Id == id);

            if (note == null)
            {
                return NotFound();
            }

            return Ok(note);
        }
        [HttpGet("{label}")]
        public async Task<IActionResult> GetTodoNotesLabel([FromQuery] string Label)
        {
            var NonNullDatas = _context.Note.Include(s => s.checkList).Include(s => s.label).Where(x => x.label != null);
            return Ok(await NonNullDatas.Where(x => x.label.Any(y => y.LabelName == Label)).ToListAsync());
        }
        [HttpGet("pin/{PinnedStatus}")]
        public async Task<IActionResult> GetTodoNotes([FromRoute] bool PinnedStatus)
        {
            IEnumerable<Note> todoNotes = await _context.Note.Include(p => p.checkList).Include(p => p.label).Where(p => p.PinStatus == PinnedStatus).ToListAsync();
            return Ok(todoNotes);
        }
        [HttpGet("title/{title}")]
        public async Task<IEnumerable<Note>> GetTodoNotesByTitle([FromRoute] string title)
        {
            var result = await _context.Note.Include(p => p.checkList).Include(p => p.label).Where(p => p.Title == title).ToListAsync();
            return result;

        }
        // PUT: api/Notes/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutNote([FromRoute] int id, [FromBody] Note note)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != note.Id)
            {
                return BadRequest();
            }

            //_context.Entry(note).State = EntityState.Modified;
            _context.Note.Update(note);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!NoteExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Notes
        [HttpPost]
        public async Task<IActionResult> PostNote([FromBody] Note note)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Note.Add(note);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetNote", new { id = note.Id }, note);
        }

        // DELETE: api/Notes/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteNote([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

             var note = await _context.Note.Include(s => s.label).Include(s => s.checkList).SingleOrDefaultAsync(s => s.Id == id);
            if (note == null)
            {
                return NotFound();
            }

            _context.Note.Remove(note);
            await _context.SaveChangesAsync();

            return Ok(note);
        }

        private bool NoteExists(int id)
        {
            return _context.Note.Any(e => e.Id == id);
        }
    }
}