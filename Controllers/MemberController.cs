using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using LibraryManagerProject.Data;
using LibraryManagerProject.Models;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace LibraryManagerProject.Controllers
{
    public class MemberController : Controller
    {
        private readonly LibraryContext _context;

        public MemberController(LibraryContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var members = await _context.Members.ToListAsync();
            return View(members);
        }

        public async Task<IActionResult> Details(int id)
        {
            var member = await _context.Members.FirstOrDefaultAsync(m => m.Id == id);
            if (member == null)
            {
                return NotFound();
            }
            return View(member);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("FullName,Email,PhoneNumber,MembershipStartDate,MembershipEndDate")] Member member)
        {
            if (ModelState.IsValid)
            {
                // Convert to UTC before saving
                member.MembershipStartDate = DateTime.SpecifyKind(member.MembershipStartDate, DateTimeKind.Utc);
                if (member.MembershipEndDate.HasValue)
                {
                    member.MembershipEndDate = DateTime.SpecifyKind(member.MembershipEndDate.Value, DateTimeKind.Utc);
                }

                _context.Add(member);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(member);
        }

        public async Task<IActionResult> Edit(int id)
        {
            var member = await _context.Members.FindAsync(id);
            if (member == null)
            {
                return NotFound();
            }
            return View(member);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,FullName,Email,PhoneNumber,MembershipStartDate,MembershipEndDate")] Member member)
        {
            if (id != member.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    // Convert to UTC before saving
                    member.MembershipStartDate = DateTime.SpecifyKind(member.MembershipStartDate, DateTimeKind.Utc);
                    if (member.MembershipEndDate.HasValue)
                    {
                        member.MembershipEndDate = DateTime.SpecifyKind(member.MembershipEndDate.Value, DateTimeKind.Utc);
                    }

                    _context.Update(member);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MemberExists(member.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(member);
        }

        public async Task<IActionResult> Delete(int id)
        {
            var member = await _context.Members.FindAsync(id);
            if (member == null)
            {
                return NotFound();
            }
            return View(member);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var member = await _context.Members.FindAsync(id);
            if (member != null)
            {
                _context.Members.Remove(member);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Index));
        }

        private bool MemberExists(int id)
        {
            return _context.Members.Any(e => e.Id == id);
        }
    }
}
