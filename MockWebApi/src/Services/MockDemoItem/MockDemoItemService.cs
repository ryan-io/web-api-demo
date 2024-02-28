using MockWebApi.Models;
using ErrorOr;
using MockWebApi.ServiceErrors;

namespace MockWebApi.Services.MockDemoItem {
	public class MockDemoItemService : IMockDemoItemService {
		public ErrorOr<Created> CreateMockDemoItem(MockDemoModel mockDemoItem) {
			var status = s_repository.TryAdd(mockDemoItem.Id, mockDemoItem);

			if (status)
				return new Created();

			return Errors.MockDemo.IdAlreadyExists(mockDemoItem.Id);
		}

		public ErrorOr<MockDemoModel> GetMockDemoItem(Guid id) {
			if (s_repository.TryGetValue(id, out var value)) {
				return value;
			}

			return Errors.MockDemo.IdNotFound(id);
		}

		public ErrorOr<List<MockDemoModel?>> GetMockDemoItems(params Guid[] ids) {
			List<MockDemoModel?> output = new();
			List<Error>          errors = new();

			for (var i = 0; i < ids.Length; i++) {
				var status = s_repository.TryGetValue(ids[i], out var value);

				if (!status)
					errors.Add(Errors.MockDemo.IdNotFound(ids[i]));
				else
					output.Add(value);
			}

			if (errors.Count > 0)
				return errors;
            
			return output;
		}

		public ErrorOr<Updated> Upsert(MockDemoModel mockDemoItem) {
			if (!s_repository.ContainsKey(mockDemoItem.Id)) {
				CreateMockDemoItem(mockDemoItem);
			}
			else {
				s_repository[mockDemoItem.Id] = mockDemoItem;
			}

			return new Updated();
		}

		public ErrorOr<Deleted> DeleteMockDemoItem(Guid id) {
			var status = s_repository.Remove(id);

			if (!status)
				return Errors.MockDemo.IdNotFound(id);

			return new Deleted();
		}

		// should be a DbContext
		// will store in dictionary for this

		// good use case for static; allow multiple MockDemoItemService objects, but only
		// one repository
		static readonly Dictionary<Guid, MockDemoModel> s_repository = new();
	}
}