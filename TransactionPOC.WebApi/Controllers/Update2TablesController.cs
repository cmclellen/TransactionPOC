using TransactionPOC.BLL.Services;
using TransactionPOC.Core.Utils;

namespace TransactionPOC.WebApi.Controllers
{
    public class Update2TablesController
    {
        public Update2TablesController(IUpdate2TablesService update2TablesService)
        {
            Guard.NotNull(() => update2TablesService, update2TablesService);
            Update2TablesService = update2TablesService;
        }

        private IUpdate2TablesService Update2TablesService { get; set; }

        public void Update2Tables()
        {
            Update2TablesService.Update2Tables();
        }
    }
}