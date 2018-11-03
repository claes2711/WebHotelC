using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebHotel.Data;
using WebHotel.Models;
using WebHotel.Models.BookingViewModels;

namespace WebHotel.Controllers
{
    [Authorize(Roles = "Customers")]
    public class BookingsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public BookingsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Bookings
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Booking.Include(b => b.TheCustomer).Include(b => b.TheRoom);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Bookings/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var booking = await _context.Booking
                .Include(b => b.TheCustomer)
                .Include(b => b.TheRoom)
                .SingleOrDefaultAsync(m => m.ID == id);
            if (booking == null)
            {
                return NotFound();
            }

            return View(booking);
        }

        // GET: Bookings/Search
        public IActionResult Search()
        {
            ViewData["Email"] = new SelectList(_context.Customer, "Email", "Email");
            ViewData["RoomID"] = new SelectList(_context.Room, "ID", "Level");
            return View();
        }

        // POST: Bookings/Search
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Search(MakeBookings booking)
        {
            if (ModelState.IsValid)
            {
                var roomBooking = new Booking
                {
                    RoomID = booking.RoomID,
                    CheckIn = booking.CheckIn,
                    CheckOut = booking.CheckOut,
                    Email = booking.Email
                };
                
                var theRoom = await _context.Room.FindAsync(booking.RoomID);
                string _email = User.FindFirst(ClaimTypes.Name).Value;
                var customer = await _context.Customer.FindAsync(_email);
                var start = DateTime.Parse(booking.CheckIn);
                var end = DateTime.Parse(booking.CheckOut);
                TimeSpan dateDiff = end.Subtract(start);
                int dd = (int)dateDiff.TotalDays;
                roomBooking.Cost = dd * theRoom.Price;

                ViewBag.Room = booking.RoomID;
                ViewBag.TotalCost = roomBooking.Cost;
                ViewBag.Checkout = booking.CheckOut;
                ViewBag.Checkin = booking.CheckIn;
                ViewBag.Night = dd;

            }
            ViewData["Email"] = new SelectList(_context.Customer, "Email", "Email", booking.Email);
            ViewData["RoomID"] = new SelectList(_context.Room, "ID", "Level", booking.RoomID);
            return View(booking);
        }

        // GET: Bookings/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var booking = await _context.Booking.SingleOrDefaultAsync(m => m.ID == id);
            if (booking == null)
            {
                return NotFound();
            }
            ViewData["Email"] = new SelectList(_context.Customer, "Email", "Email", booking.Email);
            ViewData["RoomID"] = new SelectList(_context.Room, "ID", "Level", booking.RoomID);
            return View(booking);
        }

        // POST: Bookings/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,RoomID,Email,CheckIn,CheckOut,Cost")] Booking booking)
        {
            if (id != booking.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(booking);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BookingExists(booking.ID))
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
            ViewData["Email"] = new SelectList(_context.Customer, "Email", "Email", booking.Email);
            ViewData["RoomID"] = new SelectList(_context.Room, "ID", "Level", booking.RoomID);
            return View(booking);
        }

        // GET: Bookings/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var booking = await _context.Booking
                .Include(b => b.TheCustomer)
                .Include(b => b.TheRoom)
                .SingleOrDefaultAsync(m => m.ID == id);
            if (booking == null)
            {
                return NotFound();
            }

            return View(booking);
        }

        // POST: Bookings/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var booking = await _context.Booking.SingleOrDefaultAsync(m => m.ID == id);
            _context.Booking.Remove(booking);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool BookingExists(int id)
        {
            return _context.Booking.Any(e => e.ID == id);
        }
    }
}
