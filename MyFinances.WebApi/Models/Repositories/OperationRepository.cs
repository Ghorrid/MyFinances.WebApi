using MyFinances.WebApi.Models.Domains;

namespace MyFinances.WebApi.Models.Repositories
{
    public class OperationRepository
    {
        private readonly MyFinancesContext _context;

        public OperationRepository(MyFinancesContext context)
        {
            _context = context;
        }

        public IEnumerable<Operation> Get()
        {
            return _context.Operations;
        }

        public IEnumerable<Operation> Get(int records, int page)
        {
            var operations = _context.Operations;
            
            return operations.Skip((page - 1) * records).Take(records).ToList();
        }


        public Operation Get(int id)
        {
            return _context.Operations.FirstOrDefault(o => o.Id == id);
        }

        public void Add (Operation operation)
        {
            operation.Date=DateTime.Now;
            _context.Operations.Add(operation);            
        }

        public void Update (Operation operation)
        {
            var OperationToUpdate = _context.Operations.First(x=> x.Id == operation.Id);
            OperationToUpdate.Name=operation.Name;
            OperationToUpdate.Description=operation.Description;
            OperationToUpdate.Value=operation.Value;
            OperationToUpdate.CategoryId=operation.CategoryId;
        }
        public void Delete (int id)
        {
            var  OperationToDelete = _context.Operations.First(x => x.Id == id);

            _context.Operations.Remove(OperationToDelete);

        }

    }
}
