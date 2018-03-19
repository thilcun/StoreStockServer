using ConfereEstoque.Contracts;
using ConfereEstoque.Core;
using ConfereEstoque.Data;
using ConfereEstoque.Entities;
using ConfereEstoque.Utils;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;

namespace ConfereEstoque.Controllers
{
    [Export]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class ItemAjusteController : ApiControllerBase
    {
        public ItemAjusteController()
        {
            if (ObjectBase.Container != null)
                ObjectBase.Container.SatisfyImportsOnce(this);
        }
        public ItemAjusteController(IDataRepositoryFactory dataRepositoryFactory)
        {
            _DataRepositoryFactory = dataRepositoryFactory;
        }
        [Import]
        IDataRepositoryFactory _DataRepositoryFactory;

        [HttpPost]
        public IHttpActionResult Salvar(ItemAjuste item)
        {
            return GetHttpResponse(() => {
                var repo = _DataRepositoryFactory.GetDataRepository<IItemAjusteRepository>();
                var found_itens = repo.Get(item.AjusteId, item.Produto.CodigoProduto);
                ItemAjuste returnitem = null;
                if (found_itens == null || found_itens.Count() == 0)
                {
                    int totalitens = repo.Get().Where(i => i.AjusteId == item.AjusteId).Count();
                    item.Alterado = DateTime.Now;
                    item.ItemNumero = totalitens + 1;
                    repo.Add(item);
                    returnitem = item;
                }
                else if (found_itens.Count() == 1)
                {
                    ItemAjuste founditem = found_itens.SingleOrDefault();
                    founditem.Quantidade += item.Quantidade;
                    repo.Update(founditem);
                    returnitem = founditem;
                }
                Messenger.Default.Send(returnitem);

                return Ok();
            });
            
        }
    }

    
}
