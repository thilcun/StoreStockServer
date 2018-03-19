using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;

namespace ConfereEstoque.Core
{
    public class ApiControllerBase : ApiController
    {
        protected IHttpActionResult GetHttpResponse(Func<IHttpActionResult> codeToExecute)
        {
            try
            {
                return codeToExecute.Invoke();
            }
            catch (Exception ex)
            {
                return Content(HttpStatusCode.InternalServerError, ex.Message);
            }
            
        }

    }
}
