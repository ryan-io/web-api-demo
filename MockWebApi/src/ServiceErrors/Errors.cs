using ErrorOr;

namespace MockWebApi.ServiceErrors {
	public static class Errors {
		public static class Models {
			public static Error NameEmpty() {
				return Error.Validation(
					"Models.NameEmpty",
					"The name field on a model cannot me null or empty"
				);
			}
			
			public static Error IntegerDataLessThanZero() {
				return Error.Validation(
					"Models.IntegerDataLessThanZero",
					"Integer data cannot have a value less than 0"
				);
			}
			
			public static Error DescriptionTooLong(int maxLength) {
				return Error.Validation(
					"Models.DescriptionTooLong",
					$"The description for this model exceeds a max length of {maxLength} characters."
				);
			}
		}

		public static class MockDemo {
			// MockDemo that does not exist
			public static Error IdNotFound(Guid id) {
				return Error.NotFound(
					"MockDemo.NotFound",
					$"Could not find MockDemo item with id {id}"
				);
			}

			public static Error IdAlreadyExists(Guid id) {
				return Error.Conflict(
					"MockDemo.IdAlreadyExists",
					$"An element already exists with the id {id}"
				);
			}
		}
	}
}