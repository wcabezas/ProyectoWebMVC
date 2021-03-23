using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Sistema.Datos;
using Sistema.Entidades.Almacen;
using Sistema.Web.Models.Almacen.Articulo;

namespace Sistema.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ArticulosController : ControllerBase
    {
        private readonly DbContextSistema _context;

        public ArticulosController(DbContextSistema context)
        {
            _context = context;
        }

        // GET: api/Articulos/Listar
        [HttpGet("[action]")]
        public async Task<IEnumerable<ArticuloViewModel>> Listar()
        {
            var articulo = await _context.Articulos.Include(a => a.categoria).ToListAsync();
            return articulo.Select(a => new ArticuloViewModel
            {
                idarticulo = a.idarticulo,
                idcategoria = a.idcategoria,
                categoria = a.categoria.nombre,
                codigo = a.codigo,
                nombre = a.nombre,
                stock = a.stock,
                precio_venta = a.precio_venta,
                descripcion = a.descripcion,
                condicion = a.condicion
            });
        }

              // GET: api/Articulos/Mostrar/1
              [HttpGet("[action]/{id}")]
              public async Task<IActionResult> Mostrar([FromRoute] int id)
              {

                  var articulo = await _context.Articulos.Include(a => a.categoria).
                SingleOrDefaultAsync(a => a.idarticulo == id);

                  if (articulo == null)
                  {
                      return NotFound();
                  }

                  return Ok(new ArticuloViewModel
                  {
                      idarticulo = articulo.idarticulo,
                      idcategoria = articulo.idcategoria,
                      categoria = articulo.categoria.nombre,
                      codigo = articulo.codigo,
                      nombre = articulo.nombre,
                      stock = articulo.stock,
                      precio_venta = articulo.precio_venta,
                      descripcion = articulo.descripcion,
                      condicion = articulo.condicion
                  });
              }
        /*
              // PUT: api/Categorias/Actualizar
              [HttpPut("[action]")]
              public async Task<IActionResult> Actualizar([FromBody] ActualizarViewModel model)
              {
                  if (!ModelState.IsValid)
                  {
                      return BadRequest(ModelState);
                  }

                  if (model.idcategoria <= 0)
                  {
                      return BadRequest();
                  }

                  var categoria = await _context.Categorias.FirstOrDefaultAsync(
                      c => c.idcategoria == model.idcategoria);

                  if (categoria == null) { return NotFound(); }

                  categoria.nombre = model.nombre;
                  categoria.descripcion = model.descripcion;

                  try
                  {
                      await _context.SaveChangesAsync();
                  }
                  catch (DbUpdateConcurrencyException)
                  {
                      //Guardar Exepcion
                      return BadRequest();
                  }

                  return Ok();
              }

              // POST: api/Categorias/Crear
              [HttpPost("[action]")]
              public async Task<IActionResult> Crear([FromBody] CrearViewModel model)
              {
                  if (!ModelState.IsValid)
                  {
                      return BadRequest(ModelState);
                  }

                  Categoria categoria = new Categoria
                  {
                      nombre = model.nombre,
                      descripcion = model.descripcion,
                      condicion = true
                  };

                  _context.Categorias.Add(categoria);

                  try
                  {
                      await _context.SaveChangesAsync();
                  }
                  catch (Exception ex)
                  {

                      return BadRequest();
                  }

                  return Ok();
              }

              // DELETE: api/Categorias/Eliminar/1
              [HttpDelete("[action]/{id}")]
              public async Task<IActionResult> Eliminar([FromRoute] int id)
              {
                  if (!ModelState.IsValid)
                  {
                      return BadRequest(ModelState);
                  }

                  var categoria = await _context.Categorias.FindAsync(id);
                  if (categoria == null)
                  {
                      return NotFound();
                  }

                  _context.Categorias.Remove(categoria);

                  try
                  {
                      await _context.SaveChangesAsync();
                  }
                  catch (Exception ex)
                  {

                      return BadRequest();
                  }
                  await _context.SaveChangesAsync();

                  return Ok(categoria);
              }

              // PUT: api/Categorias/Desactivar/1
              [HttpPut("[action]/{id}")]
              public async Task<IActionResult> Desactivar([FromRoute] int id)
              {

                  if (id <= 0)
                  {
                      return BadRequest();
                  }

                  var categoria = await _context.Categorias.FirstOrDefaultAsync(
                      c => c.idcategoria == id);

                  if (categoria == null) { return NotFound(); }

                  categoria.condicion = false;

                  try
                  {
                      await _context.SaveChangesAsync();
                  }
                  catch (DbUpdateConcurrencyException)
                  {
                      //Guardar Exepcion
                      return BadRequest();
                  }

                  return Ok();
              }

              // PUT: api/Categorias/Activar/1
              [HttpPut("[action]/{id}")]
              public async Task<IActionResult> Activar([FromRoute] int id)
              {

                  if (id <= 0)
                  {
                      return BadRequest();
                  }

                  var categoria = await _context.Categorias.FirstOrDefaultAsync(
                      c => c.idcategoria == id);

                  if (categoria == null) { return NotFound(); }

                  categoria.condicion = true;

                  try
                  {
                      await _context.SaveChangesAsync();
                  }
                  catch (DbUpdateConcurrencyException)
                  {
                      //Guardar Exepcion
                      return BadRequest();
                  }

                  return Ok();
              }

              private bool ArticuloExists(int id)
              {
                  return _context.Articulos.Any(e => e.idarticulo == id);
              }*/
    }
}