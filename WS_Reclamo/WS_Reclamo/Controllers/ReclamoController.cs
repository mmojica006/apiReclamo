using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Configuration;
using System.Web.Http;
using WS_Reclamo.Models;

namespace WS_Reclamo.Controllers
{
    public class ReclamoController : ApiController
    {
        private Reclamo  reclamo = new Reclamo();

        [HttpPost]
        public IHttpActionResult add(Reclamo reclamo)
        {
            String Response = "EMPTY";
            if (!ModelState.IsValid)
                return BadRequest("Not a valid data");

            try
            {
                if (existPerson(reclamo.NroDoc))
                {

           
                    String ReclamoTopaz = executeOperative(reclamo);

                    if (ReclamoTopaz.Contains("HTTP-200")){

                        Response = "HTTP-200: 0";

                    }else{
                        Response = ReclamoTopaz;
                    }
                }
                else
                {
                    Response = "No existe en sistema";
                }

            }
            catch (Exception ex)
            {
                Response = ex.Message.ToString();

            }



            return Ok(Response);

        }


        private String executeOperative(Reclamo entReclamo)
        {
            String result = "";
            try
            {
                Dictionary<String, String> parametros = new Dictionary<String, String>();
                parametros.Add("TipoEvento", entReclamo.TipoEvento);
                parametros.Add("PaisDoc", entReclamo.PaisDoc);
                parametros.Add("TipoDoc", entReclamo.TipoDoc);
                parametros.Add("NroDoc", entReclamo.NroDoc);
                parametros.Add("Credito", entReclamo.Credito.ToString());
                parametros.Add("MedioComunic", entReclamo.MedioComunic.ToString());
                parametros.Add("Categoria", entReclamo.Categoria.ToString());
                parametros.Add("Subcategoria", entReclamo.Subcategoria.ToString());
                parametros.Add("Comentario", entReclamo.Comentario);
                String usuario = WebConfigurationManager.AppSettings["TopazUserName"].ToString();
                String clave = WebConfigurationManager.AppSettings["TopazPassword"].ToString();

                TopMiddleHelperController topazHelper = new TopMiddleHelperController(usuario, clave, "Reclamos", parametros);
                String topazResult = topazHelper.execute("CodigoEstado", "DescEstado", "IdEvento");

                if (topazResult.Contains("HTTP-200"))
                   result = topazHelper.noReclamo;                 // result = "HTTP-200: 0";
                else
                    result = topazResult;
            }
            catch (Exception ex)
            {
                result = ex.Message.ToString();
            }

            return result;

        }
        private bool existPerson(string nroDoc)
        {
            bool result = false;
            var dataPersona = new CL_RelPerDoc();

            try
            {
                using (var ctx = new dbContext())
                {
                    dataPersona = ctx.CL_RelPerDoc.Where(x => x.NroDocumento == nroDoc).SingleOrDefault();
                   if (dataPersona !=null)
                    result=true;

                }

            }
            catch (Exception ex)
            {
                throw;
                string message = ex.Message.ToString();
                return false;
            }

            return result;

          



        }

    }
}
