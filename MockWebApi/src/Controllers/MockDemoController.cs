using Microsoft.AspNetCore.Mvc;
using MockWebApi.Library;
using MockWebApi.Models;
using MockWebApi.Services.MockDemoItem;

namespace MockWebApi.Controllers {
	public class MockDemoController : ApiController {
		[HttpPost("create")]
		public async Task<IActionResult> CreateMockDemoItem(CreateMockDemoRequest request) {
			await Task.Delay(1000); // mock

			// 'real' definition of our data; whats in the database
			var createNewResult = MockDemoModel.CreateNew(
				request.Name,
				request.Description,
				request.Data,
				request.CriticalDate);

			if (createNewResult.IsError)
				return ProblemInController(createNewResult.Errors);

			// save to db
			var addToRepositoryResult = m_mockDemoItemService.CreateMockDemoItem(createNewResult.Value);
			var response = MapResponse(createNewResult.Value);

			return addToRepositoryResult.Match(
				item => CreatedAtAction(
					nameof(CreateMockDemoItem),
					new { id = createNewResult.Value.Id },
					response),
				errors => ProblemInController(ref errors));
		}

		[HttpGet("get/{id:guid}")]
		public async Task<IActionResult> GetMockDemoItem(Guid id) {
			// simulate getting from db
			await Task.Delay(1000);

			var getResult = m_mockDemoItemService.GetMockDemoItem(id);

			return getResult.Match(
				item => Ok(MapResponse(item)),
				errors => ProblemInController(ref errors));
		}

		[HttpGet("getmany")]
		public async Task<IActionResult> GetMockDemoItems([FromQuery] Guid[] ids) {
			// simulate adding to db
			await Task.Delay(1000);

			var getResult = m_mockDemoItemService.GetMockDemoItems(ids);

			return getResult.Match(
				_ => Ok(),
				errors => ProblemInController(ref errors));
		}

		[HttpPut("upsert/{id:guid}")]
		public async Task<IActionResult> UpsertMockDemoItems(Guid id, UpsertMockDemoRequest response) {
			await Task.Delay(500);

			var cloneResult = MockDemoModel.CreateClone(id,
				response.Name, response.Description, response.Data, response.CriticalDate);

			if (cloneResult.IsError)
				return ProblemInController(cloneResult.Errors);
			
			var upsertResult = m_mockDemoItemService.Upsert(cloneResult.Value);

			return upsertResult.Match(
				item => Ok(response),
				errors => ProblemInController(ref errors));
		}

		// [HttpPut("upsert")]
		// public async Task<IActionResult> UpsertMockDemoItems(MockDemoResponse response) {
		// 	await Task.Delay(500);
		// 	return Ok(response);
		// }

		[HttpDelete("delete/{id:guid}")]
		public async Task<IActionResult> DeleteMockDemoItems(Guid id) {
			await Task.Delay(500);

			var deleteResult = m_mockDemoItemService.DeleteMockDemoItem(id);

			return deleteResult.Match(
				item => NoContent(),
				errors => ProblemInController(ref errors));
		}

		static MockDemoResponse MapResponse(MockDemoModel mockDemoModel) => new(
			mockDemoModel.Id,
			mockDemoModel.Name,
			mockDemoModel.Description,
			mockDemoModel.Data,
			mockDemoModel.CriticalDate,
			DateTime.UtcNow,
			true);

		static IEnumerable<MockDemoResponse> MapResponseMany(params MockDemoModel[] mockDemoModels) {
			var output = new MockDemoResponse[mockDemoModels.Length];

			for (var i = 0; i < output.Length; i++) {
				output[i] = MapResponse(mockDemoModels[i]);
			}

			return output;
		}

		public MockDemoController(IMockDemoItemService mockDemoItemService) {
			m_mockDemoItemService = mockDemoItemService;
		}

		readonly IMockDemoItemService m_mockDemoItemService;
	}
}