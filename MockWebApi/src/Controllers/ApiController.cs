using ErrorOr;
using Microsoft.AspNetCore.Mvc;

namespace MockWebApi.Controllers {
	[Route("[controller]")]
	[ApiController]
	public class ApiController : ControllerBase {
		protected IActionResult ProblemApi(List<Error> errors) {
			return ProblemApi(ref errors);
		}

		protected IActionResult ProblemApi(ref List<Error> errors) {
			int code = StatusCodes.Status500InternalServerError;

			switch (errors[0].Type) {
				case ErrorType.Failure:
					code = StatusCodes.Status417ExpectationFailed;
					break;
				case ErrorType.Validation:
					code = StatusCodes.Status400BadRequest;
					break;
				case ErrorType.Conflict:
					code = StatusCodes.Status409Conflict;
					break;
				case ErrorType.NotFound:
					code = StatusCodes.Status404NotFound;
					break;
				case ErrorType.Unauthorized:
					code = StatusCodes.Status401Unauthorized;
					break;
			}

			return Problem(
				statusCode: code,
				instance: code.ToString(), 
				detail: errors[0].Description + " TestTest");
		}
	}
}