using Conetados.Webapi.Infraestrutura;
using Conetados.Webapi.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Http;

namespace Conetados.Webapi.Controllers.Base
{
    public abstract class ControllerCrudBase<TModel> : ControllerBase
        //where TModel: ModelBase
    {
        //private DbSet<TModel> modelDbSet = null;
        //private DbContext dbContext = null;
        
        //public ControllerCrudBase()
        //{
        //    this.dbContext = InjectorManager.GetInstance<DbContext>();// (DbContext)Configuration.DependencyResolver.GetService(typeof(DbContext));
        //    modelDbSet = this.dbContext.Set<TModel>();
        //}        

        //// GET: odata/Artigos
        //[EnableQuery]
        //public IQueryable<TModel> Get()
        //{
        //    return modelDbSet;
        //}

        //public TModel Get(int key)
        //{
        //    return modelDbSet.Find(key);
        //}

        //public TModel Post(TModel model)
        //{
        //    return modelDbSet.Add(model);
        //}

        //public TModel Delete(int[] keys)
        //{
        //    throw new NotImplementedException();
        //}

        //// GET: odata/Artigos(5)
        //[EnableQuery]
        //public SingleResult<TModel> GetModel([FromODataUri] int key)
        //{
        //    return SingleResult.Create(modelDbSet.Where(model => model.Id == key));
        //}

        //// PUT: odata/Artigos(5)
        //public IHttpActionResult Put([FromODataUri] int key, Delta<TModel> patch)
        //{
        //    Validate(patch.GetEntity());

        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest(ModelState);
        //    }

        //    TModel model = modelDbSet.Find(key);
        //    if (model == null)
        //    {
        //        return NotFound();
        //    }

        //    patch.Put(model);

        //    try
        //    {
        //        db.SaveChanges();
        //    }
        //    catch (DbUpdateConcurrencyException)
        //    {
        //        if (!ModelExists(key))
        //        {
        //            return NotFound();
        //        }
        //        else
        //        {
        //            throw;
        //        }
        //    }

        //    return Updated(model);
        //}

        //// POST: odata/Artigos
        //public IHttpActionResult Post(TModel model)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest(ModelState);
        //    }

        //    modelDbSet.Add(model);
        //    db.SaveChanges();

        //    return Created(model);
        //}

        //// PATCH: odata/Artigos(5)
        //[AcceptVerbs("PATCH", "MERGE")]
        //public IHttpActionResult Patch([FromODataUri] int key, Delta<TModel> patch)
        //{
        //    Validate(patch.GetEntity());

        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest(ModelState);
        //    }

        //    TModel model = modelDbSet.Find(key);
        //    if (model == null)
        //    {
        //        return NotFound();
        //    }

        //    patch.Patch(model);

        //    try
        //    {
        //        db.SaveChanges();
        //    }
        //    catch (DbUpdateConcurrencyException)
        //    {
        //        if (!ModelExists(key))
        //        {
        //            return NotFound();
        //        }
        //        else
        //        {
        //            throw;
        //        }
        //    }

        //    return Updated(model);
        //}

        //// DELETE: odata/Artigos(5)
        //public IHttpActionResult Delete([FromODataUri] int key)
        //{
        //    TModel model = modelDbSet.Find(key);
        //    if (model == null)
        //    {
        //        return NotFound();
        //    }

        //    modelDbSet.Remove(model);
        //    db.SaveChanges();

        //    return StatusCode(HttpStatusCode.NoContent);
        //}

        //protected override void Dispose(bool disposing)
        //{
        //    if (disposing)
        //    {
        //        db.Dispose();
        //    }
        //    base.Dispose(disposing);
        //}

        //private bool ModelExists(int key)
        //{
        //    return modelDbSet.Count(e => e.Id == key) > 0;
        //}
    }
}