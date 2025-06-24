using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Tienda_PrograVI.Data;
using Tienda_PrograVI.Models;

namespace Tienda_PrograVI.Controllers
{
    public class ProductoController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ProductoController(ApplicationDbContext context)
        {
            _context = context;
        }

   
        public async Task<IActionResult> Index()
        {
            var productos = await _context.Producto
                .Include(p => p.Categoria)
                .ToListAsync();

            ViewBag.Mensaje = TempData["Mensaje"];
            return View(productos);
        }

        public IActionResult Create()
        {
            var categorias = _context.Categoria.ToList(); 
            ViewData["Categoria"] = new SelectList(categorias, "Id_categoria", "Tipo_categoria");

            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Producto producto)
        {
            if (ModelState.IsValid)
            {
                _context.Add(producto);
                _context.SaveChanges();
                return RedirectToAction(nameof(Index));
            }

            ViewData["Categoria"] = new SelectList(_context.Categoria.ToList(), "Id_categoria", "Tipo_categoria", producto.Id_categoria);
            return View(producto);
        }




        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
                return NotFound();

            var producto = await _context.Producto.FindAsync(id);
            if (producto == null)
                return NotFound();

            CargarCategorias(producto.Id_categoria);
            return View(producto);
        }

       
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Producto producto)
        {
            if (id != producto.Id_producto)
                return NotFound();

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(producto);
                    await _context.SaveChangesAsync();

                    TempData["Mensaje"] = "Producto actualizado correctamente.";
                    return RedirectToAction(nameof(Index));
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!_context.Producto.Any(e => e.Id_producto == producto.Id_producto))
                        return NotFound();
                    else
                        throw;
                }
            }

            CargarCategorias(producto.Id_categoria);
            return View(producto);
        }

      
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
                return NotFound();

            var producto = await _context.Producto
                .Include(p => p.Categoria)
                .FirstOrDefaultAsync(p => p.Id_producto == id);

            if (producto == null)
                return NotFound();

            return View(producto);
        }

     
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var producto = await _context.Producto.FindAsync(id);
            if (producto != null)
            {
                _context.Producto.Remove(producto);
                await _context.SaveChangesAsync();
                TempData["Mensaje"] = "Producto eliminado correctamente.";
            }
            return RedirectToAction(nameof(Index));
        }

        private void CargarCategorias(int? seleccionada = null)
        {
            ViewData["Categoria"] = new SelectList(
                _context.Categoria.ToList(),
                "Id_categoria",
                "Tipo_categoria",
                seleccionada
            );
        }
    }
}
