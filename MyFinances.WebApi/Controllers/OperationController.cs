using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MyFinances.Core.Dtos;
using MyFinances.Core.Response;
using MyFinances.WebApi.Models;
using MyFinances.WebApi.Models.Converters;


namespace MyFinances.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OperationController : ControllerBase
    {
        private readonly UnitOfWork _unitOfWork;

        public OperationController(UnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        /// <summary>
        /// Get all operations 
        /// </summary>
        /// <returns>DataResponse - IEnumerable OperationDto </returns>
        [HttpGet]
        public DataResponse<IEnumerable<OperationDto>> Get()
        {
            var responce = new DataResponse<IEnumerable<OperationDto>>();

            try
            {
                responce.Data = _unitOfWork.Operation.Get().ToDtos();
            }
            catch (Exception ex)
            {
                //logowanie do pliku
                responce.Errors.Add
                    (new Error (ex.Source, ex.Message));                
            }            

            return responce;
        }

        /// <summary>
        /// Gets n records from m page
        /// </summary>
        /// <param name="records">number od records</param>
        /// <param name="page">number of page</param>
        /// <returns> DataResponse IEnumerable OperationDto </returns>
        [HttpGet("{records} , {page}")]
        public DataResponse<IEnumerable<OperationDto>> Get (int records , int page)
        {
            var responce = new DataResponse<IEnumerable<OperationDto>>();
            try
            {
                responce.Data = _unitOfWork.Operation.Get(records, page).ToDtos();
            }
            catch (Exception ex)
            {
                //logowanie do pliku
                responce.Errors.Add
                    (new Error(ex.Source, ex.Message));
            }

            return responce;

        }



        /// <summary>
        /// Get operation by id
        /// </summary>
        /// <param name="id"> Operation id </param>
        /// <returns>DataResponse - OperationDto </returns>
        [HttpGet("{id}")]
        public DataResponse<OperationDto> Get(int id)
        {
            var responce = new DataResponse<OperationDto>();

            try
            {
                responce.Data = _unitOfWork.Operation.Get(id).ToDto();
            }
            catch (Exception ex)
            {
                //logowanie do pliku
                responce.Errors.Add
                    (new Error(ex.Source, ex.Message));
            }

            return responce;
        }


        /// <summary>
        /// Add new Operation
        /// </summary>
        /// <param name="operation"> OperationDto </param>
        /// <returns></returns>
        [HttpPost]
        public DataResponse<int> Add(OperationDto operation)
        {
            var responce = new DataResponse<int>();

            try
            {
                _unitOfWork.Operation.Add(operation.ToDao());
                _unitOfWork.Complete();
                responce.Data = operation.Id;
            }
            catch (Exception ex)
            {
                //logowanie do pliku
                responce.Errors.Add
                    (new Error(ex.Source, ex.Message));
            }

            return responce;
        }

        /// <summary>
        /// Update operation
        /// </summary>
        /// <param name="operation"> OperationDto </param>
        /// <returns></returns>
        [HttpPut]
        public Response Update(OperationDto operation)
        {
            var responce = new Response();

            try
            {
                _unitOfWork.Operation.Update(operation.ToDao());
                _unitOfWork.Complete();
            }
            catch (Exception ex)
            {
                //logowanie do pliku
                responce.Errors.Add
                    (new Error(ex.Source, ex.Message));
            }

            return responce;
        }

        /// <summary>
        /// Delete operation
        /// </summary>
        /// <param name="id">Operation id</param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public Response Delete(int id)
        {
            var responce = new Response();            

            try
            {
                _unitOfWork.Operation.Delete(id);
                _unitOfWork.Complete();
            }
            catch (Exception ex)
            {
                //logowanie do pliku
                responce.Errors.Add
                    (new Error(ex.Source, ex.Message));
            }

            return responce;
        }

    }
}
