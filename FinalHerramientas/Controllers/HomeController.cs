using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using RecuperatorioMVCCore.DAL;
using RecuperatorioMVCCore.Models;

namespace RecuperatorioMVCCore.Controllers
{
    public class HomeController : Controller
    {
        private readonly MateDBContext _context;

        public HomeController()
        {
            _context = new MateDBContext();
        }
        [HttpGet]
        public IActionResult Index()
        {
            IndexVM model = new IndexVM();
            model.OperadorA = 1;
            model.OperadorB = 4;

            this.eliminarregistros();

            for (int i = 1; i < 11; i++)
            {
                Operacion operacion = new Operacion();
                operacion.Valor = $"2*{i}={2 * i}";
                _context.Operaciones.Add(operacion);
                _context.SaveChanges();
            }
            model.Operaciones = _context.Operaciones.ToList();
            return View(model);
        }

        private void eliminarregistros()
        {
            List<Operacion> operaciones = new List<Operacion>();
            operaciones = _context.Operaciones.ToList();
            foreach (var operacion in operaciones)
            {
                _context.Operaciones.Remove(operacion);
                _context.SaveChanges();
            }
        }
        [HttpPost]
        public IActionResult Index(IndexVM model,
            string sumar,
            string multiplicar,
            string sumapredefinida,
            string vertipo,
            string eliminartipo)
        {
            IActionResult result = null;
            ModelState.Clear();

            if (sumar != null)
                result = this.sumar(model);
            else if (multiplicar != null)
                result = this.multiplicar(model);
            else if (sumapredefinida != null)
                result = this.sumapredefinida(model);
            else if (vertipo != null)
                result = this.vertipo(model);
            else if (eliminartipo != null)
                result = this.eliminartipo(model);
            return result;
        }
        private IActionResult sumar(IndexVM model)
        {
            if (ModelState.IsValid)
            {
                model.Resultado = model.OperadorA + model.OperadorB;
                model.OperacionMat = "+";

                Operacion operacion = new Operacion();
                operacion.Valor = model.OperadorA + model.OperacionMat + model.OperadorB + "=" + model.Resultado;
                _context.Operaciones.Add(operacion);
                _context.SaveChanges();

                model.Id = operacion.Id;

                model.Operaciones = _context.Operaciones.ToList();
            }
            return View("Index", model);
        }

        private IActionResult sumapredefinida(IndexVM model)
        {
            this.eliminarregistros();

            model.Resultado = 111 + 1;
       
            Operacion operacion = new Operacion();
            operacion.Valor = $"111 + 1 = 112";
            _context.Operaciones.Add(operacion);
            _context.SaveChanges();

            model.Id = operacion.Id;

            model.Operaciones = _context.Operaciones.ToList();

            return View("Index", model);
        }
        private IActionResult vertipo(IndexVM model)
        {
            string operacionbuscar = string.Format("{0}+{1}", model.OperadorA, model.OperadorB);
            model.Operaciones = _context.Operaciones.Where(m => m.Valor.Contains(operacionbuscar)).ToList();
            return View(model);
        }
        private IActionResult eliminartipo(IndexVM model)
        {
            List<Operacion> operacionesEliminar = new List<Operacion>();

            string operacionbuscar = string.Format("{0}*{1}", model.OperadorA, model.OperadorB);
            operacionesEliminar = _context.Operaciones.Where(m => m.Valor.Contains(operacionbuscar)).ToList();
            _context.Operaciones.RemoveRange(operacionesEliminar);
            _context.SaveChanges();

            model.Operaciones = _context.Operaciones.ToList();

            return View(model);
        }

        private IActionResult multiplicar(IndexVM model)
        {
            if (ModelState.IsValid)
            {
                model.Resultado = model.OperadorA * model.OperadorB;
                model.OperacionMat = "*";

                Operacion operacion = new Operacion();
                operacion.Valor = model.OperadorA + model.OperacionMat + model.OperadorB + "=" + model.Resultado;
                _context.Operaciones.Add(operacion);
                _context.SaveChanges();

                model.Id = operacion.Id;

                model.Operaciones = _context.Operaciones.ToList();
            }
            return View("Index", model);
        }
        
        public IActionResult eliminarID(int id)
        {
            IndexVM model = new IndexVM();

            Operacion operacion = _context.Operaciones.Find(id);
            _context.Operaciones.Remove(operacion);
            _context.SaveChanges();

            model.Operaciones = _context.Operaciones.ToList();

            return View("Index", model);
        }

        public IActionResult eliminartodo()
        {
            this.eliminarregistros();

            IndexVM model = new IndexVM();

            model.Operaciones = _context.Operaciones.ToList();

            return View("Index", model);
        }

        public IActionResult cambiarpor()
        {
            IndexVM model = new IndexVM();
            List<Operacion> operacionbuscar = new List<Operacion>();

            operacionbuscar = _context.Operaciones.Where(m => m.Valor.Contains("=")).ToList();

            foreach (var operacion in operacionbuscar)
            {
                operacion.Valor = operacion.Valor.Replace("=", ">>>");
            }
            _context.SaveChanges();

            model.Operaciones = _context.Operaciones.ToList();

            return View("Index", model);
        }

    }
}
