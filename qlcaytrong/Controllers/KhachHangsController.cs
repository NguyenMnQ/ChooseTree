using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using qlcaytrong.Models;

namespace qlcaytrong.Controllers
{
    public class KhachHangsController : Controller
    {
        private readonly AppDbContext _context;

        public KhachHangsController(AppDbContext context)
        {
            _context = context;
        }

        // GET: KhachHangs
        public async Task<IActionResult> Index()
        {
            var appDbContext = _context.KhachHangs.Include(k => k.Role);
            return View(await appDbContext.ToListAsync());
        }

        // GET: KhachHangs/Details/5
        //public async Task<IActionResult> Details(int? id)
        //{
        //    if (id == null)
        //    {
        //        return NotFound();
        //    }

        //    var khachHang = await _context.KhachHangs
        //        .Include(k => k.Role)
        //        .FirstOrDefaultAsync(m => m.Makh == id);
        //    if (khachHang == null)
        //    {
        //        return NotFound();
        //    }

        //    return View(khachHang);
        //}
        public IActionResult Details()
        {
            var khachHang = from dh in _context.DonHangs
                          join kh in _context.KhachHangs on dh.Makh equals kh.Makh
                          where kh.Tendangnhap == HttpContext.Session.GetString("username")
                          select kh;
            return View(khachHang);
        }
        // GET: KhachHangs/Create
        public IActionResult Create()
        {
            ViewData["RoleId"] = new SelectList(_context.KhachHangRoles, "RoleId", "RoleId");
            return View();
        }

        // POST: KhachHangs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Makh,Hoten,Tendangnhap,Matkhau,Email,Diachi,Dienthoai,Ngaysinh,RoleId,Status,Resetpasswordcode")] KhachHang khachHang)
        {
            if (ModelState.IsValid)
            {
                _context.Add(khachHang);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["RoleId"] = new SelectList(_context.KhachHangRoles, "RoleId", "RoleId", khachHang.RoleId);
            return View(khachHang);
        }

        // GET: KhachHangs/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var khachHang = await _context.KhachHangs.FindAsync(id);
            if (khachHang == null)
            {
                return NotFound();
            }
            ViewData["RoleId"] = new SelectList(_context.KhachHangRoles, "RoleId", "RoleId", khachHang.RoleId);
            return View(khachHang);
        }

        // POST: KhachHangs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Makh,Hoten,Tendangnhap,Matkhau,Email,Diachi,Dienthoai,Ngaysinh,RoleId,Status,Resetpasswordcode")] KhachHang khachHang)
        {
            if (id != khachHang.Makh)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(khachHang);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!KhachHangExists(khachHang.Makh))
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
            ViewData["RoleId"] = new SelectList(_context.KhachHangRoles, "RoleId", "RoleId", khachHang.RoleId);
            return View(khachHang);
        }

        // GET: KhachHangs/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var khachHang = await _context.KhachHangs
                .Include(k => k.Role)
                .FirstOrDefaultAsync(m => m.Makh == id);
            if (khachHang == null)
            {
                return NotFound();
            }

            return View(khachHang);
        }

        // POST: KhachHangs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var khachHang = await _context.KhachHangs.FindAsync(id);
            _context.KhachHangs.Remove(khachHang);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool KhachHangExists(int id)
        {
            return _context.KhachHangs.Any(e => e.Makh == id);
        }
    }
}
