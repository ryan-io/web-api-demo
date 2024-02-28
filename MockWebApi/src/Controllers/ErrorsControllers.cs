using Microsoft.AspNetCore.Mvc;

namespace MockWebApi.Controllers {
	public class ErrorsControllers : ApiController {
		[HttpGet("genericproblem")]
		public IActionResult Error() {
			return Problem();
		}
	}
}