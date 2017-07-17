using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SnippetApp.Models;

namespace SnippetApp.Controllers
{
    public class SnippetsController : Controller
    {
        private readonly MyDbContext _context;

        public SnippetsController(MyDbContext context)
        {
            _context = context;    
        }

        // GET: Snippets
        // public async Task<IActionResult> Index()
        // {
        //     return View(await _context.Snippets.ToListAsync());
        // }
        public async Task<IActionResult> Index(string descriptionSearch, string languageSearch)
        
        {
            var snippets = from snippet in _context.Snippets select snippet;

            if (!String.IsNullOrEmpty(descriptionSearch))
            {
                snippets = snippets.Where(snippet => snippet.Description.ToLower().Contains(descriptionSearch.ToLower()));
            }

            if (!String.IsNullOrEmpty(languageSearch))
            {
                snippets = snippets.Where(snippet => snippet.Language.ToLower().Contains(languageSearch.ToLower()));
            }

            return View(await snippets.ToListAsync());
        }

        // GET: Snippets/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var snippet = await _context.Snippets.SingleOrDefaultAsync(m => m.ID == id);
            if (snippet == null)
            {
                return NotFound();
            }

            return View(snippet);
        }

        // GET: Snippets/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Snippets/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,Title,Description,CodeSnippet,Language")] Snippet snippet)
        {
            if (ModelState.IsValid)
            {
                _context.Add(snippet);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(snippet);
        }

        // GET: Snippets/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var snippet = await _context.Snippets.SingleOrDefaultAsync(m => m.ID == id);
            if (snippet == null)
            {
                return NotFound();
            }
            return View(snippet);
        }

        // POST: Snippets/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,Title,Description,CodeSnippet,Language")] Snippet snippet)
        {
            if (id != snippet.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(snippet);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SnippetExists(snippet.ID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction("Index");
            }
            return View(snippet);
        }

        // GET: Snippets/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var snippet = await _context.Snippets.SingleOrDefaultAsync(m => m.ID == id);
            if (snippet == null)
            {
                return NotFound();
            }

            return View(snippet);
        }

        // POST: Snippets/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var shippet = await _context.Snippets.SingleOrDefaultAsync(m => m.ID == id);
            _context.Snippets.Remove(shippet);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        private bool SnippetExists(int id)
        {
            return _context.Snippets.Any(e => e.ID == id);
        }
    }
}
