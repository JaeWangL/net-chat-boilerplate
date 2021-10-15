namespace NetChatBoilerplate.API.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using NetChatBoilerplate.API.Constants;
    using NetChatBoilerplate.Domain.AggregatesModel.Profile;
    using Swashbuckle.AspNetCore.Annotations;

    [Route("[controller]")]
    [ApiController]
    [ApiVersion(ApiConstants.V1)]
    [SwaggerResponse(
        StatusCodes.Status500InternalServerError,
        "The MIME type in the Accept HTTP header is not acceptable.",
        typeof(ProblemDetails))]
    public class TestController : ControllerBase
    {
        private readonly IDoctorRepository _doctorRepo;

        public TestController(IDoctorRepository doctorRepo)
        {
            this._doctorRepo = doctorRepo ?? throw new ArgumentNullException(nameof(doctorRepo));
        }

        [HttpGet("all")]
        [SwaggerResponse(StatusCodes.Status200OK, "A collection of doctos for all data", typeof(DoctorEntity))]
        public async Task<IEnumerable<DoctorEntity>> GetTest() => await this._doctorRepo.FindAllAsync();

        [HttpPost("")]
        [SwaggerResponse(StatusCodes.Status201Created, "New doctor was created", typeof(DoctorEntity))]
        public async Task<DoctorEntity> Create()
        {
            var newEntity = this._doctorRepo.Create(new DoctorEntity("0", "Test", "TestURl"));
            await this._doctorRepo.UnitOfWork.SaveChangesAsync();

            return newEntity;
        }
    }
}
