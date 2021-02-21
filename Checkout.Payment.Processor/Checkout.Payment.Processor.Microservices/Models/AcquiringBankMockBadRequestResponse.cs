using System.Collections.Generic;
using System.Text;
using System.Text.Json;

namespace Checkout.Payment.Processor.MicroServices.Models
{
	public class AcquiringBankMockBadRequestResponse
	{
		public string Title { get; set; }
		public Dictionary<string, List<string>> Errors { get; set; } = new Dictionary<string, List<string>>();

		public string AllErrors => GetFormatedErrors();

		private string GetFormatedErrors()
		{
			StringBuilder formattedErrors = new StringBuilder();
			foreach(var key in Errors)
			{
				formattedErrors.Append($"{key.Key}={string.Join($", {key.Key}=", key.Value)}");
			}

			return formattedErrors.ToString();
		}

		public static AcquiringBankMockBadRequestResponse CreateFromJson(string json)
		{
			var badRequestResponse = new AcquiringBankMockBadRequestResponse();
			var jsonDoc = JsonDocument.Parse(json);
			
			if (jsonDoc.RootElement.TryGetProperty("title", out var title))
			{
				badRequestResponse.Title = title.GetString();
			}
			if (jsonDoc.RootElement.TryGetProperty("errors", out var errors))
			{
				foreach(var property in errors.EnumerateObject())
				{
					var errorsList = new List<string>();

					foreach(var error in property.Value.EnumerateArray())
					{
						errorsList.Add(error.GetString());
					}

					badRequestResponse.Errors.Add(property.Name, errorsList);
				}
			}

			return badRequestResponse;
			//{"type":"https://tools.ietf.org/html/rfc7231#section-6.5.1","title":"One or more validation errors occurred.","status":400,"traceId":"|9d18284d-4d08cdcd3dfafbc0.","errors":{"ExpiryYear":["The field ExpiryYear must be between 2021 and 2041."]
		}
	}
}
