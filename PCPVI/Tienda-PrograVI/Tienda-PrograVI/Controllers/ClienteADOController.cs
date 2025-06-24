using Microsoft.AspNetCore.Mvc;
using Tienda_PrograVI.Models;
using Tienda_PrograVI.Data;
using System;

namespace Tienda_PrograVI.Controllers
{
    public class ClienteADOController : Controller
    {

 private readonly ClienteADORepository  _repo = new ClienteADORepository();
        public IActionResult Index() => View(_repo.GetAll());
        public IActionResult Create() => View();
        [HttpPost]
        public IActionResult Create(Cliente cliente)
        {
            if (ModelState.IsValid)
            {
                _repo.Insert(cliente);
                return RedirectToAction("Index");
            }
            return View(cliente);
        }
        public IActionResult Edit(int id)
        {
            var est = _repo.GetById(id);
            if (est == null) return NotFound();
            return View(est);
        }
        [HttpPost]
        public IActionResult Edit(Cliente cliente)
        {
            if (ModelState.IsValid)
            {
                _repo.Update(cliente);
                return RedirectToAction("Index");
            }
            return View(cliente);
        }
        public IActionResult Delete(int id)
        {
            var est = _repo.GetById(id);
            if (est == null) return NotFound();
            return View(est);
        }
        [HttpPost, ActionName("Delete")]
        public IActionResult DeleteConfirmed(int id)
        {
            _repo.Delete(id);
            return RedirectToAction("Index");
        }
    }
}