using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Seton_Backend;
using Seton_Backend.Models.DTOs;
using System;
using System.Linq;
using System.Security.Claims;

namespace Seton_Backend.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class NotesController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public NotesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: /notes
        [HttpGet]
        public IActionResult GetNotes()
        {
            var userId = GetUserId();
            var notes = _context.Notes
                .Where(n => n.UserId == userId)
                .ToList();
            return Ok(notes);
        }

        // POST: /notes
        [HttpPost]
        public IActionResult CreateNote([FromBody] NoteCreateRequest request)
        {
            var userId = GetUserId();

            var note = new Note
            {
                Title = request.Title,
                Type = request.Type,
                Content = request.Content,
                UserId = userId
            };

            _context.Notes.Add(note);
            _context.SaveChanges();

            return Ok(note);
        }


        // GET: /notes/{id}
        [HttpGet("{id}")]
        public IActionResult GetNote(int id)
        {
            var userId = GetUserId();
            var note = _context.Notes
                .FirstOrDefault(n => n.Id == id && n.UserId == userId);

            if (note == null)
                return NotFound();

            return Ok(note);
        }

        // PUT: /notes/{id}
        [HttpPut("{id}")]
        public IActionResult UpdateNote(int id, [FromBody] Note updatedNote)
        {
            var userId = GetUserId();
            var note = _context.Notes.FirstOrDefault(n => n.Id == id && n.UserId == userId);

            if (note == null)
                return NotFound();

            note.Content = updatedNote.Content;
            _context.SaveChanges();

            return Ok(note);
        }

        // DELETE: /notes/{id}
        [HttpDelete("{id}")]
        public IActionResult DeleteNote(int id)
        {
            var userId = GetUserId();
            var note = _context.Notes.FirstOrDefault(n => n.Id == id && n.UserId == userId);

            if (note == null)
                return NotFound();

            _context.Notes.Remove(note);
            _context.SaveChanges();

            return NoContent();
        }

        // Helper: Get logged-in user's ID
        private int GetUserId()
        {
            return int.Parse(User.Claims.First(c => c.Type == "id").Value);
        }
    }
}
